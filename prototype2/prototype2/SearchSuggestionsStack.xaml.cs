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
	public partial class SearchSuggestionsStack : StackLayout
	{
        private List<int> suggestionIds;
        private List<string> categorySuggestions;
        
        public SearchSuggestionsStack()
		{
			InitializeComponent ();

            suggestionIds = new List<int>();
            categorySuggestions = new List<string>();
		}

        public List<int> SuggestionIds => suggestionIds;

        public virtual List<string> FindCategories(string searchWord)
        {
            List<string> suggestedCategories = new List<string>();

            foreach (string category in Product.categories)
            {
                if (category.ToLower().Contains(searchWord))
                {
                    suggestedCategories.Add(category);
                }
            }

            return suggestedCategories;
        }
        public void RemoveNonSuggestedCategoriesFromStack(List<string> suggestions)
        {
            int count = 0;
            for (int i = 0; i < categorySuggestions.Count; i++)
            {
                if (!suggestions.Contains(categorySuggestions[i]))
                {
                    categorySuggestions.Remove(categorySuggestions[i]);
                    stackSearchCategories.Children.Remove(stackSearchCategories.Children[count]);

                    i--;
                }

                else
                    count++;
            }
        }

        public void AddSuggestedCategoriesToStack(List<string> suggestions)
        {
            foreach (string suggestion in suggestions)
            {
                if (!categorySuggestions.Contains(suggestion))
                {
                    categorySuggestions.Add(suggestion);
                    AddCategoryLabelToStack(suggestion);
                }
            }
        }

        public virtual Dictionary<int, Product> FindSuggestions(string searchWord)
        {
            Dictionary<int, Product> suggestions = new Dictionary<int, Product>();
            int numSuggestions = 0;
            int numProducts = 0;

            while (numSuggestions < 15 && numProducts < Data.products.Count)
            {
                if (Data.products[numProducts].Description.ToLower().Contains(searchWord))
                {
                    suggestions.Add(Data.products[numProducts].Id, Data.products[numProducts]);
                    numSuggestions++;
                }
                numProducts++;
            }

            return suggestions;
        }

        public void RemoveNonSuggestionsFromStack(Dictionary<int, Product> suggestions)
        {
            int count = 0;
            for (int i = 0; i < suggestionIds.Count; i++)
            {
                if (!suggestions.ContainsKey(suggestionIds[i]))
                {
                    suggestionIds.Remove(suggestionIds[i]);
                    stackSearchProducts.Children.Remove(stackSearchProducts.Children[count]);

                    i--;
                }

                else
                    count++;
            }
        }

        public void AddSuggestionsToStack(Dictionary<int, Product> suggestions)
        {
            foreach (KeyValuePair<int, Product> suggestion in suggestions)
            {
                if (!suggestionIds.Contains(suggestion.Key))
                {
                    suggestionIds.Add(suggestion.Key);
                    AddProductLabelToStack(suggestion);
                }
            }
        }

        private void AddCategoryLabelToStack(string category)
        {
            Grid gridSearch = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(5, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(67.5, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(22.5, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(5, GridUnitType.Star) }
                },

                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto }
                }
            };


            Button btnSuggestion = new Button
            {
                BackgroundColor = Color.White,
                ClassId = category,
            };

            btnSuggestion.Clicked += Clicked_BtnCategory;

            gridSearch.Children.Add(btnSuggestion, 0, 4, 1, 2);

            gridSearch.Children.Add(new Label
            {
                InputTransparent = true,
                Text = category,
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 12
            }, 1, 2, 1, 2);

            gridSearch.Children.Add(new Label
            {
                InputTransparent = true,
                Text = "Category",
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 12
            }, 2, 3, 1, 2);

            gridSearch.Children.Add(new BoxView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = (Color)App.Current.Resources["SPBlue"],
                HeightRequest = 1
            }, 0, 4, 0, 1);

            stackSearchCategories.Children.Add(gridSearch);
        }

        private void AddProductLabelToStack(KeyValuePair<int, Product> suggestion)
        {
            Grid gridSearch = new Grid
            {
                ColumnDefinitions =
                {
                    new ColumnDefinition { Width = new GridLength(5, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(67.5, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(22.5, GridUnitType.Star) },
                    new ColumnDefinition { Width = new GridLength(5, GridUnitType.Star) }
                },

                RowDefinitions =
                {
                    new RowDefinition { Height = GridLength.Auto },
                    new RowDefinition { Height = GridLength.Auto }
                }
            };


            Button btnSuggestion = new Button
            {
                BackgroundColor = Color.White,
                ClassId = suggestion.Key.ToString(),
            };

            btnSuggestion.Clicked += Clicked_BtnSuggestion;

            gridSearch.Children.Add(btnSuggestion, 0, 4, 1, 2);

            gridSearch.Children.Add(new Label
            {
                InputTransparent = true,
                Text = suggestion.Value.Description,
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 12
            }, 1, 2, 1, 2);

            gridSearch.Children.Add(new Label
            {
                InputTransparent = true,
                Text = suggestion.Value.Category,
                TextColor = Color.Black,
                VerticalOptions = LayoutOptions.Center,
                FontSize = 12
            }, 2, 3, 1, 2);

            gridSearch.Children.Add(new BoxView
            {
                HorizontalOptions = LayoutOptions.FillAndExpand,
                BackgroundColor = (Color)App.Current.Resources["SPBlue"],
                HeightRequest = 1
            }, 0, 4, 0, 1);

            stackSearchProducts.Children.Add(gridSearch);
        }

        public void ClearSearch()
        {
            stackSearchCategories.Children.Clear();
            stackSearchProducts.Children.Clear();

            categorySuggestions.Clear();
            suggestionIds.Clear();
        }

        protected virtual void Clicked_BtnSuggestion(object sender, EventArgs eventArgs)
        {
            /*
            stackProducts.Children.Clear();
            AddProductToStack(Data.GetProductFromId(((Button)sender).ClassId));
            stackSearch.IsVisible = false;
            btnClearSearch.IsVisible = true;

            AnimateBackgroundToInvisible();*/

            Product product = Data.GetProductFromId(((Button)sender).ClassId);
            Navigation.PushAsync(new AccountPage(product));
        }

        protected virtual void Clicked_BtnCategory(object sender, EventArgs eventArgs)
        {
            /*stackProducts.Children.Clear();
            foreach (Product product in Data.products)
            {
                if (product.Category == ((Button)sender).ClassId)
                {
                    AddProductToStack(product);
                }
            }
            stackSearch.IsVisible = false;
            btnClearSearch.IsVisible = true;

            AnimateBackgroundToInvisible();*/

            Navigation.PushAsync(new AccountPage(((Button)sender).ClassId));
        }
    }
}