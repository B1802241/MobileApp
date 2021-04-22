using CTIS.Modal;
using CTIS.Views;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CTIS
{
    public partial class App : Application
    {
        public static CentreOfficer CentreOfficer { get; set; }
        public static Patient Patient { get; set; }
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new MainView());
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
