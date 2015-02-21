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
        private GraphClient _neo4jClient;        
        
        protected GraphClient Neo4jClient => _neo4jClient ?? (_neo4jClient = _neo4jClient = Neo4jClientBuilder.Build());

        public IHttpActionResult ExecuteCommand<TCommand>(TCommand input) where TCommand : ICommand
        {
            try
            {
                ApplicationConfiguration.DomainEntry.ExecuteCommand(input);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok();
        }        
    }
}