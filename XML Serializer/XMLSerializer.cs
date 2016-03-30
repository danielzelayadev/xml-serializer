namespace XML_Serializer
{
    public class XMLSerializer
    {
        public string Serialize(object obj)
        {
            return IsNumber(obj) ? SerializeNumber(obj) : "";
        }

        private bool IsNumber(object obj)
        {
            return obj is int || obj is long || obj is float || obj is double;
        }

        private string SerializeNumber(object num)
        {
            return "<num>"+num.ToString()+"</num>";
        }
    }
}
