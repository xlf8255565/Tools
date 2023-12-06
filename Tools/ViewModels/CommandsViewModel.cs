using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Tools.Helpers;

namespace Tools.ViewModels
{
    public partial class CommandsViewModel : BaseViewModel
    {
        private AppSettings _appSettings;
        [ObservableProperty]
        private List<CmdConfigEntity> _cmdGroups;
        [ObservableProperty]
        private CmdConfigEntity _selectedCmdGroup;

        public bool IsEnable { get; internal set; }

        public CommandsViewModel(AppSettings appSettings)
        {
            _appSettings = appSettings;
        }

        [RelayCommand]
        private void Loaded()
        {
            //CheckCmdWindow();
            if (JsonConvert.SerializeObject(CmdGroups) == JsonConvert.SerializeObject(_appSettings.Cmds))
                return;
            CmdGroups = _appSettings.Cmds;
            SelectedCmdGroup = CmdGroups.First();
            View.Height = 75 + 29 * SelectedCmdGroup.Commands.Count;
            //View.Left = SystemParameters.WorkArea.Right - View.Width;
            //View.Top = (SystemParameters.WorkArea.Bottom - View.Height) / 2;
            CheckCmdWindow();
        }

        private void CheckCmdWindow()
        {
            TimerHelper.Loop(10, () =>
            {
                var handle = Win32.FindWindow("ConsoleWindowClass", null);
                if (IsEnable && handle != IntPtr.Zero && handle == Win32.GetForegroundWindow())
                {
                    Win32.Rect rectangle = new Win32.Rect();
                    Win32.GetWindowRect(handle, ref rectangle);
                    View.Dispatcher.Invoke(() =>
                    {
                        View.Left = rectangle.left + rectangle.Width;
                        View.Top = rectangle.top + 1;
                        View.Show();
                    });
                }
                else
                {
                    View.Dispatcher.Invoke(() =>
                    {
                        View.Hide();
                    });
                }
            });
        }

        [RelayCommand]
        private void SelectedCmdChanged(CmdCommmand item)
        {
            RunCommand(item.Value);
        }

        public void RunCommand(string command)
        {
            //包含中文时采用复制粘贴的方式，但是偶尔会失灵
            if (Regex.IsMatch(command, @"[\u4e00-\u9fa5]"))
            {
                var clipText = Clipboard.GetText();
                Clipboard.SetText(command);
                System.Drawing.Point point;
                Win32.GetCursorPos(out point);
                Win32.SetCursorPos((int)View.Left - 100, (int)View.Top + 100);
                Win32.mouse_event(Win32.MouseEventFlag.RightDown | Win32.MouseEventFlag.RightUp, 0, 0, 0, 0);
                Win32.keybd_event(Win32.VkKeyScanA('p'), 0, 0, 0);
                Win32.keybd_event(Win32.VkKeyScanA('p'), 0, 2, 0);
                Win32.SetCursorPos(point.X, point.Y);
                if (command.EndsWith("\n"))
                {
                    Win32.keybd_event(Win32.VkKeyScanA('\n'), 0, 0, 0);
                    Win32.keybd_event(Win32.VkKeyScanA('\n'), 0, 2, 0);
                }
                TimerHelper.Delay(100, () =>
                {
                    if (!string.IsNullOrEmpty(clipText))
                        Clipboard.SetText(clipText);
                    else
                        Clipboard.Clear();
                });
            }
            else
            {
                foreach (var item in command)
                {
                    var shiftNums = new List<char> { '~', '!', '@', '#', '$', '%', '^', '&', '*', '(', ')', ':', '"', '<', '>', '?', '|', '{', '}', '_', '+' };
                    if (shiftNums.Contains(item) || Regex.IsMatch(item.ToString(), "[A-Z]"))
                        Win32.keybd_event(18, 0, 0, 0);
                    if (item == '←')
                    {
                        Win32.keybd_event(37, 0, 0, 0);
                        Win32.keybd_event(37, 0, 2, 0);
                    }
                    else if (item == '↓')
                    {
                        Win32.keybd_event(13, 0, 0, 0);
                        Win32.keybd_event(13, 0, 2, 0);
                    }
                    else
                    {
                        Win32.keybd_event(Win32.VkKeyScanA(item), 0, 0, 0);
                        Win32.keybd_event(Win32.VkKeyScanA(item), 0, 2, 0);
                    }
                    if (shiftNums.Contains(item) || Regex.IsMatch(item.ToString(), "[A-Z]"))
                        Win32.keybd_event(18, 0, 2, 0);
                }
            }
            //CommandPlugList.ForEach(q => q.OnCommandExecute(command));
        }
    }
}
