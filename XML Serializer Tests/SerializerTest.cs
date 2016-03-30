using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using XML_Serializer;

namespace XML_Serializer_Tests
{
    [TestClass]
    public class SerializerTest
    {
        [TestMethod]
        public void Serializer_Init ()
        {
            var serializer = new XMLSerializer();

            Assert.IsNotNull(serializer, "The Serializer should not be null when initialized");
        }

        [TestMethod]
        public void Serialize_Integer()
        {
            Serialize_Number(new[] { 100, 50, 10, -10, 2, 0, 1000, 100000, -1000, -2 });
        }

        [TestMethod]
        public void Serialize_Long()
        {
            Serialize_Number(new[] { 100000000000000L, 50000000000000L, 1000000000000000000L,
                                     -10000000000000L, -20000000000000000L });
        }

        [TestMethod]
        public void Serialize_Float()
        {
            Serialize_Number(new[] { 100.50f, 50.22f, 10.452f, -10.2031f, 2.393393f, 
                                     0.5f, 1000.39438f, 100000.1f, -1000.453f });
        }

        [TestMethod]
        public void Serialize_Double()
        {
            Serialize_Number(new[] { 10001.10010110011, 1.39848498392, 
                                     -45.384482938383838, 20.299293111 });
        }

        private void Serialize_Number<T>(IEnumerable<T> testValues)
        {
            var serializer = new XMLSerializer();

            foreach (var testVal in testValues)
            {
                var result = serializer.Serialize(testVal);

                Assert.AreEqual("<num>" + testVal + "</num>", result, 
                                "Should return the correct xml representation.");
            }
        }


    }
}
