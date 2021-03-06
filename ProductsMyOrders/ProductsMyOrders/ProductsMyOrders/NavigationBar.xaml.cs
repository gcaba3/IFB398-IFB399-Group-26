﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProductsMyOrders
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class NavigationBar : Grid
    {
        public enum Tab { Products, MyProducts, MyOrders, Support, Events };
        public NavigationBar()
        {
            InitializeComponent();
        }

        async void Clicked_Btn(object sender, EventArgs args)
        {
            await App.Current.MainPage.DisplayAlert("Clicked!", "Not implemented.", "OK");
        }

        void Clicked_Products(object sender, EventArgs args)
        {
            Navigation.PushAsync(new AccountPage(Tab.Products));
        }

        void Clicked_MyOrders(object sender, EventArgs args)
        {
            Navigation.PushAsync(new AccountPage(Tab.MyOrders));
        }
    }
}