using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Nest;
using Newtonsoft.Json.Converters;

namespace Car.API.Data
{
    public class ElasticClientFactory
    {

       

        public static ElasticClient CreateElasticClient()
        {
            var uri = new Uri(ConfigurationManager.ConnectionStrings["ElasticSearch"].ConnectionString);
            var settings = new ConnectionSettings(uri, "cars");
            settings.AddContractJsonConverters(type => typeof(Enum).IsAssignableFrom(type)
                ? new StringEnumConverter()
                : null);
            return new ElasticClient(settings);
        }
                    

    }
}