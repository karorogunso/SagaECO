using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using MySql.Data;
using MySql.Data.MySqlClient;

using SagaDB.Actor;
using SagaDB.Item;
using SagaLib;

namespace SagaDB
{
    public abstract class MySQLConnectivity
    {
        internal MySqlConnection db;
        internal MySqlConnection dbinactive;
        
        public void SQLExecuteNonQuery(string sqlstr)
        {
            bool criticalarea = ClientManager.enteredcriarea;
            if (criticalarea)
                ClientManager.LeaveCriticalArea();
            DatabaseWaitress.EnterCriticalArea();
            try
            {
                MySqlHelper.ExecuteNonQuery(db, sqlstr, null);

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
            bool criticalarea = ClientManager.enteredcriarea;
            if (criticalarea)
                ClientManager.LeaveCriticalArea();
            DatabaseWaitress.EnterCriticalArea();
            try
            {
                if (sqlstr.Substring(sqlstr.Length - 1) != ";") sqlstr += ";";
                sqlstr += "SELECT LAST_INSERT_ID();";
                index = Convert.ToUInt32(MySqlHelper.ExecuteScalar(db, sqlstr));

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
            DataSet tmp;
            bool criticalarea = ClientManager.enteredcriarea;
            if (criticalarea)
                ClientManager.LeaveCriticalArea();
            DatabaseWaitress.EnterCriticalArea();
            try
            {
                tmp = MySqlHelper.ExecuteDataset(db, sqlstr);
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

        private void CheckSQLString(ref string str)
        {
            str = str.Replace("'", "\\'");
        }
    }
}
