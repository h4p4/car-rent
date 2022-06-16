using CarRent.Models;
using CarRent.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TaskSystem;

namespace CarRent.ViewModels
{
    public class RentWindowViewModel : INotifyPropertyChanged
    {
        private ObservableCollection<string> _daysCountItemSource;

        public ObservableCollection<string> DaysCountItemSource
        {
            get { return _daysCountItemSource; }
            set { _daysCountItemSource = value; OnPropertyChanged(nameof(DaysCountItemSource)); }
        }

        public Car CarForRent{ get; set; }
        private RelayCommand _rentCarCommand;
        public RelayCommand RentCarCommand
        {
            get
            {
                return _rentCarCommand ??
                    (_rentCarCommand = new RelayCommand(obj =>
                    {
                        if (String.IsNullOrWhiteSpace(RenterPhoneNumber)) 
                        {
                            MessageBox.Show("Введите ваш номер телефона");
                            return;
                        }
                        var renter = Helper.db.Renters.Where(x => x.PhoneNumber == RenterPhoneNumber).FirstOrDefault();
                        // if renter not exists
                        if (renter == null)
                        {
                            if (String.IsNullOrWhiteSpace(RenterName))
                            {
                                MessageBox.Show("Введите ваше имя");
                                return;
                            }
                            var newRenter = new Renter()
                            {
                                FirstName = RenterName,
                                PhoneNumber = RenterPhoneNumber,
                            };
                            Helper.db.Renters.Add(newRenter);
                            AddNewRentForRenter(newRenter);
                            return;
                        }
                        // if renter exists

                        if (!String.IsNullOrWhiteSpace(RenterName))
                            renter.FirstName = RenterName;
                        AddNewRentForRenter(renter);
                    }));
            }
        }
        private void AddNewRentForRenter(Renter renter)
        {
            var newRent = new Rent()
            {
                Car = CarForRent,
                DurationDays = SelectedDaysCount + 1,
                Renter = renter,
                Review = null
            };
            Helper.db.Rents.Add(newRent);
            Helper.db.SaveChanges();
            Application.Current.Windows.OfType<RentWindow>().First().Close();
        }
        private int _selectedDaysCount;
        public int SelectedDaysCount
        {
            get 
            { 
                FullPrice = (_selectedDaysCount + 1) * CarForRent.CostPerDay;
                return _selectedDaysCount;
            }
            set { _selectedDaysCount = value; OnPropertyChanged(nameof(SelectedDaysCount)); }
        }
        private decimal _fullPrice;
        public decimal FullPrice
        {
            get { return _fullPrice; }
            set { _fullPrice = value; OnPropertyChanged(nameof(FullPrice)); }
        }
        private string _renterName;
        public string RenterName
        {
            get { return _renterName; }
            set { _renterName = value; OnPropertyChanged(nameof(RenterName)); }
        }

        private string _renterPhoneNumber;
        public string RenterPhoneNumber
        {
            get { return _renterPhoneNumber; }
            set { _renterPhoneNumber = value; OnPropertyChanged(nameof(RenterPhoneNumber)); }
        }
        public RentWindowViewModel(Car carForRent)
        {
            CarForRent = carForRent;
            DaysCountItemSource = new ObservableCollection<string>();
            for (int i = 1; i <= 99; i++)
            {
                // 1 день
                // 2-4 дня
                // 5-10 дней
                // 11-20 дней
                // 21 день
                // 22-24 дня
                // 25-30 дней
                DaysCountItemSource.Add(AddDayToDays(i));
            }
            FullPrice = carForRent.CostPerDay;
        }
        private string AddDayToDays(int i)
        {
            var a = i.ToString().Substring(i.ToString().Length-1);
            if (i >= 10 && i <= 20) return i.ToString() + " дней";
            if ((a == "2" || a == "3" || a == "4"))
                return i.ToString() + " дня";
            if ((a == "1"))
                return i.ToString() + " день";
            return i.ToString() + " дней";
        }
        public event PropertyChangedEventHandler? PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
            }
        }
    }
}
