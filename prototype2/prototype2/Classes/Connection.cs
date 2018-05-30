using System;
using System.IO;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using Xamarin.Forms;
using System.Diagnostics;

namespace prototype2.Classes
{

    static class Connection
    {
        static HttpClient client = new HttpClient();
        public static void SetDestination(String path){
            client.BaseAddress = new Uri(path);
        }

        public static async Task<bool> IsConnected(){
            try{
                HttpResponseMessage responseMessage = await client.GetAsync("login");
                return true;
            }catch(Exception e){
                return false;
                throw new Exception("Could not connect to server", e);
            }


        }

        public static async Task<int> Login(String username, String password)
        {

            try{
                String target = String.Format("login/{0}/{1}", username, password);
                var response= await client.GetStringAsync(target);
                Login login = JsonConvert.DeserializeObject<Login>(response);
                return login.userId;

            }
            catch (HttpRequestException e){
                throw new Exception("Invalid Login.", e);
            }
            catch (Exception exc)
            {
                throw new Exception("Internal App Error", exc);
            }
        }

        public static async Task<User> GetUser(int userId)
        {
            try
            {
                String target = String.Format("user/{0}", userId);
                var response = await client.GetStringAsync(target);
                User user = JsonConvert.DeserializeObject<User>(response);
                return user;
            }
            catch (HttpRequestException e)
            {
                throw new Exception("Invalid UserId", e);
            }
            catch (Exception exc)
            {
                throw new Exception("Internal App Error", exc);
            }
        }


    }
}
