using prototype2.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace prototype2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MyOrders : ContentPage
    {
        enum Tab { Quotes, Orders, Invoices };
        Tab selectedTab;

        private const string buttonIndicator = "rightarrow"; // filepath of the image to be added to the buttons
        private const int fontSize = 16, tabAnimationTime = 500;
        private double pageWidth;
        private Color buttonColor = Color.FromHex("#eaeafc");

        StackLayout stackLayoutQuotes, stackLayoutOrders, stackLayoutInvoices;
        ScrollView scrollViewQuotes, scrollViewOrders, scrollViewInvoices;
        public MyOrders()
        {
            InitializeComponent();
            Title = "My Orders";
            NavigationPage.SetHasBackButton(this, false);

            stackLayoutQuotes = new StackLayout { HorizontalOptions = LayoutOptions.FillAndExpand };
            stackLayoutOrders = new StackLayout { HorizontalOptions = LayoutOptions.FillAndExpand };
            stackLayoutInvoices = new StackLayout { HorizontalOptions = LayoutOptions.FillAndExpand };

            scrollViewQuotes = new ScrollView { HorizontalOptions = LayoutOptions.FillAndExpand };
            scrollViewOrders = new ScrollView { HorizontalOptions = LayoutOptions.FillAndExpand, IsEnabled = false};
            scrollViewInvoices = new ScrollView { HorizontalOptions = LayoutOptions.FillAndExpand, IsEnabled = false };

            pageWidth = App.Current.MainPage.Width;
            InitializeLayoutPositions();
            AddIncompleteQuote();

            for (int i = Data.quotes.Count - 1; i >= 0; i--)
            {
                AddDocumentToStack(stackLayoutQuotes, Data.quotes[i]);
            }

            buttonColor = Color.FromHex("ceffcf");

            for (int i = Data.orders.Count - 1; i >= 0; i--)
            {
                AddDocumentToStack(stackLayoutOrders, Data.orders[i]);
            }

            for (int i = Data.invoices.Count - 1; i >= 0; i--)
            {
                buttonColor = GetInvoiceColor(Data.invoices[i].Status);
                AddDocumentToStack(stackLayoutInvoices, Data.invoices[i]);
            }
                

            DisplayQuotes();

            scrollViewQuotes.Content = stackLayoutQuotes;
            scrollViewOrders.Content = stackLayoutOrders;
            scrollViewInvoices.Content = stackLayoutInvoices;

            Grid displayGrid = new Grid();
            displayGrid.Children.Add(scrollViewQuotes, 0, 0);
            displayGrid.Children.Add(scrollViewOrders, 0, 0);
            displayGrid.Children.Add(scrollViewInvoices, 0, 0);

            stackLayoutMain.Children.Add(displayGrid);
        }

        private void InitializeLayoutPositions()
        {
            stackLayoutOrders.TranslateTo(pageWidth, 0, 0, Easing.CubicIn);
            stackLayoutInvoices.TranslateTo(pageWidth * 2, 0, 0, Easing.CubicIn);
        }

        private void DisplayQuotes()
        {
            selectedTab = Tab.Quotes;
            labelQuotes.TextColor = (Color)App.Current.Resources["SPBlue"];
            btnQuotesTab.BackgroundColor = Color.White;
            scrollViewQuotes.IsEnabled = true;
        }

        private void DisplayOrders()
        {
            selectedTab = Tab.Orders;
            btnOrdersTab.BackgroundColor = Color.White;
            labelOrders.TextColor = (Color)App.Current.Resources["SPBlue"];
            scrollViewOrders.IsEnabled = true;
        }

        private void DisplayInvoices()
        {
            selectedTab = Tab.Invoices;
            btnInvoicesTab.BackgroundColor = Color.White;
            labelInvoices.TextColor = (Color)App.Current.Resources["SPBlue"];
            scrollViewInvoices.IsEnabled = true;
        }


        private void HideQuotes()
        {
            btnQuotesTab.BackgroundColor = (Color)App.Current.Resources["SPBlue"];
            labelQuotes.TextColor = Color.White;
            scrollViewQuotes.IsEnabled = false;

        }

        private void HideOrders()
        {
            btnOrdersTab.BackgroundColor = (Color)App.Current.Resources["SPBlue"];
            labelOrders.TextColor = Color.White;
            scrollViewOrders.IsEnabled = false;


        }

        private void HideInvoices()
        {
            btnInvoicesTab.BackgroundColor = (Color)App.Current.Resources["SPBlue"];
            labelInvoices.TextColor = Color.White;
            scrollViewInvoices.IsEnabled = false;
        }

        private void AddIncompleteQuote()
        {
            if (Data.newQuote.Status == QuoteStatus.Incomplete) AddDocumentToStack(stackLayoutQuotes, Data.newQuote);
        }

        private void Clicked_btnQuotes(object sender, EventArgs args)
        {
            if (selectedTab == Tab.Quotes)
                return;

            AnimateToTab(0, pageWidth, pageWidth * 2);
            if (selectedTab == Tab.Orders)
                HideOrders();
            else
                HideInvoices();

            DisplayQuotes();
        }
        private void Clicked_btnOrders(object sender, EventArgs args)
        {
            if (selectedTab == Tab.Orders)
                return;

            AnimateToTab(-pageWidth, 0, pageWidth);
            if (selectedTab == Tab.Quotes)
                HideQuotes();
            else
                HideInvoices();

            DisplayOrders();
        }
        private void Clicked_btnInvoices(object sender, EventArgs args)
        {
            if (selectedTab == Tab.Invoices)
                return;

            AnimateToTab(-pageWidth * 2, -pageWidth, 0);
            if (selectedTab == Tab.Quotes)
                HideQuotes();
            else
                HideOrders();

            DisplayInvoices();
        }

        private async void AnimateToTab(double quotesPosition, double ordersPosition, double invoicesPosition)
        {
            await Task.WhenAll(
            stackLayoutQuotes.TranslateTo(quotesPosition, 0, tabAnimationTime, Easing.SinOut),
            stackLayoutOrders.TranslateTo(ordersPosition, 0, tabAnimationTime, Easing.SinOut),
            stackLayoutInvoices.TranslateTo(invoicesPosition, 0, tabAnimationTime, Easing.SinOut)
            );
        }

        /// <summary>
        /// Adds a generic example quote to the quoteLayout StackLayout
        /// </summary>
        private void AddDocumentToStack(StackLayout stack, SalesDocument document)
        {
            var documentGrid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(45, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(45, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(6, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(2, GridUnitType.Star) },
                },

                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(3, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(42, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(42, GridUnitType.Star) },
                    new RowDefinition { Height = new GridLength(3, GridUnitType.Star) },
                }
            };

            Button btnViewDocument = new Button
            {
                BackgroundColor = buttonColor,
            };

            btnViewDocument.ClassId = document.Number.ToString();
            btnViewDocument.Clicked += Clicked_btnViewDocument;

            documentGrid.Children.Add(btnViewDocument, 0, 5, 0, 4);

            documentGrid.Children.Add(new Label
            {
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Start,
                Text = document.Number,
                FontSize = fontSize,
                InputTransparent = true,
            }, 1, 1);

            documentGrid.Children.Add(new Label
            {
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.Start,
                Text = document.Status,
                FontSize = fontSize,
                InputTransparent = true,
            }, 1, 2);

            documentGrid.Children.Add(new Label
            {
                HorizontalOptions = LayoutOptions.End,
                Text = "$" + document.TotalPrice.ToString("N2"),
                FontSize = fontSize,
                InputTransparent = true,
            }, 2, 1);

            documentGrid.Children.Add(new Image
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Source = buttonIndicator,
                InputTransparent = true,
            }, 3, 4, 0, 4);

            documentGrid.Children.Add(new Label
            {
                FontSize = fontSize,
                HorizontalOptions = LayoutOptions.End,
                Text = document.Date.ToString("MM/dd/yyyy hh:mm"),
                InputTransparent = true,
            }, 2, 2);

            StackLayout documentLayout = new StackLayout();
            documentLayout.Children.Add(documentGrid);

            stack.Children.Add(documentLayout);
        }

        private Color GetInvoiceColor(string status)
        {
            if (status == InvoiceStatus.Unpaid)
                return Color.FromHex("fccf49");
            if (status == InvoiceStatus.Partial)
                return Color.FromHex("6ce4fc");
            if (status == InvoiceStatus.Paid)
                return Color.LightGreen;
            return Color.FromHex("fc5a5d");
        }

        private void Clicked_btnViewDocument(object sender, EventArgs eventaArgs)
        {
            Button button = (Button)sender;
            string documentNumber = button.ClassId;
            if (selectedTab == Tab.Quotes)
                Navigation.PushAsync(new QuotePage(documentNumber));
            else if (selectedTab == Tab.Orders)
                Navigation.PushAsync(new OrderPage(documentNumber));
            // else open invoice page
        }
    }
}