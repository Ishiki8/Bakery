using bakery.Database;
using bakery.Objects;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
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
    /// Interaction logic for Add.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        public AddUser()
        {
            InitializeComponent();
            roleView.ItemsSource = DatabaseControl.GetRolesForView();

            foreach (Role item in roleView.Items)
            {
                if (item.Title == "Пекарь")
                {
                    roleView.SelectedItem = item;
                    break;
                }
            }
        }
        
        private void AddButton_Click(object sender, RoutedEventArgs e)
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
            else if (!DatabaseControl.isLoginUnique(loginView.Text))
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
                DatabaseControl.AddUser(new User
                {
                    FullName = fullNameView.Text.Trim(),
                    Login = loginView.Text,
                    Password = passwordView.Text,
                    RoleId = (int)roleView.SelectedValue
                });

                Close();
            }
        }
    }
}
