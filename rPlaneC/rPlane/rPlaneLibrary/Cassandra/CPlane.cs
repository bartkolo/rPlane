//http://www.datastax.com/dev/blog/csharp-driver-cassandra-new-mapper-linq-improvements

using Cassandra;

namespace rPlaneLibrary.Cassandra
{
    public class CPlane : AbstractCassandra<CPlane>
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

        #region Properties  
        public string AircraftId { get; set; }
        public string OddMessage { get; set; }
        public bool OddStatus { get; set; }
        public string EvenMessage { get; set; }
        public bool EvenStatus { get; set; }
        public int Altitude { get; set; }
        public double Longitude { get; set; }
        public double Latitude { get; set; }
        public string ICAO { get; set; }
        #endregion

        public CPlane() : base("demo", "plane")
        {
        }

        public override void CreateTable()
        {
            const string createQuery = @"CREATE TABLE demo.plane (""icao"" text ,""aircraftid"" text,""oddmessage"" text,""oddstatus"" boolean,""evenmessage"" text,
	                                ""evenstatus"" boolean,	""altitude"" int, ""longitude"" double, ""latitude"" double,
	                               PRIMARY KEY(""icao"")); ";
            CassandraDb.ExecuteQuery(createQuery);
        }

        public void InsertNewPlane(string icao, string aircraftId, string oddMessage, bool oddStatus, string evenMessage, bool evenStatus, int altitude, double longitude, double latitude)
        {
            CassandraDb.ExecuteQuery(
             @"insert into plane (icao,aircraftId,""oddmessage"",""oddstatus"",""evenmessage"",""evenstatus"",""altitude"",""longitude"",""latitude"")" +
             $" values ('{icao}','{aircraftId}', '{oddMessage}', {oddStatus} ,'{evenMessage}',{evenStatus},{altitude},{longitude},{latitude});");
        }

        public void UpdateRow(PlaneColumn column, object value, string key)
        {
            var ps = CassandraDb.Session.Prepare($"UPDATE {CassandraDb.TableName} SET {column}=? WHERE icao=?");
            var statement = ps.Bind(value, key);
            CassandraDb.Session.Execute(statement);
        }

        public RowSet CheckRowExist(string icao)
        {
            var result = CassandraDb.Session.Execute($"SELECT * FROM plane where icao='{icao}'");
            return result;
        }
    }
}