using System.Web.Http;
using BusyShopCQRS.Contracts.Commands;
using BusyShopCQRS.Helpers;
using BusyShopCQRS.Service.Documents;
using Nest;

namespace BusyShopCQRS.Web.Api
{
    public class ProductsController : BaseApiController
    {        
        [Route]
        [HttpPost]
        public IHttpActionResult Create(CreateProduct input)
        {
            return ExecuteCommand(input);
        }

        [Route]
        [HttpGet]
        public IHttpActionResult Get(string query = null)
        {
            var searchResult = EsClient.Search<Product>(sd => sd.Query(qd => qd.Prefix(mqd => mqd.OnField(p => p.Name).Value(query))).Size(int.MaxValue));

            return Ok(searchResult.Documents);
        }
    }
}