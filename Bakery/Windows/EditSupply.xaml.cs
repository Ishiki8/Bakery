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
    /// Interaction logic for EditSupply.xaml
    /// </summary>
    public partial class EditSupply : Window
    {
        public static ObservableCollection<RawToSupply> collection = null;
        public List<Supplied_Raw> supplied_raw;
        public int _supplyId;
        Supply _tempSupply;

        public EditSupply(Supply supply, int supplyId)
        {
            InitializeComponent();

            providerView.ItemsSource = DatabaseControl.GetProvidersForView();

            _supplyId = supplyId;
            _tempSupply = supply;
            supplied_raw = DatabaseControl.GetSuppliedRawForView().Where(p => p.SupplyId == _supplyId).ToList();

            collection = new ObservableCollection<RawToSupply>();

            foreach (Supplied_Raw raw in supplied_raw)
            {
                collection.Add(new RawToSupply() { Id = raw.RawId, Name = raw.RawEntity.Title, Quantity = raw.Quantity });
            }

            rawDataGrid.ItemsSource = collection;

            dateView.Text = supply.Date.ToString("yyyy-MM-dd");

            foreach (TextBlock item in statusView.Items)
            {
                if (item.Text == supply.Status)
                {
                    statusView.SelectedItem = item;
                    break;
                }
            }

            providerView.SelectedValue = supply.ProviderEntity.Id;
        }

        private void AddRawButton_Click(object sender, RoutedEventArgs e)
        {
            AddRawToSupply window = new AddRawToSupply();
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
        private void EditButton_Click(Object sender, RoutedEventArgs e)
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

                _tempSupply.Date = DateTime.Parse(dateView.Text);
                _tempSupply.Status = statusView.Text;
                _tempSupply.ProviderId = (int)providerView.SelectedValue;

                DatabaseControl.UpdateSupply(_tempSupply);

                foreach (Supplied_Raw raw in supplied_raw)
                {
                    DatabaseControl.RemoveRawFromSupply(raw);
                }

                foreach (RawToSupply raw in collection)
                {
                    DatabaseControl.AddRawToSupply(new Supplied_Raw
                    {
                        SupplyId = _supplyId,
                        RawId = raw.Id,
                        Quantity = raw.Quantity
                    });
                }

                collection.Clear();
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            collection = null;
        }
    }
}
