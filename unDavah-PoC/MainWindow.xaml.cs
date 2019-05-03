using System;
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

namespace unDavah_PoC
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

            //clipboardContnt.DataContext = cb;
            if (Clipboard.ContainsText())
            {
                rawClipboardStr = Clipboard.GetText();
                String modifiedClipboardStr = rawClipboardStr;

                Regex re = new Regex("<");
                modifiedClipboardStr = re.Replace(modifiedClipboardStr, "&lt;");
                re = new Regex(">");
                modifiedClipboardStr = re.Replace(modifiedClipboardStr, "&gt;");
                re = new Regex("&");
                modifiedClipboardStr = re.Replace(modifiedClipboardStr, "&amp;");

                re = new Regex(" ");
                modifiedClipboardStr = re.Replace(modifiedClipboardStr, "<style fgcolor='#00a1e9'>\u2423</style>");
                re = new Regex("\t");
                modifiedClipboardStr = re.Replace(modifiedClipboardStr, "<style bgcolor='#ffff00' fgcolor='#ff0000'>[\\t]</style>");

                re = new Regex(Environment.NewLine);
                modifiedClipboardStr = re.Replace(modifiedClipboardStr, "<style bgcolor='#ff0000' fgcolor='#00ffff'>↵</style><br/>");

                re = new Regex("^");
                modifiedClipboardStr = re.Replace(modifiedClipboardStr, "<texts>");
                re = new Regex("$");
                modifiedClipboardStr = re.Replace(modifiedClipboardStr, "</texts>");

                clipboardContnt.Text = modifiedClipboardStr;
            }
        }

        private void Confirmed(object sender, RoutedEventArgs e)
        {
            string currentClipboardStr = Clipboard.GetText();
            if (rawClipboardStr != currentClipboardStr)
            {
                MessageBox.Show("The contents of the clipboard seem to have changed. Please check it again.",
                                "[unDavah] Clipboard mismatch",
                                MessageBoxButton.OK,
                                MessageBoxImage.Exclamation);
                Application.Current.Shutdown();
            }

            SetCursorPos(mousePointAtStartup.X, mousePointAtStartup.Y);
            Environment.ExitCode = 0;
            Application.Current.Shutdown();
        }

        private void Cancel(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}
