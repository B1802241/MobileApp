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
    public class GenerateTestReportManagerVM : BaseVM
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
            string centreID = App.CentreOfficer.CentreID;
            List<CovidTest> covidTests = await CtisDB.GetAllCovidTestsAsync();
            foreach (CovidTest covidTest in covidTests)
            {
                if (covidTest.centreID == centreID)
                {
                    TestList.Add(covidTest);
                }
            }
        }

        public GenerateTestReportManagerVM()
        {
            TestList = new ObservableCollection<CovidTest>();
            GetAllCovidTests();
            DetailTestCommand = new Command(DetailTestExecute);
        }
    }
}
