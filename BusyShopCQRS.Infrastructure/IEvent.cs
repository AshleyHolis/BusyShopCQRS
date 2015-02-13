using System;

namespace BusyShopCQRS.Infrastructure
{
    public interface IEvent
    {
        Guid Id { get; }
    }
}
