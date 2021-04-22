using CTIS.Utilities;
using CTIS.Views;
using CTIS.Views.Tester;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CTIS.ViewModals.Tester
{
    public class TesterMainVM :BaseVM
    {
        public Command SignOutCommand { get; set; }

        public Command RecordNewTestCommand { get; set; }

        public Command GenerateTestReportCommand { get; set; }

        public Command UpdateTestResultCommand { get; set; }

        public Command RecordPatientCommand { get; set; }

        private async void GenerateTestReportExecute(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new GenerateTestReportTesterView());
        }

        private void SignOutExecute(object obj)
        {
            App.CentreOfficer = null;
            Application.Current.MainPage = new NavigationPage(new MainView());
        }

        private async void RecordNewTestExecute(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RecordTestListView());
        }

        private async void RecordPatientExecute(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new RecordPatient());
        }

        private async void UpdateTestResultExecute(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new TestListView());
        }

        public TesterMainVM()
        {
            GenerateTestReportCommand = new Command(GenerateTestReportExecute);
            SignOutCommand = new Command(SignOutExecute);
            RecordNewTestCommand = new Command(RecordNewTestExecute);
            UpdateTestResultCommand = new Command(UpdateTestResultExecute);
            RecordPatientCommand = new Command(RecordPatientExecute);
        }
    }
}
