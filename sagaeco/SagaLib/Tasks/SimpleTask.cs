using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SagaLib
{
    public class SimpleTask : MultiRunTask
    {
        Action<SimpleTask> callback;
        bool shouldDeactivate;
        /// <summary>
        /// 封装了一个简单的默认定时器，方便简单的计时，默认销毁
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="dueTime">启动延迟时间</param>
        /// <param name="callback">定时器激活回调函数，参数为该定时器实例</param>
        public SimpleTask(string name, int dueTime, Action<SimpleTask> callback)
            : base(dueTime, dueTime, name)
        {
            this.callback = callback;
            shouldDeactivate = true;
        }

        /// <summary>
        /// 封装了一个简单的默认定时器，方便简单的计时，默认不销毁，需要手动销毁！
        /// </summary>
        /// <param name="name">名称</param>
        /// <param name="dueTime">启动延迟时间</param>
        /// <param name="callback">定时器激活回调函数，参数为该定时器实例</param>
        /// <param name="period">定时器的重复周期</param>
        public SimpleTask(string name, int dueTime, int period, Action<SimpleTask> callback)
            : base(dueTime, period, name)
        {
            this.callback = callback;
        }

        public override void CallBack()
        {
            if (shouldDeactivate)
                Deactivate();
            callback(this);
        }
    }
}
