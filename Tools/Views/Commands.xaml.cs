using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Tools.Views
{
    /// <summary>
    /// Commands.xaml 的交互逻辑
    /// </summary>
    public partial class Commands : Window
    {
        public Commands()
        {
            InitializeComponent();
        }

        [DllImport("user32.dll")]
        public static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);
        [DllImport("user32.dll", SetLastError = true)]
        public static extern UInt32 GetWindowLong(IntPtr hWnd, int nIndex);

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);

            WindowInteropHelper wndHelper = new WindowInteropHelper(this);
            IntPtr HWND = wndHelper.Handle;
            int GWL_EXSTYLE = -20;
            SetWindowLong(HWND, GWL_EXSTYLE, (IntPtr)(0x8000000)); //让当前窗体不**输入焦点
        }
    }
}
