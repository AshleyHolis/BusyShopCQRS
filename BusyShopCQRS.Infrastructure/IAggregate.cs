﻿using System;
using System.Collections.Generic;

namespace BusyShopCQRS.Infrastructure
{
    public interface IAggregate
    {
        IEnumerable<IEvent> UncommitedEvents();
        void ClearUncommitedEvents();
        int Version { get; }
        Guid Id { get; }
        void ApplyEvent(IEvent @event);
    }
}