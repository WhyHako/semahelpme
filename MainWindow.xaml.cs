using System.Windows;
using System.Windows.Input;
using System.Data.SqlClient;
using System.Data;
using System;
using System.Windows.Documents;
using System.Reflection.PortableExecutable;
using System.Windows.Media.Imaging;
using System.Configuration;
using System.Windows.Controls;
using WpfApp6;
using System.Linq;

namespace WpfAppPractice
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //grd.ItemsSource = Practice_NEWEntities.GetContext().Product.ToList();
        } 

    private void Border_MouseDown(object sender, MouseButtonEventArgs e) // Возможность таскать окошко
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e) // Выйти из программы
        {
            this.Close();
        }

        private void Hide_Button_Click(object sender, RoutedEventArgs e) // Свернуть окно
        {
            this.WindowState = WindowState.Minimized;
        }

        private void LogOut_Button_Click(object sender, RoutedEventArgs e) // Выйти из профиля
        {
            UserWindow UserWindow = new UserWindow();
            UserWindow.Show();
            this.Close();
        }

        private void Refresh_Button_Click(object sender, RoutedEventArgs e) // Обновить страницу рекомендаций
        {
            MainWindow MainWindow = new MainWindow();
            MainWindow.Show();
            this.Close();
        }

        private void ShopCart_Button_Click(object sender, RoutedEventArgs e) // Перейти в корзину
        {
            UserWindow UserWindow = new UserWindow();
            UserWindow.Show();
            this.Close();
        }

        private void PCs_Button(object sender, RoutedEventArgs e) // Переход на страницу с процессорами
        {
            grd.ItemsSource = Practice_NEWEntities.GetContext().Product
            .Where(p => p.Type == "ПК")
            .ToList();
        }

        private void Processors_Button(object sender, RoutedEventArgs e) // Переход на страницу с видеокартами
        {
            grd.ItemsSource = Practice_NEWEntities.GetContext().Product
            .Where(p => p.Type == "Процессор")
            .ToList();
        }

        private void Motherboards_Click(object sender, RoutedEventArgs e)
        {
            grd.ItemsSource = Practice_NEWEntities.GetContext().Product
            .Where(p => p.Type == "Материнская плата")
            .ToList();
        }

        private void Videocards_Click(object sender, RoutedEventArgs e)
        {
            grd.ItemsSource = Practice_NEWEntities.GetContext().Product
            .Where(p => p.Type == "Видеокарта")
            .ToList();
        }

        private void RAM_Click(object sender, RoutedEventArgs e)
        {
            grd.ItemsSource = Practice_NEWEntities.GetContext().Product
            .Where(p => p.Type == "Оперативная память")
            .ToList();
        }
        private void Drives_Click(object sender, RoutedEventArgs e)
        {
            grd.ItemsSource = Practice_NEWEntities.GetContext().Product
            .Where(p => p.Type == "SSD накопитель")
            .ToList();
        }
        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            if (Visibility == Visibility.Visible)
            {
                Practice_NEWEntities.GetContext().ChangeTracker.Entries().ToList().ForEach(p => p.Reload());
                grd.ItemsSource = Practice_NEWEntities.GetContext().Product.ToList();
            }
        }
        private void Edit_Click(object sender, RoutedEventArgs e)
        {
                var button = sender as Button;
                if (button == null)
                    return;

                var item = button.DataContext as Product;


                AddEditWindow AddEditWindow = new AddEditWindow(item);
                AddEditWindow.Show();
                this.Close();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            AddEditWindow AddEditWindow = new AddEditWindow(null);
            AddEditWindow.Show();
            this.Close();
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            var productsforremoving = grd.SelectedItems.Cast<Product>().ToList();

            if (MessageBox.Show($"Вы точно хотите удалить {productsforremoving.Count()}?", "Внимание", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    Practice_NEWEntities.GetContext().Product.RemoveRange(productsforremoving);
                    Practice_NEWEntities.GetContext().SaveChanges();
                    MessageBox.Show("Данные удалены");

                    grd.ItemsSource = Practice_NEWEntities.GetContext().Product.ToList();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message.ToString());
                }
            }
        }

        private void grd_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
        }
    }
}
