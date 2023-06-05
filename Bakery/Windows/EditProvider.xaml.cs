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
    /// Interaction logic for EditProvider.xaml
    /// </summary>
    public partial class EditProvider : Window
    {
        Provider _tempProvider;

        public EditProvider(Provider provider)
        {
            InitializeComponent();
            
            _tempProvider = provider;

            nameView.Text = provider.Name;
            addressView.Text = provider.Address;
            itnView.Text = provider.ITN;
            phoneView.Text = provider.PhoneNumber;
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

                _tempProvider.Name = nameView.Text;
                _tempProvider.Address = addressView.Text;
                _tempProvider.ITN = itnView.Text;
                _tempProvider.PhoneNumber = phoneView.Text;

                DatabaseControl.UpdateProvider(_tempProvider);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}