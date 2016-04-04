using System;
using Neo4jClient;

namespace Business.Services
{
    /// <summary>
    ///     Connection to the neo4j database
    /// </summary>
    public class NeoConnection
    {
        private static NeoConnection _connection;

        /// <summary>
        ///     Setup the connection to the neo4j database based on the settings specified in the Settings object
        /// </summary>
        private NeoConnection(string username, string password, string host, int port)
        {
            // Build connectionstring for neo4j based on the settings
            var neoConnectionString = new Uri($"http://{username}:{password}@{host}:{port}/db/data");

            // Connect to the neo4j database
            GraphClient = new GraphClient(neoConnectionString);
            GraphClient.Connect();
        }

        /// <summary>
        ///     Instance of the GraphClient to connect to the neo4j database
        /// </summary>
        public GraphClient GraphClient { get; }

        public static NeoConnection GetConnection()
        {
            return _connection ?? (_connection = new NeoConnection("neo4j", "knownow", "localhost", 7474));
        }
    }
}