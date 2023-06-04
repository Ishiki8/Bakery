using bakery.Classes;
using bakery.Database;
using bakery.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
    /// Interaction logic for AddOrder.xaml
    /// </summary>
    public partial class AddOrder : Window
    {
        public static ObservableCollection<ProductToOrder> collection = null;
        public AddOrder()
        {
            InitializeComponent();
            customerView.ItemsSource = DatabaseControl.GetCustomersForView();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
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

                DatabaseControl.AddOrder(new Order
                {
                    Date = DateTime.Parse(dateView.Text),
                    Status = statusView.Text,
                    CustomerId = (int)customerView.SelectedValue,
                });

                int orderId = DatabaseControl.GetOrdersForView().Last().Id;

                foreach (ProductToOrder product in collection)
                {
                    DatabaseControl.AddProductToOrder(new Ordered_Product
                    {
                        OrderId = orderId,
                        ProductId = product.Id,
                        Quantity = product.Quantity
                    });
                }
                Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddProductButton_Click(Object sender, RoutedEventArgs e)
        {
            if (collection == null)
            {
                collection = new ObservableCollection<ProductToOrder>();
                productsDataGrid.ItemsSource = collection;
            }

            AddProductToOrder window = new AddProductToOrder();
            window.Owner = this;
            window.ShowDialog();
        }
        private void RemoveProductButton_Click(Object sender, RoutedEventArgs e)
        {
            ProductToOrder product = productsDataGrid.SelectedItem as ProductToOrder;
            collection.Remove(product);
        }
    }
}
