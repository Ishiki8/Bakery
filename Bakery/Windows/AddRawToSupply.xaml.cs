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
    /// Interaction logic for AddRawToSupply.xaml
    /// </summary>
    public partial class AddRawToSupply : Window
    {
        public AddRawToSupply()
        {
            InitializeComponent();
            rawView.ItemsSource = DatabaseControl.GetRawForView();
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (rawView.SelectedValue == null)
                {
                    throw new Exception("Выберите сырье!");
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

                RawToSupply rawToSupply = new RawToSupply()
                {
                    Name = (rawView.SelectedItem as Raw).Title,
                    Quantity = quantity,
                    Id = (int)rawView.SelectedValue
                };

                if (!(AddSupply.collection == null))
                {
                    foreach (RawToSupply raw in AddSupply.collection)
                    {
                        if (raw.Name == rawToSupply.Name)
                        {
                            throw new Exception("Этот вид сырья уже есть в поставке!");
                        }
                    }

                    AddSupply.collection.Add(rawToSupply);
                }
                
                if (!(EditSupply.collection == null))
                {
                    foreach (RawToSupply raw in EditSupply.collection)
                    {
                        if (raw.Name == rawToSupply.Name)
                        {
                            throw new Exception("Этот вид сырья уже есть в поставке!");
                        }
                    }

                    EditSupply.collection.Add(rawToSupply);
                }

                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

        }
    }
}
