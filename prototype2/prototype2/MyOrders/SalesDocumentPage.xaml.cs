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
    public abstract partial class SalesDocumentPage : ContentPage
    {
        protected SalesDocument document;
        public SalesDocumentPage(SalesDocument document)
        {
            InitializeComponent();

            NavigationBar.ChangeMyOrdersTabColor();

            this.document = document;

            DisplayData();
        }

        protected virtual void DisplayData()
        {
            labelStatus.Text = document.Status;

            EditLabelsText();

            AddDocumentControls();
            
            foreach (KeyValuePair<Product, int> product in document.Products)
            {
                AddProductToStack(product.Key, product.Value);
            }

            AddFreightToStack();

            UpdateDateLabel();

            UpdatePriceLabels();            
        }

        protected virtual void UpdateDateLabel()
        {
            labelDate.Text = document.Date.ToString("dd/MM/yyyy");
        }

        protected void AddLabelsToProductGrid(Grid grid, Product product, int quantity)
        {
            grid.Children.Add(CreateProductDescriptionLabel(product.Description), 1, 0);

            grid.Children.Add(CreateProductQuantityLabel(quantity.ToString()), 2, 0);

            grid.Children.Add(CreateProductPriceLabel(product.Price.ToString("N2")), 3, 0);

            double netPrice = product.Price * quantity;

            grid.Children.Add(CreateProductPriceLabel(netPrice.ToString("N2")), 4, 0);
        }

        private Label CreateProductDescriptionLabel(string productDescription)
        {
            Label labelProductName = new Label
            {
                Text = productDescription,
                FontSize = 14,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
                HorizontalTextAlignment = TextAlignment.Start,                
            };

            return labelProductName;
        }

        private Label CreateProductQuantityLabel(string quantity)
        {
            Label labelProductQuantity = new Label
            {
                Text = quantity.ToString(),
                FontSize = 14,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.End,
                HorizontalTextAlignment = TextAlignment.End,
            };

            return labelProductQuantity;
        }

        private Label CreateProductPriceLabel(string productPrice)
        {
            Label labelProductPrice = new Label
            {
                Text = productPrice,
                FontSize = 14,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.EndAndExpand,
                HorizontalTextAlignment = TextAlignment.End,
            };
            return labelProductPrice;
        }

        protected abstract void EditLabelsText();

        protected abstract void AddDocumentControls();

        protected virtual void AddProductToStack(Product addedProduct, int quantity)
        {
            var productGrid = new Grid
            {
                ColumnDefinitions = gridProductDetailsHeadings.ColumnDefinitions,
            };

            AddLabelsToProductGrid(productGrid, addedProduct, quantity);

            productDetailsStack.Children.Add(productGrid);
        }

        protected void AddFreightToStack()
        {
            var grid = new Grid
            {
                ColumnDefinitions = gridProductDetailsHeadings.ColumnDefinitions,
            };

            grid.Children.Add(CreateProductDescriptionLabel("Freight"), 1, 0);

            grid.Children.Add(CreateProductPriceLabel("0.00"), 4, 0);

            productDetailsStack.Children.Add(grid);
        }

        protected void UpdatePriceLabels()
        {
            double subtotal = 0;

            foreach (KeyValuePair<Product, int> product in document.Products)
            {
                subtotal += product.Key.Price * product.Value;
            }

            double gst = Math.Round(subtotal * 0.1, 2, MidpointRounding.AwayFromZero);
            
            txtExclGST.Text = subtotal.ToString("N2");
            txtGST.Text = gst.ToString("N2");
            document.TotalPrice = subtotal + gst;
            txtTotal.Text = document.TotalPrice.ToString("N2");
        }

        protected async void PopPageReturnToMyOrders()
        {
            Navigation.RemovePage(Navigation.NavigationStack[Navigation.NavigationStack.Count - 2]);
            Navigation.InsertPageBefore(new AccountPage(NavigationBar.Tab.MyOrders), this);
            await Navigation.PopAsync();
        }

        protected void AddViewQuotePDFButton()
        {
            Button button = new Button
            {
                Text = "View Full Quote PDF",
                BackgroundColor = Color.White,
                BorderColor = (Color)App.Current.Resources["SPBlue"],
                BorderWidth = 3,
                TextColor = (Color)App.Current.Resources["SPBlue"]
            };

            button.Clicked += Clicked_btnViewPDF;

            stackDocumentControls.Children.Add(button);
        }

        private void Clicked_btnViewPDF(object sender, EventArgs eventArgs)
        {
            Navigation.PushAsync(new PDFViewerPage("SO010_draft.pdf"));
        }

    }
}