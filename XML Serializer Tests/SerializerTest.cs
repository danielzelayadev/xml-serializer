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
            Serialize(new[] { 100, 50, 10, -10, 2, 0, 1000, 100000, -1000, -2 }, "num");
        }

        [TestMethod]
        public void Serialize_Long()
        {
            Serialize(new[] { 100000000000000L, 50000000000000L, 1000000000000000000L,
                                     -10000000000000L, -20000000000000000L }, "num");
        }

        [TestMethod]
        public void Serialize_Float()
        {
            Serialize(new[] { 100.50f, 50.22f, 10.452f, -10.2031f, 2.393393f, 
                                     0.5f, 1000.39438f, 100000.1f, -1000.453f }, "num");
        }

        [TestMethod]
        public void Serialize_Double()
        {
            Serialize(new[] { 10001.10010110011, 1.39848498392, 
                                     -45.384482938383838, 20.299293111 }, "num");
        }

        [TestMethod]
        public void Serialize_String()
        {
            Serialize(new[] { "Hello!!!!", "h", "Hey", "ddijeufn3893848", "Lorem ipsum lolis" }, "string");
        }

        [TestMethod]
        public void Serialize_Char()
        {
            Serialize(new[] { 'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'k' }, "char");
        }

        [TestMethod]
        public void Serialize_Bool()
        {
            Serialize(new[] { true, false }, "bool");
        }

        private void Serialize<T>(IEnumerable<T> testValues, string tag)
        {
            var serializer = new XMLSerializer();

            foreach (var testVal in testValues)
            {
                var result = serializer.Serialize(testVal);

                Assert.AreEqual("<"+tag+">" + testVal + "</"+tag+">", result, 
                                "Should return the correct xml representation.");
            }
        }


    }
}
