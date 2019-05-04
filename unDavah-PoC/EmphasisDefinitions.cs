using System;
using System.IO;
using System.Text;
using System.Runtime.Serialization.Json;

namespace com.undavah.unDavah_PoC
{
    class EmphasisDefinitions
    {
        //強調定義開始
        //--------------------------------------
        private const String rulesStr = @"
                {
                    ""doFirst"": [
                        {
                            ""description"": ""whiteSpace(0x20)"",
                            ""matchTo"": ""\\x20"",
                            ""replaceTo"": ""\u2423"",
                            ""fgcolor"": ""#00ffff"",
                            ""bgcolor"": ""#000000"",
                            ""warn"":  {}
                        }
                    ],
                    ""rules"": [
                        {
                            ""description"": ""Tab"",
                            ""matchTo"": ""\\t"",
                            ""replaceTo"": ""[\\t]"",
                            ""fgcolor"": ""#000000"",
                            ""bgcolor"": ""#ffff00"",
                            ""warn"": {
                                ""type"": ""CTRL""
                            }
                        }
                    ],
                    ""doLast"": [
                        {
                            ""description"": ""newline"",
                            ""matchTo"": ""\\r\\n"",
                            ""replaceTo"": ""↵\r\n"",
                            ""fgcolor"": ""#00ffff"",
                            ""bgcolor"": ""#ff0000"",
                            ""warn"":  {}
                        }
                    ]
                }
                ";
        //強調定義終了
        //--------------------------------------

        static public EmphasisRules GetEmphasisRules()
        {
            EmphasisRules rules;

            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(EmphasisRules));
            using (MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(rulesStr)))
            {
                rules = (EmphasisRules)serializer.ReadObject(ms);
            }

            return rules;
        }

    }
}
