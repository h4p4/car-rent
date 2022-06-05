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
            Helper.IsCurrentUserAdmin = false;
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

        private void Window_Activated(object sender, EventArgs e)
        {
            SelectedCarManipulationContextMenu.IsOpen = false;
            ChangeContextMenuVisability();

        }
        private async void ChangeContextMenuVisability()
        {
            await Task.Delay(150);
            if (Helper.IsCurrentUserAdmin)
            {
                SelectedCarManipulationContextMenu.IsEnabled = true;
                SelectedCarManipulationContextMenu.Visibility = Visibility.Visible;

                return;
            }
            SelectedCarManipulationContextMenu.IsEnabled = false;
            SelectedCarManipulationContextMenu.Visibility = Visibility.Hidden;
        }
        private bool _canUserEditCar;
        private void EditCarCheckableMenuItem_Checked(object sender, RoutedEventArgs e)
        {
            _canUserEditCar = true;
            ChangeEditableElements(_canUserEditCar);
        }

        private void EditCarCheckableMenuItem_Unchecked(object sender, RoutedEventArgs e)
        {
            _canUserEditCar = false;
            ChangeEditableElements(_canUserEditCar);
        }
        private void ChangeEditableElements(bool canEdit)
        {
            var TBoxCollection = new List<TextBox>();
            var CBoxCollection = new List<ComboBox>();
            CBoxCollection.Add(SelectedCarBrandCBox); //TBoxCollection.Add(SelectedCarBrandTBox);
            TBoxCollection.Add(SelectedCarTitleTBox);
            TBoxCollection.Add(SelectedCarCostTBox);
            //TBoxCollection.Add(SelectedCarTransmissionTypeTBox);
            TBoxCollection.Add(SelectedCarEngineVolumeTBox);
            //TBoxCollection.Add(SelectedCarSteeringWheelSideTBox);
            TBoxCollection.Add(SelectedCarYearTBox);
            if (canEdit)
            {
                // can edit
                foreach (var cbox in CBoxCollection)
                    ChangeEditableElementsVisability(true, cbox);
                foreach (var tbox in TBoxCollection)
                    ChangeEditableElementsVisability(true, 1, tbox);
                return;
            }
            foreach (var cbox in CBoxCollection)
                ChangeEditableElementsVisability(false, cbox);
            foreach (var tbox in TBoxCollection)
                ChangeEditableElementsVisability(false, 0, tbox);
            // cant edit 

        }
        private void ChangeEditableElementsVisability(bool isTrue, int BorderThickness, TextBox textBox)
        {
            textBox.IsHitTestVisible = isTrue;
            textBox.BorderThickness = new Thickness(BorderThickness);
        }        
        private void ChangeEditableElementsVisability(bool isTrue, ComboBox comboBox)
        {
            comboBox.Visibility = Visibility.Hidden;
            comboBox.Width = 0;
            if (isTrue)
            {
                comboBox.Visibility = Visibility.Visible;
                comboBox.Width = Double.NaN;
            }

        }

        private void ChangeRoleMenuItem_Click(object sender, RoutedEventArgs e)
        {
            EditCarCheckableMenuItem.IsChecked = false;
            _canUserEditCar = false;
            ChangeEditableElements(_canUserEditCar);
        }
    }
}
