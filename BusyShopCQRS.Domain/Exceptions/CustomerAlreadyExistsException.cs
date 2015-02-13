using System;

namespace BusyShopCQRS.Domain.Exceptions
{
    public class CustomerAlreadyExistsException : DuplicateAggregateException
    {
        public CustomerAlreadyExistsException(Guid id) : base(id)
        {
            
        }
    }
}