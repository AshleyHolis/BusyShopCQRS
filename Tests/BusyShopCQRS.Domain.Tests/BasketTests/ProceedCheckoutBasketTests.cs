using System;
using BusyShopCQRS.Contracts.Commands;
using BusyShopCQRS.Contracts.Events;
using BusyShopCQRS.Tests;
using NUnit.Framework;

namespace BusyShopCQRS.Domain.Tests.BasketTests
{
    [TestFixture]
    public class ProceedCheckoutBasketTests : TestBase
    {
        [Test]
        public void GivenABasket_WhenCreatingABasketForCustomerX_ThenTheBasketShouldBeCreated()
        {
            var id = Guid.NewGuid();
            var customerId = Guid.NewGuid();
            int discount = 0;
            Given(new BasketCreated(id, customerId, discount));
            When(new ProceedToCheckout(id));
            Then(new CustomerIsCheckingOutBasket(id));
        }
    }
}