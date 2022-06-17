using CarRent.Models;
using CarRent.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TaskSystem;

namespace CarRent.ViewModels
{
    public class SendReviewWindowViewModel : INotifyPropertyChanged
    {
        public Car ReviewedCar { get; set; }
        public string Review { get; set; }
        private string _reviewerPhoneNumber;

        public string ReviewerPhoneNumber
        {
            get { return _reviewerPhoneNumber; }
            set { _reviewerPhoneNumber = value; OnPropertyChanged(nameof(ReviewerPhoneNumber)); }
        }

        public SendReviewWindowViewModel(Car car, string review)
        {
            ReviewedCar = car;
            Review = review;
        }
        private RelayCommand _sendReviewCommand;
        public RelayCommand SendReviewCommand
        {
            get
            {
                return _sendReviewCommand ??
                    (_sendReviewCommand = new RelayCommand(obj =>
                    {
                        var reviewer = Helper.db.Renters.Where(x => x.PhoneNumber == ReviewerPhoneNumber).FirstOrDefault();
                        if (reviewer == null)
                        {
                            MessageBox.Show("Чтобы оставить отзыв на авто, оно должно быть арендовано хотя бы раз.");
                            return;
                        }
                        var rent = Helper.db.Rents.Where(x => x.Renter.PhoneNumber == ReviewerPhoneNumber && x.CarId == ReviewedCar.Id).FirstOrDefault();
                        var review = rent.Review;
                        if (review != null)
                        {
                            MessageBox.Show("Вы уже оставляли отзыв на вашу последнюю аренду.");
                            return;
                        }
                        rent.Review = Review;
                        Helper.db.SaveChanges();
                        Application.Current.Windows.OfType<SendReviewWindow>().FirstOrDefault().Close();

                    }));
            }
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
