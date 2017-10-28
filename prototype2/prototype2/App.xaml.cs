using Xamarin.Forms;

namespace prototype2
{
    public partial class App : Application
    {
        public class User
        {
            public readonly static User Default = new User();

            public string Username { get; set; }
        }

            
        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage( new LoginPage());
            //MainPage = new NavigationPage(new AccountPage());
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
