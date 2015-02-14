using System;
using System.Net.Http;
using BusyShopCQRS.Contracts.Commands;
using BusyShopCQRS.Contracts.Types;
using NUnit.Framework;

namespace BusyShopCQRS.Web.Tests.IntegrationTests.BasketTests
{
    [TestFixture]
    public class CreateBasketTests : TestBase
    {
        // TODO: Add check to prevent creating duplicate products
        [Test]
        public void Create10RandomBaskets()
        {
            var client = new HttpClient();
            var random = new Random();
            const int numberOfBasketsToOrder = 100;
            const int masNumberOfProductsToOrder = 10;

            for (int i = 0; i < numberOfBasketsToOrder; i++)
            {
                int cost = 0;
                var customer = Customers[random.Next(Customers.Count)];

                var createBasket = new CreateBasket(Guid.NewGuid(), customer.Id);
                var response = client.PostAsJsonAsync(_baseAddress + "api/basket/create", createBasket).Result;
                Console.WriteLine(response);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);

                var productsToOrder = random.Next(1, masNumberOfProductsToOrder);
                for (int j = 0; j < productsToOrder; j++)
                {
                    var product = Products[random.Next(Products.Count)];

                    var addItemToBasket = new AddItemToBasket(createBasket.Id, product.Id, new Random().Next(10));
                    cost += product.Price * addItemToBasket.Quantity;
                    response = client.PostAsJsonAsync(_baseAddress + "api/basket/AddItemToBasket", addItemToBasket).Result;
                    Console.WriteLine(response);
                    Console.WriteLine(response.Content.ReadAsStringAsync().Result);
                }

                var proceedToCheckout = new ProceedToCheckout(createBasket.Id);
                response = client.PostAsJsonAsync(_baseAddress + "api/basket/ProceedToCheckout", proceedToCheckout).Result;
                Console.WriteLine(response);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);

                var checkoutBasket = new CheckoutBasket(createBasket.Id, new Address("MyStreet"));
                response = client.PostAsJsonAsync(_baseAddress + "api/basket/Checkout", checkoutBasket).Result;
                Console.WriteLine(response);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);

                var makePayment = new MakePayment(createBasket.Id, cost);
                response = client.PostAsJsonAsync(_baseAddress + "api/basket/pay", makePayment).Result;
                Console.WriteLine(response);
                Console.WriteLine(response.Content.ReadAsStringAsync().Result);
            }
        }
    }
}