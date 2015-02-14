using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;
using BusyShopCQRS.Contracts.Commands;

namespace BusyShopCQRS.Web
{ 
    public class Program 
    { 
        static void Main()
        {
            const string baseAddress = "http://localhost:9000/";

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress)) 
            { 
                // Create HttpCient and make a request to api/values 
                HttpClient client = new HttpClient();

                var createCustomer = new CreateCustomer(Guid.NewGuid(), "Test02");

                var response = client.PostAsJsonAsync(baseAddress + "api/customers/create", createCustomer).Result;

                Console.WriteLine(response);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);

                response = client.GetAsync(baseAddress + "api/customers/getAll").Result; 

                Console.WriteLine(response); 
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);

                while (true)
                {

                }
            } 

            //Console.ReadLine(); 
        }
    } 
 } 