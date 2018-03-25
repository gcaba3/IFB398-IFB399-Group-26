using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prototype2
{
    public class Product
    {
        public string Description { get; set; }
        //public string code { get; set; }
        public string Category { get; set; }
        public string Manufacturer { get; set; }
        public double Price { get; set; }
        public int Stock { get; set; }
        public string ImagePath { get; set; }
        public int Id { get; set; }
    }
}
