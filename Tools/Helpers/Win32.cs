using Microsoft.Win32;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Text;

namespace Tools.Helpers
{
    public class Win32
    {
        public const int ANSI_FIXED_FONT = 11;
        public const int ANSI_VAR_FONT = 12;
        public const int BLACK_BRUSH = 4;
        public const int BLACK_PEN = 7;
        public const int DEFAULT_PALETTE = 15;
        public const int DEVICE_DEFAULT_FONT = 14;
        public const int DKGRAY_BRUSH = 3;
        public const int FALSE = 0;
        public const int GCL_HICON = -14;
        public const int GCL_HICONSM = -34;
        public const int GRAY_BRUSH = 2;
        public const int GWL_EXSTYLE = -20;
        public const int GWL_HWNDPARENT = -8;
        public const int GWL_STYLE = -16;
        public const int HIDE_WINDOW = 0;
        public const int HOLLOW_BRUSH = 5;
        public const int HWND_BOTTOM = 1;
        public const int HWND_NOTOPMOST = -2;
        public const int HWND_TOP = 0;
        public const int HWND_TOPMOST = -1;
        public const int ICON_BIG = 1;
        public const int ICON_SMALL = 0;
        public const int LTGRAY_BRUSH = 1;
        public const uint MAX_PATH = 260;
        public const int NULL_BRUSH = 5;
        public const int NULL_PEN = 8;
        public const int OEM_FIXED_FONT = 10;
        public const int RDW_ALLCHILDREN = 0x80;
        public const int RDW_ERASE = 4;
        public const int RDW_ERASENOW = 0x200;
        public const int RDW_FRAME = 0x400;
        public const int RDW_INTERNALPAINT = 2;
        public const int RDW_INVALIDATE = 1;
        public const int RDW_NOCHILDREN = 0x40;
        public const int RDW_NOERASE = 0x20;
        public const int RDW_NOFRAME = 0x800;
        public const int RDW_NOINTERNALPAINT = 0x10;
        public const int RDW_UPDATENOW = 0x100;
        public const int RDW_VALIDATE = 8;
        public const int SHOW_FULLSCREEN = 3;
        public const int SHOW_ICONWINDOW = 2;
        public const int SHOW_OPENNOACTIVATE = 4;
        public const int SHOW_OPENWINDOW = 1;
        public const int SMTO_ABORTIFHUNG = 2;
        public const int SW_OTHERUNZOOM = 4;
        public const int SW_OTHERZOOM = 2;
        public const int SW_PARENTCLOSING = 1;
        public const int SW_PARENTOPENING = 3;
        public const int SWP_ASYNCWINDOWPOS = 0x4000;
        public const int SWP_DEFERERASE = 0x2000;
        public const int SWP_DRAWFRAME = 0x20;
        public const int SWP_FRAMECHANGED = 0x20;
        public const int SWP_HIDEWINDOW = 0x80;
        public const int SWP_NOACTIVATE = 0x10;
        public const int SWP_NOCOPYBITS = 0x100;
        public const int SWP_NOMOVE = 2;
        public const int SWP_NOOWNERZORDER = 0x200;
        public const int SWP_NOREDRAW = 8;
        public const int SWP_NOREPOSITION = 0x200;
        public const int SWP_NOSENDCHANGING = 0x400;
        public const int SWP_NOSIZE = 1;
        public const int SWP_NOZORDER = 4;
        public const int SWP_SHOWWINDOW = 0x40;
        public const int SYSTEM_FIXED_FONT = 0x10;
        public const int SYSTEM_FONT = 13;
        public const int TRUE = 1;
        public const int WHITE_BRUSH = 0;
        public const int WHITE_PEN = 6;
        public const int WM_GETICON = 0x7f;
        public const int WM_QUERYDRAGICON = 0x37;
        public const int WM_SETICON = 0x80;

