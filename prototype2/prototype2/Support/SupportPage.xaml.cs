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
    public partial class SupportPage : ContentPage
    {
        public List<Ticket> ticketlist;
        public SupportPage()
        {
            InitializeComponent();
            Title = "Support";
            NavigationPage.SetHasBackButton(this, false);
            NavigationBar.ChangeSupportTabColor();
            ticketlist = Data.tickets;
            //this.Appearing += OnResume;

            //generate test data
            if (ticketlist.Count() < 1) 
            {
                for (int i = 0; i < 3; i++)
                {
                    Ticket newticket = new Ticket();
                    newticket.Title = "title" + i.ToString();
                    newticket.Date = DateTime.Now.ToString("d");
                    newticket.Messages = new List<string>();
                    newticket.Messages.Add("message" + i.ToString());
                    newticket.Number = i;
                    newticket.State = "in process";
                    newticket.SentTo = "Technical Support";
                    ticketlist.Add(newticket);
                }
            }
            updateTickets();

            
        }


        public void updateTickets()
        {
            //remove old list
            for (int i = 0; i < stackLayoutMain.Children.Count; i++)
            {
                stackLayoutMain.Children.RemoveAt(0);
            }
            stackLayoutMain.Children.Clear();
            //display list on screen
            for (int i = 0; i < ticketlist.Count; i++)
            {
                addTicketToList(ticketlist[i].Title,
                               (ticketlist[i].Date),
                               (ticketlist[i].Messages[0]),
                               (ticketlist[i].Number.ToString("#00000")),
                               (ticketlist[i].State)
                               );
            }
        }

        public void addTicketToList(string title, string date, string message, string number, string state)
        {

            var eventGrid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition {},
                    new RowDefinition {},
                    new RowDefinition {}
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(70, GridUnitType.Star)},
                    new ColumnDefinition { Width = new GridLength(20, GridUnitType.Star)},
                    new ColumnDefinition { Width = new GridLength(30, GridUnitType.Star)}
                },
            };

            eventGrid.Children.Add(new Label
            {
                ClassId = "name",
                Text = title,
                FontSize = 14,
                VerticalOptions = LayoutOptions.Center,

            }, 0, 0);

            eventGrid.Children.Add(new Label
            {
                Text = date,
                FontSize = 8,
                VerticalOptions = LayoutOptions.Center,
                VerticalTextAlignment = TextAlignment.Start,
            }, 0, 1);

            eventGrid.Children.Add(new Label
            {
                Text = message,
                FontSize = 10,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,

            }, 0, 3, 2, 3);

            eventGrid.Children.Add(new Label
            {
                Text = number,
                FontSize = 10,
                VerticalOptions = LayoutOptions.Center,
            }, 1, 0);

            eventGrid.Children.Add(new Label
            {
                ClassId = "eventState",
                BackgroundColor = Color.SkyBlue,
                TextColor = Color.Black,
                FontSize = 14,
                Text = state,
                HorizontalTextAlignment = TextAlignment.Center
            }, 2, 0);

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
            Navigation.PushAsync(new TicketSingle(ticketlist[index]));
        }

        public void NewTicket(object sender, EventArgs args)
        {
            Navigation.PushAsync(new CreateTicket());
            updateTickets();
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            NotificationPage notificationPage = new NotificationPage();
            await Navigation.PushAsync(notificationPage);
        }
    }
}