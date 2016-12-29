using rPlaneLibrary.Database;

namespace ConsoleApplicationTest
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var ttt = new RPlaneDbHendler();
            ttt.AddAdsbPackage("test");
        }
    }
}