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
    /// Interaction logic for EditCustomer.xaml
    /// </summary>
    public partial class EditCustomer : Window
    {
        Customer _tempCustomer;
        public EditCustomer(Customer customer)
        {
            InitializeComponent();

            _tempCustomer = customer;

            nameView.Text = customer.Name;
            addressView.Text = customer.Address;
            itnView.Text = customer.ITN;
            phoneView.Text = customer.PhoneNumber;
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
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

                _tempCustomer.Name = nameView.Text;
                _tempCustomer.Address = addressView.Text;
                _tempCustomer.ITN = itnView.Text;
                _tempCustomer.PhoneNumber = phoneView.Text;

                DatabaseControl.UpdateCustomer(_tempCustomer);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
