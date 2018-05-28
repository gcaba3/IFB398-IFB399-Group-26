using System;
using System.Collections.ObjectModel;
using prototype2.Classes;

using Xamarin.Forms;

namespace prototype2
{
    public partial class NotificationPage : ContentPage
    {

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
            ApplySort("Latest");
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
                    NotificationTitle = "Cool Private Notification " + i.ToString(),
                    DatePosted = new DateTime(2017, 1, 1+i),
                    NotificationContent = "Small Beginning of Message. Small Beginning of Message. Small Beginning of Message.",
                    type = "Private"

                });
            }

            for (int i = 5; i < 10; i++)
            {
                PublicNotifications.Add(new Notification
                {
                    NotificationTitle = " Awesome Public Notification " + i.ToString(),
                    DatePosted = new DateTime(2017, 1, 1+i),
                    NotificationContent = "Small Beginning of Message. Small Beginning of Message. Small Beginning of Message.",
                    type = "Public"
                        
                });
            }

            PublicNotifications.Add(new Notification
            {
                NotificationTitle = "Unique - n123 ",
                DatePosted = new DateTime(2018, 5, 1),
                NotificationContent = "The closest candidate is the Nepali word ponya, possibly referring to the adapted wrist bone of the red panda, which is native to Nepal. The Western world originally applied this name to the red panda. Until 1901, when it was erroneously stated to be related to the red panda, the giant panda was known as (Ailuropus melanoleucus)",
                type = "Public"

            });

            PublicNotifications.Add(new Notification
            {
                NotificationTitle = "Unique - n567 ",
                DatePosted = new DateTime(2018, 3, 25),
                NotificationContent = "The closest candidate is the Nepali word ponya, possibly referring to the adapted wrist bone of the red panda, which is native to Nepal. The Western world originally applied this name to the red panda. Until 1901, when it was erroneously stated to be related to the red panda, the giant panda was known as (Ailuropus melanoleucus)",
                type = "Public"

            });

            PublicNotifications.Add(new Notification
            {
                NotificationTitle = "n123 - Unique ",
                DatePosted = new DateTime(2018, 1, 26),
                NotificationContent = "The closest candidate is the Nepali word ponya, possibly referring to the adapted wrist bone of the red panda, which is native to Nepal. The Western world originally applied this name to the red panda. Until 1901, when it was erroneously stated to be related to the red panda, the giant panda was known as (Ailuropus melanoleucus)",
                type = "Public"

            });

            PrivateNotifications.Add(new Notification
            {
                NotificationTitle = "Private Unique - n321 ",
                DatePosted = new DateTime(2018, 4, 12),
                NotificationContent = "The closest candidate is the Nepali word ponya, possibly referring to the adapted wrist bone of the red panda, which is native to Nepal. The Western world originally applied this name to the red panda. Until 1901, when it was erroneously stated to be related to the red panda, the giant panda was known as (Ailuropus melanoleucus)",
                type = "Private"

            });
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
            ApplySort("Latest");
            notificationList.ItemsSource = DisplayedNotifications;
        }
        void Search(object sender, EventArgs e)
        {
            if(searchQuery.Text == null){
                notificationList.ItemsSource = DisplayedNotifications;
            }else{
                var searchedString = searchQuery.Text.ToUpper();
                notificationList.ItemsSource = SearchedNotifications(searchedString, DisplayedNotifications);
            }
        }

        ObservableCollection<Notification> SearchedNotifications(String searchString, ObservableCollection<Notification> collection){
            var searchedString = searchString.ToUpper();
            ObservableCollection<Notification> returnCollection = new ObservableCollection<Notification>(); 
            foreach (Notification notification in collection)
            {
                var notificationString = notification.NotificationTitle.ToUpper();
                if (notificationString.Contains(searchedString))
                {
                    returnCollection.Add(notification);
                }
            }

            return returnCollection;
        }

        async void Sort(object sender, EventArgs e)
        {
            var action = await DisplayActionSheet("Sort By:", "Cancel", null, "Latest", "Oldest");
            if (action != "Cancel")
            {
                ApplySort(action);
            }

        }

        void ApplySort(String action)
        {
            for (int i = 0; i <= DisplayedNotifications.Count - 1; i++)
            {
                for (int j = 0; j <= DisplayedNotifications.Count - 1; j++)
                {

                    if (action == "Latest")
                    {
                        if (DisplayedNotifications[i].DatePosted >= DisplayedNotifications[j].DatePosted)
                        {
                            DisplayedNotifications.Move(i, j);
                        }
                    }
                    else if (action == "Oldest")
                    {
                        if (DisplayedNotifications[i].DatePosted <= DisplayedNotifications[j].DatePosted)
                        {
                            DisplayedNotifications.Move(i, j);
                        }
                    }
                }
            }
        }


    }
}
