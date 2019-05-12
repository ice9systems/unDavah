using System;
using System.Windows.Controls;
using System.Text.RegularExpressions;

namespace com.undavah.unDavah_PoC
{
    /// <summary>
    /// ClipboardDisplayControl.xaml の相互作用ロジック
    /// </summary>
    public partial class ClipboardDisplayControl : UserControl
    {
        private EmphasisRules emphasisRules;
        internal EmphasisRules EmphasisRules
        {
            get => this.emphasisRules;
            set
            {
                emphasisRules = value;
                this.clipboardContntArea.Parse();
            }
        }
        private String textForDisplay;
        public String TextForDisplay
        {
            get => this.textForDisplay;
            set
            {
                this.textForDisplay = value;
                this.clipboardContntArea.Text = DoEmphasis(value, emphasisRules);
#if DEBUG
                System.Diagnostics.Debug.WriteLine(this.clipboardContntArea.Text);
#endif
            }
        }

        private string DoEmphasis(String aStr, EmphasisRules rules)
        {
            // Escaping Characters Used in XML
            aStr = Regex.Replace(aStr, "&", "&amp;");
            aStr = Regex.Replace(aStr, "(?<!&amp);", "&#x3b;");
            aStr = Regex.Replace(aStr, "<", "&lt;");
            aStr = Regex.Replace(aStr, ">", "&gt;");
            aStr = Regex.Replace(aStr, "\"", "&quot;");

            // add a ROOT node
            aStr = aStr.Insert(0, "<texts>");
            aStr = aStr.Insert(aStr.Length , "</texts>");

            // regrx characters
            aStr = Regex.Replace(aStr, "\\\\", "&#x5c;");
            aStr = Regex.Replace(aStr, "\\^", "&#x5e;");

            // apply High Priority rules
            aStr = applyStyleTag(aStr, rules.doFirst);

            // apply other rules
            aStr = applyStyleTag(aStr, rules.rules);

            // Apply last rules
            aStr = applyStyleTag(aStr, rules.doLast);

            //// add a ROOT node
            //aStr = Regex.Replace(aStr, "^", "<texts>");
            //aStr = Regex.Replace(aStr, "$", "</texts>");

            return aStr;
        }

        private String applyStyleTag(String aStr, EmphasisRules.Rule[] rules)
        {
            String replaceStr;
            foreach (EmphasisRules.Rule aRule in rules)
            {
                replaceStr =
                    $"<style bgcolor='{aRule.bgcolor}' fgcolor='{aRule.fgcolor}'>" +
                    $"{aRule.replaceTo}</style>";
                aStr = Regex.Replace(aStr, aRule.matchTo, replaceStr);
            }
            return aStr;
        }
        public ClipboardDisplayControl()
        {
            InitializeComponent();
        }
    }
}
