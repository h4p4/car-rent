using CarRent.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace CarRent.ViewModels
{
    public class UserWindowViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<ComboBoxItem> _sortComboBoxItems;
        private ObservableCollection<ComboBoxItem> _filterComboBoxItems;
        private ObservableCollection<Car> _carListCollection;
        private ComboBoxItem _sortComboBoxSelectedItem;
        private ComboBoxItem _filterComboBoxSelectedItem;
        private Car _selectedCar;

        public ObservableCollection<Car> CarListCollection
        {
            get { return _carListCollection; }
            set { _carListCollection = value; OnPropertyChanged(nameof(CarListCollection)); }
        }
        public Car SelectedCar
        {
            get { return _selectedCar; }
            set { _selectedCar = value; OnPropertyChanged(nameof(SelectedCar)); }
        }
        public ObservableCollection<ComboBoxItem> SortComboBoxItems
        {
            get { return _sortComboBoxItems; }
            set { _sortComboBoxItems = value; OnPropertyChanged(nameof(SortComboBoxItems)); }
        }

        public ComboBoxItem SortComboBoxSelectedItem
        {
            get { return _sortComboBoxSelectedItem; }
            set { _sortComboBoxSelectedItem = value; OnPropertyChanged(nameof(SortComboBoxSelectedItem)); UpdateView(); }
        }
        public ObservableCollection<ComboBoxItem> FilterComboBoxItems
        {
            get { return _filterComboBoxItems; }
            set { _filterComboBoxItems = value; OnPropertyChanged(nameof(FilterComboBoxItems)); }
        }


        public ComboBoxItem FilterComboBoxSelectedItem
        {
            get { return _filterComboBoxSelectedItem; }
            set { _filterComboBoxSelectedItem = value; OnPropertyChanged(nameof(FilterComboBoxSelectedItem)); UpdateView(); }
        }
        private string _searchedText;

        public string SearchedText
        {
            get { return _searchedText; }
            set { _searchedText = value; OnPropertyChanged(nameof(SearchedText)); UpdateView(); }
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
