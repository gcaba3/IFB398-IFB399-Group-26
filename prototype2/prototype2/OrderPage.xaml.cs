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
    public partial class OrderPage : ContentPage
    {
        public OrderPage(string number)
        {
            InitializeComponent();

            DisplayData(number);
        }

        /// <summary>
        /// Finds the data for the order and fills out the order page with the data
        /// </summary>
        /// <param name="number"></param>
        private void DisplayData(string number)
        {
            Classes.Order order;

            order = Data.orders.Single(Order => Order.Number == number.ToString());

            txtNumber.Text = order.Number.ToString();
            txtStatus.Text = order.Status.ToString();

            labelOrderDate.Text = "Order Date";

            txtOrderDate.Text = order.Date.ToString("MM/dd/yyyy HH:mm:ss");
            double subtotal = 0;

            foreach (KeyValuePair<Classes.Product, int> product in order.Products)
            {
                AddProductToStack(product.Key, product.Value);
                subtotal += product.Key.Price * product.Value;
            }
            double gst = Math.Round(subtotal * 0.1, 2, MidpointRounding.AwayFromZero);

            txtSubTotal.Text = subtotal.ToString();
            txtDiscount.Text = "0.00";
            txtExclGST.Text = subtotal.ToString();
            txtGST.Text = gst.ToString();
            txtTotal.Text = order.TotalPrice.ToString("N2");
        }

        private void AddProductToStack(Classes.Product product, int quantity)
        {
            var productGrid = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(5, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(22.5, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(22.5, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(22.5, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(22.5, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(5, GridUnitType.Star) },
                },
            };

            productGrid.Children.Add(new Label
            {
                Text = product.Name,
                FontSize = 14,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Start
            }, 1, 3, 0, 1);

            productGrid.Children.Add(new Label
            {
                Text = quantity.ToString(),
                FontSize = 14,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Center,
            }, 3, 0);

            productGrid.Children.Add(new Label
            {
                Text = product.Price.ToString("N2"),
                FontSize = 14,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.End,
            }, 4, 0);

            productDetailsStack.Children.Add(productGrid);
        }
    }
}