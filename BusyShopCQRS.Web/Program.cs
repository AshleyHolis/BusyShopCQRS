using Microsoft.Owin.Hosting;
using System;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Text;
using System.Threading.Tasks;
using BusyShopCQRS.Contracts.Commands;
using BusyShopCQRS.Contracts.Types;
using Newtonsoft.Json;

namespace BusyShopCQRS.Web
{ 
    public class Program 
    { 
        static void Main()
        {
            //var baseAddress = string.Format("http://{0}:9000/", "localhost");
            var baseAddress = string.Format("http://{0}:9000/", Environment.MachineName);

            // Start OWIN host 
            using (WebApp.Start<Startup>(baseAddress))
            {
                Console.WriteLine("Started.");
                CreateTestOrder(baseAddress);

                Console.ReadLine();
            }
        }

        private static void CreateTestOrder(string baseAddress)
        {       
            var createProduct = new CreateProduct(Guid.NewGuid(), "TestProduct" + new Random().Next(), new Random().Next(100));
            var response = UploadJsonObjectAsync(new Uri(baseAddress + "api/products/create"), createProduct);
            Console.WriteLine(response);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);

            var createCustomer = new CreateCustomer(Guid.NewGuid(), "TestCustomer" + new Random().Next());
            response = UploadJsonObjectAsync(new Uri(baseAddress + "api/customers/create"), createCustomer);
            Console.WriteLine(response);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);

            var createBasket = new CreateBasket(Guid.NewGuid(), createCustomer.Id);
            response = UploadJsonObjectAsync(new Uri(baseAddress + "api/basket/create"), createBasket);
            Console.WriteLine(response);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);

            var addItemToBasket = new AddItemToBasket(createBasket.Id, createProduct.Id, new Random().Next(100));
            response = UploadJsonObjectAsync(new Uri(baseAddress + "api/basket/addItemToBasket"), addItemToBasket);
            Console.WriteLine(response);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);

            var proceedToCheckout = new ProceedToCheckout(createBasket.Id);
            response = UploadJsonObjectAsync(new Uri(baseAddress + "api/basket/proceedToCheckout"), proceedToCheckout);
            Console.WriteLine(response);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);

            var checkoutBasket = new CheckoutBasket(createBasket.Id, new Address("TestStreet"));
            response = UploadJsonObjectAsync(new Uri(baseAddress + "api/basket/checkout"), checkoutBasket);
            Console.WriteLine(response);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);

            var paymentAmount = createProduct.Price*addItemToBasket.Quantity;
            var makePayment = new MakePayment(createBasket.Id, paymentAmount);
            response = UploadJsonObjectAsync(new Uri(baseAddress + "api/basket/pay"), makePayment);
            Console.WriteLine(response);
            Console.WriteLine(response.Content.ReadAsStringAsync().Result);
        }

        static HttpResponseMessage UploadJsonObjectAsync<T>(Uri uri, T data)
        {
            using (var client = new HttpClient())
            {
                var content = JsonConvert.SerializeObject(data);
                return client.PostAsync(uri, new StringContent(content, Encoding.UTF8, "application/json")).Result;
            }
        }
    } 
 }