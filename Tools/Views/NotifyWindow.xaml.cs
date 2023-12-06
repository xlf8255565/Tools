using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Tools.Helpers;

namespace Tools.Views
{
    /// <summary>
    /// NotifyWindow.xaml 的交互逻辑
    /// </summary>
    public partial class NotifyWindow : Window
    {
        public NotifyWindow()
        {
            InitializeComponent();
            this.Loaded += NotifyWindow_Loaded;
            Left = -10000;
        }

        private void NotifyWindow_Loaded(object sender, RoutedEventArgs e)
        {
            Left = SystemParameters.WorkArea.Right - this.Width;
            Top = SystemParameters.WorkArea.Bottom;
            var animation = new DoubleAnimation
            {
                Duration = new Duration(TimeSpan.FromSeconds(0.5)),
                To = SystemParameters.WorkArea.Bottom - this.Height,
            };
            this.BeginAnimation(TopProperty, animation);

            //关闭弹窗
            TimerHelper.Delay(3000, () =>
            {
                Dispatcher.Invoke(() =>
                {
                    var animation = new DoubleAnimation
                    {
                        Duration = new Duration(TimeSpan.FromSeconds(0.3)),
                        To = SystemParameters.WorkArea.Bottom,
                    };
                    animation.Completed += (ss, ee) =>
                    {
                        this.Close();
                    };
                    this.BeginAnimation(TopProperty, animation);
                });
            });
        }
    }
}
