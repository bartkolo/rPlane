using Cassandra;

namespace ConsoleApplicationTest
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Cluster cluster = Cluster.Builder().AddContactPoint("127.0.0.1").Build();
            ISession session = cluster.Connect("demo");
            //session.CreateKeyspace("demo"); 
            
            if(!CheckTableExist(cluster))CreatePlaneTable(session);

            session.Execute(
                "insert into users (lastname, age, city, email, firstname) values ('Jones2', 35, 'Austin', 'bob@example.com', 'Bob')");

            //var ps = session.Prepare("UPDATE user_profiles SET birth=? WHERE key=?");

            //...bind different parameters every time you need to execute
            //var statement = ps.Bind(new DateTime(1942, 11, 27), "hendrix");
            //Execute the bound statement with the provided parameters
            //session.Execute(statement);
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