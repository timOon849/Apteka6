using Apteka6.Classes;
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
    /// Логика взаимодействия для AuthPage.xaml
    /// </summary>
    public partial class AuthPage : Page
    {
        MainWindow mainWindow1;
        public AuthPage(MainWindow mainwindow)
        {
            InitializeComponent();
            mainWindow1 = mainwindow;
        }

        private void LogBtn_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (string.IsNullOrEmpty(LogBox.Text) || string.IsNullOrEmpty(PassBox.Password)) //вход в аккаунт
                {
                    MessageBox.Show("Пожалуйста заполните логин и пароль!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    var a = ConnectionClass.apteka.Logins.Where(log => log.Login == LogBox.Text && log.Password == PassBox.Password).FirstOrDefault();
                    if (a != null)
                    {
                        var b = ConnectionClass.apteka.Byers.Where(byer => byer.Login == a.Login).FirstOrDefault();
                        var c = ConnectionClass.apteka.Employee.Where(emp => emp.Login == a.Login).FirstOrDefault();
                        if (b != null)
                        {
                            ConnectionClass.byers = b;
                            mainWindow1.mainFrame.NavigationService.Navigate(new CatalogPage(mainWindow1));
                        }
                        else if (a != null)
                        {
                            ConnectionClass.employee = c;
                            mainWindow1.mainFrame.NavigationService.Navigate(new CatalogPage(mainWindow1));
                        }
                    }
                    else
                    {
                        MessageBox.Show("Логин или пароль неверный!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"{ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            RegWindow regWindow = new RegWindow(mainWindow1);
            regWindow.Show();
        }
    }
}
