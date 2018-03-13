using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prototype2.Classes
{
    public struct QuoteStatus
    {
        public const string Empty = "Empty Sale";
        public const string Incomplete = "Incomplete";
        public const string PendingResponse = "Pending Response";
        public const string Validated = "Validated";
        public const string OrderPlaced = "Order Placed";
    }
    public class Quote : SalesDocument
    {
        
    }
}
