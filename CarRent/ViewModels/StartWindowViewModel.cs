using CarRent.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TaskSystem;

namespace CarRent.ViewModels
{
    public class StartWindowViewModel : INotifyPropertyChanged
    {
        private RelayCommand _logAsUserCommand;
        private RelayCommand _logAsAdminCommand;

        private string _enteredPassword;

        public string EnteredPassword
        {
            get { return _enteredPassword; }
            set { _enteredPassword = value; OnPropertyChanged(nameof(EnteredPassword)); }
        }

        //private RelayCommand _command;
        //public RelayCommand Command
        //{
        //    get
        //    {
        //        return _command ??
        //            (_command = new RelayCommand(obj =>
        //            {

        //            }));
        //    }
        //}        
        public RelayCommand LogAsUserCommand
        {
            get
            {
                return _logAsUserCommand ??
                    (_logAsUserCommand = new RelayCommand(obj =>
                    {
                        IsAdmin = false;
                        Application.Current.Windows.OfType<StartWindow>().First().Close();
                        
                    }));
            }
        }
        public RelayCommand LogAsAdminCommand
        {
            get
            {
                return _logAsAdminCommand ??
                    (_logAsAdminCommand = new RelayCommand(obj =>
                    {
                        var pass = obj as PasswordBox;
                        IsAdmin = false;
                        if (pass.Password != "123") return;
                        IsAdmin = true;
                        Application.Current.Windows.OfType<StartWindow>().First().Close();
                    }));
            }
        }
        private bool _isAdmin;

        public bool IsAdmin
        {
            get { return _isAdmin; }
            set 
            { 
                _isAdmin = value; OnPropertyChanged(nameof(IsAdmin));
                Helper.IsCurrentUserAdmin = IsAdmin;

            }
        }

        public StartWindowViewModel()
        {

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
