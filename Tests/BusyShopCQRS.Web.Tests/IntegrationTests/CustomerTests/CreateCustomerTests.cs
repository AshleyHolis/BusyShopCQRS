using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using BusyShopCQRS.Contracts.Commands;
using NUnit.Framework;

namespace BusyShopCQRS.Web.Tests.IntegrationTests.CustomerTests
{
    [TestFixture]
    public class CreateCustomerTests : TestBase
    {
        // TODO: Add check to prevent creating duplicate products
        [Test]
        public void Create20Customers()
        {
            var customers = Customers.Take(20).ToList();
            var client = new HttpClient();

            foreach (var customer in customers)
            {
                var createCustomer = new CreateCustomer(customer.Id, customer.Name);
                var response = client.PostAsJsonAsync(_baseAddress + "api/customers/create", createCustomer).Result;
                Debug.WriteLine(response);
                Debug.WriteLine(response.Content.ReadAsStringAsync().Result);
            }
        }
    }
}