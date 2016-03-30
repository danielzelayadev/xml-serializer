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

            return "";
        }

        private string SerializeString(string str)
        {
            return "<string>" + str + "</string>";
        }

        private string SerializeNumber(object num)
        {
            return "<num>" + num.ToString() + "</num>";
        }

        private bool IsNumber(object obj)
        {
            return obj is int || obj is long || obj is float || obj is double;
        }

    }
}
