using System;
using System.Collections.Generic;
using prototype2.Classes;

using Xamarin.Forms;

namespace prototype2
{
    public partial class LoginPage : ContentPage
    {
        public bool connectedToServer = false;

        public LoginPage()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);
            connect();

        }

        /// <summary>
        /// This is the function that sets the connection to the server.
        /// If the server uses the specifed REST API the app should run fine.
        /// </summary>
        async private void connect()
        {
            Connection.SetDestination("http://10.0.2.2:3000/");

            connectedToServer = await Connection.IsConnected();

        }

        /*
         * Function that handles the login button on xaml.
         * Setups user object.
         */
        async void Handle_ClickedAsync(object sender, System.EventArgs e)
        {
            if(connectedToServer){
                if(ValidInput()){
                    int? userId = null;
                    User user = null;

                    try
                    {
                        userId = await Connection.Login(username.Text, password.Text);
                        user = await Connection.GetUser((int)userId);
                    }
                    catch (Exception exc)
                    {
                        await DisplayAlert(exc.Message, "Cause: " + exc.InnerException.Message, "ok");
                    }

                    if (userId != null && user != null)
                    {
                        App.User.Default.ID = (int)userId;
                        App.User.Default.Username = username.Text;
                        App.User.Default.Password = displayedPassword(password.Text.Length);
                        App.User.Default.ProfilePhoto = user.profilePicture;
                        App.User.Default.FirstName = user.firstName;
                        App.User.Default.LastName = user.lastName;
                        App.User.Default.Address = user.address;
                        App.User.Default.Payments = user.payments;
                        //Some pages are just static for demonstration purposes.
                        loadSomeStaticPages();
                    }
                }else{
                    await DisplayAlert("Username or Password can't be blank", "Please fill in password or username", "OK");
                }

            }else{
                //For demonstration purposes only
                await DisplayAlert("Not connected to server", "Using static pages", "OK");
                staticLogin();
            }
        }

        bool ValidInput(){
            if(username.Text == null || password.Text == null ){
                return false;
            }
            else if (username.Text.Trim() == "" || password.Text.Trim() == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        async void loadSomeStaticPages(){
            // Creates data for use throughout the app (quotes, product info)
            Data.InitializeData();

            // open library page
            NavigationPage MainPage = new NavigationPage(new AccountPage());

            NavigationPage.SetHasNavigationBar(MainPage, false);
            Navigation.InsertPageBefore(MainPage, this); //changes root page to products page
            await Navigation.PopAsync(); // pops the login page 
        }

        async void staticLogin(){
            string validUsername = "test";// fake stored username
            string validPassword = "test";// fake stored password

            if ((username.Text == validUsername) && (password.Text == validPassword))
            {
                //Set user account details
                App.User.Default.ID = 1;
                App.User.Default.ProfilePhoto = "profilephoto.jpg";
                App.User.Default.Username = username.Text;
                App.User.Default.Password = displayedPassword(password.Text.Length);
                App.User.Default.FirstName = "Static";
                App.User.Default.LastName = "McStaticcy";
                App.User.Default.Address = "20 Gotham Lane";

                App.User.Default.Payments = new string[]{"Credit Card", "Paypal"};

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
