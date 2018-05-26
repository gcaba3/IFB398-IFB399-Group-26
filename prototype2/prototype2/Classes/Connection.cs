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
    public class Test{
        public string _id { get; set; }
        public int userId { get; set; }
        public string username { get; set; }
        public string password { get; set; }
        public int __v { get; set; }
    }
    static class Connection
    {
        static HttpClient client = new HttpClient();

        public static async Task<List<Test>> GetTest(){
            List<Test> tests = null;
            try{
                HttpResponseMessage responseMessage = await client.GetAsync("login");
                tests = JsonConvert.DeserializeObject<List<Test>>(await responseMessage.Content.ReadAsStringAsync());
                return tests;
            }
            catch (Exception exc)
            {
                throw new Exception(exc.Message);
            }

        }

        public static void ShowTest(Test test){
            Debug.WriteLine("Successful connection: " + test._id);
        }

        public static void SetDestination(String path){
            client.BaseAddress = new Uri(path);
        }


    }
}
