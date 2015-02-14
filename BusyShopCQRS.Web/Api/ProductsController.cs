using System.Web.Http;
using BusyShopCQRS.Contracts.Commands;
using BusyShopCQRS.Helpers;
using BusyShopCQRS.Service.Documents;
using Nest;

namespace BusyShopCQRS.Web.Api
{
    [RoutePrefix("api/products")]
    public class ProductsController : BaseApiController
    {        
        public ProductsController()
        {
            EsClient = ElasticClientBuilder.BuildClient();
        }

        [HttpPost]
        public IHttpActionResult Create(CreateProduct input)
        {
            return ExecuteCommand(input);
        }

        [HttpGet]
        public IHttpActionResult Get(string query = null)
        {
            var searchResult = EsClient.Search<Product>(sd => sd.Query(qd => qd.Match(mqd => mqd.OnField(p => p.Name).Query(query))).Size(int.MaxValue));

            return Ok(searchResult.Documents);
        }
    }
}