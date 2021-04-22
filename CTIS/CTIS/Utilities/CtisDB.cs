using CTIS.Modal;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CTIS.Utilities
{
    public class CtisDB
    {
        static FirebaseClient firebaseClient = new FirebaseClient("https://bmc208a.firebaseio.com/");

        #region Patient start
        public static async Task<List<Patient>> GetAllPatientsAsync()
        {
            return (await firebaseClient.Child("Patient").OnceAsync<Patient>()).Select(
                item => new Patient
                {
                    ID = item.Object.ID,
                    Username = item.Object.Username,
                    Password = item.Object.Password,
                    Name = item.Object.Name,
                    PatientType = item.Object.PatientType,
                    Symptoms = item.Object.Symptoms,
                    CentreID = item.Object.CentreID
                }).ToList();
        }

        public static async Task<Patient> GetPatientAsync(string username, string password)
        {
            List<Patient> allPatients = await GetAllPatientsAsync();
            return allPatients.Where(a => a.Username == username && a.Password == password).FirstOrDefault();
        }

        public static async Task<Patient> GetPatientAsync(string patientID)
        {
            List<Patient> allPatients = await GetAllPatientsAsync();
            return allPatients.Where(a => a.ID == patientID).FirstOrDefault();
        }

        public static async Task AddPatientAsync(Patient patient, Role role)
        {
            patient.ID = Guid.NewGuid().ToString();
            patient.CentreID = App.CentreOfficer.CentreID;
            await firebaseClient.Child("Patient").PostAsync(patient);
            await firebaseClient.Child("Role").PostAsync(role);
        }

        public static async Task UpdatePatientTypeAsync(Patient patient)
        {
            var toUpdatePatient = (await firebaseClient.Child("Patient").OnceAsync<Patient>()).Where(a => a.Object.ID == patient.ID).FirstOrDefault();

            await firebaseClient.Child("Patient").Child(toUpdatePatient.Key).PutAsync(new Patient() { ID = patient.ID, Username = patient.Username,Password = patient.Password,PatientType = patient.PatientType,Symptoms = patient.Symptoms,Name = patient.Name, CentreID = patient.CentreID });
        }
        #endregion User end

        #region CentreOfficer start
        public static async Task<List<CentreOfficer>> GetAllCentreOfficersAsync()
        {
            return (await firebaseClient.Child("CentreOfficer").OnceAsync<CentreOfficer>()).Select(
                item => new CentreOfficer
                {
                    ID = item.Object.ID,
                    Username = item.Object.Username,
                    Password = item.Object.Password,
                    Name = item.Object.Name,
                    Position = item.Object.Position,
                    CentreID = item.Object.CentreID

                }).ToList();
        }

        public static async Task<CentreOfficer> GetCentreOfficerAsync(string username, string password)
        {
            List<CentreOfficer> allCentreOfficers = await GetAllCentreOfficersAsync();
            return allCentreOfficers.Where(a => a.Username == username && a.Password == password).FirstOrDefault();
        }

        public static async Task AddCentreOfficerAsync(CentreOfficer centreOfficer, Role role)
        {
            centreOfficer.ID = Guid.NewGuid().ToString();
            string username = centreOfficer.Username;
            if(centreOfficer.Position == "Tester")
            {
                centreOfficer.CentreID = App.CentreOfficer.CentreID;
            }
            
            await firebaseClient.Child("CentreOfficer").PostAsync(centreOfficer);
            await firebaseClient.Child("Role").PostAsync(role);
        }
        #endregion CantreOfficer end

        #region TestCentre start
        public static async Task<List<TestCentre>> GetAllTestCentresAsync()
        {
            return (await firebaseClient.Child("TestCentre").OnceAsync<TestCentre>()).Select(
                item => new TestCentre
                {
                    centreID = item.Object.centreID,
                    centreName = item.Object.centreName

                }).ToList();
        }

        public static async Task<TestCentre> GetTestCentreAsync(TestCentre testCentre)
        {
            List<TestCentre> allTestCentres = await GetAllTestCentresAsync();
            return allTestCentres.Where(a => a.centreID == testCentre.centreID).FirstOrDefault();
        }

        public static async Task AddTestCentreAsync(TestCentre testCentre)
        {
            testCentre.centreID = Guid.NewGuid().ToString();
            
            App.CentreOfficer.CentreID = testCentre.centreID;
            var toSetCentreID = (await firebaseClient.Child("CentreOfficer").
                OnceAsync<CentreOfficer>()).Where(a => a.Object.ID == App.CentreOfficer.ID).
                FirstOrDefault();

            await firebaseClient.Child("CentreOfficer").Child(toSetCentreID.Key).
                PutAsync(new CentreOfficer()
                {
                    ID = App.CentreOfficer.ID,
                    Username = App.CentreOfficer.Username,
                    Password = App.CentreOfficer.Password,
                    Name = App.CentreOfficer.Name,
                    Position = App.CentreOfficer.Position,
                    CentreID = testCentre.centreID
                });

            await firebaseClient.Child("TestCentre").PostAsync(testCentre);
        }
        #endregion

        #region TestKit start
        public static async Task<List<TestKit>> GetAllTestKitsAsync()
        {
            return (await firebaseClient.Child("TestKit").OnceAsync<TestKit>()).Select(
                item => new TestKit
                {
                    kitID = item.Object.kitID,
                    testName = item.Object.testName,
                    availableStock = item.Object.availableStock,
                    CentreID = item.Object.CentreID
                }).ToList();
        }

        public static async Task<TestKit> GetTestKitAsync(TestKit testKit)
        {
            List<TestKit> allTestKits = await GetAllTestKitsAsync();
            return allTestKits.Where(a => a.kitID == testKit.kitID).FirstOrDefault();
        }

        public static async Task AddTestKitAsync(TestKit testKit)
        {
            testKit.kitID = Guid.NewGuid().ToString();
            testKit.CentreID = App.CentreOfficer.CentreID;
            await firebaseClient.Child("TestKit").PostAsync(testKit);
        }
        
        public static async Task UpdateTestKitStockAsync(TestKit testKit)
        {
            var toUpdateTestKit = (await firebaseClient.Child("TestKit").OnceAsync<TestKit>()).Where(a => a.Object.kitID == testKit.kitID).FirstOrDefault();

            await firebaseClient.Child("TestKit").Child(toUpdateTestKit.Key).PutAsync(new TestKit() { kitID = testKit.kitID, testName = testKit.testName, availableStock = testKit.availableStock,CentreID = testKit.CentreID });
        }
        #endregion

        #region CovidTest start
        public static async Task<List<CovidTest>> GetAllCovidTestsAsync()
        {
            return (await firebaseClient.Child("CovidTest").OnceAsync<CovidTest>()).Select(
                item => new CovidTest
                {
                    testID = item.Object.testID,
                    testDate = item.Object.testDate,
                    result = item.Object.result,
                    resultDate = item.Object.resultDate,
                    status = item.Object.status,
                    testerID = item.Object.testerID,
                    kitID = item.Object.kitID,
                    patientID = item.Object.patientID,
                    centreID = item.Object.centreID
                }).ToList();
        }

        public static async Task<CovidTest> GetCovidTestAsync(CovidTest covidTest)
        {
            List<CovidTest> allCovidTests = await GetAllCovidTestsAsync();
            return allCovidTests.Where(a => a.testID == covidTest.testID).FirstOrDefault();
        }

        public static async Task AddCovidTestAsync(CovidTest covidTest)
        {
            covidTest.testID = Guid.NewGuid().ToString();
            covidTest.testerID = App.CentreOfficer.ID;
            covidTest.centreID = App.CentreOfficer.CentreID;
            await firebaseClient.Child("CovidTest").PostAsync(covidTest);
        }

        public static async Task UpdateTestAsync(CovidTest covidTest)
        {
            var toUpdateTest = (await firebaseClient.Child("CovidTest").OnceAsync<CovidTest>()).Where(a => a.Object.testID == covidTest.testID).FirstOrDefault();

            await firebaseClient.Child("CovidTest").Child(toUpdateTest.Key).PutAsync(new CovidTest() { testID = covidTest.testID, patientID = covidTest.patientID, testerID = covidTest.testerID, result = covidTest.result, resultDate = covidTest.resultDate, kitID = covidTest.kitID, status = covidTest.status, testDate = covidTest.testDate,centreID = covidTest.centreID });
        }
        #endregion

        #region Role start
        public static async Task<List<Role>> GetAllRolesAsync()
        {
            return (await firebaseClient.Child("Role").OnceAsync<Role>()).Select(
                item => new Role
                {
                    Username = item.Object.Username,
                    Password = item.Object.Password,
                    UserType = item.Object.UserType
                    
                }).ToList();
        }

        public static async Task<Role> GetRoleAsync(Role role)
        {
            List<Role> allRoles = await GetAllRolesAsync();
            return allRoles.Where(a => a.Username == role.Username && a.Password == role.Password).FirstOrDefault();
        }
        #endregion
    }
}
