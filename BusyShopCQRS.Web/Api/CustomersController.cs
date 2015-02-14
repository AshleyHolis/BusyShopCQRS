using System;
using System.Collections.Generic;
using System.Web.Http;
using BusyShopCQRS.Contracts.Commands;
using BusyShopCQRS.Contracts.Events;

namespace BusyShopCQRS.Web.Api
{
    [RoutePrefix("api/customers")]
    public class CustomersController : BaseApiController
    {
        [Route("createCustomer")]
        [HttpPost]
        public IHttpActionResult Create(CreateCustomer input)
        {
            return ExecuteCommand(input);
        }

        //// [UriTemplate("/api/customer/{CustomerId}/preferred")]
        //[Route("/{Id}/markAsPreferred")]
        //[HttpPost]
        //public IHttpActionResult Create(MarkCustomerAsPreferred input)
        //{
        //    return ExecuteCommand(input);
        //}

        [Route("getAll")]
        [HttpGet]
        public IHttpActionResult GetAll()
        {
            var customers = new List<CustomerCreated> { new CustomerCreated(Guid.NewGuid(), "Test01")  };
            return Ok(customers);
        }     
    }
}
