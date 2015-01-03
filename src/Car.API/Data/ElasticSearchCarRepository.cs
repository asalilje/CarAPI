using System;
using System.Collections.Generic;
using System.Configuration;
using Nest;
using Newtonsoft.Json.Converters;

namespace Car.API.Data
{
    public class ElasticSearchCarRepository : ICarRepository
    {
        private readonly ElasticClient _elasticClient;

       
        public ElasticSearchCarRepository(ElasticClient elasticClient)
        {
            _elasticClient = elasticClient;

        }

       
        public string Save(Entities.Car car)
        {
            var result = _elasticClient.Index(car);
            return result.Id;
        }

            
        public Entities.Car Get(Guid id)
        {
            var result = _elasticClient.Get<Entities.Car>(id.ToString());
            return result.Source;
        }


        public bool Delete(Guid id)
        {
            var result = _elasticClient.Delete<Entities.Car>(id.ToString(), x => x.Type("car"));
            return result.Found;
        }

        public IEnumerable<Entities.Car> List()
        {
            var result = _elasticClient.Search<Entities.Car>(search => search
                .From(0)
                .MatchAll());

            return result.Documents;
        }

        public IEnumerable<Entities.Car> Search(string query)
        {
            var result = _elasticClient.Search<Entities.Car>(search => search
                .From(0)
                .QueryString(query));

            return result.Documents;
        }

    }
}