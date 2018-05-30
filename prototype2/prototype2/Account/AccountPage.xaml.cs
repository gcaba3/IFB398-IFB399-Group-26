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
            //Title = "Account Page";
            //Icon = "menu.png";
            NavigationPage.SetHasNavigationBar(this, false);

            Detail = new NavigationPage(new ProductsPage());

            helloUser.Text = "Hello " + App.User.Default.Username + "!";


        }

        public AccountPage(NavigationBar.Tab tab)
        {
            InitializeComponent();
            //Title = "Account Page";
            NavigationPage.SetHasNavigationBar(this, false);

            switch (tab)
            {
                case NavigationBar.Tab.Products:
                    Detail = new NavigationPage(new ProductsPage());
                    IsPresented = false;
                    break;

                case NavigationBar.Tab.MyShop:
                    Detail = new NavigationPage(new MyShopPage());
                    IsPresented = false;
                    break;

                case NavigationBar.Tab.MyOrders:
                    Detail = new NavigationPage(new MyOrdersPage());
                    IsPresented = false;
                    break;

                case NavigationBar.Tab.Support:
                    Detail = new NavigationPage(new SupportPage());
                    IsPresented = false;
                    break;

                case NavigationBar.Tab.Events:
                    Detail = new NavigationPage(new Events());
                    IsPresented = false;
                    break;
            }

            helloUser.Text = "Hello " + App.User.Default.Username + "!";
        }

        public AccountPage(List<int> searchSuggestions)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);


            Detail = new NavigationPage(new ProductsPage(searchSuggestions));
            IsPresented = false;
        }

        public AccountPage(Product searchedProduct)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);


            Detail = new NavigationPage(new ProductsPage(searchedProduct));
            IsPresented = false;
        }

        public AccountPage(string searchedCategory)
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);


            Detail = new NavigationPage(new ProductsPage(searchedCategory));
            IsPresented = false;
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
