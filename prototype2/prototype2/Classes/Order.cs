using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prototype2.Classes
{
    public enum OrderStatus { ConfirmedSale, Packing, DispatchReady, InTransit, Complete };

    public class Order : SalesDocument
    {
    }
}
