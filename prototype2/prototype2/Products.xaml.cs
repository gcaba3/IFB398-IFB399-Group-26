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
    public partial class Products : ContentPage
    {
        private String addToQuoteText = "Add to quote";
        private String addedToQuoteText = "Added to quote";
        private Boolean popupIsNew;
        private Product selectedProduct;
        private Button btnAddToQuote;
        private const String imageUnfavourited = "heart1.png";
        private const String imageFavourited = "heart2.png";

        public Products()
        {
            InitializeComponent();

            NavigationPage.SetHasBackButton(this, false);

            LayoutPage();

            popupIsNew = true;
        }

        protected virtual void LayoutPage()
        {
            Title = "Products";

            for (int i = 0; i < Data.products.Count; i++)
            {
                AddProductToStack(Data.products[i]);
            }
        }

        /// <summary>
        /// Creates a generic example product StackLayout and adds it to the productLayout StackLayout
        /// </summary>
        protected void AddProductToStack(Product product)
        {
            Grid productGrid = CreateProductGrid();
            AddLabelsToProductGrid(productGrid, product);
            AddFavouriteButton(productGrid, product);
            AddQuoteButton(productGrid, product);

            StackLayout productLayout = new StackLayout();

            BoxView separatorLine = new BoxView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = (Color)App.Current.Resources["SPBlue"],
                HeightRequest = 1
            };

            productLayout.Children.Add(separatorLine);

            productLayout.Children.Add(productGrid);

            AddProductImage(productGrid, product);

            stackProducts.Children.Add(productLayout);
        }
        private Grid CreateProductGrid()
        {
            var productGrid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(30, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(20, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(15, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(35, GridUnitType.Auto) }
                },
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(35, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(40, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(25, GridUnitType.Star) }
                },
            };

            return productGrid;
        }

        private void AddLabelsToProductGrid(Grid productGrid, Product product)
        {
            productGrid.Children.Add(new Label
            {
                Text = product.Name + "\n" + product.Manufacturer,
                FontSize = 14,
                VerticalOptions = LayoutOptions.Center,
            }, 1, 0);

            productGrid.Children.Add(new Label
            {
                Text = "In Stock: " + product.Stock,
                FontSize = 14,
                VerticalOptions = LayoutOptions.Center,
            }, 1, 1);

            productGrid.Children.Add(new Label
            {
                Text = "$" + product.Price + " per unit",
                FontSize = 16,
                VerticalOptions = LayoutOptions.Center,
                HorizontalOptions = LayoutOptions.Start,
            }, 1, 2);

            productGrid.Children.Add(new Label
            {
                Text = "Spec sheet, Install Guide, Brochure",
                FontSize = 12,
                VerticalOptions = LayoutOptions.Center,
            }, 1, 3);
        }

        private void AddQuoteButton(Grid productGrid, Product product)
        {
            Button btnAddToQuote = new Button
            {
                FontSize = 12,
                BorderWidth = 3,
            };

            btnAddToQuote.BorderColor = (Color)App.Current.Resources["SPBlue"];

            if (!Data.newQuote.Products.ContainsKey(product))
            {
                btnAddToQuote.BackgroundColor = Color.Transparent;
                btnAddToQuote.Text = addToQuoteText;
                btnAddToQuote.TextColor = Color.Black;
            }
            else
            {
                btnAddToQuote.Text = addedToQuoteText;
                btnAddToQuote.TextColor = Color.White;
                btnAddToQuote.BackgroundColor = (Color)App.Current.Resources["SPBlue"];
            }

            btnAddToQuote.ClassId = product.Id.ToString();

            btnAddToQuote.Clicked += Clicked_btnAddToQuote;

            productGrid.Children.Add(btnAddToQuote, 2, 3);
        }

        private void AddProductImage(Grid productGrid, Product product)
        {
            var imageGrid = new Grid
            {
            };

            imageGrid.Children.Add(new Image
            {
                Source = product.ImagePath,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            }, 0, 0);

            productGrid.Children.Add(imageGrid, 0, 1, 0, 4);
            //Problem - If image is larger than productGrid height, it will scale down correctly 
            //but will generate lots of whitespace beneath the gid depending on how much it was scaled down
        }

        private void AddFavouriteButton(Grid productGrid, Product product)
        {
            Button btnFavourite = new Button
            {
                Image = imageUnfavourited,
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Start,
                BackgroundColor = Color.Transparent,
                BorderColor = Color.Transparent
            };

            if (Data.favourites.Contains(product)) btnFavourite.Image = imageFavourited;
            btnFavourite.ClassId = product.Id.ToString();

            btnFavourite.Clicked += Clicked_btnFavourite;


            productGrid.Children.Add(btnFavourite, 2, 0);
        }

        protected virtual void Clicked_btnFavourite(object sender, EventArgs eventArgs)
        {
            Button button = (Button)sender;
            if (button.Image == imageUnfavourited) AddFavourite(button);
            else RemoveFavourite(button);
        }

        private void AddFavourite(Button button)
        {
            Data.AddFavouriteToList(button.ClassId);
            button.Image = imageFavourited;
        }
        protected void RemoveFavourite(Button button)
        {
            Data.RemoveFavouriteFromList(button.ClassId);
            button.Image = imageUnfavourited;
        }

        private void Clicked_btnAddToQuote(object sender, EventArgs args)
        {
            btnAddToQuote = (Button)sender;
            PrepareAddToQuotePopup();
            ToggleAddToQuotePopup();
        }

        /// <summary>
        /// Edits the AddToQuote popup to have the correct labels, entry and button statess
        /// corresponding to the product and the user's new quote
        /// </summary>
        /// <param name="btnAddToQuote"></param>
        private void PrepareAddToQuotePopup()
        {
            selectedProduct = Data.GetProductFromId(btnAddToQuote.ClassId);
            labelPopupProductName.Text = selectedProduct.Name;
            btnConfirm.ClassId = btnAddToQuote.ClassId;
            if (btnAddToQuote.Text == addToQuoteText)       // if this product hasn't been added to the quote yet and the "add to quote popup"
            {                                               // isn't in a fresh state, refresh the popup's state
                if (!popupIsNew) AddToQuotePopupNew();      // otherwise if the popup is in a fresh state but the user has this product added to quote already
                entryQuantity.Text = "";
            }                                               // change the popup to its editor state
            else if (popupIsNew) AddToQuotePopupEdit();
        }

        private void AddToQuotePopupNew()
        {
            btnRemove.IsVisible = false;
            labelPopupType.Text = "Add to quote";
            popupIsNew = true;
        }

        private void AddToQuotePopupEdit()
        {
            btnRemove.IsVisible = true;
            labelPopupType.Text = "Change quantity";
            entryQuantity.Text = Data.newQuote.Products[selectedProduct].ToString();
            btnConfirm.IsEnabled = true;
            btnConfirm.BackgroundColor = (Color)App.Current.Resources["SPBlue"];
            popupIsNew = false;
        }

        private void Clicked_btnCancel(object sender, EventArgs eventArgs)
        {
            ToggleAddToQuotePopup();
        }

        private void Clicked_btnRemove(object sender, EventArgs eventArgs)
        {
            string productId = btnConfirm.ClassId;
            Data.RemoveFromQuote(productId);
            ToggleAddToQuoteText();
            ToggleAddToQuotePopup();
        }

        private void Clicked_btnConfirm(object sender, EventArgs eventArgs)
        {
            string productId = btnConfirm.ClassId;
            Data.EditQuoteProductQuantity(productId, Int32.Parse(entryQuantity.Text));
            if (Int32.Parse(entryQuantity.Text) == 0)
            {
                ToggleAddToQuoteText();
            }
            else if (btnAddToQuote.Text == addToQuoteText) ToggleAddToQuoteText();
            ToggleAddToQuotePopup();
        }

        private void ToggleAddToQuotePopup()
        {
            framePopup.IsVisible = !framePopup.IsVisible;

            boxBackground.IsVisible = !boxBackground.IsVisible;

            gridAddToQuotePopup.IsVisible = !gridAddToQuotePopup.IsVisible;
        }

        private void ToggleAddToQuoteText()
        {
            if (btnAddToQuote.Text == addToQuoteText)
            {
                btnAddToQuote.Text = addedToQuoteText;
                btnAddToQuote.TextColor = Color.White;
                btnAddToQuote.BackgroundColor = (Color)App.Current.Resources["SPBlue"];
            }
            else
            {
                btnAddToQuote.Text = addToQuoteText;
                btnAddToQuote.TextColor = (Color)App.Current.Resources["SPBlue"];
                btnAddToQuote.BackgroundColor = Color.Transparent;
            }
        }

        public bool IsInteger(string input)
        {
            return Int32.TryParse(input, out int output);
        }

        private void TextChanged_Quantity(object sender, TextChangedEventArgs args)
        {
            ValidateQuantity((Entry)sender);
        }

        private void ValidateQuantity(Entry quantity)
        {
            if (IsInteger(quantity.Text))
            {
                if (btnAddToQuote.Text == addToQuoteText)
                {
                    if (Int32.Parse(quantity.Text) > 0)
                    {
                        btnConfirm.IsEnabled = true;
                        btnConfirm.BackgroundColor = (Color)App.Current.Resources["SPBlue"];
                    }
                    else
                    {
                        btnConfirm.IsEnabled = false;
                        btnConfirm.BackgroundColor = Color.Default;
                    }
                }
                else
                {
                    btnConfirm.IsEnabled = true;
                    btnConfirm.BackgroundColor = (Color)App.Current.Resources["SPBlue"];
                }

            }
            else
            {
                btnConfirm.IsEnabled = false;
                btnConfirm.BackgroundColor = Color.Default;
            }
        }
    }
}