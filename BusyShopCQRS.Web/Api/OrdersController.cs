using System.Web.Http;
using BusyShopCQRS.Contracts.Commands;

namespace BusyShopCQRS.Web.Api
{
    public class OrdersController : BaseApiController
    {
        [HttpPost]
        public IHttpActionResult Approve(ApproveOrder input)
        {
            return ExecuteCommand(input);
        }

        [HttpPost]
        public IHttpActionResult Cancel(CancelOrder input)
        {
            return ExecuteCommand(input);
        }

        [HttpPost]
        public IHttpActionResult Ship(ShipOrder input)
        {
            return ExecuteCommand(input);
        }

        [HttpPost]
        public IHttpActionResult StartShipping(StartShippingProcess input)
        {
            return ExecuteCommand(input);
        }
    }
}
