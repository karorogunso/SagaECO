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
using System.Data;
namespace SagaDB
{
    public class AccessAccountDB : AccessConnectivity, AccountDB
    {
        private Encoding encoder = System.Text.Encoding.UTF8;
        private string Source;
        private DateTime tick = DateTime.Now;
        private bool isconnected;


        public AccessAccountDB(string Source)
        {
            this.Source = Source;
            this.isconnected = false;
            try
            {
                db = new OleDbConnection(string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};", this.Source));
                dbinactive = new OleDbConnection(string.Format("Provider=Microsoft.Jet.OLEDB.4.0;Data Source={0};", this.Source));
                db.Open();
            }
            catch (OleDbException ex)
            {
                Logger.ShowSQL(ex, null);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex, null);
            }
            if (db != null) { if (db.State != ConnectionState.Closed)this.isconnected = true; else { Console.WriteLine("SQL Connection error"); } }

        }

        public bool Connect()
        {
            if (!this.isconnected)
            {
                if (db.State == ConnectionState.Open) { this.isconnected = true; return true; }
                try
                {
                    db.Open();
                }
                catch (Exception) { }
                if (db != null) { if (db.State != ConnectionState.Closed)return true; else return false; }
            }
            return true;
        }

        public bool isConnected()
        {
            #region 暂时清除
            //if (this.isconnected)
            //{
            //    TimeSpan newtime = DateTime.Now - tick;
            //    if (newtime.TotalMinutes > 5)
            //    {
            //        OleDbConnection tmp;
            //        Logger.ShowSQL("AccountDB:Pinging SQL Server to keep the connection alive", null);
            //        /* we actually disconnect from the mysql server, because if we keep the connection too long
            //         * and the user resource of this mysql connection is full, mysql will begin to ignore our
            //         * queries -_-
            //         */
            //        tmp = dbinactive;
            //        if (tmp.State == ConnectionState.Open) tmp.Close();
            //        try
            //        {
            //            tmp.Open();
            //        }
            //        catch (Exception)
            //        {
            //            tmp = new MySqlConnection(string.Format("Server={1};Port={2};Uid={3};Pwd={4};Database={0};", database, host, port, dbuser, dbpass));
            //            tmp.Open();
            //        }
            //        dbinactive = db;
            //        db = tmp;
            //        tick = DateTime.Now;
            //    }

            //    if (db.State == System.Data.ConnectionState.Broken || db.State == System.Data.ConnectionState.Closed)
            //    {
            //        this.isconnected = false;
            //    }
            //}
            //return this.isconnected;
            #endregion
            if (db.State == ConnectionState.Open) { this.isconnected = true; return true; } else { this.isconnected =false; return false; }
        }


        #region AccountDB Members 接口成员

        public List<Account> GetAllAccount()
        {
            return null;
        }
        public void WriteUser(Account user)
        {
            string sqlstr;
            if (user != null && this.isConnected() == true)
            {
                sqlstr = string.Format("UPDATE `login` SET `username`='{0}',`password`='{1}',`deletepass`='{2}'" +
                     " WHERE account_id='{3}'",
                     user.Name, user.Password, user.DeletePassword, user.AccountID);
                try
                {
                    SQLExecuteNonQuery(sqlstr);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
        }

        public Account GetUser(string name)
        {
            string sqlstr;
            DataRowCollection result = null;
            Account account;
            CheckSQLString(ref name);
            sqlstr = "SELECT top 1 * FROM `login` WHERE username='" + name + "'";
            try
            {
                result = SQLExecuteQuery(sqlstr);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                return null;
            }
            if (result.Count == 0) return null;
            account = new Account();
         //   Console.WriteLine(result[0]["account_id"].ToString());
            account.AccountID = (int)result[0]["account_id"];
            account.Name = name;
            account.Password = (string)result[0]["password"];
            account.DeletePassword = (string)result[0]["deletepass"];
            account.GMLevel = (byte)result[0]["gmlevel"];
            return account;
        }

        public bool CheckPassword(string user, string password, uint frontword, uint backword)
        {
            string sqlstr;
            SHA1 sha1 = SHA1.Create();
            DataRowCollection result = null;
            sqlstr = "SELECT top 1 * FROM `login` WHERE username='" + user + "'";
            try
            {
                result = SQLExecuteQuery(sqlstr);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                return false;
            }
            if (result.Count == 0) return false;
            byte[] buf;
            string str = string.Format("{0}{1}{2}", frontword, ((string)result[0]["password"]).ToLower(), backword);
            buf = sha1.ComputeHash(System.Text.Encoding.ASCII.GetBytes(str));
            return password == Conversions.bytes2HexString(buf).ToLower();
        }

        public int GetAccountID(string user)
        {
            string sqlstr;
            DataRowCollection result = null;
            sqlstr = "SELECT top 1 * FROM `login` WHERE username='" + user + "'";
            try
            {
                result = SQLExecuteQuery(sqlstr);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                return -1;
            }
            if (result.Count == 0) return -1;
            return (int)result[0]["account_id"];
        }
        #endregion


    }
}
