﻿using System;
using System.Collections.Generic;
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

        public static bool TextIsDate(string text)
        {
            var dateFormat = "yyyy-MM-dd";
            DateTime scheduleDate;
            if (DateTime.TryParseExact(text, dateFormat, DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out scheduleDate))
            {
                return true;
            }
            return false;
        }

    }
}
