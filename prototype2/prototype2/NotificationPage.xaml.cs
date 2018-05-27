using System;
using System.Collections.ObjectModel;

using Xamarin.Forms;

namespace prototype2
{
    public partial class NotificationPage : ContentPage
    {
        public class Notification
        {
            public String NotificationTitle { get; set; }
            public DateTime DatePosted { get; set; }
            public String NotificationContent { get; set; }
            public string type { get; set; }
        }

        public ObservableCollection<Notification> PublicNotifications = new ObservableCollection<Notification>();
        public ObservableCollection<Notification> PrivateNotifications = new ObservableCollection<Notification>();
        public ObservableCollection<Notification> DisplayedNotifications = new ObservableCollection<Notification>();

        public NotificationPage()
        {
            InitializeComponent();
            this.Title = "Notifications";

            //Initialize view states
            GenerateSource();
            DisplayedNotifications = PublicNotifications;
            notificationList.ItemsSource = DisplayedNotifications;
            privateButton.BackgroundColor = Color.FromHex("#E0E0E0");
            publicButton.BackgroundColor = Color.FromHex("#B3B3B3");
        }


        //Creates the source of the list view
        private void GenerateSource()
        {

            for (int i = 0; i < 5; i++)
            {
                PrivateNotifications.Add(new Notification
                {
                    NotificationTitle = "Private Notification " + i.ToString(),
                    DatePosted = new DateTime(2017, 1, 1),
                    NotificationContent = "Small Beginning of Message. Small Beginning of Message. Small Beginning of Message.",
                    type = "Private"

                });
            }

            for (int i = 5; i < 10; i++)
            {
                PublicNotifications.Add(new Notification
                {
                    NotificationTitle = "Public Notification " + i.ToString(),
                    DatePosted = new DateTime(2017, 1, 1),
                    NotificationContent = "Small Beginning of Message. Small Beginning of Message. Small Beginning of Message.",
                    type = "Public"
                        
                });
            }
        }


        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {

            Notification selectedItem = (Notification)args.SelectedItem;

            IndividualNotificationPage individualNotificationPage =
                new IndividualNotificationPage(selectedItem.NotificationTitle,
                                               selectedItem.DatePosted, selectedItem.NotificationContent.ToString());
            Navigation.PushAsync(individualNotificationPage);

        }

        void Switch(object sender, EventArgs e)
        {
            Button label = (Button)sender;
            if (label.Text == "Private")
            {
                DisplayedNotifications = PrivateNotifications;
                privateButton.BackgroundColor = Color.FromHex("#B3B3B3");
                publicButton.BackgroundColor = Color.FromHex("#E0E0E0");

            }
            else
            {
                DisplayedNotifications = PublicNotifications;
                privateButton.BackgroundColor = Color.FromHex("#E0E0E0");
                publicButton.BackgroundColor = Color.FromHex("#B3B3B3");
            }
            notificationList.ItemsSource = DisplayedNotifications;
        }
        async void Search(object sender, EventArgs e)
        {
            if(searchQuery.Text != null){
                await DisplayAlert("hello", searchQuery.Text , "cancel");
            }
        }

        async void Sort(object sender, EventArgs e)
        {
            if (searchQuery.Text != null)
            {
                await DisplayAlert("hello", searchQuery.Text, "cancel");
            }
        }


    }
}
