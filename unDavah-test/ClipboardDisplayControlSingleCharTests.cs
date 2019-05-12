using System;
using System.Collections.Generic;
using com.undavah.unDavah_PoC;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text.RegularExpressions;

namespace com.undavah.undavah.Tests
{
    [TestClass]
    public class ClipboardDisplayControlSingleCharTests
    {
        [TestMethod]
        public void Empty()
        {
            EmphasisRules empRules = EmphasisDefinitions.GetEmphasisRules();
            ClipboardDisplayControl cbDisplayControl = new ClipboardDisplayControl();

            cbDisplayControl.EmphasisRules = empRules;
            cbDisplayControl.TextForDisplay = "";
            var outputStr = cbDisplayControl.clipboardContntArea.Text;
            Assert.AreEqual("<texts></texts>", outputStr);
        }

        [TestMethod]
        public void NullCharacterInTheMiddleOfString()
        {
            EmphasisRules empRules = EmphasisDefinitions.GetEmphasisRules();
            ClipboardDisplayControl cbDisplayControl = new ClipboardDisplayControl();

            cbDisplayControl.EmphasisRules = empRules;
            cbDisplayControl.TextForDisplay = "Null is here->\x00<-";
            var outputStr = cbDisplayControl.clipboardContntArea.Text;
            Assert.AreEqual("<texts>Null<style bgcolor='#000000' fgcolor='#00ffff'>␣" +
                "</style>is<style bgcolor='#000000' fgcolor='#00ffff'>␣" +
                "</style>here-&amp;gt;<style bgcolor='#ff0000' fgcolor='#ffff00'>" +
                "^@</style>&amp;lt;-</texts>", outputStr);
        }

        [TestMethod]
        public void Number()
        {
            EmphasisRules empRules = EmphasisDefinitions.GetEmphasisRules();
            ClipboardDisplayControl cbDisplayControl = new ClipboardDisplayControl();

            for( int cnt = 0; cnt<=9; cnt++)
            {
                String source = cnt.ToString();
                var ruleIsIn = empRules.doFirst;
                TestConditionsForMatchRules cond = new TestConditionsForMatchRules(source, ruleIsIn);
                Assert.AreEqual(1, cond.HitCount);


                cbDisplayControl.EmphasisRules = empRules;
                cbDisplayControl.TextForDisplay = source;

                var outputStr = cbDisplayControl.clipboardContntArea.Text;
                Assert.AreEqual(cond.GetExpect(), outputStr);
            }
        }

        [TestMethod]
        public void AlphabetUpper()
        {
            EmphasisRules empRules = EmphasisDefinitions.GetEmphasisRules();
            ClipboardDisplayControl cbDisplayControl = new ClipboardDisplayControl();

            for (int cnt = 0x41; cnt <= 0x5a; cnt++)
            {
                String source = Char.ConvertFromUtf32(cnt);
                var ruleIsIn = empRules.doFirst;
                TestConditionsForMatchRules cond = new TestConditionsForMatchRules(source, ruleIsIn);
                Assert.AreEqual(1, cond.HitCount);


                cbDisplayControl.EmphasisRules = empRules;
                cbDisplayControl.TextForDisplay = source;

                var outputStr = cbDisplayControl.clipboardContntArea.Text;
                Assert.AreEqual(cond.GetExpect(), outputStr);
            }
        }

        [TestMethod]
        public void AlphabetLower()
        {
            EmphasisRules empRules = EmphasisDefinitions.GetEmphasisRules();
            ClipboardDisplayControl cbDisplayControl = new ClipboardDisplayControl();

            for (int cnt = 0x61; cnt <= 0x7a; cnt++)
            {
                String source = Char.ConvertFromUtf32(cnt);
                var ruleIsIn = empRules.doFirst;
                TestConditionsForMatchRules cond = new TestConditionsForMatchRules(source, ruleIsIn);
                Assert.AreEqual(1, cond.HitCount);


                cbDisplayControl.EmphasisRules = empRules;
                cbDisplayControl.TextForDisplay = source;

                var outputStr = cbDisplayControl.clipboardContntArea.Text;
                Assert.AreEqual(cond.GetExpect(), outputStr);
            }
        }

        [TestMethod]
        public void Brackets1()
        {
            EmphasisRules empRules = EmphasisDefinitions.GetEmphasisRules();
            ClipboardDisplayControl cbDisplayControl = new ClipboardDisplayControl();

            String[] targets = "\x28 \x29".Split(' ');

            foreach (String target in targets)
            {
                String source = target;
                var ruleIsIn = empRules.rules;
                TestConditionsForMatchRules cond = new TestConditionsForMatchRules(source, ruleIsIn);
                Assert.AreEqual(1, cond.HitCount);


                cbDisplayControl.EmphasisRules = empRules;
                cbDisplayControl.TextForDisplay = source;

                var outputStr = cbDisplayControl.clipboardContntArea.Text;
                Assert.AreEqual(cond.GetExpect(), outputStr);
            }
        }

