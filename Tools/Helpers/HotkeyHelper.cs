using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Interop;

namespace QrCodeDecode.Helpers
{
    /// <summary>
    /// 快捷键注册帮助类，用法：HotkeyHelper.RegisterHotkey(this.Handle, Keys.Q, KeyFlags.Ctrl, () =>{MessageBox.Show("q");});
    /// </summary>
    static class HotkeyHelper
    {
        #region 系统api
        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool RegisterHotKey(IntPtr hWnd, int id, HotkeyModifiers fsModifiers, uint vk);

        [DllImport("user32.dll")]
        static extern bool UnregisterHotKey(IntPtr hWnd, int id);
        #endregion

        /// <summary>
        /// 注册快捷键
        /// </summary>
        /// <param name="window">持有快捷键窗口</param>
        /// <param name="fsModifiers">组合键</param>
        /// <param name="key">快捷键</param>
        /// <param name="callBack">回调函数</param>
        public static void Regist(Window window, HotkeyModifiers fsModifiers, Key key, Action callBack)
        {
            var hwnd = new WindowInteropHelper(window).Handle;
            if (_hwndSource==null)
            {
                _hwndSource = HwndSource.FromHwnd(hwnd);
                _hwndSource.AddHook(WndProc);
            }

            int id = keyid++;

            var vk = KeyInterop.VirtualKeyFromKey(key);
            if (!RegisterHotKey(hwnd, id, fsModifiers, (uint)vk))
                throw new Exception("regist hotkey fail.");
            keymap[id] = callBack;
        }

        /// <summary>
        /// 快捷键消息处理
        /// </summary>
        static IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg == WM_HOTKEY)
            {
                int id = wParam.ToInt32();
                if (keymap.TryGetValue(id, out var callback))
                {
                    callback();
                }
            }
            return IntPtr.Zero;
        }

        /// <summary>
        /// 注销快捷键
        /// </summary>
        /// <param name="hWnd">持有快捷键窗口的句柄</param>
        /// <param name="callBack">回调函数</param>
        public static void UnRegist(Window window)
        {
            var hwnd = new WindowInteropHelper(window).Handle;
            foreach (KeyValuePair<int, Action> var in keymap)
            {
                UnregisterHotKey(hwnd, var.Key);
            }
            keymap.Clear();
        }


        const int WM_HOTKEY = 0x312;
        static int keyid = 10;
        static Dictionary<int, Action> keymap = new Dictionary<int, Action>();
        private static HwndSource _hwndSource;

        public delegate void HotKeyCallBackHanlder();
    }

    enum HotkeyModifiers
    {
        None = 0,
        Alt = 0x1,
        Ctrl = 0x2,
        Shift = 0x4,
        Win = 0x8
    }
}
