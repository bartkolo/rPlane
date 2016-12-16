using Microsoft.VisualStudio.TestTools.UnitTesting;
using rPlaneLibrary;

namespace rPlaneUnitTest
{
    [TestClass]
    public class UnitTestAdsbMessageDecoder
    {
        [TestMethod]
        public void test_decode_downlink_format()
        {
            var msg = new AdsbMessage("8D4840D6202CC371C32CE0576098");
            var result = msg.DecodeMessageToInt(0, 4);
            Assert.AreEqual(result, 17, $"Result is not as expected. Got: {result}, expected: 17");
        }

        [TestMethod]
        public void test_decode_type_code()
        {
            var msg = new AdsbMessage("8D4840D6202CC371C32CE0576098");
            Assert.AreEqual(msg.TypeCode, 4, $"Result is not as expected. Got: {msg.TypeCode}, expected: 4");
        }

        [TestMethod]
        public void test_get_aircraft_id()
        {
            var msg = new AdsbMessage("8D4840D6202CC371C32CE0576098");
            Assert.AreEqual(msg.AircraftId, "KLM1023_", $"Result is not as expected. Got: {msg.AircraftId}, expected: KLM1023_");
        }

        [TestMethod]
        public void test_get_odd_even()
        {
            var even = new AdsbMessage("8D40621D58C382D690C8AC2863A7");
            var odd = new AdsbMessage("8D40621D58C386435CC412692AD6");
            Assert.AreEqual(even.Odd, false, $"Result is not as expected. Got: {even.Odd}, expected: false");
            Assert.AreEqual(odd.Odd, true, $"Result is not as expected. Got: {odd.Odd}, expected: true");
        }

        [TestMethod]
        public void test_get_lat_lon()
        {
            var lon_lat = new AdsbMessage("8D40621D58C382D690C8AC2863A7");
            lon_lat.GetLatitudeLongitude("8D40621D58C382D690C8AC2863A7", "8D40621D58C386435CC412692AD6");
        }
    }
}