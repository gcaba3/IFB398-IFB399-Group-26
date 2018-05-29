using System;
using System.Collections.Generic;

using prototype2.Classes;
using Xamarin.Forms;

namespace prototype2
{
    public partial class AccountDetailPage : ContentPage
    {
        public AccountDetailPage()
        {
            InitializeComponent();
            this.Title = "Account Details";


            //Display profile photo
            customerPhoto.Source = App.User.Default.ProfilePhoto;

            //Display account details
            customerName.Text = App.User.Default.FirstName + ' ' + App.User.Default.LastName;
            customerID.Text = App.User.Default.ID.ToString();
            customerLogin.Text = App.User.Default.Username;
            customerPassword.Text = App.User.Default.Password;
            customerAddress.Text = App.User.Default.Address;

            foreach(String payment in App.User.Default.Payments){
                var label = new Label { Text = payment, TextColor=Color.Black };
                paymentOptions.Children.Add(label);
            }
        }


    }
}
