using System;
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
            var result =msg.DecodeMessageToInt(0, 4);
            Assert.AreEqual(result,17, $"Result is not as expected. Got: {result}, expected: 17");
        }
    }
}
