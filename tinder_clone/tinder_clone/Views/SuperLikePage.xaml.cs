using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tinder_clone.Assistant;
using tinder_clone.Services;
using tinder_clone.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tinder_clone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SuperLikePage : ContentPage
    {
        SuperLikeViewModel SuperLikeVM = new SuperLikeViewModel();

        //auto generate superlike menu
        public SuperLikePage()
        {

            SuperLikeVM.ShowDataList(MainGrid, SuperProceduralLiked, SuperProceduralDisliked);

        }

        //auto generate match
        void SuperProceduralLiked(object sender, EventArgs e, int i)
        {
            SuperLikeVM.SuperProceduralLiked(i);
        }

        //auto remove values
        void SuperProceduralDisliked(object sender, EventArgs e, int i)
        {
            SuperLikeVM.SuperProceduralDisliked(i);
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