using System.Dynamic;
using Cassandra;

namespace rPlaneLibrary.Cassandra
{   
    public class CassandraDb<T> :TableProperty
    {
        public enum PlaneColumn
        {
            AircraftId,
            OddMessage,
            OddStatus,
            EvenMessage,
            EvenStatus,
            Altitude,
            Longitude,
            Latitude,
            ICAO
        }

        public ISession Session { get; set; }
        public Cluster CasandraCluster { get; set; }
        //public string KeySpace;
        //public  string TableName { get; set; }

        public void CassandraDbInitailization(T t)
        {

            KeySpace = t.GetType().GetProperty("ss");
            TableName = tableName;

            CasandraCluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            Session = CasandraCluster.Connect(KeySpace);
            if (!CheckTableExist(CasandraCluster)) CreatePlaneTable(Session);
        }
        

        public bool CheckTableExist(Cluster cluster)
        {
            var ks = cluster.Metadata.GetKeyspace(KeySpace);
            var table = ks.GetTableMetadata(TableName);
            return table != null;
        }

        public void UpdateRow(PlaneColumn column, T t, object value, string key)
        {
            var ps = Session.Prepare($"UPDATE {tableName} SET {column}=? WHERE key=?");
            var statement = ps.Bind(value, key);
            Session.Execute(statement);
        }

        public void InsertNewPlane(string aircraftId, string oddMessage, bool oddStatus, string evenMessage, bool evenStatus, int altitude,
double longitude, double latitude, string icao)
        {
            Session.Execute(
             "insert into plane (            AircraftId,OddMessage,OddStatus,EvenMessage,EvenStatus,Altitude,Longitude,Latitude,ICAO)" +
             $" values ({aircraftId}, {oddMessage}, {oddStatus} ,{evenMessage},{evenStatus},{altitude},{longitude},{latitude},{icao})");
        }

        public void CreatePlaneTable(ISession session)
        {
            const string createQuery = @"CREATE TABLE demo.plane (""AircraftId"" text,""OddMessage"" text,""OddStatus"" boolean,""EvenMessage"" text,
	                                ""EvenStatus"" boolean,	""Altitude"" int, ""Longitude"" double, ""Latitude"" double, ""ICAO"" text,
	                                PRIMARY KEY(""AircraftId"")); ";
            session.Execute(createQuery);
        }

    }
}