using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.IO;

namespace prototype2.Classes
{
    public class Invoices
    {

        //returns an ObservableCollection<Invoice> given a string uri
        public Invoices()
        {
            System.Collections.ObjectModel.ObservableCollection<Invoice> invoices =
            new System.Collections.ObjectModel.ObservableCollection<Invoice>();
        }

        async private void generateCollection(){
            HttpClient httpClient = new HttpClient();
            httpClient.BaseAddress = new Uri("http://10.0.2.2:3000");

            try
            {
                Stream responseBody = await httpClient.GetStreamAsync("invoice");


                List<JObject> LoadJson()
                {
                    using (StreamReader reader = new StreamReader(responseBody))
                    {
                        string jsonBody = reader.ReadToEnd();
                        List<JObject> jsonObjects = JsonConvert.DeserializeObject<List<JObject>>(jsonBody);
                        return jsonObjects;
                    }
                }
                List<JObject> idk = LoadJson();
                foreach (JObject qwe in idk)
                {
                    Invoice userInvoice = new Invoice();
                    userInvoice.invoiceNumber = qwe.GetValue("invoiceNumber").ToObject<int>();
                    userInvoice.amountDue = qwe.GetValue("amountDue").ToObject<int>();
                    userInvoice.dateDue = qwe.GetValue("dateDue").ToObject<DateTime>();

                    invoices.Add(userInvoice);
                }
        }
    }
}
