using Car.API.Data;
using Nancy;
using Nancy.TinyIoc;
using Nest;
using Newtonsoft.Json.Converters;

namespace Car.API
{
    public class Bootstrapper: DefaultNancyBootstrapper
    {
        protected override void ConfigureApplicationContainer(TinyIoCContainer container)
        {
            var elasticRepository = new ElasticSearchCarRepository(ElasticClientFactory.CreateElasticClient());
            container.Register<ICarRepository, ElasticSearchCarRepository>(elasticRepository);
        }
    }
}