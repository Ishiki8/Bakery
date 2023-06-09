using bakery.Classes;
using bakery.Database;
using bakery.Objects;
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

            collection = new ObservableCollection<RawToSupply>();

            rawDataGrid.ItemsSource = collection;
            providerView.ItemsSource = DatabaseControl.GetProvidersForView();

        }

        private void AddRawButton_Click(Object sender, RoutedEventArgs e)
        {
            AddRawToSupply window = new AddRawToSupply("add");
            window.Owner = this;
            window.ShowDialog();
        }

        private void RemoveRawButton_Click(Object sender, RoutedEventArgs e)
        {
            RawToSupply raw = rawDataGrid.SelectedItem as RawToSupply;

            if (raw != null)
            {
                collection.Remove(raw);
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
            bool isProviderCorrect = false;
            bool isRawCorrect = false;

            dateView.Text.Trim();

            if (String.IsNullOrEmpty(dateView.Text))
            {
                wrongDate.Text = "Введите дату";
            }
            else if (!DateTime.TryParseExact(dateView.Text.Trim(), MainWindow.dateFormats, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out date) ||
                (date > DateTime.Now && statusView.Text == "Совершена"))
            {
                wrongDate.Text = "Некорректный ввод";
            }
            else
            {
                isDateCorrect = true;
                wrongDate.Text = null;
            }

            if (providerView.SelectedValue == null)
            {
                wrongProvider.Text = "Выберите поставщика";
            }
            else
            {
                isProviderCorrect = true;
                wrongProvider.Text = null;
            }

            if (collection == null || collection.Count == 0)
            {
                MessageBox.Show("Выберите минимум один вид сырья");
            }
            else
            {
                isRawCorrect = true;
            }

            if (isDateCorrect && isProviderCorrect && isRawCorrect)
            {
                DatabaseControl.AddSupply(new Supply
                {
                    Date = date,
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
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            collection = null;
        }
    }
}
