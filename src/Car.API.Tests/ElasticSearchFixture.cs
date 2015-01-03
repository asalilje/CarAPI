using System;
using System.Collections.Generic;
using System.Configuration;
using Car.API.Data;
using Nest;
using Newtonsoft.Json.Converters;

namespace Car.API.Tests
{
    public class ElasticSearchFixture: IDisposable
    {

        private readonly ElasticSearchCarRepository _repository;
        private readonly ElasticClient _elasticClient;
        private const string IndexName = "cars_test";

        public ElasticSearchCarRepository Repository { get { return _repository;} }

      

        public ElasticSearchFixture()
        {
            _elasticClient = CreateElasticClient();
            CreateTestIndex();
            _repository = new ElasticSearchCarRepository(_elasticClient);
        }

        private ElasticClient CreateElasticClient()
        {
            var uri = new Uri(ConfigurationManager.ConnectionStrings["ElasticSearch"].ConnectionString);
            var settings = new ConnectionSettings(uri, IndexName);
            settings.AddContractJsonConverters(type => typeof(Enum).IsAssignableFrom(type)
                ? new StringEnumConverter()
                : null);
            return new ElasticClient(settings);
        }

        private void CreateTestIndex()
        {
            DeleteIndex();
            _elasticClient.CreateIndex(IndexName, x => x
                .NumberOfReplicas(1)
                .NumberOfShards(1));
        }

       

        public void Dispose()
        {
            DeleteIndex();
        }

        private void DeleteIndex()
        {
            _elasticClient.DeleteIndex(IndexName);
        }
    }
}