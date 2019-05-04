using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;

namespace com.undavah.unDavah_PoC
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        [DllImport("User32.dll", CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll", CallingConvention = CallingConvention.StdCall)]
        [return: MarshalAs(UnmanagedType.Bool)]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };

        private string rawClipboardStr;
        private Win32Point mousePointAtStartup = new Win32Point();

        public MainWindow()
        {
            InitializeComponent();
            Environment.ExitCode = 1;

            GetCursorPos(ref mousePointAtStartup);

            EmphasisRules empRules = EmphasisDefinitions.GetEmphasisRules();

            if (Clipboard.ContainsText())
            {
                rawClipboardStr = Clipboard.GetText();
                //String modifiedClipboardStr = rawClipboardStr;
                String modifiedClipboardStr = DoEmphasis(rawClipboardStr, empRules);

                clipboardContnt.Text = modifiedClipboardStr;
            }
        }

        private string DoEmphasis(String aStr, EmphasisRules rules)
        {
            // Escaping Characters Used in XML
            aStr = Regex.Replace(aStr, "<", "&lt;");
            aStr = Regex.Replace(aStr, ">", "&gt;");
            aStr = Regex.Replace(aStr, "&", "&amp;");

            // apply High Priority rules
            aStr = applyStyleTag(aStr, rules.doFirst);

            // apply other rules
            aStr = applyStyleTag(aStr, rules.rules);

            // Apply last rules
            aStr = applyStyleTag(aStr, rules.doLast);

            // add a ROOT node
            aStr = Regex.Replace(aStr, "^", "<texts>");
            aStr = Regex.Replace(aStr, "$", "</texts>");

            return aStr;
        }

        private String applyStyleTag(String aStr, EmphasisRules.Rule[] rules)
        {
            String replaceStr;
            foreach (EmphasisRules.Rule aRule in rules)
            {
                replaceStr =
                "<style bgcolor='" + aRule.bgcolor +
                "' fgcolor='" + aRule.fgcolor + "'>" +
                aRule.replaceTo + "</style>";
                aStr = Regex.Replace(aStr, aRule.matchTo, replaceStr);
            }

            return aStr;
        }

        private void Confirmed(object sender, RoutedEventArgs e)
        {
            string currentClipboardStr = Clipboard.GetText();
            if (rawClipboardStr != currentClipboardStr)
            {
                MessageBox.Show("The contents of the clipboard seem to have changed. " +
                    "Please check it again.",
                    "[unDavah] Clipboard mismatch",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                Application.Current.Shutdown();
            }

            SetCursorPos(mousePointAtStartup.X, mousePointAtStartup.Y);
            Environment.ExitCode = 0;
            Application.Current.Shutdown();
        }

        private void Canceled(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