        [DllImport("kernel32.dll")] //获取模块句柄  
        public static extern IntPtr GetModuleHandle(string lpModuleName);
        [DllImport("gdi32.dll")]
        public static extern bool BitBlt(IntPtr hdcDst, int xDst, int yDst, int cx, int cy, IntPtr hdcSrc, int xSrc, int ySrc, uint ulRop);
        [DllImport("user32.dll")]
        public static extern bool BringWindowToTop(IntPtr window);
        [DllImport("user32.dll")]
        public static extern int CallNextHookEx(IntPtr hookHandle, int nCode, IntPtr wParam, IntPtr lParam);
        [DllImport("Kernel32")]
        public static extern int CopyFile(string source, string destination, int failIfExists);
        [DllImport("gdi32.dll")]
        public static extern IntPtr CreateDC(IntPtr lpszDriver, string lpszDevice, IntPtr lpszOutput, IntPtr lpInitData);
        [DllImport("gdi32.dll")]
        public static extern IntPtr DeleteDC(IntPtr hdc);
        [DllImport("user32.dll")]
        public static extern int DeregisterShellHookWindow(IntPtr hWnd);
        [DllImport("User32.dll")]
        public static extern void EnumWindows(EnumWindowEventHandler callback, int lParam);
        [DllImport("User32.dll")]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);
        [DllImport("user32.dll")]
        public static extern int EnumChildWindows(IntPtr hWndParent, CallBack lpfn, int lParam);
        [DllImport("user32.dll")]
        public static extern int GetClassNameW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll")]
        public static extern int GetWindowTextW(IntPtr hWnd, [MarshalAs(UnmanagedType.LPWStr)] StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll")]
        public static extern uint ScreenToClient(uint hwnd, ref POINTAPI p);
        [DllImport("User32.dll")]
        public static extern int GetClassLong(IntPtr hWnd, int index);
        [DllImport("User32.dll")]
        public static extern int GetClassName(IntPtr hWnd, StringBuilder lpClassName, int nMaxCount);
        [DllImport("user32.dll")]
        public static extern bool GetClientRect(IntPtr hwnd, ref Rect rectangle);

        [DllImport("user32.dll")]
        public static extern int ActivateKeyboardLayout(int HKL, uint flags);

        [DllImport("user32.dll")]
        public static extern IntPtr GetDC(IntPtr hwnd);
        [DllImport("user32.dll")]
        public static extern IntPtr GetDesktopWindow();
        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();
        [DllImport("user32.dll")]
        public static extern IntPtr GetActiveWindow();
        [DllImport("kernel32.dll", CallingConvention = CallingConvention.StdCall, SetLastError = true, ExactSpelling = true)]
        public static extern int GetLastError();
        [DllImport("User32.dll")]
        public static extern IntPtr GetParent(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern IntPtr GetStockObject(int nObject);
        [DllImport("User32.dll")]
        public static extern IntPtr GetWindow(IntPtr hWnd, int wCmd);
        [DllImport("user32.dll")]
        public static extern IntPtr GetWindowDC(IntPtr hwnd);
        [DllImport("user32.dll")]
        public static extern bool GetWindowInfo(IntPtr hwnd, ref WindowInfo info);
        [DllImport("User32.dll")]
        public static extern WindowStyles GetWindowLong(IntPtr hWnd, int index);
        [DllImport("user32.dll")]
        public static extern int GetWindowModuleFileName(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        [DllImport("user32.dll")]
        public static extern bool GetWindowPlacement(IntPtr window, ref WindowPlacement position);
        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);
        [DllImport("User32.dll")]
        public static extern int GetWindowText(IntPtr hWnd, StringBuilder lpString, int nMaxCount);
        [DllImport("User32.dll")]
        public static extern int GetWindowThreadProcessId(IntPtr hWnd, out int processId);
        [DllImport("Kernel32")]
        public static extern short GlobalAddAtom(string atomName);
        [DllImport("Kernel32")]
        public static extern short GlobalDeleteAtom(short atom);
        [DllImport("user32.dll")]
        public static extern IntPtr LoadKeyboardLayout(string pwszKLID, uint Flags);
        public static byte HIBYTE(short a)
        {
            return (byte)(a >> 8);
        }

        public static short HIWORD(int a)
        {
            return (short)(a >> 0x10);
        }

        [DllImport("user32.dll")]
        public static extern int InvalidateRect(IntPtr hWnd, IntPtr lpRect, int bErase);
        [DllImport("User32.dll")]
        public static extern bool IsWindowVisible(IntPtr hWnd);
        public static byte LOBYTE(short a)
        {
            return (byte)(a & 0xff);
        }

        [DllImport("User32")]
        public static extern int LockWindowUpdate(IntPtr windowHandle);
        public static short LOWORD(int a)
        {
            return (short)(a & 0xffff);
        }

        public static int MAKELONG(short a, short b)
        {
            return a & 0xffff | (b & 0xffff) << 0x10;
        }

        public static short MAKEWORD(byte a, byte b)
        {
            return (short)((byte)(a & 0xff) | (byte)(b & 0xff) << 8);
        }

        public static string PathCompactPathEx(string source, uint maxChars)
        {
            StringBuilder pszOut = new StringBuilder(260);
            StringBuilder pszSrc = new StringBuilder(source);
            if (PathCompactPathEx(pszOut, pszSrc, maxChars, 0) == 1)
            {
                return pszOut.ToString();
            }
            Debug.WriteLine(string.Concat(new object[] { "Win32.PathCompactPathEx failed to compact the path '", source, "' down to '", maxChars, "' characters." }));
            return string.Empty;
        }

        [DllImport("Shlwapi.dll")]
        public static extern int PathCompactPathEx(StringBuilder pszOut, StringBuilder pszSrc, uint cchMax, uint dwFlags);
        [DllImport("Shlwapi.dll")]
        public static extern string PathGetArgs(string path);
        [DllImport("User32.dll")]
        public static extern int PostMessage(IntPtr hWnd, int uMsg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern int RedrawWindow(IntPtr hWnd, IntPtr lprcUpdate, IntPtr hrgnUpdate, uint flags);
        [DllImport("User32")]
        public static extern int RegisterHotKey(IntPtr hWnd, int id, uint modifiers, uint virtualkeyCode);
        [DllImport("user32.dll")]
        public static extern int RegisterShellHookWindow(IntPtr hWnd);
        [DllImport("User32.dll")]
        public static extern int RegisterWindowMessage(string message);
        [DllImport("user32.dll")]
        public static extern int ReleaseCapture();
        [DllImport("user32.dll")]
        public static extern int ReleaseDC(IntPtr hwnd, IntPtr hdc);
        public static string SafePathGetArgs(string path)
        {
            try
            {
                return PathGetArgs(path);
            }
            catch (Exception)
            {
            }
            return string.Empty;
        }

        [DllImport("user32.dll")]
        public static extern IntPtr SelectObject(IntPtr hDc, IntPtr hObject);
        [DllImport("User32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, IntPtr wParam, IntPtr lParam);
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd, int uMsg, IntPtr wParam, StringBuilder lParam);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern bool SendMessage(IntPtr hWnd, uint Msg, int wParam, int lParam);
        [DllImport("User32.dll")]
        public static extern int SendMessageTimeout(IntPtr hWnd, int uMsg, int wParam, int lParam, int fuFlags, int uTimeout, out int lpdwResult);
        [DllImport("user32.dll")]
        public static extern IntPtr SetCapture(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern int SetWindowPos(IntPtr hWnd, IntPtr hWndInsertAfter, int X, int Y, int cx, int cy, uint uFlags);
        [DllImport("user32.dll")]
        public static extern IntPtr SetWindowsHookEx(HookTypes hookType, HookProc hookProc, IntPtr hInstance, int nThreadId);
        [DllImport("User32.dll")]
        public static extern int ShowWindowAsync(IntPtr hWnd, int command);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="hwnd"></param>
        /// <param name="nCmdShow">3最大化2最小化1正常</param>
        /// <returns></returns>
        [DllImport("User32.dll")]
        public static extern int ShowWindow(IntPtr hwnd, int nCmdShow);
        [DllImport("User32.dll")]
        public static extern void BlockInput(bool block);

        [DllImport("gdi32.dll")]
        public static extern bool StretchBlt(IntPtr hdcDst, int xDst, int yDst, int cx, int cy, IntPtr hdcSrc, int xSrc, int ySrc, int cxSrc, int cySrc, uint ulRop);
        [DllImport("User32.dll")]
        public static extern void SwitchToThisWindow(IntPtr hWnd, int altTabActivated);
        [DllImport("user32.dll")]
        public static extern int UnhookWindowsHookEx(IntPtr hookHandle);
        [DllImport("User32")]
        public static extern int UnregisterHotKey(IntPtr hWnd, int id);
        [DllImport("user32.dll")]
        public static extern int UpdateWindow(IntPtr hWnd);
        [DllImport("user32.dll")]
        public static extern IntPtr WindowFromPoint(Point pt);
        [DllImport("user32.dll")]
        public static extern bool MoveWindow(IntPtr hWnd, int X, int Y, int nWidth, int nHeight, bool bRepaint);

        public enum BinaryRasterOperations
        {
            R2_BLACK = 1,
            R2_COPYPEN = 13,
            R2_LAST = 0x10,
            R2_MASKNOTPEN = 3,
            R2_MASKPEN = 9,
            R2_MASKPENNOT = 5,
            R2_MERGENOTPEN = 12,
            R2_MERGEPEN = 15,
            R2_MERGEPENNOT = 14,
            R2_NOP = 11,
            R2_NOT = 6,
            R2_NOTCOPYPEN = 4,
            R2_NOTMASKPEN = 8,
            R2_NOTMERGEPEN = 2,
            R2_NOTXORPEN = 10,
            R2_WHITE = 0x10,
            R2_XORPEN = 7
        }

        public delegate bool EnumWindowEventHandler(IntPtr hWnd, int lParam);
        public delegate bool CallBack(IntPtr hwnd, int lParam);

        public class HookEventArgs : EventArgs
        {
            private int _code;
            private IntPtr _lParam;
            private IntPtr _wParam;

            public HookEventArgs(int code, IntPtr wParam, IntPtr lParam)
            {
                _code = code;
                _wParam = wParam;
                _lParam = lParam;
            }

            public int Code
            {
                get
                {
                    return _code;
                }
            }

            public IntPtr lParam
            {
                get
                {
                    return _lParam;
                }
            }

            public IntPtr wParam
            {
                get
                {
                    return _wParam;
                }
            }
        }

        public delegate void HookEventHandler(object sender, HookEventArgs e);

        public delegate int HookProc(int nCode, IntPtr wParam, IntPtr lParam);

        public enum HookTypes
        {
            WH_JOURNALRECORD,
            WH_JOURNALPLAYBACK,
            WH_KEYBOARD,
            WH_GETMESSAGE,
            WH_CALLWNDPROC,
            WH_CBT,
            WH_SYSMSGFILTER,
            WH_MOUSE,
            WH_HARDWARE,
            WH_DEBUG,
            WH_SHELL,
            WH_FOREGROUNDIDLE,
            WH_CALLWNDPROCRET,
            WH_KEYBOARD_LL,
            WH_MOUSE_LL
        }

        [Flags]
        public enum HotkeyModifiers
        {
            MOD_ALT = 1,
            MOD_CONTROL = 2,
            MOD_SHIFT = 4,
            MOD_WIN = 8
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct MSLLHOOKSTRUCT
        {
            public Point Point;
            public int MouseData;
            public int Flags;
            public int Time;
            public int ExtraInfo;
        }

        public enum PeekMessageFlags
        {
            PM_NOREMOVE,
            PM_REMOVE,
            PM_NOYIELD
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct Rect
        {
            public int left;
            public int top;
            public int right;
            public int bottom;
            public int Width
            {
                get
                {
                    return right - left;
                }
            }
            public int Height
            {
                get
                {
                    return bottom - top;
                }
            }
        }

        public enum ShellHookMessages
        {
            HSHELL_ACCESSIBILITYSTATE = 11,
            HSHELL_ACTIVATESHELLWINDOW = 3,
            HSHELL_GETMINRECT = 5,
            HSHELL_LANGUAGE = 8,
            HSHELL_REDRAW = 6,
            HSHELL_TASKMAN = 7,
            HSHELL_WINDOWACTIVATED = 4,
            HSHELL_WINDOWCREATED = 1,
            HSHELL_WINDOWDESTROYED = 2
        }

        public enum ShowWindowCmds
        {
            SW_FORCEMINIMIZE = 11,
            SW_HIDE = 0,
            SW_MAX = 11,
            SW_MAXIMIZE = 3,
            SW_MINIMIZE = 6,
            SW_NORMAL = 1,
            SW_RESTORE = 9,
            SW_SHOW = 5,
            SW_SHOWDEFAULT = 10,
            SW_SHOWMAXIMIZED = 3,
            SW_SHOWMINIMIZED = 2,
            SW_SHOWMINNOACTIVE = 7,
            SW_SHOWNA = 8,
            SW_SHOWNOACTIVATE = 4,
            SW_SHOWNORMAL = 1
        }

        public enum TernaryRasterOperations
        {
            BLACKNESS = 0x42,
            DSTINVERT = 0x550009,
            MERGECOPY = 0xc000ca,
            MERGEPAINT = 0xbb0226,
            NOTSRCCOPY = 0x330008,
            NOTSRCERASE = 0x1100a6,
            PATCOPY = 0xf00021,
            PATINVERT = 0x5a0049,
            PATPAINT = 0xfb0a09,
            SRCAND = 0x8800c6,
            SRCCOPY = 0xcc0020,
            SRCERASE = 0x440328,
            SRCINVERT = 0x660046,
            SRCPAINT = 0xee0086,
            WHITENESS = 0xff0062
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WindowInfo
        {
            public int size;
            public Rectangle window;
            public Rectangle client;
            public int style;
            public int exStyle;
            public int windowStatus;
            public uint xWindowBorders;
            public uint yWindowBorders;
            public short atomWindowtype;
            public short creatorVersion;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WinInfo
        {
            public IntPtr hWnd;
            public string szWindowName;
            public string szClassName;
            public Rect window;
            public Rect client;
            public POINTAPI point;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct POINTAPI
        {
            public uint x;
            public uint y;
        }

        public enum WindowMessages
        {
            WM_ACTIVATE = 6,
            WM_ACTIVATEAPP = 0x1c,
            WM_AFXFIRST = 0x360,
            WM_AFXLAST = 0x37f,
            WM_APP = 0x8000,
            WM_ASKCBFORMATNAME = 780,
            WM_CANCELJOURNAL = 0x4b,
            WM_CANCELMODE = 0x1f,
            WM_CAPTURECHANGED = 0x215,
            WM_CHANGECBCHAIN = 0x30d,
            WM_CHAR = 0x102,
            WM_CHARTOITEM = 0x2f,
            WM_CHILDACTIVATE = 0x22,
            WM_CLEAR = 0x303,
            WM_CLOSE = 0x10,
            WM_COMMAND = 0x111,
            WM_COMMNOTIFY = 0x44,
            WM_COMPACTING = 0x41,
            WM_COMPAREITEM = 0x39,
            WM_CONTEXTMENU = 0x7b,
            WM_COPY = 0x301,
            WM_COPYDATA = 0x4a,
            WM_CREATE = 1,
            WM_CTLCOLOR = 0x19,
            WM_CTLCOLORBTN = 0x135,
            WM_CTLCOLORDLG = 310,
            WM_CTLCOLOREDIT = 0x133,
            WM_CTLCOLORLISTBOX = 0x134,
            WM_CTLCOLORMSGBOX = 0x132,
            WM_CTLCOLORSCROLLBAR = 0x137,
            WM_CTLCOLORSTATIC = 0x138,
            WM_CUT = 0x300,
            WM_DEADCHAR = 0x103,
            WM_DELETEITEM = 0x2d,
            WM_DESTROY = 2,
            WM_DESTROYCLIPBOARD = 0x307,
            WM_DEVICECHANGE = 0x219,
            WM_DEVMODECHANGE = 0x1b,
            WM_DISPLAYCHANGE = 0x7e,
            WM_DRAWCLIPBOARD = 0x308,
            WM_DRAWITEM = 0x2b,
            WM_DROPFILES = 0x233,
            WM_ENABLE = 10,
            WM_ENDSESSION = 0x16,
            WM_ENTERIDLE = 0x121,
            WM_ENTERMENULOOP = 0x211,
            WM_ENTERSIZEMOVE = 0x231,
            WM_ERASEBKGND = 20,
            WM_EXITMENULOOP = 530,
            WM_EXITSIZEMOVE = 0x232,
            WM_FONTCHANGE = 0x1d,
            WM_GETDLGCODE = 0x87,
            WM_GETFONT = 0x31,
            WM_GETHOTKEY = 0x33,
            WM_GETICON = 0x7f,
            WM_GETMINMAXINFO = 0x24,
            WM_GETOBJECT = 0x3d,
            WM_GETTEXT = 13,
            WM_GETTEXTLENGTH = 14,
            WM_HANDHELDFIRST = 0x358,
            WM_HANDHELDLAST = 0x35f,
            WM_HELP = 0x53,
            WM_HOTKEY = 0x312,
            WM_HSCROLL = 0x114,
            WM_HSCROLLCLIPBOARD = 0x30e,
            WM_ICONERASEBKGND = 0x27,
            WM_IME_CHAR = 0x286,
            WM_IME_COMPOSITION = 0x10f,
            WM_IME_COMPOSITIONFULL = 0x284,
            WM_IME_CONTROL = 0x283,
            WM_IME_ENDCOMPOSITION = 270,
            WM_IME_KEYDOWN = 0x290,
            WM_IME_KEYLAST = 0x10f,
            WM_IME_KEYUP = 0x291,
            WM_IME_NOTIFY = 0x282,
            WM_IME_REQUEST = 0x288,
            WM_IME_SELECT = 0x285,
            WM_IME_SETCONTEXT = 0x281,
            WM_IME_STARTCOMPOSITION = 0x10d,
            WM_INITDIALOG = 0x110,
            WM_INITMENU = 0x116,
            WM_INITMENUPOPUP = 0x117,
            WM_INPUTLANGCHANGE = 0x51,
            WM_INPUTLANGCHANGEREQUEST = 80,
            WM_KEYDOWN = 0x100,
            WM_KEYLAST = 0x108,
            WM_KEYUP = 0x101,
            WM_KILLFOCUS = 8,
            WM_LBUTTONDBLCLK = 0x203,
            WM_LBUTTONDOWN = 0x201,
            WM_LBUTTONUP = 0x202,
            WM_MBUTTONDBLCLK = 0x209,
            WM_MBUTTONDOWN = 0x207,
            WM_MBUTTONUP = 520,
            WM_MDIACTIVATE = 0x222,
            WM_MDICASCADE = 0x227,
            WM_MDICREATE = 0x220,
            WM_MDIDESTROY = 0x221,
            WM_MDIGETACTIVE = 0x229,
            WM_MDIICONARRANGE = 0x228,
            WM_MDIMAXIMIZE = 0x225,
            WM_MDINEXT = 0x224,
            WM_MDIREFRESHMENU = 0x234,
            WM_MDIRESTORE = 0x223,
            WM_MDISETMENU = 560,
            WM_MDITILE = 550,
            WM_MEASUREITEM = 0x2c,
            WM_MENUCHAR = 0x120,
            WM_MENUCOMMAND = 0x126,
            WM_MENUDRAG = 0x123,
            WM_MENUGETOBJECT = 0x124,
            WM_MENURBUTTONUP = 290,
            WM_MENUSELECT = 0x11f,
            WM_MOUSEACTIVATE = 0x21,
            WM_MOUSEHOVER = 0x2a1,
            WM_MOUSELEAVE = 0x2a3,
            WM_MOUSEMOVE = 0x200,
            WM_MOUSEWHEEL = 0x20a,
            WM_MOVE = 3,
            WM_MOVING = 0x216,
            WM_NCACTIVATE = 0x86,
            WM_NCCALCSIZE = 0x83,
            WM_NCCREATE = 0x81,
            WM_NCDESTROY = 130,
            WM_NCHITTEST = 0x84,
            WM_NCLBUTTONDBLCLK = 0xa3,
            WM_NCLBUTTONDOWN = 0xa1,
            WM_NCLBUTTONUP = 0xa2,
            WM_NCMBUTTONDBLCLK = 0xa9,
            WM_NCMBUTTONDOWN = 0xa7,
            WM_NCMBUTTONUP = 0xa8,
            WM_NCMOUSEMOVE = 160,
            WM_NCPAINT = 0x85,
            WM_NCRBUTTONDBLCLK = 0xa6,
            WM_NCRBUTTONDOWN = 0xa4,
            WM_NCRBUTTONUP = 0xa5,
            WM_NEXTDLGCTL = 40,
            WM_NEXTMENU = 0x213,
            WM_NOTIFY = 0x4e,
            WM_NOTIFYFORMAT = 0x55,
            WM_NULL = 0,
            WM_PAINT = 15,
            WM_PAINTCLIPBOARD = 0x309,
            WM_PAINTICON = 0x26,
            WM_PALETTECHANGED = 0x311,
            WM_PALETTEISCHANGING = 0x310,
            WM_PARENTNOTIFY = 0x210,
            WM_PASTE = 770,
            WM_PENWINFIRST = 0x380,
            WM_PENWINLAST = 0x38f,
            WM_POWER = 0x48,
            WM_PRINT = 0x317,
            WM_PRINTCLIENT = 0x318,
            WM_QUERYDRAGICON = 0x37,
            WM_QUERYENDSESSION = 0x11,
            WM_QUERYNEWPALETTE = 0x30f,
            WM_QUERYOPEN = 0x13,
            WM_QUEUESYNC = 0x23,
            WM_QUIT = 0x12,
            WM_RBUTTONDBLCLK = 0x206,
            WM_RBUTTONDOWN = 0x204,
            WM_RBUTTONUP = 0x205,
            WM_REFLECT = 0x2000,
            WM_RENDERALLFORMATS = 0x306,
            WM_RENDERFORMAT = 0x305,
            WM_SETCURSOR = 0x20,
            WM_SETFOCUS = 7,
            WM_SETFONT = 0x30,
            WM_SETHOTKEY = 50,
            WM_SETICON = 0x80,
            WM_SETREDRAW = 11,
            WM_SETTEXT = 12,
            WM_SETTINGCHANGE = 0x1a,
            WM_SHOWWINDOW = 0x18,
            WM_SIZE = 5,
            WM_SIZECLIPBOARD = 0x30b,
            WM_SIZING = 0x214,
            WM_SPOOLERSTATUS = 0x2a,
            WM_STYLECHANGED = 0x7d,
            WM_STYLECHANGING = 0x7c,
            WM_SYNCPAINT = 0x88,
            WM_SYSCHAR = 0x106,
            WM_SYSCOLORCHANGE = 0x15,
            WM_SYSCOMMAND = 0x112,
            WM_SYSDEADCHAR = 0x107,
            WM_SYSKEYDOWN = 260,
            WM_SYSKEYUP = 0x105,
            WM_TCARD = 0x52,
            WM_TIMECHANGE = 30,
            WM_TIMER = 0x113,
            WM_UNDO = 0x304,
            WM_UNINITMENUPOPUP = 0x125,
            WM_USER = 0x400,
            WM_USERCHANGED = 0x54,
            WM_VKEYTOITEM = 0x2e,
            WM_VSCROLL = 0x115,
            WM_VSCROLLCLIPBOARD = 0x30a,
            WM_WINDOWPOSCHANGED = 0x47,
            WM_WINDOWPOSCHANGING = 70,
            WM_WININICHANGE = 0x1a
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct WindowPlacement
        {
            public int length;
            public int flags;
            public int showCmd;
            public Point minPosition;
            public Point maxPosition;
            public Rectangle normalPosition;
        }

        public enum WindowStyles : uint
        {
            WS_BORDER = 0x800000,
            WS_CAPTION = 0xc00000,
            WS_CHILD = 0x40000000,
            WS_CHILDWINDOW = 0x40000000,
            WS_CLIPCHILDREN = 0x2000000,
            WS_CLIPSIBLINGS = 0x4000000,
            WS_DISABLED = 0x8000000,
            WS_DLGFRAME = 0x400000,
            WS_EX_ACCEPTFILES = 0x10,
            WS_EX_APPWINDOW = 0x40000,
            WS_EX_DLGMODALFRAME = 1,
            WS_EX_NOPARENTNOTIFY = 4,
            WS_EX_TOOLWINDOW = 0x80,
            WS_EX_TOPMOST = 8,
            WS_EX_TRANSPARENT = 0x20,
            WS_GROUP = 0x20000,
            WS_HSCROLL = 0x100000,
            WS_ICONIC = 0x20000000,
            WS_MAXIMIZE = 0x1000000,
            WS_MAXIMIZEBOX = 0x10000,
            WS_MINIMIZE = 0x20000000,
            WS_MINIMIZEBOX = 0x20000,
            WS_OVERLAPPED = 0,
            WS_OVERLAPPEDWINDOW = 0xcf0000,
            WS_POPUP = 0x80000000,
            WS_POPUPWINDOW = 0x80880000,
            WS_SIZEBOX = 0x40000,
            WS_SYSMENU = 0x80000,
            WS_TABSTOP = 0x10000,
            WS_THICKFRAME = 0x40000,
            WS_TILED = 0,
            WS_VISIBLE = 0x10000000,
            WS_VSCROLL = 0x200000
        }

        public static WinInfo GetWindowInfoByPoint(Point point)
        {
            var selectedHandle = WindowFromPoint(point);
            var info = new WinInfo();
            StringBuilder lpString = new StringBuilder(0x100);
            info.hWnd = selectedHandle;
            GetWindowTextW(selectedHandle, lpString, lpString.Capacity);
            info.szWindowName = lpString.ToString();
            GetClassNameW(selectedHandle, lpString, lpString.Capacity);
            info.szClassName = lpString.ToString();
            Rect rectangle = new Rect();
            GetWindowRect(selectedHandle, ref rectangle);
            info.window = rectangle;
            Rect rect2 = new Rect();
            GetClientRect(selectedHandle, ref rect2);
            info.client = rect2;
            POINTAPI p = new POINTAPI();
            ScreenToClient((uint)(int)selectedHandle, ref p);
            info.point = p;
            return info;
        }

        [Flags]
        public enum MouseEventFlag : uint
        {
            Absolute = 0x8000,
            LeftDown = 2,
            LeftUp = 4,
            MiddleDown = 0x20,
            MiddleUp = 0x40,
            Move = 1,
            RightDown = 8,
            RightUp = 0x10,
            VirtualDesk = 0x4000,
            Wheel = 0x800,
            XDown = 0x80,
            XUp = 0x100
        }

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern bool GetCursorPos(out Point pt);
        [DllImport("User32")]
        public extern static void SetCursorPos(int x, int y);

        [DllImport("user32.dll", EntryPoint = "keybd_event", SetLastError = true)]
        public static extern void keybd_event(int bVk, byte bScan, int dwFlags, int dwExtraInfo);
        [DllImport("user32")]
        public static extern int mouse_event(MouseEventFlag dwFlags, int dx, int dy, int cButtons, int dwExtraInfo);

        [DllImport("user32.dll")]
        public static extern int VkKeyScanA(char ch);

        [DllImport("user32.dll", EntryPoint = "SetForegroundWindow")]
        public static extern bool SetForegroundWindow(IntPtr hWnd);//设置此窗体为活动窗体

        [DllImport("user32.dll")]
        public static extern IntPtr GetWindow(IntPtr hWnd, uint wWcmd);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool GetWindowRect(IntPtr hWnd, ref RECT lpRect);

        [StructLayout(LayoutKind.Sequential)]
        public struct RECT
        {
            public int Left;
            public int Top;
            public int Right;
            public int Bottom;
        }
    }
}

