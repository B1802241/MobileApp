using CTIS.Modal;
using CTIS.Utilities;
using CTIS.Views;
using CTIS.Views.Tester;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace CTIS.ViewModals.Tester
{
    class UpdateTestVM : BaseVM
    {
        public static CovidTest CovidTest;

        public string result
        {
            get { return CovidTest.result; }
            set
            {
                CovidTest.result = value;
                OnPropertyChanged();
            }
        }

        public string resultDate
        {
            get { return CovidTest.resultDate; }
            set
            {
                CovidTest.resultDate = value;
                OnPropertyChanged();
            }
        }

        public string status
        {
            get { return CovidTest.status; }
            set
            {
                CovidTest.status = value;
                OnPropertyChanged();
            }
        }

        public string testID
        {
            get { return CovidTest.testID; }
            set
            {
                CovidTest.testID = value;
                OnPropertyChanged();
            }
        }

        public string patientID
        {
            get { return CovidTest.patientID; }
            set
            {
                CovidTest.patientID = value;
                OnPropertyChanged();
            }
        }

        public string testDate
        {
            get { return CovidTest.testDate; }
            set
            {
                CovidTest.testDate = value;
                OnPropertyChanged();
            }
        }

        public ICommand UpdateCommand { get; set; }
        public ICommand CancelCommand { get; set; }

        private async void UpdateExecute(object obj)
        {
            CovidTest.resultDate = DateTime.Now.ToString();
            CovidTest.status = "complete";
            await CtisDB.UpdateTestAsync(CovidTest);
            await Application.Current.MainPage.Navigation.PopAsync();
            await Application.Current.MainPage.Navigation.PushAsync(new TesterMainView());
        }

        private async void CancelExecute(object obj)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
            await Application.Current.MainPage.Navigation.PushAsync(new TesterMainView());
        }

        public UpdateTestVM()
        {
            UpdateCommand = new Command(UpdateExecute);
            CancelCommand = new Command(CancelExecute);

            testID = CovidTest.testID;
            testDate = CovidTest.testDate;
            patientID = CovidTest.patientID;
        }


    }
}

