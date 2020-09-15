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
using Xamarin.Forms;
using Xamarin.Forms.Xaml;


namespace tinder_clone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {

        public LoginPage()
        {
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
            MockDataStore data = new MockDataStore();
            try
            {
                Users.MainUser = data.GetItemAsyncBynameAndPassword(EntryUser.Text, EntryPassword.Text).Result;
                if (Users.MainUser != null)
                {
                    App.Current.MainPage = new HomePage();
                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {

                        var result = await this.DisplayAlert("Congratulations", "failed user name and password", "Yes", "Cancel");

                        if (result)
                            App.Current.MainPage = new LoginPage();
                        else
                        {
                            App.Current.MainPage = new LoginPage();
                        }
                    });
                }



            }
            catch (Exception ex)
            {
                Device.BeginInvokeOnMainThread(async () =>
                {

                    var result = await this.DisplayAlert("Congratulations", "failed user name and password", "Yes", "Cancel");

                    if (result)
                        App.Current.MainPage = new LoginPage();
                    else
                    {
                        App.Current.MainPage = new LoginPage();
                    }
                });
            }
        }
    }
}
