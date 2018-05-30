using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace prototype2
{    
    public partial class QuoteValidatedPage : QuotePage
    {
        public QuoteValidatedPage(Quote quote) : base(quote)
        {
            
        }        

        protected override void AddDocumentControls()
        {
            AddViewQuotePDFButton();
            AddPlaceOrderButton();
            AddDeleteQuoteButton();
        }

        protected override void EditLabelsText()
        {
            labelDateHeading.Text = "Quotation Date";
        }

        private void AddPlaceOrderButton()
        {
            Button button = new Button
            {
                Text = "Place Order",
                BackgroundColor = (Color)App.Current.Resources["SPBlue"],
                TextColor = Color.White,
            };

            button.Clicked += Clicked_btnPlaceOrder;

            stackDocumentControls.Children.Add(button);
        }

        private void Clicked_btnPlaceOrder(object sender, EventArgs e)
        {
            ConfirmPlaceOrder();
            
        }

        private async void ConfirmPlaceOrder()
        {
            var answer = await DisplayAlert("Place Order", "", "Confirm", "Cancel");
            if (answer)
            {
                Data.ConvertQuoteToOrder(quote);
                //quote.Status = QuoteStatus.OrderPlaced;
                PopPageReturnToMyOrders();
            }            
        }

        private void AddDeleteQuoteButton()
        {
            Button button = new Button
            {
                Text = "Delete Quote",
                BackgroundColor = (Color)App.Current.Resources["SPRed"],
                TextColor = Color.White
            };

            button.Clicked += Clicked_btnDeleteQuote;

            stackDocumentControls.Children.Add(button);
        }

        private void Clicked_btnDeleteQuote(object sender, EventArgs e)
        {
            Data.DeleteQuote(quote);
            PopPageReturnToMyOrders();
        }
    }
}
