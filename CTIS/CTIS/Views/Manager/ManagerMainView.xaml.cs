using CTIS.Modal;
using CTIS.Utilities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace CTIS.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ManagerMainView : ContentPage
    {
        public ManagerMainView()
        {
            InitializeComponent();
        }

        protected async override void OnAppearing()
        {
            CentreOfficer centreOfficer = await CtisDB.GetCentreOfficerAsync(App.CentreOfficer.Username, App.CentreOfficer.Password);
            if (!string.IsNullOrEmpty(centreOfficer.CentreID))
            {
                RegisterTestCentre.IsEnabled = false;
            }
            else
            {
                RegisterTestCentre.IsEnabled = true;
            }
        }
    }
}