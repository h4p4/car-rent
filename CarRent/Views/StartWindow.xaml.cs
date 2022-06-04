using CarRent.ViewModels;
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

namespace CarRent.Views
{
    /// <summary>
    /// Логика взаимодействия для StartWindow.xaml
    /// </summary>
    public partial class StartWindow : Window
    {
        public StartWindow()
        {
            InitializeComponent();
            this.DataContext = new StartWindowViewModel();
        }
        private void LogAsAdminBtn_Click(object sender, RoutedEventArgs e)
        {
            AdminPBox.Visibility = Visibility.Visible;
            PassTBlock.Visibility = Visibility.Visible;
        }
        //private void HidePBoxBtn_Click(object sender, RoutedEventArgs e)
        //{
        //    AdminPBox.Password = String.Empty;
        //    AdminPBox.Visibility = Visibility.Hidden;
        //    PassTBlock.Visibility = Visibility.Hidden;
        //}
        private void AdminPBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            PassTBlock.Visibility = Visibility.Visible;
            if (String.IsNullOrWhiteSpace(AdminPBox.Password)) return;
            PassTBlock.Visibility = Visibility.Hidden;
        }

    }
}

