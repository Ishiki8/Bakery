using bakery.Classes;
using bakery.Database;
using bakery.Objects;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for AddProductToOrder.xaml
    /// </summary>
    public partial class AddProductToOrder : Window
    {
        public AddProductToOrder()
        {
            InitializeComponent();
            productView.ItemsSource = DatabaseControl.GetProductsForView();
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (productView.SelectedValue == null)
                {
                    throw new Exception("Выберите продукцию!");
                }

                int quantity;

                if (String.IsNullOrEmpty(quantityView.Text))
                {
                    throw new Exception("Введите количество!");
                }
                else if (!int.TryParse(quantityView.Text, out quantity))
                {
                    throw new Exception("Количество должно быть числом!");
                }

                ProductToOrder productsOrder = new ProductToOrder() { Name = (productView.SelectedItem as Product).Title, 
                                                                    Quantity = quantity, 
                                                                    Id = (int)productView.SelectedValue };

                foreach (ProductToOrder order in AddOrder.collection)
                {
                    if (order.Name == productsOrder.Name)
                    {
                        throw new Exception("Этот вид продукции уже есть в заказе!");
                    }
                }

                AddOrder.collection.Add(productsOrder);

                Close();
            } 
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            
        }
    }
}
