using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ProductsMyOrders
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyOrders : ContentPage
    {
        enum Tab { Quotes, Orders, Invoices };
        Tab selectedTab;

        StackLayout stackLayoutQuotes, stackLayoutOrders, stackLayoutInvoices;
        public MyOrders()
        {
            InitializeComponent();
            NavigationPage.SetHasNavigationBar(this, false);

            stackLayoutQuotes = new StackLayout { HorizontalOptions = LayoutOptions.FillAndExpand };
            stackLayoutOrders = new StackLayout { HorizontalOptions = LayoutOptions.FillAndExpand };
            stackLayoutInvoices = new StackLayout { HorizontalOptions = LayoutOptions.FillAndExpand };

            for (int i = 0; i < 12; i++)
            {
                AddQuoteToStack();
            }

            DisplayQuotes();
        }

        private void DisplayQuotes()
        {
            selectedTab = Tab.Quotes;
            labelQuotes.TextColor = (Color)App.Current.Resources["SPBlue"];
            btnQuotesTab.BackgroundColor = Color.White;
            stackLayoutMain.Children.Add(stackLayoutQuotes);
        }

        private void DisplayOrders()
        {
            selectedTab = Tab.Orders;
            btnOrdersTab.BackgroundColor = Color.White;
            labelOrders.TextColor = (Color)App.Current.Resources["SPBlue"];
            stackLayoutMain.Children.Add(stackLayoutOrders);
        }

        private void DisplayInvoices()
        {
            selectedTab = Tab.Invoices;
            btnInvoicesTab.BackgroundColor = Color.White;
            labelInvoices.TextColor = (Color)App.Current.Resources["SPBlue"];
            stackLayoutMain.Children.Add(stackLayoutInvoices);
        }


        private void HideQuotes()
        {
            btnQuotesTab.BackgroundColor = (Color)App.Current.Resources["SPBlue"];
            labelQuotes.TextColor = Color.White;
        }

        private void HideOrders()
        {
            btnOrdersTab.BackgroundColor = (Color)App.Current.Resources["SPBlue"];
            labelOrders.TextColor = Color.White;

        }

        private void HideInvoices()
        {
            btnInvoicesTab.BackgroundColor = (Color)App.Current.Resources["SPBlue"];
            labelInvoices.TextColor = Color.White;
        }
        private void Quotes_Clicked(object sender, EventArgs args)
        {
            if (selectedTab == Tab.Orders) HideOrders();
            else HideInvoices();
            stackLayoutMain.IsVisible = false;
            stackLayoutMain.Children.RemoveAt(0);
            DisplayQuotes();
            stackLayoutMain.IsVisible = true;
        }
        private void Orders_Clicked(object sender, EventArgs args)
        {
            if (selectedTab == Tab.Quotes) HideQuotes();
            else HideInvoices();
            stackLayoutMain.IsVisible = false;
            stackLayoutMain.Children.RemoveAt(0);
            DisplayOrders();
            stackLayoutMain.IsVisible = true;
        }
        private void Invoices_Clicked(object sender, EventArgs args)
        {
            if (selectedTab == Tab.Quotes) HideQuotes();
            else HideOrders();
            stackLayoutMain.IsVisible = false;
            stackLayoutMain.Children.RemoveAt(0);
            DisplayInvoices();
            stackLayoutMain.IsVisible = true;
        }

        /// <summary>
        /// Adds a generic example quote to the quoteLayout StackLayout
        /// </summary>
        private void AddQuoteToStack()
        {
            var quoteGrid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(30, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(39, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(30, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(6, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) },
                }
            };

            quoteGrid.Children.Add(new Button
            {
                BackgroundColor = Color.FromHex("#eaeafc"),
            }, 0, 6, 0, 2);
            quoteGrid.Children.Add(new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Start,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Text = "Quote Number",
                FontSize = 16,
            }, 1, 0);

            quoteGrid.Children.Add(new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Start,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Text = "###",
                FontSize = 16,
                InputTransparent = true,
            }, 1, 1);

            quoteGrid.Children.Add(new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Start,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Text = "Issue Date",
                FontSize = 16,
                InputTransparent = true,
            }, 2, 0);

            quoteGrid.Children.Add(new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Start,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Text = "DD.MM.YYYY",
                FontSize = 16,
                InputTransparent = true,
            }, 2, 1);

            quoteGrid.Children.Add(new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Start,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Text = "Price",
                FontSize = 16,
                InputTransparent = true,
            }, 3, 0);

            quoteGrid.Children.Add(new Label
            {
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Start,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Text = "$XXXXX",
                FontSize = 16,
                InputTransparent = true,
            }, 3, 1);

            quoteGrid.Children.Add(new Image
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Source = "rightarrow",
                InputTransparent = true,
            }, 4, 5, 0, 2);

            StackLayout quoteLayout = new StackLayout();
            quoteLayout.Children.Add(quoteGrid);

            stackLayoutQuotes.Children.Add(quoteLayout);
        }
    }
}