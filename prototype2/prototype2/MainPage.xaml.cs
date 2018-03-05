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
