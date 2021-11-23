using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SagaLib
{
    public class SingleRunTask
    {
        private Timer myTimer;
        public int dueTime;

        public SingleRunTask()
        {
        }


        public SingleRunTask(int dueTime)
        {
            this.dueTime = dueTime;
        }

        public virtual void CallBack(object o)
        {

        }


        public void Activate()
        {
            this.myTimer = new Timer(new TimerCallback(this.CallBack), null, dueTime, Timeout.Infinite);
        }

        public void Deactivate()
        {
            this.myTimer.Dispose();
        }

    }
}
