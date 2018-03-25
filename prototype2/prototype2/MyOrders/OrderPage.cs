using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace prototype2
{
    public partial class OrderPage : SalesDocumentPage
    {
        private Order order;
        public OrderPage(Order order) : base(order)
        {
            Title = "Order " + order.Number;

            this.order = order;
        }

        protected override void EditLabelsText()
        {
            labelDateHeading.Text = "Order Placed:";
        }

        protected override void AddProductToStack(Product addedProduct, int quantity)
        {
            var productGrid = new Grid
            {
                ColumnDefinitions = gridProductDetailsHeadings.ColumnDefinitions,
            };

            AddLabelsToProductGrid(productGrid, addedProduct, quantity);

            productDetailsStack.Children.Add(productGrid);
        }

        protected override void AddDocumentControls()
        {
            AddViewQuotePDFButton();
            if (((Order)document).Status == OrderStatus.InTransit)
                AddOrderTrackingInformationButton();
        }

        private void AddOrderTrackingInformationButton()
        {
            Button button = new Button
            {
                Text = "Track Delivery",
                BackgroundColor = Color.White,
                BorderColor = (Color)App.Current.Resources["SPBlue"],
                BorderWidth = 3,
                TextColor = (Color)App.Current.Resources["SPBlue"]
            };

            button.Clicked += Clicked_btnOrderTrackingInformation;

            stackDocumentControls.Children.Add(button);
        }

        private void Clicked_btnOrderTrackingInformation(object sender, EventArgs eventArgs)
        {
            ShowOrderTrackingInformation();
        }

        private async void ShowOrderTrackingInformation()
        {
            await DisplayAlert("Order Tracking", "", "OK");
        }
    }
}
