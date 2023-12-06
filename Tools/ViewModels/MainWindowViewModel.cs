using Microsoft.Win32;
using QrCodeDecode.Helpers;
using System;
using System.Drawing;
using System.IO;
using System.Runtime.InteropServices;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using Tools.Helpers;
using Tools.Views;
using Application = System.Windows.Forms.Application;
using MessageBox = System.Windows.MessageBox;
using Clipboard = System.Windows.Forms.Clipboard;
using DataFormats = System.Windows.Forms.DataFormats;
using KeyEventArgs = System.Windows.Input.KeyEventArgs;
using System.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Threading;
using System.Threading.Tasks;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;
using System.Windows.Media.Media3D;
using Newtonsoft.Json.Linq;

namespace Tools.ViewModels
{
    public partial class MainWindowViewModel : BaseViewModel
    {
        private Qrcode _qrcode;
        private bool _isFocus;
        private Sunflower _sunflower;
        private SunflowerViewModel _sunflowerViewModel;
        private QrcodeViewModel _qrcodeViewModel;
        private CommandsViewModel _commandsViewModel;

        [DllImport("user32 ")]
        public static extern bool LockWorkStation();//这个是调用windows的系统锁定

        [DllImport("user32.dll", EntryPoint = "FindWindow", CharSet = CharSet.Auto)]
        private extern static IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        public static extern int PostMessage(IntPtr hWnd, int msg, IntPtr wParam, IntPtr lParam);
        const int WM_CLOSE = 0x10;

        public MainWindowViewModel(SunflowerViewModel sunflowerViewModel, QrcodeViewModel qrcodeViewModel, CommandsViewModel commandsViewModel)
        {
            _sunflowerViewModel = sunflowerViewModel;
            _qrcodeViewModel = qrcodeViewModel;
            _commandsViewModel = commandsViewModel;
        }

        void InitNotifyIcon()
        {
            var menuStrip = new ContextMenuStrip();
            var setting = new ToolStripMenuItem { Text = "设置" };
            setting.Click += (s, e) => { Show(); };
            var exit = new ToolStripMenuItem { Text = "退出" };
            exit.Click += (s, e) => { _isFocus = true; Environment.Exit(0); };
            var remote = new ToolStripMenuItem { Text = "显示远程" };
            remote.Click += (s, e) => { ShowRemote(); };
            var lockPc = new ToolStripMenuItem { Text = "锁屏" };
            lockPc.Click += (s, e) => { LockWorkStation(); };
            menuStrip.Items.Add(setting);
            menuStrip.Items.Add(remote);
            menuStrip.Items.Add(lockPc);
            menuStrip.Items.Add(exit);
            var notifyIcon = new NotifyIcon
            {
                Text = "工具",
                Icon = new Icon(Path.GetFullPath(@"app.ico")),
                Visible = true,
                ContextMenuStrip = menuStrip
            };
        }

        public string LockTime
        {
            get => _lockTime; set
            {
                SetProperty(ref _lockTime, value);
                LocalConfig.Current.LockTime = value;
            }
        }
        public bool IsAutoLock
        {
            get => _isAutoLock; set
            {
                SetProperty(ref _isAutoLock, value);
                LocalConfig.Current.IsAutoLock = value;
            }
        }
        public string ReadQrcodeHotKey
        {
            get => _readQrcodeHotKey; set
            {
                SetProperty(ref _readQrcodeHotKey, value);
                LocalConfig.Current.ReadQrcodeHotKey = value;
                SetHotKey();
            }
        }
        public string CreateQrcodeHotKey
        {
            get => _createQrcodeHotKey; set
            {
                SetProperty(ref _createQrcodeHotKey, value);
                LocalConfig.Current.CreateQrcodeHotKey = value;
                SetHotKey();
            }
        }
        public string ConnectMachineHotKey
        {
            get => _connectMachineHotKey; set
            {
                SetProperty(ref _connectMachineHotKey, value);
                LocalConfig.Current.ConnectMachineHotKey = value;
                SetHotKey();
            }
        }
        public bool IsCommand
        {
            get => _isCommand; set
            {
                SetProperty(ref _isCommand, value);
                LocalConfig.Current.IsCommand = value;
                _commandsViewModel.IsEnable = LocalConfig.Current.IsCommand;
                if (LocalConfig.Current.IsCommand && _commands == null)
                {
                    _commands = new Commands();
                    _commands.Show();
                }
            }
        }

        void Show()
        {
            View.Visibility = Visibility.Visible;
        }

        void Hide()
        {
            View.Visibility = Visibility.Collapsed;
        }

        public bool IsAutoStart
        {
            get
            {
                try
                {
                    var reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                    if (reg == null)
                        reg = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                    if (reg?.GetValue(Application.ProductName)?.ToString().ToLower() == Application.ExecutablePath.ToLower())
                        return true;
                }
                catch (Exception)
                {
                }
                return false;
            }
            set
            {
                //开机启动
                try
                {
                    var reg = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run", true);
                    if (reg == null)
                        reg = Registry.CurrentUser.CreateSubKey(@"SOFTWARE\Microsoft\Windows\CurrentVersion\Run");
                    if (value)
                        reg?.SetValue(Application.ProductName, Application.ExecutablePath);
                    else
                        reg?.DeleteValue(Application.ProductName);
                }
                catch (Exception)
                {
                }
            }
        }

