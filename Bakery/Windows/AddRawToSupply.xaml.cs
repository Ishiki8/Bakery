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
    /// Interaction logic for AddRawToSupply.xaml
    /// </summary>
    public partial class AddRawToSupply : Window
    {
        private ObservableCollection<RawToSupply> _collection;
        private string _mode;
        public AddRawToSupply(string mode = "add")
        {
            InitializeComponent();
            rawView.ItemsSource = DatabaseControl.GetRawForView();
            _mode = mode;

            if (_mode == "add")
            {
                _collection = AddSupply.collection;
            }
            else
            {
                _collection = EditSupply.collection;
            }
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            bool isRawCorrect = false;
            bool isQuantityCorrect = false;
            int quantity = 0;

            quantityView.Text.Trim();

            if (rawView.SelectedValue == null)
            {
                wrongRaw.Text = "Выберите сырье";
            }
            else
            {
                wrongRaw.Text = null;
                isRawCorrect = true;
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

            if (isRawCorrect && isQuantityCorrect)
            {
                RawToSupply rawSupply = new RawToSupply()
                {
                    Name = (rawView.SelectedItem as Raw).Title,
                    Quantity = quantity,
                    Id = (int)rawView.SelectedValue
                };

                bool isRawInSupply = false;

                foreach (RawToSupply raw in _collection)
                {
                    if (raw.Name == rawSupply.Name)
                    {
                        isRawInSupply = true;
                        MessageBox.Show("Этот вид сырья уже есть в поставке");
                        break;
                    }
                }

                if (!isRawInSupply)
                {
                    switch (_mode)
                    {
                        case "add":
                            AddSupply.collection.Add(rawSupply);
                            break;

                        case "edit":
                            EditSupply.collection.Add(rawSupply);
                            break;
                    }
                    Close();
                }
            }
        }
    }
}
