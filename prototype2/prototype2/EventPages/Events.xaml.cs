using System;
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
        public Events()
        {
            InitializeComponent();
            Title = "Events";
            NavigationPage.SetHasBackButton(this, false);
            NavigationBar.ChangeEventsTabColor();

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

            eventGrid.Children.Add(new StackLayout
            {
                BackgroundColor = Color.BlanchedAlmond,
                HorizontalOptions = LayoutOptions.CenterAndExpand
            },0,0);

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

            }, 2, 0);

            eventGrid.Children.Add(new Label
            {
                ClassId = "eventState",
                BackgroundColor = Color.BlanchedAlmond,
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                
            }, 3, 0);

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

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            NotificationPage notificationPage = new NotificationPage();
            await Navigation.PushAsync(notificationPage);
        }

    }
}