using CarRent.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        Grid _carPictureRowDefinition;

        public UserWindow()
        {
            InitializeComponent();
            this.DataContext = new UserWindowViewModel();
            Helper.db.CarBrands.Load();
            SortComboBox.SelectedIndex = 1;
            _carPictureRowDefinition = FindVisualChildren<Grid>(CarList).FirstOrDefault();
        }
        //private RowDefinition FindGrid(string rowName)
        //{
        //    Grid grid = null;
        //    foreach (var row in FindVisualChildren<Grid>(this))
        //    {
        //        if (row.Name == rowName)
        //        {
        //            grid = row;
        //        }
        //    }
        //    return rowDefinition;
        //}

        private void SearchTBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchTBlock.Visibility = Visibility.Hidden;
            if (String.IsNullOrWhiteSpace(SearchTBox.Text))
                SearchTBlock.Visibility = Visibility.Visible;

        }

        public IEnumerable<T> FindVisualChildren<T>(DependencyObject depObj) where T : DependencyObject
        {
            if (depObj != null)
            {
                for (int i = 0; i < VisualTreeHelper.GetChildrenCount(depObj); i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(depObj, i);

                    if (child != null && child is T)
                        yield return (T)child;

                    foreach (T childOfChild in FindVisualChildren<T>(child))
                        yield return childOfChild;
                }
            }
        }
        //private void ChangeDTemplateRowHeight(double newHeight)
        //{
        //    foreach (var row in FindVisualChildren<RowDefinition>(this))
        //    {
        //        if (row.Name == "CarPictureRowDefiniton")
        //        {
        //            row.Height = new GridLength(newHeight, GridUnitType.Pixel);
        //        }
        //    }
        //}

        //bool _isResize;
        private void CarList_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            //_isResize = true;
            //var width = (sender as ListBox).Width;
            //if (_width != width)
            //{
            //    _width = width;

            //}
            //if (_carPictureRowDefinition != null)
            //    _carPictureRowDefinition.Height = new GridLength(CarList.ActualWidth, GridUnitType.Pixel);
            //ChangeDTemplateRowHeight();
        }
        private void MainBorder_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            var border = (sender as Border);
            if (border.Height != GlobalGridLeftColumn.Width.Value/2)
            {
                border.Height = GlobalGridLeftColumn.Width.Value/2;
            }
        }
    }
}
