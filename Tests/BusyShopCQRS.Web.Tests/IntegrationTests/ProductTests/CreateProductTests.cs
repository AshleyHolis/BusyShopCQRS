using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using BusyShopCQRS.Contracts.Commands;
using NUnit.Framework;

namespace BusyShopCQRS.Web.Tests.IntegrationTests.ProductTests
{
    [TestFixture]
    public class CreateProductTests : TestBase
    {
        // TODO: Add check to prevent creating duplicate products
        [Test]
        public void Create10Products()
        {
            var products = Products.Take(10).ToList();
            var client = new HttpClient();

            foreach (var product in products)
            {
                var createProduct = new CreateProduct(product.Id, product.Name, product.Price);
                var response = client.PostAsJsonAsync(_baseAddress + "api/products/create", createProduct).Result;
                Debug.WriteLine(response);
                Debug.WriteLine(response.Content.ReadAsStringAsync().Result);
            }
        }
    }
}