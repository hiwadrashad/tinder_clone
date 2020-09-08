using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using SQLite;
using tinder_clone.Tables;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tinder_clone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {
    
        public RegistrationPage()
        {

            InitializeComponent();
            this.btnfoto.Clicked += Btnfoto_Clicked;
        }


        private async void Btnfoto_Clicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("Photos Not Supported", ":( Permission not granted to photos.", "OK");
                return;
            }
            var file = await Plugin.Media.CrossMedia.Current.PickPhotoAsync(new Plugin.Media.Abstractions.PickMediaOptions
            {
                PhotoSize = Plugin.Media.Abstractions.PhotoSize.Medium,

            });

            if (file == null)
                return;

            this.imgcamara.Source = ImageSource.FromStream(() =>
            {
                var stream = file.GetStream();
                file.Dispose();
                return stream;
            });

            /* try
             {
                 await CrossMedia.Current.Initialize();

                 if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsPickPhotoSupported)
                 {
                     await DisplayAlert("no camera available", "use another mobile", "OK");
                     return;
                 }
                 var file = await CrossMedia.Current.TakePhotoAsync(new Plugin.Media.Abstractions.StoreCameraMediaOptions { SaveToAlbum = true });
                 if (file == null)
                     return;
                 this.imgcamara.Source = ImageSource.FromStream(() =>
                 {
                     var stream = file.GetStream();
                     return stream;
                 });

                 await DisplayAlert("foto ", "locatie" + file.AlbumPath, "ok");
             }
             catch (Exception ex)
             {
                 await DisplayAlert("permission denied", "permissions is denied \nError: " + ex.Message, "Ok");
             } */

        }

        void Handle_Clicked(object sender, EventArgs e)
        {
            var binFormatter = new BinaryFormatter();
            var mstream = new MemoryStream();
            Random rnd = new Random();
            binFormatter.Serialize(mstream, this.imgcamara);
           

            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            var db = new SQLiteConnection(dbpath);
            db.CreateTable<RegUserTable>();


            var item = new RegUserTable()
            {
                UserId = rnd.Next(10000000),
                Username = EntryUserName.Text,
                Password = EntryUserPassword.Text,
                Email = EntryUserEmail.Text,
                PhoneNumber = EntryUserPhoneNumber.Text,
                UploadedImage = mstream.ToArray()
                

            };
            var ms = new MemoryStream(item.UploadedImage);
            var returnImage = BitmapImage();

            db.Insert(item);
            Device.BeginInvokeOnMainThread(async () =>
            {

                var result = await this.DisplayAlert("Congratulations", "User Registration Succesfull", "Yes", "Cancel");

                if (result)
                    App.Current.MainPage = new LoginPage();

            }
            );
        }
    }
}