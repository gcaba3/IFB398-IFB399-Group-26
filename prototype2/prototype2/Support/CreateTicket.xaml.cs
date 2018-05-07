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
    public partial class CreateTicket : ContentPage
    {
        public CreateTicket()
        {
            InitializeComponent();
        }

        private void SubmitTicket(object sender, EventArgs e)
        {

            Ticket newTicket = new Ticket();
            newTicket.Title = "Title"; // + ticketList.length + 1;
            newTicket.Date = DateTime.Now.ToString("d");
            newTicket.Messages = Message.Text;
            newTicket.Number = 1; //ticketList.length + 1;
            newTicket.State = "In Process";
            App.Current.MainPage.DisplayAlert("Clicked!", "Submission Not implemented.", "OK");
            //ticketList.add(newTicket);
            //return newTicket;
            Navigation.PopAsync();

        }

        private void EditorPlaceHolder(object sender, TextChangedEventArgs e)
        {
            Message.Text = "";
            Message.TextChanged -= EditorPlaceHolder;

        }
    }
}