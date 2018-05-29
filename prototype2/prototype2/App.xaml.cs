using Xamarin.Forms;

namespace prototype2
{
    public partial class App : Application
    {
        /*
         * App wide user account class and object.
         * Creates a default user object when the app starts.
         * User properties changed when a valid login is completed.
         */
        public class User
        {
            public readonly static User Default = new User();

            public int ID { get; set; } // User key / account number
            public string ProfilePhoto { get; set; }
            public string Username { get; set; }
            public string Password { get; set; } // represented as a number of asterisks.
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string Address { get; set; }
            public string[] Payments { get; set; }
        }


        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage(new LoginPage());
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
