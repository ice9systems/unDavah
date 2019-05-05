using System;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.undavah.unDavah_PoC
{
    class ClipboardInfo
    {
        public String Text { get; private set; }
        public bool setCurrentClipboardText()
        {
            if (Clipboard.ContainsText()) {
                Text = Clipboard.GetText();
                return true;
            }
            return false;
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
    }

}
