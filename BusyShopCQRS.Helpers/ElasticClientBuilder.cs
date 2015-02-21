using System;
using Nest;

namespace BusyShopCQRS.Helpers
{
    public static class ElasticClientBuilder
    {
        public static string IndexName = "BusyShopCQRS".ToLower();

        private static IElasticClient _esClient;

        public static IElasticClient EsClient => _esClient ?? (_esClient = BuildClient());

        public static IElasticClient BuildClient()
        {
            var elasticUrl = "ElasticUrl".GetConfigSetting(y => new Uri(y), () => new Uri("http://localhost:9200"));
            var settings = new ConnectionSettings(elasticUrl);
            settings.SetDefaultIndex(IndexName);            
            var esClient = new ElasticClient(settings);
            return esClient;
        }
    }
}
