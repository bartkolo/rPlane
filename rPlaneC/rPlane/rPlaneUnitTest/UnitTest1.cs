using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using rPlaneLibrary;

namespace rPlaneUnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            AdsbMessage msg = new AdsbMessage("8D4840D6202CC371C32CE0576098");
            Console.WriteLine(msg.TestMessage);



            Assert.AreEqual(1,1);
        }
    }
}
