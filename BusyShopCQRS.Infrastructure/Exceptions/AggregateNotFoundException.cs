using System;

namespace BusyShopCQRS.Infrastructure.Exceptions
{
    public class AggregateNotFoundException : Exception
    {
        public AggregateNotFoundException(string message)
            : base(message)
        {
        }
    }
}