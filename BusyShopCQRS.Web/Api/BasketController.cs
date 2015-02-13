using System;
using BusyShopCQRS.Domain;
using BusyShopCQRS.Infrastructure;
using System.Web.Http;
using BusyShopCQRS.Contracts.Commands;

namespace BusyShopCQRS.Web.Api.Order
{
    [RoutePrefix("api/basket")]
    public class BasketController : BaseApiController
    {
        [Route("create")]
        [HttpPost]
        public IHttpActionResult Create(CreateBasket input)
        {
            return ExecuteCommand(input);
        }

        // [UriTemplate("/api/basket/{BasketId}/checkout")]
        [Route("/{Id}/checkout")]
        [HttpPost]
        public IHttpActionResult Checkout(AddItemToBasket input)
        {
            return ExecuteCommand(input);
        }

        // [UriTemplate("/api/basket/{BasketId}/items")]
        [Route("/{Id}/items")]
        [HttpPost]
        public IHttpActionResult AddItemToBasket(AddItemToBasket input)
        {
            return ExecuteCommand(input);
        }

        // [UriTemplate("/api/basket/{BasketId}/pay")]
        [Route("/{Id}/pay")]
        [HttpPost]
        public IHttpActionResult Pay(MakePayment input)
        {
            return ExecuteCommand(input);
        }

        // [UriTemplate("/api/basket/{BasketId}/proceed")]
        [Route("/{Id}/proceed")]
        [HttpPost]
        public IHttpActionResult ProceedToCheckout(ProceedToCheckout input)
        {
            return ExecuteCommand(input);
        }
    }
}
