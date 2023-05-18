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
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace WpfAppPractice
{
    /// <summary>
    /// Логика взаимодействия для Window2.xaml
    /// </summary>
    public partial class AddEditWindow : Window
    {
        private Product _currentProduct = new Product();
        public AddEditWindow(Product product)
        {
            if (product != null)
            {
                _currentProduct = product;
            }
            InitializeComponent();

            DataContext = _currentProduct;
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e) 
        {
            if (e.ChangedButton == MouseButton.Left)
            {
                this.DragMove();
            }
        }

        private void Exit_Button_Click(object sender, RoutedEventArgs e) 
        {
            MainWindow MainWindow = new MainWindow();
            MainWindow.Show();
            this.Close();
        }

        private void Hide_Button_Click(object sender, RoutedEventArgs e) 
        {
            this.WindowState = WindowState.Minimized;
        }

        private void SaveAddEdit_Click(object sender, RoutedEventArgs e) 
        {
            StringBuilder errors = new StringBuilder();
            if (string.IsNullOrWhiteSpace(_currentProduct.Manufacturer))
                errors.AppendLine("Укажите производителя");
            if (string.IsNullOrWhiteSpace(_currentProduct.Name))
                errors.AppendLine("Укажите название");
            if (string.IsNullOrWhiteSpace(_currentProduct.Description))
                errors.AppendLine("Укажите описание");
            if (_currentProduct.Price < 1 || _currentProduct.Price == null)
                errors.AppendLine("Укажите цену");

            if (errors.Length > 0)
            {
                MessageBox.Show(errors.ToString());
                return;
            }

            if (_currentProduct.ProductCode == 0)
                Practice_NEWEntities.GetContext().Product.Add(_currentProduct);

                try
                {
                Practice_NEWEntities.GetContext().SaveChanges();
                    MessageBox.Show("Информация сохранена");

                    MainWindow MainWindow = new MainWindow();
                    MainWindow.Show();
                    this.Close();
                }
                catch (Exception ex) 
                {
                MessageBox.Show(ex.Message.ToString());
                }
        }
        private void TextBox_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            e.Handled = regex.IsMatch(e.Text);
        }
    }
}
