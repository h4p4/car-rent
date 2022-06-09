﻿using CarRent.Models;
using CarRent.Views;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Resources;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using TaskSystem;

namespace CarRent.ViewModels
{
    public class UserWindowViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ComboBoxItem> _sortComboBoxItems;
        private ObservableCollection<ComboBoxItem> _filterComboBoxItems;
        private ObservableCollection<Car> _carListCollection;
        private ObservableCollection<Rent> _selectedCarRents;
        private ComboBoxItem _sortComboBoxSelectedItem;
        private ComboBoxItem _filterComboBoxSelectedItem;
        private Car _selectedCar;

        private bool _canUserEditCar;

        public bool CanUserEditCar
        {
            get { return _canUserEditCar; }
            set { _canUserEditCar = value; OnPropertyChanged(nameof(CanUserEditCar)); }
        }

        public ObservableCollection<Rent> SelectedCarRents
        {
            get { return _selectedCarRents; }
            set
            { 
                _selectedCarRents = value; 
                OnPropertyChanged(nameof(SelectedCarRents));
                Helper.db.SaveChanges();
            }
        }

        public ObservableCollection<Car> CarListCollection
        {
            get { return _carListCollection; }
            set 
            {
                _carListCollection = value; 
                OnPropertyChanged(nameof(CarListCollection)); 
                Helper.db.SaveChanges();
            }
        }
        public Car SelectedCar
        {
            get { return _selectedCar; }
            set
            {
                _selectedCar = value;
                OnPropertyChanged(nameof(SelectedCar));
                if (SelectedCar != null)
                    SelectedCarRents = new ObservableCollection<Rent>(Helper.db.Rents.Where(x => x.CarId == SelectedCar.Id));
            }
        }
        public ObservableCollection<ComboBoxItem> SortComboBoxItems
        {
            get { return _sortComboBoxItems; }
            set { _sortComboBoxItems = value; OnPropertyChanged(nameof(SortComboBoxItems)); }
        }

        public ComboBoxItem SortComboBoxSelectedItem
        {
            get { return _sortComboBoxSelectedItem; }
            set 
            {
                _sortComboBoxSelectedItem = value;
                OnPropertyChanged(nameof(SortComboBoxSelectedItem)); 
                UpdateView(); 
            }
        }
        public ObservableCollection<ComboBoxItem> FilterComboBoxItems
        {
            get { return _filterComboBoxItems; }
            set { _filterComboBoxItems = value; OnPropertyChanged(nameof(FilterComboBoxItems)); }
        }


        public ComboBoxItem FilterComboBoxSelectedItem
        {
            get { return _filterComboBoxSelectedItem; }
            set 
            { 
                _filterComboBoxSelectedItem = value; 
                OnPropertyChanged(nameof(FilterComboBoxSelectedItem));
                UpdateView();
            }
        }
        private string _searchedText;

        public string SearchedText
        {
            get { return _searchedText; }
            set { 
                _searchedText = value;
                OnPropertyChanged(nameof(SearchedText)); 
                UpdateView();
            }
        }


        private void UpdateView()
        {
            CarListCollection = new ObservableCollection<Car>(Helper.db.Cars);
            if (SortComboBoxSelectedItem != null)
                switch (SortComboBoxSelectedItem.Name)
                {
                    case "OrderByCostDesc":
                        {
                            CarListCollection = new ObservableCollection<Car>(CarListCollection.OrderByDescending(x => x.CostPerDay));
                            break;
                        }
                    case "OrderByCostAsc":
                        {
                            CarListCollection = new ObservableCollection<Car>(CarListCollection.OrderBy(x => x.CostPerDay));
                            break;
                        }
                    case "OrderByYearDesc":
                        {
                            CarListCollection = new ObservableCollection<Car>(CarListCollection.OrderByDescending(x => x.ReleaseYear));
                            break;
                        }
                    case "OrderByYearAsc":
                        {
                            CarListCollection = new ObservableCollection<Car>(CarListCollection.OrderBy(x => x.ReleaseYear));
                            break;
                        }
                    default: break;
                }
            if (FilterComboBoxSelectedItem != null && FilterComboBoxSelectedItem.Name != "FilterByDefault")
                CarListCollection = new ObservableCollection<Car>(CarListCollection.Where(x => x.CarBrand.Title == FilterComboBoxSelectedItem.Content));
            if (!String.IsNullOrWhiteSpace(SearchedText))
                CarListCollection = new ObservableCollection<Car>(CarListCollection.Where(x => x.CarBrand.Title.ToLower().Contains(SearchedText.ToLower()) ||
                                                                                               //x.TransmissionType.Title.ToLower().Contains(SearchedText.ToLower()) ||
                                                                                               //x.SteeringWheelSide.Title.ToLower().Contains(SearchedText.ToLower()) || // хз почему не работает поиск по стороне руля и по коробке
                                                                                               x.EngineVolume.ToString().ToLower().Contains(SearchedText.ToLower()) ||
                                                                                               x.ReleaseYear.ToString().ToLower().Contains(SearchedText.ToLower()) ||
                                                                                               x.CostPerDay.ToString().ToLower().Contains(SearchedText.ToLower()) ||
                                                                                               x.Title.ToLower().Contains(SearchedText.ToLower())
                                                                                               ));
                    
        }

