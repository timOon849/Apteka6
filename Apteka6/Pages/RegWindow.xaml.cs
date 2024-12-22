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
using System.Windows.Shapes;

namespace Apteka6.Pages
{
    /// <summary>
    /// Логика взаимодействия для RegWindow.xaml
    /// </summary>
    public partial class RegWindow : Window
    {
        MainWindow _mainwindow;
        public RegWindow(MainWindow mainwindow)
        {
            InitializeComponent();
            _mainwindow = mainwindow;
        }

        Byers byers = new Byers();
        Logins log = new Logins();
        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {
            if (PassBox.Text == Pass2Box.Text)
            {
                try
                {
                    byers.Name = NameBox.Text;
                    byers.FName = FNameBox.Text;
                    byers.Login = LogBox.Text;
                    byers.Birthday = Convert.ToDateTime(BirthdayBox.Text);
                    ConnectionClass.apteka.Byers.Add(byers);

                    log.Login = byers.Login;
                    log.Password = PassBox.Text;

                    ConnectionClass.apteka.Logins.Add(log);
                    ConnectionClass.apteka.SaveChanges();

                    var a = ConnectionClass.apteka.Logins.Where(log => log.Login == LogBox.Text && log.Password == PassBox.Text).FirstOrDefault();
                    var b = ConnectionClass.apteka.Byers.Where(byers => byers.Login == a.Login).FirstOrDefault();

                    ConnectionClass.byers = b;
                    this.Close();
                    if (_mainwindow != null)
                    {
                        _mainwindow.mainFrame.NavigationService.Navigate(new CatalogPage(_mainwindow));
                    }

                    else
                    {
                        return;
                    }
                }

                catch
                {
                    MessageBox.Show("Такой логин уже занят!");
                }
            }
            else
            {
                MessageBox.Show("Пароли не совпадают!");
            }

        }
    }
}
