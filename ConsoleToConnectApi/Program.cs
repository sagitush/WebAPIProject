using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace ConsoleToConnectApi
{
    class Program
    {
        public class Food
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public Nullable<int> Calories { get; set; }
            public string Ingridients { get; set; }
            public Nullable<int> Grade { get; set; }
        }

        private const string URL = "http://localhost:58603/api/food";
        //private const string URL = "http://webapichat110919.azurewebsites.net/api/messages";

        static async Task<Uri> CreateFoodAsync(Food food, HttpClient client)
        {
            HttpResponseMessage response = await client.PostAsJsonAsync(
                "api/messages", food);
            response.EnsureSuccessStatusCode();

            // return URI of the created resource.
            return response.Headers.Location;
        }

        static void Main(string[] args)
        {
            // POST REQUEST
            HttpClient client_post = new HttpClient();

            client_post.BaseAddress = new Uri(URL);
            client_post.DefaultRequestHeaders.Accept.Clear();
            client_post.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

           Food food = new Food
            {
                ID = 10,
                Name = "Cookies",
                Calories= 5000,
                Ingridients= "butter flour chocolate sugar",
                Grade =7
            };

            var response_post = client_post.PostAsJsonAsync(
                 "", food).Result;

            Console.WriteLine(response_post);


            // GET REQUEST
            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri(URL);

            // Add an Accept header for JSON format.
            client.DefaultRequestHeaders.Accept.Add(
                new MediaTypeWithQualityHeaderValue("application/json"));

            // List data response.
            HttpResponseMessage response = client.GetAsync("").Result;  // Blocking call! Program will wait here until a response is received or a timeout occurs.
            if (response.IsSuccessStatusCode)
            {
                // Parse the response body.
                var dataObjects = response.Content.ReadAsAsync<IEnumerable<Food>>().Result;  //Make sure to add a reference to System.Net.Http.Formatting.dll
                foreach (var d in dataObjects)
                {
                    Console.Write("Id:{0} ", d.ID);
                    Console.Write("Name:{0} ", d.Name);
                    Console.Write("Calories:{0} ", d.Calories);
                    Console.Write("Ingridients:{0} ", d.Ingridients);
                    Console.Write("Grade:{0} ", d.Grade);
                    Console.WriteLine();
                }
            }
            else
            {
                Console.WriteLine("{0} ({1})", (int)response.StatusCode, response.ReasonPhrase);
            }

            //Make any other calls using HttpClient here.

            //Dispose once all HttpClient calls are complete. This is not necessary if the containing object will be disposed of; for example in this case the HttpClient instance will be disposed automatically when the application terminates so the following call is superfluous.
            client.Dispose();
        }
    }
}

