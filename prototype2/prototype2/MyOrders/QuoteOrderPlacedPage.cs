using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prototype2
{
    public partial class QuoteOrderPlacedPage : QuotePage
    {
        public QuoteOrderPlacedPage(Quote quote) : base(quote)
        {
        }

        protected override void AddDocumentControls()
        {
            AddViewQuotePDFButton();
            //AddDeleteQuoteButton();
        }
    }
}
