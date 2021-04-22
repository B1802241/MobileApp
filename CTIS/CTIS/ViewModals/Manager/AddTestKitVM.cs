using CTIS.Modal;
using CTIS.Utilities;
using CTIS.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CTIS.ViewModals.Manager
{
    class AddTestKitVM : BaseVM
    {
        public TestKit testkit { get; set; }

        public string testName
        {
            get { return testkit.testName; }
            set
            {
                testkit.testName = value;
                OnPropertyChanged();
            }
        }

        public int availableStock
        {
            get { return testkit.availableStock; }
            set
            {
                testkit.availableStock = value;
                OnPropertyChanged();
            }
        }


        public Command RegisterCommand { get; set; }

        public Command CancelCommand { get; set; }

        private async void CancelExecute(object obj)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
            await Application.Current.MainPage.Navigation.PushAsync(new ManageTestKitView());
        }

        private async void RegisterExecute(object obj)
        {
            if (!string.IsNullOrEmpty(testName) && availableStock != 0)
            {
                await CtisDB.AddTestKitAsync(testkit);
                await Application.Current.MainPage.Navigation.PopAsync();
                await Application.Current.MainPage.Navigation.PushAsync(new ManageTestKitView());
            }
            else
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please fill in every field", "OK");
                
            }
        }

        public AddTestKitVM()
        {
            testkit = new TestKit();
            RegisterCommand = new Command(RegisterExecute);
            CancelCommand = new Command(CancelExecute);
        }
    }
}
    