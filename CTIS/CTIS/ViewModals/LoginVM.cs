using CTIS.Modal;
using CTIS.Utilities;
using CTIS.Views;
using Xamarin.Forms;

namespace CTIS.ViewModals
{
    class LoginVM : BaseVM
    {
        public Role Role { get; set; }

        public string Username
        {
            get { return Role.Username; }
            set
            {
                Role.Username = value;
                LoginCommand.ChangeCanExecute();
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get { return Role.Password; }
            set
            {
                Role.Password = value;
                LoginCommand.ChangeCanExecute();
                OnPropertyChanged();
            }
        }

        public string UserType
        {
            get { return Role.UserType; }
            set
            {
                Role.UserType = value;
                OnPropertyChanged();
            }
        }

        public Command LoginCommand { get; set; }
        public Command SignUpCommand { get; set; }

        private async void LoginExecute(object obj)
        {
            Role role = await CtisDB.GetRoleAsync(Role);
            if (role != null)
            {
                if(role.UserType == "Manager")
                {
                    CentreOfficer manager = await CtisDB.GetCentreOfficerAsync(role.Username, role.Password);
                    App.CentreOfficer = manager;
                    Application.Current.MainPage = new NavigationPage(new ManagerMainView());
                }
                else if (role.UserType == "Tester")
                {
                    CentreOfficer tester = await CtisDB.GetCentreOfficerAsync(role.Username, role.Password);
                    App.CentreOfficer = tester;
                    Application.Current.MainPage = new NavigationPage(new TesterMainView());
                }
                else if (role.UserType == "Officer")
                {
                    CentreOfficer officer = await CtisDB.GetCentreOfficerAsync(role.Username, role.Password);
                    App.CentreOfficer = officer;
                    Application.Current.MainPage = new NavigationPage(new OfficerMainView());
                }
                else
                {
                    Patient patient = await CtisDB.GetPatientAsync(role.Username, role.Password);
                    App.Patient = patient;
                    Application.Current.MainPage = new NavigationPage(new PatientMainView());
                }
            }
        }

        private bool CanLoginExecute(object obj)
        {
            if (!string.IsNullOrWhiteSpace(Role.Username) &&
                !string.IsNullOrWhiteSpace(Role.Password))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private async void SignUpExecute(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new SignUpView());
        }

        private async void addManager()
        {
            CentreOfficer manager = new CentreOfficer();
            manager.ID = null;
            manager.Username = "a";
            manager.Password = "a";
            manager.Name = "a";
            manager.Position = "Manager";
            manager.CentreID = null;

            Role managerRole = new Role();
            managerRole.Username = "a";
            managerRole.Password = "a";
            managerRole.UserType = "Manager";

            await CtisDB.AddCentreOfficerAsync(manager, managerRole);
        }

        public LoginVM()
        {
            Role = new Role();
            LoginCommand = new Command(LoginExecute, CanLoginExecute);
            SignUpCommand = new Command(SignUpExecute);
        }
    }
}
