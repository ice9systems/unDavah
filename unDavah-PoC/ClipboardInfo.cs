using System;
using System.Windows;

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
                this.Text = Clipboard.GetText();
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
            if (Clipboard.GetText() == Text)
            {
                return true;
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
            else if(Text == String.Empty)
            {
                return "The ClipBoard is empty.";
            }else if(Text.Contains(Environment.NewLine))
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