        [TestMethod]
        public void Brackets2()
        {
            EmphasisRules empRules = EmphasisDefinitions.GetEmphasisRules();
            ClipboardDisplayControl cbDisplayControl = new ClipboardDisplayControl();

            String[] targets = "\x5b \x5d".Split(' ');

            foreach (String target in targets)
            {
                String source = target;
                var ruleIsIn = empRules.rules;
                TestConditionsForMatchRules cond = new TestConditionsForMatchRules(source, ruleIsIn);
                Assert.AreEqual(1, cond.HitCount);


                cbDisplayControl.EmphasisRules = empRules;
                cbDisplayControl.TextForDisplay = source;

                var outputStr = cbDisplayControl.clipboardContntArea.Text;
                Assert.AreEqual(cond.GetExpect(), outputStr);
            }
        }

        [TestMethod]
        public void Brakets3()
        {
            EmphasisRules empRules = EmphasisDefinitions.GetEmphasisRules();
            ClipboardDisplayControl cbDisplayControl = new ClipboardDisplayControl();

            String[] targets = "\x7b \x7d".Split(' ');

            foreach (String target in targets)
            {
                String source = target;
                var ruleIsIn = empRules.rules;
                TestConditionsForMatchRules cond = new TestConditionsForMatchRules(source, ruleIsIn);
                Assert.AreEqual(1, cond.HitCount);


                cbDisplayControl.EmphasisRules = empRules;
                cbDisplayControl.TextForDisplay = source;

                var outputStr = cbDisplayControl.clipboardContntArea.Text;
                Assert.AreEqual(cond.GetExpect(), outputStr);
            }
        }

        [TestMethod]
        public void Brakets4()
        {
            EmphasisRules empRules = EmphasisDefinitions.GetEmphasisRules();
            ClipboardDisplayControl cbDisplayControl = new ClipboardDisplayControl();

            Dictionary<String, String> targets = new Dictionary<string, string>(){
                    { "<", "&lt;" },
                    { ">", "&gt;" }
                };
            foreach (KeyValuePair<String, String> item in targets)
            {
                String source = item.Key;
                String pattern = item.Value;
                var ruleIsIn = empRules.doFirst;
                TestConditionsForMatchRules cond = new TestConditionsForMatchRules(pattern, ruleIsIn);
                Assert.AreEqual(1, cond.HitCount);


                cbDisplayControl.EmphasisRules = empRules;
                cbDisplayControl.TextForDisplay = source;

                var outputStr = cbDisplayControl.clipboardContntArea.Text;
                Assert.AreEqual(cond.GetExpect(), outputStr);
            }
        }

        [TestMethod]
        public void Semicolon()
        {
            EmphasisRules empRules = EmphasisDefinitions.GetEmphasisRules();
            ClipboardDisplayControl cbDisplayControl = new ClipboardDisplayControl();

            Dictionary<String, String> targets = new Dictionary<string, string>(){
                    { ";", "&#x3b;" }
                };
            foreach (KeyValuePair<String, String> item in targets)
            {
                String source = item.Key;
                String pattern = item.Value;
                var ruleIsIn = empRules.doFirst;
                TestConditionsForMatchRules cond = new TestConditionsForMatchRules(pattern, ruleIsIn);
                Assert.AreEqual(1, cond.HitCount);


                cbDisplayControl.EmphasisRules = empRules;
                cbDisplayControl.TextForDisplay = source;

                var outputStr = cbDisplayControl.clipboardContntArea.Text;
                Assert.AreEqual(cond.GetExpect(), outputStr);
            }
        }

        [TestMethod]
        public void Symbols()
        {
            EmphasisRules empRules = EmphasisDefinitions.GetEmphasisRules();
            ClipboardDisplayControl cbDisplayControl = new ClipboardDisplayControl();

            String[] targets = ("\x21 \x24 \x25 \x2a \x2b " +
                "\x2c \x2d \x2e \x2f \x3a \x3d \x3f \x40 \x5f \x7c \x7e").Split(' ');

            foreach (String target in targets)
            {
                String source = target;
                var ruleIsIn = empRules.rules;
                TestConditionsForMatchRules cond = new TestConditionsForMatchRules(source, ruleIsIn);
                Assert.AreEqual(1, cond.HitCount);


                cbDisplayControl.EmphasisRules = empRules;
                cbDisplayControl.TextForDisplay = source;

                var outputStr = cbDisplayControl.clipboardContntArea.Text;
                Assert.AreEqual(cond.GetExpect(), outputStr);
            }
        }

