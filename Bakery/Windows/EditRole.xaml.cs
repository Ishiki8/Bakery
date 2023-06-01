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
    /// Interaction logic for EditRole.xaml
    /// </summary>
    public partial class EditRole : Window
    {
        Role _tempRole;
        public EditRole(Role role)
        {
            InitializeComponent();

            _tempRole = role;

            titleView.Text = role.Title;
        }

        private void EditButton_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (String.IsNullOrEmpty(titleView.Text))
                {
                    throw new Exception("Не указано название!");
                }

                _tempRole.Title = titleView.Text;

                DatabaseControl.UpdateRole(_tempRole);
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
    }
}
