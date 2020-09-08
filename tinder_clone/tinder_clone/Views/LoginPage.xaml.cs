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
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            SetValue(NavigationPage.HasNavigationBarProperty, false);
            InitializeComponent();
        }

         void Handle_Clicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new RegistrationPage();
           //await Navigation.PushAsync(new RegistrationPage());
        }

        void Handle_Clicked1(object sender, EventArgs e)
        {
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            var db = new SQLiteConnection(dbpath);
            var myquery = db.Table<RegUserTable>().Where(u => u.Username.Equals(EntryUser.Text) && u.Password.Equals(EntryPassword.Text)).FirstOrDefault();

            if (myquery != null)
            {
                App.Current.MainPage = new HomePage();

            }
            else 
            {
                Device.BeginInvokeOnMainThread(async () =>
                {

                    var result = await this.DisplayAlert("Congratulations", "failed user name and password", "Yes", "Cancel");

                    if (result)
                        App.Current.MainPage = new LoginPage();
                    else
                    {
                        App.Current.MainPage = new LoginPage();
                    }
                });
            }
        
        }
    }
}