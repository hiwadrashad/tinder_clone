using System;
using System.Collections.Generic;
using System.Text;
using tinder_clone.Views;
using tinder_clone.Assistant;
using tinder_clone.Services;
using Xamarin.Forms;

namespace tinder_clone.ViewModels
{
    class GeoViewModel
    {
        public async void SetDistanceMethod(Slider slider)
        { 
            Users.MainUser.Distance = slider.Value;
            await Users.dataStore.UpdateItemAsync(Users.MainUser);
        }
    }
}
