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
    /// Interaction logic for AddProduct.xaml
    /// </summary>
    public partial class AddProduct : Window
    {
        public AddProduct()
        {
            InitializeComponent();
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(titleView.Text))
                {
                    throw new Exception("Не указано название!");
                }

                if (String.IsNullOrEmpty(weightView.Text))
                {
                    throw new Exception("Не указан вес!");
                }

                if (String.IsNullOrEmpty(priceView.Text))
                {
                    throw new Exception("Не указана цена!");
                }

                decimal weight;
                decimal price;

                if (!decimal.TryParse(weightView.Text, out weight))
                {
                    throw new Exception("Вес должен быть числом!");
                }

                if (!decimal.TryParse(priceView.Text, out price))
                {
                    throw new Exception("Цена должна быть числом!");
                }

                DatabaseControl.AddProduct(new Product
                {
                    Title = titleView.Text,
                    Weight = weight,
                    Price = price,
                });
                Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