        [RelayCommand]
        private void Loaded()
        {
            IsCommand = LocalConfig.Current.IsCommand;
            IsAutoLock = LocalConfig.Current.IsAutoLock;
            LockTime = LocalConfig.Current.LockTime;
            ReadQrcodeHotKey = LocalConfig.Current.ReadQrcodeHotKey;
            CreateQrcodeHotKey = LocalConfig.Current.CreateQrcodeHotKey;
            ConnectMachineHotKey = LocalConfig.Current.ConnectMachineHotKey;
            View.Visibility = Visibility.Collapsed;
            InitNotifyIcon();
            AutoLock();
        }


        Commands _commands;
        private string _lockTime;
        private bool _isAutoLock;
        private bool _isCommand;
        private string _readQrcodeHotKey;
        private string _createQrcodeHotKey;
        private string _connectMachineHotKey;

        private void AutoLock()
        {
            Task.Run(() =>
            {
                while (true)
                {
                    if (DateTime.Now.ToString("HH:mm:ss") == LocalConfig.Current.LockTime + ":00")
                        LockWorkStation();
                    Thread.Sleep(1000);
                }
            });
        }

        [RelayCommand]
        private void CreateQrcode(KeyEventArgs a)
        {
            CreateQrcodeHotKey = a.Key.ToString();
        }

        [RelayCommand]
        private void ReadQrcode(KeyEventArgs a)
        {
            ReadQrcodeHotKey = a.Key.ToString();
        }

        [RelayCommand]
        private void ConnectMachine(KeyEventArgs a)
        {
            ConnectMachineHotKey = a.Key.ToString();
        }

        [RelayCommand]
        private void Closing(CancelEventArgs a)
        {
            if (!_isFocus)
            {
                a.Cancel = true;
                Hide();
            }
        }

        private void SetHotKey()
        {
            //快捷键
            HotkeyHelper.UnRegist(View);
            RegistCreateQrcodeHotKey();
            RegistReadQrcodeHotKey();
            RegistConnectMachineHotKey();
        }

        private void RegistHotKey(string keyStr, Action action)
        {
            if (string.IsNullOrEmpty(keyStr))
                return;
            var key = (Key)Enum.Parse(typeof(Key), keyStr);
            HotkeyHelper.Regist(View, HotkeyModifiers.None, key, () =>
            {
                if (View.Visibility == Visibility.Visible)
                    return;
                action();
            });
        }

        private void RegistCreateQrcodeHotKey()
        {
            RegistHotKey(LocalConfig.Current.CreateQrcodeHotKey, () =>
            {
                _qrcodeViewModel.Image = BitmapToBitmapSource((Bitmap)QrcodeHelper.GenerateQRCode(Clipboard.GetText()));
                if (_qrcode == null)
                    _qrcode = new Qrcode();
                _qrcode.Visibility = _qrcode.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
            });
        }

        private void RegistReadQrcodeHotKey()
        {
            RegistHotKey(LocalConfig.Current.ReadQrcodeHotKey, () =>
            {
                //查找MessageBox的弹出窗口,注意MessageBox对应的标题Title
                IntPtr ptr = FindWindow(null, "提示");
                if (ptr != IntPtr.Zero)
                {
                    //查找到窗口则关闭
                    PostMessage(ptr, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
                    return;
                }
                try
                {
                    var iData = Clipboard.GetDataObject();
                    Bitmap bitMap = null;
                    if (iData.GetDataPresent(DataFormats.Bitmap) || iData.GetDataPresent(DataFormats.MetafilePict))
                    {
                        bitMap = new Bitmap(Clipboard.GetImage());
                    }
                    else if (iData.GetDataPresent(DataFormats.FileDrop))
                    {
                        var files = Clipboard.GetFileDropList();
                        if (files.Count == 0) { return; }
                        try
                        {
                            bitMap = new Bitmap(Image.FromFile(files[0]));
                        }
                        catch (Exception)
                        {
                        }
                    }
                    if (bitMap == null)
                    {
                        MessageBox.Show("请先截图或复制图片", "提示", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                    Clipboard.SetText(QrcodeHelper.ReadQrCode(bitMap));
                    new NotifyWindow().Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            });
        }

        private void RegistConnectMachineHotKey()
        {
            RegistHotKey(LocalConfig.Current.ConnectMachineHotKey, () =>
            {
                ShowRemote();
            });
        }

        private void ShowRemote()
        {
            if (_sunflower == null)
                _sunflower = new Sunflower();
            _sunflower.Visibility = _sunflower.Visibility == Visibility.Visible ? Visibility.Hidden : Visibility.Visible;
            if (_sunflower.Visibility == Visibility.Visible)
                _sunflowerViewModel.InitMachines();
        }


        [DllImport("gdi32")]
        static extern int DeleteObject(IntPtr o);

        private BitmapSource BitmapToBitmapSource(Bitmap bt)
        {
            var ip = bt.GetHbitmap();
            var bitmapSource = System.Windows.Interop.Imaging.CreateBitmapSourceFromHBitmap(ip, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
            DeleteObject(ip);
            return bitmapSource;
        }

    }
}
