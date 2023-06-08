using bakery.Classes;
using bakery.Database;
using bakery.Objects;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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
using static System.Net.Mime.MediaTypeNames;

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

            collection = new ObservableCollection<ProductToOrder>();

            productsDataGrid.ItemsSource = collection;
            customerView.ItemsSource = DatabaseControl.GetCustomersForView();
        }

        private void AddProductButton_Click(Object sender, RoutedEventArgs e)
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

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            DateTime date = DateTime.Now;
            bool isDateCorrect = false;
            bool isCustomerCorrect = false;
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

            if (customerView.SelectedValue == null)
            {
                wrongCustomer.Text = "Выберите заказчика";
            }
            else
            {
                isCustomerCorrect = true;
                wrongCustomer.Text = null;
            }

            if (collection == null || collection.Count == 0)
            {
                MessageBox.Show("Выберите минимум один вид продукции");
            }
            else
            {
                isProductsCorrect = true;
            }

            if (isDateCorrect && isCustomerCorrect && isProductsCorrect)
            {
                DatabaseControl.AddOrder(new Order
                {
                    Date = date,
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
        }     

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            collection = null;
        }
    }
}
