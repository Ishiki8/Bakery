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
    /// Interaction logic for EditRaw.xaml
    /// </summary>
    public partial class EditRaw : Window
    {
        Raw _tempRaw;
        public EditRaw(Raw raw)
        {
            InitializeComponent();

            _tempRaw = raw;

            titleView.Text = raw.Title;
            weightView.Text = raw.Weight.ToString();
            priceView.Text = raw.Price.ToString();
            
            if (raw.InStock == "Есть")
            {
                inStockView.IsChecked = true;
            }
            else if (raw.InStock == "Нет")
            {
                inStockView.IsChecked = false;
            }
            else
            {
                inStockView.IsChecked = null;
            }
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
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

                _tempRaw.Title = titleView.Text;
                _tempRaw.Weight = weight;
                _tempRaw.Price = price;
                
                if (inStockView.IsChecked == true)
                {
                    _tempRaw.InStock = "Есть";
                }
                else if (inStockView.IsChecked == false)
                {
                    _tempRaw.InStock = "Нет";
                }
                else
                {
                    _tempRaw.InStock = "Неизвестно";
                }

                DatabaseControl.UpdateRaw(_tempRaw);
                Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
