using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Tools.Helpers
{
    public class ClipboardHelper
    {
        [DllImport("User32")]
        private static extern bool OpenClipboard(IntPtr hWndNewOwner);

        [DllImport("User32")]
        private static extern bool CloseClipboard();

        [DllImport("User32")]
        private static extern bool EmptyClipboard();

        [DllImport("User32")]
        private static extern bool IsClipboardFormatAvailable(int format);

        [DllImport("User32")]
        private static extern IntPtr GetClipboardData(int uFormat);

        [DllImport("User32", CharSet = CharSet.Unicode)]
        private static extern IntPtr SetClipboardData(int uFormat, IntPtr hMem);

        /// <summary>
        /// 向剪贴板中添加文本
        /// </summary>
        /// <param name="text">文本</param>
        public static void SetText(string text)
        {
            if (!OpenClipboard(IntPtr.Zero))
            {
                SetText(text);
                return;
            }
            EmptyClipboard();
            SetClipboardData(13, Marshal.StringToHGlobalUni(text));
            CloseClipboard();
        }
    }
}
