using System;
namespace prototype2
{
    public enum InvoiceStatus { Unpaid, Partial, Paid, Overdue };
    public class Invoice : Classes.SalesDocument
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
