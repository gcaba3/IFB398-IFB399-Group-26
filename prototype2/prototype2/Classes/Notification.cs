using System;
namespace prototype2.Classes
{
    public class Notification
    {
        public String NotificationTitle { get; set; }
        public DateTime DatePosted { get; set; }
        public String NotificationContent { get; set; }
        public string type { get; set; }

        public Notification()
        {
        }
    }
}
