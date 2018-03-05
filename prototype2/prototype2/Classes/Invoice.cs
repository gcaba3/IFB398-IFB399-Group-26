using System;
namespace prototype2
{
    public class Invoice
    {
        public int InvoiceNumber { get; set; }
        public DateTime DatePaid { get; set; }
        public int AmountDue { get; set; }
        public int AmountPaid { get; set; } 

        public Invoice()
        {                
        }


    }
}
