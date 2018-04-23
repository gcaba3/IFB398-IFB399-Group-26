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
    public partial class CreateTicket : ContentPage
    {
        public CreateTicket()
        {
            InitializeComponent();
        }

        private void SubmitTicket(object sender, EventArgs e)
        {
            App.Current.MainPage.DisplayAlert("Clicked!", "Submission Not implemented.", "OK");
        }

        private void EditorPlaceHolder(object sender, TextChangedEventArgs e)
        {
            Message.Text = "";
            Message.TextChanged -= EditorPlaceHolder;
        }
    }
}