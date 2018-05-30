using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace prototype2
{
    public class SearchBarProductsPage : SearchBarProducts
    {
        ProductsPage productsPage;
        SearchSuggestionsStack stackSearch;
        public SearchBarProductsPage(SearchSuggestionsStack stackSearch, BoxView boxViewBackground, ProductsPage productsPage) : base(stackSearch, boxViewBackground)
        {
            this.stackSearch = stackSearch;
            this.productsPage = productsPage;
        }

        protected override void Searched_SearchBarProducts(object sender, EventArgs eventArgs)
        {
            productsPage.Searched_SearchBarProducts(stackSearch.SuggestionIds);
        }        
    }
}
