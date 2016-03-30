using System.Collections;

namespace XML_Serializer
{
    public class XMLSerializer
    {
        public string Serialize(object obj)
        {
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

            return "";
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
