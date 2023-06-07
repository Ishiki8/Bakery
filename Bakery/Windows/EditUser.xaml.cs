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
            bool isFullNameCorrect = false;
            bool isLoginCorrect = false;
            bool isPasswordCorrect = false;

            if (String.IsNullOrEmpty(fullNameView.Text))
            {
                wrongFullName.Text = "Введите ФИО";
            }
            else if (!Regex.IsMatch(fullNameView.Text, "^(?=.{1,50}$)([А-ЯЁ][а-яё]+[\\-\\s]?){3,4}$"))
            {
                wrongFullName.Text = "Некорректный ввод";
            }
            else
            {
                wrongFullName.Text = null;
                isFullNameCorrect = true;
            }

            if (String.IsNullOrEmpty(loginView.Text))
            {
                wrongLogin.Text = "Введите логин";
            }
            else if (!Regex.IsMatch(loginView.Text, "^(?=.{3,20}$)[a-zA-Z][a-zA-Z0-9-_\\.]"))
            {
                wrongLogin.Text = "Некорректный ввод";
            }
            else if (loginView.Text != _tempUser.Login && !DatabaseControl.isLoginUnique(loginView.Text))
            {
                wrongLogin.Text = "Логин не уникален";
            }
            else
            {
                wrongLogin.Text = null;
                isLoginCorrect = true;
            }

            if (String.IsNullOrEmpty(passwordView.Text))
            {
                wrongPassword.Text = "Введите пароль";
            }
            else if (!Regex.IsMatch(passwordView.Text, "^(?=.{8,30}$)[a-zA-Z0-9]"))
            {
                wrongPassword.Text = "Некорректный ввод";
            }
            else
            {
                wrongPassword.Text = null;
                isPasswordCorrect = true;
            }

            if (isFullNameCorrect && isLoginCorrect && isPasswordCorrect)
            {

                _tempUser.FullName = fullNameView.Text.Trim();
                _tempUser.Login = loginView.Text;
                _tempUser.Password = passwordView.Text;
                _tempUser.RoleId = (int)roleView.SelectedValue;

                DatabaseControl.UpdateUser(_tempUser);

                Close();
            }
        }
    }
}
