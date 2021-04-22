using CTIS.Modal;
using CTIS.Utilities;
using CTIS.Views.Tester;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace CTIS.ViewModals.Tester
{
    public class TestListVM : BaseVM
    {
        public ObservableCollection<CovidTest> CovidTestList { get; set; }

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
                    UpdateTestCommand.Execute(SelectedItem);
                }
            }
        }

        public Command UpdateTestCommand { get; set; }

        private async void UpdateTestExecute(object obj)
        {
            UpdateTestVM.CovidTest = (CovidTest)obj;
            await Application.Current.MainPage.Navigation.PopAsync();
            await Application.Current.MainPage.Navigation.PushAsync(new UpdateTestView());
        }
        public async void GetAllTests()
        {
            List<CovidTest> tests = await CtisDB.GetAllCovidTestsAsync();
            CovidTestList.Clear();
            foreach (CovidTest test in tests)
            {
                if(test.centreID == App.CentreOfficer.CentreID)
                {
                    CovidTestList.Add(test);
                }
            }
        }
        public TestListVM()
        {
            CovidTestList = new ObservableCollection<CovidTest>();
            UpdateTestCommand = new Command(UpdateTestExecute);
            GetAllTests();
        }
    }
}