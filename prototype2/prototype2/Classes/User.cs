using System;
namespace prototype2.Classes
{
    //User model for the rest API response
    public class User
    {
        public int userId { get; set; }
        public string profilePicture { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string[] payments { get; set; }
        public User()
        {
        }
    }
}
