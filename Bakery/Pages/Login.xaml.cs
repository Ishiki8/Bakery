using bakery.Database;
using bakery.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace bakery
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class Login : Page
    {
        public MainWindow mainWindow;
        public List<User> users;
        public Login(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            users = DatabaseControl.GetUsersForView();
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (LoginField.Text.Length == 0 && Password.Password.Length == 0)
            {
                wrongLogin.Text = "Введите логин!";
                wrongPassword.Text = "Введите пароль!";
            }
            else
            {
                if (LoginField.Text.Length > 0)
                {
                    wrongLogin.Text = "";

                    if (Password.Password.Length > 0)
                    {
                        wrongPassword.Text = "";

                        bool flag = false;

                        foreach (User user in users)
                        {
                            if (user.Login == LoginField.Text && user.Password == Password.Password)
                            {
                                flag = true;
                                mainWindow.dbUser = user.FullName;
                                mainWindow.dbUserRole = user.RoleEntity.Title;
                                break;
                            }
                        }

                        if (flag)
                        {
                            mainWindow.OpenPage(MainWindow.pages.Menu);
                        }
                        else
                        {
                            MessageBox.Show("Неправильный логин или пароль!");
                        }
                    }
                    else
                    {
                        wrongPassword.Text = "Введите пароль!";
                    }
                }
                else
                {
                    wrongLogin.Text = "Введите логин!";
                }
            }
        }
    }
}
