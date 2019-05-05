using System;
using System.Runtime.InteropServices;
using System.Windows;

namespace com.undavah.unDavah_PoC
{
   class UDVMouseCursorLib
   {
        [DllImport("User32.dll")]
        private static extern bool SetCursorPos(int X, int Y);

        [DllImport("user32.dll")]
        internal static extern bool GetCursorPos(ref Win32Point pt);

        [StructLayout(LayoutKind.Sequential)]
        internal struct Win32Point
        {
            public Int32 X;
            public Int32 Y;
        };

        public static Point GetPos()
        {
            Win32Point win32Point = new Win32Point();
            GetCursorPos(ref win32Point);

            Point pt = new Point(win32Point.X, win32Point.Y);

            return pt;
        }

        public static void SetPos(Point pt)
        {
            SetCursorPos((int)pt.X, (int)pt.Y);
        }
    }
}
