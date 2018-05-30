using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace prototype2
{
    public partial class QuoteIncompletePage : QuotePage
    {
        private ColumnDefinitionCollection productDetailsColumnDefinitions;
        private EditQuotePopup editQuotePopup;
        private MyOrders.OrderAddressView billingAddressView, deliveryAddressView;
        public QuoteIncompletePage(Quote quote) : base(quote)
        {
            AddBillingAddressOptions();
            AddDeliveryAddressOptions();

            SetUpEditQuotePopup();
        }

        private void AddBillingAddressOptions()
        {
            billingAddressView = new MyOrders.OrderAddressView(scrollviewPage, Data.billingAddresses, MyOrders.AddressType.BillingAddress, "Billing Address");
            billingAddressView.DisplayAlertInvalidAddress += DisplayAlertInvalidAddressEvent;
            stackDocumentControls.Children.Add(billingAddressView);
        }

        private void AddDeliveryAddressOptions()
        {
            deliveryAddressView = new MyOrders.OrderAddressView(scrollviewPage, Data.deliveryAddresses, MyOrders.AddressType.DeliveryAddress, "Delivery Address");
            stackDocumentControls.Children.Add(deliveryAddressView);
        }

        private void SetUpEditQuotePopup()
        {
            editQuotePopup = new EditQuotePopup();

            editQuotePopup.SetEditQuotePopupChange("");
            salesDocumentPageGrid.Children.Add(editQuotePopup, 0, 0);
            
            editQuotePopup.OnCancelButtonClicked += OnCancelButtonClickedEvent;
            editQuotePopup.OnRemoveButtonClicked += OnRemoveButtonClickedEvent;
            editQuotePopup.OnConfirmButtonClicked += OnConfirmButtonClickedEvent;

        }

        protected override void EditLabelsText()
        {
            ChangeColumnDefinitions();

            labelDateHeading.Text = "Last Edited:";
            Label labelEditHeading = new Label
            {
                Text = "Edit",
                HorizontalOptions = LayoutOptions.CenterAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
            };
            gridProductDetailsHeadings.Children.Add(labelEditHeading, 5, 0);
        }

        private void ChangeColumnDefinitions()
        {
            productDetailsColumnDefinitions = new ColumnDefinitionCollection
            {
                new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(40, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(16, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(14, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(14, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(10, GridUnitType.Star) },
                new ColumnDefinition { Width = new GridLength(3, GridUnitType.Star) },
            };

            gridProductDetailsHeadings.ColumnDefinitions = productDetailsColumnDefinitions;
        }

        protected override void AddDocumentControls()
        {
            AddDeleteButton();
            AddValidateButton();
        }

        protected override void AddProductToStack(Product addedProduct, int quantity)
        {
            var productGrid = new Grid
            {
                ColumnDefinitions = productDetailsColumnDefinitions,
                ClassId = addedProduct.Id.ToString(),
            };

            AddLabelsToProductGrid(productGrid, addedProduct, quantity);

            productGrid.Children.Add(GetProductEditButton(addedProduct.Id.ToString()), 5, 0);

            productDetailsStack.Children.Add(productGrid);
        }

        private Button GetProductEditButton(string productId)
        {
            Button btnProductEdit = new Button
            {
                Image = "edit.png",
                BackgroundColor = Color.White,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.End,
            };

            btnProductEdit.ClassId = productId;

            btnProductEdit.Clicked += Clicked_btnProductEdit;

            return btnProductEdit;
        }

        private void Clicked_btnProductEdit(object sender, EventArgs eventArgs)
        {
            Button button = (Button)sender;
            Product product = Data.GetProductFromId(button.ClassId);
            PrepareEditQuotePopup(product);
            editQuotePopup.ToggleAddToQuotePopup();
        }

        private void PrepareEditQuotePopup(Product product)
        {
            editQuotePopup.SetLabelProductName(product.Description);
            editQuotePopup.SetBtnConfirmClassId(product.Id.ToString());
            editQuotePopup.EntryQuantityText = Data.newQuote.Products[product].ToString();
        }

        private void OnCancelButtonClickedEvent()
        {
            editQuotePopup.ToggleAddToQuotePopup();
        }

        private void OnRemoveButtonClickedEvent(string productId)
        {
            Data.RemoveFromQuote(productId);

            if (document.Status == QuoteStatus.Empty)
                DeleteQuote();
            else
            {
                RemoveProductFromQuoteStack(productId);
                editQuotePopup.ToggleAddToQuotePopup();
                UpdatePriceLabels();
                UpdateDateLabel();
            }            
        }

        private void DeleteQuote()
        {
            Data.CreateFreshQuote();
            PopPageReturnToMyOrders();
        }

        protected override void UpdateDateLabel()
        {
            labelDate.Text = document.Date.ToString("dd/MM/yyyy hh:mm:ss tt");
        }

        private void RemoveProductFromQuoteStack(string productId)
        {
            View view = productDetailsStack.Children.First(x => x.ClassId == productId);
            productDetailsStack.Children.Remove(view);
        }

        private void OnConfirmButtonClickedEvent(string productId)
        {
            int quantity = Int32.Parse(editQuotePopup.EntryQuantityText);
            Product product = Data.GetProductFromId(productId);

            if (quantity != document.Products[product])

                if (document.Status == QuoteStatus.Empty)
                DeleteQuote();

                else
                {
                    Data.EditQuoteProductQuantity(productId, quantity);

                    Grid grid = (Grid)productDetailsStack.Children.First(x => x.ClassId == productId);

                    if (quantity < 1)
                        productDetailsStack.Children.Remove(grid);

                    else
                        ((Label)grid.Children.ElementAt(1)).Text = editQuotePopup.EntryQuantityText;

                    UpdateDateLabel();
                    UpdateProductNetPrice(product, quantity, (Label)grid.Children.ElementAt(3));
                    UpdatePriceLabels();
                }

            editQuotePopup.ToggleAddToQuotePopup();
        }

        private void UpdateProductNetPrice(Product product, int quantity, Label netPriceLabel)
        {
            netPriceLabel.Text = (product.Price * quantity).ToString("N2");
        }

        private void AddDeleteButton()
        {
            Button button = new Button
            {
                Text = "Delete",
                BackgroundColor = (Color)App.Current.Resources["SPRed"],
                TextColor = Color.White
            };

            button.Clicked += Clicked_btnDelete;

            stackDocumentControls.Children.Add(button);
        }

        private void Clicked_btnDelete(object sender, EventArgs eventArgs)
        {
            Data.CreateFreshQuote();
            PopPageReturnToMyOrders();
        }
        
        private void AddValidateButton()
        {
            Button button = new Button
            {
                Text = "Request Validation",
                BackgroundColor = (Color)App.Current.Resources["SPBlue"],
                TextColor = Color.White,
            };

            button.Clicked += Clicked_btnValidate;

            stackDocumentControls.Children.Add(button);
        }

        private void Clicked_btnValidate(object sender, EventArgs eventArgs)
        {
            ValidateAddresses();            
        }

        private void ValidateAddresses()
        {
            if (billingAddressView.CheckBoxChecked() && deliveryAddressView.CheckBoxChecked())
            {
                AddNewQuoteToList();
                PopPageReturnToMyOrders();
            }
            else
            {
                DisplayAlertNotConfirmed();
            }
        }

        private async void DisplayAlertNotConfirmed()
        {
            await DisplayAlert("", "Please check both the billing and delivery address checkboxes first", "OK");
        }

        private async void DisplayAlertInvalidAddressEvent()
        {
            await DisplayAlert("Invalid Address", "No address field can be blank", "OK");
        }

        private void AddNewQuoteToList()
        {
            Data.numSalesOrders++;
            document.Date = DateTime.Now;
            document.Status = QuoteStatus.PendingResponse;
            document.Number = "Q" + Data.numSalesOrders.ToString("D5");
            Data.quotes.Add((Quote)document);
            Data.CreateFreshQuote();
        }
    }
}
