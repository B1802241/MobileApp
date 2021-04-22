using CTIS.Modal;
using CTIS.Utilities;
using CTIS.Views;
using CTIS.Views.Manager;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CTIS.ViewModals
{
    public class OfficerMainVM : BaseVM
    {
        public Command GenerateTestReportCommand { get; set; }
        public Command SignOutCommand { get; set; }

        private async void GenerateTestReportExecute(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new GenerateTestReportOfficerView());
        }

        private void SignOutExecute(object obj)
        {
            App.CentreOfficer = null;
            Application.Current.MainPage = new NavigationPage(new MainView());
        }

        public OfficerMainVM()
        {
            GenerateTestReportCommand = new Command(GenerateTestReportExecute);
            SignOutCommand = new Command(SignOutExecute);
        }
    }
}

