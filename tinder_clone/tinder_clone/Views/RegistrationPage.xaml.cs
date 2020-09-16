using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using tinder_clone.Services;
using tinder_clone.Models;

namespace tinder_clone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {

        Xamarin.Essentials.Location myLocation;
        public RegistrationPage()
        {

            InitializeComponent();
            this.btnfoto.Clicked += Btnfoto_Clicked;
        }


        //pick picture from gallery
        private async void Btnfoto_Clicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }
            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = PhotoSize.MaxWidthHeight,
                MaxWidthHeight = 500

            }); ;

            if (file == null)
                return;

            this.imgcamara.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }

        //register person
        async void Handle_Clicked(object sender, EventArgs e)
        {
            var binFormatter = new BinaryFormatter();
            var mstream = new MemoryStream();
            Random rnd = new Random();
            binFormatter.Serialize(mstream, this.imgcamara);



            MockDataStore dataStore = new MockDataStore();          
            var request = new GeolocationRequest(GeolocationAccuracy.High);
            var location = await Geolocation.GetLocationAsync(request);




            var item = new Item()
            {
                Id = Guid.NewGuid().ToString(),
                Username = EntryUserName.Text,
                Password = EntryUserPassword.Text,
                Email = EntryUserEmail.Text,
                PhoneNumber = EntryUserPhoneNumber.Text,
                UploadedImage = mstream.ToArray(),
                Matches = new Dictionary<string, bool>(),
                telephonenumbers = new List<string>(),
                MatchNames = new List<string>(),
                SuperLikes = new List<string>(),
                Latitude = myLocation.Latitude,
                Longitude = myLocation.Longitude,
                Distance = 50
            };


            await dataStore.AddItemAsync(item);
            //  var ms = new MemoryStream(item.UploadedImage);
            //   this.imgcamara.Source = ImageSource.FromStream(() => ms);
            Device.BeginInvokeOnMainThread(async () =>
            {

                var result = await this.DisplayAlert("Congratulations", "User Registration Succesfull", "Yes", "Cancel");

                if (result)
                    App.Current.MainPage = new HomePage();

            }
            );
        }

        //back button
        void Back_Page(object sender, EventArgs e)
        {
            App.Current.MainPage = new LoginPage();
        }
    }
}