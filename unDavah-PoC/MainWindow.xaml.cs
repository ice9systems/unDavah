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

            mousePointAtStartup = UDVMouseCursorLib.GetPos();

            EmphasisRules empRules = EmphasisDefinitions.GetEmphasisRules();

            cbInfo = new ClipboardInfo();
            cbInfo.SetCurrentClipboardText();
            if (cbInfo.ContainsText)
            {
                clipboardContnt.EmphasisRules = empRules;
                clipboardContnt.TextForDisplay = cbInfo.Text;

                WarnMessage.Text = cbInfo.WarnMessage();
            }
            else
            {
                WarnMessage.Text = "The clipboard contains not text.";
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

            UDVMouseCursorLib.SetPos(mousePointAtStartup);
            Environment.ExitCode = 0;
            Application.Current.Shutdown();
        }

        private void Canceled(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
