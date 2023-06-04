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
        public static ObservableCollection<ProductToOrder> collection = new ObservableCollection<ProductToOrder>();
        Order _tempOrder;
        public EditOrder(Order order, int orderId)
        {
            InitializeComponent();

            _tempOrder = order;

            List<Ordered_Product> ordered_products = DatabaseControl.GetOrderedProductsForView().Where(p => p.OrderId == orderId).ToList();

            Console.WriteLine(ordered_products);

            foreach (Ordered_Product product in ordered_products)
            {
                collection.Add(new ProductToOrder() { Id = product.ProductId, Name = product.ProductEntity.Title, Quantity = product.Quantity });
            }

            // ДОДЕЛАТЬ
            productsDataGrid.ItemsSource = collection;

            dateView.Text = order.Date.ToString();
            statusView.SelectedValue = order.Status.ToString();
            customerView.Text = order.CustomerEntity.Name;
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void RemoveProductButton_Click(Object sender, RoutedEventArgs e)
        {

        }
        private void EditButton_Click(Object sender, RoutedEventArgs e)
        {

        }
    }
}
