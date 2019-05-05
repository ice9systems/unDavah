using System;
using System.Text;
using System.Runtime.InteropServices;

namespace com.undavah.unDavah_PoC
{
    class UDVClipboardLib
    {
        private const uint CF_UNICODETEXT = 13U;

        [DllImport("User32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool IsClipboardFormatAvailable(uint format);

        [DllImport("User32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool CloseClipboard();

        [DllImport("User32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("User32.dll", SetLastError = true)]
        private static extern IntPtr GetClipboardData(uint uFormat);

        [DllImport("Kernel32.dll", SetLastError = true)]
        [return: MarshalAs(UnmanagedType.Bool)]
        private static extern bool GlobalUnlock(IntPtr hMem);

        [DllImport("Kernel32.dll", SetLastError = true)]
        private static extern IntPtr GlobalLock(IntPtr hMem);

        [DllImport("Kernel32.dll", SetLastError = true)]
        private static extern int GlobalSize(IntPtr hMem);

        public static String UDVGetClipboardString()
        {
            String clipboardStr;
            if (!IsClipboardFormatAvailable(CF_UNICODETEXT)) { return null; }

            try
            {
                if (!OpenClipboard(IntPtr.Zero)) { return null; }

                IntPtr handle = GetClipboardData(CF_UNICODETEXT);
                if (handle == IntPtr.Zero) { return null; }

                IntPtr ptr = IntPtr.Zero;

                try
                {
                    ptr = GlobalLock(handle);
                    if (ptr == IntPtr.Zero) { return null; }

                    int size = GlobalSize(handle);
                    byte[] buff = new byte[size];

                    Marshal.Copy(ptr, buff, 0, size);

                    clipboardStr = Encoding.Unicode.GetString(buff).TrimEnd('\0');

                }
                finally
                {
                    GlobalUnlock(handle);
                }
            }
            finally
            {
                CloseClipboard();
            }
            return clipboardStr;
        }
    }

}
