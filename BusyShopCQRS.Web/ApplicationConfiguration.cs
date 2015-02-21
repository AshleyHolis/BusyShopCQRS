using BusyShopCQRS.Domain;
using BusyShopCQRS.Helpers;
using BusyShopCQRS.Infrastructure;

namespace BusyShopCQRS.Web
{
    public class ApplicationConfiguration
    {
        private static DomainEntry _domainEntry;
        public static DomainEntry DomainEntry => _domainEntry = _domainEntry ?? CreateDomainEntry();

        public static DomainEntry CreateDomainEntry()
        {
            var connection = EventStoreConnectionWrapper.Connect();
            var domainRepository = new EventStoreDomainRepository(connection);
            var domainEntry = new DomainEntry(domainRepository);
            return domainEntry;
        }
    }
}