        [TestMethod]
        public void Backslash()
        {
            EmphasisRules empRules = EmphasisDefinitions.GetEmphasisRules();
            ClipboardDisplayControl cbDisplayControl = new ClipboardDisplayControl();

            Dictionary<String, String> targets = new Dictionary<string, string>(){
                    { "\\", "&#x5c;" }
                };
            foreach (KeyValuePair<String, String> item in targets)
            {
                String source = item.Key;
                String pattern = item.Value;
                var ruleIsIn = empRules.rules;
                TestConditionsForMatchRules cond = new TestConditionsForMatchRules(pattern, ruleIsIn);
                Assert.AreEqual(1, cond.HitCount);


                cbDisplayControl.EmphasisRules = empRules;
                cbDisplayControl.TextForDisplay = source;

                var outputStr = cbDisplayControl.clipboardContntArea.Text;
                Assert.AreEqual(cond.GetExpect(), outputStr);
            }
        }

        [TestMethod]
        public void Caret()
        {
            EmphasisRules empRules = EmphasisDefinitions.GetEmphasisRules();
            ClipboardDisplayControl cbDisplayControl = new ClipboardDisplayControl();

            Dictionary<String, String> targets = new Dictionary<string, string>(){
                    { "^", "&#x5e;" }
                };
            foreach (KeyValuePair<String, String> item in targets)
            {
                String source = item.Key;
                String pattern = item.Value;
                var ruleIsIn = empRules.rules;
                TestConditionsForMatchRules cond = new TestConditionsForMatchRules(pattern, ruleIsIn);
                Assert.AreEqual(1, cond.HitCount);


                cbDisplayControl.EmphasisRules = empRules;
                cbDisplayControl.TextForDisplay = source;

                var outputStr = cbDisplayControl.clipboardContntArea.Text;
                Assert.AreEqual(cond.GetExpect(), outputStr);
            }
        }

        [TestMethod]
        public void Ampersand()
        {
            EmphasisRules empRules = EmphasisDefinitions.GetEmphasisRules();
            ClipboardDisplayControl cbDisplayControl = new ClipboardDisplayControl();

            Dictionary<String, String> targets = new Dictionary<string, string>(){
                    { "&", "&amp;" }
                };
            foreach (KeyValuePair<String, String> item in targets)
            {
                String source = item.Key;
                String pattern = item.Value;
                var ruleIsIn = empRules.rules;
                TestConditionsForMatchRules cond = new TestConditionsForMatchRules(pattern, ruleIsIn);
                Assert.AreEqual(1, cond.HitCount);


                cbDisplayControl.EmphasisRules = empRules;
                cbDisplayControl.TextForDisplay = source;

                var outputStr = cbDisplayControl.clipboardContntArea.Text;
                Assert.AreEqual(cond.GetExpect(), outputStr);
            }
        }

        [TestMethod]
        public void Quotes()
        {
            EmphasisRules empRules = EmphasisDefinitions.GetEmphasisRules();
            ClipboardDisplayControl cbDisplayControl = new ClipboardDisplayControl();

            String[] targets = "\x27 \x60".Split(' ');

            foreach (String target in targets)
            {
                String source = target;
                var ruleIsIn = empRules.rules;
                TestConditionsForMatchRules cond = new TestConditionsForMatchRules(source, ruleIsIn);
                Assert.AreEqual(1, cond.HitCount);


                cbDisplayControl.EmphasisRules = empRules;
                cbDisplayControl.TextForDisplay = source;

                var outputStr = cbDisplayControl.clipboardContntArea.Text;
                Assert.AreEqual(cond.GetExpect(), outputStr);
            }
        }

        [TestMethod]
        public void DoubleQuote()
        {
            EmphasisRules empRules = EmphasisDefinitions.GetEmphasisRules();
            ClipboardDisplayControl cbDisplayControl = new ClipboardDisplayControl();

            Dictionary<String, String> targets = new Dictionary<string, string>(){
                    { "\"", "&quot;" }
                };
            foreach (KeyValuePair<String, String> item in targets)
            {
                String source = item.Key;
                String pattern = item.Value;
                var ruleIsIn = empRules.doFirst;
                TestConditionsForMatchRules cond = new TestConditionsForMatchRules(pattern, ruleIsIn);
                Assert.AreEqual(1, cond.HitCount);


                cbDisplayControl.EmphasisRules = empRules;
                cbDisplayControl.TextForDisplay = source;

                var outputStr = cbDisplayControl.clipboardContntArea.Text;
                Assert.AreEqual(cond.GetExpect(), outputStr);
            }
        }

