using System;
using System.Reflection;
using System.IO;
using System.Collections.ObjectModel;
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
        public ObservableCollection<Invoice> invoices = new ObservableCollection<Invoice>();

        String filterAction = "Overdue";
        String sortAction = "Invoice Number Asc";

        public LiveStatementPage()
        {
            InitializeComponent();

            Title = "Live Statement";
            GenerateSource(); // Pulls all the users invoices
            invoiceList.ItemsSource = invoices; // Just initializes the view
            UpdateListView(); // Displays the list view
            GenerateOutstandingBalance(); // Shows outstanding balance of users invoices

        }


        //Creates the source of the list view
        private void GenerateSource()
        {
            //Oldest most overdue invoices
            for (int i = 0; i < 3; i++)
            {
                invoices.Add(new Invoice
                {
                    InvoiceNumber = i,
                    DateDue = new DateTime(2017, 1, 1 + i),
                    AmountDue = 1000,
                    AmountPaid = 0
                });
            }

            //Oldest fully paid invoices
            for (int i = 3; i < 6; i++)
            {
                invoices.Add(new Invoice
                {
                    InvoiceNumber = i,
                    DateDue = new DateTime(2017, 1, 1 + i),
                    AmountDue = 1000,
                    AmountPaid = 1000
                });
            }

            //Old partially paid invoices
            for (int i = 6; i < 9; i++)
            {
                invoices.Add(new Invoice
                {
                    InvoiceNumber = i,
                    DateDue = new DateTime(2018, 1, 1 + i),
                    AmountDue = 1000,
                    AmountPaid = 500
                });
            }

            //Newest
            for (int i = 9; i < 12; i++)
            {
                invoices.Add(new Invoice
                {
                    InvoiceNumber = i,
                    DateDue = new DateTime(2020, 1, 1 + i),
                    AmountDue = 1000,
                    AmountPaid = 0
                });
            }


        }

        void UpdateListView()
        {
            //Used to control the list view
            ObservableCollection<Invoice> displayInvoices = new ObservableCollection<Invoice>();

            displayedInvoicesTitle.Text = filterAction + " Invoices";


            if (filterAction != "All")
            {
                displayInvoices = ApplyFilterAction(filterAction);

            }
            else
            {
                displayInvoices = invoices;
            }


            invoiceList.ItemsSource = ApplySortAction(displayInvoices);

        }

        ObservableCollection<Invoice> ApplySortAction( ObservableCollection<Invoice> displayList){
            for (int i = 0; i <= displayList.Count - 1; i++){
                for (int j = 0; j <= displayList.Count - 1; j++){
                    if (sortAction == "Invoice Number Asc")
                    {
                        if (displayList[i].InvoiceNumber <= displayList[j].InvoiceNumber)
                        {
                            displayList.Move(i, j);
                        }
                    }
                    else if (sortAction == "Invoice Number Desc")
                    {
                        if (displayList[i].InvoiceNumber >= displayList[j].InvoiceNumber)
                        {
                            displayList.Move(i, j);
                        }
                    }
                    else if(sortAction == "Date Due Asc"){
                        if(displayList[i].DateDue <= displayList[j].DateDue){
                            displayList.Move(i, j);
                        }
                    }
                    else if (sortAction == "Date Due Desc")
                    {
                        if (displayList[i].DateDue >= displayList[j].DateDue)
                        {
                            displayList.Move(i, j);
                        }
                    }
                    else if (sortAction == "Amount Due Asc")
                    {
                        if (displayList[i].AmountDue <= displayList[j].AmountDue)
                        {
                            displayList.Move(i, j);
                        }
                    }
                    else if (sortAction == "Amount Due Desc")
                    {
                        if (displayList[i].AmountDue >= displayList[j].AmountDue)
                        {
                            displayList.Move(i, j);
                        }
                    }
                    else if (sortAction == "Amount Paid Asc")
                    {
                        if (displayList[i].AmountPaid <= displayList[j].AmountPaid)
                        {
                            displayList.Move(i, j);
                        }
                    }
                    else if (sortAction == "Amount Paid Desc")
                    {
                        if (displayList[i].AmountPaid >= displayList[j].AmountPaid)
                        {
                            displayList.Move(i, j);
                        }
                    }
                }
            }
            return displayList;
        }

        ObservableCollection<Invoice> ApplyFilterAction(String action)
        {
            ObservableCollection<Invoice> displayInvoices = new ObservableCollection<Invoice>();
            foreach (Invoice invoice in invoices)
            {
                var amountRemaining = invoice.AmountDue - invoice.AmountPaid;
                if (action == "Paid" && amountRemaining.Equals(0.0))
                {
                    displayInvoices.Add(invoice);
                }
                else if (action == "No Payments" && amountRemaining.Equals(invoice.AmountDue))
                {
                    displayInvoices.Add(invoice);
                }
                else if (action == "Partially Paid" && invoice.AmountPaid > 0.0 && !amountRemaining.Equals(0.0))
                {
                    displayInvoices.Add(invoice);
                }
                else if (action == "Overdue" && invoice.DateDue < DateTime.Today && !amountRemaining.Equals(0.0))
                {
                    displayInvoices.Add(invoice);
                }

            }

            return displayInvoices;
        }

        void GenerateOutstandingBalance()
        {
            double total = 0;
            foreach (Invoice invoice in invoices)
            {
                total += invoice.AmountDue - invoice.AmountPaid;
            }
            OutstandingBalance = total;

            outstandingBalance.Text = "$" + OutstandingBalance.ToString(); // Push to view
        }

        //Authorizes the payment of all invoices. Updates the invoices on server
        async void Payall(object sender, System.EventArgs e)
        {
            //Query server to delete all invoices of the user
            //Generate source again

            var answer = await DisplayAlert("Confirm Payment", "hello", "Confirm", "Cancel");
            if(answer){
                foreach(Invoice invoice in invoices){
                    var amountRemaining = invoice.AmountDue - invoice.AmountPaid;
                    invoice.AmountPaid += amountRemaining;
                }

            }

            GenerateOutstandingBalance();
            UpdateListView();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            //Open invoice page
            Invoice selectedInvoice = args.SelectedItem as Invoice;

            await DisplayAlert("Open Invoice IN0000" + selectedInvoice.InvoiceNumber, "Temp. This should open individual invoice page", "OK"); // Replace
        }

        async void OnFilter(object sender, EventArgs args)
        {
            var action = await DisplayActionSheet("Filter By:", "Cancel", null, "All", "Paid", "No Payments", "Partially Paid", "Overdue");
            if (action != "Cancel")
            {
                filterAction = action;
            }
            UpdateListView();
        }

        async void OnSort(object sender, EventArgs args)
        {
            var action = await DisplayActionSheet("Sort By:", "Cancel", null,"Invoice Number Asc", "Invoice Number Desc", "Date Due Asc", "Date Due Desc", "Amount Due Asc", "Amount Due Desc", "Amount Paid Asc","Amount Paid Desc");
            if (action != "Cancel")
            {
                sortAction = action;
            }
            UpdateListView();
        }


    }
}
