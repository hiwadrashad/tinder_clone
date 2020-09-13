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
    public partial class GeoSettings : ContentPage
    {
        string fileNameu = @"C:\Temp.txt";
        string filenamew = @"C:\Tempw.txt";

        public GeoSettings()
        {
            InitializeComponent();
        }

        void SetDistance_Clicked(object sender, EventArgs e)
        {
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            var db = new SQLiteConnection(dbpath);
            var myquery = db.Table<RegUserTable>().Where(u => u.Username.Equals(File.ReadAllText(fileNameu)) && u.Password.Equals(File.ReadAllText(filenamew))).FirstOrDefault();
            myquery.Distance = Mainslider.Value;
            db.Update(myquery);

        }
        void BackButton_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new LoginPage();
        }

    }
}