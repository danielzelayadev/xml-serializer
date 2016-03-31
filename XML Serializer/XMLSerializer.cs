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
                throw new ArgumentNullException();

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
                return SerializeCharArray((IEnumerable) obj);
            if (obj is string[])
                return SerializeStringArray((IEnumerable) obj);
            if (obj is bool[])
                return SerializeBoolArray((IEnumerable) obj);
            if (obj is DateTime)
                return SerializeDate((DateTime)obj);
            if (obj is DateTime[])
                return SerializeDateArray((IEnumerable) obj);

            return obj is object[] ? SerializeOtherObjectArray((IEnumerable) obj) : SerializeOtherObject(obj);
        }

        private string SerializeOtherObjectArray(IEnumerable objs)
        {
            var elements = "";

            foreach (var obj in objs)
            {
                elements += SerializeOtherObject(obj);
            }

            return "<array>" + elements + "</array>";
        }

        private string SerializeOtherObject(object obj)
        {
            var xml = "";

            var type = obj.GetType();

            var className = TrimToClassName(type.FullName);

            foreach (var propInfo in type.GetProperties())
            {
                var propName = propInfo.Name;
                var propValue = propInfo.GetValue(obj);

                if ( PropertyIsAValidClass(propInfo) && propValue != null )
                    xml += ReplaceRootTagName(SerializeOtherObject(propValue), propName);

                else 
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

        private bool PropertyIsAValidClass(PropertyInfo propInfo)
        {
            return propInfo.PropertyType.IsClass &&
                   propInfo.PropertyType.ToString() != "System.String" &&
                   propInfo.PropertyType.ToString() != "System.DateTime";
        }

        private string ReplaceRootTagName(string xml, string newName)
        {
            var openRootTagEndIndex = xml.IndexOf(">");

            var oldName = xml.Substring(1, openRootTagEndIndex - 1);

            return xml.Replace(oldName, newName); ;
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
