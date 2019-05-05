using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.undavah.unDavah_PoC;

namespace com.undavah.undavah.Tests
{
    [TestClass]
    public class ClipboardInfoWarnMessagesTests
    {
        [TestMethod]
        public void Empty()
        {
            ClipboardInfo cbInfo = new ClipboardInfo();
            var privateCbInfo = new PrivateObject(cbInfo);
            privateCbInfo.SetProperty("ContainsText", true);
            privateCbInfo.SetProperty("Text", "");
            String warnMessage = cbInfo.WarnMessage();
            Assert.AreEqual(
                "The ClipBoard is empty.",
                warnMessage);
        }
        [TestMethod]
        public void Multiline()
        {
            ClipboardInfo cbInfo = new ClipboardInfo();
            var privateCbInfo = new PrivateObject(cbInfo);
            privateCbInfo.SetProperty("ContainsText", true);
            privateCbInfo.SetProperty("Text", Environment.NewLine);
            String warnMessage = cbInfo.WarnMessage();
            Assert.AreEqual(
                "The clipboard contains multiple lines of text. Would you like to continue?",
                warnMessage);
        }
        [TestMethod]
        public void SingleLine()
        {
            ClipboardInfo cbInfo = new ClipboardInfo();
            var privateCbInfo = new PrivateObject(cbInfo);
            privateCbInfo.SetProperty("ContainsText", true);
            privateCbInfo.SetProperty("Text", "Single line text.");
            String warnMessage = cbInfo.WarnMessage();
            Assert.AreEqual(
                "Please confirm, The contents of your clipboard are as follows.",
                warnMessage);
        }
        [TestMethod]
        public void NotText()
        {
            ClipboardInfo cbInfo = new ClipboardInfo();
            var privateCbInfo = new PrivateObject(cbInfo);
            privateCbInfo.SetProperty("ContainsText", false);
            String warnMessage = cbInfo.WarnMessage();
            Assert.AreEqual(
                "The clipboard contains not text.",
                warnMessage);
        }
        [TestMethod]
        public void ContainControlChar()
        {
            ClipboardInfo cbInfo = new ClipboardInfo();
            var privateCbInfo = new PrivateObject(cbInfo);
            privateCbInfo.SetProperty("ContainsText", true);
            privateCbInfo.SetProperty("Text", "\x00");
            String warnMessage = cbInfo.WarnMessage();
            Assert.AreEqual(
                "*** The clipboard contains CONTROL CHARACTER(S) ***" +
                Environment.NewLine + "Please check it carefully.",
                warnMessage);
        }
    }
}
