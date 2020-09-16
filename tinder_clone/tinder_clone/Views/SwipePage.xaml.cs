using MLToolkit.Forms.SwipeCardView;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using tinder_clone.Views;
using Xamarin.Essentials;
using tinder_clone.Models;
using tinder_clone.Assistant;
using tinder_clone.Services;

namespace tinder_clone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SwipePage : ContentPage
    {
        MockDataStore dataStore = new MockDataStore();
        public SwipePage()
        {
          
            createnextmatch();
    
            

        }

        // generate next potential match to show on the swipepage including geolocation calculator
        public void createnextmatch()
        {
            var previousmatcheslist = Users.MainUser.Matches;
            foreach (var item in dataStore.ReturnList())
            {
                if (!Users.MainUser.Matches.ContainsKey(item.Id))
                {

                    Location firstpersonlocation = new Location(Users.MainUser.Latitude, Users.MainUser.Longitude);
                    Location secondpersonlocation = new Location(item.Latitude, item.Longitude);
                    double distance = Location.CalculateDistance(firstpersonlocation, secondpersonlocation, DistanceUnits.Kilometers);

                    if (Users.MainUser.Distance < distance && item.Distance < distance)
                    {

                        Users.SwipeUser = item;
                        var ms = new MemoryStream(item.UploadedImage);
                        this.SwipeImage.Source = ImageSource.FromStream(() => ms);
                        break;
                    }
                    else
                    {
                        this.SwipeImage.Source = "test2.jpg";
                    }
                }
                else
                {
                    this.SwipeImage.Source = "test2.jpg";
                }

            }

        }

   
        //action after no tapped
        async void NoTapped(object sender, EventArgs args)
        { 
            var dictionaryofmatches = Users.MainUser.Matches;
            dictionaryofmatches.Add(Users.SwipeUser.Id, false);
            Users.MainUser.Matches = dictionaryofmatches;
            await dataStore.UpdateItemAsync(Users.MainUser);
            createnextmatch();

        }

        //action after yes tapped

        async void YesTapped(object sender, EventArgs args)
        {
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            var db = new SQLiteConnection(dbpath);
            var dictionaryofmatches = Users.MainUser.Matches;
            dictionaryofmatches.Add(Users.SwipeUser.Id, true);
            Users.MainUser.Matches = dictionaryofmatches;
            await dataStore.UpdateItemAsync(Users.MainUser);
            if (Users.SwipeUser.Matches[Users.MainUser.Id])
            {
                Users.SwipeUser.telephonenumbers.Add(Users.MainUser.PhoneNumber);
                Users.SwipeUser.MatchNames.Add(Users.MainUser.Username);
                await dataStore.UpdateItemAsync(Users.SwipeUser);

                Users.MainUser.telephonenumbers.Add(Users.SwipeUser.PhoneNumber);
                Users.MainUser.MatchNames.Add(Users.SwipeUser.Username);
                await dataStore.UpdateItemAsync(Users.MainUser);
            }
            createnextmatch();
            //mockdata
            // testmodel testitem = new testmodel();
            // testitem.Images = "test2.jpg";
            // BindingContext = testitem;

        }

        //action after super like tapped
        async void SuperLikeTapped(object sender, EventArgs args)
        {
            var dictionaryofmatches = Users.MainUser.Matches;
            dictionaryofmatches.Add(Users.SwipeUser.Id, true);
            Users.MainUser.Matches = dictionaryofmatches;

            await dataStore.UpdateItemAsync(Users.MainUser);

            if (Users.SwipeUser.Matches[Users.MainUser.Id])
            {
                Users.SwipeUser.telephonenumbers.Add(Users.MainUser.PhoneNumber);
                Users.SwipeUser.MatchNames.Add(Users.MainUser.Username);

                await dataStore.UpdateItemAsync(Users.SwipeUser);

                Users.MainUser.telephonenumbers.Add(Users.SwipeUser.PhoneNumber);
                Users.MainUser.MatchNames.Add(Users.SwipeUser.Username);

                await dataStore.UpdateItemAsync(Users.MainUser);
            }
            else
            {
                var superlikelistsecondperson = Users.SwipeUser.SuperLikes;
                superlikelistsecondperson.Add(Users.MainUser.Id);
                Users.SwipeUser.SuperLikes = superlikelistsecondperson;
                await dataStore.UpdateItemAsync(Users.SwipeUser);
            }
            createnextmatch();

        }

        //back menu tapped
        void Go_Back(object sender, EventArgs e)
        {
            App.Current.MainPage = new LoginPage();
        }


    }
}