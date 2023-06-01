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
    /// Interaction logic for AddCustomer.xaml
    /// </summary>
    public partial class AddCustomer : Window
    {
        public AddCustomer()
        {
            InitializeComponent();
        }
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(nameView.Text))
                {
                    throw new Exception("Не указано наименование!");
                }

                if (String.IsNullOrEmpty(addressView.Text))
                {
                    throw new Exception("Не указан адрес!");
                }

                if (String.IsNullOrEmpty(itnView.Text))
                {
                    throw new Exception("Не указан ИНН!");
                }

                if (String.IsNullOrEmpty(phoneView.Text))
                {
                    throw new Exception("Не указан номер телефона!");
                }

                DatabaseControl.AddCustomer(new Customer
                {
                    Name = nameView.Text,
                    Address = addressView.Text,
                    ITN = itnView.Text,
                    PhoneNumber = phoneView.Text
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
