using rPlaneLibrary;

namespace ConsoleApplicationTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var plane2 = new AircraftPosition("8dc0ffee58b986d0b3bd25000000", "8dc0ffee58b9835693c897000000");
            plane2.GetLatitude();
            plane2.GetLongitude();
        }
    }
}
