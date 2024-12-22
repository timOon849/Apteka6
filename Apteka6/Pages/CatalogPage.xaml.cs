using Apteka6.Classes;
using Apteka6.DBModel;
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

namespace Apteka6.Pages
{
    /// <summary>
    /// Логика взаимодействия для CatalogPage.xaml
    /// </summary>
    public partial class CatalogPage : Page
    {
        MainWindow _mainWindow;
        public CatalogPage(MainWindow mainwindow)
        {
            InitializeComponent();
            _mainWindow = mainwindow;
            CatalogList.ItemsSource = ConnectionClass.apteka.Tovars.ToList();
        }

        private void AddBtn_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.mainFrame.NavigationService.Navigate(new AddTovarPage(_mainWindow));
        }

        private void RedBtn_Click(object sender, RoutedEventArgs e)
        {
            var a = CatalogList.SelectedItem as Tovars;
            _mainWindow.mainFrame.NavigationService.Navigate(new EditTovarPage(_mainWindow, a));
        }

        private void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Удалить лекарство?", "Удаление", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.No)
            {
                return;
            }
            else
            {
                Tovars b = CatalogList.SelectedItem as Tovars;
                if (b != null)
                {
                    ConnectionClass.apteka.Tovars.Remove(b);
                    ConnectionClass.apteka.SaveChanges();
                    CatalogList.ItemsSource = ConnectionClass.apteka.Tovars.ToList();
                    MessageBox.Show($"Лекарство удалено", "Сообщение", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    MessageBox.Show("Выберете лекарство");
                }
            }
        }

        private void ShopBtn_Click(object sender, RoutedEventArgs e)
        {
            var a = CatalogList.SelectedItem as Tovars;
            var b = ConnectionClass.apteka.ShoppingCart.Where(tovar => tovar.ByerID == ConnectionClass.byers.Id && tovar.TovarID == a.Id).FirstOrDefault();
            if (b != null)
            {
                b.Count += 1;
            }

            else
            {
                ShoppingCart cart = new ShoppingCart();
                cart.TovarID = a.Id;
                cart.Count = 1;
                cart.ByerID = ConnectionClass.byers.Id;
                ConnectionClass.apteka.ShoppingCart.Add(cart);
                ConnectionClass.apteka.SaveChanges();
            }
            
        }

        private void CartBtn_Click(object sender, RoutedEventArgs e)
        {
            _mainWindow.mainFrame.NavigationService.Navigate(new ShopCartPage(_mainWindow));
        }
    }
}
