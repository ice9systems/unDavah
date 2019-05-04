using System;
using System.Windows;
using System.Text.RegularExpressions;

namespace com.undavah.unDavah_PoC
{
    /// <summary>
    /// MainWindow.xaml の相互作用ロジック
    /// </summary>
    public partial class MainWindow : Window
    {
        private ClipboardInfo cbInfo;
        private Point mousePointAtStartup;
        public MainWindow()
        {
            InitializeComponent();
            Environment.ExitCode = 1;

            mousePointAtStartup = mouseCursorLib.GetPos();

            EmphasisRules empRules = EmphasisDefinitions.GetEmphasisRules();

            if (Clipboard.ContainsText())
            {
                cbInfo = new ClipboardInfo();
                cbInfo.setCurrentClipboardText();

                String modifiedClipboardStr = DoEmphasis(cbInfo.text, empRules);

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
            if (!cbInfo.isUpToDate())
            {
                MessageBox.Show("The contents of the clipboard seem to have changed. " +
                    "Please check it again.",
                    "[unDavah] Clipboard mismatch",
                    MessageBoxButton.OK,
                    MessageBoxImage.Exclamation);
                Application.Current.Shutdown();
            }

            mouseCursorLib.SetPos(mousePointAtStartup);
            Environment.ExitCode = 0;
            Application.Current.Shutdown();
        }

        private void Canceled(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