        private RelayCommand _changeRoleCommand;
        public RelayCommand ChangeRoleCommand
        {
            get
            {
                return _changeRoleCommand ??
                    (_changeRoleCommand = new RelayCommand(obj =>
                    {
                        StartWindow startWindow = new StartWindow();
                        startWindow.ShowDialog();
                    }));
            }
        }
        private RelayCommand _selectedCarBrandChanged;
        public RelayCommand SelectedCarBrandChanged
        {
            get
            {
                return _selectedCarBrandChanged ??
                    (_selectedCarBrandChanged = new RelayCommand(obj =>
                    {
                        //if (SelectedCar != null)
                        //    SelectedCar.CarBrand.Title = Helper.db.CarBrands.Where(x => x.Id == SelectedCar.CarBrandId).First().Title;
                        UpdateView();
                    }));
            }
        }
        private RelayCommand _selectedCarTransmissionTypeChanged;
        public RelayCommand SelectedCarTransmissionTypeChanged
        {
            get
            {
                return _selectedCarTransmissionTypeChanged ??
                    (_selectedCarTransmissionTypeChanged = new RelayCommand(obj =>
                    {
                        //SelectedCar.CarBrand.Title = Helper.db.CarBrands.Where(x => x.Id == SelectedCar.CarBrandId).First().Title;
                        //UpdateView();
                    }));
            }
        }
        private RelayCommand _selecteCarSteeringWheelSideChanged;
        public RelayCommand SelecteCarSteeringWheelSideChanged
        {
            get
            {
                return _selecteCarSteeringWheelSideChanged ??
                    (_selecteCarSteeringWheelSideChanged = new RelayCommand(obj =>
                    {
                        //SelectedCar.CarBrand.Title = Helper.db.CarBrands.Where(x => x.Id == SelectedCar.CarBrandId).First().Title;
                        //UpdateView();
                    }));
            }
        }
        private RelayCommand _createNewCarCommand;
        public RelayCommand CreateNewCarCommand
        {
            get
            {
                return _createNewCarCommand ??
                    (_createNewCarCommand = new RelayCommand(obj =>
                    {
                        SelectedCar = new Car() { CarStatusId = 1 };
                    }));
            }
        }

