using System;
using tinder_clone.Views;
using tinder_clone.Models;
using tinder_clone.Assistant;
using Xamarin.Essentials;
using System.IO;
using tinder_clone.Services;
using Xamarin.Forms;

namespace tinder_clone.ViewModels
{
    public class SwipeViewModel
    {
        public void CreateNextMatch(Image SwipeImage)
        {
            var previousmatcheslist = Users.MainUser.Matches;
            foreach (var item in Users.dataStore.ReturnList())
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
                        SwipeImage.Source = ImageSource.FromStream(() => ms);
                        break;
                    }
                    else
                    {
                        SwipeImage.Source = "test2.jpg";
                    }
                }
                else
                {
                    SwipeImage.Source = "test2.jpg";
                }

            }
        }

        public async void NoClicked()
        {
            var dictionaryofmatches = Users.MainUser.Matches;
            dictionaryofmatches.Add(Users.SwipeUser.Id, false);
            Users.MainUser.Matches = dictionaryofmatches;
            await Users.dataStore.UpdateItemAsync(Users.MainUser);
        }

        public async void Yesclicked()
        {
            var dictionaryofmatches = Users.MainUser.Matches;
            dictionaryofmatches.Add(Users.SwipeUser.Id, true);
            Users.MainUser.Matches = dictionaryofmatches;
            await Users.dataStore.UpdateItemAsync(Users.MainUser);
            if (Users.SwipeUser.Matches[Users.MainUser.Id])
            {
                Users.SwipeUser.telephonenumbers.Add(Users.MainUser.PhoneNumber);
                Users.SwipeUser.MatchNames.Add(Users.MainUser.Username);
                await Users.dataStore.UpdateItemAsync(Users.SwipeUser);

                Users.MainUser.telephonenumbers.Add(Users.SwipeUser.PhoneNumber);
                Users.MainUser.MatchNames.Add(Users.SwipeUser.Username);
                await Users.dataStore.UpdateItemAsync(Users.MainUser);
            }
        }

        public async void SuperLikeClicked()
        {
            var dictionaryofmatches = Users.MainUser.Matches;
            dictionaryofmatches.Add(Users.SwipeUser.Id, true);
            Users.MainUser.Matches = dictionaryofmatches;

            await Users.dataStore.UpdateItemAsync(Users.MainUser);

            if (Users.SwipeUser.Matches[Users.MainUser.Id])
            {
                Users.SwipeUser.telephonenumbers.Add(Users.MainUser.PhoneNumber);
                Users.SwipeUser.MatchNames.Add(Users.MainUser.Username);

                await Users.dataStore.UpdateItemAsync(Users.SwipeUser);

                Users.MainUser.telephonenumbers.Add(Users.SwipeUser.PhoneNumber);
                Users.MainUser.MatchNames.Add(Users.SwipeUser.Username);

                await Users.dataStore.UpdateItemAsync(Users.MainUser);
            }
            else
            {
                var superlikelistsecondperson = Users.SwipeUser.SuperLikes;
                superlikelistsecondperson.Add(Users.MainUser.Id);
                Users.SwipeUser.SuperLikes = superlikelistsecondperson;
                await Users.dataStore.UpdateItemAsync(Users.SwipeUser);
            }
        }
    }
}
