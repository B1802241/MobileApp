using CTIS.Utilities;
using CTIS.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CTIS.ViewModals
{
    public class PatientMainVM : BaseVM
    {
        public Command TestHistoryCommand { get; set; }
        public Command SignOutCommand { get; set; }

        private async void TestHistoryExecute(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new TestHistoryView());
        }

        private void SignOutExecute(object obj)
        {
            App.CentreOfficer = null;
            Application.Current.MainPage = new NavigationPage(new MainView());
        }

        public PatientMainVM()
        {
            TestHistoryCommand = new Command(TestHistoryExecute);
            SignOutCommand = new Command(SignOutExecute);
        }
    }
}
