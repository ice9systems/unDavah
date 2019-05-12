using System.Runtime.Serialization;

namespace com.undavah.unDavah_PoC
{

    [DataContract]
    class EmphasisRules
    {
        [DataMember]
        public Rule[] doFirst { get; set; }
        [DataMember]
        public Rule[] rules { get; set; }
        [DataMember]
        public Rule[] doLast { get; set; }


        [DataContract]
        public class Rule
        {
            [DataMember]
            public string description { get; set; }
            [DataMember]
            public string matchTo { get; set; }
            [DataMember]
            public string replaceTo { get; set; }
            [DataMember]
            public string fgcolor { get; set; }
            [DataMember]
            public string bgcolor { get; set; }
            [DataMember]
            public Warn warn { get; set; }
        }

        [DataContract]
        public class Warn
        {
            [DataMember]
            public string type { get; set; }
        }

    }
}
