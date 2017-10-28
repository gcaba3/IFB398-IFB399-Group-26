using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ProductsMyOrders
{
    public partial class AccountPage : MasterDetailPage
    {
        public AccountPage()
        {
            InitializeComponent();
            Title = "Account Page";
            NavigationPage.SetHasNavigationBar(this, false);

            Detail = new NavigationPage(new MainPage());
            IsPresented = false;


        }

        public AccountPage(NavigationBar.Tab tab)
        {
            InitializeComponent();
            Title = "Account Page";
            NavigationPage.SetHasNavigationBar(this, false);

            switch (tab)
            {
                case NavigationBar.Tab.Products:
                    Detail = new NavigationPage(new Products());
                    IsPresented = false;
                    break;

                case NavigationBar.Tab.MyOrders:
                    Detail = new NavigationPage(new MyOrders());
                    IsPresented = false;
                    break;
            }

            
        }
    }
}