using CTIS.Modal;
using CTIS.Utilities;
using CTIS.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CTIS.ViewModals.Manager
{
    public class RecordTesterVM : BaseVM
    {
        public CentreOfficer tester { get; set; }

        public Role role { get; set; }

        public string Username
        {
            get { return tester.Username; }
            set { tester.Username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return tester.Password; }
            set
            {
                tester.Password = value;
                OnPropertyChanged();
            }
        }

        public string FullName
        {
            get { return tester.Name; }
            set
            {
                tester.Name = value;
                OnPropertyChanged();
            }
        }


        public Command RegisterCommand { get; set; }

        public Command CancelCommand { get; set; }

        private async void CancelExecute(object obj)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async void RegisterExecute(object obj)
        {
            if (string.IsNullOrEmpty(Username) && string.IsNullOrEmpty(FullName) && string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please fill in every field", "OK");
            }
            else
            {
                tester.Position = "Tester";
                role.Username = tester.Username;
                role.Password = tester.Password;
                role.UserType = tester.Position;
                await CtisDB.AddCentreOfficerAsync(tester,role);
                Application.Current.MainPage = new NavigationPage(new ManagerMainView());
            }
        }

        public RecordTesterVM()
        {
            tester = new CentreOfficer();
            role = new Role();
            RegisterCommand = new Command(RegisterExecute);
            CancelCommand = new Command(CancelExecute);
        }
    }
}
