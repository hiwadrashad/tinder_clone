using System;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Threading.Tasks;
using tinder_clone.Views;
using Xamarin.Forms;
using tinder_clone.Models;
using Plugin.Media;
using Plugin.Media.Abstractions;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using tinder_clone.Services;
using Xamarin.Essentials;
using System.Collections.Generic;
using tinder_clone.Assistant;

namespace tinder_clone.ViewModels
{
    public class RegistrationViewModel : RegistrationPage
    {
        public async void PickPhoto(Image imagesource)
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

            imagesource.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });
        }

        public async void RegisterPerson(Image imagecamera, Entry EntryUserName, Entry EntryUserPassword, Entry EntryUserEmail, Entry EntryUserPhoneNumber)
        {
            var binFormatter = new BinaryFormatter();
            var mstream = new MemoryStream();
            Random rnd = new Random();
            binFormatter.Serialize(mstream, imagecamera);

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
                Latitude = location.Latitude,
                Longitude = location.Longitude,
                Distance = 50
            };


            await Users.dataStore.AddItemAsync(item);
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
    }
}