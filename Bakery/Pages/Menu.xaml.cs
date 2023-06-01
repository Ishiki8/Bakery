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
            customersDataGrid.ItemsSource = DatabaseControl.GetCustomersForView();
            ordersDataGrid.ItemsSource = DatabaseControl.GetOrdersForView();
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
            AddRole window = new AddRole();
            window.Owner = mainWindow;
            window.ShowDialog();

            RefreshRolesTable();
        }
        private void AddCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            AddCustomer window = new AddCustomer();
            window.Owner = mainWindow;
            window.ShowDialog();

            RefreshCustomersTable();
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
        private void EditRoleButton_Click(Object sender, RoutedEventArgs e)
        {
            Role role = rolesDataGrid.SelectedItem as Role;
            
            if (role != null)
            {
                EditRole window = new EditRole(role);
                window.Owner = mainWindow;
                window.ShowDialog();

                RefreshRolesTable();
                RefreshUsersTable();
            }
            else
            {
                MessageBox.Show("Выберите запись для редактирования");
            }
        }
        private void EditCustomerButton_Click(Object sender, RoutedEventArgs e)
        {
            Customer customer = customersDataGrid.SelectedItem as Customer;

            if (customer != null)
            {
                EditCustomer window = new EditCustomer(customer);
                window.Owner = mainWindow;
                window.ShowDialog();

                RefreshCustomersTable();
                RefreshOrdersTable();
            }
        }

        private void RemoveUserButton_Click(object sender, RoutedEventArgs e)
        {
            User user = usersDataGrid.SelectedItem as User;
            
            if (user != null)
            {
                DatabaseControl.RemoveUser(user);

                RefreshUsersTable();
                RefreshRolesTable();
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления");
            }
            
        }
        private void RemoveRoleButton_Click(object sender, RoutedEventArgs e)
        {
            Role role = rolesDataGrid.SelectedItem as Role;

            if (role != null)
            {
                DatabaseControl.RemoveRole(role);

                RefreshRolesTable();
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления");
            }
            
        }
        private void RemoveCustomerButton_Click(object sender, RoutedEventArgs e)
        {
            Customer customer = customersDataGrid.SelectedItem as Customer;

            if (customer != null)
            {
                DatabaseControl.RemoveCustomer(customer);
                RefreshCustomersTable();
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления");
            }
        }
        private void RemoveOrderButton_Click(object sender, RoutedEventArgs e)
        {
            Order order = ordersDataGrid.SelectedItem as Order;

            if (order != null)
            {
                DatabaseControl.RemoveOrder(order);
                RefreshOrdersTable();
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления");
            }
        }

        public void RefreshUsersTable()
        {
            usersDataGrid.ItemsSource = null;

            if (searchUserByFullNameView.Text != null)
            {
                usersDataGrid.ItemsSource = DatabaseControl.GetUsersForViewByFullName(searchUserByFullNameView.Text);
            }
            else
            {
                usersDataGrid.ItemsSource = DatabaseControl.GetUsersForView();
            }
            
        }
        public void RefreshRolesTable()
        {
            rolesDataGrid.ItemsSource = null;
            rolesDataGrid.ItemsSource = DatabaseControl.GetRolesForView();
        }
        public void RefreshCustomersTable()
        {
            customersDataGrid.ItemsSource = null;
            customersDataGrid.ItemsSource = DatabaseControl.GetCustomersForView();
        }
        public void RefreshOrdersTable()
        {
            ordersDataGrid.ItemsSource = null;
            ordersDataGrid.ItemsSource = DatabaseControl.GetOrdersForView();
        }

        private void SearchUserByFullName_KeyUp(object sender, KeyEventArgs e)
        {
            usersDataGrid.ItemsSource = null;
            usersDataGrid.ItemsSource = DatabaseControl.GetUsersForViewByFullName(searchUserByFullNameView.Text);
        }
        private void SearchCustomerByName_KeyUp(object sender, KeyEventArgs e)
        {
            customersDataGrid.ItemsSource = null;
            customersDataGrid.ItemsSource = DatabaseControl.GetCustomersForViewByName(searchCustomerByNameView.Text);
        }
    }
}
