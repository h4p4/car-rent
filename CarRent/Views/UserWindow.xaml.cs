using CarRent.ViewModels;
using Microsoft.EntityFrameworkCore;
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
    /// Логика взаимодействия для UserWindow.xaml
    /// </summary>
    public partial class UserWindow : Window
    {
        public UserWindow()
        {
            InitializeComponent();
            this.DataContext = new UserWindowViewModel();
            Helper.db.CarBrands.Load();
            Helper.db.SteeringWheelSides.Load();
            Helper.db.TransmissionTypes.Load();
            Helper.db.Rents.Load();
            Helper.db.Renters.Load();
            SortComboBox.SelectedIndex = 1;
            ChangeSelectedCarBorderVisability();
        }

        private void SearchTBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchTBlock.Visibility = Visibility.Hidden;
            if (String.IsNullOrWhiteSpace(SearchTBox.Text))
                SearchTBlock.Visibility = Visibility.Visible;

        }

        private void CarList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ChangeSelectedCarBorderVisability();
        }
        private void ChangeSelectedCarBorderVisability()
        {
            if (CarList.SelectedIndex == -1)
            {
                SelectedCarVisabilityBorder.Visibility = Visibility.Visible;
                return;
            }
            SelectedCarVisabilityBorder.Visibility = Visibility.Hidden;
        }
    }
}
