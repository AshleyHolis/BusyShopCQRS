using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web.Http;
using BusyShopCQRS.Contracts.Commands;
using BusyShopCQRS.Helpers;
using BusyShopCQRS.Service.Documents;
using Neo4jClient.Cypher;
using Nest;

namespace BusyShopCQRS.Web.Api
{
    public class ProductsController : BaseApiController
    {        
        [HttpPost]
        public IHttpActionResult Create(CreateProduct input)
        {
            return ExecuteCommand(input);
        }

        [HttpGet]
        public IHttpActionResult Get(string query = null)
        {
            var searchResult = ElasticClientBuilder.EsClient.Search<Product>(sd => sd.Query(qd => qd.Prefix(mqd => mqd.OnField(p => p.Name).Value(query))).Size(int.MaxValue));

            return Ok(searchResult.Documents);
        }

        [HttpPost]
        public IHttpActionResult GetRecommendations(ProductRecommendationQuery query)
        {
            var queryResult = Neo4jClient.Cypher
                .Match("(p1:Product)--(b:Basket)--(c:Customer)")
                .Match("(c)--(b:Basket)--(p2:Product)")
                .Where("p1.Id IN {Ids} AND NOT(p2.Id IN {Ids})")
                .WithParams(new
                {
                    Ids = query.ProductIds
                })
                .Match("(p2)--(b3:Basket)")
                .Return((p2, b3) => new ProductRecommendationResult
                {
                    Product = Return.As<Product>("p2"),
                    Cnt = Return.As<int>("COUNT(DISTINCT b3)"),
                })
                .OrderByDescending("Cnt");

            return Ok(queryResult.Results.ToList());
        }

        public class ProductRecommendationQuery
        {
            public List<Guid> ProductIds { get; set; }
        }

        public class ProductRecommendationResult
        {
            public Product Product { get; set; }
            public int Cnt { get; set; }
        }
    }
}