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


        private UDClipboardContent cb = new UDClipboardContent();
        private string rawClipboardStr;
        private Win32Point mousePointAtStartup = new Win32Point();

        public MainWindow()
        {
            InitializeComponent();
            Environment.ExitCode = 1;

            GetCursorPos(ref mousePointAtStartup);

            clipboardContnt.DataContext = cb;
            if (Clipboard.ContainsText())
            {
                rawClipboardStr = Clipboard.GetText();

                string replaceFrom = Environment.NewLine;
                String replaceTo = "↵" + Environment.NewLine;
                ////Regex re = new Regex(replaceFrom);
                //string modifiedClipboardStr = re.Replace(rawClipboardStr, replaceTo);

                string modifiedClipboardStr = rawClipboardStr.Replace(replaceFrom, replaceTo);
                modifiedClipboardStr = modifiedClipboardStr.Replace(" ", "\u00a0");
                cb.contentStr = modifiedClipboardStr;
            }
        }

        private void Confirmed(object sender, RoutedEventArgs e)
        {
            string currentClipboardStr = Clipboard.GetText();
            if (rawClipboardStr != currentClipboardStr)
            {
                MessageBox.Show("クリップボードの内容が変化したようです。\n" +
                                "再度確認ください。",
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
