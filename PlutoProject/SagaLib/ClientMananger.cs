//#define Debug

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
        //public byte CheckUnLockSecond = 1;
        public static bool noCheckDeadLock = false;

        private static bool enteredcriarea = false;
        private static List<Thread> blockedThread = new List<Thread>();
        private static Dictionary<string, Thread> Threads = new Dictionary<string, Thread>();
        private static Thread currentBlocker;
        private static DateTime timestamp;
        private static string blockdetail;

//#if Debug
        private static StackTrace Stacktrace;
//#endif

        /// <summary>
        /// Command table contains the commands that need to be called when a
        /// packet is received. Key will be the packet type
        /// </summary>
        public Dictionary<ushort, Packet> commandTable;

        public ClientManager()
        {
           
        }

        public static bool Blocked
        {
            get
            {
                return (blockedThread.Contains(Thread.CurrentThread));
            }
        }

        public static void AddThread(Thread thread)
        {
            AddThread(thread.Name, thread);
        }

        public static void AddThread(string name,Thread thread)
        {
            if (!Threads.ContainsKey(name))
            {
                
                lock (Threads)
                {
                    try
                    {
                        Threads.Add(name, thread);
                    }
                    catch (Exception ex)
                    {
                        Logger.ShowError(ex);
                        Logger.ShowDebug("Threads count:" + Threads.Count, null);
                    }
                }
            }
        }

        public static void RemoveThread(string name)
        {
            if (Threads.ContainsKey(name))
            {
                lock (Threads)
                {
                    Threads.Remove(name);
                }
            }
        }

        public static Thread GetThread(string name)
        {
            if (Threads.ContainsKey(name))
            {
                lock (Threads)
                {
                    return Threads[name];
                }
            }
            else
                return null;
        }

        //solve deadlock
        public void checkCriticalArea()
        {
            while (true)
            {
                if (enteredcriarea)
                {
                    TimeSpan span = DateTime.Now - timestamp;
                    if (span.TotalSeconds > 10 && !noCheckDeadLock && !Debugger.IsAttached)
                    {
                        Logger.ShowError("Deadlock detected");
                        Logger.ShowError("Automatically unlocking....");
                        Logger.ShowDebug(blockdetail,Logger.defaultlogger);
//#if Debug
                        Logger.ShowError("Call Stack Before Entered Critical Area:");
                        try
                        {
                            Logger.ShowError("Thread name:" + getThreadName(currentBlocker));
                            foreach (StackFrame i in Stacktrace.GetFrames())
                            {
                                Logger.ShowError("at " + i.GetMethod().ReflectedType.FullName + "." + i.GetMethod().Name + " " + i.GetFileName() + ":" + i.GetFileLineNumber());
                            }
                        }
                        catch { }
                        Console.WriteLine();
//#endif
                        StackTrace running;
                        try
                        {
                            if (currentBlocker != null)
                            {
                                Logger.ShowError("Call Stack of current blocking Thread:");
                                Logger.ShowError("Thread name:" + getThreadName(currentBlocker));
                                if (currentBlocker.ThreadState != System.Threading.ThreadState.Running)
                                    Logger.ShowWarning("Unexpected thread state:" + currentBlocker.ThreadState.ToString());
                                currentBlocker.Suspend();
                                running = new StackTrace(currentBlocker, true);
                                currentBlocker.Resume();
                                foreach (StackFrame i in running.GetFrames())
                                {
                                    Logger.ShowError("at " + i.GetMethod().ReflectedType.FullName + "." + i.GetMethod().Name + " " + i.GetFileName() + ":" + i.GetFileLineNumber());
                                }
                            }
                        }
                        catch (Exception ex) { Logger.ShowError(ex); }
                        Console.WriteLine();
                        Logger.ShowError("Call Stack of all blocking Threads:");
                        Thread[] list = blockedThread.ToArray();
                        foreach (Thread j in list)
                        {
                            try
                            {
                                Logger.ShowError("Thread name:" + getThreadName(j));
                                if (j.ThreadState != System.Threading.ThreadState.Running)
                                    Logger.ShowWarning("Unexpected thread state:" + j.ThreadState.ToString());
                                j.Suspend();
                                running = new StackTrace(j, true);
                                j.Resume();
                                foreach (StackFrame i in running.GetFrames())
                                {
                                    Logger.ShowError("at " + i.GetMethod().ReflectedType.FullName + "." + i.GetMethod().Name + " " + i.GetFileName() + ":" + i.GetFileLineNumber());
                                }
                            }
                            catch (Exception ex) { Logger.ShowError(ex); }
                            Console.WriteLine();
                        }
                        Console.WriteLine();
                        Logger.ShowError("Call Stack of all Threads:");
                        string[] keys = new string[Threads.Keys.Count];
                        Threads.Keys.CopyTo(keys, 0);
                        foreach (string k in keys)
                        {
                            try
                            {
                                Thread j = GetThread(k);
                                Logger.ShowError("Thread name:" + k);
                                if (j.ThreadState != System.Threading.ThreadState.Running)
                                    Logger.ShowWarning("Unexpected thread state:" + j.ThreadState.ToString());
                                j.Suspend();
                                running = new StackTrace(j, true);
                                j.Resume();
                                foreach (StackFrame i in running.GetFrames())
                                {
                                    Logger.ShowError("at " + i.GetMethod().ReflectedType.FullName + "." + i.GetMethod().Name + " " + i.GetFileName() + ":" + i.GetFileLineNumber());
                                }
                            }
                            catch
                            {

                            }
                            Console.WriteLine();
                        }
                        LeaveCriticalArea(currentBlocker);
                    }
                }
                Thread.Sleep(5000);
            }
        }

        static string getThreadName(Thread thread)
        {
            foreach (string i in Threads.Keys)
            {
                if (thread == Threads[i])
                    return i;
            }
            return "";
        }

        public static void PrintAllThreads()
        {
            Logger.ShowWarning("Call Stack of all blocking Threads:");
            Thread[] list = blockedThread.ToArray();
            foreach (Thread j in list)
            {
                try
                {
                    Logger.ShowWarning("Thread name:" + getThreadName(j));
                    j.Suspend();
                    StackTrace running = new StackTrace(j, true);
                    j.Resume();
                    foreach (StackFrame i in running.GetFrames())
                    {
                        Logger.ShowWarning("at " + i.GetMethod().ReflectedType.FullName + "." + i.GetMethod().Name + " " + i.GetFileName() + ":" + i.GetFileLineNumber());
                    }
                }
                catch { }
                Console.WriteLine();
            }
            Logger.ShowWarning("Call Stack of all Threads:");
            string[] keys = new string[Threads.Keys.Count];
            Threads.Keys.CopyTo(keys, 0);
            foreach (string k in keys)
            {
                try
                {
                    Thread j = GetThread(k);
                    j.Suspend();
                    StackTrace running = new StackTrace(j, true);
                    j.Resume();
                    Logger.ShowWarning("Thread name:" + k);
                    foreach (StackFrame i in running.GetFrames())
                    {
                        Logger.ShowWarning("at " + i.GetMethod().ReflectedType.FullName + "." + i.GetMethod().Name + " " + i.GetFileName() + ":" + i.GetFileLineNumber());
                    }
                }
                catch
                {

                }
                Console.WriteLine();
            }
        }
       
        public static void EnterCriticalArea()
        {
            if (blockedThread.Contains(Thread.CurrentThread))
            {
                //Logger.ShowDebug("Current thread is already blocked, skip blocking to avoid deadlock!", Logger.defaultlogger);
            }
            else
            {
                //Global.clientMananger.AddWaitingWaitress();
                Global.clientMananger.waitressQueue.WaitOne();
                timestamp = DateTime.Now;
                enteredcriarea = true;
                blockedThread.Add(Thread.CurrentThread);
                currentBlocker = Thread.CurrentThread;
                blockdetail = "锁定线程名:" + Thread.CurrentThread.Name;
                //Global.clientMananger.waitressQueue.WaitOne();
//#if Debug
                Stacktrace = new StackTrace(1, true);
//#endif

            }
        }

        public static void LeaveCriticalArea()
        {
            LeaveCriticalArea(Thread.CurrentThread);
        }

        public static void LeaveCriticalArea(Thread blocker)
        {
            if (blockedThread.Contains(blocker) || blockedThread.Count != 0)
            {
                //Global.clientMananger.RemoveWaitingWaitress();
                // Global.clientMananger.waitressHasFinished.Set();
                int sec = (DateTime.Now - timestamp).Seconds;
                if (sec >= 1)
                {
                    Logger.ShowDebug(string.Format("Thread({0}) used unnormal time till unlock({1} sec)", blocker.Name, sec), Logger.defaultlogger);
                    
                }
                enteredcriarea = false;
                if (blockedThread.Contains(blocker))
                {
                    try
                    {
                        blockedThread.Remove(blocker);
                    }
                    catch(Exception ex)
                    {
                        if (blockedThread.Count > 0)
                            blockedThread.RemoveAt(0);
                        SagaLib.Logger.ShowError("a2333" + ex.ToString());
                    }
                }
                else
                {
                    if (blockedThread.Count > 0)
                        blockedThread.RemoveAt(0);
                    else
                        SagaLib.Logger.ShowError("线程不存在！！");
                }
                currentBlocker = null;
                timestamp = DateTime.Now;
                Global.clientMananger.waitressQueue.Set();
            }
            else
            {
                //Logger.ShowDebug("Current thread isn't blocked while trying unblock, skiping", Logger.defaultlogger);
                Global.clientMananger.waitressQueue.Set();
            }
        }


        public void Start() { }

        public void Stop()
        {
            this.listener.Stop();
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

        public virtual Client GetClientForName(string SessionName) { return null; }

        public virtual void NetworkLoop(int maxNewConnections) { }

        public virtual void OnClientDisconnect(Client client){ }

    }
}
