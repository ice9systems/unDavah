using System;
using System.Windows;

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

                clipboardContnt.EmphasisRules = empRules;
                clipboardContnt.TextForDisplay = cbInfo.Text;
            }
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
