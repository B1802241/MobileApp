using CTIS.Modal;
using CTIS.Utilities;
using System;
using System.Collections.Generic;
using System.Text;

namespace CTIS.ViewModals
{
    public class DetailTestVM
    {
        public static CovidTest CovidTest;
        public Patient patient { get; set; }
        public string testID
        {
            get { return CovidTest.testID; }
            set { CovidTest.testID = value; }
        }

        public string testDate
        {
            get { return CovidTest.testDate; }
            set { CovidTest.testDate = value; }
        }

        public string result
        {
            get { return CovidTest.result; }
            set { CovidTest.result = value; }
        }

        public string resultDate
        {
            get { return CovidTest.resultDate; }
            set { CovidTest.resultDate = value; }
        }

        public string status
        {
            get { return CovidTest.status; }
            set { CovidTest.status = value; }
        }

        public string testerID
        {
            get { return CovidTest.testerID; }
            set { CovidTest.testerID = value; }
        }

        public string kitID
        {
            get { return CovidTest.kitID; }
            set { CovidTest.kitID = value; }
        }

        public string patientID
        {
            get { return CovidTest.patientID; }
            set { CovidTest.patientID = value; }
        }

        public string patientName
        {
            get
            {
                return CovidTest.patientID;
            }
            set
            {
                CovidTest.patientID = value;
            }
        }

        public async void getPatient()
        {
            patient = await CtisDB.GetPatientAsync(CovidTest.patientID);
        }

        public DetailTestVM()
        {
            getPatient();
        }
    }
}
