using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GalaSoft.MvvmLight.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Threading;
using System.Windows;
using Tools.Helpers;
using Tools.Views;

namespace Tools.ViewModels
{
    public partial class SunflowerViewModel : BaseViewModel
    {
        [ObservableProperty]
        private ObservableCollection<MachineAccount> machines;
        private AppSettings _appSettings;

        public SunflowerViewModel() { }

        public SunflowerViewModel(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        public void InitMachines()
        {
            if (JsonConvert.SerializeObject(_appSettings.Machines) == JsonConvert.SerializeObject(Machines))
                return;
            Machines = _appSettings.Machines;
            View.Height = 75 + 29 * machines.Count;
            View.Left = SystemParameters.WorkArea.Right - View.Width;
            View.Top = (SystemParameters.WorkArea.Bottom - View.Height) / 2;
        }

        [RelayCommand]
        private void Connect(MachineAccount machine)
        {
            var handle = Win32.FindWindow(null, "向日葵远程控制");
            if (handle != IntPtr.Zero && handle == Win32.GetForegroundWindow())
            {
                Win32.Rect rectangle = new Win32.Rect();
                Win32.GetWindowRect(handle, ref rectangle);


                //双击账号输入框，只点两次有时会无效，这边多点几次
                var left = rectangle.left + 589;
                var top = rectangle.top + 210;

                for (int i = 0; i < 5; i++)
                {
                    MouseClick(left, top);
                }

                //输入账号
                InputKey(machine.Account);

                //双击密码输入框，只点两次有时会无效，这边多点几次
                left = rectangle.left + 600;
                top = rectangle.top + 306;
                for (int i = 0; i < 5; i++)
                {
                    MouseClick(left, top);
                }

                //输入密码
                InputKey(machine.Password);

                //点击连接按钮
                left = rectangle.left + 657;
                top = rectangle.top + 410;
                MouseClick(left, top);
                ClipboardHelper.SetText(machine.Account + " " + machine.Password);
                return;
            }
            handle = Win32.FindWindow(null, "ToDesk");
            if (handle != IntPtr.Zero && handle == Win32.GetForegroundWindow())
            {
                Win32.Rect rectangle = new Win32.Rect();
                Win32.GetWindowRect(handle, ref rectangle);


                //点击账号输入框
                var left = rectangle.left + 500;
                var top = rectangle.top + 293;
                MouseClick(left, top);

                //输入账号
                for (int i = 0; i < 20; i++)
                {
                    Win32.keybd_event(8, 0, 0, 0);
                    Thread.Sleep(10);
                    Win32.keybd_event(8, 0, 2, 0);
                }
                InputKey(machine.Account);

                //点击连接
                left = rectangle.left + 642;
                top = rectangle.top + 293;
                MouseClick(left, top);

                ////点击密码输入框
                //left = rectangle.left + 472;
                //top = rectangle.top + 257;
                //MouseClick(left, top);

                ////输入账号
                //for (int i = 0; i < 10; i++)
                //{
                //    Win32.keybd_event(8, 0, 0, 0);
                //    Thread.Sleep(10);
                //    Win32.keybd_event(8, 0, 2, 0);
                //}
                //InputKey(machine.Account);

                ////输入密码
                //InputKey(machine.Password);

                ////点击连接按钮
                //left = rectangle.left + 657;
                //top = rectangle.top + 410;
                //MouseClick(left, top);
            }
        }

        [RelayCommand]
        private void EnterPassword(MachineAccount machine)
        {
            if (string.IsNullOrEmpty(machine.WindowsPassword))
            {
                MessageBox.Show("Windows登录密码为空");
                return;
            }
            InputKey(machine.WindowsPassword);
            for (int i = 0; i < 3; i++)
            {
                Win32.keybd_event(13, 0, 0, 0);
                Thread.Sleep(10);
                Win32.keybd_event(13, 0, 2, 0);
            }
        }

        [RelayCommand]
        private void EnterCopy()
        {
            var str = Clipboard.GetText();
            InputKey(str);
        }

        [RelayCommand]
        private void Closing(CancelEventArgs a)
        {
            a.Cancel = true;
            View.Visibility = Visibility.Hidden;
        }

        private void MouseClick(int x, int y)
        {
            Win32.SetCursorPos(x, y);
            Win32.mouse_event(Win32.MouseEventFlag.LeftDown, x, y, 0, 0);
            Thread.Sleep(50);
            Win32.mouse_event(Win32.MouseEventFlag.LeftUp, x, y, 0, 0);
        }

        private void InputKey(string str)
        {
            foreach (var item in str)
            {
                var shiftNums = new List<char> { '~', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', ':', '"', '<', '>', '?', '|', '{', '}', '_', '+' };
                if (shiftNums.Contains(item) || Regex.IsMatch(item.ToString(), "[A-Z]"))
                    Win32.keybd_event(16, 0, 0, 0);

                Win32.keybd_event(Win32.VkKeyScanA(item), 0, 0, 0);
                Thread.Sleep(10);
                Win32.keybd_event(Win32.VkKeyScanA(item), 0, 2, 0);

                if (shiftNums.Contains(item) || Regex.IsMatch(item.ToString(), "[A-Z]"))
                    Win32.keybd_event(16, 0, 2, 0);
            }
        }

        private void CheckCmdWindow()
        {
            IntPtr handle = IntPtr.Zero;
            void SetPosition(int relativeX = 0, int relativeY = 0)
            {
                Win32.Rect rectangle = new Win32.Rect();
                Win32.GetWindowRect(handle, ref rectangle);
                View.Dispatcher.BeginInvoke(() =>
                {
                    View.Left = rectangle.left + rectangle.Width + relativeX;
                    View.Top = rectangle.top + relativeY;
                    View.Visibility = Visibility.Visible;
                });
            }

            TimerHelper.Loop(10, () =>
            {
                //bool isFind = false;
                //Win32.EnumWindows((a, b) =>
                //{
                //    handle = a;
                //    StringBuilder lpString = new StringBuilder(0x100);
                //    Win32.GetWindowText(a, lpString, lpString.Capacity);
                //    if (lpString.ToString() == "向日葵远程控制"|| lpString.ToString() == "ToDesk" || lpString.ToString().StartsWith("WIN-"))
                //    {
                //        if (handle == Win32.GetForegroundWindow())
                //        {
                //            isFind = true;
                //            SetPosition();
                //            return false;
                //        }
                //    }
                //    return true;
                //}, 0);
                //if (isFind)
                //    return;
                handle = Win32.FindWindow(null, "向日葵远程控制");
                if (handle != IntPtr.Zero && handle == Win32.GetForegroundWindow())
                {
                    SetPosition();
                    return;
                }
                handle = Win32.FindWindow(null, "ToDesk");
                if (handle != IntPtr.Zero && handle == Win32.GetForegroundWindow())
                {
                    SetPosition(-10, 10);
                    return;
                }
                View.Dispatcher.BeginInvoke(() =>
                {
                    View.Visibility = Visibility.Hidden;
                });
            });
        }

        [RelayCommand]
        public void ModifySettings()
        {
            OpenFile(AppDomain.CurrentDomain.BaseDirectory + "appsettings.json");
        }

        private void OpenFile(string NewFileName)
        {
            Process process = new Process();
            ProcessStartInfo processStartInfo = new ProcessStartInfo(NewFileName);
            process.StartInfo = processStartInfo;
            process.StartInfo.UseShellExecute = true;
            try
            {
                process.Start();
            }
            catch (Exception ex)
            {

            }
        }
    }
}
