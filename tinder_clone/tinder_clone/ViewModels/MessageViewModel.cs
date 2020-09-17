using Plugin.Messaging;
using System;
using System.Windows.Input;
using tinder_clone.Assistant;
using Xamarin.Essentials;
using Xamarin.Forms;
using tinder_clone.Views;

namespace tinder_clone.ViewModels
{
    public class MessageViewModel
    {
        //send sms trough auto generated list of users including data
        public void GenerateListFromDatabase(Grid inputgrid,string textsms, string textcall, Action<object, EventArgs, int> SendSMSprocedural, Action<object, EventArgs, int> SendCallprocedural)
        {
            var telephonenumbers = Users.MainUser.telephonenumbers;
            var names = Users.MainUser.MatchNames;



            // autogenerate all matches trough users database
            for (int i = 0; i < names.Count; i++)
            {
                Label namelabel = new Label();
                Label numberlabel = new Label();
                namelabel.Text = names[i];
                numberlabel.Text = telephonenumbers[i];
                namelabel.TextColor = Xamarin.Forms.Color.Black;
                numberlabel.TextColor = Xamarin.Forms.Color.Black;
                Grid.SetRow(namelabel, 2 + (i * 2));
                Grid.SetColumn(namelabel, 0);
                Grid.SetRow(numberlabel, 3 + (i * 2));
                Grid.SetColumn(numberlabel, 0);
                namelabel.FontSize = 25;
                numberlabel.FontSize = 25;
                namelabel.FontAttributes = Xamarin.Forms.FontAttributes.Bold;
                numberlabel.FontAttributes = Xamarin.Forms.FontAttributes.Bold;
                namelabel.HorizontalTextAlignment = TextAlignment.Center;
                numberlabel.HorizontalTextAlignment = TextAlignment.Center;
                inputgrid.Children.Add(namelabel);
                inputgrid.Children.Add(numberlabel);

                Button smsbutton = new Button();
                Button callbutton = new Button();
                smsbutton.Text = textsms;
                callbutton.Text = textcall;
                smsbutton.VerticalOptions = LayoutOptions.End;
                callbutton.VerticalOptions = LayoutOptions.Start;
                smsbutton.FontAttributes = FontAttributes.Bold | FontAttributes.Italic;
                callbutton.FontAttributes = FontAttributes.Bold | FontAttributes.Italic;
                smsbutton.BackgroundColor = Color.LightSalmon;
                callbutton.BackgroundColor = Color.LightSalmon;
                Grid.SetRow(smsbutton, 2 + (i * 2));
                Grid.SetRow(callbutton, 3 + (i * 2));
                Grid.SetColumn(smsbutton, 1);
                Grid.SetColumn(callbutton, 1);
                smsbutton.Clicked += ((s, e) => SendSMSprocedural(s, e, i));
                callbutton.Clicked += ((s, e) => SendCallprocedural(s, e, i));
                inputgrid.Children.Add(smsbutton);
                inputgrid.Children.Add(callbutton);
            }
        }

        public void SendSMSprocedural( int phoneindex)
        {


            var telephonenumbers = Users.MainUser.telephonenumbers;

            var smsMessanger = CrossMessaging.Current.SmsMessenger;

            if (smsMessanger.CanSendSms)
            {
                smsMessanger.SendSms(telephonenumbers[phoneindex], "Welcome to Xamarin.Forms");
            }

        }

        //send telephone call trough auto generated list of users including data
        public void SendCallprocedural( int phoneindex)
        {
            var telephonenumbers = Users.MainUser.telephonenumbers;

            var phoneCallTask = CrossMessaging.Current.PhoneDialer;
            if (phoneCallTask.CanMakePhoneCall)
            {
                phoneCallTask.MakePhoneCall(telephonenumbers[phoneindex], "my name");
            }

        }

        //mockdata sms call
        public void SendSMS_OnClicked()
        {
            var smsMessanger = CrossMessaging.Current.SmsMessenger;

            if (smsMessanger.CanSendSms)
            {
                smsMessanger.SendSms("+91 7200606860", "Welcome to Xamarin.Forms");
            }
        }

        //mockdata phone call
        public void PhoneCall_OnClicked()
        {
            var phoneCallTask = CrossMessaging.Current.PhoneDialer;
            if (phoneCallTask.CanMakePhoneCall)
            {
                phoneCallTask.MakePhoneCall("+91 7200606860", "my name");
            }
        }
  
    }
}