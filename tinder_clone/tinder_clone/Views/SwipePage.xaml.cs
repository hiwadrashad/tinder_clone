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
        Item eligableuser;
        MockDataStore dataStore = new MockDataStore();

        //        LoginPage page = new LoginPage();
        public SwipePage()
        {
            //   LoginPage login = new LoginPage();
            createnextmatch();
    
            //mockdata
            // var listofallavailable = db.Table<RegUserTable>().Where(u => u.UserId)
            //  testmodel testitem = new testmodel();
            //  testitem.Images = "test1.png";
            //  BindingContext = testitem;
            //  InitializeComponent();

        }

        // generate next potential match to show on the swipepage including geolocation calculator
        public void createnextmatch()
        {
            var previousmatcheslist = Users.MainUser.Matches;
            for (int i = 0; i < previousmatcheslist.Count; i++)
            {
                if (db.Table<RegUserTable>().Where(u => !u.UserId.Equals(previousmatcheslist[i])) != null)
                {
                    var testeligableuser = db.Table<RegUserTable>().Where(u => !u.UserId.Equals(previousmatcheslist[i])).FirstOrDefault();

                    Location firstpersonlocation = new Location(myquery.Latitude, myquery.Longitude);
                    Location secondpersonlocation = new Location(testeligableuser.Latitude, testeligableuser.Longitude);
                    double distance = Location.CalculateDistance(firstpersonlocation, secondpersonlocation, DistanceUnits.Kilometers);

                    if (myquery.Distance < distance && testeligableuser.Distance < distance)
                    {

                        eligableuser = db.Table<RegUserTable>().Where(u => !u.UserId.Equals(previousmatcheslist[i])).FirstOrDefault();
                        var ms = new MemoryStream(eligableuser.UploadedImage);
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
        void NoTapped(object sender, EventArgs args)
        { 
            var dictionaryofmatches = Users.MainUser.Matches;
            dictionaryofmatches.Add(eligableuser.Id, false);
            myquery.Matches = dictionaryofmatches;
            db.Update(myquery);
            createnextmatch();
            //mockdata
            // testmodel testitem = new testmodel();
            // testitem.Images = "test2.jpg";
            // BindingContext = testitem;

        }

        //action after yes tapped

        async void YesTapped(object sender, EventArgs args)
        {
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            var db = new SQLiteConnection(dbpath);
            Users.SwipeUser = dataStore.GetItemAsync(eligableuser.Id).Result;
            var dictionaryofmatches = Users.MainUser.Matches;
            dictionaryofmatches.Add(eligableuser.Id, true);
            Users.MainUser.Matches = dictionaryofmatches;
            await dataStore.UpdateItemAsync(Users.MainUser);
            if (eligableuser.Matches[Users.MainUser.Id])
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
        void SuperLikeTapped(object sender, EventArgs args)
        {
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            var db = new SQLiteConnection(dbpath);
            var myquery = db.Table<RegUserTable>().Where(u => u.Username.Equals(File.ReadAllText(fileNameu)) && u.Password.Equals(File.ReadAllText(filenamew))).FirstOrDefault();
            var secondpersonquery = db.Table<RegUserTable>().Where(u => u.UserId.Equals(eligableuser.UserId)).FirstOrDefault();
            var dictionaryofmatches = myquery.Matches;
            dictionaryofmatches.Add(eligableuser.UserId, true);
            myquery.Matches = dictionaryofmatches;
            db.Update(myquery);
            if (eligableuser.Matches[myquery.UserId])
            {
                secondpersonquery.telephonenumbers.Add(myquery.PhoneNumber);
                secondpersonquery.MatchNames.Add(myquery.Username);
                db.Update(secondpersonquery);
                myquery.telephonenumbers.Add(secondpersonquery.PhoneNumber);
                myquery.MatchNames.Add(secondpersonquery.Username);
                db.Update(myquery);
            }
            else
            {
                var superlikelistsecondperson = secondpersonquery.SuperLikes;
                superlikelistsecondperson.Add(myquery.UserId);
                secondpersonquery.SuperLikes = superlikelistsecondperson;
                db.Update(secondpersonquery);
            }
            createnextmatch();
            //mockdata
            // testmodel testitem = new testmodel();
            // testitem.Images = "test2.jpg";
            // BindingContext = testitem;

        }

        //back menu tapped
        void Go_Back(object sender, EventArgs e)
        {
            App.Current.MainPage = new LoginPage();
        }


    }
}