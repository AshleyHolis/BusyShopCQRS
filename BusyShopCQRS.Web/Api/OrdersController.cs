using System.Web.Http;
using BusyShopCQRS.Contracts.Commands;

namespace BusyShopCQRS.Web.Api
{
    [RoutePrefix("api/orders")]
    public class OrdersController : BaseApiController
    {
        [Route("approve")]
        [HttpPost]
        public IHttpActionResult Approve(ApproveOrder input)
        {
            return ExecuteCommand(input);
        }

        [Route("cancel")]
        [HttpPost]
        public IHttpActionResult Cancel(CancelOrder input)
        {
            return ExecuteCommand(input);
        }

        [Route("ship")]
        [HttpPost]
        public IHttpActionResult Ship(ShipOrder input)
        {
            return ExecuteCommand(input);
        }

        [Route("startShipping")]
        [HttpPost]
        public IHttpActionResult StartShipping(StartShippingProcess input)
        {
            return ExecuteCommand(input);
        }
    }
}
