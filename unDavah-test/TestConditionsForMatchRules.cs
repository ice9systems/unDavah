using System;
using System.Text.RegularExpressions;
using com.undavah.unDavah_PoC;

namespace com.undavah.undavah.Tests
{
    internal class TestConditionsForMatchRules
    {
        public String Source { get; set; }
        public String BgColor { get; private set; }
        public String FgColor { get; private set; }
        public String ReplaceTo { get; set; }
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
                    ReplaceTo = aRule.replaceTo;
                    HitCount++;
                }
            }
        }
        public String GetExpect()
        {
            return "<texts><style bgcolor='" + BgColor + "' fgcolor='" +
                FgColor + "'>" + ReplaceTo + "</style></texts>";
        }
    }
}
