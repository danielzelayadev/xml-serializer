using System;
using System.Collections;
using System.Reflection;

namespace XML_Serializer
{
    public class XMLSerializer
    {
        public string Serialize(object obj)
        {
            if (obj == null)
                return "";

            if (IsNumber(obj))
                return SerializeNumber(obj);
            if (obj is string)
                return SerializeString(obj.ToString());
            if (obj is char)
                return SerializeChar((char)obj);
            if (obj is bool)
                return SerializeBool((bool) obj);
            if (IsNumberArray(obj))
                return SerializeNumberArray(obj as IEnumerable);
            if (obj is char[])
                return SerializeCharArray(obj as IEnumerable);
            if (obj is string[])
                return SerializeStringArray(obj as IEnumerable);
            if (obj is bool[])
                return SerializeBoolArray(obj as IEnumerable);
            if (obj is DateTime)
                return SerializeDate((DateTime)obj);
            if (obj is DateTime[])
                return SerializeDateArray(obj as IEnumerable);

            return SerializeOtherObject(obj);
        }

        private string SerializeOtherObject(object obj)
        {
            var xml = "";

            Type type = obj.GetType();

            var className = TrimToClassName(type.FullName);

            foreach (var propInfo in type.GetProperties())
            {
                var propName = propInfo.Name;
                var propValue = propInfo.GetValue(obj);

                xml += "<" + propName + ">" + propValue + "</" + propName + ">";
            }

            return "<" + className + ">" + xml + "</" + className + ">";
        }

        private string SerializeDateArray(IEnumerable dates)
        {
            var elements = "";

            foreach (var date in dates)
            {
                elements += SerializeDate((DateTime)date);
            }

            return "<array>" + elements + "</array>";
        }

        private string SerializeDate(DateTime date)
        {
            var year = "<year>" + date.Year + "</year>";
            var month = "<month>" + date.Month + "</month>";
            var day = "<day>" + date.Day + "</day>";
            var hour = "<hour>" + date.Hour + "</hour>";
            var minute = "<minute>" + date.Minute + "</minute>";
            var second = "<second>" + date.Second + "</second>";

            return "<date>" + year +  month + day + hour + minute + second + "</date>";
        }

        private string SerializeBoolArray(IEnumerable bools)
        {
            var elements = "";

            foreach (var b in bools)
            {
                elements += SerializeBool((bool)b);
            }

            return "<array>" + elements + "</array>";
        }

        private string SerializeStringArray(IEnumerable strings)
        {
            var elements = "";

            foreach (var str in strings)
            {
                elements += SerializeString((string)str);
            }

            return "<array>" + elements + "</array>";
        }

        private string SerializeCharArray(IEnumerable charArray)
        {
            var elements = "";

            foreach (var c in charArray)
            {
                elements += SerializeChar((char)c);
            }

            return "<array>" + elements + "</array>";
        }

        private string SerializeNumberArray(IEnumerable numArray)
        {
            var elements = "";

            foreach (var num in numArray)
            {
                elements += SerializeNumber(num);
            }

            return "<array>" + elements + "</array>";
        }

        private string SerializeBool(bool b)
        {
            return "<bool>" + b + "</bool>";
        }

        private string SerializeChar(char c)
        {
            return "<char>" + c + "</char>";
        }

        private string SerializeString(string str)
        {
            return "<string>" + str + "</string>";
        }

        private string SerializeNumber(object num)
        {
            return "<num>" + num.ToString() + "</num>";
        }

        private string TrimToClassName(string fullName)
        {
            var lastDotIndex = fullName.LastIndexOf(".");

            return fullName.Substring(lastDotIndex + 1);
        }

        private bool IsNumberArray(object obj)
        {
            return obj is int[] || obj is long[] || obj is float[] || obj is double[];
        }

        private bool IsNumber(object obj)
        {
            return obj is int || obj is long || obj is float || obj is double;
        }

    }
}
