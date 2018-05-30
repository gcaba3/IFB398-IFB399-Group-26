using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace prototype2
{
    public class SearchBarProductsTool : SearchBarProducts
    {
        private SearchGrid searchGrid;


        public SearchBarProductsTool(SearchSuggestionsStack stackSearch, BoxView boxViewBackground, SearchGrid searchGrid) : base(stackSearch, boxViewBackground)
        {
            this.searchGrid = searchGrid;
        }

        protected override void Searched_SearchBarProducts(object sender, EventArgs eventArgs)
        {
            List<int> suggestionIds = new List<int>(suggestions.Keys);
            searchGrid.Searched_SearchBarProducts(suggestionIds);
        }
    }
}
