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

            bool continueLoadTrying = true;
            while (continueLoadTrying)
            {
                try
                {
                    LoadData();
                    continueLoadTrying = false;
                }
                catch (Exception)
                {
                    continueLoadTrying = true;
                    var result = MessageBox.Show(
                        "Не удалось подключиться к базе данных.\nПовторить попытку?",
                        "Warning",
                        MessageBoxButton.YesNo,
                        MessageBoxImage.Error
                        );
                    if (result == MessageBoxResult.No)
                    {
                        continueLoadTrying = false;
                        Application.Current.Shutdown();
                    }
                }
            }

            SortComboBox.SelectedIndex = 1;
            ChangeSelectedCarBorderVisability();
        }
        private void LoadData()
        {
            this.DataContext = new UserWindowViewModel();
            Helper.db.CarBrands.Load();
            Helper.db.SteeringWheelSides.Load();
            Helper.db.TransmissionTypes.Load();
            Helper.db.Rents.Load();
            Helper.db.Renters.Load();
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
            ChangeSelectedCarReviewListBoxVisability();
        }
        private void ChangeSelectedCarReviewListBoxVisability()
        {
            if (CarList.SelectedIndex == -1)
            {
                CarReviewListBox.Visibility = Visibility.Hidden;
                ReviewTBlock.Visibility = Visibility.Hidden;
                return;
            }
            CarReviewListBox.Visibility = Visibility.Visible;
            ReviewTBlock.Visibility = Visibility.Visible;
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
            CBoxCollection.Add(SelectedCarTransmissionTypeCBox); //TBoxCollection.Add(SelectedCarTransmissionTypeTBox);
            TBoxCollection.Add(SelectedCarEngineVolumeTBox);
            CBoxCollection.Add(SelectedCarSteeringWheelSideCBox); //TBoxCollection.Add(SelectedCarSteeringWheelSideTBox);
            TBoxCollection.Add(SelectedCarYearTBox);
            if (canEdit)
            {
                // can edit
                ChangeEditableElementsVisability(canEdit, SelectCarPicBtn);

                foreach (var cbox in CBoxCollection)
                    ChangeEditableElementsVisability(canEdit, cbox);
                foreach (var tbox in TBoxCollection)
                    ChangeEditableElementsVisability(canEdit, 1, tbox);
                return;
            }
            ChangeEditableElementsVisability(canEdit, SelectCarPicBtn);
            foreach (var cbox in CBoxCollection)
                ChangeEditableElementsVisability(canEdit, cbox);
            foreach (var tbox in TBoxCollection)
                ChangeEditableElementsVisability(canEdit, 0, tbox);
            // cant edit 

        }
        private void ChangeEditableElementsVisability(bool isTrue, int BorderThickness, TextBox textBox)
        {
            textBox.IsHitTestVisible = isTrue;
            textBox.BorderThickness = new Thickness(BorderThickness);
        }              
        private void ChangeEditableElementsVisability(bool isTrue, Button button)
        {
            button.Visibility = Visibility.Hidden;
            if (isTrue)
            {
                button.Visibility = Visibility.Visible;
            }
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
            CarListAndBorderVisabilityChange(false);
        }

        private void AddNewCarMenuItem_Click(object sender, RoutedEventArgs e)
        {
            //CarList.SelectedIndex = -1;
            //CarList.IsEnabled = false;
            //SelectedCarVisabilityBorder.Visibility = Visibility.Hidden;

            CarListAndBorderVisabilityChange(true);
        }

        private void CancelAddCarBtn_Click(object sender, RoutedEventArgs e)
        {
            CarListAndBorderVisabilityChange(false);
        }

        private void AddAddCarBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CarList.SelectedIndex == -1)
                CarListAndBorderVisabilityChange(false);
        }
        private void CarListAndBorderVisabilityChange(bool isVisible)
        {
            EditCarCheckableMenuItem.IsChecked = false;
            CarList.SelectedIndex = -1;
            if (isVisible)
            {
                CarList.IsEnabled = !isVisible;
                RentBtn.Visibility = Visibility.Hidden;
                AddAddCarBtn.Visibility = Visibility.Visible;
                CancelAddCarBtn.Visibility = Visibility.Visible;
                SelectedCarVisabilityBorder.Visibility = Visibility.Hidden;
                SelectedCarBrandCBox.MinWidth = 96;
                SelectedCarTitleTBox.MinWidth = 96;
                SelectedCarCostTBox.MinWidth = 96;
                ChangeEditableElements(true);
                return;
            }
            CarList.IsEnabled = !isVisible;
            RentBtn.Visibility = Visibility.Visible;
            AddAddCarBtn.Visibility = Visibility.Hidden;
            CancelAddCarBtn.Visibility = Visibility.Hidden;
            SelectedCarVisabilityBorder.Visibility = Visibility.Visible;
            SelectedCarBrandCBox.MinWidth = 0;
            SelectedCarTitleTBox.MinWidth = 0;
            SelectedCarCostTBox.MinWidth = 0;
            ChangeEditableElements(false);
        }
    }
}
