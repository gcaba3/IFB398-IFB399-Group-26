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
    public partial class TicketSingle : ContentPage
    {
        private Ticket thisTicket;
        public TicketSingle(Ticket selectedTicket)
        {
            
            InitializeComponent();
            thisTicket = selectedTicket;
            Title = thisTicket.Number.ToString("#000000");

            FillData();
        }

        private void FillData()
        {
            //fill the page here
            //name
            Name.Text = thisTicket.Title;
            
        }

        private void sendMessage()
        {
            //send the newly message here
        }
    }
}