#define Debug

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace SagaLib
{
    public class ClientManager
    {
        public TcpListener listener;

        public  Thread packetCoordinator;

        public AutoResetEvent waitressQueue;
        public ManualResetEvent waitressHasFinished;
        public uint waitingWaitressesCount;
        public Object waitressCountLock;

        public static bool enteredcriarea = false;
        private static DateTime timestamp;

#if Debug
        private static StackTrace Stacktrace;
#endif

        /// <summary>
        /// Command table contains the commands that need to be called when a
        /// packet is received. Key will be the packet type
        /// </summary>
        public Dictionary<ushort, Packet> commandTable;

        public ClientManager() {}

        //solve deadlock
        public void checkCriticalArea()
        {
            while (true)
            {
                if (enteredcriarea)
                {
                    TimeSpan span = DateTime.Now - timestamp;
                    if (span.TotalSeconds > 30)
                    {
                        Logger.ShowError("Deadlock detected");
                        Logger.ShowError("Automatically unlocking....");
#if Debug
                        Logger.ShowError("Call Stack Before Entered Critical Area:");
                        foreach (StackFrame i in Stacktrace.GetFrames())
                        {
                            Logger.ShowError("at " + i.GetMethod().ReflectedType.FullName + "." + i.GetMethod().Name + " " + i.GetFileName() + ":" + i.GetFileLineNumber());
                        }
#endif
                        LeaveCriticalArea();
                    }
                }
                Thread.Sleep(10000);
            }
        }

        public void packetCoordinationLoop()
        {
            while (true)
            {
                uint count = 0;
                lock (this.waitressCountLock)
                {
                    count = this.waitingWaitressesCount;
                }

                if (count > 0)
                {
                    this.waitressQueue.Set();
                    this.waitressHasFinished.WaitOne();
                    this.waitressHasFinished.Reset();                    
                }
                else Thread.Sleep(1);
            }
        }

        public void AddWaitingWaitress()
        {
            lock (this.waitressCountLock)
            {
                this.waitingWaitressesCount++;
            }
        }

        public void RemoveWaitingWaitress()
        {
            lock (this.waitressCountLock)
            {
                this.waitingWaitressesCount--;
            }
        }


        public static void EnterCriticalArea()
        {
            Global.clientMananger.AddWaitingWaitress();
            Global.clientMananger.waitressQueue.WaitOne();
            enteredcriarea = true;
            timestamp = DateTime.Now;
#if Debug
            Stacktrace = new StackTrace(1, true);
#endif
        }

        public static void LeaveCriticalArea()
        {
            Global.clientMananger.RemoveWaitingWaitress();
            Global.clientMananger.waitressHasFinished.Set();
            enteredcriarea = false;
        }


        public void Start() { }

        public void Stop()
        {
            this.packetCoordinator.Abort();
        }


        /// <summary>
        /// Starts the network listener socket.
        /// </summary>
        public bool StartNetwork(int port)
        {
            /*IPAddress host = IPAddress.Parse(lcfg.Host);
            listener = new TcpListener(host, lcfg.Port);*/
            this.listener = new TcpListener(port);
            try { listener.Start(); }
            catch (Exception)
            {
                return false;
            }
            return true;

        }

        public virtual Client GetClient(uint SessionID) { return null; }

        public virtual void NetworkLoop(int maxNewConnections) { }

        public virtual void OnClientDisconnect(Client client){ }

    }
}
