using Plugin.Messaging;
using System;
using System.Windows.Input;
using tinder_clone.Assistant;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace tinder_clone.ViewModels
{
    public class MessageViewModel : BaseViewModel
    {

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