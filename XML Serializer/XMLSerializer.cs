using System;
using System.Collections;
using System.Reflection;

namespace XML_Serializer
{
    public class XMLSerializer
    {

        private delegate string SerializeCallback(object obj);

        public string Serialize(object obj)
        {
            if (obj == null)
                throw new ArgumentNullException();

            return GetRespectiveSerialization(obj);
        }


         ////////////////////////////////////////////////////////////////////////////////////////////////
        //                                                                                            //
       //                                   SERIALIZATIONS                                           //
      //                                                                                            //
     ////////////////////////////////////////////////////////////////////////////////////////////////
     

        private string SerializePrimitive(object obj, string tagName)
        {
            return "<" + tagName + ">" + obj + "</" + tagName + ">";
        }

        private string SerializeDate(DateTime date)
        {
            var year = "<year>" + date.Year + "</year>";
            var month = "<month>" + date.Month + "</month>";
            var day = "<day>" + date.Day + "</day>";
            var hour = "<hour>" + date.Hour + "</hour>";
            var minute = "<minute>" + date.Minute + "</minute>";
            var second = "<second>" + date.Second + "</second>";

            return "<date>" + year + month + day + hour + minute + second + "</date>";
        }

        private string SerializeOtherObject(object obj)
        {
            var xml = "";

            var type = obj.GetType();
            var className = TrimToClassName(type.FullName);
            var properties = type.GetProperties();

            foreach (var propInfo in properties)
                xml += SerializeProperty(propInfo, obj);

            return "<" + className + ">" + xml + "</" + className + ">";
        }

        private string SerializeProperty(PropertyInfo propInfo, object obj)
        {
            var propertyXml = "";

            var attrs = propInfo.GetCustomAttributes(true);
            var XMLName = GetXMLName(attrs);

            var propName = XMLName.Length > 0 ? XMLName : propInfo.Name;
            var propValue = propInfo.GetValue(obj);

            if (propValue is object[])
                propertyXml = ReplaceRootTagName( GetArraySerialization(propValue), propName );

            else if (PropertyIsAValidClass(propInfo) && propValue != null)
                propertyXml = ReplaceRootTagName( SerializeOtherObject(propValue), propName );

            else propertyXml = "<" + propName + ">" + propValue + "</" + propName + ">";

            return propertyXml;
        }

        private string SerializeArray(IEnumerable objs, SerializeCallback sc)
        {
            var elements = "";

            foreach (var obj in objs)
                elements += sc(obj);

            return "<array>" + elements + "</array>";
        }


          ////////////////////////////////////////////////////////////////////////////////////////////////
         //                                                                                            //
        //                                       HELPERS                                              //
       //                                                                                            //
      ////////////////////////////////////////////////////////////////////////////////////////////////


        private string GetRespectiveSerialization(object obj)
        {
            if (obj.GetType().IsArray)
                return GetArraySerialization(obj);

            if (IsNumber(obj))
                return SerializePrimitive(obj, "num");

            if (obj is string)
                return SerializePrimitive(obj, "string");

            if (obj is char)
                return SerializePrimitive(obj, "char");

            if (obj is bool)
                return SerializePrimitive(obj, "bool");

            if (obj is DateTime)
                return SerializeDate((DateTime)obj);

            return SerializeOtherObject(obj);
        }

        private string GetArraySerialization(object obj)
        {
            if (IsNumberArray(obj))
                return SerializeArray((IEnumerable) obj, o => SerializePrimitive(o, "num"));

            if (obj is char[])
                return SerializeArray((IEnumerable)obj, o => SerializePrimitive(o, "char"));

            if (obj is string[])
                return SerializeArray((IEnumerable)obj, o => SerializePrimitive(o, "string"));

            if (obj is bool[])
                return SerializeArray((IEnumerable)obj, o => SerializePrimitive(o, "bool"));

            if (obj is DateTime[])
                return SerializeArray((IEnumerable)obj, o => SerializeDate((DateTime)o));
            
            return SerializeArray((IEnumerable)obj, SerializeOtherObject);
        }

        private bool IsNumber(object obj)
        {
            return obj is int || obj is long || obj is float || obj is double;
        }

        private bool IsNumberArray(object obj)
        {
            return obj is int[] || obj is long[] || obj is float[] || obj is double[];
        }

        private string TrimToClassName(string fullName)
        {
            var lastDotIndex = fullName.LastIndexOf(".");

            return fullName.Substring(lastDotIndex + 1);
        }

        private string ReplaceRootTagName(string xml, string newName)
        {
            var openRootTagEndIndex = xml.IndexOf(">");

            var oldName = xml.Substring(1, openRootTagEndIndex - 1);

            return xml.Replace(oldName, newName); ;
        }

        private bool PropertyIsAValidClass(PropertyInfo propInfo)
        {
            return propInfo.PropertyType.IsClass &&
                   propInfo.PropertyType.ToString() != "System.String" &&
                   propInfo.PropertyType.ToString() != "System.DateTime";
        }

        private string GetXMLName(object[] attrs)
        {
            foreach (var attr in attrs)
            {
                XMLNameAttribute xattr = attr as XMLNameAttribute;

                if (xattr != null) return xattr.Name;
            }

            return "";
        }


    }
}
