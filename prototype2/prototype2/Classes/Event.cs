using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace prototype2.Classes
{
    public struct EventStatus
    {
        public const string Upcoming = "Upcoming";
        public const string InProgress = "In Progress";
        public const string Finished = "Finished";
    }

    public class Event
    {
        public string Name { get; set; }
        public string Date { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public double Price { get; set; }
        public string State { get; set; }
        public string PurchaseURL { get; set; }
    }
}
