using System;
using BusyShopCQRS.Contracts.Commands;
using BusyShopCQRS.Domain.Aggregates;
using BusyShopCQRS.Domain.Exceptions;
using BusyShopCQRS.Infrastructure;
using BusyShopCQRS.Infrastructure.Exceptions;

namespace BusyShopCQRS.Domain.CommandHandlers
{
    internal class CustomerCommandHandler : 
        IHandle<CreateCustomer>, 
        IHandle<MarkCustomerAsPreferred>
    {
        private readonly IDomainRepository _domainRepository;

        public CustomerCommandHandler(IDomainRepository domainRepository)
        {
            _domainRepository = domainRepository;
        }

        public IAggregate Handle(CreateCustomer command)
        {
            try
            {
                var customer = _domainRepository.GetById<Customer>(command.Id);
                throw new CustomerAlreadyExistsException(command.Id);
            }
            catch (AggregateNotFoundException)
            {
                // We expect not to find anything
            }
            return Customer.Create(command.Id, command.Name);
        }

        public IAggregate Handle(MarkCustomerAsPreferred command)
        {
            var customer = _domainRepository.GetById<Customer>(command.Id);
            customer.MakePreferred(command.Discount);
            return customer;
        }
    }
}