using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
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
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public string dbUser;
        public string dbUserRole;

        public static string[] dateFormats =  { "dd/MM/yyyy", "dd.MM.yyyy" };

        public MainWindow()
        {
            InitializeComponent();
            OpenPage(pages.Login);
        }

        public enum pages
        {
            Login,
            Menu
        }

        public void OpenPage(pages pages)
        {
            if (pages == pages.Login)
            {
                MainFrame.Navigate(new Login(this));
            }
            else if (pages == pages.Menu)
            {
                MainFrame.Navigate(new Menu(this, dbUser, dbUserRole));
            }  
        }

        public static DateTime getDateFromString(string text)
        {
            DateTime date;
            var formatStrings = new String[] { "dd/MM/yyyy", "dd.MM.yyyy" };

            DateTime.TryParseExact(text, formatStrings, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out date);

            return date;
        }

    }
}
