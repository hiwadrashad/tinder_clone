using Plugin.Messaging;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tinder_clone.Tables;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tinder_clone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MessagePage : ContentPage
    {

     //   string fileNameu = @"C:\Temp.txt";
     //   string filenamew = @"C:\Tempw.txt";

        public MessagePage()
        {
            InitializeComponent();
            

          /*  var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            var db = new SQLiteConnection(dbpath);
            var myquery = db.Table<RegUserTable>().Where(u => u.Username.Equals(File.ReadAllText(fileNameu)) && u.Password.Equals(File.ReadAllText(filenamew))).FirstOrDefault();
            var matchnumbers = myquery.telephonenumbers;
            var matchnames = myquery.MatchNames;


            for (int i = 0; i < matchnumbers.Count; i++)
            {
                Label label = new Label();
                label.Text = matchnames[i];
                Grid.SetRow(label, 2 + i);
                Grid.SetColumn(label, 0);
                label.FontSize = 30;
                label.HorizontalTextAlignment = TextAlignment.Center;
                MainGrid.Children.Add(label);

                Button button = new Button();
                button.Text = "Send Sms";
                Grid.SetRow(button, 3 + i);
                Grid.SetColumn(button, 0);
                button.Clicked += SendSMSprocedural;
                MainGrid.Children.Add(button);


                Button buttoncall = new Button();
                buttoncall.Text = "Call";
                Grid.SetRow(buttoncall, 4 + i);
                Grid.SetColumn(buttoncall, 0);
                buttoncall.Clicked += PhoneCall_OnClicked;
                MainGrid.Children.Add(buttoncall);
            }
            */

          /*  Label label = new Label();
            label.Text = "mellisa bangs";
            Grid.SetRow(label, 2);
            Grid.SetColumn(label, 0);
            label.FontSize = 30;
            label.HorizontalTextAlignment = TextAlignment.Center;
            MainGrid.Children.Add(label);

            Button button = new Button();
            button.Text = "Send Sms";
            Grid.SetRow(button, 3);
            Grid.SetColumn(button, 0);
            button.Clicked += SendSMS_OnClicked;
            MainGrid.Children.Add(button);


            Button buttoncall = new Button();
            buttoncall.Text = "Send Sms";
            Grid.SetRow(buttoncall, 4);
            Grid.SetColumn(buttoncall, 0);
            buttoncall.Clicked += PhoneCall_OnClicked;
            MainGrid.Children.Add(buttoncall);
            */
        }

        private void SendSMSprocedural(object sender, EventArgs e)
        { 
        
        }

        private void SendCallprocedural(string number)
        {

        }

        private void SendSMS_OnClicked (object sender, EventArgs e)
        {
            var smsMessanger = CrossMessaging.Current.SmsMessenger;

            if (smsMessanger.CanSendSms)
            {
                smsMessanger.SendSms("+91 7200606860", "Welcome to Xamarin.Forms");
            }
        }

        private void PhoneCall_OnClicked(object sender, EventArgs e)
        {
            var phoneCallTask = CrossMessaging.Current.PhoneDialer;
            if (phoneCallTask.CanMakePhoneCall)
            {
                phoneCallTask.MakePhoneCall("+91 7200606860","my name");
            }
        }
    }
}