using System;
namespace prototype2.Classes
{
    public class User
    {
        public int ID { get; set; } // User key / account number
        public string ProfilePhoto { get; set; }
        public string Username { get; set; }
        public string Password { get; set; } // represented as a number of asterisks.
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }
        public bool HasCreditCard { get; set; }
        public bool HasPaypal { get; set; }

        public User()
        {
        }
    }
}
