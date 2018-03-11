using System;
namespace prototype2
{
    public class Invoice
    {
        public int invoiceNumber { get; set; }
        public DateTime dateIssued { get; set; }
        public DateTime dateDue { get; set; }
        public DateTime datePaid { get; set; }
        public int amountDue { get; set; }
        public int amountPaid { get; set; } 

        public Invoice()
        {                
        }


    }
}
