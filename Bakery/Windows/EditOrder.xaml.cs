using bakery.Classes;
using bakery.Database;
using bakery.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace bakery.Windows
{
    /// <summary>
    /// Interaction logic for EditOrder.xaml
    /// </summary>
    public partial class EditOrder : Window
    {
        public static ObservableCollection<ProductToOrder> collection = null;
        public List<Ordered_Product> ordered_products;
        public int _orderId;
        Order _tempOrder;
        public EditOrder(Order order, int orderId)
        {
            InitializeComponent();
            customerView.ItemsSource = DatabaseControl.GetCustomersForView();

            _orderId = orderId;
            _tempOrder = order;
            ordered_products = DatabaseControl.GetOrderedProductsForView().Where(p => p.OrderId == _orderId).ToList();

            collection = new ObservableCollection<ProductToOrder>();

            foreach (Ordered_Product product in ordered_products)
            {
                collection.Add(new ProductToOrder() { Id = product.ProductId, Name = product.ProductEntity.Title, Quantity = product.Quantity });
            }

            productsDataGrid.ItemsSource = collection;

            dateView.Text = order.Date.ToString("yyyy-MM-dd");

            foreach (TextBlock item in statusView.Items)
            {
                if (item.Text == order.Status)
                {
                    statusView.SelectedItem = item;
                    break;
                }
            }
            
            customerView.SelectedValue = order.CustomerEntity.Id;
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            AddProductToOrder window = new AddProductToOrder();
            window.Owner = this;
            window.ShowDialog();
        }

        private void RemoveProductButton_Click(Object sender, RoutedEventArgs e)
        {
            ProductToOrder product = productsDataGrid.SelectedItem as ProductToOrder;

            if (product != null)
            {
                collection.Remove(product);
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления");
            }
        }
        private void EditButton_Click(Object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(dateView.Text))
                {
                    throw new Exception("Не указана дата!");
                }
                else if (!MainWindow.TextIsDate(dateView.Text))
                {
                    throw new Exception("Дата введена неверно. Верный формат гггг-мм-дд!");
                }
                else if (DateTime.Parse(dateView.Text) > DateTime.Now)
                {
                    throw new Exception("Дата заказа не может превышать текущую!");
                }

                if (customerView.SelectedValue == null)
                {
                    throw new Exception("Не указан заказчик!");
                }

                if (collection.Count == 0)
                {
                    throw new Exception("Не выбран ни один вид продукции!");
                }

                _tempOrder.Date = DateTime.Parse(dateView.Text);
                _tempOrder.Status = statusView.Text;
                _tempOrder.CustomerId = (int)customerView.SelectedValue;

                DatabaseControl.UpdateOrder(_tempOrder);

                foreach (Ordered_Product product in ordered_products)
                {
                    DatabaseControl.RemoveProductFromOrder(product);
                }

                foreach (ProductToOrder product in collection)
                {
                    DatabaseControl.AddProductToOrder(new Ordered_Product
                    {
                        OrderId = _orderId,
                        ProductId = product.Id,
                        Quantity = product.Quantity
                    });
                }

                collection.Clear();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            collection = null;
        }
    }
}
