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
	public abstract partial class SearchBarProducts : SearchBar
	{
        private SearchSuggestionsStack stackSearch;
        private BoxView boxViewBackground;
        protected Dictionary<int, Product> suggestions;

		public SearchBarProducts (SearchSuggestionsStack stackSearch, BoxView boxViewBackground)
		{
			InitializeComponent ();

            this.stackSearch = stackSearch;
            this.boxViewBackground = boxViewBackground;

		}

        protected abstract void Searched_SearchBarProducts(object sender, EventArgs eventArgs);



        private void TextChanged_SearchBarProducts(object sender, TextChangedEventArgs eventArgs)
        {
            string searchWord = Text.ToLower();

            if (searchWord.Length > 0)
            {
                List<string> suggestedCategories = stackSearch.FindCategories(searchWord);

                stackSearch.RemoveNonSuggestedCategoriesFromStack(suggestedCategories);
                stackSearch.AddSuggestedCategoriesToStack(suggestedCategories);

                suggestions = stackSearch.FindSuggestions(searchWord);

                stackSearch.RemoveNonSuggestionsFromStack(suggestions);
                stackSearch.AddSuggestionsToStack(suggestions);

                stackSearch.IsVisible = true;

                if (!boxViewBackground.IsVisible)
                    AnimateBackgroundToVisible();
            }

            else
            {
                stackSearch.IsVisible = false;
                //AnimateBackgroundToInvisible();
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

    }
}