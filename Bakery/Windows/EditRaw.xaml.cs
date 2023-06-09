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
            bool isTitleCorrect = false;
            bool isWeightCorrect = false;
            bool isPriceCorrect = false;

            decimal weight = 0;
            decimal price = 0;

            titleView.Text.Trim();
            weightView.Text.Trim();
            priceView.Text.Trim();

            if (String.IsNullOrEmpty(titleView.Text))
            {
                wrongTitle.Text = "Введите название";
            }
            else if (titleView.Text != _tempRaw.Title && !DatabaseControl.isNameUnique(titleView.Text, "raw"))
            {
                wrongTitle.Text = "Название не уникально";
            }
            else if (titleView.Text.Length > 50)
            {
                wrongTitle.Text = "Некорректный ввод";
            }
            else
            {
                wrongTitle.Text = null;
                isTitleCorrect = true;
            }

            if (String.IsNullOrEmpty(weightView.Text))
            {
                wrongWeight.Text = "Введите вес";
            }
            else if (!decimal.TryParse(weightView.Text, out weight) || weight == 0 || weight > 99.99M)
            {
                wrongWeight.Text = "Некорректный ввод";
            }
            else
            {
                wrongWeight.Text = null;
                isWeightCorrect = true;
            }

            if (String.IsNullOrEmpty(priceView.Text))
            {
                wrongPrice.Text = "Введите цену";
            }
            else if (!decimal.TryParse(priceView.Text, out price) || price == 0 || price > 99999.99M)
            {
                wrongPrice.Text = "Некорректный ввод";
            }
            else
            {
                wrongPrice.Text = null;
                isPriceCorrect = true;
            }

            if (isTitleCorrect && isWeightCorrect && isPriceCorrect)
            {
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
        }
    }
}
