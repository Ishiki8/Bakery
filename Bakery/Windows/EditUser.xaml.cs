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
    /// Interaction logic for EditUser.xaml
    /// </summary>
    public partial class EditUser : Window
    {
        User _tempUser;
        public EditUser(User user)
        {
            InitializeComponent();
            
            _tempUser = user;
            roleView.ItemsSource = DatabaseControl.GetRolesForView();
            
            fullNameView.Text = user.FullName;
            loginView.Text = user.Login;
            passwordView.Text = user.Password;
            roleView.SelectedValue = user.RoleEntity.Id;
        }
        private void EditButton_Click(object sender, RoutedEventArgs e)
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

                _tempUser.FullName = fullNameView.Text;
                _tempUser.Login = loginView.Text;
                _tempUser.Password = passwordView.Text;
                _tempUser.RoleId = (int)roleView.SelectedValue;

                DatabaseControl.UpdateUser(_tempUser);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
