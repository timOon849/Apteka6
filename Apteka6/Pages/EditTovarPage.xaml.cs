using Apteka6.Classes;
using Apteka6.DBModel;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Логика взаимодействия для EditTovarPage.xaml
    /// </summary>
    public partial class EditTovarPage : Page
    {
        MainWindow mainWindow1;
        Tovars obj;

        public EditTovarPage(MainWindow mainWindow, Tovars obj1)
        {
            InitializeComponent();
            mainWindow1 = mainWindow;
            obj = obj1;
            this.DataContext = obj;
            LoadCategories();
        }

        private void LoadCategories()
        {
            var categories = ConnectionClass.apteka.CategoryTovar.ToList();
            CategoryBox.ItemsSource = categories;
            CategoryBox.DisplayMemberPath = "Name";
            CategoryBox.SelectedValuePath = "Id";
            CategoryBox.SelectedIndex = 0;
        }

        private void PhotoBtn_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "JPEG Files (*.jpeg)|*.jpeg|PNG Files (*.png)|*.png|JPG Files (*.jpg)|*.jpg|GIF Files (*.gif)|*.gif";
            if (openFileDialog.ShowDialog().GetValueOrDefault())
            {
                obj.Image = File.ReadAllBytes(openFileDialog.FileName);
                MemoryStream byteStream = new MemoryStream(obj.Image);
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.StreamSource = byteStream;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
                IPicture.Source = image;
            }
        }

        private void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            while (true)
            {
                if (string.IsNullOrEmpty(NameBlock.Text))
                {
                    MessageBox.Show("Введите название!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                }
                if (string.IsNullOrEmpty(CountBlock.Text))
                {
                    MessageBox.Show("Введите количество!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                }
                if (string.IsNullOrEmpty(InfoBlock.Text))
                {
                    MessageBox.Show("Введите информацию!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                }
                if (string.IsNullOrEmpty(SrokBlock.Text))
                {
                    MessageBox.Show("Введите срок годности!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                }
                if (string.IsNullOrEmpty(DateProsrochkiBlock.Text))
                {
                    MessageBox.Show("Введите дату просрочки!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                }
                if (string.IsNullOrEmpty(PriceBlock.Text))
                {
                    MessageBox.Show("Введите цену!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                }
                if (string.IsNullOrEmpty(InstructionBlock.Text))
                {
                    MessageBox.Show("Введите инструкцию!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
                }

                var tovar = ConnectionClass.apteka.Tovars.Where(z => z.Id == obj.Id).FirstOrDefault();
                tovar.Name = NameBlock.Text;
                tovar.Count = Convert.ToInt32(CountBlock.Text);
                tovar.Info = InfoBlock.Text;
                tovar.Price = Convert.ToInt32(PriceBlock.Text);
                tovar.SrokGodnosti = SrokBlock.Text;
                tovar.SrokGodnostiDate = Convert.ToDateTime(DateProsrochkiBlock.Text);
                tovar.Instuction = InstructionBlock.Text;
                if (CategoryBox.SelectedIndex == 0)
                {
                    tovar.Category_Id = 1;
                }
                else if (CategoryBox.SelectedIndex == 1)
                {
                    tovar.Category_Id = 2;
                }
                else if (CategoryBox.SelectedIndex == 2)
                {
                    tovar.Category_Id = 3;
                }
                tovar.Resept = Resept.IsChecked;


                if (tovar.Image == null || tovar.Image.Length == 0)
                {
                    MessageBox.Show("Изображение не выбрано!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }


                try
                {
                    ConnectionClass.apteka.SaveChanges();
                    MessageBox.Show("Товар успешно сохранен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
                    mainWindow1.mainFrame.NavigationService.Navigate(new CatalogPage(mainWindow1));
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при добавлении: {ex.Message}", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                break;
            }
        }

        private void BackBtn_Click(object sender, RoutedEventArgs e)
        {

            mainWindow1.mainFrame.NavigationService.GoBack();
        }

        private void CategoryBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedCategory = CategoryBox.SelectedItem as CategoryTovar;
        }
    }
}
