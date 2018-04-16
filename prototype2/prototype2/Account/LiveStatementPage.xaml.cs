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
        public int OutstandingBalance;

        //Holds the user's invoices
        public System.Collections.ObjectModel.ObservableCollection<Invoice> invoices =
            new System.Collections.ObjectModel.ObservableCollection<Invoice>();


        public LiveStatementPage()
        {
            InitializeComponent();

            Title = "Live Statement";
            DisplayAlert("Reached 1", "", "OK");
            GenerateSource(); // generates the item source for the listview


        }

        //WARNING - has no validation so if any entry contains values thats not suppose to be used the like its type
        //Will throw an exception thats not caught

        //Note* write validation functions

        //Creates the source of the list view based on the currently logged in user
        async void GenerateSource()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://10.0.2.2:3000");//address of server

            try
            {
                //Invoices of the currently logged in user
                String userInvoicesURI = "invoice?userId=" + App.User.Default.ID.ToString();

                //queries the server for invoices of the current user
                Stream responseBody = await httpClient.GetStreamAsync(userInvoicesURI);

                //Creates a list of JSON objects based on the response of the server
                List<JObject> LoadJson()
                {
                    using (StreamReader reader = new StreamReader(responseBody))
                    {
                        string jsonBody = reader.ReadToEnd();
                        List<JObject> jsonObjects = JsonConvert.DeserializeObject<List<JObject>>(jsonBody);
                        return jsonObjects;
                    }
                }

                //stores the JSON list in a local variable
                List<JObject> jsonInvoices = LoadJson();

                //Creates the user's unpaid invoices to fill in the invoices collection
                foreach (JObject jsonInvoice in jsonInvoices)
                {
                    if (jsonInvoice.GetValue("amountPaid").ToObject<int>() <= 0)
                    {
                        Invoice userInvoice = new Invoice();
                        //userInvoice.invoiceNumber = jsonInvoice.GetValue("invoiceNumber").ToObject<int>();
                        //userInvoice.amountDue = jsonInvoice.GetValue("amountDue").ToObject<int>();
                        //userInvoice.dateDue = jsonInvoice.GetValue("dateDue").ToObject<DateTime>();

                        invoices.Add(userInvoice);
                    }

                }


                invoiceList.ItemsSource = invoices;
                //GenerateOutstandingBalance(); // sets the outstanding balance
            }
            catch (HttpRequestException exc)
            {
                await DisplayAlert("Something went wrong", "Exception caught: " + exc.Message, "OK");
            }

            // Disposes of client so that it doesnt leak info
            httpClient.Dispose();

        }


        //totals all the unpaid invoices and sets it to the outstanding balance
        private void GenerateOutstandingBalance()
        {
            int total = 0;

            foreach (Invoice invoice in invoices)
            {
                //total += invoice.amountDue;
            }
            this.OutstandingBalance = total;
            outstandingBalance.Text = "$" + total.ToString();

        }

        //Authorizes the payment of all invoices. Updates the invoices on server
        void Payall(object sender, System.EventArgs e)
        {
            //Query server to delete all invoices of the user
            //Generate source again
            invoices.Clear();
            GenerateOutstandingBalance();
        }

        //Query's the server to delete the specified invoice id of the user
        void PayInvoice(object sender, System.EventArgs e)
        {
            var invoiceEntry = (Xamarin.Forms.Button)sender;
            Invoice toDeleteItem;
            foreach (Invoice itm in invoices)
            {
                
                //if (itm.invoiceNumber.ToString() == invoiceEntry.CommandParameter.ToString())
                //{
                //    toDeleteItem = itm;
                //    invoices.Remove(toDeleteItem);
                //    GenerateOutstandingBalance();
                //    break;
                //}
            }

        }
    }
}
