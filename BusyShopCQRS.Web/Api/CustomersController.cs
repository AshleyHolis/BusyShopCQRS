using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Services.Description;
using BusyShopCQRS.Contracts.Commands;
using BusyShopCQRS.Contracts.Events;
using BusyShopCQRS.Contracts.Types;
using BusyShopCQRS.Service.Documents;
using BusyShopCQRS.Helpers;
using Neo4jClient;
using Neo4jClient.Cypher;
using Nest;

namespace BusyShopCQRS.Web.Api
{
    public class CustomersController : BaseApiController
    {       
        [HttpPost]
        public IHttpActionResult Create(CreateCustomer input)
        {
            return ExecuteCommand(input);
        }

        [HttpGet]
        public IHttpActionResult Get(string query = null)
        {
            var searchResult =
                EsClient.Search<Customer>(sd => sd.Query(qd => qd.Prefix(mqd => mqd.OnField(p => p.Name).Value(query))).Size(int.MaxValue));

            return Ok(searchResult.Documents);
        }

        [HttpGet]
        public IHttpActionResult GetPreviousOrders(string query = null)
        {
            Guid customerId;
            if (!Guid.TryParse(query, out customerId)) return BadRequest("Invalid Guid");

            var queryResult = Neo4jClient.Cypher
                .Match("(customer:Customer)-[:HAS_BASKET]->(basket:Basket)-[:HAS_ORDERLINE]->(product:Product)")
                .Where((Customer customer) => customer.Id == customerId)
                .Return((basket, product) => new 
                {
                    Basket = basket.As<Basket>(),                    
                    OrderLines = product.CollectAs<Product>()
                })
                .Results;

            var baskets = queryResult.ToDictionary(a => a.Basket, a => a.OrderLines).Select(b => new Basket
            {
                BasketState = b.Key.BasketState,
                Id = b.Key.Id,
                OrderId = b.Key.OrderId,
                OrderLines = b.Value.ToList().Select(c => c.Data).Select(d => new OrderLine(d.Id, d.Name, d.Price, 0, 0)).ToArray()
            });

            return Ok(baskets);
        }
    }
}