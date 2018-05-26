using System;
using System.Reflection;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Xamarin.Forms;

namespace prototype2
{
    public partial class LiveStatementPage : ContentPage
    {
        public double OutstandingBalance;

        //Holds the user's invoices
        public System.Collections.ObjectModel.ObservableCollection<Invoice> invoices =
            new System.Collections.ObjectModel.ObservableCollection<Invoice>();

        //Used to control the list view
        public System.Collections.ObjectModel.ObservableCollection<Invoice> displayInvoices =
            new System.Collections.ObjectModel.ObservableCollection<Invoice>();


        public LiveStatementPage()
        {
            InitializeComponent();

            Title = "Live Statement";
            GenerateSource(); // Pulls all the users invoices
            UpdateListView("All"); // Displays the list view
            GenerateOutstandingBalance(); // Shows outstanding balance of users invoices

        }


        //Creates the source of the list view
        private void GenerateSource()
        {
            for (int i = 0; i < 5; i++)
            {
                invoices.Add(new Invoice
                {
                    InvoiceNumber = i,
                    DateDue = new DateTime(2017, 1, 1),
                    AmountDue = 1000,
                    AmountPaid = 0
                });
            }

            for (int i = 5; i <= 10; i++)
            {
                invoices.Add(new Invoice
                {
                    InvoiceNumber = i,
                    DateDue = new DateTime(2017, 1, 1),
                    AmountDue = 1000,
                    AmountPaid = 1000
                });
            }


        }

        void UpdateListView(String action){
            if(action == "All"){
                displayInvoices = invoices;
            }

            invoiceList.ItemsSource = invoices;

        }

        void GenerateOutstandingBalance(){
            double total = 0;
            foreach(Invoice invoice in invoices){
                total += invoice.AmountDue - invoice.AmountPaid;
            }
            OutstandingBalance = total;

            outstandingBalance.Text = "$" + OutstandingBalance.ToString(); // Push to view
        }

        //Authorizes the payment of all invoices. Updates the invoices on server
        void Payall(object sender, System.EventArgs e)
        {
            //Query server to delete all invoices of the user
            //Generate source again
            invoices.Clear();
            GenerateOutstandingBalance();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            //Open invoice page
            Invoice selectedInvoice = args.SelectedItem as Invoice;

            await DisplayAlert("Open Invoice IN0000" + selectedInvoice.InvoiceNumber, "Temp. This should open individual invoice page", "OK"); // Replace
        }

        async void OnFilter(object sender, EventArgs args)
        {
            var action = await DisplayActionSheet("Filter By:", "Cancel", null, "All", "Paid", "Unpaid", "Partial", "Overdue");
            await DisplayAlert("Action Chosen", action, "exit");
        }

        async void OnSort(object sender, EventArgs args)
        {
            var action = await DisplayActionSheet("Sort By:", "Cancel", null, "Invoice Id", "Date Due");
            await DisplayAlert("Action Chosen", action, "exit");
        }


    }
}
