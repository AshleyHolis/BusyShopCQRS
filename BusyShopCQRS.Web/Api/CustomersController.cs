using System;
using BusyShopCQRS.Domain;
using BusyShopCQRS.Infrastructure;
using System.Web.Http;
using BusyShopCQRS.Contracts.Commands;

namespace BusyShopCQRS.Web.Api.Order
{
    [RoutePrefix("api/customers")]
    public class CustomersController : BaseApiController
    {
        [Route("create")]
        [HttpPost]
        public IHttpActionResult Create(CreateCustomer input)
        {
            return ExecuteCommand(input);
        }

        // [UriTemplate("/api/customer/{CustomerId}/preferred")]
        [Route("/{Id}/markAsPreferred")]
        [HttpPost]
        public IHttpActionResult Create(MarkCustomerAsPreferred input)
        {
            return ExecuteCommand(input);
        }
    }
}
