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
    public class ManagerMainVM : BaseVM
    {
        public Command RegisterTestCentreCommand { get; set; }
        public Command RecordTesterCommand { get; set; }
        public Command ManageTestKitStockCommand { get; set; }
        public Command GenerateTestReportCommand { get; set; }
        public Command SignOutCommand { get; set; }

        private async void RegisterTestCentreExecute (object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RegisterTestCentreView());
        }

        private async void RecordTesterExecute(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RecordTesterView());
        }

        private async void ManageTestKitStockExecute(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new ManageTestKitView());
        }

        private async void GenerateTestReportExecute(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new GenerateTestReportManagerView());
        }

        private void SignOutExecute(object obj)
        {
            App.CentreOfficer = null;
            Application.Current.MainPage = new NavigationPage(new MainView());
        }

        public ManagerMainVM()
        {
            RegisterTestCentreCommand = new Command(RegisterTestCentreExecute);
            RecordTesterCommand = new Command(RecordTesterExecute);
            ManageTestKitStockCommand = new Command(ManageTestKitStockExecute);
            GenerateTestReportCommand = new Command(GenerateTestReportExecute);
            SignOutCommand = new Command(SignOutExecute);
        }
    }
}
