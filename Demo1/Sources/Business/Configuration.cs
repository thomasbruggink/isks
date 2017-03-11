using Akka.Configuration;
using System;
using System.IO;

namespace Business
{
    /// <summary>
    /// Contains all configuration objects from the conf file
    /// </summary>
    public class Configuration
    {
        public string ElasticSearchEndpoint { get; set; }
        public string CassandraEndpoint { get; set; }

        private static Configuration _instance;
        public static Configuration Instance => _instance ?? (_instance = new Configuration());

        public object HostingEnvironment { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Configuration"/> class.
        /// </summary>
        public Configuration()
        {
            var path = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + @"application.conf";

            var content = File.ReadAllText(path);
            var config = ConfigurationFactory.ParseString(content);

            ElasticSearchEndpoint = config.GetString("elasticsearchLog");
            CassandraEndpoint = config.GetString("cassandraEndpoint");
        }
    }
}
