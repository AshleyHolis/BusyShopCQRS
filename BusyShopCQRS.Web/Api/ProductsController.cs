using System;
using BusyShopCQRS.Domain;
using BusyShopCQRS.Infrastructure;
using System.Web.Http;
using BusyShopCQRS.Contracts.Commands;

namespace BusyShopCQRS.Web.Api.Order
{
    [RoutePrefix("api/products")]
    public class ProductsController : BaseApiController
    {
        [Route("create")]
        [HttpPost]
        public IHttpActionResult Create(CreateProduct input)
        {
            return ExecuteCommand(input);
        }        
    }
}
