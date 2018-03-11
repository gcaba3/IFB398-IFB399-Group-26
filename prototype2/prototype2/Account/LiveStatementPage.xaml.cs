using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;
using Xamarin.Forms;

namespace prototype2
{
    public partial class LiveStatementPage : ContentPage
    {
        public int OutstandingBalance;

        public System.Collections.ObjectModel.ObservableCollection<Invoice> invoices = 
            new System.Collections.ObjectModel.ObservableCollection<Invoice>();



        public LiveStatementPage()
        {
            InitializeComponent();
            Title = "Live Statement";
            GenerateSource(); // generates the item source for the listview


        }

        //Creates the source of the list view
        async private void GenerateSource()
        {
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://10.0.2.2:3000");

            try
            {
                Stream responseBody = await httpClient.GetStreamAsync("invoice");


                List<JObject> LoadJson (){
                    using (StreamReader reader = new StreamReader(responseBody))
                    {
                        string jsonBody = reader.ReadToEnd();
                        List<JObject> jsonObjects = JsonConvert.DeserializeObject<List<JObject>>(jsonBody);
                        return jsonObjects;
                    }
                }
                List<JObject> idk = LoadJson();
                foreach (JObject qwe in idk){
                    Invoice userInvoice = new Invoice();
                    userInvoice.invoiceNumber = qwe.GetValue("invoiceNumber").ToObject<int>();
                    userInvoice.amountDue = qwe.GetValue("amountDue").ToObject<int>();
                    userInvoice.dateDue = qwe.GetValue("dateDue").ToObject<DateTime>();

                    invoices.Add(userInvoice);
                }


                invoiceList.ItemsSource = invoices;
                SetBalance(); // sets the outstanding balance

                //await DisplayAlert("Connected: Now format", output, "OK");
            }
            catch (HttpRequestException exc)
            {
                await DisplayAlert("Alert", "Exception caught: " + exc.Message, "OK");
            }

            // Disposes of client so that it doesnt leak info
            httpClient.Dispose();

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
                total += invoice.amountDue;
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
                if (itm.invoiceNumber.ToString() == invoiceEntry.CommandParameter.ToString()){
                    toDeleteItem = itm;
                    invoices.Remove(toDeleteItem);
                    SetBalance();
                    break;
                }
            }

        }
    }
}
