using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Diagnostics;

namespace SagaLib
{
    public partial class Logger
    {
        public static Logger defaultlogger;
        public static Logger CurrentLogger = defaultlogger;
        private string path;
        private string filename;

        public LogContent LogLevel = (LogContent)31;

        public enum LogContent
        {
            Info = 1,
            Warning = 2,
            Error = 4,
            SQL = 8,
            Debug = 16,
            Custom = 32,
        }
        public Logger(string filename)
        {
            this.filename = filename;
            path = GetLogFile();
            if (!File.Exists(path))
            {
                FileStream f =  File.Create(path);
                f.Close();
            }
        }


        /*public Logger(string path)
        {
            if (!System.IO.Directory.Exists("Log"))
                System.IO.Directory.CreateDirectory("Log");
            this.path = path;
            if (!File.Exists(path))
            {
                System.IO.File.Create(path);
            }
        }*/

        public void WriteLog(string p)
        {
            try
            {
                path = GetLogFile();
                FileStream file = new FileStream(path, FileMode.Append);
                StreamWriter sw = new StreamWriter(file);
                string final = GetDate() + "|" + p; // Add character to make exploding string easier for reading specific log entry by ReadLog()
                sw.WriteLine(final);
                sw.Close();
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }

        }

        public void WriteLog(string prefix,string p)
        {
            try
            {
                path = GetLogFile(); 
                p = string.Format("{0}->{1}", prefix, p);
                FileStream file = new FileStream(path, FileMode.Append);
                StreamWriter sw = new StreamWriter(file);
                string final = GetDate() + "|" + p; // Add character to make exploding string easier for reading specific log entry by ReadLog()
                sw.WriteLine(final);
                sw.Close();
            }
            catch (Exception)
            {
                
            }

        }

        public static void ShowInfo(Exception ex, Logger log)
        {
            if ((defaultlogger.LogLevel | LogContent.Info) != defaultlogger.LogLevel)
                return;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[Info]");
            Console.ResetColor();
            Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            if (log != null)
            {
                log.WriteLog(ex.Message);
            }
        }

        public static void ShowInfo(string ex)
        {
            if ((defaultlogger.LogLevel | LogContent.Info) != defaultlogger.LogLevel)
                return;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[Info]");
            Console.ResetColor();
            Console.WriteLine(ex);
        }

        public static void ShowInfo(string ex, Logger log)
        {
            if ((defaultlogger.LogLevel | LogContent.Info) != defaultlogger.LogLevel)
                return;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("[Info]");
            Console.ResetColor();
            Console.WriteLine(ex);
            if (log != null)
            {
                log.WriteLog(ex);
            }
        }
        public static NLog.Logger DefaultLogger = null;
        public static void ShowSQL(Exception ex)
        {
            LoggerIntern.EnqueueMsg(Level.SQL, ex.ToString(), DefaultLogger);
        }

        public static void ShowSQL(string ex)
        {
            LoggerIntern.EnqueueMsg(Level.SQL, ex, DefaultLogger);
        }

        public static void ShowWarning(Exception ex)
        {
            if ((defaultlogger.LogLevel | LogContent.Warning) != defaultlogger.LogLevel)
                return;
            ShowWarning(ex, defaultlogger);
        }

        public static void ShowWarning(string ex)
        {
            if ((defaultlogger.LogLevel | LogContent.Warning) != defaultlogger.LogLevel)
                return;
            ShowWarning(ex, defaultlogger);
        }

