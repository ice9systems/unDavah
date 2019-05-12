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
                            ""matchTo"": ""(\\x20)"",
                            ""replaceTo"": ""\u2423"",
                            ""fgcolor"": ""#00ffff"",
                            ""bgcolor"": ""#000000"",
                            ""warn"":  {}
                        },
                        {
                            ""description"": ""Semicolon"",
                            ""matchTo"": ""(?<!<[^>]*)(&#x3b;)"",
                            ""replaceTo"": ""$1"",
                            ""fgcolor"": ""#ffff00"",
                            ""bgcolor"": ""#000000"",
                            ""warn"": {
                            }
                        },
                        {
                            ""description"": ""DoubleQuote"",
                            ""matchTo"": ""(?<!<[^>]*)(&quot;)"",
                            ""replaceTo"": ""$1"",
                            ""fgcolor"": ""#ffd700"",
                            ""bgcolor"": ""#000000"",
                            ""warn"": {
                            }
                        },
                        {
                            ""description"": ""Brackets-4<>"",
                            ""matchTo"": ""(?<!<[^>]*)(&lt;|&gt;)"",
                            ""replaceTo"": ""$1"",
                            ""fgcolor"": ""#ffd700"",
                            ""bgcolor"": ""#000000"",
                            ""warn"": {
                            }
                        },
                        {
                            ""description"": ""Numbers"",
                            ""matchTo"": ""(?<!<[^>]*)(?<!&[a-zA-Z0-9#]*)([0-9])"",
                            ""replaceTo"": ""$1"",
                            ""fgcolor"": ""#0088ff"",
                            ""bgcolor"": ""#000000"",
                            ""warn"":  {}
                        },
                        {
                            ""description"": ""Uppercase"",
                            ""matchTo"": ""(?<!<[^>]*)(?<!&[a-zA-Z0-9#]*)([A-Z])"",
                            ""replaceTo"": ""$1"",
                            ""fgcolor"": ""#00ff33"",
                            ""bgcolor"": ""#000000"",
                            ""warn"":  {}
                        },
                        {
                            ""description"": ""Lowercase"",
                            ""matchTo"": ""(?<!<[^>]*)(?<!&[a-zA-Z0-9#]*)([a-z])"",
                            ""replaceTo"": ""$1"",
                            ""fgcolor"": ""#00dd33"",
                            ""bgcolor"": ""#000000"",
                            ""warn"":  {}
                        }
                    ],
                    ""rules"": [
                        {
                            ""description"": ""Ampersand"",
                            ""matchTo"": ""(?<!<[^>]*)(&amp;)"",
                            ""replaceTo"": ""$1"",
                            ""fgcolor"": ""#0088ff"",
                            ""bgcolor"": ""#000000"",
                            ""warn"": {
                            }
                        },
                        {
                            ""description"": ""Brackets-1()"",
                            ""matchTo"": ""(?<!<[^>]*)(\\x28|\\x29)"",
                            ""replaceTo"": ""$1"",
                            ""fgcolor"": ""#ff8c00"",
                            ""bgcolor"": ""#000000"",
                            ""warn"": {
                            }
                        },
                        {
                            ""description"": ""Brackets-2[]"",
                            ""matchTo"": ""(?<!<[^>]*)(\\x5b|\\x5d)"",
                            ""replaceTo"": ""$1"",
                            ""fgcolor"": ""#ffd700"",
                            ""bgcolor"": ""#000000"",
                            ""warn"": {
                            }
                        },
                        {
                            ""description"": ""Brackets-3{}"",
                            ""matchTo"": ""(?<!<[^>]*)(\\x7b|\\x7d)"",
                            ""replaceTo"": ""$1"",
                            ""fgcolor"": ""#ffd700"",
                            ""bgcolor"": ""#000000"",
                            ""warn"": {
                            }
                        },
                        {
                            ""description"": ""SingleQuote"",
                            ""matchTo"": ""(?<!<[^>]*)(\\x27)"",
                            ""replaceTo"": ""$1"",
                            ""fgcolor"": ""#ff8c00"",
                            ""bgcolor"": ""#000000"",
                            ""warn"": {
                            }
                        },
                        {
                            ""description"": ""Symbols"",
                            ""matchTo"":""(?<!<[^>]*)((\\x21)|(\\x24)|(\\x25)|(\\x2a)|(\\x2b)|(\\x2c)|(\\x2d)|(\\x2e)|(\\x2f)|(\\x3a)|(\\x3d)|(\\x3f)|(\\x40)|(\\x5f)|(\\x7c)|(\\x7e))"",
                            ""replaceTo"": ""$1"",
                            ""fgcolor"": ""#0088ff"",
                            ""bgcolor"": ""#000000"",
                            ""warn"": {
                            }
                        },
                        {
                            ""description"": ""Hash"",
                            ""matchTo"":""(?<!<[^>]*)(?<!&[a-zA-Z0-9#]*)(#)"",
                            ""replaceTo"": ""$1"",
                            ""fgcolor"": ""#0088ff"",
                            ""bgcolor"": ""#000000"",
                            ""warn"": {
                            }
                        },
                        {
                            ""description"": ""Backslash"",
                            ""matchTo"":""(?<!<[^>]*)(?<!&[a-zA-Z0-9#]*)(&#x5c;)"",
                            ""replaceTo"": ""$1"",
                            ""fgcolor"": ""#0088ff"",
                            ""bgcolor"": ""#000000"",
                            ""warn"": {
                            }
                        },
                        {
                            ""description"": ""Caret"",
                            ""matchTo"":""(?<!<[^>]*)(&#x5e;)"",
                            ""replaceTo"": ""$1"",
                            ""fgcolor"": ""#0088ff"",
                            ""bgcolor"": ""#000000"",
                            ""warn"": {
                            }
                        },
                        {
                            ""description"": ""BackQuote"",
                            ""matchTo"": ""(?<!<[^>]*)(\\x60)"",
                            ""replaceTo"": ""$1"",
                            ""fgcolor"": ""#ffFF00"",
                            ""bgcolor"": ""#000000"",
                            ""warn"": {
                            }
                        },
                        {
                            ""description"": ""Null"",
                            ""matchTo"": ""\\x00"",
                            ""replaceTo"": ""^@"",
                            ""fgcolor"": ""#ffff00"",
                            ""bgcolor"": ""#ff0000"",
                            ""warn"": {
                                ""type"": ""CTRL""
                            }
                        },
                        {
                            ""description"": ""Tab"",
                            ""matchTo"": ""\\t"",
                            ""replaceTo"": ""[\\t]"",
                            ""fgcolor"": ""#000000"",
                            ""bgcolor"": ""#ffff00"",
                            ""warn"": {
                                ""type"": ""CTRL""
                            }
                        },
                        {
                            ""description"": ""BackSpace"",
                            ""matchTo"": ""\\x08"",
                            ""replaceTo"": ""^H"",
                            ""fgcolor"": ""#00ffff"",
                            ""bgcolor"": ""#ff0000"",
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
