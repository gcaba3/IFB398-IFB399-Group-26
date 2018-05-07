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

            ticketlist = new List<Ticket>();

            //generate test data
            for (int i = 0; i < 10; i++)
            {
                //Ticket newTicket = new Ticket();
                //newTicket.Title = "Title" + i.ToString();
                //newTicket.Date = i.ToString();
                //newTicket.Messages = "Message1-!-Message2-!-Message3" + i.ToString();
                //newTicket.Number = i;
                //newTicket.State = "In Process";
                //ticketlist.Add(newTicket);
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

            //display list on screen
            for (int i = 0; i < ticketlist.Count; i++)
            {
                addTicketToList(ticketlist[i].Title,
                               (ticketlist[i].Date),
                               (ticketlist[i].Messages),
                               (ticketlist[i].Number.ToString("#00000")),
                               (ticketlist[i].State)
                               );
            }
        }

        public void addTicketToList(string name, string date, string description, string price, string state)
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
                    new ColumnDefinition { Width = new GridLength(10, GridUnitType.Star)},
                    new ColumnDefinition { Width = new GridLength(20, GridUnitType.Star)}
                },
            };

            eventGrid.Children.Add(new Label
            {
                ClassId = "name",
                Text = name,
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
                Text = description,
                FontSize = 10,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,

            }, 0, 3, 2, 3);

            eventGrid.Children.Add(new Label
            {
                Text = "$" + price,
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
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            NotificationPage notificationPage = new NotificationPage();
            await Navigation.PushAsync(notificationPage);
        }
    }
}