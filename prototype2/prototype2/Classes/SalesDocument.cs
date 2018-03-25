using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prototype2
{
    public abstract class SalesDocument
    {
        public string Number { get; set; }
        public string Status { get; set; }
        public Dictionary<Product, int> Products { get; set; }
        public double TotalPrice { get; set; }
        public DateTime Date { get; set; }
    }
}
