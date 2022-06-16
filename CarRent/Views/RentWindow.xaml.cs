﻿using CarRent.Models;
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
    /// Логика взаимодействия для RentWindow.xaml
    /// </summary>
    public partial class RentWindow : Window
    {
        public RentWindow(Car carForRent)
        {
            InitializeComponent();
            this.DataContext = new RentWindowViewModel(carForRent);
            DaysCountCBox.SelectedIndex = 1;
        }
    }
}
