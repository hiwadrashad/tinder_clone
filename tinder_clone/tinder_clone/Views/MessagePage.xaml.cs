using Plugin.Messaging;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tinder_clone.Assistant;
using tinder_clone.Services;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using tinder_clone.ViewModels;

namespace tinder_clone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]

  
    // message page to contact users
    public partial class MessagePage : ContentPage
    {

        ViewModels.MessageViewModel messageViewModel = new MessageViewModel();
        public MessagePage()
        {
            InitializeComponent();
            //auto generate list of all matches
            messageViewModel.GenerateListFromDatabase(MainGrid,"Send SMS", "Phone Call", SendSMSprocedural, SendCallprocedural);              

        }

        
        
        //sendsms trough auto generated list of users including data
         void SendSMSprocedural(object sender, EventArgs e, int phoneindex)
        {
            messageViewModel.SendSMSprocedural(phoneindex);

        }

        //send telephone call trough auto generated list of users including data
        void SendCallprocedural(object sender, EventArgs e, int phoneindex)
        {
            messageViewModel.SendCallprocedural(phoneindex);
 
        }

        //mockdata sms call
         void SendSMS_OnClicked (object sender, EventArgs e)
        {
            messageViewModel.SendSMS_OnClicked();
        }

        //mockdata phone call
         void PhoneCall_OnClicked(object sender, EventArgs e)
        {
            messageViewModel.PhoneCall_OnClicked();
        }

        //back buttno
        void Go_Back(object sender, EventArgs e)
        {
            App.Current.MainPage = new HomePage();

        }
    }
}