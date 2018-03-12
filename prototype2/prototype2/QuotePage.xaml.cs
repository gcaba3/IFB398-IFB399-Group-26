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
    public partial class QuotePage : ContentPage
    {
        public QuotePage(int number)
        {
            InitializeComponent();

            DisplayData(number);
        }

        /// <summary>
        /// Finds the data for the quote and fills out the quote page with the data
        /// </summary>
        /// <param name="number"></param>
        private void DisplayData(int number)
        {
            Classes.Quote quote;
            if (number == 0) quote = Data.newQuote;
            else quote = Data.quotes.Single(Quote => Quote.Number == number);

            txtNumber.Text = quote.Number.ToString();
            txtStatus.Text = quote.Status.ToString();

            if (quote.Status == Classes.QuoteStatus.Incomplete.ToString())
            {
                labelQuotationDate.Text = "Last Edited";
                AddIncompleteQuoteButtons();
            }


            else if (quote.Status == Classes.QuoteStatus.PendingResponse.ToString())
            {
                labelQuotationDate.Text = "Request Sent";
                AddCancelButton();
            }
            else labelQuotationDate.Text = "Quotation Date";

            txtQuotationDate.Text = quote.Date.ToString("MM/dd/yyyy HH:mm:ss");
            double subtotal = 0;

            foreach (KeyValuePair<Classes.Product, int> product in quote.Products)
            {
                AddProductToStack(product.Key, product.Value);
                subtotal += product.Key.Price * product.Value;
            }
            double gst = Math.Round(subtotal * 0.1, 2, MidpointRounding.AwayFromZero);

            txtSubTotal.Text = subtotal.ToString();
            txtDiscount.Text = "0.00";
            txtExclGST.Text = subtotal.ToString();
            txtGST.Text = gst.ToString();
            txtTotal.Text = quote.TotalPrice.ToString("N2");
        }

        private void AddProductToStack(Classes.Product product, int quantity)
        {
            var productGrid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(5, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(22.5, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(22.5, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(22.5, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(22.5, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(5, GridUnitType.Star) },
                },
            };

            productGrid.Children.Add(new Label
            {
                Text = product.Name,
                FontSize = 14,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Start
            }, 1, 3, 0, 1);

            productGrid.Children.Add(new Label
            {
                Text = quantity.ToString(),
                FontSize = 14,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
            }, 3, 0);

            productGrid.Children.Add(new Label
            {
                Text = product.Price.ToString("N2"),
                FontSize = 14,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.End,
            }, 4, 0);

            productDetailsStack.Children.Add(productGrid);
        }

        private void AddIncompleteQuoteButtons()
        {
            AddDeleteButton();
            AddRequestQuoteButton();
        }

        private void AddDeleteButton()
        {
            Button button = new Button
            {
                Text = "Delete",
                BackgroundColor = (Color)App.Current.Resources["SPRed"],
                TextColor = Color.White
            };

            button.Clicked += Clicked_btnDelete;

            stackQuoteControls.Children.Add(button);
        }

        private void AddCancelButton()
        {
            Button button = new Button
            {
                Text = "Cancel",
                BackgroundColor = (Color)App.Current.Resources["SPRed"],
                TextColor = Color.White
            };

            button.Clicked += Clicked_btnCancel;

            stackQuoteControls.Children.Add(button);
        }

        private void Clicked_btnDelete(object sender, EventArgs eventArgs)
        {

        }
        private void Clicked_btnCancel(object sender, EventArgs eventArgs)
        {

        }
        private void AddRequestQuoteButton()
        {
            Button button = new Button
            {
                Text = "Request Quote",
                BackgroundColor = (Color)App.Current.Resources["SPBlue"],
                TextColor = Color.White
            };

            button.Clicked += Clicked_btnRequestQuote;

            stackQuoteControls.Children.Add(button);
        }

        private void Clicked_btnRequestQuote(object sender, EventArgs eventArgs)
        {

        }
    }
}