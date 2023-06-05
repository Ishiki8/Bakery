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
    /// Interaction logic for AddSupply.xaml
    /// </summary>
    public partial class AddSupply : Window
    {
        public static ObservableCollection<RawToSupply> collection = null;
        public AddSupply()
        {
            InitializeComponent();
            providerView.ItemsSource = DatabaseControl.GetProvidersForView();

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
                else if (DateTime.Parse(dateView.Text) > DateTime.Now && statusView.Text == "Совершена")
                {
                    throw new Exception("Поставка не может быть совершена, поскольку дата превышает текущую!");
                }

                if (providerView.SelectedValue == null)
                {
                    throw new Exception("Не указан поставщик!");
                }

                if (collection.Count == 0)
                {
                    throw new Exception("Не выбран ни один вид сырья!");
                }

                DatabaseControl.AddSupply(new Supply
                {
                    Date = DateTime.Parse(dateView.Text),
                    Status = statusView.Text,
                    ProviderId = (int)providerView.SelectedValue,
                });

                int supplyId = DatabaseControl.GetSuppliesForView().Last().Id;

                foreach (RawToSupply raw in collection)
                {
                    DatabaseControl.AddRawToSupply(new Supplied_Raw
                    {
                        SupplyId = supplyId,
                        RawId = raw.Id,
                        Quantity = raw.Quantity
                    });
                }
                Close();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void AddRawButton_Click(Object sender, RoutedEventArgs e)
        {
            if (collection == null)
            {
                collection = new ObservableCollection<RawToSupply>();
                rawDataGrid.ItemsSource = collection;
            }

            AddRawToSupply window = new AddRawToSupply();
            window.Owner = this;
            window.ShowDialog();
        }

        private void RemoveRawButton_Click(Object sender, RoutedEventArgs e)
        {
            RawToSupply raw = rawDataGrid.SelectedItem as RawToSupply;
            collection.Remove(raw);
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            collection = null;
        }
    }
}
