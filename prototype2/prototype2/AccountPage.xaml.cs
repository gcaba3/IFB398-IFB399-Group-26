using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace prototype2
{
    public partial class AccountPage : MasterDetailPage
    {
        public AccountPage()
        {
            InitializeComponent();
            Title = "Account Page";
            NavigationPage.SetHasNavigationBar(this, false);

            Detail = new NavigationPage(new MainPage());
            IsPresented = true;

           
        }

    }
}
