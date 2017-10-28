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
        public Products()
        {
            InitializeComponent();
            Title = "Products";
            NavigationPage.SetHasBackButton(this, false);

            for (int i = 0; i < 12; i++)
            {
                AddProductToStack();
            }
        }

        /// <summary>
        /// Creates a generic example product StackLayout and adds it to the productLayout StackLayout
        /// </summary>
        public void AddProductToStack()
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

            productGrid.Children.Add(new Label
            {
                Text = "Product name, code, small description ",
                FontSize = 14,
                VerticalOptions = LayoutOptions.Center,
            }, 1, 0);

            productGrid.Children.Add(new Label
            {
                Text = "Available: ",
                FontSize = 14,
                VerticalOptions = LayoutOptions.Center,
            }, 1, 1);

            productGrid.Children.Add(new Label
            {
                Text = "Price $$$$",
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

            Button btnFavourite = new Button
            {
                Image = "heart1",
                HorizontalOptions = LayoutOptions.End,
                VerticalOptions = LayoutOptions.Start,
                BackgroundColor = Color.Transparent,
                BorderColor = Color.Transparent
            };
            btnFavourite.Clicked += Clicked_btnFavourite;

            productGrid.Children.Add(btnFavourite, 2, 0);

            Button btnAddToQuote = new Button
            {
                BackgroundColor = Color.SkyBlue,
                TextColor = Color.Black,
                BorderColor = Color.Black,
                FontSize = 14,
                BorderWidth = 3,
                Text = "Add to quote",
            };

            btnAddToQuote.Clicked += Clicked_btnAddToQuote;

            productGrid.Children.Add(btnAddToQuote, 2, 3);

            StackLayout productLayout = new StackLayout();
            productLayout.Children.Add(productGrid);

            var imageGrid = new Grid
            {
            };

            imageGrid.Children.Add(new Image
            {
                Source = "solarpanelexample",
                HorizontalOptions = LayoutOptions.FillAndExpand,
                VerticalOptions = LayoutOptions.FillAndExpand,
            }, 0, 0);

            productGrid.Children.Add(imageGrid, 0, 1, 0, 4);
            //Problem - If image is larger than productGrid height, it will scale down correctly 
            //but will generate lots of whitespace beneath the gid depending on how much it was scaled down

            productStack.Children.Add(productLayout);
        }

        public void Clicked_btnFavourite(object sender, EventArgs eventaArgs)
        {
            Button button = (Button)sender;
            button.Image = (button.Image == "heart1.png") ? "heart2.png" : "heart1.png";
        }

        async void Clicked_btnAddToQuote(object sender, EventArgs args)
        {
            //await App.Current.MainPage.DisplayAlert("No", "I don't want to.", "damn it");
        }

    }
}