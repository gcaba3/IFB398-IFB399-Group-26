using prototype2.Classes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace prototype2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SendMessagePopup : ContentPage
    {
        private Ticket thisTicket;
        public SendMessagePopup(Ticket selectedTicket)
        {
            InitializeComponent();
            thisTicket = selectedTicket;
        }

        private void Clicked_btnCancel(object sender, EventArgs eventArgs)
        {
            Navigation.PopAsync();
        }



        private void Clicked_btnConfirm(object sender, EventArgs eventArgs)
        {
            thisTicket.Messages.Add("Client:" + Message.Text);
            Navigation.PushAsync(new TicketSingle(thisTicket));
        }

    }
}