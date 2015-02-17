using System;
using System.Web.Http;
using BusyShopCQRS.Contracts.Commands;
using BusyShopCQRS.Service.Documents;

namespace BusyShopCQRS.Web.Api
{
    public class BasketController : BaseApiController
    {
        [HttpPost]
        public IHttpActionResult Create(CreateBasket command)
        {
            return ExecuteCommand(command);
        }

        // [UriTemplate("/api/basket/{BasketId}/items")]
        [HttpPost]
        public IHttpActionResult AddItemToBasket(AddItemToBasket command)
        {
            return ExecuteCommand(command);
        }

        // [UriTemplate("/api/basket/{BasketId}/proceed")]
        [HttpPost]
        public IHttpActionResult ProceedToCheckout(ProceedToCheckout command)
        {
            return ExecuteCommand(command);
        }

        // [UriTemplate("/api/basket/{BasketId}/checkout")]
        [HttpPost]
        public IHttpActionResult Checkout(CheckoutBasket command)
        {
            return ExecuteCommand(command);
        }

        // [UriTemplate("/api/basket/{BasketId}/pay")]
        [HttpPost]
        public IHttpActionResult Pay(MakePayment command)
        {
            return ExecuteCommand(command);
        }        
    }
}
