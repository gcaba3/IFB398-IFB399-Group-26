using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace prototype2
{
    public partial class MainPage : TabbedPage
    {
        public MainPage()
        {
            InitializeComponent();
            Title = "Library";
            //NavigationPage.SetHasBackButton(this, false);

            NavigationPage Products = new NavigationPage(new MyPage());
            Products.Title = "Products";

            NavigationPage Orders = new NavigationPage(new MyPage());
            Orders.Title = "Orders";

            NavigationPage Support = new NavigationPage(new MyPage());
            Support.Title = "Support";

            NavigationPage Events = new NavigationPage(new MyPage());
            Events.Title = "Events";


            this.Children.Add(Products);
            this.Children.Add(Orders);
            this.Children.Add(Support);
            this.Children.Add(Events);

           
        }

        protected override void OnCurrentPageChanged()
        {
            base.OnCurrentPageChanged();

            Title = CurrentPage.Title;

        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            NotificationPage notificationPage = new NotificationPage();
            await Navigation.PushAsync(notificationPage);
        }
    }
}
