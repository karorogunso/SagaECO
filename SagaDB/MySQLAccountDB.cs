using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Security.Cryptography;


using SagaDB.Actor;
using SagaDB.Item;
using SagaLib;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SagaDB
{
    public class MySQLAccountDB : MySQLConnectivity,AccountDB 
    {
        private Encoding encoder = System.Text.Encoding.UTF8;
        private string host;
        private string port;
        private string database;
        private string dbuser;
        private string dbpass;
        private DateTime tick = DateTime.Now;
        private bool isconnected;


        public MySQLAccountDB(string host, int port, string database, string user, string pass)
        {
            this.host = host;
            this.port = port.ToString();
            this.dbuser = user;
            this.dbpass = pass;
            this.database = database;
            this.isconnected = false;
            try
            {
                db = new MySqlConnection(string.Format("Server={1};Port={2};Uid={3};Pwd={4};Database={0};", database, host, port, user, pass));
                dbinactive = new MySqlConnection(string.Format("Server={1};Port={2};Uid={3};Pwd={4};Database={0};", database, host, port, user, pass));
                db.Open();
            }
            catch (MySqlException ex)
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
            if (this.isconnected)
            {
                TimeSpan newtime = DateTime.Now - tick;
                if (newtime.TotalMinutes > 5)
                {
                    MySqlConnection tmp;
                    Logger.ShowSQL("AccountDB:Pinging SQL Server to keep the connection alive", null);
                    /* we actually disconnect from the mysql server, because if we keep the connection too long
                     * and the user resource of this mysql connection is full, mysql will begin to ignore our
                     * queries -_-
                     */
                    tmp = dbinactive;
                    if (tmp.State == ConnectionState.Open) tmp.Close();
                    try
                    {
                        tmp.Open();
                    }
                    catch (Exception)
                    {
                        tmp = new MySqlConnection(string.Format("Server={1};Port={2};Uid={3};Pwd={4};Database={0};", database, host, port, dbuser, dbpass));
                        tmp.Open();
                    }
                    dbinactive = db;
                    db = tmp;
                    tick = DateTime.Now;
                }

                if (db.State == System.Data.ConnectionState.Broken || db.State == System.Data.ConnectionState.Closed)
                {
                    this.isconnected = false;
                }
            }
            return this.isconnected;
        }

        

        #region AccountDB Members

        public void WriteUser(Account user)
        {
            string sqlstr;
            if (user != null && this.isConnected() == true)
            {
                sqlstr = string.Format("UPDATE `login` SET `username`='{0}',`password`='{1}',`deletepass`='{2}'" +
                     " WHERE account_id='{3}' LIMIT 1",
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
            sqlstr = "SELECT * FROM `login` WHERE username='" + name + "' LIMIT 1";
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
            account.AccountID = (int)(uint)result[0]["account_id"];
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
            sqlstr = "SELECT * FROM `login` WHERE username='" + user + "' LIMIT 1";
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
            sqlstr = "SELECT * FROM `login` WHERE username='" + user + "' LIMIT 1";
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
