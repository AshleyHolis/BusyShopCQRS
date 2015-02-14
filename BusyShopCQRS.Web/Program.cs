using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;
using BusyShopCQRS.Contracts.Commands;
using BusyShopCQRS.Contracts.Types;

namespace BusyShopCQRS.Web
{ 
    public class Program 
    { 
        static void Main()
        {
            //string baseAddress = string.Format("http://{0}:9000/", "localhost");
            string baseAddress = string.Format("http://{0}:9000/", Environment.MachineName);

            // Start OWIN host 
            using (WebApp.Start<Startup>(url: baseAddress))
            {
                Console.WriteLine("Started.");
                //CreateTestOrder(baseAddress);

                Console.ReadLine();
            }
        }

        private static void CreateTestOrder(string baseAddress)
        {
            // Create HttpCient and make a request to api/values 
            HttpClient client = new HttpClient();

            var createProduct = new CreateProduct(Guid.NewGuid(), "TestProduct" + new Random().Next(), new Random().Next(100));
            var response = client.PostAsJsonAsync(baseAddress + "api/products/create", createProduct).Result;
            Console.WriteLine(response);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);

            var createCustomer = new CreateCustomer(Guid.NewGuid(), "Test" + new Random().Next());
            response = client.PostAsJsonAsync(baseAddress + "api/customers/create", createCustomer).Result;
            Console.WriteLine(response);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);

            var createBasket = new CreateBasket(Guid.NewGuid(), createCustomer.Id);
            response = client.PostAsJsonAsync(baseAddress + "api/basket/create", createBasket).Result;
            Console.WriteLine(response);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);

            var addItemToBasket = new AddItemToBasket(createBasket.Id, createProduct.Id, new Random().Next(100));
            response = client.PostAsJsonAsync(baseAddress + "api/basket/AddItemToBasket", addItemToBasket).Result;
            Console.WriteLine(response);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);

            var proceedToCheckout = new ProceedToCheckout(createBasket.Id);
            response = client.PostAsJsonAsync(baseAddress + "api/basket/ProceedToCheckout", proceedToCheckout).Result;
            Console.WriteLine(response);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);

            var checkoutBasket = new CheckoutBasket(createBasket.Id, new Address("MyStreet"));
            response = client.PostAsJsonAsync(baseAddress + "api/basket/Checkout", checkoutBasket).Result;
            Console.WriteLine(response);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);

            var paymentAmount = createProduct.Price*addItemToBasket.Quantity;
            var makePayment = new MakePayment(createBasket.Id, paymentAmount);
            response = client.PostAsJsonAsync(baseAddress + "api/basket/pay", makePayment).Result;
            Console.WriteLine(response);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
        }
    } 
 }