using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Security.Cryptography;

using SagaDB.Actor;
using SagaDB.Item;
using SagaLib;
//引入OLEDB
using System.Data.OleDb;
namespace SagaDB
{
    public abstract class AccessConnectivity
    {
        internal OleDbConnection db;
        internal OleDbConnection dbinactive;

        public void SQLExecuteNonQuery(string sqlstr)
        {
            bool criticalarea = ClientManager.Blocked;
            if (criticalarea)
                ClientManager.LeaveCriticalArea();
            DatabaseWaitress.EnterCriticalArea();
            try
            {
                //    MySqlHelper.ExecuteNonQuery(db, sqlstr, null);
                OleDbCommand tmp = new OleDbCommand(sqlstr, db);
                tmp.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                Logger.ShowSQL("Error on query:" + sqlstr, null);
                Logger.ShowSQL(ex, null);
            }
            DatabaseWaitress.LeaveCriticalArea();
            if (criticalarea)
                ClientManager.EnterCriticalArea();
        }


        public void SQLExecuteScalar(string sqlstr, ref uint index)
        {
            bool criticalarea = ClientManager.Blocked;
            if (criticalarea)
                ClientManager.LeaveCriticalArea();
            DatabaseWaitress.EnterCriticalArea();
            try
            {
                if (sqlstr.Substring(sqlstr.Length - 1) != ";") sqlstr += ";";
                sqlstr += "SELECT LAST_INSERT_ID();";

                OleDbCommand tmp = new OleDbCommand(sqlstr, db);
                index = Convert.ToUInt32(tmp.ExecuteScalar());
                //index = Convert.ToUInt32(MySqlHelper.ExecuteScalar(db, sqlstr));

            }
            catch (Exception ex)
            {
                Logger.ShowSQL("Error on query:" + sqlstr, null);
                Logger.ShowSQL(ex, null);
            }
            DatabaseWaitress.LeaveCriticalArea();
            if (criticalarea)
                ClientManager.EnterCriticalArea();
        }
        public DataRowCollection SQLExecuteQuery(string sqlstr)
        {
            DataRowCollection result;
            DataSet tmp =new DataSet();
            bool criticalarea = ClientManager.Blocked;
            if (criticalarea)
                ClientManager.LeaveCriticalArea();
            DatabaseWaitress.EnterCriticalArea();
            try
            {
                OleDbDataAdapter tmp2 = new OleDbDataAdapter(sqlstr, db);
                tmp2.Fill(tmp);

                //tmp = MySqlHelper.ExecuteDataset(db, sqlstr);
                if (tmp.Tables.Count == 0)
                {
                    throw new Exception("Unexpected Empty Query Result!");
                }
                result = tmp.Tables[0].Rows;
                DatabaseWaitress.LeaveCriticalArea();
                if (criticalarea)
                    ClientManager.EnterCriticalArea();
                return result;
            }
            catch (Exception ex)
            {
                Logger.ShowSQL("Error on query:" + sqlstr, null);
                Logger.ShowSQL(ex, null);
                DatabaseWaitress.LeaveCriticalArea();
                if (criticalarea)
                    ClientManager.EnterCriticalArea();
                return null;
            }

        }

        internal string ToSQLDateTime(DateTime date)
        {
            return string.Format("{0}-{1}-{2} {3}:{4}:{5}", date.Year, date.Month, date.Day, date.Hour, date.Minute, date.Second);
        }

        internal void CheckSQLString(ref string str)
        {
            str = str.Replace("'", "\\'");
        }
    }
}
