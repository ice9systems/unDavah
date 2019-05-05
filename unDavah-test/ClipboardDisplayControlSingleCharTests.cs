using System;
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
        public void WhiteSpace0x20()
        {
            EmphasisRules empRules = EmphasisDefinitions.GetEmphasisRules();
            ClipboardDisplayControl cbDisplayControl = new ClipboardDisplayControl();

            String source = " ";
            var ruleIsIn = empRules.doFirst;
            TestConditionsForMatchRules cond = new TestConditionsForMatchRules(source, ruleIsIn);
            Assert.AreEqual(cond.HitCount, 1);


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
            Assert.AreEqual(cond.HitCount, 1);
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
            Assert.AreEqual(cond.HitCount, 1);
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
            Assert.AreEqual(cond.HitCount, 1);
            Assert.AreEqual(expectedReplaceTo, cond.ReplaceTo);


            cbDisplayControl.EmphasisRules = empRules;
            cbDisplayControl.TextForDisplay = source;

            var outputStr = cbDisplayControl.clipboardContntArea.Text;
            Assert.AreEqual(cond.GetExpect(), outputStr);
        }
    }
}
