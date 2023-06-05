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
        private string dbUser;
        private string dbUserRole;

        public Menu(MainWindow _mainWindow, string _dbUser = "", string _dbUserRole = "")
        {
            InitializeComponent();
            mainWindow = _mainWindow;
            dbUser = _dbUser;
            dbUserRole = _dbUserRole;

            dbUserView.Text = dbUser;
            dbUserRoleView.Text = dbUserRole;

            if (dbUserRole == "Администратор" || dbUserRole == "Administrator")
            {
                usersDataGrid.ItemsSource = DatabaseControl.GetUsersForView();
                rolesDataGrid.ItemsSource = DatabaseControl.GetRolesForView();
                
            }
            else
            {
                tabControl.Items.Remove(usersRolesTabItem);

                customersIdColumn.Visibility = Visibility.Collapsed;
                ordersIdColumn.Visibility = Visibility.Collapsed;
                providersIdColumn.Visibility = Visibility.Collapsed;
                suppliesIdColumn.Visibility = Visibility.Collapsed;
                productsIdColumn.Visibility = Visibility.Collapsed;
                rawIdColumn.Visibility = Visibility.Collapsed;
            }

            if (dbUserRole != "Пекарь" && dbUserRole != "Baker")
            {
                customersDataGrid.ItemsSource = DatabaseControl.GetCustomersForView();
                ordersDataGrid.ItemsSource = DatabaseControl.GetOrdersForView();
                providersDataGrid.ItemsSource = DatabaseControl.GetProvidersForView();
                suppliesDataGrid.ItemsSource = DatabaseControl.GetSuppliesForView();
            }
            else
            {
                customersOrdersTabItem.Header = "Заказы";
                customersDataGrid.Visibility = Visibility.Collapsed;
                customersText.Visibility = Visibility.Collapsed;
                customersSearch.Visibility = Visibility.Collapsed;
                addCustomerButton.Visibility = Visibility.Collapsed;
                customersColumn.Width = new GridLength(0);

                ordersContextEdit.Header = "Подробнее";
                ordersContextRemove.Visibility = Visibility.Collapsed;
                addOrderButton.Visibility = Visibility.Collapsed;
                ordersButtons.Visibility = Visibility.Collapsed;
                ordersDateColumn.Width = DataGridLength.Auto;
                ordersStatusColumn.Width = DataGridLength.Auto;
                ordersCustomerColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);

                ordersDataGrid.ItemsSource = DatabaseControl.GetOrdersForView().Where(p => p.Status != "Доставлен");
                
                providersSuppliesTabItem.Visibility = Visibility.Collapsed;

                productsRawTabItem.Header = "Сырье";
                productsDataGrid.Visibility = Visibility.Collapsed;
                productsText.Visibility = Visibility.Collapsed;
                productsSearch.Visibility = Visibility.Collapsed;
                addProductButton.Visibility = Visibility.Collapsed;
                productsColumn.Width = new GridLength(0);

                addRawButton.Visibility = Visibility.Collapsed;
                rawPriceColumn.Visibility = Visibility.Collapsed;
                rawContext.Visibility = Visibility.Collapsed;
                rawButtons.Visibility = Visibility.Collapsed;
                rawTitleColumn.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
                rawWeightColumn.Width = DataGridLength.Auto;
                rawInStockColumn.Width = DataGridLength.Auto;
            }
            
            productsDataGrid.ItemsSource = DatabaseControl.GetProductsForView();
            rawDataGrid.ItemsSource = DatabaseControl.GetRawForView();

        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            mainWindow.OpenPage(MainWindow.pages.Login);
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

        private void AddOrderButton_Click(object sender, RoutedEventArgs e)
        {
            AddOrder window = new AddOrder();
            window.Owner = mainWindow;
            window.ShowDialog();

            RefreshOrdersTable();
        }

        private void AddProductButton_Click(object sender, RoutedEventArgs e)
        {
            AddProduct window = new AddProduct();
            window.Owner = mainWindow;
            window.ShowDialog();

            RefreshProductsTable();
        }

        private void AddProviderButton_Click(object sender, RoutedEventArgs e)
        {
            AddProvider window = new AddProvider();
            window.Owner = mainWindow;
            window.ShowDialog();

            RefreshProvidersTable();
        }

        private void AddSupplyButton_Click(object sender, RoutedEventArgs e)
        {
            AddSupply window = new AddSupply();
            window.Owner = mainWindow;
            window.ShowDialog();

            RefreshSuppliesTable();
        }

        private void AddRawButton_Click(object sender, RoutedEventArgs e)
        {
            AddRaw window = new AddRaw();
            window.Owner = mainWindow;
            window.ShowDialog();

            RefreshRawTable();
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
            else
            {
                MessageBox.Show("Выберите запись для редактирования");
            }
        }

        private void EditOrderButton_Click(Object sender, RoutedEventArgs e)
        {
            Order order = ordersDataGrid.SelectedItem as Order;

            if (order != null)
            {
                EditOrder window;

                if (dbUserRole == "Пекарь" || dbUserRole == "Baker")
                {
                    window = new EditOrder(order, order.Id, true);
                }
                else
                {
                    window = new EditOrder(order, order.Id);
                }
                
                window.Owner = mainWindow;
                window.ShowDialog();

                RefreshOrdersTable();
                RefreshCustomersTable();
            }
            else
            {
                MessageBox.Show("Выберите запись для редактирования");
            }
        }

        private void EditProductButton_Click(Object sender, RoutedEventArgs e)
        {
            Product product = productsDataGrid.SelectedItem as Product;

            if (product != null)
            {
                EditProduct window = new EditProduct(product);
                window.Owner = mainWindow;
                window.ShowDialog();

                RefreshProductsTable();
            }
            else
            {
                MessageBox.Show("Выберите запись для редактирования");
            }
        }

        private void EditProviderButton_Click(Object sender, RoutedEventArgs e)
        {
            Provider provider = providersDataGrid.SelectedItem as Provider;

            if (provider != null)
            {
                EditProvider window = new EditProvider(provider);
                window.Owner = mainWindow;
                window.ShowDialog();

                RefreshProvidersTable();
                RefreshSuppliesTable();
            }
            else
            {
                MessageBox.Show("Выберите запись для редактирования");
            }
        }

        private void EditSupplyButton_Click(Object sender, RoutedEventArgs e)
        {
            Supply supply = suppliesDataGrid.SelectedItem as Supply;

            if (supply != null)
            {
                EditSupply window = new EditSupply(supply, supply.Id);
                window.Owner = mainWindow;
                window.ShowDialog();

                RefreshSuppliesTable();
                RefreshProvidersTable();
            }
            else
            {
                MessageBox.Show("Выберите запись для редактирования");
            }
        }

        private void EditRawButton_Click(Object sender, RoutedEventArgs e)
        {
            Raw raw = rawDataGrid.SelectedItem as Raw;

            if (raw != null)
            {
                EditRaw window = new EditRaw(raw);
                window.Owner = mainWindow;
                window.ShowDialog();

                RefreshRawTable();
            }
            else
            {
                MessageBox.Show("Выберите запись для редактирования");
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

        private void RemoveProductButton_Click(Object sender, RoutedEventArgs e)
        {
            Product product = productsDataGrid.SelectedItem as Product;

            if (product != null)
            {
                DatabaseControl.RemoveProduct(product);
                RefreshProductsTable();
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления");
            }
        }

        private void RemoveProviderButton_Click(object sender, RoutedEventArgs e)
        {
            Provider provider = providersDataGrid.SelectedItem as Provider;

            if (provider != null)
            {
                DatabaseControl.RemoveProvider(provider);
                RefreshProvidersTable();
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления");
            }
        }

        private void RemoveSupplyButton_Click(object sender, RoutedEventArgs e)
        {
            Supply supply = suppliesDataGrid.SelectedItem as Supply;

            if (supply != null)
            {
                DatabaseControl.RemoveSupply(supply);
                RefreshSuppliesTable();
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления");
            }
        }

        private void RemoveRawButton_Click(Object sender, RoutedEventArgs e)
        {
            Raw raw = rawDataGrid.SelectedItem as Raw;

            if (raw != null)
            {
                DatabaseControl.RemoveRaw(raw);
                RefreshRawTable();
            }
            else
            {
                MessageBox.Show("Выберите запись для удаления");
            }
        }

        public void RefreshUsersTable()
        {
            usersDataGrid.ItemsSource = null;
            usersDataGrid.ItemsSource = DatabaseControl.GetUsersForView(searchUserByFullNameView.Text);
 
        }

        public void RefreshRolesTable()
        {
            rolesDataGrid.ItemsSource = null;
            rolesDataGrid.ItemsSource = DatabaseControl.GetRolesForView();
        }

        public void RefreshCustomersTable()
        {
            customersDataGrid.ItemsSource = null;
            customersDataGrid.ItemsSource = DatabaseControl.GetCustomersForView(searchCustomerByNameView.Text);
        }

        public void RefreshOrdersTable()
        {
            ordersDataGrid.ItemsSource = null;

            if (dbUserRole == "Пекарь" || dbUserRole == "Baker")
            {
                ordersDataGrid.ItemsSource = DatabaseControl.GetOrdersForView(searchOrderByCustomerView.Text)
                                                            .Where(p => p.Status != "Доставлен");
            }
            else
            {
                ordersDataGrid.ItemsSource = DatabaseControl.GetOrdersForView(searchOrderByCustomerView.Text);
            }    
        }

        public void RefreshProductsTable()
        {
            productsDataGrid.ItemsSource= null;
            productsDataGrid.ItemsSource = DatabaseControl.GetProductsForView(searchProductByNameView.Text);
        }

        public void RefreshProvidersTable()
        {
            providersDataGrid.ItemsSource = null;
            providersDataGrid.ItemsSource = DatabaseControl.GetProvidersForView(searchProviderByNameView.Text);
        }

        public void RefreshSuppliesTable()
        {
            suppliesDataGrid.ItemsSource = null;
            suppliesDataGrid.ItemsSource = DatabaseControl.GetSuppliesForView(searchSupplyByProviderView.Text);
        }

        public void RefreshRawTable()
        {
            rawDataGrid.ItemsSource = null;
            rawDataGrid.ItemsSource = DatabaseControl.GetRawForView(searchRawByNameView.Text);
        }

        private void SearchUserByFullName_KeyUp(object sender, KeyEventArgs e)
        {
            RefreshUsersTable();
        }

        private void SearchCustomerByName_KeyUp(object sender, KeyEventArgs e)
        {
            RefreshCustomersTable();
        }

        private void SearchProductByName_KeyUp(object sender, KeyEventArgs e)
        {
            RefreshProductsTable();
        }

        private void SearchOrderByCustomer_KeyUp(object sender, KeyEventArgs e)
        {
            RefreshOrdersTable();
        }

        private void SearchProviderByName_KeyUp(object sender, KeyEventArgs e)
        {
            RefreshProvidersTable();
        }

        private void SearchRawByName_KeyUp(object sender, KeyEventArgs e)
        {
            RefreshRawTable();
        }

        private void SearchSupplyByProvider_KeyUp(object sender, KeyEventArgs e)
        {
            RefreshSuppliesTable();
        }
    }
}
