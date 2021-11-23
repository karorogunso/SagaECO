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
            :base()
        {
            this.host = host;
            this.port = port.ToString();
            this.dbuser = user;
            this.dbpass = pass;
            this.database = database;
            this.isconnected = false;
            try
            {
                db = new MySqlConnection(string.Format("Server={1};Port={2};Uid={3};Pwd={4};Database={0};Charset=utf8;", database, host, port, user, pass));
                dbinactive = new MySqlConnection(string.Format("Server={1};Port={2};Uid={3};Pwd={4};Database={0};Charset=utf8;", database, host, port, user, pass));
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
                    bool criticalarea = ClientManager.Blocked;
                    if (criticalarea)
                        ClientManager.LeaveCriticalArea();
                    DatabaseWaitress.EnterCriticalArea();
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
                    DatabaseWaitress.LeaveCriticalArea();
                    if (criticalarea)
                        ClientManager.EnterCriticalArea();
                }

                if (db.State == System.Data.ConnectionState.Broken || db.State == System.Data.ConnectionState.Closed)
                {
                    this.isconnected = false;
                }
            }
            return this.isconnected;
        }



        #region AccountDB Members
        void SavePaper(ActorPC aChar)
        {

        }
        public void WriteUser(Account user)
        {
            string sqlstr;
            if (user != null && this.isConnected() == true)
            {
                byte banned;
                if (user.Banned)
                    banned = 1;
                else
                    banned = 0;
                if(user.PlayerNames.Length < 1)
                {
                    sqlstr = string.Format("UPDATE `login` SET `username`='{0}',`password`='{1}',`deletepass`='{2}',`bank`='{4}',`banned`='{5}',`lastip`='{6}',`questresettime`='{7}',`lastlogintime`='{8}'," +
    "`macaddress` = '{9}'" +
     " WHERE account_id='{3}' LIMIT 1",
     user.Name, user.Password, user.DeletePassword, user.AccountID, user.Bank, banned, user.LastIP, user.questNextTime, DateTime.Now, user.MacAddress);

                }
                else
                {
                    sqlstr = string.Format("UPDATE `login` SET `username`='{0}',`password`='{1}',`deletepass`='{2}',`bank`='{4}',`banned`='{5}',`lastip`='{6}',`questresettime`='{7}',`lastlogintime`='{8}'," +
    "`macaddress` = '{9}',`playernames` = '{10}'" +
     " WHERE account_id='{3}' LIMIT 1",
     user.Name, user.Password, user.DeletePassword, user.AccountID, user.Bank, banned, user.LastIP, user.questNextTime, DateTime.Now, user.MacAddress, user.PlayerNames);

                }
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

        public List<Account> GetAllAccount()
        {
            List<Account> accounts = new List<Account>();
            string sqlstr;
            DataRowCollection result = null;
            Account account;
            sqlstr = "SELECT * FROM `login`";
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
            for (int i = 0; i < result.Count; i++)
            {
                account = new Account();
                account.AccountID = (int)(uint)result[i]["account_id"];
                account.Name = (string)result[i]["username"];
                account.Password = (string)result[i]["password"];
                account.DeletePassword = (string)result[i]["deletepass"];
                account.GMLevel = (byte)result[i]["gmlevel"];
                account.Bank = (uint)result[i]["bank"];
                account.questNextTime = (DateTime)result[i]["questresettime"];
                account.lastLoginTime = (DateTime)result[i]["lastlogintime"];
                try
                {
                    account.LastIP = (string)result[i]["lastip"];
                }
                catch { }
                accounts.Add(account);
            }
            return accounts;
        }

        public Account GetUser(string name)
        {
            string sqlstr;
            DataRowCollection result = null;
            Account account;
            CheckSQLString(ref name);
            sqlstr = "SELECT * FROM `login` WHERE `username`='" + name + "' LIMIT 1";
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
            account.Bank = (uint)result[0]["bank"];
            account.questNextTime = (DateTime)result[0]["questresettime"];
            try
            {
                account.LastIP2 = (string)result[0]["lastip2"];
            }
            catch { }
            if (((byte)result[0]["banned"]) == 1)
                account.Banned = true;
            else
                account.Banned = false;
            return account;
        }

        public bool CheckPassword(string user, string password, uint frontword, uint backword)
        {
            string sqlstr;
            SHA1 sha1 = SHA1.Create();
            DataRowCollection result = null;
            sqlstr = "SELECT * FROM `login` WHERE `username`='" + CheckSQLString(user) + "' LIMIT 1";
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
            sqlstr = "SELECT * FROM `login` WHERE `username`='" + user + "' LIMIT 1";
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
