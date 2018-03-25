using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace prototype2
{
    public partial class QuotePendingResponsePage : QuotePage
    {
        public QuotePendingResponsePage(Quote quote) : base(quote)
        {

        }

        protected override void EditLabelsText()
        {
            labelDateHeading.Text = "Validation Requested:";
        }

        protected override void AddDocumentControls()
        {
            AddCancelButton();
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

            stackDocumentControls.Children.Add(button);
        }

        private void Clicked_btnCancel(object sender, EventArgs eventArgs)
        {
            ConfirmCancellation();
        }

        private async void ConfirmCancellation()
        {
            var answer = await DisplayAlert("Cancel", "Delete this quote and cancel validation?", "Yes", "No");
            if (answer)
            {
                Data.DeleteQuote(quote);
                PopPageReturnToMyOrders();
            }
        }
    }
}
