using CTIS.Modal;
using CTIS.Utilities;
using CTIS.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace CTIS.ViewModals.Tester
{
    public class AddNewTestVM : BaseVM
    {
        public static Patient patient { get; set; }

        public ObservableCollection<string> patientTypeList { get; set; }

        public TestKit testKit { get; set; }

        public ObservableCollection<string> TestKitList { get; set; }

        public CovidTest covidTest { get; set; }

        public string Symptoms
        {
            get { return patient.Symptoms; }
            set
            {
                patient.Symptoms = value;
                OnPropertyChanged();
            }
        }

        public string PatientType
        {
            get { return patient.PatientType; }
            set
            {
                patient.PatientType = value;
                OnPropertyChanged();
            }
        }

        private string _TestKitProp;

        public string TestKitProp
        {
            get { return _TestKitProp; }
            set { _TestKitProp = value;
                OnPropertyChanged();
            }
        }



        public Command AddNewTestCommand { get; set; }

        public Command CancelCommand { get; set; }

        private async void CancelExecute(object obj)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async void AddNewTestExecute(object obj)
        {
            if(string.IsNullOrEmpty(Symptoms) && string.IsNullOrEmpty(PatientType) && string.IsNullOrEmpty(TestKitProp))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please fill in every field or select every field", "OK");
            }
            else
            {
                string testKitID = null;
                List<TestKit> testkitChecker = await CtisDB.GetAllTestKitsAsync();
                foreach (TestKit TestKit in testkitChecker)
                {
                    if(TestKit.testName == TestKitProp)
                    {
                        testKit = TestKit;
                    }
                }
                testKitID = testKit.kitID;
                covidTest.kitID = testKitID;
                covidTest.patientID = patient.ID;
                covidTest.status = "Pending";
                string todayDate = DateTime.Now.ToString("dd'/'MM'/'yyyy");
                covidTest.testDate = todayDate;
                await CtisDB.AddCovidTestAsync(covidTest);
                testKit.availableStock -= 1;
                await CtisDB.UpdateTestKitStockAsync(testKit);
                patient.PatientType = PatientType;
                patient.Symptoms = Symptoms;
                await CtisDB.UpdatePatientTypeAsync(patient);
                Application.Current.MainPage = new NavigationPage(new TesterMainView());
            }
        }

        private async void GetAllTestKit()
        {
            List<TestKit> testkit = await CtisDB.GetAllTestKitsAsync();
            TestKitList.Clear();
            foreach (TestKit TestKit in testkit)
            {
                if(TestKit.CentreID == App.CentreOfficer.CentreID)
                {
                    TestKitList.Add(TestKit.testName);
                }
            }
        }

        private async void directBackToMainPage()
        {
            await Application.Current.MainPage.DisplayAlert("Error", "Manager has not added any test kit yet", "OK");
            Application.Current.MainPage = new NavigationPage(new TesterMainView());
        }

        public AddNewTestVM()
        {
            AddNewTestCommand = new Command(AddNewTestExecute);
            CancelCommand = new Command(CancelExecute);
            patientTypeList = new ObservableCollection<string>();
            patientTypeList.Add("Returnee");
            patientTypeList.Add("Quarantined");
            patientTypeList.Add("Close Contact");
            patientTypeList.Add("Infected");
            patientTypeList.Add("Suspeted");
            testKit = new TestKit();
            TestKitList = new ObservableCollection<string>();
            covidTest = new CovidTest();
            GetAllTestKit();
        }
    }
}
