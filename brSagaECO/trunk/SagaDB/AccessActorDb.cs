using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Security.Cryptography;

using SagaDB.Actor;
using SagaDB.Item;
using SagaDB.Quests;
using SagaLib;
//引入OLEDB
using System.Data.OleDb;

namespace SagaDB
{
    public class AccessActorDb : AccessConnectivity, ActorDB
    {
        private Encoding encoder = System.Text.Encoding.UTF8;
        private string Source;
        private DateTime tick = DateTime.Now;
        private bool isconnected;

        public AccessActorDb(string Source)
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
        public void AJIClear() { }
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
            if (db.State == ConnectionState.Open) { this.isconnected = true; return true; } else { this.isconnected = false; return false; }
        }

        public void CreateChar(ActorPC aChar, int account_id)
        {
            string sqlstr;
            uint charID = 0;
            if (aChar != null && this.isConnected() == true)
            {
                string name = aChar.Name;
                CheckSQLString(ref name);
                Map.MapInfo info = Map.MapInfoFactory.Instance.MapInfo[aChar.MapID];
                sqlstr = string.Format("INSERT INTO `char`(`account_id`,`name`,`race`,`gender`,`hairStyle`,`hairColor`,`wig`," +
                    "`face`,`job`,`mapID`,`lv`,`jlv1`,`jlv2x`,`jlv2t`,`questRemaining`,`slot`,`x`,`y`,`dir`,`hp`,`max_hp`,`mp`," +
                    "`max_mp`,`sp`,`max_sp`,`str`,`dex`,`intel`,`vit`,`agi`,`mag`,`statspoint`,`skillpoint`,`skillpoint2x`,`skillpoint2t`,`gold`," +
                    "`tailStyle`,`wingStyle`,`wingColor`" +
                    ") VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}'," +
                    "'{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}'" +
                    ",'{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}')",
                    account_id, name, (byte)aChar.Race, (byte)aChar.Gender, aChar.HairStyle, aChar.HairColor, aChar.Wig,
                    aChar.Face, (byte)aChar.Job, aChar.MapID, aChar.Level, aChar.JobLevel1, aChar.JobLevel2X, aChar.JobLevel2T,
                    aChar.QuestRemaining, aChar.Slot, Global.PosX16to8(aChar.X, info.width), Global.PosY16to8(aChar.Y, info.height), (byte)(aChar.Dir / 45), aChar.HP, aChar.MaxHP, aChar.MP,
                    aChar.MaxMP, aChar.SP, aChar.MaxSP, aChar.Str, aChar.Dex, aChar.Int, aChar.Vit, aChar.Agi, aChar.Mag, aChar.StatsPoint,
                    aChar.SkillPoint, aChar.SkillPoint2X, aChar.SkillPoint2T, aChar.Gold, aChar.TailStyle, aChar.WingStyle, aChar.WingColor);
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
            catch (OleDbException ex)
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
            SaveChar(aChar, true);
        }

        public void SaveChar(ActorPC aChar, bool fullinfo)
        {
            SaveChar(aChar, true, fullinfo);
        }
        public void SaveChar(ActorPC aChar, bool itemInfo, bool fullinfo)
        {
            string sqlstr;
            Map.MapInfo info = Map.MapInfoFactory.Instance.MapInfo[aChar.MapID];
            if (aChar != null && this.isConnected() == true)
            {
                uint questid = 0;
                uint partyid = 0;
                int count1 = 0, count2 = 0, count3 = 0;
                DateTime questtime = DateTime.Now;
                QuestStatus status = QuestStatus.OPEN;
                if (aChar.Quest != null)
                {
                    questid = aChar.Quest.ID;
                    count1 = aChar.Quest.CurrentCount1;
                    count2 = aChar.Quest.CurrentCount2;
                    count3 = aChar.Quest.CurrentCount3;
                    questtime = aChar.Quest.EndTime;
                    status = aChar.Quest.Status;
                }
                if (aChar.Party != null)
                    partyid = aChar.Party.ID;

                sqlstr = string.Format("UPDATE `char` SET `name`='{0}',`race`='{1}',`gender`='{2}',`hairStyle`='{3}',`hairColor`='{4}',`wig`='{5}'," +
                     "`face`='{6}',`job`='{7}',`mapID`='{8}',`lv`='{9}',`jlv1`='{10}',`jlv2x`='{11}',`jlv2t`='{12}',`questRemaining`='{13}',`slot`='{14}'" +
                     ",`x`='{16}',`y`='{17}',`dir`='{18}',`hp`='{19}',`max_hp`='{20}',`mp`='{21}'," +
                    "`max_mp`='{22}',`sp`='{23}',`max_sp`='{24}',`str`='{25}',`dex`='{26}',`intel`='{27}',`vit`='{28}',`agi`='{29}',`mag`='{30}'," +
                    "`statspoint`='{31}',`skillpoint`='{32}',`skillpoint2x`='{33}',`skillpoint2t`='{34}',`gold`='{35}',`cexp`='{36}',`jexp`='{37}'," +
                    "`save_map`='{38}',`save_x`='{39}',`save_y`='{40}',`possession_target`='{41}',`questid`='{42}',`questendtime`='{43}'" +
                    ",`queststatus`='{44}',`questcurrentcount1`='{45}',`questcurrentcount2`='{46}',`questcurrentcount3`='{47}'" +
                    ",`questresettime`='{48}',`fame`='{49}',`party`='{50}',`tailStyle`='{51}',`wingStyle`='{52}',`wingColor`='{53}' WHERE char_id='{15}' LIMIT 1",
                     aChar.Name, (byte)aChar.Race, (byte)aChar.Gender, aChar.HairStyle, aChar.HairColor, aChar.Wig,
                    aChar.Face, (byte)aChar.Job, aChar.MapID, aChar.Level, aChar.JobLevel1, aChar.JobLevel2X, aChar.JobLevel2T,
                    aChar.QuestRemaining, aChar.Slot, aChar.CharID, Global.PosX16to8(aChar.X, info.width), Global.PosY16to8(aChar.Y, info.height), (byte)(aChar.Dir / 45), aChar.HP, aChar.MaxHP, aChar.MP,
                    aChar.MaxMP, aChar.SP, aChar.MaxSP, aChar.Str, aChar.Dex, aChar.Int, aChar.Vit, aChar.Agi, aChar.Mag, aChar.StatsPoint,
                    aChar.SkillPoint, aChar.SkillPoint2X, aChar.SkillPoint2T, aChar.Gold, aChar.CEXP, aChar.JEXP, aChar.SaveMap, aChar.SaveX, aChar.SaveY,
                    aChar.PossessionTarget, questid, ToSQLDateTime(questtime), (byte)status, count1, count2, count3, ToSQLDateTime(aChar.QuestNextResetTime),
                    aChar.Fame, partyid, aChar.TailStyle, aChar.WingStyle, aChar.WingColor);
                try
                {
                    SQLExecuteNonQuery(sqlstr);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
                SaveItem(aChar);
                SaveSkill(aChar);
                SaveVar(aChar);
            }
        }

        public void DeleteChar(ActorPC aChar)
        {
            string sqlstr;
            uint account_id = (uint)SQLExecuteQuery("SELECT `account_id` FROM `char` WHERE `char_id`='" + aChar.CharID + "' LIMIT 1")[0]["account_id"];
            sqlstr = "DELETE FROM `char` WHERE char_id='" + aChar.CharID + "';";
            sqlstr += "DELETE FROM `inventory` WHERE char_id='" + aChar.CharID + "';";
            sqlstr += "DELETE FROM `skill` WHERE char_id='" + aChar.CharID + "';";
            sqlstr += "DELETE FROM `cVar` WHERE char_id='" + aChar.CharID + "';";
            sqlstr += "DELETE FROM `aVar` WHERE account_id='" + account_id + "';";
            sqlstr += "DELETE FROM `friend` WHERE `char_id`='" + aChar.CharID + "' OR `friend_char_id`='" + aChar.CharID + "';";

            try
            {
                SQLExecuteNonQuery(sqlstr);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        public ActorPC GetChar(uint charID, bool fullinfo)
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
            pc.TailStyle = (byte)result["tailStyle"];
            pc.WingStyle = (byte)result["wingStyle"];
            pc.WingColor = (byte)result["wingColor"];
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
            pc.QuestNextResetTime = (DateTime)result["questresettime"];
            pc.Fame = (uint)result["fame"];
            pc.Slot = (byte)result["slot"];
            if (fullinfo)
            {
                Map.MapInfo info = Map.MapInfoFactory.Instance.MapInfo[pc.MapID];

                pc.X = Global.PosX8to16((byte)result["x"], info.width);
                pc.Y = Global.PosY8to16((byte)result["y"], info.height);
            }
            pc.Dir = (ushort)((byte)result["dir"] * 45);

            pc.SaveMap = (uint)result["save_map"];
            pc.SaveX = (byte)result["save_x"];
            pc.SaveY = (byte)result["save_y"];
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
            pc.StatsPoint = (ushort)result["statspoint"];
            pc.SkillPoint = (ushort)result["skillpoint"];
            pc.SkillPoint2X = (ushort)result["skillpoint2x"];
            pc.SkillPoint2T = (ushort)result["skillpoint2t"];

            pc.Gold = (long)result["gold"];
            pc.CEXP = (uint)result["cexp"];
            pc.JEXP = (uint)result["jexp"];
            pc.PossessionTarget = (uint)result["possession_target"];
            Party.Party party = new SagaDB.Party.Party();
            party.ID = (uint)result["party"];
            if (party.ID != 0)
                pc.Party = party;

            if (fullinfo)
            {
                uint questid = (uint)result["questid"];
                if (questid != 0)
                {
                    Quest quest = new Quest(questid);
                    quest.EndTime = (DateTime)result["questendtime"];
                    quest.Status = (QuestStatus)(byte)result["queststatus"];
                    quest.CurrentCount1 = (int)result["questcurrentcount1"];
                    quest.CurrentCount2 = (int)result["questcurrentcount2"];
                    quest.CurrentCount3 = (int)result["questcurrentcount3"];
                    pc.Quest = quest;
                }
                GetSkill(pc);
                GetVar(pc);
            }
            GetItem(pc);
            return pc;
        }

        public ActorPC GetChar(uint charID)
        {
            return GetChar(charID, true);
        }

        public void SaveSkill(ActorPC pc)
        {
            string sqlstr;
            sqlstr = "DELETE FROM `skill` WHERE char_id='" + pc.CharID + "';";

            foreach (Skill.Skill i in pc.Skills.Values)
            {
                sqlstr += string.Format("INSERT INTO `skill`(`char_id`,`skill_id`,`lv`) VALUES " +
                    "('{0}','{1}','{2}');", pc.CharID, i.ID, i.Level);
            }
            foreach (Skill.Skill i in pc.Skills2.Values)
            {
                sqlstr += string.Format("INSERT INTO `skill`(`char_id`,`skill_id`,`lv`) VALUES " +
                    "('{0}','{1}','{2}');", pc.CharID, i.ID, i.Level);
            }
            foreach (Skill.Skill i in pc.SkillsReserve.Values)
            {
                sqlstr += string.Format("INSERT INTO `skill`(`char_id`,`skill_id`,`lv`) VALUES " +
                    "('{0}','{1}','{2}');", pc.CharID, i.ID, i.Level);
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

        private void SaveVar(ActorPC pc)
        {
            string sqlstr = "DELETE FROM `cVar` WHERE char_id='" + pc.CharID + "';";
            uint account_id = (uint)SQLExecuteQuery("SELECT `account_id` FROM `char` WHERE `char_id`='" + pc.CharID + "' LIMIT 1")[0]["account_id"];
            sqlstr += "DELETE FROM `aVar` WHERE account_id='" + account_id + "';";
            foreach (string i in pc.AStr.Keys)
            {
                sqlstr += string.Format("INSERT INTO `aVar`(`account_id`,`name`,`type`,`content`) VALUES " +
                    "('{0}','{1}',0,'{2}');", account_id, i, pc.AStr[i]);
            }
            foreach (string i in pc.AInt.Keys)
            {
                sqlstr += string.Format("INSERT INTO `aVar`(`account_id`,`name`,`type`,`content`) VALUES " +
                    "('{0}','{1}',1,'{2}');", account_id, i, pc.AInt[i]);
            }
            foreach (string i in pc.AMask.Keys)
            {
                sqlstr += string.Format("INSERT INTO `aVar`(`account_id`,`name`,`type`,`content`) VALUES " +
                    "('{0}','{1}',2,'{2}');", account_id, i, pc.AMask[i].Value);
            }
            foreach (string i in pc.CStr.Keys)
            {
                sqlstr += string.Format("INSERT INTO `cVar`(`char_id`,`name`,`type`,`content`) VALUES " +
                    "('{0}','{1}',0,'{2}');", pc.CharID, i, pc.CStr[i]);
            }
            foreach (string i in pc.CInt.Keys)
            {
                sqlstr += string.Format("INSERT INTO `cVar`(`char_id`,`name`,`type`,`content`) VALUES " +
                    "('{0}','{1}',1,'{2}');", pc.CharID, i, pc.CInt[i]);
            }
            foreach (string i in pc.CMask.Keys)
            {
                sqlstr += string.Format("INSERT INTO `cVar`(`char_id`,`name`,`type`,`content`) VALUES " +
                    "('{0}','{1}',2,'{2}');", pc.CharID, i, pc.CMask[i].Value);
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

        private void GetVar(ActorPC pc)
        {
            string sqlstr = "SELECT * FROM `cVar` WHERE `char_id`='" + pc.CharID + "';";
            uint account_id = (uint)SQLExecuteQuery("SELECT `account_id` FROM `char` WHERE `char_id`='" + pc.CharID + "' LIMIT 1")[0]["account_id"];
            DataRowCollection res;
            res = SQLExecuteQuery(sqlstr);
            foreach (DataRow i in res)
            {
                byte type = (byte)i["type"];
                switch (type)
                {
                    case 0:
                        pc.CStr[(string)i["name"]] = (string)i["content"];
                        break;
                    case 1:
                        pc.CInt[(string)i["name"]] = int.Parse((string)i["content"]);
                        break;
                    case 2:
                        pc.CMask[(string)i["name"]] = new BitMask(int.Parse((string)i["content"]));
                        break;
                }
            }
            sqlstr = "SELECT * FROM `aVar` WHERE `account_id`='" + account_id + "';";
            res = SQLExecuteQuery(sqlstr);
            foreach (DataRow i in res)
            {
                byte type = (byte)i["type"];
                switch (type)
                {
                    case 0:
                        pc.AStr[(string)i["name"]] = (string)i["content"];
                        break;
                    case 1:
                        pc.AInt[(string)i["name"]] = int.Parse((string)i["content"]);
                        break;
                    case 2:
                        pc.AMask[(string)i["name"]] = new BitMask(int.Parse((string)i["content"]));
                        break;

                }
            }
        }

        public void NewItem(ActorPC pc, Item.Item item, ContainerType container)
        {

        }

        public void DeleteItem(ActorPC pc, Item.Item item)
        {
            string sqlstr = string.Format("DELETE FROM `inventory` WHERE `char_id`='{0}' AND `id`='{01}';", pc.CharID, item.DBID);
            SQLExecuteNonQuery(sqlstr);
        }

        public void NewWarehouseItem(ActorPC pc, Item.Item item, WarehousePlace container)
        {

        }

        public void DeleteWarehouseItem(ActorPC pc, Item.Item item)
        {
            uint account = GetAccountID(pc);
            string sqlstr = string.Format("DELETE FROM `inventory` WHERE `account_id`='{0}' AND `id`='{01}';", account, item.DBID);
            SQLExecuteNonQuery(sqlstr);
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
                        "('{0}','{1}','{2}','{3}','{4}','{5}');", pc.CharID, j.ItemID, j.Durability, j.identified, j.Stack, (byte)container);
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



        public void GetSkill(ActorPC pc)
        {
            string sqlstr;
            DataRowCollection result = null;
            sqlstr = "SELECT * FROM `skill` WHERE `char_id`='" + pc.CharID + "';";
            try
            {
                result = SQLExecuteQuery(sqlstr);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                return;
            }
            //Dictionary<uint, byte> skills = Skill.SkillFactory.Instance.CheckSkillList(pc, Skill.SkillFactory.SkillPaga.p1);
            Skill.SkillFactory.Instance.SkillList(pc.JobBasic);
            //Dictionary<uint, byte> skills2x = Skill.SkillFactory.Instance.CheckSkillList(pc, Skill.SkillFactory.SkillPaga.p21);
            Skill.SkillFactory.Instance.SkillList(pc.Job2X);
            //Dictionary<uint, byte> skills2t = Skill.SkillFactory.Instance.CheckSkillList(pc, Skill.SkillFactory.SkillPaga.p22);
            Skill.SkillFactory.Instance.SkillList(pc.Job2T);
            //foreach (DataRow i in result)
            //{
            //    Skill.Skill skill = Skill.SkillFactory.Instance.GetSkill((uint)i["skill_id"], (byte)i["lv"]);
            //    if (skills.ContainsKey(skill.ID))
            //        pc.Skills.Add(skill.ID, skill);
            //    else if (skills2x.ContainsKey(skill.ID))
            //    {
            //        if (pc.Job == pc.Job2X)
            //            pc.Skills2.Add(skill.ID, skill);
            //        else
            //            pc.SkillsReserve.Add(skill.ID, skill);
            //    }
            //    else if (skills2t.ContainsKey(skill.ID))
            //    {
            //        if (pc.Job == pc.Job2T)
            //            pc.Skills2.Add(skill.ID, skill);
            //        else
            //            pc.SkillsReserve.Add(skill.ID, skill);
            //    }
            //    else
            //        pc.Skills.Add(skill.ID, skill);
            //}
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
                newItem.Durability = (ushort)i["durability"];
                newItem.Stack = (ushort)i["stack"];
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

        public uint GetAccountID(ActorPC pc)
        {
            return GetAccountID(pc.CharID);
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
            string sqlstr = "SELECT `name` FROM `char` WHERE `char_id`='" + id.ToString() + "' LIMIT 1;";

            DataRowCollection result = SQLExecuteQuery(sqlstr);
            if (result.Count == 0)
                return null;
            else
                return (string)result[0]["name"];

        }

        public List<ActorPC> GetFriendList(ActorPC pc)
        {
            string sqlstr = "SELECT `friend_char_id` FROM `friend` WHERE `char_id`='" + pc.CharID + "';";
            DataRowCollection result = SQLExecuteQuery(sqlstr);
            List<ActorPC> list = new List<ActorPC>();
            for (int i = 0; i < result.Count; i++)
            {
                uint friend = (uint)result[i]["friend_char_id"];
                ActorPC chara = new ActorPC();
                chara.CharID = friend;
                sqlstr = "SELECT `name`,`job`,`lv`,`jlv1`,`jlv2x`,`jlv2t` FROM `char` WHERE `char_id`='" + friend + "' LIMIT 1;";
                DataRowCollection res = SQLExecuteQuery(sqlstr);
                if (res.Count == 0)
                    continue;
                DataRow row = res[0];
                chara.Name = (string)row["name"];
                chara.Job = (PC_JOB)(byte)row["job"];
                chara.Level = (byte)row["lv"];
                chara.JobLevel1 = (byte)row["jlv1"];
                chara.JobLevel2X = (byte)row["jlv2x"];
                chara.JobLevel2T = (byte)row["jlv2t"];
                list.Add(chara);
            }
            return list;
        }

        public List<ActorPC> GetFriendList2(ActorPC pc)
        {
            string sqlstr = "SELECT `char_id` FROM `friend` WHERE `friend_char_id`='" + pc.CharID + "';";
            DataRowCollection result = SQLExecuteQuery(sqlstr);
            List<ActorPC> list = new List<ActorPC>();
            for (int i = 0; i < result.Count; i++)
            {
                uint friend = (uint)result[i]["char_id"];
                ActorPC chara = new ActorPC();
                chara.CharID = friend;
                sqlstr = "SELECT `name`,`job`,`lv`,`jlv1`,`jlv2x`,`jlv2t` FROM `char` WHERE `char_id`='" + friend + "' LIMIT 1;";
                DataRowCollection res = SQLExecuteQuery(sqlstr);
                if (res.Count == 0)
                    continue;
                DataRow row = res[0];
                chara.Name = (string)row["name"];
                chara.Job = (PC_JOB)(byte)row["job"];
                chara.Level = (byte)row["lv"];
                chara.JobLevel1 = (byte)row["jlv1"];
                chara.JobLevel2X = (byte)row["jlv2x"];
                chara.JobLevel2T = (byte)row["jlv2t"];
                list.Add(chara);
            }
            return list;
        }

        public void AddFriend(ActorPC pc, uint charID)
        {
            string sqlstr = string.Format("INSERT INTO `friend`(`char_id`,`friend_char_id`) VALUES " +
                    "('{0}','{1}');", pc.CharID, charID);
            SQLExecuteNonQuery(sqlstr);
        }

        public bool IsFriend(uint char1, uint char2)
        {
            string sqlstr = "SELECT `char_id` FROM `friend` WHERE `friend_char_id`='" + char2 + "' AND `char_id`='" + char1 + "';";
            DataRowCollection result = SQLExecuteQuery(sqlstr);
            return result.Count > 0;
        }

        public void DeleteFriend(uint char1, uint char2)
        {
            string sqlstr = "DELETE FROM `friend` WHERE `friend_char_id`='" + char2 + "' AND `char_id`='" + char1 + "';";
            SQLExecuteNonQuery(sqlstr);
        }

        public Party.Party GetParty(uint id)
        {
            string sqlstr = "SELECT * FROM `party` WHERE `party_id`='" + id + "' LIMIT 1;";
            DataRowCollection result = SQLExecuteQuery(sqlstr);

            Party.Party party = new SagaDB.Party.Party();
            if (result.Count != 0)
            {
                party.ID = id;
                uint leader = (uint)result[0]["leader"];
                party.Name = (string)result[0]["name"];
                for (byte i = 1; i <= 8; i++)
                {
                    uint member = (uint)result[0]["member" + i.ToString()];
                    if (member != 0)
                    {
                        ActorPC pc = new ActorPC();
                        pc.CharID = member;
                        sqlstr = "SELECT `name`,`job` FROM `char` WHERE `char_id`='" + member + "' LIMIT 1;";
                        DataRowCollection rows = SQLExecuteQuery(sqlstr);
                        if (rows.Count > 0)
                        {
                            DataRow row = rows[0];
                            pc.Name = (string)row["name"];
                            pc.Job = (PC_JOB)(byte)row["job"];
                            party.Members.Add((byte)(i - 1), pc);
                            if (leader == member)
                                party.Leader = pc;
                        }
                    }
                }
                return party;
            }
            return null;
        }

        public void NewParty(Party.Party party)
        {
            uint index = 0;
            string sqlstr = string.Format("INSERT INTO `party`(`leader`,`name`,`member1`,`member2`,`member3`,`member4`,`member5`,`member6`,`member7`,`member8`) VALUES " +
                    "('0','{0}','0','0','0','0','0','0','0','0');", party.Name);
            SQLExecuteScalar(sqlstr, ref index);
            party.ID = index;
        }

        public void SaveParty(Party.Party party)
        {
            uint leader = party.Leader.CharID;
            uint member1, member2, member3, member4, member5, member6, member7, member8;
            if (party.Members.ContainsKey(0))
                member1 = party[0].CharID;
            else
                member1 = 0;
            if (party.Members.ContainsKey(1))
                member2 = party[1].CharID;
            else
                member2 = 0;
            if (party.Members.ContainsKey(2))
                member3 = party[2].CharID;
            else
                member3 = 0;
            if (party.Members.ContainsKey(3))
                member4 = party[3].CharID;
            else
                member4 = 0;
            if (party.Members.ContainsKey(4))
                member5 = party[4].CharID;
            else
                member5 = 0;
            if (party.Members.ContainsKey(5))
                member6 = party[5].CharID;
            else
                member6 = 0;
            if (party.Members.ContainsKey(6))
                member7 = party[6].CharID;
            else
                member7 = 0;
            if (party.Members.ContainsKey(7))
                member8 = party[7].CharID;
            else
                member8 = 0;
            string sqlstr = string.Format("UPDATE `party` SET `leader`='{0}',`member1`='{1}',`member2`='{2}',`member3`='{3}',`member4`='{4}'" +
                ",`member5`='{5}',`member6`='{6}',`member7`='{7}',`member8`='{8}',`name`='{10}' WHERE `party_id`='{9}' LIMIT 1;",
                leader, member1, member2, member3, member4, member5, member6, member7, member8, party.ID, party.Name);
            SQLExecuteNonQuery(sqlstr);
        }

        public void DeleteParty(Party.Party party)
        {
            string sqlstr = string.Format("DELETE FROM `party` WHERE `party_id`='{0}';", party.ID);
            SQLExecuteNonQuery(sqlstr);
        }

        public Ring.Ring GetRing(uint id)
        {
            return null;
        }

        public void NewRing(Ring.Ring ring)
        {

        }

        public void SaveRing(Ring.Ring ring, bool saveMembers)
        {

        }

        public void DeleteRing(Ring.Ring ring)
        {

        }

        public List<BBS.Post> GetBBSPage(uint bbsID, int page)
        {
            return new List<SagaDB.BBS.Post>();
        }

        //public List<BBS.Mail> GetMail(ActorPC pc)
        //{ return null; }

        public bool BBSNewPost(ActorPC poster, uint bbsID, string title, string content)
        {
            return false;
        }

        public void RingEmblemUpdate(Ring.Ring ring, byte[] buf)
        {

        }

        public byte[] GetRingEmblem(uint ring_id, DateTime date, out bool needUpdate, out DateTime newTime)
        {
            needUpdate = false;
            newTime = DateTime.Now;
            return null;
        }

        public ActorPC LoadServerVar()
        {
            return null;
        }

        public void SaveServerVar(ActorPC fakepc)
        {
        }
        public List<SagaDB.FFarden.FFarden> GetFFList()
        {
            string sqlstr = string.Format("SELECT * FROM `ff`;");
            List<SagaDB.FFarden.FFarden> list = new List<SagaDB.FFarden.FFarden>();

            return list;
        }
        public void SaveFF(Ring.Ring ring)
        {
        }
        public void SaveSerFF(Server.Server ser)
        {
        }
        public void GetSerFFurniture(Server.Server ser)
        {
        }
        public void SaveFFCopy(Dictionary<SagaDB.FFarden.FurniturePlace, List<ActorFurniture>> Furnitures)
        {
        }
        public void CreateFF(ActorPC pc)
        {
        }
        public void GetFF(ActorPC pc)
        {
        }
        public uint GetFFRindID(uint ffid)
        {
            return 0;
        }
        public void GetFFurniture(SagaDB.Ring.Ring ring)
        {
        }
        public void GetFFurnitureCopy(Dictionary<SagaDB.FFarden.FurniturePlace, List<ActorFurniture>> Furnitures)
        {
        }
        public void GetLevelLimit()
        {
        }
        public void SavaLevelLimit()
        {
        }
        #region ActorDB Members


        public void GetVShop(ActorPC pc)
        {
            throw new NotImplementedException();
        }

        public void SaveVShop(ActorPC pc)
        {
            throw new NotImplementedException();
        }
        public void SaveWRP(ActorPC pc)
        {

        }

        public List<ActorPC> GetWRPRanking()
        {
            return null;
        }
        #endregion


        public List<BBS.Mail> GetMail(ActorPC pc)
        {
            throw new NotImplementedException();
        }
    }
}
