using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace CarRent.Models
{
    public partial class Car : INotifyPropertyChanged
    {
        public Car()
        {
            Rents = new HashSet<Rent>();
        }

        public int Id { get; set; }
        public int CarStatusId { get; set; }
        private int _carBrandId;
        public int CarBrandId
        {
            get { return _carBrandId; }
            set { _carBrandId = value; OnPropertyChanged(nameof(CarBrandId)); Helper.db.SaveChanges(); }
        }

        private int _transmissionTypeId;
        public int TransmissionTypeId
        {
            get { return _transmissionTypeId; }
            set { _transmissionTypeId = value; OnPropertyChanged(nameof(TransmissionTypeId)); Helper.db.SaveChanges(); }
        }

        private int _steeringWheelSideId;
        public int SteeringWheelSideId
        {
            get { return _steeringWheelSideId; }
            set { _steeringWheelSideId = value; OnPropertyChanged(nameof(SteeringWheelSideId)); Helper.db.SaveChanges(); }
        }

        private decimal _engineVolume;

        public decimal EngineVolume
        {
            get { return _engineVolume; }
            set 
            { 
                var reserve = _engineVolume;
                _engineVolume = value; 
                OnPropertyChanged(nameof(EngineVolume));
                try
                {
                    Helper.db.SaveChanges();
                }
                catch (Exception)
                {
                    MessageBox.Show("Wrong format, entered data would not be saved!", "Wrong format", MessageBoxButton.OK, MessageBoxImage.Warning);
                    OnPropertyChanged(nameof(EngineVolume));
                    _engineVolume = reserve;
                }
            }
        }

        private int _releaseYear;

        public int ReleaseYear
        {
            get { return _releaseYear; }
            set { _releaseYear = value; OnPropertyChanged(nameof(ReleaseYear)); Helper.db.SaveChanges(); }
        }

        private string? _image;

        public string? Image
        {
            get { return _image; }
            set { _image = value; OnPropertyChanged(nameof(Image)); Helper.db.SaveChanges(); }
        }

        private BitmapImage? _imagePicture;

        [NotMapped]
        public BitmapImage? ImagePicture
        {
            get
            {
                if (_image == null) return null;
                try
                {
                    return _imagePicture = new BitmapImage(new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Image));
                }
                catch (System.IO.FileNotFoundException)
                {
                    return _imagePicture = new BitmapImage(new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + "\\Images\\default-car.png"));
                }
            }
            set
            {
                _imagePicture = new BitmapImage(new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Image));
                //_imagePicture = new BitmapImage(new Uri(Directory.GetParent(Environment.CurrentDirectory).Parent.Parent.FullName + Image));
                OnPropertyChanged(nameof(ImagePicture));
            }
        }



        private decimal _costPerDay;
        public decimal CostPerDay
        {
            get { return _costPerDay; }
            set { _costPerDay = value; OnPropertyChanged(nameof(CostPerDay)); Helper.db.SaveChanges(); }
        }

        private string _title;

        public string Title
        {
            get { return _title; }
            set { _title = value; OnPropertyChanged(nameof(Title)); Helper.db.SaveChanges(); }
        }


        private CarBrand _carBrand;
        public virtual CarBrand CarBrand
        {
            get { return _carBrand; }
            set { _carBrand = value; OnPropertyChanged(nameof(CarBrand)); Helper.db.SaveChanges(); }
        }

        public virtual CarStatus CarStatus { get; set; } = null!;
        
        private SteeringWheelSide _steeringWheelSide;
        public virtual SteeringWheelSide SteeringWheelSide
        {
            get { return _steeringWheelSide; }
            set { _steeringWheelSide = value; OnPropertyChanged(nameof(SteeringWheelSide)); Helper.db.SaveChanges(); }
        }


        private TransmissionType _transmissionType;
        public virtual TransmissionType TransmissionType
        {
            get { return _transmissionType; }
            set { _transmissionType = value; OnPropertyChanged(nameof(TransmissionType)); Helper.db.SaveChanges(); }
        }

        public virtual ICollection<Rent> Rents { get; set; }

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
