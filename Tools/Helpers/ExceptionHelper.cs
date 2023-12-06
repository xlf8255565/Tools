using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using Microsoft.Win32;


namespace QrCodeDecode.Helpers
{
    /// <summary>
    /// 报错弹窗后应用程序会继续执行
    /// </summary>
    public class ExceptionHelper
    {
        public static void Handle()
        {
            //设置应用程序处理异常方式：ThreadException处理  
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            //处理UI线程异常  
            Application.ThreadException += Application_ThreadException;
            //处理非UI线程异常  
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
        }

        private static void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            GetExceptionMsg((Exception)e.ExceptionObject);
        }

        private static void Application_ThreadException(object sender, ThreadExceptionEventArgs e)
        {
            GetExceptionMsg(e.Exception);
        }

        public static void GetExceptionMsg(Exception ex)
        {
            MessageBox.Show(ex.ToString());
        }
    }
}
