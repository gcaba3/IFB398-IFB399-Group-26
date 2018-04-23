using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace prototype2
{
    public partial class NotificationPage : ContentPage
    {
        public class Notification
        {
            public int NotificationNumber { get; set; }
            public DateTime DatePosted { get; set; }
            public String NotificationContent { get; set; }
            public string type { get; set; }
        }

        public System.Collections.ObjectModel.ObservableCollection<Notification> Notifications =
            new System.Collections.ObjectModel.ObservableCollection<Notification>();
        public System.Collections.ObjectModel.ObservableCollection<Notification> PrivateNotifications =
            new System.Collections.ObjectModel.ObservableCollection<Notification>();

        public NotificationPage()
        {
            InitializeComponent();
            this.Title = "Notifications";

            GenerateSource();
            privateButton.BackgroundColor = Color.FromHex("#E0E0E0");
            publicButton.BackgroundColor = Color.FromHex("#B3B3B3");
        }



        //Creates the source of the list view
        private void GenerateSource()
        {

            for (int i = 0; i < 5; i++)
            {
                Notifications.Add(new Notification
                {
                    NotificationNumber = i,
                    DatePosted = new DateTime(2017, 1, 1),
                    NotificationContent = "Small Beginning of Message. Small Beginning of Message. Small Beginning of Message.",
                    type = "Private"

                });
            }

            for (int i = 5; i < 10; i++)
            {
                Notifications.Add(new Notification
                {
                    NotificationNumber = i,
                    DatePosted = new DateTime(2017, 1, 1),
                    NotificationContent = "Small Beginning of Message. Small Beginning of Message. Small Beginning of Message.",
                    type = "Public"

                });
            }

            notificationList.ItemsSource = Notifications;
            SortPrivateNotifications();
        }

        void SortPrivateNotifications()
        {
            foreach (Notification ntf in Notifications)
            {
                if (ntf.type == "Private")
                {
                    PrivateNotifications.Add(ntf);
                }
            }
        }

        void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {

            Notification selectedItem = (Notification)args.SelectedItem;

            IndividualNotificationPage individualNotificationPage =
                new IndividualNotificationPage(selectedItem.NotificationNumber.ToString(),
                                               selectedItem.DatePosted, selectedItem.NotificationContent.ToString());
            Navigation.PushAsync(individualNotificationPage);

        }

        void Handle_Clicked(object sender, EventArgs e)
        {
            Button label = (Button)sender;
            if (label.Text == "Private")
            {
                notificationList.ItemsSource = PrivateNotifications;
                privateButton.BackgroundColor = Color.FromHex("#B3B3B3");
                publicButton.BackgroundColor = Color.FromHex("#E0E0E0");

            }
            else
            {
                notificationList.ItemsSource = Notifications;
                privateButton.BackgroundColor = Color.FromHex("#E0E0E0");
                publicButton.BackgroundColor = Color.FromHex("#B3B3B3");
            }
        }
    }
}
