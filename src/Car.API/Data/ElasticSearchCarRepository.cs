using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Nest;

namespace Car.API.Data
{
    public class ElasticSearchCarRepository : ICarRepository
    {
        private readonly ElasticClient _elasticClient;

       
        public ElasticSearchCarRepository(ElasticClient elasticClient)
        {
            _elasticClient = new ElasticClient(new ConnectionSettings(new Uri("http://mac.localhost:9200"), "cars"));//= elasticClient;
        }


        public async Task<string> SaveAsync(Entities.Car car)
        {
            var result = await _elasticClient.IndexAsync(car);
            return result.Id;
        }


        public async Task<Entities.Car> GetAsync(Guid id)
        {
            var result = await _elasticClient.GetAsync<Entities.Car>(id.ToString());
            return result.Source;
        }


        public async Task<bool> DeleteAsync(Guid id)
        {
            var result = await _elasticClient.DeleteAsync<Entities.Car>(id.ToString(), x => x.Type("car"));
            return result.Found;
        }

        public async Task<IEnumerable<Entities.Car>> ListAsync()
        {
            var result = await _elasticClient.SearchAsync<Entities.Car>(search => search
                .From(0)
                .MatchAll());

            return result.Documents;
        }

        public async Task<IEnumerable<Entities.Car>> SearchAsync(string query)
        {
            var result = await _elasticClient.SearchAsync<Entities.Car>(search => search
                .From(0)
                .QueryString(query));

            return result.Documents;
        }

    }
}