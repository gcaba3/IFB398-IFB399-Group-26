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
            Title.Text = thisTicket.Number.ToString("#000000");

            FillData();
        }

        private void FillData()
        {
            //fill the page here
            Title.Text = thisTicket.Title;
            PostedDate.Text = thisTicket.Date;
            SentTo.Text = thisTicket.SentTo;
            State.Text = thisTicket.State;
            // messages place into page
            for (int i = 0; i < thisTicket.Messages.Count; i++)
            {
                // place message into grid
                if (thisTicket.Messages[i].StartsWith("Client:"))
                {
                    messagesContainer.Children.Add(new Label
                    {
                        BackgroundColor = Color.SkyBlue,
                        TextColor = Color.Black,
                        FontSize = 14,
                        Text = thisTicket.Messages[i].Remove(0, 7),
                        HorizontalTextAlignment = TextAlignment.Center,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand
                    },0, 1, i,i+1);
                } else
                {
                    messagesContainer.Children.Add(new Label
                    {
                        BackgroundColor = Color.LightGreen,
                        TextColor = Color.Black,
                        FontSize = 14,
                        Text = thisTicket.Messages[i],
                        HorizontalTextAlignment = TextAlignment.Center,
                        HorizontalOptions = LayoutOptions.CenterAndExpand,
                        VerticalOptions = LayoutOptions.CenterAndExpand
                    }, 1, 2, i, i+1);
                }

            };
            
        }

        private void SendMessage(object sender, EventArgs e)
        {
            Navigation.PushAsync(new SendMessagePopup(thisTicket));
            //send the newly message here
            //thisTicket.Messages += EnteredMessage
        }
    }
}