namespace rPlaneLibrary.Cassandra
{
    public abstract class AbstractCassandra<T>
    {
        public CassandraDb<T> CassandraDb;

        protected AbstractCassandra(string keySpace, string tableName)
        {
            CassandraDb = new CassandraDb<T>(keySpace, tableName);
        }

        public abstract void CreateTable();
    }
}