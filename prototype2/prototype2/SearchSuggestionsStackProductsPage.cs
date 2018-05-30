using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace prototype2
{
    public class SearchSuggestionsStackProductsPage : SearchSuggestionsStack
    {
        private ProductsPage productsPage;
        public SearchSuggestionsStackProductsPage(ProductsPage productsPage)
        {
            this.productsPage = productsPage;
        }

        public override Dictionary<int, Product> FindSuggestions(string searchWord)
        {
            Dictionary<int, Product> suggestions = new Dictionary<int, Product>();
            int numSuggestions = 0;
            int numProducts = 0;

            while (numSuggestions < 15 && numProducts < Data.products.Count)
            {
                Product product = Data.products[numProducts];
                if ((productsPage.CategoryAll() == productsPage.FilterCategory() || productsPage.FilterCategory() == product.Category) && product.Description.ToLower().Contains(searchWord))
                {
                    suggestions.Add(Data.products[numProducts].Id, Data.products[numProducts]);
                    numSuggestions++;
                }
                numProducts++;
            }

            return suggestions;
        }

        public override List<string> FindCategories(string searchWord)
        {
            List<string> suggestedCategories = new List<string>();

            if (productsPage.FilterCategory() == productsPage.CategoryAll())
            {
                foreach (string category in Product.categories)
                {
                    if (category.ToLower().Contains(searchWord))
                    {
                        suggestedCategories.Add(category);
                    }
                }
            }/*
            else
            {
                foreach (string category in Product.categories)
                {
                    if (category == productsPage.FilterCategory() && category.ToLower().Contains(searchWord))
                    {
                        suggestedCategories.Add(category);
                        break;
                    }
                }
            }  */          

            return suggestedCategories;
        }

        protected override void Clicked_BtnSuggestion(object sender, EventArgs eventArgs)
        {
            productsPage.Clicked_BtnSuggestion(Data.GetProductFromId(((Button)sender).ClassId));
        }

        protected override void Clicked_BtnCategory(object sender, EventArgs eventArgs)
        {
            productsPage.Clicked_BtnCategory(((Button)sender).ClassId);
        }
    }
}
