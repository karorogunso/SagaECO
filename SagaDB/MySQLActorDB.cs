using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using SagaDB.Actor;
using SagaDB.Item;
using SagaLib;
using MySql.Data;
using MySql.Data.MySqlClient;

namespace SagaDB
{
    public class MySQLActorDB :MySQLConnectivity, ActorDB
    {
        private Encoding encoder = System.Text.Encoding.UTF8;
        private string host;
        private string port;
        private string database;
        private string dbuser;
        private string dbpass;
        private DateTime tick = DateTime.Now;
        private bool isconnected;


        public MySQLActorDB(string host, int port, string database, string user, string pass)
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
                    Logger.ShowSQL("ActorDB:Pinging SQL Server to keep the connection alive", null);
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

       
        public void CreateChar(ActorPC aChar, int account_id)
        {
            string sqlstr;
            uint charID = 0;
            if (aChar != null && this.isConnected() == true)
            {
                sqlstr = string.Format("INSERT INTO `char`(`account_id`,`name`,`race`,`gender`,`hairStyle`,`hairColor`,`wig`," +
                    "`face`,`job`,`mapID`,`lv`,`jlv1`,`jlv2x`,`jlv2t`,`questRemaining`,`slot`,`x`,`y`,`dir`,`hp`,`max_hp`,`mp`," +
                    "`max_mp`,`sp`,`max_sp`,`str`,`dex`,`intel`,`vit`,`agi`,`mag`,`gold`" +
                    ") VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}'," +
                    "'{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}'" +
                    ",'{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}')",
                    account_id, aChar.Name, (byte)aChar.Race, (byte)aChar.Gender, aChar.HairStyle, aChar.HairColor, aChar.Wig,
                    aChar.Face, (byte)aChar.Job, aChar.MapID, aChar.Level, aChar.JobLevel1, aChar.JobLevel2X, aChar.JobLevel2T,
                    aChar.QuestRemaining, aChar.Slot, Global.PosX16to8(aChar.X), Global.PosY16to8(aChar.Y), (byte)(aChar.Dir / 45), aChar.HP, aChar.MaxHP, aChar.MP,
                    aChar.MaxMP, aChar.SP, aChar.MaxSP, aChar.Str, aChar.Dex, aChar.Int, aChar.Vit, aChar.Agi, aChar.Mag, aChar.Gold);
                try
                {
                    SQLExecuteScalar(sqlstr, ref charID);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
                aChar.CharID = charID;
                SaveItem(aChar);
            }
        }

        private uint getCharID(string name)
        {
            string sqlstr;
            DataRowCollection result = null;
            sqlstr = "SELECT `char_id` FROM `char` WHERE name='" + name + "' LIMIT 1";
            try
            {
                result = SQLExecuteQuery(sqlstr);
            }
            catch (MySqlException ex)
            {
                Logger.ShowSQL(ex, null);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            return (uint)result[0]["charID"];

        }

        public void SaveChar(ActorPC aChar)
        {
            string sqlstr;
            if (aChar != null && this.isConnected() == true)
            {
                sqlstr = string.Format("UPDATE `char` SET `name`='{0}',`race`='{1}',`gender`='{2}',`hairStyle`='{3}',`hairColor`='{4}',`wig`='{5}'," +
                     "`face`='{6}',`job`='{7}',`mapID`='{8}',`lv`='{9}',`jlv1`='{10}',`jlv2x`='{11}',`jlv2t`='{12}',`questRemaining`='{13}',`slot`='{14}'" +
                     ",`x`='{16}',`y`='{17}',`dir`='{18}',`hp`='{19}',`max_hp`='{20}',`mp`='{21}'," +
                    "`max_mp`='{22}',`sp`='{23}',`max_sp`='{24}',`str`='{25}',`dex`='{26}',`intel`='{27}',`vit`='{28}',`agi`='{29}',`mag`='{30}',`gold`='{31}'" +
                    " WHERE char_id='{15}' LIMIT 1",
                     aChar.Name, (byte)aChar.Race, (byte)aChar.Gender, aChar.HairStyle, aChar.HairColor, aChar.Wig,
                    aChar.Face, (byte)aChar.Job, aChar.MapID, aChar.Level, aChar.JobLevel1, aChar.JobLevel2X, aChar.JobLevel2T,
                    aChar.QuestRemaining, aChar.Slot, aChar.CharID, Global.PosX16to8(aChar.X), Global.PosY16to8(aChar.Y), (byte)(aChar.Dir / 45), aChar.HP, aChar.MaxHP, aChar.MP,
                    aChar.MaxMP, aChar.SP, aChar.MaxSP, aChar.Str, aChar.Dex, aChar.Int, aChar.Vit, aChar.Agi, aChar.Mag, aChar.Gold);
                try
                {
                    SQLExecuteNonQuery(sqlstr);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
                SaveItem(aChar);
            }
        }

        public void DeleteChar(ActorPC aChar)
        {
            string sqlstr;
            sqlstr = "DELETE FROM `char` WHERE char_id='" + aChar.CharID + "';";
            sqlstr += "DELETE FROM `inventory` WHERE char_id='" + aChar.CharID + "';";
            
            try
            {
                SQLExecuteNonQuery(sqlstr);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        public ActorPC GetChar(uint charID)
        {
            string sqlstr;
            DataRow result = null;
            ActorPC pc;
            sqlstr = "SELECT * FROM `char` WHERE `char_id`='" + charID + "' LIMIT 1";
            try
            {
                result = SQLExecuteQuery(sqlstr)[0];
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                return null;
            }
            pc = new ActorPC();
            pc.CharID = charID;
            pc.Account = null;
            pc.Name = (string)result["name"];
            pc.Race = (PC_RACE)(byte)result["race"];
            pc.Gender = (PC_GENDER)(byte)result["gender"];
            pc.HairStyle = (byte)result["hairStyle"];
            pc.HairColor = (byte)result["hairColor"];
            pc.Wig = (byte)result["wig"];
            pc.Face = (byte)result["face"];
            pc.Job = (PC_JOB)(byte)result["job"];
            pc.MapID = (uint)result["mapID"];
            pc.Level = (byte)result["lv"];
            pc.JobLevel1 = (byte)result["jlv1"];
            pc.JobLevel2X = (byte)result["jlv2x"];
            pc.JobLevel2T = (byte)result["jlv2t"];
            pc.QuestRemaining = (ushort)result["questRemaining"];
            pc.Slot = (byte)result["slot"];            
            pc.X = Global.PosX8to16((byte)result["x"]);
            pc.Y = Global.PosY8to16((byte)result["y"]);
            pc.Dir = (ushort)((byte)result["dir"] * 45);
            pc.HP = (uint)result["hp"];
            pc.MP = (uint)result["mp"];
            pc.SP = (uint)result["sp"];
            pc.MaxHP = (uint)result["max_hp"];
            pc.MaxMP = (uint)result["max_mp"];
            pc.MaxSP = (uint)result["max_sp"];
            pc.Str = (ushort)result["str"];
            pc.Dex = (ushort)result["dex"];
            pc.Int = (ushort)result["intel"];
            pc.Vit = (ushort)result["vit"];
            pc.Agi = (ushort)result["agi"];
            pc.Mag = (ushort)result["mag"];
            pc.Gold = (uint)result["gold"];

            GetItem(pc);
            return pc;
        }

        private void SaveItem(ActorPC pc)
        {
            string[] names = Enum.GetNames(typeof(ContainerType));
            string sqlstr;
            sqlstr = "DELETE FROM `inventory` WHERE char_id='" + pc.CharID + "';";

            foreach (string i in names)
            {
                ContainerType container = (ContainerType)Enum.Parse(typeof(ContainerType), i);
                List<Item.Item> items = pc.Inventory.GetContainer(container);
                foreach (Item.Item j in items)
                {
                    sqlstr += string.Format("INSERT INTO `inventory`(`char_id`,`itemID`,`durability`,`identified`,`stack`,`container`) VALUES " +
                        "('{0}','{1}','{2}','{3}','{4}','{5}');", pc.CharID, j.ItemID, j.durability, j.identified, j.stack, (byte)container);
                }
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

        private void GetItem(ActorPC pc)
        {
            string sqlstr;
            DataRowCollection result = null;
            sqlstr = "SELECT * FROM `inventory` WHERE `char_id`='" + pc.CharID + "';";
            try
            {
                result = SQLExecuteQuery(sqlstr);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                return;
            }
            foreach (DataRow i in result)
            {
                Item.Item newItem = ItemFactory.Instance.GetItem((uint)i["itemID"]);
                newItem.durability = (ushort)i["durability"];
                newItem.stack = (byte)i["stack"];
                newItem.identified = (byte)i["identified"];
                ContainerType container = (ContainerType)(byte)i["container"];
                pc.Inventory.AddItem(container, newItem);
            }
        }

        public bool CharExists(string name)
        {
            string sqlstr;
            DataRowCollection result = null;
            sqlstr = "SELECT count(*) FROM `char` WHERE name='" + name + "'";
            try
            {
                result = SQLExecuteQuery(sqlstr);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            if (Convert.ToInt32(result[0][0]) > 0) return true;
            return false;
        }

        public uint GetAccountID(uint charID)
        {
            string sqlstr = "SELECT `account_id` FROM `char` WHERE `char_id`='`" + charID + "' LIMIT 1;";

            DataRowCollection result = SQLExecuteQuery(sqlstr);
            if (result.Count == 0)
                return 0;
            else
                return (uint)result[0]["account_id"];
        }

        public uint[] GetCharIDs(int account_id)
        {
            string sqlstr;
            uint[] buf;
            DataRowCollection result = null;
            sqlstr = "SELECT `char_id` FROM `char` WHERE account_id='" + account_id + "'";
            try
            {
                result = SQLExecuteQuery(sqlstr);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                return new uint[0];
            }
            if (result.Count == 0) return new uint[0];
            buf = new uint[result.Count];
            for (int i = 0; i < buf.Length; i++)
            {
                buf[i] = (uint)result[i]["char_id"];
            }
            return buf;
        }

        public string GetCharName(uint id)
        {
            string sqlstr = "SELECT `name` FROM `char` WHERE `char_id`='`" + id.ToString() + "' LIMIT 1;";

            DataRowCollection result = SQLExecuteQuery(sqlstr);
            if (result.Count == 0)
                return null;
            else
                return (string)result[0]["name"];

        }
    }
}
