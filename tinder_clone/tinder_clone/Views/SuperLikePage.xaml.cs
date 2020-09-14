using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tinder_clone.Tables;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tinder_clone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SuperLikePage : ContentPage
    {
        string fileNameu = @"C:\Temp.txt";
        string filenamew = @"C:\Tempw.txt";

        //auto generate superlike menu
        public SuperLikePage()
        {
            InitializeComponent();
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            var db = new SQLiteConnection(dbpath);
            var myquery = db.Table<RegUserTable>().Where(u => u.Username.Equals(File.ReadAllText(fileNameu)) && u.Password.Equals(File.ReadAllText(filenamew))).FirstOrDefault();
            var superlikeslist = myquery.SuperLikes;
            for (int i = 0; i < superlikeslist.Count; i++)
            {
                Label namelabel = new Label();
                Label numberlabel = new Label();
                var secondquery = db.Table<RegUserTable>().Where(u => u.UserId.Equals(superlikeslist[i])).FirstOrDefault();
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
        void SuperProceduralLiked(object sender, EventArgs e, int i)
        {
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            var db = new SQLiteConnection(dbpath);
            var myquery = db.Table<RegUserTable>().Where(u => u.Username.Equals(File.ReadAllText(fileNameu)) && u.Password.Equals(File.ReadAllText(filenamew))).FirstOrDefault();
            var superlikedlist = myquery.SuperLikes;
            var matcheslist = myquery.Matches;
            var secondpersonid = superlikedlist[i];
            var secondpersonquery = db.Table<RegUserTable>().Where(u => u.UserId.Equals(secondpersonid)).FirstOrDefault();
            
            matcheslist.Add(secondpersonid, true);
            secondpersonquery.Matches.Add(myquery.UserId, true);
            superlikedlist.RemoveAt(i);
            myquery.SuperLikes = superlikedlist;
            myquery.Matches = matcheslist;
            db.Update(myquery);
            db.Update(secondpersonquery);

            myquery.telephonenumbers.Add(secondpersonquery.PhoneNumber);
            myquery.MatchNames.Add(secondpersonquery.Username);
            db.Update(myquery);
            secondpersonquery.telephonenumbers.Add(myquery.PhoneNumber);
            secondpersonquery.MatchNames.Add(myquery.Username);
            db.Update(secondpersonquery);

        }

        //auto remove values
        void SuperProceduralDisliked(object sender, EventArgs e, int i)
        {
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            var db = new SQLiteConnection(dbpath);
            var myquery = db.Table<RegUserTable>().Where(u => u.Username.Equals(File.ReadAllText(fileNameu)) && u.Password.Equals(File.ReadAllText(filenamew))).FirstOrDefault();
            var superlikedlist = myquery.SuperLikes;
            var matcheslist = myquery.Matches;
            var secondpersonid = superlikedlist[i];
            matcheslist.Add(secondpersonid, false);
            superlikedlist.RemoveAt(i);
            myquery.SuperLikes = superlikedlist;
            myquery.Matches = matcheslist;
            db.Update(myquery);
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