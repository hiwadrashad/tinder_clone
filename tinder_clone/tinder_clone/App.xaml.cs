using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using tinder_clone.Services;
using tinder_clone.Views;

namespace tinder_clone
{
    public partial class App : Application
    {

        public App()
        {
            InitializeComponent();

            DependencyService.Register<MockDataStore>();
            MainPage = new MessagePage();
                       
        }

        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }
    }
}
