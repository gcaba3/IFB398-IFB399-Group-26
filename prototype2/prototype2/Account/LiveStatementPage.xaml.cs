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
            UpdateListView(); // Displays the list view
            GenerateOutstandingBalance(); // Shows outstanding balance of users invoices

        }

        //View Controllers

        //Authorizes the payment of all invoices. Updates the invoices on server
        async void Payall(object sender, System.EventArgs e)
        {
            //Query server to delete all invoices of the user
            //Generate source again
            String confirmationMessage = ConfirmationMessageMaker();
            var answer = await DisplayAlert("Confirm Payment", confirmationMessage, "Confirm", "Cancel");
            if(answer){
                foreach(Invoice invoice in invoices){
                    var amountRemaining = invoice.AmountDue - invoice.AmountPaid;
                    if(invoice.DateDue < DateTime.Today && !amountRemaining.Equals(0.0)){
                        invoice.AmountPaid += amountRemaining;
                    }
                }

            }

            GenerateOutstandingBalance();
            UpdateListView();
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            //Open invoice page
            Invoice selectedInvoice = args.SelectedItem as Invoice;

            String invoiceID = String.Format("IN{0:00000}", selectedInvoice.InvoiceNumber); // Formatting to work with james thing

            if(selectedInvoice.InvoiceNumber > 4){
                await DisplayAlert("Open Invoice: " + invoiceID, "Sorry for now invoices from IN00001 to IN00004 are valid... for now...", "OK"); // Get rid of this when connected to database
            } else {
                // This is using james's way to open an individual invoice page. Change when connected to database
                Invoice invoice = Data.GetInvoiceFromId(invoiceID); 

                await Navigation.PushAsync(new InvoicePage(invoice));
            }
        }

        async void OnFilter(object sender, EventArgs args)
        {
            var action = await DisplayActionSheet("Filter By:", "Cancel", null, "All", "New", "Overdue", "Paid", "Unpaid", "Partially Paid");
            if (action != "Cancel")
            {
                filterAction = action;
            }
            UpdateListView();
        }

        //Helper functions

        //Creates the source of the list view
        private void GenerateSource()
        {
            //Valid invoice openings - based on james individual invoice page
            invoices.Add(new Invoice
            {
                InvoiceNumber = 1,
                DateDue = new DateTime(2017, 04, 14),
                AmountDue = 9623,
                AmountPaid = 9623
            });
            invoices.Add(new Invoice
            {
                InvoiceNumber = 2,
                DateDue = new DateTime(2018, 05, 03),
                AmountDue = 48632,
                AmountPaid = 0
            });
            invoices.Add(new Invoice
            {
                InvoiceNumber = 3,
                DateDue = new DateTime(2018, 03, 16),
                AmountDue = 22231,
                AmountPaid = 4000
            });
            invoices.Add(new Invoice
            {
                InvoiceNumber = 4,
                DateDue = new DateTime(2018, 11, 5),
                AmountDue = 4310,
                AmountPaid = 0
            });
            //Extra padding

            //Oldest fully paid invoices
            for (int i = 5; i < 8; i++)
            {
                invoices.Add(new Invoice
                {
                    InvoiceNumber = i,
                    DateDue = new DateTime(2017, 1, 1 + i),
                    AmountDue = 1000,
                    AmountPaid = 1000
                });
            }

            //OverDue partially paid invoices
            for (int i = 8; i < 11; i++)
            {
                invoices.Add(new Invoice
                {
                    InvoiceNumber = i,
                    DateDue = new DateTime(2018, 1, 1 + i),
                    AmountDue = 1000,
                    AmountPaid = 500
                });
            }

            //Newest unpaid invocies
            for (int i = 11; i < 14; i++)
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

        void GenerateOutstandingBalance()
        {
            double total = 0; // Total for all the overdue invoces
            foreach (Invoice invoice in invoices)
            {
                var amountRemaining = invoice.AmountDue - invoice.AmountPaid;
                if (invoice.DateDue < DateTime.Today && !amountRemaining.Equals(0.0))
                {
                    total += amountRemaining;
                }
            }
            OutstandingBalance = total;

            outstandingBalance.Text = "$" + OutstandingBalance.ToString(); // Push to view
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

        String ConfirmationMessageMaker(){
            String returnMessage = "";
            foreach(Invoice invoice in invoices){
                var amountRemaining = invoice.AmountDue - invoice.AmountPaid;
                if(invoice.DateDue < DateTime.Today && !amountRemaining.Equals(0.0)){
                    String invoiceMessage = String.Format("Invoice IN0000{0} - Pay: ${1}\n", invoice.InvoiceNumber, amountRemaining);
                    returnMessage += invoiceMessage;
                }
            }

            returnMessage += String.Format("Total: {0}", OutstandingBalance);

            return returnMessage;


        }

        ObservableCollection<Invoice> ApplySortAction(ObservableCollection<Invoice> displayList)
        {
            for (int i = 0; i <= displayList.Count - 1; i++)
            {
                for (int j = 0; j <= displayList.Count - 1; j++)
                {
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
                    else if (sortAction == "Date Due Asc")
                    {
                        if (displayList[i].DateDue <= displayList[j].DateDue)
                        {
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
                else if (action == "Unpaid" && amountRemaining.Equals(invoice.AmountDue))
                {
                    displayInvoices.Add(invoice);
                }
                else if (action == "Partially Paid" && invoice.AmountPaid > 0.0 && !amountRemaining.Equals(0.0))
                {
                    displayInvoices.Add(invoice);
                }
                else if (action == "Overdue" && invoice.DateDue <= DateTime.Today && !amountRemaining.Equals(0.0))
                {
                    displayInvoices.Add(invoice);
                }
                else if (action == "New" && invoice.DateDue > DateTime.Today)
                {
                    displayInvoices.Add(invoice);
                }

            }

            return displayInvoices;
        }

    }
}
