using System;
using System.Text.RegularExpressions;
using com.undavah.unDavah_PoC;

namespace com.undavah.undavah.Tests
{
    internal class TestConditionsForMatchRules
    {
        public String Source { get; private set; }
        public String BgColor { get; private set; }
        public String FgColor { get; private set; }
        public String MatchTo { get; private set; }
        public String ReplaceTo { get; private set; }
        public int HitCount { get; private set; }

        public TestConditionsForMatchRules(String srcText, EmphasisRules.Rule[] rules)
        {
            Source = srcText;

            foreach (EmphasisRules.Rule aRule in rules)
            {
                if (Regex.IsMatch(Source, aRule.matchTo))
                {
                    BgColor = aRule.bgcolor;
                    FgColor = aRule.fgcolor;
                    MatchTo = aRule.matchTo;
                    ReplaceTo = aRule.replaceTo;
                    HitCount++;
                }
            }
        }
        public String GetExpect()
        {
            String pattern =  $"<texts><style bgcolor='{BgColor}' fgcolor='" +
                $"{FgColor}'>{ReplaceTo}</style></texts>";
            return Regex.Replace(Source, MatchTo, pattern);
        }
    }
}
