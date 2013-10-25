using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SagaLib
{
    public class MultiRunTask
    {
        private Timer myTimer;
        public int dueTime;
        public int period;
        public delegate void func();
        public func Func;

        public MultiRunTask()
        {
        }

        public MultiRunTask(int dueTime, int period)
        {
            this.dueTime = dueTime;
            this.period = period;
        }

        public virtual void CallBack(object o)
        {
            if (Func != null) Func.Invoke();
        }

        public bool Activated()
        {
            if (this.myTimer != null) return true; return false;
        }

        public void Activate()
        {
            this.myTimer = new Timer(new TimerCallback(this.CallBack), null, this.dueTime, this.period);
        }

        public virtual void Deactivate()
        {
            if (this.myTimer == null) return;
            this.myTimer.Dispose();
            this.myTimer = null;
        }

    }
}
