using Cassandra;
using Cassandra.Mapping;
using System.Collections.Generic;

namespace rPlaneLibrary.Cassandra
{
    public class CassandraDb<T>
    {
        public ISession Session { get; set; }
        public Cluster CasandraCluster { get; set; }
        public IMapper Mapper { get; set; }
        public string KeySpace;
        public string TableName;

        public CassandraDb(string keyspace, string tableName)
        {
            KeySpace = keyspace;
            TableName = tableName;
            CasandraCluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            Session = CasandraCluster.Connect(KeySpace);
            Mapper = new Mapper(Session);
            //if (!CheckTableExist(CasandraCluster)) CreatePlaneTable(Session);
        }

        public bool CheckTableExist(Cluster cluster)
        {
            var ks = cluster.Metadata.GetKeyspace(KeySpace);
            var table = ks.GetTableMetadata(TableName);
            return table != null;
        }

        public void ExecuteQuery(string query)
        {
            Session.Execute(query);
        }

        public IEnumerable<T> Select(string query)
        {
            return Mapper.Fetch<T>(query);
        }
    }
}