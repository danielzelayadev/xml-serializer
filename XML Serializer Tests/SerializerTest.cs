using System;
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

        [TestMethod]
        public void Serialize_Date_Only()
        {
            var serializer = new XMLSerializer();

            var date = new DateTime(2015, 10, 6);

            var result = serializer.Serialize(date);

            Assert.AreEqual("<date><year>2015</year><month>10</month><day>6</day>" +
                            "<hour>0</hour><minute>0</minute><second>0</second></date>", result, 
                "Should return the correct xml representation.");
        }

        [TestMethod]
        public void Serialize_Date_And_Time()
        {
            var serializer = new XMLSerializer();

            var date = new DateTime(2015, 10, 6, 3, 30, 15);

            var result = serializer.Serialize(date);

            Assert.AreEqual("<date><year>2015</year><month>10</month><day>6</day>" +
                            "<hour>3</hour><minute>30</minute><second>15</second></date>", result,
                "Should return the correct xml representation.");
        }

        [TestMethod]
        public void Serialize_Class()
        {
            var serializer = new XMLSerializer();

            var person = new Person
            {
                 Id = 100,
                 FirstName = "Daniel",
                 LastName = "Zelaya",
                 Age = 20,
                 Drives = true,
                 BloodType = 'O',
                 Birthday = new DateTime(1995, 10, 1, 23, 45, 0)
            };

            var result = serializer.Serialize(person);

            Assert.AreEqual("<Person>" +
                            "<Id>100</Id><FirstName>Daniel</FirstName><LastName>Zelaya</LastName><Age>20</Age>" +
                            "<Drives>True</Drives><BloodType>O</BloodType><Birthday>10/1/1995 11:45:00 PM</Birthday>" +
                            "</Person>", 
                            result, "Should return the correct xml representation.");
        }

        [TestMethod]
        public void Serialize_Null()
        {
            var serializer = new XMLSerializer();

            var result = serializer.Serialize(null);

            Assert.AreEqual("", result, "Should return an empty string.");
        }


        [TestMethod]
        public void Serialize_Array_Of_Ints()
        {
            var serializer = new XMLSerializer();

            var ints = new[] { 100, 50 };

            var result = serializer.Serialize(ints);

            Assert.AreEqual("<array><num>100</num><num>50</num></array>", result, 
                "Should return the correct xml representation.");
        }

        [TestMethod]
        public void Serialize_Array_Of_Longs()
        {
            var serializer = new XMLSerializer();

            var longs = new[] { 100L, 50L };

            var result = serializer.Serialize(longs);

            Assert.AreEqual("<array><num>100</num><num>50</num></array>", result,
                "Should return the correct xml representation.");
        }

        [TestMethod]
        public void Serialize_Array_Of_Floats()
        {
            var serializer = new XMLSerializer();

            var floats = new[] { 100.5F, 50.5939F };

            var result = serializer.Serialize(floats);

            Assert.AreEqual("<array><num>100.5</num><num>50.5939</num></array>", result,
                "Should return the correct xml representation.");
        }

        [TestMethod]
        public void Serialize_Array_Of_Doubles()
        {
            var serializer = new XMLSerializer();

            var doubles = new[] { 100.5, 50.833 };

            var result = serializer.Serialize(doubles);

            Assert.AreEqual("<array><num>100.5</num><num>50.833</num></array>", result,
                "Should return the correct xml representation.");
        }

        [TestMethod]
        public void Serialize_Array_Of_Chars()
        {
            var serializer = new XMLSerializer();

            var chars = new[] { 'a', 'b', 'c' };

            var result = serializer.Serialize(chars);

            Assert.AreEqual("<array><char>a</char><char>b</char><char>c</char></array>", result,
                "Should return the correct xml representation.");
        }

        [TestMethod]
        public void Serialize_Array_Of_Strings()
        {
            var serializer = new XMLSerializer();

            var strings = new[] { "Hola", "Hello, world!", "", " " };

            var result = serializer.Serialize(strings);

            Assert.AreEqual("<array><string>Hola</string><string>Hello, world!</string>" +
                            "<string></string><string> </string></array>", result,
                "Should return the correct xml representation.");
        }

        [TestMethod]
        public void Serialize_Array_Of_Bools()
        {
            var serializer = new XMLSerializer();

            var bools = new[] { true, false, false, true, true };

            var result = serializer.Serialize(bools);

            Assert.AreEqual("<array><bool>True</bool><bool>False</bool><bool>False</bool><bool>True</bool>" +
                            "<bool>True</bool></array>", result,
                "Should return the correct xml representation.");
        }

        [TestMethod]
        public void Serialize_Array_Of_Dates()
        {
            var serializer = new XMLSerializer();

            var dates = new[]
            {
                new DateTime(2000, 12, 5),
                new DateTime(2015, 2, 2, 8, 5, 10),
                new DateTime(2003, 10, 1, 11, 11, 11),
                new DateTime(1900, 7, 12), 
            };

            var result = serializer.Serialize(dates);

            var expected = "<array>" + 

                           "<date>" + 
                           "<year>2000</year><month>12</month><day>5</day><hour>0" +
                           "</hour><minute>0</minute><second>0</second>" +
                           "</date>" +

                           "<date>" + 
                           "<year>2015</year><month>2</month><day>2</day>" +
                           "<hour>8</hour><minute>5</minute><second>10</second>" +
                           "</date>" +

                           "<date>" + 
                           "<year>2003</year><month>10</month><day>1</day>" +
                           "<hour>11</hour><minute>11</minute><second>11</second>" +
                           "</date>" +

                           "<date>" +
                           "<year>1900</year><month>7</month><day>12</day>" +
                           "<hour>0</hour><minute>0</minute><second>0</second>" +
                           "</date>" +

                           "</array>";

            Assert.AreEqual(expected, result, "Should return correct xml representation.");
        }

        [TestMethod]
        public void Serialize_Array_Of_Classes()
        {
            var serializer = new XMLSerializer();

            var people = new[] {
                new Person
                {
                    Id = 100
                },
                new Person(),
                new Person
                {
                    Id = 200,
                    FirstName = "Lolis"
                }
            };

            var result = serializer.Serialize(people);

            var expected = "<array>" +

                           "<Person>" +
                           "<Id>100</Id><FirstName></FirstName><LastName></LastName><Age>0</Age>" +
                           "<Drives>False</Drives><BloodType>"+'\0'+"</BloodType><Birthday>1/1/0001 12:00:00 AM</Birthday>" +
                           "</Person>" +

                           "<Person>" +
                           "<Id>0</Id><FirstName></FirstName><LastName></LastName><Age>0</Age>" +
                           "<Drives>False</Drives><BloodType>" + '\0' + "</BloodType><Birthday>1/1/0001 12:00:00 AM</Birthday>" +
                           "</Person>" +

                           "<Person>" +
                           "<Id>200</Id><FirstName>Lolis</FirstName><LastName></LastName><Age>0</Age>" +
                           "<Drives>False</Drives><BloodType>" + '\0' + "</BloodType><Birthday>1/1/0001 12:00:00 AM</Birthday>" +
                           "</Person>" +

                           "</array>";

            Assert.AreEqual(expected, result, "Should return correct xml representation.");
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
