using bakery.Database;
using bakery.Objects;
using bakery.Windows;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace bakery
{
    /// <summary>
    /// Interaction logic for Menu.xaml
    /// </summary>
    public partial class Menu : Page
    {
        public MainWindow mainWindow;
        
        public Menu(MainWindow _mainWindow)
        {
            InitializeComponent();
            mainWindow = _mainWindow;

            usersDataGrid.ItemsSource = DatabaseControl.GetUsersForView();
            rolesDataGrid.ItemsSource = DatabaseControl.GetRolesForView();
        }

        private void AddUserButton_Click(object sender, RoutedEventArgs e)
        {
            AddUser window = new AddUser();
            window.Owner = mainWindow;
            window.ShowDialog();

            RefreshUsersTable();
            RefreshRolesTable();
        }

        private void AddRoleButton_Click(object sender, RoutedEventArgs e)
        {


        }
        private void EditUserButton_Click(object sender, RoutedEventArgs e)
        {
            User user = usersDataGrid.SelectedItem as User;
            if (user != null)
            {
                EditUser window = new EditUser(user);
                window.Owner = mainWindow;
                window.ShowDialog();

                RefreshUsersTable();
                RefreshRolesTable();
            }
            else
            {
                MessageBox.Show("Выберите запись для редактирования");
            }
        }

        private void RemoveUserButton_Click(object sender, RoutedEventArgs e)
        {
            User user = usersDataGrid.SelectedItem as User;
            DatabaseControl.RemoveUser(user);
            
            RefreshUsersTable();
            RefreshRolesTable();
        }
        private void RemoveRoleButton_Click(object sender, RoutedEventArgs e)
        {
            Role role = rolesDataGrid.SelectedItem as Role;
            DatabaseControl.RemoveRole(role);

            RefreshRolesTable();
        }

        public void RefreshUsersTable()
        {
            usersDataGrid.ItemsSource = null;
            usersDataGrid.ItemsSource = DatabaseControl.GetUsersForView();
        }
        public void RefreshRolesTable()
        {
            rolesDataGrid.ItemsSource = null;
            rolesDataGrid.ItemsSource = DatabaseControl.GetRolesForView();
        }

    }
}
