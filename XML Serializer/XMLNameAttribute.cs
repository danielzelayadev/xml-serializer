using System;

namespace XML_Serializer
{
    public class XMLNameAttribute : Attribute
    {
        public string Name { get; set; }

        public XMLNameAttribute(string name)
        {
            Name = name;
        }
    }
}
