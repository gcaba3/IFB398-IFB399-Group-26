using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace prototype2
{
    public partial class LiveStatementPage : ContentPage
    {
        public int OutstandingBalance;

        public class Invoice
        {
            public int InvoiceNumber { get; set; }
            public DateTime Date { get; set; }
            public int Amount { get; set; }
        }

        public System.Collections.ObjectModel.ObservableCollection<Invoice> invoices = new System.Collections.ObjectModel.ObservableCollection<Invoice>();

        public LiveStatementPage()
        {
            InitializeComponent();
            Title = "Live Statement";
            GenerateSource(); // generates the item source for the listview
            SetBalance(); // sets the outstanding balance


        }

        //Creates the source of the list view
        private void GenerateSource()
        {
            for (int i = 0; i < 20; i++)
            {
                invoices.Add(new Invoice
                {
                    InvoiceNumber = i,
                    Date = new DateTime(2017, 1, 1),
                    Amount = 500
                });
            }

            invoiceList.ItemsSource = invoices;
        }

        //Sets the outstanding balance
        private void SetBalance()
        {
            int pulledValue = GenerateOutstandingBalance(); 

            this.OutstandingBalance = pulledValue;
            outstandingBalance.Text = "$" + OutstandingBalance.ToString();
        }

        //totals all the unpaid balances and sets it to the outstanding balance
        private int GenerateOutstandingBalance()
        {
            int total = 0;

            foreach (Invoice invoice in invoices)
            {
                total += invoice.Amount;
            }
            return total;

        }

        //clears all the invoices collection
        void Payall(object sender, System.EventArgs e)
        {
            invoices.Clear();
            SetBalance();
        }

        //removes the entry with the matching name of the button sender's command parameter
        void PayInvoice(object sender, System.EventArgs e)
        {
            var invoiceEntry = (Xamarin.Forms.Button)sender;
            Invoice toDeleteItem;
            foreach(Invoice itm in invoices){
                if (itm.InvoiceNumber.ToString() == invoiceEntry.CommandParameter.ToString()){
                    toDeleteItem = itm;
                    invoices.Remove(toDeleteItem);
                    SetBalance();
                    break;
                }
            }

        }
    }
}
