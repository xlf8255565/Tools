using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace Pos.Component.Helpers
{
    /// <summary>
    /// 快捷键注册帮助类，用法：this.RegisterHotkey(Keys.Q, KeyFlags.Ctrl, () =>{MessageBox.Show("q");});
    /// </summary>
    public static class HotkeyHelper2
    {
        private static Hotkey _hotkey;
        public static int RegisterHotkey(IntPtr handle, string value, Action action)
        {
            if (string.IsNullOrEmpty(value))
                return 0;
            if (value.Contains("+"))
            {
                var key = value.Split('+')[1];
                var keyflags = value.Split('+')[0];
                var field = typeof(Keys).GetField(key, BindingFlags.Public | BindingFlags.Static);
                if (field == null)
                    return 0;
                var flagfield = typeof(KeyFlags).GetField(keyflags, BindingFlags.Public | BindingFlags.Static);
                if (flagfield == null)
                    return RegisterHotkey(handle, (Keys)field.GetValue(null), action);
                return RegisterHotkey(handle, (Keys)field.GetValue(null), (KeyFlags)flagfield.GetValue(null), action);
            }
            else
            {
                var field = typeof(Keys).GetField(value, BindingFlags.Public | BindingFlags.Static);
                if (field == null)
                    return 0;
                return RegisterHotkey(handle, (Keys)field.GetValue(null), action);
            }
        }

        public static int RegisterHotkey(IntPtr handle, Keys key, Action action)
        {
            return RegisterHotkey(handle, key, KeyFlags.None, action);
        }
        public static int RegisterHotkey(IntPtr handle, Keys key, KeyFlags keyflags, Action action)
        {
            if (_hotkey == null)
                _hotkey = new Hotkey(handle);
            var speakHotkey = _hotkey.RegisterHotkey(key, keyflags);
            _hotkey.OnHotkey += a =>
            {
                if (a == speakHotkey)
                {
                    action();
                }
            };
            return speakHotkey;
        }

        public static void UnregisterHotkeys()
        {
            _hotkey?.UnregisterHotkeys();
            _hotkey = null;
        }
    }

    #region winapi
    public delegate void HotkeyEventHandler(int hotKeyId);
    public class Hotkey : System.Windows.Forms.IMessageFilter
    {
        readonly Hashtable _keyIDs = new Hashtable();
        readonly IntPtr _hWnd;

        public event HotkeyEventHandler OnHotkey;

        [DllImport("user32.dll")]
        public static extern UInt32 RegisterHotKey(IntPtr hWnd, UInt32 id, UInt32 fsModifiers, UInt32 vk);

        [DllImport("user32.dll")]
        public static extern UInt32 UnregisterHotKey(IntPtr hWnd, UInt32 id);

        [DllImport("kernel32.dll")]
        public static extern UInt32 GlobalAddAtom(String lpString);

        [DllImport("kernel32.dll")]
        public static extern UInt32 GlobalDeleteAtom(UInt32 nAtom);

        public Hotkey(IntPtr hWnd)
        {
            this._hWnd = hWnd;
            Application.AddMessageFilter(this);
        }

        public int RegisterHotkey(Keys key, KeyFlags keyflags)
        {
            UInt32 hotkeyid = GlobalAddAtom(System.Guid.NewGuid().ToString());
            RegisterHotKey((IntPtr)_hWnd, hotkeyid, (UInt32)keyflags, (UInt32)key);
            _keyIDs.Add(hotkeyid, hotkeyid);
            return (int)hotkeyid;
        }

        public void UnregisterHotkeys()
        {
            Application.RemoveMessageFilter(this);
            foreach (UInt32 key in _keyIDs.Values)
            {
                UnregisterHotKey(_hWnd, key);
                //GlobalDeleteAtom(key);
            }
            _keyIDs.Clear();
        }

        public bool PreFilterMessage(ref System.Windows.Forms.Message m)
        {
            if (m.Msg == 0x312)
            {
                if (OnHotkey != null)
                {
                    foreach (UInt32 key in _keyIDs.Values)
                    {
                        if ((UInt32)m.WParam == key)
                        {
                            OnHotkey((int)m.WParam);
                            return true;
                        }
                    }
                }
            }
            return false;
        }
    }

    [Flags]
    public enum KeyFlags
    {
        None = 0,
        Alt = 0x1,
        Ctrl = 0x2,
        Shift = 0x4,
        Win = 0x8
    }
    #endregion
}
