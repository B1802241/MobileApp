using CTIS.Modal;
using CTIS.Utilities;
using CTIS.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace CTIS.ViewModals.Manager
{
    class UpdateTestKitVM : BaseVM
    {
        public static TestKit TestKit;

        public string testName
        {
            get { return TestKit.testName; }
            set
            {
                TestKit.testName = value;
                OnPropertyChanged();
            }
        }

        public int availableStock
        {
            get { return TestKit.availableStock; }
            set
            {
                TestKit.availableStock = value;
                OnPropertyChanged();
            }
        }

        public ICommand UpdateCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private async void UpdateExecute(object obj)
        {
            TestKit.testName = testName;
            TestKit.availableStock = availableStock;
            await CtisDB.UpdateTestKitStockAsync(TestKit);
            await Application.Current.MainPage.Navigation.PopAsync();
            await Application.Current.MainPage.Navigation.PushAsync(new ManageTestKitView());
        }

        private async void CancelExecute(object obj)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
            await Application.Current.MainPage.Navigation.PushAsync(new ManageTestKitView());
        }

        public UpdateTestKitVM()
        {
            UpdateCommand = new Command(UpdateExecute);
            CancelCommand = new Command(CancelExecute);

            testName = TestKit.testName;
            availableStock = TestKit.availableStock;
        }


    }
}

