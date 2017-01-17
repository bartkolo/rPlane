using System.Data.Entity;

namespace rPlaneLibrary.Database
{
    public class RPlaneDbHendler
    {
        public void AddAdsbPackage(string package)
        {
            using (var db = new RPlaneContext())
            {
                var message = new AdsbPackage { Message = package };

                db.AdsbPackages.Add(message);
                db.SaveChanges();
            }
        }
    }

    public class RPlaneContext : DbContext
    {
        public DbSet<AdsbPackage> AdsbPackages { get; set; }
    }
}