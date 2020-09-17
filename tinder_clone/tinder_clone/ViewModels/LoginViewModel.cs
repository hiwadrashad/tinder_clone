using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using tinder_clone.Views;
using tinder_clone.Assistant;
using Xamarin.Forms;
using tinder_clone.Models;
using tinder_clone.Services;


namespace tinder_clone.ViewModels
{
    public class LoginViewModel: LoginPage
    {

       #pragma warning disable CS1998 
        public async void LoginMethod(Entry username, Entry password)
        #pragma warning restore CS1998
        {
            try
            {
                Users.MainUser = Users.dataStore.GetItemAsyncBynameAndPassword(username.Text, password.Text).Result;
                if (Users.MainUser != null)
                {
                    App.Current.MainPage = new HomePage();
                }
                else
                {
                    Device.BeginInvokeOnMainThread(async () =>
                    {

                        var result = await DisplayAlert("Congratulations", "failed user name and password", "Yes", "Cancel");

                        if (result)
                            App.Current.MainPage = new LoginPage();
                        else
                        {
                            App.Current.MainPage = new LoginPage();
                        }
                    });
                }



            }
#pragma warning disable CS0168
            catch (Exception ex)
#pragma warning restore CS0168 
            {
                Device.BeginInvokeOnMainThread(async () =>
                {

                    var result = await DisplayAlert("Congratulations", "failed user name and password", "Yes", "Cancel");

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
