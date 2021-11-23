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
        public static MySQLConnectivity defaultSql;

        public enum EventType
        {
            ItemGolemGet = 1,
            ItemLootGet = 2,
            ItemWareGet = 4,
            ItemNPCGet = 8,
            ItemVShopGet = 16,
            ItemTradeGet = 32,
            ItemGMGet = 64,
            ItemGolemLost = 128,
            ItemUseLost = 256,
            ItemWareLost = 512,
            ItemNPCLost = 1024,
            ItemTradeLost = 2048,
            ItemDropLost = 4096,
            GoldChange = 8192,
            WarehouseGet = 16384,
            WarehousePut = 32768,
            GMCommand = 65536,
        }

        public static BitMask<EventType> SQLLogLevel = new BitMask<EventType>(new BitMask(68712));

        static string GetStackTrace()
        {
            string[] trace = new StackTrace(2, false).ToString().Split('\n');
            string Stacktrace = "";
            foreach (string i in trace)
            {
                if (i.Contains(" System."))
                    continue;
                Stacktrace += i.Replace("\r", "\r\n");
            }
            int size = 1024;
            if (size > Stacktrace.Length)
                size = Stacktrace.Length;
            Stacktrace.Substring(0, size);
            return Stacktrace;
        }

        public static void LogItemGet(EventType type, string pc, string item, string detail, bool stack)
        {
            if (type >= EventType.ItemGolemGet && type <= EventType.ItemGMGet)
            {
                if (defaultSql != null && SQLLogLevel.Test(type))
                {
                    string content = detail;
                    if (stack)
                        content += ("\r\n" + GetStackTrace());
                    SQLLog(type, pc, item, content);
                }
            }
        }

        public static void LogItemLost(EventType type, string pc, string item, string detail, bool stack)
        {
            if (type >= EventType.ItemGolemLost && type <= EventType.ItemDropLost)
            {
                if (defaultSql != null && SQLLogLevel.Test(type))
                {
                    string content = detail;
                    if (stack)
                        content += ("\r\n" + GetStackTrace());
                    SQLLog(type, pc, item, content);
                }
            }
        }

        public static void LogGoldChange(string pc, long amount)
        {
            if (defaultSql != null && SQLLogLevel.Test(EventType.GoldChange))
            {                
                SQLLog(EventType.GoldChange, pc, amount.ToString(), GetStackTrace());
            }
        }

        public static void LogWarehouseGet(string pc, string item, string detail)
        {
            if (defaultSql != null && SQLLogLevel.Test(EventType.WarehouseGet))
            {
                SQLLog(EventType.WarehouseGet, pc, item, detail);
            }
        }

        public static void LogWarehousePut(string pc, string item, string detail)
        {
            if (defaultSql != null && SQLLogLevel.Test(EventType.WarehouseGet))
            {   
                SQLLog(EventType.WarehousePut, pc, item, detail);
            }
        }

        public static void LogGMCommand(string pc, string item, string detail)
        {
            if (defaultSql != null && SQLLogLevel.Test(EventType.GMCommand))
            {
                SQLLog(EventType.GMCommand, pc, item, detail);
            }
        }

        static void SQLLog(EventType type, string src, string dst, string detail)
        {
            DateTime time = DateTime.Now;
            defaultSql.CheckSQLString(ref src);
            string sql = string.Format("INSERT INTO `log`(`eventType`,`eventTime`,`src`,`dst`,`detail`) VALUES ('{0}','{1}','{2}','{3}','{4}');",
                type, defaultSql.ToSQLDateTime(time), defaultSql.CheckSQLString(src), defaultSql.CheckSQLString(dst), defaultSql.CheckSQLString(detail));
            try
            {
                defaultSql.SQLExecuteNonQuery(sql);
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }
    }
}
