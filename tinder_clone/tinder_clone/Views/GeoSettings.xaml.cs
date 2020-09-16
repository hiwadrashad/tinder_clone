using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tinder_clone.Assistant;
using tinder_clone.Services;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tinder_clone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class GeoSettings : ContentPage
    {

        public GeoSettings()
        {
            InitializeComponent();
        }

        //set maximum distance allowed
        async void SetDistance_Clicked(object sender, EventArgs e)
        {
            MockDataStore dataStore = new MockDataStore();
            Users.MainUser.Distance = Mainslider.Value;
            await dataStore.UpdateItemAsync(Users.MainUser);

        }

        // set label value
        void ValueChanged(object sender, EventArgs e)
        {
            MainLabel.Text = Mainslider.Value.ToString();
        }

        //back button
        void BackButton_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new LoginPage();
        }

    }
}