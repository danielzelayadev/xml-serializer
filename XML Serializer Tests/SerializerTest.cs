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
            var serializer = new XMLSerializer();

            int[] testInts = new[] { 100, 50, 10 -10, 2, 0, 1000, 100000, -1000, -2 };

            foreach (var testInt in testInts)
            {
                string result = serializer.Serialize(testInt);

                Assert.AreEqual("<int>"+testInt+"</int>", result, "Should return the correct xml representation.");
            }
        }

    }
}
