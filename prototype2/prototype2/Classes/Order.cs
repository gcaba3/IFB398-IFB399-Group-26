using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prototype2.Classes
{
    public struct OrderStatus
    {
        public const string ConfirmedSale = "Confirmed Sale";
        public const string Packing = "Packing";
        public const string DispatchReady = "Dispatch Ready";
        public const string InTransit = "In Transit";
        public const string Complete = "Complete";
    }
    public class Order : SalesDocument
    {       
        
    }
}
