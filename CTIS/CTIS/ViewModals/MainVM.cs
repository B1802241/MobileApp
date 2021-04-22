using CTIS.Utilities;
using CTIS.Views;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace CTIS.ViewModals
{
    public class MainVM : BaseVM
    {
        public Command SignUpCommand { get; set; }

        public Command LoginCommand { get; set; }
        private async void SignUpExecute(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new SignUpView());
        }

        private async void LoginExecute(object obj)
        {
            await Application.Current.MainPage.Navigation.PushAsync(new LoginView());
        }
        public MainVM()
        {
            LoginCommand = new Command(LoginExecute);
            SignUpCommand = new Command(SignUpExecute);
            
        }
    }
}
