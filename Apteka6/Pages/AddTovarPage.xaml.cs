using Apteka6.Classes;
using Apteka6.DBModel;
using Microsoft.Win32;
using System;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace Apteka6.Pages
{
    /// <summary>
    /// Логика взаимодействия для AddTovarPage.xaml
    /// </summary>
    public partial class AddTovarPage : Page
    {
        MainWindow mainWindow1;
        Tovars obj = new Tovars();
        
        public AddTovarPage(MainWindow mainWindow)
        {
            InitializeComponent();
            mainWindow1 = mainWindow;
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


                obj.Name = NameBlock.Text;
                obj.Count = Convert.ToInt32(CountBlock.Text);
                obj.Info = InfoBlock.Text;
                obj.Price = Convert.ToInt32(PriceBlock.Text);
                obj.SrokGodnosti = SrokBlock.Text;
                obj.SrokGodnostiDate = Convert.ToDateTime(DateProsrochkiBlock.Text);
                obj.Instuction = InstructionBlock.Text;
                obj.Category_Id = CategoryBox.SelectedIndex;
                obj.Resept = Resept.IsChecked;


                if (obj.Image == null || obj.Image.Length == 0)
                {
                    MessageBox.Show("Изображение не выбрано!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                
                try
                {
                    ConnectionClass.apteka.Tovars.Add(obj);
                    ConnectionClass.apteka.SaveChanges();
                    MessageBox.Show("Объект успешно добавлен!", "Успех", MessageBoxButton.OK, MessageBoxImage.Information);
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
