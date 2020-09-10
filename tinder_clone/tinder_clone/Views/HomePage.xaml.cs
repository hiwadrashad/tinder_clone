﻿using System;
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
    }
}