using Apteka6.Classes;
using Apteka6.Pages;
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

namespace Apteka6
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {

            InitializeComponent();
            mainFrame.Navigate(new AuthPage(this));
            Account.Content = "Карелова";


        }


        private void CartBtn_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new ShopCartPage(this));
        }
        private void CatalogBtn_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new CatalogPage(this));
        }

        private void Account_Click(object sender, RoutedEventArgs e)
        {
            mainFrame.NavigationService.Navigate(new AccountPage());
        }
    }
}
