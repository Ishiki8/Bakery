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
    /// Interaction logic for Add.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        public AddUser()
        {
            InitializeComponent();
            roleView.ItemsSource = DatabaseControl.GetRolesForView();
        }
        
        private void AddButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(fullNameView.Text))
                {
                    throw new Exception("Не указано ФИО!");
                }

                if (String.IsNullOrEmpty(loginView.Text))
                {
                    throw new Exception("Не указан логин!");
                }

                if (String.IsNullOrEmpty(passwordView.Text))
                {
                    throw new Exception("Не указан пароль!");
                }

                if (roleView.SelectedValue == null)
                {
                    throw new Exception("Не указана роль!");
                }

                DatabaseControl.AddUser(new User
                {
                    FullName = fullNameView.Text,
                    Login = loginView.Text,
                    Password = passwordView.Text,
                    RoleId = (int)roleView.SelectedValue
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
