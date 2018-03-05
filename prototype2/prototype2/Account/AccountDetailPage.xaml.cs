using System;
using System.Collections.Generic;

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

            if (App.User.Default.HasCreditCard)
            {
                creditCard.Text = "valid";
            } else {
                creditCard.Text = "invalid";
            }

            if (App.User.Default.HasPaypal){
                paypal.Text = "valid";
            } else {
                paypal.Text = "invalid";
            }
        }

        //Note* Write validation functions e.g. passwords should only contain '*' characters.
    }
}
