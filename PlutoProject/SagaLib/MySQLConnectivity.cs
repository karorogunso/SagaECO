using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Threading;

using MySql.Data;
using MySql.Data.MySqlClient;

using SagaLib;

namespace SagaLib
{
    public abstract class MySQLConnectivity
    {
        class MySQLCommand
        {
            public enum CommandType
            {
                NonQuery,
                Query,
                Scalar
            }
            MySqlCommand cmd;
            DataRowCollection reader;
            CommandType type;
            uint scalar = uint.MaxValue;
            int errorCount = 0;

            public MySqlCommand Command { get { return this.cmd; } }

            public DataRowCollection DataRows { get { return this.reader;} set { this.reader = value; } }

            public CommandType Type { get { return this.type; } }

            public uint Scalar { get { return this.scalar; } set { this.scalar = value; } }

            public MySQLCommand(MySqlCommand cmd)
            {
                this.cmd = cmd;
                type = CommandType.NonQuery;
            }

            public MySQLCommand(MySqlCommand cmd, CommandType type)
            {
                this.cmd = cmd;
                this.type = type;
            }

            public int ErrorCount { get { return this.errorCount; } set { this.errorCount = value; } }
        }
        protected MySqlConnection db;
        protected MySqlConnection dbinactive;
        Thread mysqlPool;

        List<MySQLCommand> waitQueue = new List<MySQLCommand>();
        internal int cuurentCount = 0;

        public MySQLConnectivity()
        {
            mysqlPool = new Thread(new ThreadStart(ProcessMysql));
            mysqlPool.Start();
        }

        public bool CanClose
        {
            get
            {
                lock (waitQueue)
                    return (waitQueue.Count == 0 && cuurentCount == 0);
            }
        }

        void ProcessMysql()
        {
            while (true)
            {
                try
                {
                    MySQLCommand[] cmds;
                    lock (waitQueue)
                    {
                        if (waitQueue.Count > 0)
                        {
                            cmds = waitQueue.ToArray();
                            waitQueue.Clear();
                            cuurentCount = cmds.Length;
                        }
                        else
                            cmds = new MySQLCommand[0];
                    }
                    if (cmds.Length > 0)
                    {
                        List<MySQLCommand> pending = new List<MySQLCommand>();
                        DatabaseWaitress.EnterCriticalArea();

                        foreach (MySQLCommand i in cmds)
                        {
                            try
                            {
                                i.Command.Connection = db;
                                switch (i.Type)
                                {
                                    case MySQLCommand.CommandType.NonQuery:
                                        i.Command.ExecuteNonQuery();
                                        break;
                                    case MySQLCommand.CommandType.Query:
                                        MySqlDataAdapter adapter = new MySqlDataAdapter(i.Command);
                                        DataSet set = new DataSet();
                                        adapter.Fill(set);
                                        i.DataRows = set.Tables[0].Rows;
                                        break;
                                    case MySQLCommand.CommandType.Scalar:
                                        i.Scalar = Convert.ToUInt32(i.Command.ExecuteScalar());
                                        break;
                                }
                            }
                            catch (Exception ex)
                            {
                                Logger.ShowSQL("Error on query:" + command2String(i.Command), Logger.defaultlogger);
                                Logger.ShowSQL(ex, Logger.defaultlogger);
                                i.ErrorCount++;
                                if (i.ErrorCount > 10)
                                {
                                    Logger.ShowSQL("Error to many times, dropping command", Logger.defaultlogger);
                                }
                                else
                                    pending.Add(i);
                            }
                        }

                        DatabaseWaitress.LeaveCriticalArea();
                        if (pending.Count > 0)
                        {
                            lock (waitQueue)
                            {
                                foreach (MySQLCommand i in pending)
                                {
                                    waitQueue.Add(i);
                                }
                            }
                        }
                        pending = null;
                    }
                    cmds = null;
                    cuurentCount = 0;
                    Thread.Sleep(10);
                }
                catch (System.Threading.ThreadAbortException)
                {
                    DatabaseWaitress.LeaveCriticalArea();
                }
            }
        }
        
        public bool SQLExecuteNonQuery(string sqlstr)
        {
            lock (waitQueue)
            {
                MySQLCommand cmd = new MySQLCommand(new MySqlCommand(sqlstr));
                waitQueue.Add(cmd);
            }
            return true;
        }

        string command2String(MySqlCommand cmd)
        {
            string output;
            output = cmd.CommandText;
            if (cmd.Parameters.Count > 0)
            {
                string para = "";
                foreach (MySqlParameter i in cmd.Parameters)
                {
                    para += string.Format("{0}={1},", i.ParameterName, value2String(i.Value));
                }
                para = para.Substring(0, para.Length - 1);
                output = string.Format("{0} VALUES({1})", output, para);
            }
            return output;
        }

        string value2String(object val)
        {
            if (val.GetType() == typeof(byte[]))
            {
                byte[] tmp = (byte[])val;
                return "0x" + Conversions.bytes2HexString(tmp);
            }
            return val.ToString();
        }

        public bool SQLExecuteNonQuery(MySqlCommand cmd)
        {
            lock (waitQueue)
            {
                waitQueue.Add(new MySQLCommand(cmd));
            }
            return true;
        }

        public bool SQLExecuteScalar(string sqlstr, out uint index)
        {
            bool criticalarea = ClientManager.Blocked;
            bool result = true;
            if (criticalarea)
                ClientManager.LeaveCriticalArea();
            try
            {
                if (sqlstr.Substring(sqlstr.Length - 1) != ";") sqlstr += ";";
                sqlstr += "SELECT LAST_INSERT_ID();";
                MySQLCommand cmd = new MySQLCommand(new MySqlCommand(sqlstr), MySQLCommand.CommandType.Scalar);
                lock (waitQueue)
                {
                    waitQueue.Add(cmd);
                }
                while (cmd.Scalar == uint.MaxValue)
                {
                    Thread.Sleep(10);
                }
                index = cmd.Scalar;
            }
            catch (Exception ex)
            {
                Logger.ShowSQL(ex, Logger.defaultlogger);
                result = false;
                index = 0;
            }
            if (criticalarea)
                ClientManager.EnterCriticalArea();
            return result;
        }

        public DataRowCollection SQLExecuteQuery(string sqlstr)
        {
            DataRowCollection result;
            DataSet tmp;
            bool criticalarea = ClientManager.Blocked;
            if (criticalarea)
                ClientManager.LeaveCriticalArea();
            try
            {
                MySQLCommand cmd = new MySQLCommand(new MySqlCommand(sqlstr), MySQLCommand.CommandType.Query);
                lock (waitQueue)
                    waitQueue.Add(cmd);

                while (cmd.DataRows == null)
                {
                    Thread.Sleep(10);
                }
                result = cmd.DataRows;
                if (criticalarea)
                    ClientManager.EnterCriticalArea();
                return result;
            }
            catch (Exception ex)
            {
                Logger.ShowSQL("Error on query:" + sqlstr, Logger.defaultlogger);
                Logger.ShowSQL(ex, Logger.defaultlogger);
                if (criticalarea)
                    ClientManager.EnterCriticalArea();
                return null;
            }

        }

        public string ToSQLDateTime(DateTime date)
        {
            return string.Format("{0}-{1}-{2} {3}:{4}:{5}", date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
        }

        public void CheckSQLString(ref string str)
        {
            str = str.Replace("\\", "").Replace("'", "\\'");
        }

        public string CheckSQLString(string str)
        {
            return str.Replace("\\", "").Replace("'", "\\'");
        }

    }
}
