using bakery.Classes;
using bakery.Database;
using bakery.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Globalization;
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
        public bool _userIsBaker;
        Order _tempOrder;

        public EditOrder(Order order, int orderId, bool userIsBaker = false)
        {
            InitializeComponent();
            customerView.ItemsSource = DatabaseControl.GetCustomersForView();

            _orderId = orderId;
            _tempOrder = order;
            _userIsBaker = userIsBaker;
            ordered_products = DatabaseControl.GetOrderedProductsForView().Where(p => p.OrderId == _orderId).ToList();

            collection = new ObservableCollection<ProductToOrder>();

            foreach (Ordered_Product product in ordered_products)
            {
                collection.Add(new ProductToOrder() { Id = product.ProductId, Name = product.ProductEntity.Title, Quantity = product.Quantity });
            }

            productsDataGrid.ItemsSource = collection;

            dateView.Text = order.Date.ToString("dd/MM/yyyy");

            foreach (TextBlock item in statusView.Items)
            {
                if (item.Text == order.Status)
                {
                    statusView.SelectedItem = item;
                    break;
                }
            }
            
            customerView.SelectedValue = order.CustomerEntity.Id;

            if (_userIsBaker)
            {
                orderEditText.Text = "Просмотр заказа";
                editButton.Content = "Изменить статус";

                dateView.IsEnabled = false;
                customerView.IsEnabled = false;
                statusView.Items.Remove(arrivedStatus);
                //arrivedStatus.Visibility = Visibility.Collapsed;
                addProductButton.Visibility = Visibility.Collapsed;
                buttonColumn.Visibility = Visibility.Collapsed;
            }
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            AddProductToOrder window = new AddProductToOrder("edit");
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
            if (_userIsBaker)
            {
                _tempOrder.Status = statusView.Text;

                DatabaseControl.UpdateOrder(_tempOrder);
                collection.Clear();
                Close();
            }
            else
            {
                DateTime date = DateTime.Now;
                bool isDateCorrect = false;
                bool isProductsCorrect = false;

                dateView.Text.Trim();

                if (String.IsNullOrEmpty(dateView.Text))
                {
                    wrongDate.Text = "Введите дату";
                }
                else if (!DateTime.TryParseExact(dateView.Text.Trim(), MainWindow.dateFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out date) ||
                    date > DateTime.Now)
                {
                    wrongDate.Text = "Некорректный ввод";
                }
                else
                {
                    isDateCorrect = true;
                    wrongDate.Text = null;
                }

                if (collection == null || collection.Count == 0)
                {
                    MessageBox.Show("Выберите минимум один вид продукции");
                }
                else
                {
                    isProductsCorrect = true;
                }

                if (isDateCorrect && isProductsCorrect)
                {
                    _tempOrder.Date = date;
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

                    Close();
                }
            }  
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            collection = null;
        }
    }
}
