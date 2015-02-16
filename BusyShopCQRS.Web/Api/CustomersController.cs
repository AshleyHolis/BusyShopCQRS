using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Http;
using System.Web.Services.Description;
using BusyShopCQRS.Contracts.Commands;
using BusyShopCQRS.Contracts.Events;
using BusyShopCQRS.Service.Documents;
using BusyShopCQRS.Helpers;
using Nest;

namespace BusyShopCQRS.Web.Api
{
    public class CustomersController : BaseApiController
    {
        public CustomersController()
        {
            EsClient = ElasticClientBuilder.BuildClient();
        }

        [HttpPost]
        public IHttpActionResult Create(CreateCustomer input)
        {
            return ExecuteCommand(input);
        }

        [HttpGet]
        public IHttpActionResult Get(string query = null)
        {
            var searchResult =
                EsClient.Search<Customer>(sd => sd.Query(qd => qd.Match(mqd => mqd.OnField(p => p.Name).Query(query))).Size(int.MaxValue));

            return Ok(searchResult.Documents);
        }
    }
}