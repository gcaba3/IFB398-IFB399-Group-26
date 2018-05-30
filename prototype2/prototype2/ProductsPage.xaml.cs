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
    public partial class ProductsPage : ContentPage
    {
        private EditQuotePopup editQuotePopup;
        private FilterGridProducts filterGridProducts;
        private String addToQuoteText = " Add to quote ";
        private String addedToQuoteText = " Added to quote ";
        private string hyperLinkColor = "#0000EE";
        private Boolean popupIsNew;
        private Product selectedProduct;
        private Button btnAddToQuote;
        private Label labelAddToQuote;
        private List<Product> suggestedProducts;
        //private List<string> categorySuggestions;
        private const String imageUnfavourited = "heart1.png";
        private const String imageFavourited = "heart2.png";

        private string filterCategory;
        private const string categoryAll = "All";

        private SearchGrid gridSearchGrid;
        private SearchSuggestionsStackProductsPage stackSearch;

        public ProductsPage()
        {
            InitializePage();
        }

        public ProductsPage(List<int> searchSuggestions)
        {
            InitializePage();

            Searched_SearchBarProducts(searchSuggestions);
        }

        public ProductsPage(Product searchedSuggestion)
        {
            InitializePage();

            Clicked_BtnSuggestion(searchedSuggestion);
        }

        public ProductsPage(string searchedCategory)
        {
            InitializePage();

            Clicked_BtnCategory(searchedCategory);
        }

        public void InitializePage()
        {
            InitializeComponent();

            suggestedProducts = new List<Product>();
            filterCategory = categoryAll;

            LayoutPage();
            SetUpSearchControls();

            SetUpEditQuotePopup();
            SetUpFilterGridPopup();

            gridPage.Children.Add(editQuotePopup);
            gridPage.Children.Add(filterGridProducts);

            popupIsNew = true;

            NavigationPage.SetHasBackButton(this, false);
        }

        protected virtual void LayoutPage()
        {
            Title = "Products";
            NavigationBar.ChangeProductsTabColor();            

            AddAllProductsToStack();

            /*
             * Title = "Products";
            NavigationBar.ChangeProductsTabColor();

            StackLayout stackSearchFeature = new StackLayout();

            SearchSuggestionsStack searchSuggestionsStack = new SearchSuggestionsStack();
            gridSearchGrid = new SearchGrid(searchSuggestionsStack);

            stackSearchFeature.Children.Add(gridSearchGrid);
            stackSearchFeature.Children.Add(searchSuggestionsStack);

            gridPageContent.Children.Add(stackSearchFeature, 0, 0);

            AddAllProductsToStack();*/
        }

        private void SetUpSearchControls()
        {
            stackSearch = new SearchSuggestionsStackProductsPage(this);
            gridSearchArea.Children.Add(stackSearch);

            stackPageContent.Children.Insert(0, new SearchBarProductsPage(stackSearch, boxViewBackground, this));

            gridSearchGrid = new SearchGrid();
            gridPageContent.Children.Add(gridSearchGrid, 0, 0);
        }

        public string FilterCategory() => filterCategory;
        public string CategoryAll() => categoryAll;

        private void AddAllProductsToStack()
        {
            if (filterCategory == categoryAll)
            {
                for (int i = 0; i < Data.products.Count; i++)
                {
                    AddProductToStack(Data.products[i]);
                }
            }
            else
            {
                for (int i = 0; i < Data.products.Count; i++)
                {
                    if (filterCategory == Data.products[i].Category)
                        AddProductToStack(Data.products[i]);
                }
            }            
        }

        private void AddAllProductSuggestionsToStack()
        {
            if (filterCategory == categoryAll)
            {
                for (int i = 0; i < Data.products.Count; i++)
                {
                    if (suggestedProducts.Count < 1)
                    {
                            AddProductToStack(Data.products[i]);
                    }
                    else if (suggestedProducts.Contains(Data.products[i]))
                    {
                        AddProductToStack(Data.products[i]);
                    }
                }
            }
            else
            {
                for (int i = 0; i < Data.products.Count; i++)
                {
                    if (filterCategory == Data.products[i].Category)
                    {
                        if (suggestedProducts.Count < 1)
                        {
                            AddProductToStack(Data.products[i]);
                        }
                        else if (suggestedProducts.Contains(Data.products[i]))
                        {
                            AddProductToStack(Data.products[i]);
                        }
                    }
                }
            }
            
        }
        private void SetUpEditQuotePopup()
        {
            editQuotePopup = new EditQuotePopup();
            editQuotePopup.OnCancelButtonClicked += OnCancelButtonClickedEvent;
            editQuotePopup.OnRemoveButtonClicked += OnRemoveButtonClickedEvent;
            editQuotePopup.OnConfirmButtonClicked += OnConfirmButtonClickedEvent;
            editQuotePopup.newProduct = true;
        }

        private void SetUpFilterGridPopup()
        {
            filterGridProducts = new FilterGridProducts();
            filterGridProducts.OnBtnApplyClicked += OnBtnApplyFilterClickedEvent;
        }

        /// <summary>
        /// Creates a generic example product StackLayout and adds it to the productLayout StackLayout
        /// </summary>
        protected void AddProductToStack(Product product)
        {
            Grid productGrid = CreateProductGrid();
            AddLabelsToProductGrid(productGrid, product);
            AddFavouriteButtonToGrid(productGrid, product);
            AddAddToQuoteButtonToGrid(productGrid, product);

            StackLayout productLayout = new StackLayout();

            BoxView separatorLine = new BoxView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = (Color)App.Current.Resources["SPBlue"],
                HeightRequest = 1,
                InputTransparent = true
            };

            productLayout.Children.Add(separatorLine);

            productLayout.Children.Add(productGrid);

            AddProductImageToGrid(productGrid, product);

            stackProducts.Children.Add(productLayout);
        }
        private Grid CreateProductGrid()
        {
            var productGrid = new Grid
            {
                RowDefinitions =
                {
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
                    new RowDefinition { Height = new GridLength(1, GridUnitType.Auto) },
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
                Text = product.Description,
                FontSize = 14,
                VerticalOptions = LayoutOptions.Center,
            }, 0, 3, 0, 1);

            productGrid.Children.Add(new Label
            {
                Text = product.Manufacturer,
                FontSize = 14,
                VerticalOptions = LayoutOptions.Center,
            }, 1, 1);
            
            productGrid.Children.Add(new Label
            {
                Text = GetProductStockText(product.Stock),
                FontSize = 14,
                VerticalOptions = LayoutOptions.Center,
            }, 1, 2);

            productGrid.Children.Add(new Label
            {
                Text = "$" + product.Price.ToString("N2") + " per unit",
                FontSize = 12,
                VerticalOptions = LayoutOptions.Center,
                FontAttributes = FontAttributes.Bold
            }, 1, 3);

            StackLayout stackDataSheet = new StackLayout
            {
                Spacing = 0,
                Padding = 0
            };

            Label labelDataSheet = new Label
            {
                BackgroundColor = Color.Transparent,
                TextColor = Color.Blue,
                Text = "Data Sheet",
                FontSize = 12,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                LineBreakMode = LineBreakMode.TailTruncation
            };

            labelDataSheet.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => OpenPdf()),
            });

            stackDataSheet.Children.Add(labelDataSheet);
            stackDataSheet.Children.Add(new BoxView
            {
                BackgroundColor = Color.FromHex(hyperLinkColor),
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 1,
            });

            StackLayout stackInstallGuide = new StackLayout
            {
                Spacing = 0,
                Padding = 0
            };

            Label labelInstallGuide = new Label
            {
                BackgroundColor = Color.Transparent,
                TextColor = Color.Blue,
                Text = "Install Guide",
                FontSize = 12,
                LineBreakMode = LineBreakMode.TailTruncation
            };

            labelInstallGuide.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => OpenPdf()),
            });

            stackInstallGuide.Children.Add(labelInstallGuide);
            stackInstallGuide.Children.Add(new BoxView
            {
                BackgroundColor = Color.FromHex(hyperLinkColor),
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 1,
            });

            StackLayout stackWarranty = new StackLayout
            {
                Spacing = 0,
                Padding = 0
            };

            Label labelWarranty = new Label
            {                
                Text = "Warranty",
                FontSize = 12,
                BackgroundColor = Color.Transparent,
                TextColor = Color.FromHex(hyperLinkColor),
                LineBreakMode = LineBreakMode.TailTruncation,
                VerticalOptions = LayoutOptions.End,
                VerticalTextAlignment = TextAlignment.End
            };

            labelWarranty.GestureRecognizers.Add(new TapGestureRecognizer
            {
                Command = new Command(() => OpenPdf()),
            });

            stackWarranty.Children.Add(labelWarranty);
            stackWarranty.Children.Add(new BoxView
            {
                BackgroundColor = Color.FromHex(hyperLinkColor),
                VerticalOptions = LayoutOptions.Start,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 1,
            });

            Grid gridDocumentation = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) },
                    new ColumnDefinition { Width = new GridLength(1, GridUnitType.Auto) }
                },
            };

            gridDocumentation.Children.Add(stackDataSheet, 0, 0);
            gridDocumentation.Children.Add(stackInstallGuide, 1, 0);
            gridDocumentation.Children.Add(stackWarranty, 2, 0);

            productGrid.Children.Add(gridDocumentation, 1, 3, 4, 5);

            /*productGrid.Children.Add(new Label
            {
                Text = "Spec sheet, Install Guide, Brochure",
                FontSize = 12,
                VerticalOptions = LayoutOptions.FillAndExpand,
            }, 1, 4);*/
        }


        private string GetProductStockText(int numStock)
        {
            string returnString = "In Stock: ";
            if (numStock > 10)
                returnString += "Limited";
            else
                returnString += numStock;
            return returnString;
        }

        private void AddAddToQuoteButtonToGrid(Grid productGrid, Product product)
        {
            Grid gridAddToQuote = new Grid
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                Margin = new Thickness(0,0,4,0)
            };

            Label labelAddToQuote = new Label
            {
                FontSize = 12,
                Margin = 4,
                BackgroundColor = Color.Transparent,
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HorizontalTextAlignment = TextAlignment.Center,
                VerticalTextAlignment = TextAlignment.Center,
                InputTransparent = true,
            };

            Button btnAddToQuote = new Button
            {
                TextColor = Color.Transparent,
                BorderWidth = 3,
                BorderColor = (Color)App.Current.Resources["SPBlue"],
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
                HeightRequest = 10,
            };

            if (!Data.newQuote.Products.ContainsKey(product))
            {
                labelAddToQuote.TextColor = Color.Black;
                labelAddToQuote.Text = addToQuoteText;

                btnAddToQuote.Text = addToQuoteText;
                btnAddToQuote.BackgroundColor = Color.White;
            }
            else
            {
                labelAddToQuote.Text = addedToQuoteText;
                labelAddToQuote.TextColor = Color.White;

                btnAddToQuote.BackgroundColor = (Color)App.Current.Resources["SPBlue"];
                btnAddToQuote.Text = addedToQuoteText;
            }

            btnAddToQuote.ClassId = product.Id.ToString();

            btnAddToQuote.Command = new Command(o => BtnAddToQuoteClicked(btnAddToQuote, labelAddToQuote));
            
            gridAddToQuote.Children.Add(btnAddToQuote);
            gridAddToQuote.Children.Add(labelAddToQuote);

            productGrid.Children.Add(gridAddToQuote, 2, 3, 2, 4);
        }

        private void BtnAddToQuoteClicked(Button button, Label label)
        {
            btnAddToQuote = button;
            labelAddToQuote = label;
            PrepareEditQuotePopup();
            editQuotePopup.ToggleAddToQuotePopup();
        }

        private void OnCancelButtonClickedEvent()
        {
            editQuotePopup.ToggleAddToQuotePopup();
        }

        private void OnRemoveButtonClickedEvent(string productId)
        {
            Data.RemoveFromQuote(productId);
            ToggleAddToQuoteText();
            editQuotePopup.ToggleAddToQuotePopup();
        }

        private void OnConfirmButtonClickedEvent(string productId)
        {
            Data.EditQuoteProductQuantity(productId, Int32.Parse(editQuotePopup.EntryQuantityText));            
            if (Int32.Parse(editQuotePopup.EntryQuantityText) == 0)
            {
                ToggleAddToQuoteText();
            }
            else if (labelAddToQuote.Text == addToQuoteText) ToggleAddToQuoteText();
            editQuotePopup.ToggleAddToQuotePopup();
        }

        private void OnBtnApplyFilterClickedEvent(string category)
        {
            filterCategory = category;
            RefreshProductsStack();
        }

        private void RefreshProductsStack()
        {
            stackProducts.Children.Clear();
            AddAllProductSuggestionsToStack();
        }

        private void AddProductImageToGrid(Grid productGrid, Product product)
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

            productGrid.Children.Add(imageGrid, 0, 1, 1, 5);
            //Problem - If image is larger than productGrid height, it will scale down correctly 
            //but will generate lots of whitespace beneath the gid depending on how much it was scaled down
        }

        private void AddFavouriteButtonToGrid(Grid productGrid, Product product)
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


            productGrid.Children.Add(btnFavourite, 2, 3, 0, 2);
        }

        protected virtual void Clicked_btnFavourite(object sender, EventArgs eventArgs)
        {
            Button button = (Button)sender;
            if (button.Image == imageUnfavourited) AddFavourite(button);
            else RemoveFavourite(button);
            AnimateFavouriteButton(button);
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

        protected async void AnimateFavouriteButton(Button button)
        {
            await button.ScaleTo(1.2, 50, Easing.CubicIn);
            await button.ScaleTo(1, 50, Easing.CubicIn);
        }        

        /// <summary>
        /// Edits the AddToQuote popup to have the correct labels, entry and button states
        /// corresponding to the product and the user's new quote
        /// </summary>
        /// <param name="btnAddToQuote"></param>
        private void PrepareEditQuotePopup()
        {
            selectedProduct = Data.GetProductFromId(btnAddToQuote.ClassId);
            editQuotePopup.SetLabelProductName(selectedProduct.Description);
            editQuotePopup.SetBtnConfirmClassId(btnAddToQuote.ClassId);
            if (labelAddToQuote.Text == addToQuoteText)       // if this product hasn't been added to the quote yet and the "add to quote popup"
            {                                               // isn't in a fresh state, refresh the popup's state
                if (!popupIsNew)
                {
                    editQuotePopup.newProduct = true;
                    popupIsNew = true;
                    editQuotePopup.SetEditQuotePopupNew();      // otherwise if the popup is in a fresh state but the user has this product added to quote already
                }
                editQuotePopup.EntryQuantityText = "";
            }                                               // change the popup to its editor state
            else if (popupIsNew)
            {
                string productQuantity = Data.newQuote.Products[selectedProduct].ToString();
                popupIsNew = false;
                editQuotePopup.newProduct = false;
                editQuotePopup.SetEditQuotePopupChange(productQuantity);
            }
        }        

        public void ToggleAddToQuoteText()
        {
            if (labelAddToQuote.Text == addToQuoteText)
            {
                labelAddToQuote.Text = addedToQuoteText;
                labelAddToQuote.TextColor = Color.White;
                btnAddToQuote.BackgroundColor = (Color)App.Current.Resources["SPBlue"];
            }
            else
            {
                labelAddToQuote.Text = addToQuoteText;
                labelAddToQuote.TextColor = Color.Black;
                btnAddToQuote.BackgroundColor = Color.Transparent;
            }
        }     
        
        private async void AnimateBackgroundToVisible()
        {
            boxViewBackground.IsVisible = true;

            await boxViewBackground.FadeTo(0.5, 250, Easing.Linear);            
        }

        private async void AnimateBackgroundToInvisible()
        {
            await boxViewBackground.FadeTo(0, 250, Easing.Linear);

            boxViewBackground.IsVisible = false;
        }

        private void Clicked_BtnClearSearch( object sender, EventArgs eventArgs)
        {
            stackProducts.Children.Clear();
            suggestedProducts.Clear();
            AddAllProductsToStack();
            btnClearSearch.IsVisible = false;
        }

        public void Searched_SearchBarProducts(List<int> suggestionIds)
        {
            suggestedProducts.Clear();
            stackProducts.Children.Clear();
            if (suggestionIds.Count < 1)
                DisplayNoResults();
            else
            {
                foreach (var suggestion in suggestionIds)
                {
                    Product product = Data.GetProductFromId(suggestion.ToString());

                    suggestedProducts.Add(product);
                    AddProductToStack(product);
                }
            }
            
            btnClearSearch.IsVisible = true;
            stackSearch.IsVisible = false;
            AnimateBackgroundToInvisible();
        }

        private void DisplayNoResults()
        {
            Label resultsLabel1 = new Label
            {
                Text = "No results were found...",
                FontSize = 16,
                TextColor = Color.Black,
                HorizontalTextAlignment = TextAlignment.Center
            };

            Label resultsLabel2 = new Label
            {
                Text = "Try changing your filters",
                HorizontalTextAlignment = TextAlignment.Center
            };

            stackProducts.Children.Add(resultsLabel1);
            stackProducts.Children.Add(resultsLabel2);
        }

        private async void Notifications_Clicked(object sender, EventArgs eventArgs)
        {
            NotificationPage notificationPage = new NotificationPage();
            await Navigation.PushAsync(notificationPage);
        }

        private void OnBoxViewGestureRecognizerTapped(object sender, EventArgs eventArgs)
        {
            stackSearch.ClearSearch();
            
            AnimateBackgroundToInvisible();
        }

        public void Clicked_BtnSuggestion(Product product)
        {
            suggestedProducts.Clear();
            stackProducts.Children.Clear();

            suggestedProducts.Add(product);
            AddProductToStack(product);
            stackSearch.IsVisible = false;
            btnClearSearch.IsVisible = true;

            AnimateBackgroundToInvisible();
        }

        public void Clicked_BtnCategory(string category)
        {
            stackProducts.Children.Clear();
            foreach (Product product in Data.products)
            {
                if (product.Category == category)
                {
                    AddProductToStack(product);
                }
            }
            stackSearch.IsVisible = false;
            btnClearSearch.IsVisible = true;

            AnimateBackgroundToInvisible();
        }

        private void Clicked_BtnSearchTool(object sender, EventArgs eventArgs)
        {
            gridSearchGrid.Clicked_ButtonSearchTool();
        }

        private void Clicked_BtnFilters(object sender, EventArgs eventArgs)
        {
            filterGridProducts.ToggleVisibility();
        }

        private void OpenPdf()
        {
            Navigation.PushAsync(new PDFViewerPage("PVI-3.0-3.6-4.2_BCD.00374_EN.pdf"));
        }
    }
}