        [TestMethod]
        public void WhiteSpace0x20()
        {
            EmphasisRules empRules = EmphasisDefinitions.GetEmphasisRules();
            ClipboardDisplayControl cbDisplayControl = new ClipboardDisplayControl();

            String source = " ";
            var ruleIsIn = empRules.doFirst;
            TestConditionsForMatchRules cond = new TestConditionsForMatchRules(source, ruleIsIn);
            Assert.AreEqual(1, cond.HitCount);


            cbDisplayControl.EmphasisRules = empRules;
            cbDisplayControl.TextForDisplay = source;

            var outputStr = cbDisplayControl.clipboardContntArea.Text;
            Assert.AreEqual(cond.GetExpect(), outputStr);
        }

        [TestMethod]
        public void Hash()
        {
            EmphasisRules empRules = EmphasisDefinitions.GetEmphasisRules();
            ClipboardDisplayControl cbDisplayControl = new ClipboardDisplayControl();

            String source = "#";
            var ruleIsIn = empRules.rules;
            TestConditionsForMatchRules cond = new TestConditionsForMatchRules(source, ruleIsIn);
            Assert.AreEqual(1, cond.HitCount);


            cbDisplayControl.EmphasisRules = empRules;
            cbDisplayControl.TextForDisplay = source;

            var outputStr = cbDisplayControl.clipboardContntArea.Text;
            Assert.AreEqual(cond.GetExpect(), outputStr);
        }

        [TestMethod]
        public void Newline()
        {
            EmphasisRules empRules = EmphasisDefinitions.GetEmphasisRules();
            ClipboardDisplayControl cbDisplayControl = new ClipboardDisplayControl();

            String source = Environment.NewLine;
            String expectedReplaceTo = "↵" + Environment.NewLine;
            var ruleIsIn = empRules.doLast;
            TestConditionsForMatchRules cond = new TestConditionsForMatchRules(source, ruleIsIn);
            Assert.AreEqual(1, cond.HitCount);
            Assert.AreEqual(expectedReplaceTo, cond.ReplaceTo);


            cbDisplayControl.EmphasisRules = empRules;
            cbDisplayControl.TextForDisplay = source;

            var outputStr = cbDisplayControl.clipboardContntArea.Text;
            Assert.AreEqual(cond.GetExpect(), outputStr);
        }
        [TestMethod]
        public void Tab()
        {
            EmphasisRules empRules = EmphasisDefinitions.GetEmphasisRules();
            ClipboardDisplayControl cbDisplayControl = new ClipboardDisplayControl();

            String source = "\t";
            String expectedReplaceTo = "[\\t]";
            var ruleIsIn = empRules.rules;
            TestConditionsForMatchRules cond = new TestConditionsForMatchRules(source, ruleIsIn);
            Assert.AreEqual(1, cond.HitCount);
            Assert.AreEqual(expectedReplaceTo, cond.ReplaceTo);


            cbDisplayControl.EmphasisRules = empRules;
            cbDisplayControl.TextForDisplay = source;

            var outputStr = cbDisplayControl.clipboardContntArea.Text;
            Assert.AreEqual(cond.GetExpect(), outputStr);
        }
        [TestMethod]
        public void Null()
        {
            EmphasisRules empRules = EmphasisDefinitions.GetEmphasisRules();
            ClipboardDisplayControl cbDisplayControl = new ClipboardDisplayControl();

            String source = "\x00";
            String expectedReplaceTo = "^@";
            var ruleIsIn = empRules.rules;
            TestConditionsForMatchRules cond = new TestConditionsForMatchRules(source, ruleIsIn);
            Assert.AreEqual(1, cond.HitCount);
            Assert.AreEqual(expectedReplaceTo, cond.ReplaceTo);


            cbDisplayControl.EmphasisRules = empRules;
            cbDisplayControl.TextForDisplay = source;

            var outputStr = cbDisplayControl.clipboardContntArea.Text;
            Assert.AreEqual(cond.GetExpect(), outputStr);
        }
        [TestMethod]
        public void BackSpace()
        {
            EmphasisRules empRules = EmphasisDefinitions.GetEmphasisRules();
            ClipboardDisplayControl cbDisplayControl = new ClipboardDisplayControl();

            String source = "\b";
            String expectedReplaceTo = "^H";
            var ruleIsIn = empRules.rules;
            TestConditionsForMatchRules cond = new TestConditionsForMatchRules(source, ruleIsIn);
            Assert.AreEqual(1, cond.HitCount);
            Assert.AreEqual(expectedReplaceTo, cond.ReplaceTo);


            cbDisplayControl.EmphasisRules = empRules;
            cbDisplayControl.TextForDisplay = source;

            var outputStr = cbDisplayControl.clipboardContntArea.Text;
            Assert.AreEqual(cond.GetExpect(), outputStr);
        }
    }
}
