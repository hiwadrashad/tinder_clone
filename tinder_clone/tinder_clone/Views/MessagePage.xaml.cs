using Plugin.Messaging;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using tinder_clone.Tables;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace tinder_clone.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    
    // message page to contact users
    public partial class MessagePage : ContentPage
    {

       string fileNameu = @"C:\Temp.txt";
       string filenamew = @"C:\Tempw.txt";

        public MessagePage()
        {
            //create databse connection trough combination of sqlite and LINQ
            InitializeComponent();
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            var db = new SQLiteConnection(dbpath);
            var myquery = db.Table<RegUserTable>().Where(u => u.Username.Equals(File.ReadAllText(fileNameu)) && u.Password.Equals(File.ReadAllText(filenamew))).FirstOrDefault();
            var telephonenumbers = myquery.telephonenumbers;
            var names = myquery.MatchNames;


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
                  MainGrid.Children.Add(namelabel);
                  MainGrid.Children.Add(numberlabel);

                Button smsbutton = new Button();
                Button callbutton = new Button();
                smsbutton.Text = "Send Sms";
                callbutton.Text = "Phone Call";
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
                callbutton.Clicked += ((s, e)=> SendCallprocedural(s,e,i));
                MainGrid.Children.Add(smsbutton);
                MainGrid.Children.Add(callbutton);


              
              }
              

        }

        //sendsms trough auto generated list of users including data
         void SendSMSprocedural(object sender, EventArgs e, int phoneindex)
        {
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            var db = new SQLiteConnection(dbpath);
            var myquery = db.Table<RegUserTable>().Where(u => u.Username.Equals(File.ReadAllText(fileNameu)) && u.Password.Equals(File.ReadAllText(filenamew))).FirstOrDefault();
            var telephonenumbers = myquery.telephonenumbers;

            var smsMessanger = CrossMessaging.Current.SmsMessenger;

            if (smsMessanger.CanSendSms)
            {
                smsMessanger.SendSms(telephonenumbers[phoneindex], "Welcome to Xamarin.Forms");
            }

        }

        //send telephone call trough auto generated list of users including data
        void SendCallprocedural(object sender, EventArgs e, int phoneindex)
        {
            var dbpath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "UserDatabase.db");
            var db = new SQLiteConnection(dbpath);
            var myquery = db.Table<RegUserTable>().Where(u => u.Username.Equals(File.ReadAllText(fileNameu)) && u.Password.Equals(File.ReadAllText(filenamew))).FirstOrDefault();
            var telephonenumbers = myquery.telephonenumbers;

            var phoneCallTask = CrossMessaging.Current.PhoneDialer;
            if (phoneCallTask.CanMakePhoneCall)
            {
                phoneCallTask.MakePhoneCall(telephonenumbers[phoneindex], "my name");
            }

        }

        //mockdata sms call
         void SendSMS_OnClicked (object sender, EventArgs e)
        {
            var smsMessanger = CrossMessaging.Current.SmsMessenger;

            if (smsMessanger.CanSendSms)
            {
                smsMessanger.SendSms("+91 7200606860", "Welcome to Xamarin.Forms");
            }
        }

        //mockdata phone call
         void PhoneCall_OnClicked(object sender, EventArgs e)
        {
            var phoneCallTask = CrossMessaging.Current.PhoneDialer;
            if (phoneCallTask.CanMakePhoneCall)
            {
                phoneCallTask.MakePhoneCall("+91 7200606860","my name");
            }
        }

        //back buttno
        void Go_Back(object sender, EventArgs e)
        {
            App.Current.MainPage = new HomePage();

        }
    }
}