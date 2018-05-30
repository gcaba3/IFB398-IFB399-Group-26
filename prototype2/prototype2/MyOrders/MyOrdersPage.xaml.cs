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
    public partial class MyOrdersPage : ContentPage
    {
        enum Tab { Quotes, Orders, Invoices };
        Tab selectedTab;

        private const string buttonIndicator = "rightarrow.png"; // filepath of the image to be added to the buttons
        private const int fontSize = 16, tabAnimationTime = 500;
        private double pageWidth;
        private Color buttonColor = Color.FromHex("#eaeafc");

        SearchGrid gridSearchGrid;
        StackLayout stackLayoutQuotes, stackLayoutOrders, stackLayoutInvoices;
        ScrollView scrollViewQuotes, scrollViewOrders, scrollViewInvoices;
        public MyOrdersPage()
        {
            InitializeComponent();
            Title = "My Orders";
            NavigationPage.SetHasBackButton(this, false);
            NavigationBar.ChangeMyOrdersTabColor();


            gridSearchGrid = new SearchGrid();
            gridPageContent.Children.Add(gridSearchGrid, 0, 1, 0, 3);

            stackLayoutQuotes = new StackLayout { HorizontalOptions = LayoutOptions.FillAndExpand, Spacing=0 };
            stackLayoutOrders = new StackLayout { HorizontalOptions = LayoutOptions.FillAndExpand, Spacing = 0 };
            stackLayoutInvoices = new StackLayout { HorizontalOptions = LayoutOptions.FillAndExpand, Spacing = 0 };

            scrollViewQuotes = new ScrollView { HorizontalOptions = LayoutOptions.FillAndExpand };
            scrollViewOrders = new ScrollView { HorizontalOptions = LayoutOptions.FillAndExpand, IsEnabled = false};
            scrollViewInvoices = new ScrollView { HorizontalOptions = LayoutOptions.FillAndExpand, IsEnabled = false };

            pageWidth = App.Current.MainPage.Width;
            InitializeLayoutPositions();
            AddIncompleteQuote();

            FillStackLayoutQuotes();
            FillStackLayoutOrders();
            FillStackLayoutInvoices();

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

        private void FillStackLayoutQuotes()
        {
            stackLayoutQuotes.Children.Add(new BoxView { Color = Color.Transparent, HeightRequest = 5 });
            for (int i = Data.quotes.Count - 1; i >= 0; i--)
            {
                AddDocumentToStack(stackLayoutQuotes, Data.quotes[i]);
            }
            stackLayoutQuotes.Children.Add(new BoxView { Color = Color.Transparent, HeightRequest = 5 });
        }
        private void FillStackLayoutOrders()
        {
            stackLayoutOrders.Children.Add(new BoxView { Color = Color.Transparent, HeightRequest = 5 });

            buttonColor = Color.FromHex("ceffcf");

            for (int i = Data.orders.Count - 1; i >= 0; i--)
            {
                AddDocumentToStack(stackLayoutOrders, Data.orders[i]);
            }

            stackLayoutOrders.Children.Add(new BoxView { Color = Color.Transparent, HeightRequest = 5 });
        }
        private void FillStackLayoutInvoices()
        {
            stackLayoutInvoices.Children.Add(new BoxView { Color = Color.Transparent, HeightRequest = 5 });

            for (int i = Data.invoices.Count - 1; i >= 0; i--)
            {
                buttonColor = GetInvoiceColor(Data.invoices[i].Status);
                AddDocumentToStack(stackLayoutInvoices, Data.invoices[i]);
            }

            stackLayoutInvoices.Children.Add(new BoxView { Color = Color.Transparent, HeightRequest = 5 });
        }

        private void DisplayQuotes()
        {
            selectedTab = Tab.Quotes;
            scrollViewQuotes.IsEnabled = true;            
        }

        private void DisplayOrders()
        {
            selectedTab = Tab.Orders;
            scrollViewOrders.IsEnabled = true;
        }

        private void DisplayInvoices()
        {
            selectedTab = Tab.Invoices;
            scrollViewInvoices.IsEnabled = true;
        }


        private void HideQuotes()
        {
            scrollViewQuotes.IsEnabled = false;

        }

        private void HideOrders()
        {
            scrollViewOrders.IsEnabled = false;


        }

        private void HideInvoices()
        {
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
            
            if (selectedTab == Tab.Orders)
                HideOrders();
            else
                HideInvoices();

            DisplayQuotes();

            AnimateToTab(0, pageWidth, pageWidth * 2, btnQuotesTab.X);
        }
        private void Clicked_btnOrders(object sender, EventArgs args)
        {
            if (selectedTab == Tab.Orders)
                return;
            
            if (selectedTab == Tab.Quotes)
                HideQuotes();
            else
                HideInvoices();

            DisplayOrders();

            AnimateToTab(-pageWidth, 0, pageWidth, btnOrdersTab.X);
        }
        private void Clicked_btnInvoices(object sender, EventArgs args)
        {
            if (selectedTab == Tab.Invoices)
                return;
            
            if (selectedTab == Tab.Quotes)
                HideQuotes();
            else
                HideOrders();

            DisplayInvoices();

            AnimateToTab(-pageWidth * 2, -pageWidth, 0, btnInvoicesTab.X);
        }

        private async void AnimateToTab(double quotesPosition, double ordersPosition, double invoicesPosition, double btnSelectedPosition)
        {
            labelSelected.Text = selectedTab.ToString();
            await Task.WhenAll(
            stackLayoutQuotes.TranslateTo(quotesPosition, 0, tabAnimationTime, Easing.SinOut),
            stackLayoutOrders.TranslateTo(ordersPosition, 0, tabAnimationTime, Easing.SinOut),
            stackLayoutInvoices.TranslateTo(invoicesPosition, 0, tabAnimationTime, Easing.SinOut),
            btnSelected.TranslateTo(btnSelectedPosition, 0, tabAnimationTime, Easing.SinOut),
            labelSelected.TranslateTo(btnSelectedPosition, 0, tabAnimationTime, Easing.SinOut)
            );            
        }

        /// <summary>
        /// Adds a generic example document to the corresponding StackLayout
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
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Star) },
                }
            };

            Button btnViewDocument = new Button
            {
                BackgroundColor = buttonColor,
                BorderColor = (Color)App.Current.Resources["SPBlue"],      
                BorderRadius = 20,
            };

            btnViewDocument.ClassId = document.Number.ToString();
            btnViewDocument.Clicked += Clicked_btnViewDocument;

            documentGrid.Children.Add(btnViewDocument, 0, 4, 0, 4);

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

            string productInfoText = document.Products.Values.First() + "x " + document.Products.Keys.First().Description;
            if (document.Products.Count > 1)
            {
                productInfoText += "\n(plus " + (document.Products.Keys.Count - 1) + " other product";
                if (document.Products.Count > 2)
                    productInfoText += "s";
                productInfoText += ")";
            }

            StackLayout stackProductInfo = new StackLayout //stack layout used to contain label to fix word wrap issues
            {
                Padding = new Thickness(0,0,0,4),
                InputTransparent = true
            };

            stackProductInfo.Children.Add(new Label
            {
                VerticalOptions = LayoutOptions.StartAndExpand,
                HorizontalOptions = LayoutOptions.StartAndExpand,
                Text = productInfoText,
                FontSize = 12,
            });

            documentGrid.Children.Add(stackProductInfo, 1, 3, 3, 4);

            documentGrid.Children.Add(new Image
            {
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
                Source = buttonIndicator,
                InputTransparent = true,
            }, 3, 4, 0, 5);

            documentGrid.Children.Add(new Label
            {
                FontSize = fontSize,
                HorizontalOptions = LayoutOptions.End,
                Text = document.Date.ToString("dd/MM/yyyy"),
                InputTransparent = true,
            }, 2, 2);

            StackLayout documentLayout = new StackLayout();
            documentLayout.Children.Add(documentGrid);

            stack.Children.Add(documentLayout);
        }

        private Color GetInvoiceColor(string status)
        {
            if (status == InvoiceStatus.Unpaid)
                return Color.FromHex("#ffd3a5");
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
                GoToQuotePage(documentNumber);
            else if (selectedTab == Tab.Orders)
                GoToOrderPage(documentNumber);
            else GoToInvoicePage(documentNumber);
        }

        private async void GoToQuotePage(string documentNumber)
        {
            Quote quote = Data.GetQuoteFromId(documentNumber);

            switch (quote.Status)
            {
                case QuoteStatus.Incomplete:
                    await Navigation.PushAsync(new QuoteIncompletePage(quote));
                    break;
                case QuoteStatus.PendingResponse:
                    await Navigation.PushAsync(new QuotePendingResponsePage(quote));
                    break;
                case QuoteStatus.Validated:
                    await Navigation.PushAsync(new QuoteValidatedPage(quote));
                    break;
            }
        }

        private async void GoToOrderPage(string orderNumber)
        {
            Order order = Data.GetOrderFromId(orderNumber);

            await Navigation.PushAsync(new OrderPage(order));
        }

        private async void GoToInvoicePage(string invoiceNumber)
        {
            Invoice invoice = Data.GetInvoiceFromId(invoiceNumber);

            await Navigation.PushAsync(new InvoicePage(invoice));
        }

        private async void Notifications_Clicked(object sender, EventArgs eventArgs)
        {
            NotificationPage notificationPage = new NotificationPage();
            await Navigation.PushAsync(notificationPage);
        }

        private void Clicked_BtnSearchTool(object sender, EventArgs eventArgs)
        {
            gridSearchGrid.Clicked_ButtonSearchTool();
        }
    }
}