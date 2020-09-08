using MLToolkit.Forms.SwipeCardView;
using SQLite;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tinder_clone.Tables;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using tinder_clone.Views;

namespace tinder_clone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SwipePage : ContentPage
    {
        string fileNameu = @"C:\Temp.txt";
        string filenamew = @"C:\Tempw.txt";

        LoginPage page = new LoginPage();
        public SwipePage()
        {
            LoginPage login = new LoginPage();
            
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            var db = new SQLiteConnection(dbpath);
            var myquery = db.Table<RegUserTable>().Where(u => u.Username.Equals(File.ReadAllText(fileNameu)) && u.Password.Equals(File.ReadAllText(filenamew))).FirstOrDefault();
            var previousmatcheslist =  myquery.Matches;
          //  var listofallavailable = db.Table<RegUserTable>().Where(u => u.UserId)
            // testmodel testitem = new testmodel();
           // testitem.Images = "test1.png";
           // BindingContext = testitem;
           // InitializeComponent();

        }

        void OnImageNameTapped(object sender, EventArgs args)
        {
           // testmodel testitem = new testmodel();
           // testitem.Images = "test2.jpg";
           // BindingContext = testitem;
          
        }


    }
}