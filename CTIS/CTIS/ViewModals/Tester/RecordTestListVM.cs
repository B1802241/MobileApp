
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
    public class RecordTestListVM : BaseVM
    {
        public ObservableCollection<Patient> PatientList { get; set; }

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
                    AddTestCommand.Execute(SelectedItem);
                }
            }
        }

        public Command AddTestCommand { get; set; }

        private async void AddTestExecute(object obj)
        {
            AddNewTestVM.patient = (Patient)obj;
            await Application.Current.MainPage.Navigation.PushAsync(new AddNewTestView());
        }

        private async void GetAllPatient()
        {
            List<Patient> patient = await CtisDB.GetAllPatientsAsync();
            PatientList.Clear();
            foreach(Patient Patient in patient)
            {
                if(Patient.CentreID == App.CentreOfficer.CentreID)
                {
                    PatientList.Add(Patient);
                }
            }
        }

        public RecordTestListVM()
        {
            AddTestCommand = new Command(AddTestExecute);
            PatientList = new ObservableCollection<Patient>();
            GetAllPatient();
        }
    }
}
