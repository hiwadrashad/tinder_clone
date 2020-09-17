using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tinder_clone.Assistant;
using tinder_clone.Models;
using tinder_clone.Services;
using tinder_clone.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace tinder_clone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        LoginViewModel loginvm = new LoginViewModel();

        public LoginPage()
        {
            Users.dataStore = new MockDataStore();
            SetValue(NavigationPage.HasNavigationBarProperty, false);
            InitializeComponent();
        }

        // registration button
        void Handle_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new RegistrationPage();
            //await Navigation.PushAsync(new RegistrationPage());
        }

        // login mechanism
        void Handle_Clicked1(object sender, EventArgs e)
        {
            loginvm.LoginMethod(EntryUser, EntryPassword);
        }
    }
}
