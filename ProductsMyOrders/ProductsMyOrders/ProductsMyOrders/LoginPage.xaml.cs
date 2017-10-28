using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace ProductsMyOrders
{
    public partial class LoginPage : ContentPage
    {
        public bool validUser = false;
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

        }

        async void Handle_ClickedAsync(object sender, System.EventArgs e)
        {
            if (ValidLogin())
            {
                // Open open library page
                NavigationPage MainPage = new NavigationPage(new AccountPage());

                NavigationPage.SetHasNavigationBar(MainPage, false);
                Navigation.InsertPageBefore(MainPage, this);
                await Navigation.PopAsync(); // pops the login page and changes root page to products page
            }
            else
            {
                await DisplayAlert("Incorrect login", "Either the username or password entered is incorrect. " +
                                   "Please try again.", "OK");
            }


        }

        bool ValidLogin()
        {
            string validUsername = " ";// fake stored username
            string validPassword = " ";// fake stored password

            if ((username.Text == validUsername) && (password.Text == validPassword))
            {
                return true;
            }
            else
            {
                return false;
            }

        }


    }
}
