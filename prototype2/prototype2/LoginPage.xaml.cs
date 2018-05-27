using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace prototype2
{
    public partial class LoginPage : ContentPage
    {
        public bool validUser = false;
        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

        }

        /*
         * Function that handles the login button on xaml.
         * Setups user object.
         */
        async void Handle_ClickedAsync(object sender, System.EventArgs e)
        {
            if (ValidLogin())
            {

                //Query database statement for account details;

                //Note* Change to access returned account details. Not hard coded.
                //Set user account details
                App.User.Default.ID = 1;
                App.User.Default.ProfilePhoto = "profilephoto.jpg";
                App.User.Default.Username = username.Text;
                App.User.Default.Password = displayedPassword(password.Text.Length);
                App.User.Default.FirstName = "Bruce";
                App.User.Default.LastName = "Wayne";
                App.User.Default.Address = "20 Gotham Lane";

                //check if credit card is on database;
                App.User.Default.HasCreditCard = true;

                //check if paypal is on database;
                App.User.Default.HasPaypal = false;

                // Creates data for use throughout the app (quotes, product info)
                Data.InitializeData();

                // open library page
                NavigationPage MainPage = new NavigationPage(new AccountPage());

                NavigationPage.SetHasNavigationBar(MainPage, false);
                Navigation.InsertPageBefore(MainPage, this); //changes root page to products page
                await Navigation.PopAsync(); // pops the login page 

            }
            else
            {
                await DisplayAlert("Incorrect login", "Either the username or password entered is incorrect. " +
                                   "Please try again.", "OK");
            }


        }

        /*
         * This function should query the database to check the entered credentials,
         * exists in the database. 
         * 
         */
        private bool ValidLogin()
        {
            //Query database statement

            string validUsername = "test";// fake stored username
            string validPassword = "test";// fake stored password

            if ((username.Text == validUsername) && (password.Text == validPassword))
            {
                return true;
            }
            else
            {
                return false;
            }

        }

        /*
         * Returns the asterisks representation of the users password.
         */
        private string displayedPassword(int passwordLength)
        {
            string astPassword = "";
            for (int i = 0; i < passwordLength; i++)
            {
                astPassword += "*";
            }
            return astPassword;
        }

        void OpenLink(object sender, System.EventArgs e){
            Device.OpenUri(new Uri("http://supplypartners.com.au/"));
        }


    }
}
