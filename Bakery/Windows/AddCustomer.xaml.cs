using bakery.Database;
using bakery.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            bool isNameCorrect = false;
            bool isAddressCorrect = false;
            bool isITNCorrect = false;
            bool isPhoneCorrect = false;

            if (String.IsNullOrEmpty(nameView.Text))
            {
                wrongName.Text = "Введите наименование";
            }
            else if (nameView.Text.Length > 50)
            {
                wrongName.Text = "Некорректный ввод";
            }
            else if (!DatabaseControl.isNameUnique(nameView.Text, "customer"))
            {
                wrongName.Text = "Наименование не уникально";
            }
            else
            {
                isNameCorrect = true;
                wrongName.Text = null;
            }

            if (String.IsNullOrEmpty(addressView.Text))
            {
                wrongAddress.Text = "Введите адрес";
            }
            else if (addressView.Text.Length > 150)
            {
                wrongAddress.Text = "Некорректный ввод";
            }
            else
            {
                isAddressCorrect = true;
                wrongAddress.Text = null;
            }

            if (String.IsNullOrEmpty(itnView.Text))
            {
                wrongITN.Text = "Введите ИНН";
            }
            else if (!Regex.IsMatch(itnView.Text, "^\\d{10}(\\d{2})?$"))
            {
                wrongITN.Text = "Некорректный ввод";
            }
            else if (!DatabaseControl.isITNUnique(itnView.Text, "customer"))
            {
                wrongITN.Text = "ИНН не уникален";
            }
            else
            {
                isITNCorrect = true;
                wrongITN.Text = null;
            }

            if (String.IsNullOrEmpty(phoneView.Text))
            {
                wrongPhone.Text = "Введите номер телефона";
            }
            else if (!Regex.IsMatch(phoneView.Text, "^9\\d{9}$"))
            {
                wrongPhone.Text = "Некорректный ввод";
            }
            else if (!DatabaseControl.isPhoneUnique(phoneView.Text, "customer"))
            {
                wrongPhone.Text = "Номер не уникален";
            }
            else
            {
                isPhoneCorrect = true;
                wrongPhone.Text = null;
            }
            
            if (isNameCorrect && isAddressCorrect && isITNCorrect && isPhoneCorrect)
            {
                DatabaseControl.AddCustomer(new Customer
                {
                    Name = nameView.Text.Trim(),
                    Address = addressView.Text.Trim(),
                    ITN = itnView.Text,
                    PhoneNumber = phoneView.Text
                });
                Close();
            }
        }
    }
}
