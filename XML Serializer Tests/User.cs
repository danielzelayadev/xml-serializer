using XML_Serializer;

namespace XML_Serializer_Tests
{
    class User
    {
        public string Username { get; set; }

        [XMLName("Pass")]
        public string Password { get; set; }

        public int Points { get; set; }

        [XMLName("MUP")]
        public string MegaUltraProp { get; set; }
    }

}
