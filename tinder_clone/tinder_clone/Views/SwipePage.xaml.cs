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
using tinder_clone.ViewModels;

namespace tinder_clone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SwipePage : ContentPage
    {
        MockDataStore dataStore = new MockDataStore();

        SwipeViewModel SwipeVM = new SwipeViewModel();
        public SwipePage()
        {
          
            createnextmatch();
    
            

        }

        // generate next potential match to show on the swipepage including geolocation calculator
        public void createnextmatch()
        {
            SwipeVM.CreateNextMatch(this.SwipeImage);
        }

   
        //action after no tapped
        void NoTapped(object sender, EventArgs args)
        {
            SwipeVM.NoClicked();
            createnextmatch();

        }

        //action after yes tapped

        void YesTapped(object sender, EventArgs args)
        {
            SwipeVM.Yesclicked();
            createnextmatch();
    

        }

        //action after super like tapped
        void SuperLikeTapped(object sender, EventArgs args)
        {
            SwipeVM.SuperLikeClicked();
            createnextmatch();

        }

        //back menu tapped
        void Go_Back(object sender, EventArgs e)
        {
            App.Current.MainPage = new LoginPage();
        }


    }
}