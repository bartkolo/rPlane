using Cassandra;
using rPlaneLibrary.Cassandra;

namespace ConsoleApplicationTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            //Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            //ISession session = cluster.Connect("demo");
            //session.CreateKeyspace("demo");

            //if (!CheckTableExist(cluster)) CreatePlaneTable(session);

            CPlane plane = new CPlane();
            plane.InsertNewPlane("dasd", "asdads", true, "dasda", false, 222, 2222, 23, "22");


            //session.Execute(
            //    "insert into users (lastname, age, city, email, firstname) values ('Jones2', 35, 'Austin', 'bob@example.com', 'Bob')");
        }

        public static bool CheckTableExist(Cluster cluster)
        {
            KeyspaceMetadata ks = cluster.Metadata.GetKeyspace("demo");
            TableMetadata table = ks.GetTableMetadata("users");
            return table != null;
        }

        public static void CreatePlaneTable(ISession session)
        {
            string create_query =
                @"CREATE TABLE demo.plane (""AircraftId"" text,""OddMessage"" text,""OddStatus"" boolean,""EvenMessage"" text,
	                                ""EvenStatus"" boolean,	""Altitude"" int, ""Longitude"" double, ""Latitude"" double, ""ICAO"" text,
	                                PRIMARY KEY(""AircraftId"")); ";
            session.Execute(create_query);
        }
    }
}