using System;
using BusyShopCQRS.Contracts.Commands;
using BusyShopCQRS.Contracts.Events;
using BusyShopCQRS.Contracts.Types;
using BusyShopCQRS.Domain.Exceptions;
using BusyShopCQRS.Tests;
using NUnit.Framework;

namespace BusyShopCQRS.Domain.Tests.BasketTests
{
    [TestFixture]
    public class CheckoutBasketTests : TestBase
    {
        [TestCase(null)]
        [TestCase("")]
        [TestCase("    ")]
        public void WhenTheUserCheckoutWithInvalidAddress_IShouldGetNotified(string street)
        {
            var address = street == null ? null : new Address(street);
            var id = Guid.NewGuid();
            Given(new BasketCreated(id, Guid.NewGuid(), 0));
            WhenThrows<MissingAddressException>(new CheckoutBasket(id, address));
        }

        [Test]
        public void WhenTheUserCheckoutWithAValidAddress_IShouldProceedToTheNextStep()
        {
            var address = new Address("Valid street");
            var id = Guid.NewGuid();
            Given(new BasketCreated(id, Guid.NewGuid(), 0));
            When(new CheckoutBasket(id, address));
            Then(new BasketCheckedOut(id, address));
        }
    }
}