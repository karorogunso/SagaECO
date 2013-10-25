using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace SagaDB
{
    public class DatabaseWaitress
    {
        public static AutoResetEvent waitressQueue = new AutoResetEvent(false);
        public static ManualResetEvent waitressHasFinished = new ManualResetEvent(false);
        public static uint waitingWaitressesCount;
        public static Object waitressCountLock = new object();
        public static Thread Coordinator;

        public static void AddWaitingWaitress()
        {
            lock (waitressCountLock)
            {
                waitingWaitressesCount++;
            }
        }

        public static void RemoveWaitingWaitress()
        {
            lock (waitressCountLock)
            {
                waitingWaitressesCount--;
            }
        }

        public static void packetCoordinationLoop()
        {
            while (true)
            {
                uint count = 0;
                lock (waitressCountLock)
                {
                    count = waitingWaitressesCount;
                }

                if (count > 0)
                {
                    waitressQueue.Set();
                    waitressHasFinished.WaitOne();
                    waitressHasFinished.Reset();
                }
                else Thread.Sleep(1);
            }
        }

        public static void EnterCriticalArea()
        {
            if (Coordinator == null)
            {
                Coordinator = new Thread(new ThreadStart(packetCoordinationLoop));
                Coordinator.Start();
            }
            AddWaitingWaitress();
            waitressQueue.WaitOne();            
        }

        public static void LeaveCriticalArea()
        {
            RemoveWaitingWaitress();
            waitressHasFinished.Set();            
        }

    }
}
