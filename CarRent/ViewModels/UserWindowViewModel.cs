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
        private ObservableCollection<Car> _carListCollection;
        private Car _selectedCar;
        private ObservableCollection<ComboBoxItem> _sortComboBoxItems;

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
        private ComboBoxItem _sortComboBoxSelectedItem;

        public ComboBoxItem SortComboBoxSelectedItem
        {
            get { return _sortComboBoxSelectedItem; }
            set { _sortComboBoxSelectedItem = value; OnPropertyChanged(nameof(SortComboBoxSelectedItem)); UpdateView(); }
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
                }
        }


        public UserWindowViewModel()
        {
            CarListCollection = new ObservableCollection<Car>(Helper.db.Cars);
            var sortCbItems = new ObservableCollection<ComboBoxItem>
            {
                new ComboBoxItem { Name = "OrderByCostDesc", Content = "По убыванию цены" },
                new ComboBoxItem { Name = "OrderByCostAsc", Content = "По возрастанию цены" },
                new ComboBoxItem { Name = "OrderByYearDesc", Content = "По убыванию года" },
                new ComboBoxItem { Name = "OrderByYearAsc", Content = "По возрастанию года" },
            };
            SortComboBoxItems = sortCbItems;
            SortComboBoxSelectedItem = sortCbItems.First();
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