        public static void ShowDebug(Exception ex, Logger log)
        {
            if ((defaultlogger.LogLevel | LogContent.Debug) != defaultlogger.LogLevel)
                return;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("[Debug]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            Console.ResetColor();
            if (log != null)
                log.WriteLog("[Debug]" + ex.Message + "\r\n" + ex.StackTrace);
        }

        public static void ShowDebug(string ex, Logger log)
        {
            if ((defaultlogger.LogLevel | LogContent.Debug) != defaultlogger.LogLevel)
                return;
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write("[Debug]");
            Console.ForegroundColor = ConsoleColor.White;
            StackTrace Stacktrace = new StackTrace(1, true);
            string txt = ex;
            foreach (StackFrame i in Stacktrace.GetFrames())
            {
                txt = txt + "\r\n      at " + i.GetMethod().ReflectedType.FullName + "." + i.GetMethod().Name + " " + i.GetFileName() + ":" + i.GetFileLineNumber();
            }
            txt = FilterSQL(txt);
            Console.WriteLine(txt);
            Console.ResetColor();
            if (log != null)
            {
                log.WriteLog("[Debug]" + txt);
            }
        }

        public static void ShowSQL(Exception ex, Logger log)
        {
            if ((defaultlogger.LogLevel | LogContent.SQL) != defaultlogger.LogLevel)
                return;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("[SQL]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(ex.Message + "\r\n" + FilterSQL(ex.StackTrace));
            Console.ResetColor();
            if (log != null)
                log.WriteLog("[SQL]" + ex.Message + "\r\n" + FilterSQL(ex.StackTrace));
        }

        private static string FilterSQL(string input)
        {
            string[] tmp = input.Split('\n');
            string tmp2 = "";
            foreach (string i in tmp)
            {
                if (!i.Contains(" MySql.") && !i.Contains(" System."))
                    tmp2 = tmp2 + i + "\n";
            }
            return tmp2;
        }

        public static void ShowSQL(String ex, Logger log)
        {
            if ((defaultlogger.LogLevel | LogContent.SQL) != defaultlogger.LogLevel)
                return;
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write("[SQL]");
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(ex);
            Console.ResetColor();
            if (log != null)
                log.WriteLog("[SQL]" + ex);
        }

        public static void ShowWarning(Exception ex, Logger log)
        {
            if ((defaultlogger.LogLevel | LogContent.Warning) != defaultlogger.LogLevel)
                return;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[Warning]");
            Console.ResetColor();
            Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
            if (log != null)
            {
                log.WriteLog("Warning:" + ex.ToString());
            }
        }

        public static void ShowWarning(string ex, Logger log)
        {
            if ((defaultlogger.LogLevel | LogContent.Warning) != defaultlogger.LogLevel)
                return;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write("[Warning]");
            Console.ResetColor();
            Console.WriteLine(ex);
            if (log != null)
            {
                log.WriteLog("Warning:" + ex);
            }
        }
        public static void ShowError(Exception ex, Logger log)
        {
            try
            {
                if ((defaultlogger.LogLevel | LogContent.Error) != defaultlogger.LogLevel)
                    return;
                if (log == null) log = defaultlogger;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("[Error]");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(ex.Message + "\r\n" + ex.StackTrace);
                Console.ResetColor();
                log.WriteLog("[Error]" + ex.Message + "\r\n" + ex.StackTrace);
            }
            catch { }
        }

        public static void ShowError(string ex, Logger log)
        {
            try
            {
                if ((defaultlogger.LogLevel | LogContent.Error) != defaultlogger.LogLevel)
                    return;
                if (log == null) log = defaultlogger;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.Write("[Error]");
                Console.ForegroundColor = ConsoleColor.White;
                Console.WriteLine(ex);
                Console.ResetColor();
                log.WriteLog("[Error]" + ex);
            }
            catch { }
        }

        public static void ShowError(string ex)
        {
            if ((defaultlogger.LogLevel | LogContent.Error) != defaultlogger.LogLevel)
                return;
            ShowError(ex, defaultlogger);
        }

        public static void ShowError(Exception ex)
        {
            if ((defaultlogger.LogLevel | LogContent.Error) != defaultlogger.LogLevel)
                return;
            ShowError(ex, defaultlogger);
        }

        public string GetLogFile()
        {
            // Read in from XML here if needed.
            if (!System.IO.Directory.Exists("Log"))
                System.IO.Directory.CreateDirectory("Log");

            return "Log/[" + string.Format("{0}-{1}-{2}", DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day) + "]" + filename;
        }

        public string GetDate()
        {
            return DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();
        }

        public static void ProgressBarShow(uint progressPos, uint progressTotal, string label)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\r[Info]");
            Console.ResetColor();
            Console.Write(string.Format("{0} [", label));
            StringBuilder sb = new StringBuilder();
            //sb.AppendFormat("\r{0} [", label);
            uint barPos = progressPos * 40 / progressTotal + 1;
            for (uint p = 0; p < barPos; p++) sb.AppendFormat("#");
            for (uint p = barPos; p < 40; p++) sb.AppendFormat(" ");
            sb.AppendFormat("] {0}%\r", progressPos * 100 / progressTotal);
            Console.ForegroundColor = ConsoleColor.White;
            Console.Write(sb.ToString());
            Console.ResetColor();
        }

        public static void ProgressBarHide(string label)
        {
            //char[] buffer = new char[80];
            //label.CopyTo(0, buffer, 0, label.Length);
            //Console.Write(buffer);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write("\r[Info]");
            Console.ResetColor();
            Console.Write(string.Format("{0}                                                                                            \r", label));
        }
    }
}
