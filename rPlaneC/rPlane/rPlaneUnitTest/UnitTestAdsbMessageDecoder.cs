using Microsoft.VisualStudio.TestTools.UnitTesting;
using rPlaneLibrary;
using rPlaneLibrary.Decoder;

namespace rPlaneUnitTest
{
    [TestClass]
    public class UnitTestAdsbMessageDecoder
    {
        [TestMethod]
        public void test_decode_downlink_format()
        {
            var downlinkFormat = new AdsbMessage("8D4840D6202CC371C32CE0576098").GetDownlinkFormat();
            Assert.AreEqual(downlinkFormat, 17, $"Result is not as expected. Got: { downlinkFormat}, expected: 17");
        }

        [TestMethod]
        public void test_get_icao_address()
        {
            var icaoId = new AdsbMessage("8D4840D6202CC371C32CE0576098").GetIcaoId();
            Assert.AreEqual(icaoId, "4840D6", $"Result is not as expected. Got: { icaoId}, expected: 4840D6");
        }

        [TestMethod]
        public void test_decode_type_code()
        {
            var typeCode = new AdsbMessage("8D4840D6202CC371C32CE0576098").GetTypeCode(); ;
            Assert.AreEqual(typeCode, 4, $"Result is not as expected. Got: {typeCode}, expected: 4");
        }

        [TestMethod]
        public void test_get_aircraft_id()
        {
            var aircraftId = new AdsbMessage("8D4840D6202CC371C32CE0576098").GetAircraftId();
            Assert.AreEqual(aircraftId, "KLM1023_", $"Result is not as expected. Got: {aircraftId}, expected: KLM1023_");
        }

        [TestMethod]
        public void test_get_odd_even()
        {
            var even = new AdsbMessage("8D40621D58C382D690C8AC2863A7").GetOddEven();
            var odd = new AdsbMessage("8D40621D58C386435CC412692AD6").GetOddEven();
            Assert.AreEqual(even, false, $"Result is not as expected. Got: {even}, expected: false");
            Assert.AreEqual(odd, true, $"Result is not as expected. Got: {odd}, expected: true");
        }

        [TestMethod]
        public void test_get_lat_lon()
        {
            var planeLocationFirst = new AircraftPosition("8D75804B580FF2CF7E9BA6F701D0", "8D75804B580FF6B283EB7A157117");
            var latitude = planeLocationFirst.GetLatitude();
            var longitude = planeLocationFirst.GetLongitude();
            Assert.AreEqual(10.215774536132813, latitude, $"Result is not as expected. Got: {latitude}, expected: 10.215774536132813");
            Assert.AreEqual(123.88881877317266, longitude, $"Result is not as expected. Got: {longitude}, expected: 123.88881877317266");

            var planeLocationSecond = new AircraftPosition("8D40621D58C382D690C8AC2863A7", "8D40621D58C386435CC412692AD6");
            latitude = planeLocationSecond.GetLatitude();
            longitude = planeLocationSecond.GetLongitude();
            Assert.AreEqual(52.2572021484375, latitude, $"Result is not as expected. Got: {latitude}, expected: 52.2572021484375");
            Assert.AreEqual(3.91937255859375, longitude, $"Result is not as expected. Got: {longitude}, expected: 3.91937255859375");

            var planeLocationThird = new AircraftPosition("8dc0ffee58b986d0b3bd25000000", "8dc0ffee58b9835693c897000000");
            latitude = planeLocationThird.GetLatitude();
            longitude = planeLocationThird.GetLongitude();
            Assert.AreEqual(-49.777175903320312, latitude, $"Result is not as expected. Got: {latitude}, expected: -49.777175903320312");
            Assert.AreEqual(-10.710730301706462, longitude, $"Result is not as expected. Got: {longitude}, expected: -10.710730301706462");
        }

        [TestMethod]
        public void test_get_altitude()
        {
            var positionPlane = new AdsbMessage("8D40621D58C386435CC412692AD6");
            var altitude = positionPlane.GetAltitude();
            Assert.AreEqual(altitude, 38000, $"Result is not as expected. Got: {altitude}, expected: 38000");
        }
    }
}