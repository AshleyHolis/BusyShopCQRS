using System;
using BusyShopCQRS.Domain;
using BusyShopCQRS.Infrastructure;
using System.Web.Http;
using Nest;

namespace BusyShopCQRS.Web.Api
{
    public abstract class BaseApiController : ApiController
    {
        protected IElasticClient EsClient;

        //public IHttpActionResult Post()
        //{
        //    try
        //    {
        //        var connection = BusyShopCQRS.Web.Configuration.CreateConnection();
        //        var domainRepository = new EventStoreDomainRepository(connection);
        //        var application = new DomainEntry(domainRepository);
        //        application.ExecuteCommand(Input);
        //    }
        //    catch (Exception)
        //    {
        //        return InternalServerError();
        //    }

        //    return Ok();
        //}

        public IHttpActionResult ExecuteCommand<TCommand>(TCommand input) where TCommand : ICommand
        {
            try
            {
                var connection = Web.Configuration.CreateConnection();
                var domainRepository = new EventStoreDomainRepository(connection);
                var application = new DomainEntry(domainRepository);
                application.ExecuteCommand(input);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }

            return Ok();
        }        
    }
}