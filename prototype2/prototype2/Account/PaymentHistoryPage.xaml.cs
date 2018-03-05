using System;
using System.Collections.Generic;

using Xamarin.Forms;


namespace prototype2
{
    public partial class PaymentHistoryPage : ContentPage
    {
        public System.Collections.ObjectModel.ObservableCollection<Invoice> invoices = 
            new System.Collections.ObjectModel.ObservableCollection<Invoice>();


        public PaymentHistoryPage()
        {
            InitializeComponent();
            this.Title = "Payment History";
            GenerateSource();
        }

        //Creates the source of the list view
        private void GenerateSource()
        {
            for (int i = 0; i < 20; i++)
            {
                invoices.Add(new Invoice
                {
                    InvoiceNumber = i,
                    DatePaid = new DateTime(2017, 1, 1),
                    AmountDue = 1000,
                    AmountPaid = 1000
                });
            }

            invoiceList.ItemsSource = invoices;
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            //Open invoice page
            await DisplayAlert("Open Invoice page", "Temp. This should open individual invoice page", "OK"); // Replace 
        }

        async void Handle_Clicked(object sender, System.EventArgs e)
        {
            var action = await DisplayActionSheet("Sort by:", "Cancel", null, "Invoice Asc", "Invoice Desc", "Amount Due Asc", "Amount Due Desc", "Date Paid Asc", 
                                                  "Date Paid Desc", "Amount Paid Asc", "Amount Paid Desc");
           await  DisplayAlert("hello",action,"exit");
        }
    }
}
