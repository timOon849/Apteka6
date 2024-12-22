using Apteka6.Classes;
using System;
using System.Collections.Generic;
using Apteka6.Pages;
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
using Apteka6.DBModel;
using static System.Net.Mime.MediaTypeNames;

namespace Apteka6.Pages
{
    /// <summary>
    /// Логика взаимодействия для ShopCartPage.xaml
    /// </summary>
    public partial class ShopCartPage : Page
    {
        MainWindow _mainWindow;
        public ShopCartPage(MainWindow mainwindow)
        {
            InitializeComponent();
            _mainWindow = mainwindow;
            CartList.ItemsSource = ConnectionClass.apteka.ShoppingCart.Where(z => z.ByerID == ConnectionClass.byers.Id).ToList();
            LoadPaymentMethods();
        }
        private void LoadPaymentMethods()
        {
            // Получаем все методы оплаты из базы данных
            var paymentMethods = ConnectionClass.apteka.PayMethod.ToList();

            // Привязываем данные к ComboBox
            PaymentMethodsComboBox.ItemsSource = paymentMethods;
        }

        // Обработчик для изменения выбора в ComboBox
        private void PaymentMethodsComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            // Получаем выбранный метод оплаты
            var selectedPaymentMethod = (PayMethod)PaymentMethodsComboBox.SelectedItem;
        }
        private void Plus_Click(object sender, RoutedEventArgs e)
        {
            var a = (ShoppingCart)CartList.SelectedItem;
            var b = ConnectionClass.apteka.ShoppingCart.Where(z => z.Id == a.Id).First();
            b.Count += 1;
            ConnectionClass.apteka.SaveChanges();
            RefreshCart();
        }

        private void Minus_Click(object sender, RoutedEventArgs e)
        {
            var a = (ShoppingCart)CartList.SelectedItem;
            var b = ConnectionClass.apteka.ShoppingCart.Where(z => z.Id == a.Id).First();
            b.Count -= 1;
            ConnectionClass.apteka.SaveChanges();
            RefreshCart();
        }
        private void RefreshCart()
        {
            CartList.ItemsSource = ConnectionClass.apteka.ShoppingCart
                .Where(z => z.ByerID == ConnectionClass.byers.Id)
                .ToList();
        }
        class PriceCount
        {
            
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.GoBack();
        }


        private void Button_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new HistoryPage(_mainWindow));
        }
    }
}
