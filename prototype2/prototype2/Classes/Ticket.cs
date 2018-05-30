using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prototype2.Classes
{
    public struct TicketStatus
    {
        public const string InProgress = "In Progress";
        public const string Solved = "Solved";
    }

    public class Ticket
    {
        public string Title { get; set; }
        public int Number { get; set; }
        public string Date { get; set; }
        public string SentTo { get; set; }
        public List<string> Messages { get; set; }
        public string State { get; set; }
    }
}
