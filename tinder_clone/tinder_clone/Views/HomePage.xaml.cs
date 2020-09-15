using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tinder_clone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HomePage : ContentPage
    {
        //homepage with click function to navigate to other pages
        public HomePage()
        {
            InitializeComponent();
        }

       


        void SwipePageTapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new SwipePage();

        }

        void ContactPageTapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new MessagePage();

        }

        void LogOutClicked(object sender, EventArgs e)
        {
            App.Current.MainPage = new LoginPage();

        }

        void SuperLikePageTapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new SuperLikePage();

        }

        void SettingsTapped(object sender, EventArgs e)
        {
            App.Current.MainPage = new SuperLikePage();
        }
    }
}