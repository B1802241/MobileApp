using CTIS.Modal;
using CTIS.Utilities;
using CTIS.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace CTIS.ViewModals
{
    public class TestHistoryVM : BaseVM
    {
        public ObservableCollection<CovidTest> TestList { get; set; }

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
                    DetailTestCommand.Execute(SelectedItem);
                }
            }
        }

        public Command DetailTestCommand { get; set; }

        private async void DetailTestExecute(object obj)
        {
            DetailTestVM.CovidTest = (CovidTest)obj;
            await Application.Current.MainPage.Navigation.PushAsync(new DetailTestView());
        }

        private async void GetAllCovidTests()
        {
            List<CovidTest> covidTests = await CtisDB.GetAllCovidTestsAsync();
            foreach (CovidTest covidTest in covidTests)
            {
                if (covidTest.patientID == App.Patient.ID)
                {
                    TestList.Add(covidTest);
                }
            }
        }

        public TestHistoryVM()
        {
            TestList = new ObservableCollection<CovidTest>();
            DetailTestCommand = new Command(DetailTestExecute);
            GetAllCovidTests();
        }
    }
}
