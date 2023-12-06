using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace Tools.Helpers
{
    public class TimerHelper
    {
        /// <summary>
        /// 延迟执行任务
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Timer Delay(int interval, Action action)
        {
            var timer = new Timer(interval);
            timer.Elapsed += (s, e) =>
            {
                timer.Dispose();
                action();
            };
            timer.Start();
            return timer;
        }

        /// <summary>
        /// 循环执行任务
        /// </summary>
        /// <param name="interval"></param>
        /// <param name="action"></param>
        /// <returns></returns>
        public static Timer Loop(int interval, Action action)
        {
            var timer = new Timer(interval);
            timer.Elapsed += (s, e) => action();
            timer.Start();
            return timer;
        }
    }
}
