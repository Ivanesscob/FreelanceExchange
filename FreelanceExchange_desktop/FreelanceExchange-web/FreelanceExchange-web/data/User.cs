﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace FreelanceExchange_desktop.Data
{
    public class User : INotifyPropertyChanged
    {
        private int id;
        private string email;
        private string firstName;
        private string lastName;
        private string username;
        private string password;
        private DateTime birthDate;
        private DateTime registrationDate = DateTime.Now;
        private ObservableCollection<string> roles = new ObservableCollection<string>();

        public int Id
        {
            get => id;
            set => SetField(ref id, value);
        }

        public string Email
        {
            get => email;
            set => SetField(ref email, value);
        }

        public string FirstName
        {
            get => firstName;
            set => SetField(ref firstName, value);
        }

        public string LastName
        {
            get => lastName;
            set => SetField(ref lastName, value);
        }

        public string Username
        {
            get => username;
            set => SetField(ref username, value);
        }

        public string Password
        {
            get => password;
            set => SetField(ref password, value);
        }

        public DateTime BirthDate
        {
            get => birthDate;
            set => SetField(ref birthDate, value);
        }

        public DateTime RegistrationDate
        {
            get => registrationDate;
            set => SetField(ref registrationDate, value);
        }

        public ObservableCollection<string> Roles
        {
            get => roles;
            set => SetField(ref roles, value);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null) =>
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

        protected bool SetField<T>(ref T field, T value, [CallerMemberName] string propertyName = null)
        {
            if (EqualityComparer<T>.Default.Equals(field, value))
                return false;
            field = value;
            OnPropertyChanged(propertyName);
            return true;
        }
    }
}
