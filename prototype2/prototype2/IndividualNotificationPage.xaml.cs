using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace prototype2
{
    public partial class IndividualNotificationPage : ContentPage
    {
        public IndividualNotificationPage(String title, DateTime date, String message)
        {
            InitializeComponent();
            this.Title = title;
            this.postedDate.Text = "Posted on " + date.ToString("dd/MM/yyyy");
            this.Message.Text =  message;
        }
    }
}
