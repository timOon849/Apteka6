using Apteka6.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Apteka6.DBModel;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Wpf.Ui;

namespace Apteka6.Pages
{
    /// <summary>
    /// Логика взаимодействия для PayMethodPage.xaml
    /// </summary>
    /// 

    public partial class PayMethodPage : Page
    {
        MainWindow _mainWindow;

        public PayMethodPage(MainWindow mainwindow)
        {
            InitializeComponent();
            _mainWindow = mainwindow;
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
    }
}
