using CTIS.Modal;
using CTIS.Utilities;
using CTIS.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CTIS.ViewModals.Manager
{
    public class RegisterTestCentreVM : BaseVM
    {
        public TestCentre TestCentre { get; set; }

        public string centreName
        {
            get { return TestCentre.centreName; }
            set { TestCentre.centreName = value;
                OnPropertyChanged();
            }
        }

        public Command RegisterCommand { get; set; }

        public Command CancelCommand { get; set; }

        private async void RegisterExecute(object obj)
        {
            if (string.IsNullOrEmpty(centreName))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please enter anything before you register", "OK");
            }
            else
            {
                await CtisDB.AddTestCentreAsync(TestCentre);
                Application.Current.MainPage = new NavigationPage(new ManagerMainView());
            }
        }

        private async void CancelExecute(object obj)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }
        public RegisterTestCentreVM()
        {
            TestCentre = new TestCentre();
            RegisterCommand = new Command(RegisterExecute);
            CancelCommand = new Command(CancelExecute);
        }
    }
}
