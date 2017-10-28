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

            customerName.Text = App.User.Default.Username;
        }
    }
}
