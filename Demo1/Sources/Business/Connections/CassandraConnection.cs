using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Cassandra;

namespace Business.Connections
{
    public class CassandraConnection
    {
        private readonly Cluster _cluster;
        private readonly ISession _session;
        private const int RetryCount = 5;
        public const string KeySpace = "rdwdemo";

        private CassandraConnection()
        {
            _cluster = Cluster.Builder().AddContactPoint(Configuration.Instance.CassandraEndpoint).Build();
            var tryCount = 0;
            while (tryCount < RetryCount)
            {
                try
                {
                    _session = _cluster.Connect(KeySpace);
                    return;
                }
                catch (Exception e)
                {
                    tryCount++;
                }
            }
            throw new Exception(string.Format("Unable to connect to cassandra at '{0}'", Configuration.Instance.CassandraEndpoint));
        }

        private static CassandraConnection _connection;

        public static CassandraConnection GetInstance()
        {
            return _connection ?? (_connection = new CassandraConnection());
        }

        public CassandraConnection(string keyspace)
        {
            _cluster = Cluster.Builder().AddContactPoint(Configuration.Instance.CassandraEndpoint).Build();
            _session = _cluster.Connect(keyspace);
        }

        
        public void DefineUdt(params UdtMap[] mapping)
        {
            _session.UserDefinedTypes.Define(mapping);
        }

        public RowSet ExecuteReader(string query)
        {
            for (var i = 0; i < RetryCount; i++)
            {
                try
                {
                    return _session.Execute(query);
                }
                catch (Exception e)
                {
                    Thread.Sleep(500);
                }
            }
            throw new Exception("Error while executing cassandra query");
        }

        public RowSet ExecuteReader(IStatement query)
        {
            for (var i = 0; i < RetryCount; i++)
            {
                try
                {
                    return _session.Execute(query);
                }
                catch (Exception e)
                {
                    Thread.Sleep(500);
                }
            }
            throw new Exception("Error while executing cassandra query");
        }

        public void ExecuteNonReader(string query)
        {
            for (var i = 0; i < RetryCount; i++)
            {
                try
                {
                    _session.Execute(query);
                    return;
                }
                catch (Exception e)
                {
                    Thread.Sleep(500);
                }
            }
            throw new Exception("Error while executing cassandra query");
        }

        public void ExecuteNonReader(IStatement statement)
        {
            for (var i = 0; i < RetryCount; i++)
            {
                try
                {
                    _session.Execute(statement);
                    return;
                }
                catch (Exception e)
                {
                    Thread.Sleep(500);
                }
            }
            throw new Exception("Error while executing cassandra query");
        }

        public PreparedStatement PreparedStatement(string query)
        {
            return _session.Prepare(query);
        }

        public bool KeySpaceExists()
        {
            return _cluster.Metadata.GetKeyspaces().Any(ks => ks == KeySpace);
        }

        public List<string> GetTables()
        {
            return _cluster.Metadata.GetTables(KeySpace).ToList();
        }

        public void Disconnect()
        {
            _session.Dispose();
        }
    }
}
