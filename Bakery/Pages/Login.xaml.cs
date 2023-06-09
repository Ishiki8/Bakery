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
        public Login(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;

            if (DatabaseControl.GetUsersForView().Count == 0)
            {
                MessageBox.Show("В базе данных отсутствуют пользователи. Обратитесь к системному администратору");
                mainWindow.Close();
            }
        }

        private void Login_Click(object sender, RoutedEventArgs e)
        {
            if (LoginField.Text.Length == 0)
            {
                wrongLogin.Text = "Введите логин";
                wrongLoginOrPassword.Text = null;
            } 
            else
            {
                wrongLogin.Text = null;
            }

            if (Password.Password.Length == 0)
            {
                wrongPassword.Text = "Введите пароль";
                wrongLoginOrPassword.Text = null;
            }
            else
            {
                wrongPassword.Text = null;
            }

            if (LoginField.Text.Length > 0 && Password.Password.Length > 0)
            {
                User user = DatabaseControl.GetCurrentUser(LoginField.Text, Password.Password);

                if (user != null)
                {

                    mainWindow.dbUser = user.FullName;
                    mainWindow.dbUserRole = user.RoleEntity.Title;

                    mainWindow.OpenPage(MainWindow.pages.Menu);
                }
                else
                {
                    wrongLoginOrPassword.Text = "Неправильный логин или пароль";
                }
            }
        }
    }
}
