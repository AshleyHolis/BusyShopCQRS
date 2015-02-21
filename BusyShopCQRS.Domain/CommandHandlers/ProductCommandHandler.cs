using System;
using BusyShopCQRS.Contracts.Commands;
using BusyShopCQRS.Domain.Aggregates;
using BusyShopCQRS.Domain.Exceptions;
using BusyShopCQRS.Infrastructure;
using BusyShopCQRS.Infrastructure.Exceptions;

namespace BusyShopCQRS.Domain.CommandHandlers
{
    internal class ProductCommandHandler :
        IHandle<CreateProduct>
    {
        private readonly IDomainRepository _domainRepository;

        public ProductCommandHandler(IDomainRepository domainRepository)
        {
            _domainRepository = domainRepository;
        }

        public IAggregate Handle(CreateProduct command)
        {
            var product = _domainRepository.GetById<Product>(command.Id);
            if (product.Id != Guid.Empty)
            {
                throw new ProductAlreadyExistsException(command.Id);
            }

            return Product.Create(command.Id, command.Name, command.Price);
        }
    }
}