using CTIS.Modal;
using CTIS.Utilities;
using CTIS.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CTIS.ViewModals
{
    public class SignUpVM : BaseVM
    {
        public CentreOfficer account { get; set; }

        public Role role { get; set; }

        public string Username
        {
            get { return account.Username; }
            set
            {
                account.Username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return account.Password; }
            set
            {
                account.Password = value;
                OnPropertyChanged();
            }
        }

        public string FullName
        {
            get { return account.Name; }
            set
            {
                account.Name = value;
                OnPropertyChanged();
            }
        }

        public string Position
        {
            get { return account.Position; }
            set
            {
                account.Position = value;
                OnPropertyChanged();
            }
        }

        public Command SignUpCommand { get; set; }

        public Command CancelCommand { get; set; }

        private async void CancelExecute(object obj)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async void SignUpExecute(object obj)
        {
            if (string.IsNullOrEmpty(Username) && string.IsNullOrEmpty(FullName) && string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please fill in every field", "OK");
            }
            else
            {
                
                role.Username = account.Username;
                role.Password = account.Password;
                role.UserType = account.Position;
                await CtisDB.AddCentreOfficerAsync(account, role);
                Application.Current.MainPage = new NavigationPage(new MainView());
            }
        }

        public SignUpVM()
        {
            account = new CentreOfficer();
            role = new Role();
            SignUpCommand = new Command(SignUpExecute);
            CancelCommand = new Command(CancelExecute);
        }
    }
}

