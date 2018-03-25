using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace prototype2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Events : ContentPage
    {
        public Events()
        {
            InitializeComponent();
            Title = "Events";
            NavigationPage.SetHasBackButton(this, false);
            NavigationBar.ChangeEventsTabColor();

            for (int i = 0; i < 10; i++)
            {

                addEventToList(("name" + i.ToString()),
                               ("date" + i.ToString()),
                               ("This is description #" + i.ToString()),
                               (i.ToString() + ".00"),
                               ("state" + i.ToString())
                               );
            }

        }

        public void addEventToList(string name, string date, string description, string price, string state)
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
            Grid tappedEvent = (Grid)sender;
            foreach (Label ree in tappedEvent.Children)
            {
                if (ree.ClassId.Equals("name"))
                {
                    App.Current.MainPage.DisplayAlert("Tapped on event named " + ree.Text + "!", "Not implemented.", "OK");
                    break;
                }
            }


        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            NotificationPage notificationPage = new NotificationPage();
            await Navigation.PushAsync(notificationPage);
        }

    }
}