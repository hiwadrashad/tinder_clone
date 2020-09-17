using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using SQLite;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using tinder_clone.Services;
using tinder_clone.Models;
using tinder_clone.ViewModels;

namespace tinder_clone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class RegistrationPage : ContentPage
    {

        RegistrationViewModel registrationVM = new RegistrationViewModel();
        public RegistrationPage()
        {

            InitializeComponent();
            this.btnfoto.Clicked += Btnfoto_Clicked;
        }


        //pick picture from gallery
        private void Btnfoto_Clicked(object sender, EventArgs e)
        {
            registrationVM.PickPhoto(imgcamara);
        }

        //register person
        void Handle_Clicked(object sender, EventArgs e)
        {
            registrationVM.RegisterPerson(imgcamara, EntryUserName, EntryUserPassword, EntryUserEmail, EntryUserPhoneNumber);
        }

        //back button
        void Back_Page(object sender, EventArgs e)
        {
            App.Current.MainPage = new LoginPage();
        }
    }
}