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
            //plane.CheckRowExist("2d");
            plane.InsertNewPlane("223", "2d", "asdads", true, "dasda", false, 222, 2222, 23);
            plane.UpdateRow(CPlane.PlaneColumn.Latitude, 9999.0, "2d");


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
                @"CREATE TABLE demo.plane (""aircraftid"" text,""oddmessage"" text,""oddstatus"" boolean,""evenmessage"" text,
	                                ""evenstatus"" boolean,	""altitude"" int, ""longitude"" double, ""latitude"" double, ""icao"" text,
	                                PRIMARY KEY(""aircraftid"")); ";
            session.Execute(create_query);
        }
    }
}