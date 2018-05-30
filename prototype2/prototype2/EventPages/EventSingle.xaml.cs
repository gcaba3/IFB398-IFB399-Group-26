using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

using prototype2.Classes;

namespace prototype2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class EventSingle : ContentPage
    {
        private Event thisEvent;
        public EventSingle(Event selectedEvent)
        {
            InitializeComponent();
            thisEvent = selectedEvent;
            Title = thisEvent.Name;

            fillData();
            
        }

        private void fillData()
        {
            //picture

            Picture.Source = "http://supplypartners.com.au/files/uploads/2015/10/2018_MASTERCLASS_LOGO-150x150@2x.png";
            //name
            Name.Text = thisEvent.Name;
            //description
            Description.Text = thisEvent.Description;
            //price
            Price.Text = "$"+thisEvent.Price.ToString("00.00");
            //address
            Address.Text = thisEvent.Address;
            //date
            Date.Text = thisEvent.Date;
        }

        private void paymentButton(object sender, System.EventArgs e)
        {
            Device.OpenUri(new Uri(thisEvent.PurchaseURL));
            //await DisplayAlert("button", "purchase "+ NumTicketsEntry.Text + " tickets - not implemented", "ok");
        }

    }
}