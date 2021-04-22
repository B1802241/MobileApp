using CTIS.Modal;
using CTIS.Utilities;
using CTIS.Views.Manager;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace CTIS.ViewModals.Manager
{
    public class ManageTestKitVM : BaseVM
    {
        public ObservableCollection<TestKit> TestKitsList { get; set; }

        private object _SelectedItem;

        public object SelectedItem
        {
            get { return _SelectedItem; }
            set
            {
                _SelectedItem = value;
                if (_SelectedItem == null)
                {
                    OnPropertyChanged();
                }
                else
                {
                    UpdateTestKitCommand.Execute(SelectedItem);
                }
            }
        }

        public Command AddTestKitCommand { get; set; }

        public Command UpdateTestKitCommand { get; set; }
        private async void AddTestKitExecute(object obj)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
            await Application.Current.MainPage.Navigation.PushAsync(new AddTestKitView());
        }
        private async void UpdateTestKitExecute(object obj)
        {
            UpdateTestKitVM.TestKit = (TestKit)obj;
            await Application.Current.MainPage.Navigation.PopAsync();
            await Application.Current.MainPage.Navigation.PushAsync(new UpdateTestKitView());
        }
        public async void GetAllTestKits()
        {
            List<TestKit> testKits = await CtisDB.GetAllTestKitsAsync();
            TestKitsList.Clear();
            foreach (TestKit testKit in testKits)
            {
                TestKitsList.Add(testKit);
            }
        }
        public ManageTestKitVM()
        {
            TestKitsList = new ObservableCollection<TestKit>();
            AddTestKitCommand = new Command(AddTestKitExecute);
            UpdateTestKitCommand = new Command(UpdateTestKitExecute);
            GetAllTestKits();
        }
    }
}