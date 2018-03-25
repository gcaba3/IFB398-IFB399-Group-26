using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace prototype2
{
    public partial class InvoicePage : SalesDocumentPage
    {
        private Invoice invoice;
        public InvoicePage(Invoice invoice) : base(invoice)
        {
            Title = "Invoice " + invoice.Number;

            this.invoice = invoice;

            AddHeaderInformation();

            if (invoice.AmountPaid > 0)
                AddAmountPaidLabels();
        }

        private void AddHeaderInformation()
        {
            gridHeaderInfo.Children.Add(new Label
            {
                Text = "Source:",
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 14,
            }, 1, 3);

            gridHeaderInfo.Children.Add(new Label
            {
                Text = invoice.Source,
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 14,
            }, 1, 4);

            gridHeaderInfo.Children.Add(new Label
            {
                Text = "Due Date:",
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 14,
            }, 3, 3);

            gridHeaderInfo.Children.Add(new Label
            {
                Text = invoice.DateDue.ToString("MM/dd/yyyy"),
                HorizontalOptions = LayoutOptions.Start,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 14,
            }, 3, 4);
        }

        private void AddAmountPaidLabels()
        {
            gridAmountRemaining.IsVisible = true;

            labelAmountPaid.Text = invoice.AmountPaid.ToString("N2");


            labelAmountDue.Text = (invoice.TotalPrice - invoice.AmountPaid).ToString("N2");
        }

        protected override void AddDocumentControls()
        {
            AddViewInvoicePDFButton();
            if (document.Status != InvoiceStatus.Paid)
                AddPayInvoiceButton();
        }

        protected override void EditLabelsText()
        {
            labelDateHeading.Text = "Invoice Date:";
        }

        private void AddViewInvoicePDFButton()
        {
            Button button = new Button
            {
                Text = "View Full Invoice PDF",
                BackgroundColor = Color.White,
                BorderColor = (Color)App.Current.Resources["SPBlue"],
                BorderWidth = 3,
                TextColor = (Color)App.Current.Resources["SPBlue"]
            };

            button.Clicked += Clicked_btnViewInvoicePDF;

            stackDocumentControls.Children.Add(button);
        }

        private void Clicked_btnViewInvoicePDF(object sender, EventArgs eventArgs)
        {
            Navigation.PushAsync(new PDFViewerPage("Invoice_INV_2018_0001_.pdf"));
        }

        private void AddPayInvoiceButton()
        {
            Button button = new Button
            {
                Text = "Pay Invoice",
                BackgroundColor = (Color)App.Current.Resources["SPBlue"],
                BorderWidth = 3,
                TextColor = Color.White,
            };

            button.Clicked += Clicked_btnPayInvoice;

            stackDocumentControls.Children.Add(button);
        }

        private void Clicked_btnPayInvoice(object sender, EventArgs eventArgs)
        {
            
        }
    }
}