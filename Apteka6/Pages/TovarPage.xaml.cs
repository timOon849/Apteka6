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
    /// Логика взаимодействия для TovarPage.xaml
    /// </summary>
    public partial class TovarPage : Page
    {
        Tovars obj;
        MainWindow main;
        public TovarPage(MainWindow mainWindow, Tovars obj1)
        {
            this.main = mainWindow;
            obj = obj1;
            this.DataContext = obj;
            LoadCategories();
            //OtzivList.ItemsSource = ConnectionClass.apteka.Reviews.Where(obj.Id == );
            InitializeComponent();


        }
        private void LoadCategories()
        {
            var categories = ConnectionClass.apteka.CategoryTovar.ToList();
            CategoryBox.ItemsSource = categories;
            CategoryBox.DisplayMemberPath = "Name";
            CategoryBox.SelectedValuePath = "Id";
            CategoryBox.SelectedIndex = obj.Category_Id.Value;
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void CategoryBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
