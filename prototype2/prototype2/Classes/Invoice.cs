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
    public class Invoice : SalesDocument
    {        
        public int InvoiceNumber { get; set; }
        public DateTime DateIssued { get; set; }
        public DateTime DateDue { get; set; }
        public double AmountDue { get; set; }
        public double AmountPaid { get; set; }
        public string Source { get; set; }

        public Invoice()
        {
        }


    }
}
