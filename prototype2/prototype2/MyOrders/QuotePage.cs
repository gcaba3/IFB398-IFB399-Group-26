using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace prototype2
{
    public abstract partial class QuotePage : SalesDocumentPage
    {
        protected Quote quote;
        public QuotePage(Quote quote) : base(quote)
        {
            Title = "Quote " + quote.Number;

            this.quote = quote;
        }        

        protected override void EditLabelsText()
        {
            labelDateHeading.Text = "Quotation Date:";
        }
    }
}