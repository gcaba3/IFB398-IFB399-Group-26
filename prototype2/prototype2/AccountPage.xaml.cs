using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace prototype2
{
    public partial class AccountPage : MasterDetailPage
    {
        public AccountPage()
        {
            InitializeComponent();
            Title = "Account Page";
            NavigationPage.SetHasNavigationBar(this, false);

            Detail = new NavigationPage(new Products());

            helloUser.Text = "Hello " + App.User.Default.Username + "!";


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

        async void OpenPage(object sender, System.EventArgs e)
        {
            Button PageSubpages = (Button)sender;


            switch (PageSubpages.Text)
            {
                case "Live Statement":
                    LiveStatementPage liveStatementPage = new LiveStatementPage();
                    await Navigation.PushAsync(liveStatementPage);
                    break;
                case "Account Details":
                    AccountDetailPage accountDetailPage = new AccountDetailPage();
                    await Navigation.PushAsync(accountDetailPage);
                    break;
                case "Payment History":
                    PaymentHistoryPage paymentHistoryPage = new PaymentHistoryPage();
                    await Navigation.PushAsync(paymentHistoryPage);
                    break;
                case "Contact Us":
                    ContactUsPage contactUsPage = new ContactUsPage();
                    await Navigation.PushAsync(contactUsPage);
                    break;


                
            }



        }

        async void Logout(object sender, System.EventArgs e)
        {
            NavigationPage loginPage = new NavigationPage(new LoginPage());
            NavigationPage.SetHasNavigationBar(loginPage, false);
            Navigation.InsertPageBefore(loginPage, this); // changes the root page to the login page
            await Navigation.PopAsync(); // pops the current page 

        }
    }
}
