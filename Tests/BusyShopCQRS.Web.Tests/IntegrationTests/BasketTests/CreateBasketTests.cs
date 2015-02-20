using System;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using BusyShopCQRS.Contracts.Commands;
using BusyShopCQRS.Contracts.Types;
using NUnit.Framework;

namespace BusyShopCQRS.Web.Tests.IntegrationTests.BasketTests
{
    [TestFixture]
    public class CreateBasketTests : TestBase
    {
        [Test]
        public void Create100RandomBasketsInParallel()
        {
            const int numberOfBasketsToOrder = int.MaxValue;
            const int maxNumberOfProductsToOrder = 10;

            //for (int i = 0; i < numberOfBasketsToOrder; i++)
            //{
            //    Debug.WriteLine("Order: {0}", i);
            //    CreateRandomBasket(maxNumberOfProductsToOrder);
            //}

            Parallel.For(0, numberOfBasketsToOrder, new ParallelOptions {MaxDegreeOfParallelism = 20}, i =>
            {
                Debug.WriteLine("Order: {0}", i);
                CreateRandomBasket(maxNumberOfProductsToOrder);
            });

            //Parallel.For(0, numberOfBasketsToOrder, i =>
            //{
            //    Debug.WriteLine("Order: {0}", i);
            //    CreateRandomBasket(maxNumberOfProductsToOrder);
            //});
        }

        private void CreateRandomBasket(int maxNumberOfProductsToOrder)
        {
            var random = new Random();

            var cost = 0;
            var customer = Customers[random.Next(Customers.Count)];

            var createBasket = new CreateBasket(Guid.NewGuid(), customer.Id);
            var response = Json.UploadJsonObjectAsync(new Uri(_baseAddress + "api/basket/create"), createBasket);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var numberOfProductsToOrder = random.Next(1, maxNumberOfProductsToOrder);
            for (var j = 0; j < numberOfProductsToOrder; j++)
            {
                var product = Products[random.Next(Products.Count)];

                var addItemToBasket = new AddItemToBasket(createBasket.Id, product.Id, new Random().Next(10));
                cost += product.Price*addItemToBasket.Quantity;
                response = Json.UploadJsonObjectAsync(new Uri(_baseAddress + "api/basket/AddItemToBasket"),
                    addItemToBasket);
                Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            }

            var proceedToCheckout = new ProceedToCheckout(createBasket.Id);
            response = Json.UploadJsonObjectAsync(new Uri(_baseAddress + "api/basket/ProceedToCheckout"),
                proceedToCheckout);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var checkoutBasket = new CheckoutBasket(createBasket.Id, new Address("MyStreet"));
            response = Json.UploadJsonObjectAsync(new Uri(_baseAddress + "api/basket/Checkout"), checkoutBasket);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);

            var makePayment = new MakePayment(createBasket.Id, cost);
            response = Json.UploadJsonObjectAsync(new Uri(_baseAddress + "api/basket/pay"), makePayment);
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
        }
    }
}