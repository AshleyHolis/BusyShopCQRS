using System;
using BusyShopCQRS.Domain;
using BusyShopCQRS.Infrastructure;
using System.Web.Http;
using BusyShopCQRS.Helpers;
using Neo4jClient;
using Nest;

namespace BusyShopCQRS.Web.Api
{
    public abstract class BaseApiController : ApiController
    {
        private IElasticClient _esClient;
        private GraphClient _neo4jClient;
        private DomainEntry _domainEntry;

        protected IElasticClient EsClient => _esClient ?? (_esClient = ElasticClientBuilder.BuildClient());
        protected GraphClient Neo4jClient => _neo4jClient ?? (_neo4jClient = _neo4jClient = Neo4jClientBuilder.Build());
        private DomainEntry DomainEntry => _domainEntry = _domainEntry ?? ApplicationConfiguration.CreateDomainEntry();

        public IHttpActionResult ExecuteCommand<TCommand>(TCommand input) where TCommand : ICommand
        {
            try
            {
                DomainEntry.ExecuteCommand(input);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok();
        }        
    }
}