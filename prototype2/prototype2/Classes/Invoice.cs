using System;
namespace prototype2
{
    public struct InvoiceStatus
    {
        public const string Unpaid = "Unpaid";
        public const string Partial = "Partial";
        public const string Paid = "Paid";
        public const string Overdue = "Overdue";
    }
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
