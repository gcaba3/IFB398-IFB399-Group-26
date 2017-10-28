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

        async void Handle_ClickedAsync(object sender, System.EventArgs e)
        {
            if (ValidLogin()){

                App.User.Default.Username = username.Text;// set username

                // open library page
                NavigationPage MainPage = new NavigationPage(new AccountPage());

                NavigationPage.SetHasNavigationBar(MainPage, false);
                Navigation.InsertPageBefore(MainPage,this); //changes root page to products page
                await Navigation.PopAsync(); // pops the login page 

            } else {
                await DisplayAlert("Incorrect login", "Either the username or password entered is incorrect. " +
                                   "Please try again.", "OK");
            }


        }

        bool ValidLogin(){
            string validUsername = "test";// fake stored username
            string validPassword = "test";// fake stored password

            if((username.Text == validUsername)&&(password.Text == validPassword)){
                return true;
            } else {
                return false;
            }

        }


    }
}
