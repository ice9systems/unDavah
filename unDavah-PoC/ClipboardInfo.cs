using System;
using System.Windows;
using System.Text.RegularExpressions;

namespace com.undavah.unDavah_PoC
{
    class ClipboardInfo
    {
        public String Text { get; private set; }
        public bool ContainsText { get; private set; }
        public bool SetCurrentClipboardText()
        {
            if (Clipboard.ContainsText())
            {
                this.Text = UDVClipboardLib.UDVGetClipboardString();
                this.ContainsText = true;
                return true;
            }
            else
            {
                this.ContainsText = false;
                this.Text = "";
                return false;
            }
        }

        public bool isUpToDate()
        {
            String currentClipboardText = "";
            if (Clipboard.ContainsText() && this.ContainsText)
            {
                currentClipboardText = UDVClipboardLib.UDVGetClipboardString();
                if (currentClipboardText == this.Text)
                {
                    return true;
                }
                else
                {
                    String now = UDVClipboardLib.UDVGetClipboardString();

                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        public String WarnMessage()
        {
            if(ContainsText == false)
            {
                return "The clipboard contains not text.";
            }
            else if (Text == String.Empty)
            {
                return "The ClipBoard is empty.";
            }
            else if (Regex.IsMatch(Regex.Replace(Text, Environment.NewLine, ""),
                "[\x00-\x1f\x7f]"))
            {
                return "*** The clipboard contains CONTROL CHARACTER(S) ***" +
                    Environment.NewLine + "Please check it carefully.";
            }
            else if(Text.Contains(Environment.NewLine))
            {
                return "The clipboard contains multiple lines of text. " +
                    "Would you like to continue?";
            }
            else
            {
                return "Please confirm, The contents of your clipboard " +
                    "are as follows.";
            }
        }
    }
}