        private RelayCommand _addNewCarCommand;
        public RelayCommand AddNewCarCommand
        {
            get
            {
                return _addNewCarCommand ??
                    (_addNewCarCommand = new RelayCommand(obj =>
                    {
                        try
                        {
                            Helper.db.Cars.Add(SelectedCar);
                            Helper.db.SaveChanges();
                            SelectedCar = null;
                        }
                        catch (Exception)
                        {
                            MessageBox.Show("Не удалось добавить авто в базу данных!\nНеправильный формат ввода.", "Wrong format", MessageBoxButton.OK, MessageBoxImage.Warning);
                        }

                    }));
            }
        }
        private RelayCommand _deleteCarCommand;
        public RelayCommand DeleteCarCommand
        {
            get
            {
                return _deleteCarCommand ??
                    (_deleteCarCommand = new RelayCommand(obj =>
                    {
                        if (SelectedCar != null)
                        {
                            //CarListCollection.Remove(SelectedCar);
                            Helper.db.Cars.Remove(SelectedCar);
                            SelectedCar = null;
                            OnPropertyChanged(nameof(CarListCollection));
                            UpdateView();
                        }
                    }));
            }
        }
        private RelayCommand _changeSelectedCarPictureCommand;
        public RelayCommand ChangeSelectedCarPictureCommand
        {
            get
            {
                return _changeSelectedCarPictureCommand ??
                    (_changeSelectedCarPictureCommand = new RelayCommand(obj =>
                    {
                        string fileName;
                        string newName;
                        var binDir = Environment.CurrentDirectory;
                        var imageDir = Directory.GetParent(binDir).Parent.Parent.FullName + "\\Images";
                        // CarRent\\Images
                        OpenFileDialog openFileDialog = new OpenFileDialog();
                        openFileDialog.Filter = "Image files (*.png;*.jpeg;*.jpg)|*.png;*.jpeg;*.jpg|All files (*.*)|*.*";
                        openFileDialog.Multiselect = false;
                        if (openFileDialog.ShowDialog() == true)
                        {
                            var name = DateTime.Now.ToString("yyyy_MM_dd_HH_mm_ss__") + openFileDialog.SafeFileName;
                            fileName = openFileDialog.FileName;
                            newName = imageDir + "\\" + name;
                            try
                            {
                                File.Copy(fileName, newName, true);
                            }
                            catch (Exception) { }
                            finally
                            {
                                var oldFile = (Directory.GetParent(binDir).Parent.Parent.FullName + _selectedCar.Image).Replace("/", "\\");
                                SelectedCar.Image = "/Images/" + name;
                                SelectedCar.ImagePicture = new BitmapImage(new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Images\\" + name));
                                OnPropertyChanged(nameof(Car.Image));
                                UpdateView();
                                GC.Collect();
                                GC.WaitForPendingFinalizers();


                                // Раскоментить когда пойму как сделать так чтобы этот файл не использовался
                                //File.Delete(oldFile);



                                //DeleteOldPic(oldFile);
                            }
                        }
                    }));
            }
        }
        //private async void DeleteOldPic(string oldFile)
        //{
        //    await Task.Delay(20000);
        //    File.Delete(oldFile);

        //}
        private ObservableCollection<CarBrand> _carBrands;
        public ObservableCollection<CarBrand> CarBrands
        {
            get { return _carBrands; }
            set { _carBrands = value; OnPropertyChanged(nameof(CarBrands)); }
        }        

        private ObservableCollection<TransmissionType> _transmissionTypes;
        public ObservableCollection<TransmissionType> TransmissionTypes
        {
            get { return _transmissionTypes; }
            set { _transmissionTypes = value; OnPropertyChanged(nameof(TransmissionTypes)); }
        }
        private ObservableCollection<SteeringWheelSide> _steeringWheelSides;
        public ObservableCollection<SteeringWheelSide> SteeringWheelSides
        {
            get { return _steeringWheelSides; }
            set { _steeringWheelSides = value; OnPropertyChanged(nameof(SteeringWheelSide)); }
        }

        public UserWindowViewModel()
        {
            CarListCollection = new ObservableCollection<Car>(Helper.db.Cars);
            var sortCbItems = new ObservableCollection<ComboBoxItem>
            {
                new ComboBoxItem { Name = "OrderByDefault", Content = "--Сортировка--" },
                new ComboBoxItem { Name = "OrderByCostDesc", Content = "По убыванию цены" },
                new ComboBoxItem { Name = "OrderByCostAsc", Content = "По возрастанию цены" },
                new ComboBoxItem { Name = "OrderByYearDesc", Content = "По убыванию года" },
                new ComboBoxItem { Name = "OrderByYearAsc", Content = "По возрастанию года" },
            };
            SortComboBoxItems = sortCbItems;
            SortComboBoxSelectedItem = sortCbItems.First();

            var filterCbFirstItem = new ComboBoxItem { Name = "FilterByDefault", Content = "--Фильтрация--" };
            FilterComboBoxSelectedItem = filterCbFirstItem;
            var carBrands = Helper.db.CarBrands.ToList();
            FilterComboBoxItems = new ObservableCollection<ComboBoxItem>();
            FilterComboBoxItems.Add(filterCbFirstItem);
            foreach (var carBrand in carBrands)
            {
                FilterComboBoxItems.Add(new ComboBoxItem { Content=carBrand.Title });
            }
            TransmissionTypes = new ObservableCollection<TransmissionType>(Helper.db.TransmissionTypes);
            CarBrands = new ObservableCollection<CarBrand>(Helper.db.CarBrands);
            SteeringWheelSides = new ObservableCollection<SteeringWheelSide>(Helper.db.SteeringWheelSides);
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }

    }
}
