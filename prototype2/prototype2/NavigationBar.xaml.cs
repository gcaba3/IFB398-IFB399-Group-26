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
    public partial class NavigationBar : Grid
    {
        public enum Tab { Products, MyShop, MyOrders, Support, Events };
        private static Button productsButton, myShopButton, myOrdersButton, supportButton, eventsButton;

        public NavigationBar()
        {
            InitializeComponent();
            AssignButtons();
        }

        private void AssignButtons()
        {
            productsButton = btnProducts;
            myShopButton = btnMyShop;
            myOrdersButton = btnMyOrders;
            supportButton = btnSupport;
            eventsButton = btnEvents;
        }

        public static void ChangeProductsTabColor()
        {
            productsButton.BackgroundColor = (Color)App.Current.Resources["SPRed"];
        }

        public static void ChangeMyShopTabColor()
        {
            myShopButton.BackgroundColor = (Color)App.Current.Resources["SPRed"];
        }

        public static void ChangeMyOrdersTabColor()
        {
            myOrdersButton.BackgroundColor = (Color)App.Current.Resources["SPRed"];
        }

        public static void ChangeSupportTabColor()
        {
            supportButton.BackgroundColor = (Color)App.Current.Resources["SPRed"];
        }

        public static void ChangeEventsTabColor()
        {
            eventsButton.BackgroundColor = (Color)App.Current.Resources["SPRed"];
        }

        async void Clicked_Btn(object sender, EventArgs args)
        {
            await App.Current.MainPage.DisplayAlert("Clicked!", "Not implemented.", "OK");
        }

        void Clicked_Products(object sender, EventArgs args)
        {
            Navigation.PushAsync(new AccountPage(Tab.Products));
        }

        void Clicked_MyShop(object sender, EventArgs args)
        {
            Navigation.PushAsync(new AccountPage(Tab.MyShop));
        }

        void Clicked_MyOrders(object sender, EventArgs args)
        {
            Navigation.PushAsync(new AccountPage(Tab.MyOrders));
        }

        void Clicked_Events(object sender, EventArgs args)
        {
            Navigation.PushAsync(new AccountPage(Tab.Events));
        }        
    }
}