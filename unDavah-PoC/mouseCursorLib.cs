using System;
using System.Runtime.InteropServices;
using System.Windows;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace com.undavah.unDavah_PoC
{
   class mouseCursorLib
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

        public static Point GetPos()
        {
            Win32Point win32Point = new Win32Point();
            GetCursorPos(ref win32Point);

            Point pt = new Point((double)win32Point.X,
                (double)win32Point.Y);

            return pt;
        }

        public static void SetPos(Point pt)
        {
            SetCursorPos((int)pt.X, (int)pt.Y);
        }
    }
}
