using System;
using System.Web.Http;
using BusyShopCQRS.Contracts.Commands;
using BusyShopCQRS.Service.Documents;

namespace BusyShopCQRS.Web.Api
{
    public class BasketController : BaseApiController
    {
        [HttpPost]
        public IHttpActionResult Create(CreateBasket input)
        {
            return ExecuteCommand(input);
        }

        // [UriTemplate("/api/basket/{BasketId}/items")]
        [HttpPost]
        public IHttpActionResult AddItemToBasket(AddItemToBasket input)
        {
            return ExecuteCommand(input);
        }

        // [UriTemplate("/api/basket/{BasketId}/proceed")]
        [HttpPost]
        public IHttpActionResult ProceedToCheckout(ProceedToCheckout input)
        {
            return ExecuteCommand(input);
        }

        // [UriTemplate("/api/basket/{BasketId}/checkout")]
        [HttpPost]
        public IHttpActionResult Checkout(CheckoutBasket input)
        {
            return ExecuteCommand(input);
        }

        // [UriTemplate("/api/basket/{BasketId}/pay")]
        [HttpPost]
        public IHttpActionResult Pay(MakePayment input)
        {
            return ExecuteCommand(input);
        }        
    }
}
