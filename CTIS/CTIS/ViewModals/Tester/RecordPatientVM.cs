using CTIS.Modal;
using CTIS.Utilities;
using CTIS.Views;
using CTIS.Views.Tester;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace CTIS.ViewModals.Tester
{
    public class RecordPatientVM : BaseVM
    {
        public Patient patient { get; set; }

        public ObservableCollection<string> patientTypeList { get; set; }

        public Role role { get; set; }

        public string Username
        {
            get { return patient.Username; }
            set
            {
                patient.Username = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return patient.Password; }
            set
            {
                patient.Password = value;
                OnPropertyChanged();
            }
        }

        public string FullName
        {
            get { return patient.Name; }
            set
            {
                patient.Name = value;
                OnPropertyChanged();
            }
        }

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

        public Command CancelCommand { get; set; }

        public Command RecordCommand { get; set; }

        private async void CancelExecute(object obj)
        {
            await Application.Current.MainPage.Navigation.PopAsync();
        }

        private async void RecordExecute(object obj)
        {
            if (string.IsNullOrEmpty(Username) && string.IsNullOrEmpty(FullName) && string.IsNullOrEmpty(Password)
                && string.IsNullOrEmpty(Symptoms) && string.IsNullOrEmpty(PatientType))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "Please fill in every field or select every field", "OK");
            }
            else
            {
                role.Username = Username;
                role.Password = Password;
                role.UserType = "Patient";
                await CtisDB.AddPatientAsync(patient, role);
                var ans = await Application.Current.MainPage.DisplayAlert("Question", "Would you like to record test for this patient", "Yes", "No");
                if(ans == true)
                {
                    AddNewTestVM.patient = patient;
                    await Application.Current.MainPage.Navigation.PushAsync(new AddNewTestView());
                }
                else
                {
                    Application.Current.MainPage = new NavigationPage(new TesterMainView());
                }
            }
        }

        public RecordPatientVM()
        {
            patient = new Patient();
            patientTypeList = new ObservableCollection<string>();
            role = new Role();
            patientTypeList.Add("Returnee");
            patientTypeList.Add("Quarantined");
            patientTypeList.Add("Close Contact");
            patientTypeList.Add("Infected");
            patientTypeList.Add("Suspeted");
            CancelCommand = new Command(CancelExecute);
            RecordCommand = new Command(RecordExecute);
        }
    }
}
