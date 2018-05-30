using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using prototype2.Classes;
using static prototype2.NavigationBar;

namespace prototype2
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CreateTicket : ContentPage
    {
        public CreateTicket()
        {
            InitializeComponent();
        }

        private void SubmitTicket(object sender, EventArgs e)
        {

            Ticket newTicket = new Ticket();
            newTicket.Title = Subject.Text;
            newTicket.Date = DateTime.Now.ToString("d");
            newTicket.Messages = new List<string>();
            newTicket.Messages.Add("Client:"+ Message.Text);
            newTicket.Number = Data.tickets.Count();
            newTicket.State = TicketStatus.InProgress;
            newTicket.SentTo = "Technical Support";
            Data.tickets.Add(newTicket);

            Navigation.PushAsync(new AccountPage(Tab.Support));

        }

        private void EditorPlaceHolder(object sender, TextChangedEventArgs e)
        {
            Message.Text = "";
            Message.TextChanged -= EditorPlaceHolder;

        }
    }
}