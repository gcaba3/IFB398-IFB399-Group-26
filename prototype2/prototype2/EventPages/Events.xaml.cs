﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using prototype2.Classes;

namespace prototype2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Events : ContentPage
    {
        private List<Event> eventlist;
        SearchGrid gridSearchGrid;
        public Events()
        {
            InitializeComponent();
            Title = "Events";
            NavigationPage.SetHasBackButton(this, false);
            NavigationBar.ChangeEventsTabColor();

            gridSearchGrid = new SearchGrid();
            gridPageContent.Children.Add(gridSearchGrid, 0, 1, 0, 3);

            eventlist = new List<Event>();

            //generate test data
            for (int i = 0; i < 10; i++)
            {
                Event singleEvent = new Event();
                singleEvent.Name = "name" + i.ToString();
                singleEvent.Date = "date" + i.ToString();
                singleEvent.Description = "This is description #" + i.ToString();
                singleEvent.Address = "1 random Street, Bowen Hills, Brisbane, QLD, Australia";
                singleEvent.Price = i;
                singleEvent.State = EventStatus.Upcoming;
                singleEvent.PurchaseURL = "http://supplypartners.com.au/events/#Price";
                eventlist.Add(singleEvent);
            }
            updateEvents();
        }

        public void updateEvents()
        {
            //remove old list
            for (int i = 0; i < stackLayoutMain.Children.Count; i++)
            {
                stackLayoutMain.Children.RemoveAt(0);
            }
                
            //display list on screen
            for (int i = 0; i < eventlist.Count; i++)
            {
                addEventToList(eventlist[i].Name,
                               (eventlist[i].Date),
                               (eventlist[i].Description),
                               (eventlist[i].Price.ToString("00.00")),
                               (eventlist[i].State)
                               );
            }
            
        }

        public void addEventToList(string name, string date, string description, string price, string state)
        {

            Grid eventGrid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition {},
                    new RowDefinition {}
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(25, GridUnitType.Star)},
                    new ColumnDefinition { Width = new GridLength(50, GridUnitType.Star)},
                    new ColumnDefinition { Width = new GridLength(12.5, GridUnitType.Star)},
                    new ColumnDefinition { Width = new GridLength(12.5, GridUnitType.Star)}
                },
            };


            eventGrid.Children.Add(new Image
            {
                Source = ImageSource.FromUri(new Uri("http://supplypartners.com.au/files/uploads/2015/10/2018_MASTERCLASS_LOGO-150x150@2x.png")),
                BackgroundColor = Color.BlanchedAlmond,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            },0,1,0,2);

            eventGrid.Children.Add(new Label
            {
                ClassId = "name",
                Text = name,
                FontSize = 18,
                VerticalOptions = LayoutOptions.Center,

            }, 1, 0);

            eventGrid.Children.Add(new Label
            {
                Text = date,
                FontSize = 12,
                VerticalOptions = LayoutOptions.Center,
                VerticalTextAlignment = TextAlignment.Start,
            }, 1, 1);

            

            eventGrid.Children.Add(
                
               
                    new Label
            {
                Text = "$" + price,
                FontSize = 10,
                VerticalOptions = LayoutOptions.Center,

            }, 2,3, 0,2);


            Button newbutton = new Button
            {
                Text = "Buy Tickets",
                FontSize = 10,
                ClassId = "eventState",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
            };
            newbutton.Clicked += PaymentButton;

            eventGrid.Children.Add(newbutton, 3,4, 0,2);



            var tapGestureRecognizer = new TapGestureRecognizer();
            tapGestureRecognizer.Tapped += OpenEvent;

            eventGrid.GestureRecognizers.Add(tapGestureRecognizer);

            //add borders
            Frame frame = new Frame();
            frame.Content = eventGrid;
            frame.OutlineColor = Color.Silver;

            stackLayoutMain.Children.Add(frame);
        }


        public void OpenEvent(object sender, EventArgs args)
        {
            //gets the index of sender in parent, thats also its position in eventlist
            int index = stackLayoutMain.Children.IndexOf((Frame)((Grid)sender).Parent);
            Navigation.PushAsync(new EventSingle(eventlist[index]));
        }

        private void PaymentButton(object sender, System.EventArgs e)
        {
            Device.OpenUri(new Uri("http://supplypartners.com.au/events/#Price"));
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            NotificationPage notificationPage = new NotificationPage();
            await Navigation.PushAsync(notificationPage);
        }

        private void Clicked_BtnSearchTool(object sender, EventArgs eventArgs)
        {
            gridSearchGrid.Clicked_ButtonSearchTool();
        }
    }
}