using System;
using BusyShopCQRS.Helpers;
using BusyShopCQRS.Service.Documents;
using Nest;

namespace BusyShopCQRS.Service
{
    internal class Indexer
    {
        private readonly ElasticClient _esClient;

        public Indexer()
        {
            var settings = new ConnectionSettings(new Uri("http://localhost:9200"));
            settings.SetDefaultIndex(ElasticClientBuilder.IndexName);
            _esClient = new ElasticClient(settings);
        }

        public TDocument Get<TDocument>(Guid id) where TDocument : class
        {
            return _esClient.Get<TDocument>(id.ToString()).Source;
        }

        public void Index<TDocument>(TDocument document) where TDocument : class
        {
            _esClient.Index(document, y => y.Index(ElasticClientBuilder.IndexName));
        }

        public void Init()
        {
            _esClient.CreateIndex(ElasticClientBuilder.IndexName, y => y
                .AddMapping<Basket>(m => m.MapFromAttributes())
                .AddMapping<Customer>(m => m.MapFromAttributes())
                .AddMapping<Product>(m => m.MapFromAttributes()));
        }
    }
}