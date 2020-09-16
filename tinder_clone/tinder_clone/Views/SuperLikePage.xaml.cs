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
    public partial class SuperLikePage : ContentPage
    {
        MockDataStore dataStore = new MockDataStore();

        //auto generate superlike menu
        public SuperLikePage()
        {
            MockDataStore dataStore = new MockDataStore();
            var superlikeslist = Users.MainUser.SuperLikes;
            for (int i = 0; i < superlikeslist.Count; i++)
            {
                Label namelabel = new Label();
                Label numberlabel = new Label();
                var secondquery = dataStore.GetItemAsync(superlikeslist[i]).Result;
                namelabel.Text = secondquery.Username;
                numberlabel.Text = secondquery.PhoneNumber;
                namelabel.TextColor = Xamarin.Forms.Color.Black;
                numberlabel.TextColor = Xamarin.Forms.Color.Black;
                Grid.SetRow(namelabel, 2 + (i * 2));
                Grid.SetColumn(namelabel, 0);
                Grid.SetRow(numberlabel, 3 + (i * 2));
                Grid.SetColumn(numberlabel, 0);
                namelabel.FontSize = 25;
                numberlabel.FontSize = 25;
                namelabel.FontAttributes = Xamarin.Forms.FontAttributes.Bold;
                numberlabel.FontAttributes = Xamarin.Forms.FontAttributes.Bold;
                namelabel.HorizontalTextAlignment = TextAlignment.Center;
                numberlabel.HorizontalTextAlignment = TextAlignment.Center;
                MainGrid.Children.Add(namelabel);
                MainGrid.Children.Add(numberlabel);

                Button SuperLike = new Button();
                Button SuperDislike = new Button();
                SuperLike.Text = "Super Like";
                SuperDislike.Text = "Super Dislike";
                SuperLike.VerticalOptions = LayoutOptions.End;
                SuperDislike.VerticalOptions = LayoutOptions.Start;
                SuperLike.FontAttributes = FontAttributes.Bold | FontAttributes.Italic;
                SuperDislike.FontAttributes = FontAttributes.Bold | FontAttributes.Italic;
                SuperLike.BackgroundColor = Color.LightSalmon;
                SuperDislike.BackgroundColor = Color.LightSalmon;
                Grid.SetRow(SuperLike, 2 + (i * 2));
                Grid.SetRow(SuperDislike, 3 + (i * 2));
                Grid.SetColumn(SuperLike, 1);
                Grid.SetColumn(SuperDislike, 1);
                SuperLike.Clicked += ((s, e) => SuperProceduralLiked(s, e, i));
                SuperDislike.Clicked += ((s, e) => SuperProceduralDisliked(s, e, i));
                MainGrid.Children.Add(SuperLike);
                MainGrid.Children.Add(SuperDislike);



            }

        }

        //auto generate match
        async void SuperProceduralLiked(object sender, EventArgs e, int i)
        {
           

           
            var superlikedlist = Users.MainUser.SuperLikes;
            var matcheslist = Users.MainUser.Matches;
            var secondpersonid = superlikedlist[i];
            var secondpersonquery = dataStore.GetItemAsync(secondpersonid).Result;
            
            matcheslist.Add(secondpersonid, true);
            superlikedlist.RemoveAt(i);
            Users.MainUser.SuperLikes = superlikedlist;
            Users.MainUser.Matches = matcheslist;
            Users.MainUser.telephonenumbers.Add(secondpersonquery.PhoneNumber);
            Users.MainUser.MatchNames.Add(secondpersonquery.Username);
            await dataStore.UpdateItemAsync(Users.MainUser);

            secondpersonquery.Matches.Add(Users.MainUser.Id, true);
            secondpersonquery.telephonenumbers.Add(Users.MainUser.PhoneNumber);
            secondpersonquery.MatchNames.Add(Users.MainUser.Username);
            await dataStore.UpdateItemAsync(secondpersonquery);

        }

        //auto remove values
        async void SuperProceduralDisliked(object sender, EventArgs e, int i)
        {
            var superlikedlist = Users.MainUser.SuperLikes;
            var matcheslist = Users.MainUser.Matches;
            var secondpersonid = superlikedlist[i];
            matcheslist.Add(secondpersonid, false);
            superlikedlist.RemoveAt(i);
            Users.MainUser.SuperLikes = superlikedlist;
            Users.MainUser.Matches = matcheslist;
            await dataStore.UpdateItemAsync(Users.MainUser);
        }

        //mock like
        void SuperLiked(object sender, EventArgs e)
        { 
        
        }

        //mock dislike
        void SuperDisliked(object sender, EventArgs e)
        {

        }

        //back button
        void Go_Back(object sender, EventArgs e)
        {
            App.Current.MainPage = new HomePage();

        }
    }
}