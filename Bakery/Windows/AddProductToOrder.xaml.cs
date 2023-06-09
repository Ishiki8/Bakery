using bakery.Classes;
using bakery.Database;
using bakery.Objects;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
        private ObservableCollection<ProductToOrder> _collection;
        private string _mode;
        public AddProductToOrder(string mode = "add")
        {
            InitializeComponent();
            productView.ItemsSource = DatabaseControl.GetProductsForView();
            _mode = mode;

            if (_mode == "add")
            {
                _collection = AddOrder.collection;
            }
            else
            {
                _collection = EditOrder.collection;
            }
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            bool isProductCorrect = false;
            bool isQuantityCorrect = false;
            int quantity = 0;

            quantityView.Text.Trim();

            if (productView.SelectedValue == null)
            {
                wrongProduct.Text = "Выберите продукцию";
            }
            else
            {
                wrongProduct.Text = null;
                isProductCorrect = true;
            }

            if (String.IsNullOrEmpty(quantityView.Text))
            {
                wrongQuantity.Text = "Введите количество";
            }
            else if (!int.TryParse(quantityView.Text, out quantity) || quantity == 0)
            {
                wrongQuantity.Text = "Некорректный ввод";
            }
            else
            {
                wrongQuantity.Text = null;
                isQuantityCorrect = true;
            }

            if (isProductCorrect && isQuantityCorrect)
            {
                ProductToOrder productsOrder = new ProductToOrder()
                {
                    Name = (productView.SelectedItem as Product).Title,
                    Quantity = quantity,
                    Id = (int)productView.SelectedValue
                };

                bool isProductInOrder = false;

                foreach (ProductToOrder product in _collection)
                {
                    if (product.Name == productsOrder.Name)
                    {
                        isProductInOrder = true;
                        MessageBox.Show("Этот вид продукции уже есть в заказе");
                        break;
                    }
                }

                if (!isProductInOrder)
                {
                    switch (_mode)
                    {
                        case "add":
                            AddOrder.collection.Add(productsOrder);
                            break;

                        case "edit":
                            EditOrder.collection.Add(productsOrder);
                            break;
                    }
                    Close();
                }
            } 
        }
    }
}
