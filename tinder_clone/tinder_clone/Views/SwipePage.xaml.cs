using MLToolkit.Forms.SwipeCardView;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tinder_clone.Tables;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tinder_clone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SwipePage : ContentPage
    {
        public int imageid = 0;
        //Dictionary<int, string> imagedic;
        //List<string> imageurls;
        public SwipePage()
        {
            //imageurls.Add("test1.png");
            //imageurls.Add("test2.jpg");
            testmodel testitem = new testmodel();
            testitem.Images = "test1.png";
            BindingContext = testitem;
            InitializeComponent();

        }

        void OnImageNameTapped(object sender, EventArgs args)
        {
            testmodel testitem = new testmodel();
            testitem.Images = "test2.jpg";
            BindingContext = testitem;
            // DisplayAlert("this works","this doesn't work","cancel");
        }


    }
}