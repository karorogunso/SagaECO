using System;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;
using System.Text;

namespace SagaLib
{
    public struct LogData
    {
        public Level LogLevel;
        public string Text;
        public NLog.Logger Logger;
    }

    public enum Level
    {
        Debug,
        Info,
        Warn,
        Error,
        SQL,
    }

    public class LoggerIntern
    {
        static ConcurrentQueue<LogData> queue = new ConcurrentQueue<LogData>();
        static Thread thread;
        static AutoResetEvent waiter = new AutoResetEvent(false);
        public static bool Ready = false;
        public static void Init()
        {
            if (thread == null)
            {
                thread = new Thread(MainLoop);
                thread.Start();
                Ready = true;
            }
        }

        static void MainLoop()
        {
            while (true)
            {
                LogData data;
                while (queue.TryDequeue(out data))
                {
                    switch (data.LogLevel)
                    {
                        case Level.Debug:
                            data.Logger.Debug(data.Text);
                            break;
                        case Level.Info :
                            data.Logger.Info(data.Text);
                            break;
                        case Level.Warn:
                            data.Logger.Warn(data.Text);
                            break;
                        case Level.Error:
                            data.Logger.Error(data.Text);
                            break;
                        case Level.SQL:
                            data.Logger.Debug(data.Text);
                            break;
                    }
                }
                waiter.WaitOne();
            }
        }

        public static void EnqueueMsg(Level level, string msg, NLog.Logger logger)
        {
            LogData data = new LogData();
            data.LogLevel = level;
            data.Text = msg;
            data.Logger = logger;
            queue.Enqueue(data);
            waiter.Set();
        }
    }
}
