using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Runtime.Serialization.Formatters.Binary;

using ICSharpCode.SharpZipLib.BZip2;

using SagaDB.Actor;
using SagaDB.Partner;
using SagaDB.Item;
using SagaDB.Quests;
using SagaLib;
using MySql.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using SagaDB.Mob;

namespace SagaDB
{
    public class MySQLActorDB : MySQLConnectivity, ActorDB
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
            : base()
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
            if (db != null) { if (db.State != ConnectionState.Closed) this.isconnected = true; else { Console.WriteLine("SQL Connection error"); } }
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
                if (db != null) { if (db.State != ConnectionState.Closed) return true; else return false; }
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
                        tmp = new MySqlConnection(string.Format("Server={1};Port={2};Uid={3};Pwd={4};Database={0};Charset=utf8;", database, host, port, dbuser, dbpass));
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

        public void AJIClear()
        {
            string sqlstr = "UPDATE `char` SET `lv` = 1, `cexp` = 0;";
            try
            {
                SQLExecuteNonQuery(sqlstr);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        public void CreateChar(ActorPC aChar, int account_id)
        {
            string sqlstr;
            uint charID = 0;
            if (aChar != null && this.isConnected() == true)
            {
                string name = aChar.Name;
                CheckSQLString(ref name);
                //Map.MapInfo info = Map.MapInfoFactory.Instance.MapInfo[aChar.MapID];
                sqlstr = string.Format("INSERT INTO `char`(`account_id`,`name`,`race`,`gender`,`hairStyle`,`hairColor`,`wig`," +
                    "`face`,`job`,`mapID`,`lv`,`jlv1`,`jlv2x`,`jlv2t`,`questRemaining`,`slot`,`x`,`y`,`dir`,`hp`,`max_hp`,`mp`," +
                    "`max_mp`,`sp`,`max_sp`,`str`,`dex`,`intel`,`vit`,`agi`,`mag`,`statspoint`,`skillpoint`,`skillpoint2x`,`skillpoint2t`,`gold`," +
                    "`ep`,`eplogindate` ,`tailStyle` ,`wingStyle` ,`wingColor` ,`lv1` ,`jlv3`,`skillpoint3`,`explorerEXP`) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}'," +
                    "'{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}'" +
                    ",'{24}','{25}','{26}','{27}','{28}','{29}','{30}','{31}','{32}','{33}','{34}','{35}','{36}','{37}','{38}','{39}','{40}','{41}','{42}','{43}','{44}');",
                    account_id, name, (byte)aChar.Race, (byte)aChar.Gender, aChar.HairStyle, aChar.HairColor, aChar.Wig,
                    aChar.Face, (byte)aChar.Job, aChar.MapID, aChar.Level, aChar.JobLevel1, aChar.JobLevel2X, aChar.JobLevel2T,
                    aChar.QuestRemaining, aChar.Slot, aChar.X2, aChar.Y2, (byte)(aChar.Dir / 45), aChar.HP, aChar.MaxHP, aChar.MP,
                    aChar.MaxMP, aChar.SP, aChar.MaxSP, aChar.Str, aChar.Dex, aChar.Int, aChar.Vit, aChar.Agi, aChar.Mag, aChar.StatsPoint,
                    aChar.SkillPoint, aChar.SkillPoint2X, aChar.SkillPoint2T, aChar.Gold, aChar.EP, ToSQLDateTime(aChar.EPLoginTime), aChar.TailStyle, aChar.WingStyle, aChar.WingColor,
                    aChar.Level1, aChar.JobLevel3, aChar.SkillPoint3, aChar.ExplorerEXP);

                try
                {
                    SQLExecuteScalar(sqlstr, out charID);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
                aChar.CharID = charID;
                MySqlCommand cmd = new MySqlCommand(string.Format("INSERT INTO `inventory`(`char_id`,`data`) VALUES ('{0}',?data);\r\n", aChar.CharID));
                cmd.Parameters.Add("?data", MySqlDbType.Blob).Value = aChar.Inventory.ToBytes();

                try
                {
                    SQLExecuteNonQuery(cmd);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }

                if (aChar.Inventory.WareHouse != null)
                {
                    DataRowCollection result = null;
                    sqlstr = "SELECT count(*) FROM `warehouse` WHERE `account_id`='" + account_id + "' LIMIT 1;";
                    try
                    {
                        result = SQLExecuteQuery(sqlstr);
                    }
                    catch (Exception ex)
                    {
                        Logger.ShowError(ex);
                    }
                    if (Convert.ToInt32(result[0][0]) == 0)
                    {
                        cmd = new MySqlCommand(string.Format("INSERT INTO `warehouse`(`account_id`,`data`) VALUES ('{0}',?data);\r\n", account_id));
                        cmd.Parameters.Add("?data", MySqlDbType.Blob).Value = aChar.Inventory.WareToBytes();

                        try
                        {
                            SQLExecuteNonQuery(cmd);
                        }
                        catch (Exception ex)
                        {
                            Logger.ShowError(ex);
                        }
                    }
                }
                aChar.Inventory.WareHouse = null;
                //to avoid deleting items from warehouse
                SaveItem(aChar);
            }
        }

        public uint CreatePartner(Item.Item partnerItem)
        {
            ActorPartner ap = new ActorPartner(partnerItem.BaseData.petID, partnerItem);
            ap.HP = ap.BaseData.hp_in;
            ap.MaxHP = ap.BaseData.hp_in;
            ap.MP = ap.BaseData.mp_in;
            ap.MaxMP = ap.BaseData.mp_in;
            ap.SP = ap.BaseData.sp_in;
            ap.MaxSP = ap.BaseData.sp_in;
            uint apid = 0;
            //tbc
            string sqlstr;
            if (ap != null && this.isConnected() == true)
            {
                string name = ap.BaseData.name;
                CheckSQLString(ref name);
                sqlstr = string.Format("INSERT INTO `partner`(`pid`,`name`,`lv`,`tlv`,`rb`,`rank`,`perkspoints`,`perk0`,`perk1`,`perk2`," +
                   " `perk3`,`perk4`,`perk5`,`aimode`,`basicai1`,`basicai2`,`hp`,`maxhp`,`mp`,`maxmp`,`sp`,`maxsp`)" +
                    "VALUES ('{0}','{1}','{2}','{3}','0','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}','{16}'," +
                    "'{17}','{18}','{19}','{20}','{21}');",
                     ap.partnerid, ap.Name, ap.Level, ap.reliability, ap.rebirth, ap.rank, ap.perkpoint, ap.perk0,
                     ap.perk1, ap.perk2, ap.perk3, ap.perk4, ap.perk5, ap.ai_mode, ap.basic_ai_mode, ap.basic_ai_mode_2,
                     ap.HP, ap.MaxHP, ap.MP, ap.MaxMP, ap.SP, ap.MaxSP);
                try
                {
                    SQLExecuteScalar(sqlstr, out apid);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
                ap.ActorPartnerID = apid;
            }
            return apid;
        }

        public void SavePartner(ActorPartner ap)
        {
            string sqlstr;
            if (ap != null)
            {
                uint apid = ap.ActorPartnerID;
                byte rb = 0;
                if (ap.rebirth) rb = 1;
                sqlstr = string.Format("UPDATE `partner` SET `pid`='{1}',`name`='{2}',`lv`='{3}',`tlv`='{4}',`rb`='{5}',`rank`='{6}',`perkspoints`='{7}'," +
                    "`hp`='{8}',`maxhp`='{9}',`mp`='{10}',`maxmp`='{11}',`sp`='{12}',`maxsp`='{13}',`perk0`='{14}',`perk1`='{15}',`perk2`='{16}',`perk3`='{17}'" +
                    ",`perk4`='{18}',`perk5`='{19}',`aimode`='{20}',`basicai1`='{21}',`basicai2`='{22}',`exp` = '{23}',`pictid` = '{24}',`nextfeedtime` = '{25}'" +
                 ", `reliabilityuprate`='{26}',`texp`='{27}' WHERE apid='{0}' LIMIT 1",
                    apid, ap.partnerid, ap.Name, ap.Level, ap.reliability, rb, ap.rank, ap.perkpoint, ap.HP, ap.MaxHP, ap.MP, ap.MaxMP, ap.SP, ap.MaxSP,
                    ap.perk0, ap.perk1, ap.perk2, ap.perk3, ap.perk4, ap.perk5, ap.ai_mode, ap.basic_ai_mode, ap.basic_ai_mode_2, ap.exp, ap.PictID, ToSQLDateTime(ap.nextfeedtime), ap.reliabilityuprate, ap.reliabilityexp);
                //SagaLib.Logger.ShowError(sqlstr);
                try
                {
                    SQLExecuteNonQuery(sqlstr);
                    /*SavePartnerEquip(ap);
                    SavePartnerCube(ap);
                    SavePartnerAI(ap);*/
                    //暂时注释防止卡死
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
        }
        public void SavePartnerEquip(ActorPartner ap)
        {
            string sqlstr;
            if (ap != null)
            {
                uint apid = ap.ActorPartnerID;
                sqlstr = string.Format("DELETE FROM `partnerequip` WHERE `apid`='{0}';", apid);
                if (ap.equipments.ContainsKey(EnumPartnerEquipSlot.COSTUME))
                {
                    sqlstr += string.Format("INSERT INTO `partnerequip`(`apid`,`type`,`item_id`,`count`) VALUES ('{0}','1','{1}','{2}');",
                          apid, ap.equipments[Partner.EnumPartnerEquipSlot.COSTUME].ItemID, ap.equipments[Partner.EnumPartnerEquipSlot.COSTUME].Stack);
                }
                if (ap.equipments.ContainsKey(EnumPartnerEquipSlot.WEAPON))
                {
                    sqlstr += string.Format("INSERT INTO `partnerequip`(`apid`,`type`,`item_id`,`count`) VALUES ('{0}','2','{1}','{2}');",
                          apid, ap.equipments[Partner.EnumPartnerEquipSlot.WEAPON].ItemID, ap.equipments[Partner.EnumPartnerEquipSlot.WEAPON].Stack);
                }
                if (ap.foods.Count > 0)
                {
                    for (int i = 0; i < ap.foods.Count; i++)
                    {
                        sqlstr += string.Format("INSERT INTO `partnerequip`(`apid`,`type`,`item_id`,`count`) VALUES ('{0}','3','{1}','{2}');",
                          apid, ap.foods[i].ItemID, ap.foods[i].Stack);
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
        }
        public void SavePartnerCube(ActorPartner ap)
        {
            string sqlstr;
            if (ap != null)
            {
                uint apid = ap.ActorPartnerID;
                sqlstr = string.Format("DELETE FROM `partnercube` WHERE `apid`='{0}';", apid);
                if (ap.equipcubes_condition.Count > 0)
                {
                    for (int i = 0; i < ap.equipcubes_condition.Count; i++)
                    {
                        sqlstr += string.Format("INSERT INTO `partnercube`(`apid`,`type`,`unique_id`) VALUES ('{0}','1','{1}');",
                        apid, ap.equipcubes_condition[i]);
                    }
                }
                if (ap.equipcubes_action.Count > 0)
                {
                    for (int i = 0; i < ap.equipcubes_action.Count; i++)
                    {
                        sqlstr += string.Format("INSERT INTO `partnercube`(`apid`,`type`,`unique_id`) VALUES ('{0}','2','{1}');",
                        apid, ap.equipcubes_action[i]);
                    }
                }
                if (ap.equipcubes_activeskill.Count > 0)
                {
                    for (int i = 0; i < ap.equipcubes_activeskill.Count; i++)
                    {
                        sqlstr += string.Format("INSERT INTO `partnercube`(`apid`,`type`,`unique_id`) VALUES ('{0}','3','{1}');",
                        apid, ap.equipcubes_activeskill[i]);
                    }
                }
                if (ap.equipcubes_passiveskill.Count > 0)
                {
                    for (int i = 0; i < ap.equipcubes_passiveskill.Count; i++)
                    {
                        sqlstr += string.Format("INSERT INTO `partnercube`(`apid`,`type`,`unique_id`) VALUES ('{0}','4','{1}');",
                        apid, ap.equipcubes_passiveskill[i]);
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
        }
        public void SavePartnerAI(ActorPartner ap)
        {
            string sqlstr;
            if (ap != null)
            {
                uint apid = ap.ActorPartnerID;
                sqlstr = string.Format("DELETE FROM `partnerai` WHERE `apid`='{0}';", apid);
                if (ap.ai_conditions.Count > 0)
                {
                    foreach (var item in ap.ai_conditions)
                    {
                        sqlstr += string.Format("INSERT INTO `partnerai`(`apid`,`type`,`index`,`value`) VALUES ('{0}','1','{1}','{2}');",
                        apid, item.Key, item.Value);
                    }
                }
                if (ap.ai_reactions.Count > 0)
                {
                    foreach (var item in ap.ai_reactions)
                    {
                        sqlstr += string.Format("INSERT INTO `partnerai`(`apid`,`type`,`index`,`value`) VALUES ('{0}','2','{1}','{2}');",
                        apid, item.Key, item.Value);
                    }
                }
                if (ap.ai_intervals.Count > 0)
                {
                    foreach (var item in ap.ai_intervals)
                    {
                        sqlstr += string.Format("INSERT INTO `partnerai`(`apid`,`type`,`index`,`value`) VALUES ('{0}','3','{1}','{2}');",
                        apid, item.Key, item.Value);
                    }
                }
                if (ap.ai_states.Count > 0)
                {
                    foreach (var item in ap.ai_states)
                    {
                        sqlstr += string.Format("INSERT INTO `partnerai`(`apid`,`type`,`index`,`value`) VALUES ('{0}','4','{1}','{2}');",
                        apid, item.Key, Convert.ToUInt16(item.Value));
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

        }
        public ActorPartner GetActorPartner(uint ActorPartnerID, Item.Item partneritem)
        {
            string sqlstr = string.Format("SELECT * FROM `partner` WHERE `apid`='{0}' LIMIT 1;", ActorPartnerID);
            DataRowCollection result = SQLExecuteQuery(sqlstr);
            if (result.Count > 0)
            {
                uint partnerid = (uint)result[0]["pid"];
                ActorPartner ap = new ActorPartner(partnerid, partneritem);
                ap.ActorPartnerID = ActorPartnerID;
                ap.Name = (string)result[0]["name"];
                ap.Level = (byte)result[0]["lv"];
                ap.reliability = (byte)result[0]["tlv"];
                ap.reliabilityexp = (ulong)result[0]["texp"];
                if (((byte)result[0]["rb"]) == 0)
                    ap.rebirth = false;
                else
                    ap.rebirth = true;
                ap.rank = (byte)result[0]["rank"];
                ap.perkpoint = (ushort)result[0]["perkspoints"];
                ap.HP = (uint)result[0]["hp"];
                ap.MaxHP = (uint)result[0]["maxhp"];
                ap.MP = (uint)result[0]["mp"];
                ap.MaxMP = (uint)result[0]["maxmp"];
                ap.SP = (uint)result[0]["sp"];
                ap.MaxSP = (uint)result[0]["maxsp"];
                ap.perk0 = (byte)result[0]["perk0"];
                ap.perk1 = (byte)result[0]["perk1"];
                ap.perk2 = (byte)result[0]["perk2"];
                ap.perk3 = (byte)result[0]["perk3"];
                ap.perk4 = (byte)result[0]["perk4"];
                ap.perk5 = (byte)result[0]["perk5"];
                ap.ai_mode = (byte)result[0]["aimode"];
                ap.basic_ai_mode = (byte)result[0]["basicai1"];
                ap.basic_ai_mode_2 = (byte)result[0]["basicai2"];
                ap.exp = (ulong)result[0]["exp"];
                ap.nextfeedtime = (DateTime)result[0]["nextfeedtime"];
                ap.reliabilityuprate = (ushort)result[0]["reliabilityuprate"];

                GetPartnerEquip(ap);
                GetPartnerCube(ap);
                GetPartnerAI(ap);
                return ap;
                //暂时注释防止卡死
            }
            return null;
        }
        public void GetPartnerEquip(ActorPartner ap)
        {
            string sqlstr = string.Format("SELECT * FROM `partnerequip` WHERE `apid`='{0}';", ap.ActorPartnerID);
            DataRowCollection result = SQLExecuteQuery(sqlstr);
            if (result.Count > 0)
            {
                foreach (DataRow i in result)
                {
                    Item.Item item = SagaDB.Item.ItemFactory.Instance.GetItem((uint)i["item_id"]);
                    item.Stack = (ushort)i["count"];
                    if (byte.Parse(i["type"].ToString()) == 0x1)
                        ap.equipments[Partner.EnumPartnerEquipSlot.COSTUME] = item;
                    if (byte.Parse(i["type"].ToString()) == 0x2)
                        ap.equipments[Partner.EnumPartnerEquipSlot.WEAPON] = item;
                    if (byte.Parse(i["type"].ToString()) == 0x3)
                        ap.foods.Add(item);
                }
            }
        }
        public void GetPartnerCube(ActorPartner ap)
        {
            string sqlstr = string.Format("SELECT * FROM `partnercube` WHERE `apid`='{0}';", ap.ActorPartnerID);
            DataRowCollection result = SQLExecuteQuery(sqlstr);
            if (result.Count > 0)
            {
                foreach (DataRow i in result)
                {
                    if ((byte)i["type"] == 1)
                        ap.equipcubes_condition.Add((ushort)i["unique_id"]);
                    if ((byte)i["type"] == 2)
                        ap.equipcubes_action.Add((ushort)i["unique_id"]);
                    if ((byte)i["type"] == 3)
                        ap.equipcubes_activeskill.Add((ushort)i["unique_id"]);
                    if ((byte)i["type"] == 4)
                        ap.equipcubes_passiveskill.Add((ushort)i["unique_id"]);
                }
            }
        }
        public void GetPartnerAI(ActorPartner ap)
        {
            string sqlstr = string.Format("SELECT * FROM `partnerai` WHERE `apid`='{0}';", ap.ActorPartnerID);
            DataRowCollection result = SQLExecuteQuery(sqlstr);
            if (result.Count > 0)
            {
                foreach (DataRow i in result)
                {
                    if ((byte)i["type"] == 1)
                    {
                        ap.ai_conditions.Add((byte)i["index"], (ushort)i["value"]);
                    }
                    if ((byte)i["type"] == 2)
                    {
                        ap.ai_reactions.Add((byte)i["index"], (ushort)i["value"]);
                    }
                    if ((byte)i["type"] == 3)
                    {
                        ap.ai_intervals.Add((byte)i["index"], (ushort)i["value"]);
                    }
                    if ((byte)i["type"] == 4)
                    {
                        ap.ai_states.Add((byte)i["index"], Convert.ToBoolean((ushort)i["value"]));
                    }
                }
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
            SaveChar(aChar, true);
        }

        public void SaveChar(ActorPC aChar, bool fullinfo)
        {
            SaveChar(aChar, true, fullinfo);
        }

        public void SaveChar(ActorPC aChar, bool itemInfo, bool fullinfo)
        {
            string sqlstr;
            Map.MapInfo info = null;
            if (Map.MapInfoFactory.Instance.MapInfo.ContainsKey(aChar.MapID))
                info = Map.MapInfoFactory.Instance.MapInfo[aChar.MapID];
            else
            {
                if (Map.MapInfoFactory.Instance.MapInfo.ContainsKey((uint)(aChar.MapID / 1000 * 1000)))
                    info = Map.MapInfoFactory.Instance.MapInfo[(uint)(aChar.MapID / 1000 * 1000)];
            }
            if (aChar != null)
            {
                uint questid = 0;
                uint partyid = 0;
                uint ringid = 0;
                uint golemid = 0;
                uint mapid = 0;
                byte x = 0, y = 0;
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
                if (aChar.Ring != null)
                    ringid = aChar.Ring.ID;
                if (aChar.Golem != null)
                    golemid = aChar.Golem.ActorID;
                if (info != null)
                {
                    mapid = aChar.MapID;
                    x = Global.PosX16to8(aChar.X, info.width);
                    y = Global.PosY16to8(aChar.Y, info.height);
                }
                else
                {
                    mapid = aChar.SaveMap;
                    x = aChar.SaveX;
                    y = aChar.SaveY;
                }
                bool online = aChar.Online;
                aChar.Online = false;
                sqlstr = string.Format("UPDATE `char` SET `name`='{0}',`race`='{1}',`gender`='{2}',`hairStyle`='{3}',`hairColor`='{4}',`wig`='{5}'," +
                     "`face`='{6}',`job`='{7}',`mapID`='{8}',`lv`='{9}',`jlv1`='{10}',`jlv2x`='{11}',`jlv2t`='{12}',`questRemaining`='{13}',`slot`='{14}'" +
                     ",`x`='{16}',`y`='{17}',`dir`='{18}',`hp`='{19}',`max_hp`='{20}',`mp`='{21}'," +
                    "`max_mp`='{22}',`sp`='{23}',`max_sp`='{24}',`str`='{25}',`dex`='{26}',`intel`='{27}',`vit`='{28}',`agi`='{29}',`mag`='{30}'," +
                    "`statspoint`='{31}',`skillpoint`='{32}',`skillpoint2x`='{33}',`skillpoint2t`='{34}',`skillpoint3`='{35}',`gold`='{36}',`cexp`='{37}',`jexp`='{38}'," +
                    "`save_map`='{39}',`save_x`='{40}',`save_y`='{41}',`possession_target`='{42}',`questid`='{43}',`questendtime`='{44}'" +
                    ",`queststatus`='{45}',`questcurrentcount1`='{46}',`questcurrentcount2`='{47}',`questcurrentcount3`='{48}'" +
                    ",`questresettime`='{49}',`fame`='{50}',`party`='{51}',`ring`='{52}',`golem`='{53}'" +
                    ",`cp`='{54}',`ecoin`='{55}'" +
                    ",`jointjlv`='{56}',`jjexp`='{57}',`wrp`='{58}'" +
                    ",`ep`='{59}',`eplogindate`='{60}',`epgreetingdate`='{61}',`cl`='{62}'" +
                    ",`epused`='{63}',`tailStyle`='{64}',`wingStyle`='{65}',`wingColor`='{66}',`lv1`='{67}',`jlv3`='{68}',`explorerEXP`='{69}',`usingpaper_id` = '{70}'" +
                    " ,`title_id` = '{71}' ,`abyssfloor`='{72}',`DualJobID`='{73}', exstatpoint='{74}',exskillpoint='{75}' " + " WHERE char_id='{15}' LIMIT 1",

                    CheckSQLString(aChar.Name), (byte)aChar.Race, (byte)aChar.Gender, aChar.HairStyle, aChar.HairColor, aChar.Wig,
                    aChar.Face, (byte)aChar.Job, mapid, aChar.Level, aChar.JobLevel1, aChar.JobLevel2X, aChar.JobLevel2T,
                    aChar.QuestRemaining, aChar.Slot, aChar.CharID, x, y, (byte)(aChar.Dir / 45), aChar.HP, aChar.MaxHP, aChar.MP,
                    aChar.MaxMP, aChar.SP, aChar.MaxSP, aChar.Str, aChar.Dex, aChar.Int, aChar.Vit, aChar.Agi, aChar.Mag, aChar.StatsPoint,
                    aChar.SkillPoint, aChar.SkillPoint2X, aChar.SkillPoint2T, aChar.SkillPoint3, aChar.Gold, aChar.CEXP, aChar.JEXP, aChar.SaveMap, aChar.SaveX, aChar.SaveY,
                    aChar.PossessionTarget, questid, ToSQLDateTime(questtime), (byte)status, count1, count2, count3, ToSQLDateTime(aChar.QuestNextResetTime),
                    aChar.Fame, partyid, ringid, golemid,
                    aChar.CP, aChar.ECoin, aChar.JointJobLevel,
                    aChar.JointJEXP, aChar.WRP, aChar.EP, ToSQLDateTime(aChar.EPLoginTime), ToSQLDateTime(aChar.EPGreetingTime),
                    aChar.CL, aChar.EPUsed, aChar.TailStyle, aChar.WingStyle, aChar.WingColor,
                    aChar.Level1, aChar.JobLevel3, aChar.ExplorerEXP, aChar.UsingPaperID.ToString(), aChar.PlayerTitleID.ToString(), aChar.AbyssFloor, aChar.DualJobID, aChar.EXStatPoint, aChar.EXSkillPoint);
                aChar.Online = online;

                try
                {
                    SQLExecuteNonQuery(sqlstr);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }

                SaveVar(aChar);

                SavePaper(aChar);
                SaveFGarden(aChar);
                //SaveFF(aChar.Ring);
                //SaveNavi(aChar);
                if (itemInfo)
                    SaveItem(aChar);
                if (fullinfo)
                {
                    SaveSkill(aChar);
                    SaveDualJobInfo(aChar, true);
                    //SaveNPCStates(aChar);
                }
                SaveQuestInfo(aChar);
                SaveStamps(aChar);
            }
        }
        public void SaveQuestInfo(ActorPC pc)
        {

            string sqlstr = string.Format("DELETE FROM `questinfo` WHERE `char_id`='{0}';", pc.CharID);
            foreach (KeyValuePair<uint, ActorPC.KillInfo> i in pc.KillList)
            {
                byte ss = 0;
                if (i.Value.isFinish)
                    ss = 1;
                sqlstr += string.Format("INSERT INTO `questinfo`(`char_id`,`object_id`,`count`,`totalcount`,`infinish`) VALUES ('{0}','{1}','{2}','{3}','{4}');", pc.CharID, i.Key, i.Value.Count, i.Value.TotalCount, ss);
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
        public void GetQuestInfo(ActorPC pc)
        {
            string sqlstr;
            DataRowCollection result = null;
            try
            {
                sqlstr = "SELECT * FROM `questinfo` WHERE `char_id`='" + pc.CharID + "'";
                try
                {
                    result = SQLExecuteQuery(sqlstr);
                    if (result.Count > 0)
                    {
                        for (int i = 0; i < result.Count; i++)
                        {
                            ActorPC.KillInfo ki = new ActorPC.KillInfo();
                            uint mobid = (uint)result[i]["object_id"];
                            uint c = (uint)result[i]["count"];
                            ki.Count = (int)c;
                            c = (uint)result[i]["totalcount"];
                            ki.TotalCount = (int)c;
                            byte s = (byte)result[i]["infinish"];
                            if (s == 1)
                                ki.isFinish = true;
                            else
                                ki.isFinish = false;
                            if (!pc.KillList.ContainsKey(mobid))
                                pc.KillList.Add(mobid, ki);
                        }
                    }
                }
                catch (Exception ex)
                {
                    SagaLib.Logger.ShowError(ex);
                }
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
            }
        }
        /*
        public void SaveNavi(ActorPC pc)
        {
            string sqlstr = string.Format("DELETE FROM `navi` WHERE `account_id`='{0}';", pc.Account.AccountID);
            foreach (SagaDB.Navi.Category c in pc.Navi.Categories.Values)
            {
                foreach (SagaDB.Navi.Event e in c.Events.Values)
                {
                    foreach (SagaDB.Navi.Step s in e.Steps.Values)
                    {
                        if (e.Show)
                            sqlstr += string.Format("INSERT INTO `navi`(`account_id`,`CategoryID`,`EventID`,`StepID`,`EventState`,`StepDisplay`, `StepFinished`) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}');", pc.Account.AccountID,
                                c.ID, e.ID, s.ID, e.State, s.Display, s.Finished);
                    }
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
        public void GetNavi(ActorPC pc)
        {
            string sqlstr;
            DataRow result = null;
            try
            {
                sqlstr = "SELECT * FROM `navi` WHERE `anncount_id`='" + pc.Account.AccountID + "' LIMIT 1";
                try
                {
                    result = SQLExecuteQuery(sqlstr)[0];
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    return;
                }
                uint categoryId, eventId, stepId; ;

                categoryId = (uint)result["CategoryID"];
                eventId = (uint)result["EventID"];
                stepId = (uint)result["StepID"];
                pc.Navi.Categories[categoryId].Events[eventId].State = (byte)result["EventState"];
                pc.Navi.Categories[categoryId].Events[eventId].Steps[stepId].Display = (bool)result["StepDisplay"];
                pc.Navi.Categories[categoryId].Events[eventId].Steps[stepId].Finished = (bool)result["StepFinished"];
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                return;
            }
        }
        */
        public void SaveWRP(ActorPC pc)
        {
            string sqlstr = string.Format("UPDATE `char` SET `wrp`='{0}' WHERE char_id='{1}' LIMIT 1",
                    pc.WRP, pc.CharID);
            try
            {
                SQLExecuteNonQuery(sqlstr);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        public void DeleteChar(ActorPC aChar)
        {
            string sqlstr;
            uint account_id = (uint)SQLExecuteQuery("SELECT `account_id` FROM `char` WHERE `char_id`='" + aChar.CharID + "' LIMIT 1")[0]["account_id"];
            sqlstr = "DELETE FROM `char` WHERE char_id='" + aChar.CharID + "';";
            sqlstr += "DELETE FROM `inventory` WHERE char_id='" + aChar.CharID + "';";
            sqlstr += "DELETE FROM `skill` WHERE char_id='" + aChar.CharID + "';";
            sqlstr += "DELETE FROM `cvar` WHERE char_id='" + aChar.CharID + "';";
            sqlstr += "DELETE FROM `friend` WHERE `char_id`='" + aChar.CharID + "' OR `friend_char_id`='" + aChar.CharID + "';";
            if (aChar.Party != null)
            {
                if (aChar.Party.Leader != null)
                {
                    if (aChar.Party.Leader.CharID == aChar.CharID)
                        DeleteParty(aChar.Party);
                }
            }
            if (aChar.Ring != null)
            {
                if (aChar.Ring.Leader != null)
                {
                    if (aChar.Ring.Leader.CharID == aChar.CharID)
                        DeleteRing(aChar.Ring);
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

        public ActorPC GetChar(uint charID, bool fullinfo)
        {
            string sqlstr;
            DataRow result = null;
            ActorPC pc = null;
            try
            {
                uint account = GetAccountID(charID);
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
                pc.UsingPaperID = (ushort)result["usingpaper_id"];
                pc.PlayerTitleID = (ushort)result["title_id"];
                pc.Gender = (PC_GENDER)(byte)result["gender"];
                pc.TailStyle = (byte)result["tailStyle"];
                pc.WingStyle = (byte)result["wingStyle"];
                pc.WingColor = (byte)result["wingColor"];
                pc.HairStyle = (ushort)result["hairStyle"];
                pc.HairColor = (byte)result["hairColor"];
                pc.Wig = (ushort)result["wig"];
                pc.Face = (ushort)result["face"];
                pc.Job = (PC_JOB)(byte)result["job"];
                pc.MapID = (uint)result["mapID"];
                pc.Level = (byte)result["lv"];
                pc.Level1 = (byte)result["lv1"];
                pc.JobLevel1 = (byte)result["jlv1"];
                pc.JobLevel2X = (byte)result["jlv2x"];
                pc.JobLevel2T = (byte)result["jlv2t"];
                pc.JobLevel3 = (byte)result["jlv3"];
                pc.JointJobLevel = (byte)result["jointjlv"];

                pc.QuestRemaining = (ushort)result["questRemaining"];
                pc.QuestNextResetTime = (DateTime)result["questresettime"];
                pc.Fame = (uint)result["fame"];
                pc.Slot = (byte)result["slot"];
                if (fullinfo)
                {
                    Map.MapInfo info = null;
                    if (Map.MapInfoFactory.Instance.MapInfo.ContainsKey(pc.MapID))
                    {
                        info = Map.MapInfoFactory.Instance.MapInfo[pc.MapID];
                    }
                    else
                    {
                        if (Map.MapInfoFactory.Instance.MapInfo.ContainsKey((uint)(pc.MapID / 1000 * 1000)))
                        {
                            info = Map.MapInfoFactory.Instance.MapInfo[(uint)(pc.MapID / 1000 * 1000)];
                        }
                    }

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
                pc.EP = (uint)result["ep"];
                pc.EPLoginTime = (DateTime)result["eplogindate"];
                pc.EPGreetingTime = (DateTime)result["epgreetingdate"];
                pc.EPUsed = (short)result["epused"];
                pc.CL = (short)result["cl"];
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
                pc.SkillPoint3 = (ushort)result["skillpoint3"];
                pc.EXStatPoint = (ushort)result["exstatpoint"];
                pc.EXSkillPoint = (byte)result["exskillpoint"];
                //pc.DefLv = (byte)result["deflv"];
                //pc.MDefLv = (byte)result["mdeflv"];
                //pc.DefPoint = (uint)result["defpoint"];
                //pc.MDefPoint = (uint)result["mdefpoint"];
                lock (this)
                {
                    int old = Logger.SQLLogLevel.Value;
                    Logger.SQLLogLevel.Value = 0;
                    pc.Gold = (long)result["gold"];
                    Logger.SQLLogLevel.Value = old;
                }
                pc.CP = (uint)result["cp"];
                pc.ECoin = (uint)result["ecoin"];
                pc.CEXP = (ulong)result["cexp"];
                //pc.JEXP = (ulong)result["jexp"];
                pc.JointJEXP = (ulong)result["jjexp"];
                pc.ExplorerEXP = (ulong)result["explorerEXP"];
                pc.WRP = (int)result["wrp"];
                pc.PossessionTarget = (uint)result["possession_target"];
                pc.AbyssFloor = (int)result["abyssfloor"];
                Party.Party party = new SagaDB.Party.Party();
                party.ID = (uint)result["party"];
                Ring.Ring ring = new SagaDB.Ring.Ring();
                ring.ID = (uint)result["ring"];
                if (party.ID != 0)
                    pc.Party = party;
                if (ring.ID != 0)
                    pc.Ring = ring;
                uint golem = (uint)result["golem"];
                if (golem != 0)
                {
                    pc.Golem = new ActorGolem();
                    pc.Golem.ActorID = golem;
                }

                pc.DualJobID = (byte)(result["DualJobID"] == null ? 0 : result["DualJobID"]);

                if (fullinfo)
                {
                    uint questid = (uint)result["questid"];
                    if (questid != 0)
                    {
                        try
                        {
                            Quest quest = new Quest(questid);
                            quest.EndTime = (DateTime)result["questendtime"];
                            quest.Status = (QuestStatus)(byte)result["queststatus"];
                            quest.CurrentCount1 = (int)result["questcurrentcount1"];
                            quest.CurrentCount2 = (int)result["questcurrentcount2"];
                            quest.CurrentCount3 = (int)result["questcurrentcount3"];
                            pc.Quest = quest;
                        }
                        catch { }
                    }
                    GetSkill(pc);
                    GetJobLV(pc);
                    GetNPCStates(pc);
                    GetFGarden(pc);
                    GetVar(pc);
                    //GetNavi(pc);
                    //GetFF(pc);
                }
                GetPaper(pc);
                GetQuestInfo(pc);
                GetItem(pc);
                GetVShop(pc);
                GetStamps(pc);
                //GetGifts(pc);
                //GetMail(pc);
                GetTamaireLending(pc);
                GetTamaireRental(pc);
                GetMosterGuide(pc);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            return pc;
        }

        public ActorPC GetChar(uint charID)
        {
            return GetChar(charID, true);
        }

        public void GetVShop(ActorPC pc)
        {
            uint account = GetAccountID(pc);
            string sqlstr = "SELECT `vshop_points`,`used_vshop_points` FROM `login` WHERE account_id='" + account + "' LIMIT 1";
            DataRow result = SQLExecuteQuery(sqlstr)[0];
            ActorEventHandler eh = pc.e;
            pc.e = null;
            pc.VShopPoints = (uint)result["vshop_points"];
            pc.e = eh;
            pc.UsedVShopPoints = (uint)result["used_vshop_points"];
        }
        public void SaveSkill(ActorPC pc)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            System.IO.BinaryWriter bw = new System.IO.BinaryWriter(ms);
            int count = pc.Skills.Count + pc.Skills2.Count + pc.SkillsReserve.Count;
            if (pc.Rebirth || pc.Job == pc.Job3)
                count = pc.Skills.Count + pc.Skills2_1.Count + pc.Skills2_2.Count + pc.Skills3.Count;
            else
                count = pc.Skills.Count + pc.Skills2.Count + pc.SkillsReserve.Count;
            int nosave = 0;
            foreach (Skill.Skill i in pc.Skills.Values)
            {
                if (i.NoSave)
                    nosave++;
            }
            if (pc.Rebirth || pc.Job == pc.Job3)
            {
                foreach (Skill.Skill i in pc.Skills2_1.Values)
                {
                    if (i.NoSave)
                        nosave++;
                }
                foreach (Skill.Skill i in pc.Skills2_2.Values)
                {
                    if (i.NoSave)
                        nosave++;
                }
                foreach (Skill.Skill i in pc.Skills3.Values)
                {
                    if (i.NoSave)
                        nosave++;
                }
            }
            else
            {
                foreach (Skill.Skill i in pc.Skills2.Values)
                {
                    if (i.NoSave)
                        nosave++;
                }
                foreach (Skill.Skill i in pc.SkillsReserve.Values)
                {
                    if (i.NoSave)
                        nosave++;
                }
            }
            count -= nosave;
            bw.Write(count);
            foreach (uint j in pc.Skills.Keys)
            {
                if (pc.Skills[j].NoSave)
                    continue;
                bw.Write(j);
                bw.Write(pc.Skills[j].Level);
            }
            if (pc.Rebirth || pc.Job == pc.Job3)
            {
                foreach (uint j in pc.Skills2_1.Keys)
                {
                    if (pc.Skills2_1[j].NoSave)
                        continue;
                    bw.Write(j);
                    bw.Write(pc.Skills2_1[j].Level);
                }
                foreach (uint j in pc.Skills2_2.Keys)
                {
                    if (pc.Skills2_2[j].NoSave)
                        continue;
                    bw.Write(j);
                    bw.Write(pc.Skills2_2[j].Level);
                }
                foreach (uint j in pc.Skills3.Keys)
                {
                    if (pc.Skills3[j].NoSave)
                        continue;
                    bw.Write(j);
                    bw.Write(pc.Skills3[j].Level);
                }
            }
            else
            {
                foreach (uint j in pc.Skills2.Keys)
                {
                    if (pc.Skills2[j].NoSave)
                        continue;
                    bw.Write(j);
                    bw.Write(pc.Skills2[j].Level);
                }
                foreach (uint j in pc.SkillsReserve.Keys)
                {
                    if (pc.SkillsReserve[j].NoSave)
                        continue;
                    bw.Write(j);
                    bw.Write(pc.SkillsReserve[j].Level);
                }
            }
            ms.Flush();
            //MySqlCommand cmd = new MySqlCommand(string.Format("REPLACE INTO `skill`(`char_id`,`skills`) VALUES ('{0}',?data);", pc.CharID));
            //string sqlstr = string.Format("DELETE FROM `skill` WHERE `char_id`='{0}' AND `jobbasic`='{1}';", pc.CharID, (int)pc.JobBasic);
            MySqlCommand cmd = new MySqlCommand(string.Format("REPLACE INTO `skill`(`char_id`,`skills`,`jobbasic`,`joblv`,`jobexp`,`skillpoint`,`skillpoint2x`," +
                "`skillpoint2t`,`skillpoint3`) VALUES ('{0}',?data,'{1}','{2}','{3}','{4}','{5}','{6}','{7}');",
                   pc.CharID, (int)pc.JobBasic, pc.JobLevel3, pc.JEXP, pc.SkillPoint, pc.SkillPoint2X, pc.SkillPoint2T, pc.SkillPoint3));
            cmd.Parameters.Add("?data", MySqlDbType.Blob).Value = ms.ToArray();
            ms.Close();
            try
            {
                //SQLExecuteNonQuery(sqlstr);
                SQLExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        public void SaveVShop(ActorPC pc)
        {
            ActorEventHandler eh = pc.e;
            pc.e = null;
            string sqlstr = string.Format("UPDATE `login` SET `vshop_points`='{0}',`used_vshop_points`='{1}'" +
                  " WHERE account_id='{2}' LIMIT 1",
                  pc.VShopPoints, pc.UsedVShopPoints, pc.Account.AccountID);
            pc.e = eh;
            SQLExecuteNonQuery(sqlstr);
        }

        public void SaveServerVar(ActorPC fakepc)
        {
            string sqlstr = "TRUNCATE TABLE `sVar`;";
            foreach (string i in fakepc.AStr.Keys)
            {
                sqlstr += string.Format("INSERT INTO `sVar`(`name`,`type`,`content`) VALUES " + "('{0}',0,'{1}');", i, fakepc.AStr[i]);
            }
            foreach (string i in fakepc.AInt.Keys)
            {
                sqlstr += string.Format("INSERT INTO `sVar`(`name`,`type`,`content`) VALUES " + "('{0}',1,'{1}');", i, fakepc.AInt[i]);
            }
            foreach (string i in fakepc.AMask.Keys)
            {
                sqlstr += string.Format("INSERT INTO `sVar`(`name`,`type`,`content`) VALUES " + "('{0}',2,'{1}');", i, fakepc.AMask[i].Value);
            }
            SQLExecuteNonQuery(sqlstr);
            sqlstr = "TRUNCATE TABLE `sList`;";
            foreach (var item in fakepc.Adict)
            {
                foreach (var i in item.Value.Keys)
                {
                    sqlstr += string.Format("INSERT INTO `sList`(`name`,`key`,`type`,`content`) VALUES " + "('{0}','{1}',1,'{2}');", item.Key, i, item.Value[i]);
                }
            }
            SQLExecuteNonQuery(sqlstr);
        }

        public ActorPC LoadServerVar()
        {
            ActorPC fakepc = new ActorPC();
            string sqlstr = "SELECT * FROM `sVar`;";
            DataRowCollection res;
            res = SQLExecuteQuery(sqlstr);
            foreach (DataRow i in res)
            {
                byte type = (byte)i["type"];
                switch (type)
                {
                    case 0:
                        fakepc.AStr[(string)i["name"]] = (string)i["content"];
                        break;
                    case 1:
                        fakepc.AInt[(string)i["name"]] = int.Parse((string)i["content"]);
                        break;
                    case 2:
                        fakepc.AMask[(string)i["name"]] = new BitMask(int.Parse((string)i["content"]));
                        break;

                }
            }

            sqlstr = "SELECT * FROM `sList`;";
            res = SQLExecuteQuery(sqlstr);
            foreach (DataRow i in res)
            {
                byte type = (byte)i["type"];
                switch (type)
                {
                    case 1:
                        fakepc.Adict[(string)i["name"]][(string)i["key"]] = int.Parse((string)i["content"]);
                        break;
                }
            }
            return fakepc;
        }
        public void SaveVar(ActorPC pc)
        {
            uint account_id = (uint)SQLExecuteQuery("SELECT `account_id` FROM `char` WHERE `char_id`='" + pc.CharID + "' LIMIT 1")[0]["account_id"];
            Encoding enc = Encoding.UTF8;

            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            System.IO.BinaryWriter bw = new System.IO.BinaryWriter(ms);
            bw.Write(pc.CInt.Count);
            foreach (string j in pc.CInt.Keys)
            {
                byte[] buf = enc.GetBytes(j);
                bw.Write(buf.Length);
                bw.Write(buf);
                bw.Write(pc.CInt[j]);
            }
            bw.Write(pc.CMask.Count);
            foreach (string j in pc.CMask.Keys)
            {
                byte[] buf = enc.GetBytes(j);
                bw.Write(buf.Length);
                bw.Write(buf);
                bw.Write(pc.CMask[j].Value);
            }
            bw.Write(pc.CStr.Count);
            foreach (string j in pc.CStr.Keys)
            {
                byte[] buf = enc.GetBytes(j);
                bw.Write(buf.Length);
                bw.Write(buf);
                buf = enc.GetBytes(pc.CStr[j]);
                bw.Write(buf.Length);
                bw.Write(buf);
            }

            ms.Flush();

            MySqlCommand cmd = new MySqlCommand(string.Format("REPLACE `cvar`(`char_id`,`values`) VALUES ('{0}',?data);",
                   pc.CharID));
            cmd.Parameters.Add("?data", MySqlDbType.Blob).Value = ms.ToArray();
            ms.Close();
            try
            {
                SQLExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }

            ms = new System.IO.MemoryStream();
            bw = new System.IO.BinaryWriter(ms);
            bw.Write(pc.AInt.Count);
            foreach (string j in pc.AInt.Keys)
            {
                byte[] buf = enc.GetBytes(j);
                bw.Write(buf.Length);
                bw.Write(buf);
                bw.Write(pc.AInt[j]);
            }
            bw.Write(pc.AMask.Count);
            foreach (string j in pc.AMask.Keys)
            {
                byte[] buf = enc.GetBytes(j);
                bw.Write(buf.Length);
                bw.Write(buf);
                bw.Write(pc.AMask[j].Value);
            }
            bw.Write(pc.AStr.Count);
            foreach (string j in pc.AStr.Keys)
            {
                byte[] buf = enc.GetBytes(j);
                bw.Write(buf.Length);
                bw.Write(buf);
                buf = enc.GetBytes(pc.AStr[j]);
                bw.Write(buf.Length);
                bw.Write(buf);
            }

            ms.Flush();

            cmd = new MySqlCommand(string.Format("REPLACE INTO `avar`(`account_id`,`values`) VALUES ('{0}',?data); ",
                  account_id));
            cmd.Parameters.Add("?data", MySqlDbType.Blob).Value = ms.ToArray();
            ms.Close();
            try
            {
                SQLExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        private void GetVar(ActorPC pc)
        {
            string sqlstr = "SELECT * FROM `cvar` WHERE `char_id`='" + pc.CharID + "' LIMIT 1;";
            uint account_id = (uint)SQLExecuteQuery("SELECT `account_id` FROM `char` WHERE `char_id`='" + pc.CharID + "' LIMIT 1")[0]["account_id"];
            Encoding enc = Encoding.UTF8;
            DataRowCollection res;
            res = SQLExecuteQuery(sqlstr);

            if (res.Count > 0)
            {
                byte[] buf = (byte[])res[0]["values"];
                System.IO.MemoryStream ms = new System.IO.MemoryStream(buf);
                System.IO.BinaryReader br = new System.IO.BinaryReader(ms);
                int count = br.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    string name = enc.GetString(br.ReadBytes(br.ReadInt32()));
                    pc.CInt[name] = br.ReadInt32();
                }
                count = br.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    string name = enc.GetString(br.ReadBytes(br.ReadInt32()));
                    pc.CMask[name] = new BitMask(br.ReadInt32());
                }
                count = br.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    string name = enc.GetString(br.ReadBytes(br.ReadInt32()));
                    pc.CStr[name] = enc.GetString(br.ReadBytes(br.ReadInt32()));
                }
            }
            sqlstr = "SELECT * FROM `avar` WHERE `account_id`='" + account_id + "' LIMIT 1;";
            res = SQLExecuteQuery(sqlstr);
            if (res.Count > 0)
            {
                byte[] buf = (byte[])res[0]["values"];
                System.IO.MemoryStream ms = new System.IO.MemoryStream(buf);
                System.IO.BinaryReader br = new System.IO.BinaryReader(ms);
                int count = br.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    string name = enc.GetString(br.ReadBytes(br.ReadInt32()));
                    pc.AInt[name] = br.ReadInt32();
                }
                count = br.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    string name = enc.GetString(br.ReadBytes(br.ReadInt32()));
                    pc.AMask[name] = new BitMask(br.ReadInt32());
                }
                count = br.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    string name = enc.GetString(br.ReadBytes(br.ReadInt32()));
                    pc.AStr[name] = enc.GetString(br.ReadBytes(br.ReadInt32()));
                }
            }
        }

        public void SaveItem(ActorPC pc)
        {
            try
            {
                uint account = GetAccountID(pc);
                BinaryFormatter bf = new BinaryFormatter();
                MySqlCommand cmd;
                if ((!pc.Inventory.IsEmpty || pc.Inventory.NeedSave) && pc.Inventory.Items[ContainerType.BODY].Count < 1000)
                {
                    cmd = new MySqlCommand(string.Format("UPDATE `inventory` SET `data`=?data WHERE `char_id`='{0}' LIMIT 1;\r\n",
                        pc.CharID));
                    byte[] itemdata = pc.Inventory.ToBytes();
                    cmd.Parameters.Add("?data", MySqlDbType.Blob).Value = itemdata;

                    if (pc.Account != null)
                        Logger.ShowInfo("存储玩家(" + pc.Account.AccountID.ToString() + ")：" + pc.Name + "道具信息...大小：" + itemdata.Length);
                    try
                    {
                        SQLExecuteNonQuery(cmd);
                    }
                    catch (Exception ex)
                    {
                        Logger.ShowError(ex);
                    }
                }

                if (pc.Inventory.WareHouse != null)
                {
                    if (!pc.Inventory.IsWarehouseEmpty || pc.Inventory.NeedSaveWare)
                    {
                        cmd = new MySqlCommand(string.Format("UPDATE `warehouse` SET `data`=?data WHERE `account_id`='{0}' LIMIT 1;\r\n",
                        account));
                        MySqlParameter para = cmd.Parameters.Add("?data", MySqlDbType.Blob);
                        para.Value = pc.Inventory.WareToBytes();

                        try
                        {
                            SQLExecuteNonQuery(cmd);
                        }
                        catch (Exception ex)
                        {
                            Logger.ShowError(ex);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }
        public void GetJobLV(ActorPC pc)
        {
            string sqlstr;
            DataRowCollection result = null;
            try
            {
                sqlstr = "SELECT * FROM `skill` WHERE `char_id`='" + pc.CharID + "' AND `jobbasic`='" + 1 + "' LIMIT 1;";
                result = SQLExecuteQuery(sqlstr);
                if (result.Count > 0)
                    pc.JobLV_GLADIATOR = (byte)result[0]["joblv"];
                sqlstr = "SELECT * FROM `skill` WHERE `char_id`='" + pc.CharID + "' AND `jobbasic`='" + 31 + "' LIMIT 1;";
                result = SQLExecuteQuery(sqlstr);
                if (result.Count > 0)
                    pc.JobLV_HAWKEYE = (byte)result[0]["joblv"];
                sqlstr = "SELECT * FROM `skill` WHERE `char_id`='" + pc.CharID + "' AND `jobbasic`='" + 41 + "' LIMIT 1;";
                result = SQLExecuteQuery(sqlstr);
                if (result.Count > 0)
                    pc.JobLV_FORCEMASTER = (byte)result[0]["joblv"];
                sqlstr = "SELECT * FROM `skill` WHERE `char_id`='" + pc.CharID + "' AND `jobbasic`='" + 61 + "' LIMIT 1;";
                result = SQLExecuteQuery(sqlstr);
                if (result.Count > 0)
                    pc.JobLV_CARDINAL = (byte)result[0]["joblv"];
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                return;
            }
        }

        public void GetSkill(ActorPC pc)
        {
            try
            {
                string sqlstr;
                DataRowCollection result = null;
                //sqlstr = "SELECT * FROM `skill` WHERE `char_id`='" + pc.CharID + "' LIMIT 1;";
                sqlstr = "SELECT * FROM `skill` WHERE `char_id`='" + pc.CharID + "' AND `jobbasic`='" + (int)pc.JobBasic + "' LIMIT 1;";
                try
                {
                    result = SQLExecuteQuery(sqlstr);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    return;
                }

                if (result.Count > 0)
                {
                    byte[] buf = (byte[])result[0]["skills"];
                    pc.JobLevel3 = (byte)result[0]["joblv"];
                    if (pc.JobLevel3 == 0) pc.JobLevel3 = 1;
                    pc.JEXP = (ulong)result[0]["jobexp"];
                    pc.SkillPoint = (ushort)result[0]["skillpoint"];
                    pc.SkillPoint2X = (ushort)result[0]["skillpoint2x"];
                    pc.SkillPoint2T = (ushort)result[0]["skillpoint2t"];
                    pc.SkillPoint3 = (ushort)result[0]["skillpoint3"];

                    Dictionary<uint, byte> skills = Skill.SkillFactory.Instance.SkillList(pc.JobBasic);
                    Dictionary<uint, byte> skills2x = Skill.SkillFactory.Instance.SkillList(pc.Job2X);
                    Dictionary<uint, byte> skills2t = Skill.SkillFactory.Instance.SkillList(pc.Job2T);
                    Dictionary<uint, byte> skills3 = Skill.SkillFactory.Instance.SkillList(pc.Job3);

                    System.IO.MemoryStream ms = new System.IO.MemoryStream(buf);
                    System.IO.BinaryReader br = new System.IO.BinaryReader(ms);
                    int count = br.ReadInt32();
                    for (int i = 0; i < count; i++)
                    {
                        uint skillID = br.ReadUInt32();
                        byte lv = br.ReadByte();
                        Skill.Skill skill = Skill.SkillFactory.Instance.GetSkill(skillID, lv);
                        if (skill == null)
                            continue;
                        if (skills.ContainsKey(skill.ID))
                        {
                            if (!pc.Skills.ContainsKey(skill.ID))
                                pc.Skills.Add(skill.ID, skill);
                        }
                        else if (skills2x.ContainsKey(skill.ID))
                        {
                            if (!pc.Rebirth || pc.Job != pc.Job3)
                            {
                                if (pc.Job == pc.Job2X)
                                {
                                    if (!pc.Skills2.ContainsKey(skill.ID))
                                        pc.Skills2.Add(skill.ID, skill);
                                }
                                else
                                {
                                    if (!pc.SkillsReserve.ContainsKey(skill.ID))
                                        pc.SkillsReserve.Add(skill.ID, skill);
                                }
                            }
                            else
                            {
                                if (!pc.Skills2_1.ContainsKey(skill.ID))
                                    pc.Skills2_1.Add(skill.ID, skill);
                                if (!pc.Skills2.ContainsKey(skill.ID))
                                    pc.Skills2.Add(skill.ID, skill);
                            }
                        }
                        else if (skills2t.ContainsKey(skill.ID))
                        {
                            if (!pc.Rebirth || pc.Job != pc.Job3)
                            {
                                if (pc.Job == pc.Job2T)
                                {
                                    if (!pc.Skills2.ContainsKey(skill.ID))
                                        pc.Skills2.Add(skill.ID, skill);
                                }
                                else
                                {
                                    if (!pc.SkillsReserve.ContainsKey(skill.ID))
                                        pc.SkillsReserve.Add(skill.ID, skill);
                                }
                            }
                            else
                            {
                                if (!pc.Skills2_2.ContainsKey(skill.ID))
                                    pc.Skills2_2.Add(skill.ID, skill);
                                if (!pc.Skills2.ContainsKey(skill.ID))
                                    pc.Skills2.Add(skill.ID, skill);
                            }
                        }
                        else if (skills3.ContainsKey(skill.ID))
                        {
                            if (!pc.Skills3.ContainsKey(skill.ID))
                                pc.Skills3.Add(skill.ID, skill);
                        }
                        else
                        {
                            if (!pc.Skills.ContainsKey(skill.ID))
                                pc.Skills.Add(skill.ID, skill);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                return;
            }
        }

        public void SaveNPCState(ActorPC pc, uint npcID)
        {
            if (pc.NPCStates.ContainsKey(npcID))
            {
                bool state = pc.NPCStates[npcID];
                byte value = 0;
                if (state)
                    value = 1;
                string sqlstr = $"SELECT * FROM `npcstates` WHERE `npc_id`='{npcID}',char_id='{pc.CharID}'";
                DataRowCollection result = SQLExecuteQuery(sqlstr);
                if (result.Count > 0)
                    sqlstr = $"INSERT INTO `npcstates`(`char_id`,`npc_id`,`state`) VALUES ('{pc.CharID}','{npcID}','{value}')";
                else
                    sqlstr = $"UPDATE `npcstates` SET `npc_id`='{npcID}',`state`='{value}' WHERE `char_id`='{pc.CharID}'";
                SQLExecuteNonQuery(sqlstr);
            }
        }

        public void GetNPCStates(ActorPC pc)
        {
            string sqlstr;
            DataRowCollection result = null;
            sqlstr = $"SELECT * FROM `npcstates` WHERE `char_id`='{pc.CharID}';";
            try
            {
                result = SQLExecuteQuery(sqlstr);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                return;
            }
            for (int i = 0; i < result.Count; i++)
            {
                uint npcID = (uint)result[i]["npc_id"];
                bool state = (bool)result[i]["state"];
                pc.NPCStates.Add(npcID, state);
            }
        }

        public void GetItem(ActorPC pc)
        {
            try
            {
                string sqlstr;
                DataRowCollection result = null;
                uint account = GetAccountID(pc);
                sqlstr = "SELECT * FROM `inventory` WHERE `char_id`='" + pc.CharID + "' LIMIT 1;";
                try
                {
                    result = SQLExecuteQuery(sqlstr);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    return;
                }
                if (result.Count > 0)
                {
                    Item.Inventory inv = null;
                    try
                    {
                        byte[] buf = (byte[])result[0]["data"];
                        Logger.ShowInfo("获取玩家(" + account.ToString() + ")：" + pc.Name + "道具信息...大小：" + buf.Length);
                        System.IO.MemoryStream ms = new System.IO.MemoryStream(buf);
                        if (buf[0] == 0x42 && buf[1] == 0x5A)
                        {
                            System.IO.MemoryStream ms2 = new System.IO.MemoryStream();
                            BZip2.Decompress(ms, ms2);
                            ms = new System.IO.MemoryStream(ms2.ToArray());
                            BinaryFormatter bf = new BinaryFormatter();
                            inv = (Item.Inventory)bf.Deserialize(ms);

                            if (inv != null)
                            {
                                pc.Inventory = inv;
                                pc.Inventory.Owner = pc;
                            }
                        }
                        else
                        {
                            inv = new Inventory(pc);
                            inv.FromStream(ms);
                            pc.Inventory = inv;
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.ShowError(ex);
                    }
                }
                sqlstr = "SELECT * FROM `warehouse` WHERE `account_id`='" + account + "';";
                try
                {
                    result = SQLExecuteQuery(sqlstr);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    return;
                }
                if (result.Count > 0)
                {
                    Dictionary<WarehousePlace, List<SagaDB.Item.Item>> inv = null;
                    try
                    {
                        byte[] buf = (byte[])result[0]["data"];
                        System.IO.MemoryStream ms = new System.IO.MemoryStream(buf);
                        if (buf[0] == 0x42 && buf[1] == 0x5A)
                        {
                            pc.Inventory.WareHouse = new Dictionary<WarehousePlace, List<SagaDB.Item.Item>>();
                            System.IO.MemoryStream ms2 = new System.IO.MemoryStream();
                            BZip2.Decompress(ms, ms2);
                            ms = new System.IO.MemoryStream(ms2.ToArray());
                            BinaryFormatter bf = new BinaryFormatter();
                            inv = (Dictionary<WarehousePlace, List<SagaDB.Item.Item>>)bf.Deserialize(ms);
                            if (inv != null)
                            {
                                pc.Inventory.wareIndex = 200000001;
                                foreach (WarehousePlace i in inv.Keys)
                                {
                                    pc.Inventory.WareHouse.Add(i, new List<SagaDB.Item.Item>());
                                    foreach (Item.Item j in inv[i])
                                    {
                                        pc.Inventory.AddWareItem(i, j);
                                    }
                                }
                            }
                        }
                        else
                        {
                            if (pc.Inventory.WareHouse == null)
                                pc.Inventory.WareHouse = new Item.Inventory(pc).WareHouse;
                            pc.Inventory.WareFromSteam(ms);
                        }

                    }
                    catch (Exception ex)
                    {
                        Logger.ShowError(ex);
                    }

                }
                if (!pc.Inventory.WareHouse.ContainsKey(WarehousePlace.Acropolis))
                    pc.Inventory.WareHouse.Add(WarehousePlace.Acropolis, new List<Item.Item>());
                if (!pc.Inventory.WareHouse.ContainsKey(WarehousePlace.FederalOfIronSouth))
                    pc.Inventory.WareHouse.Add(WarehousePlace.FederalOfIronSouth, new List<Item.Item>());
                if (!pc.Inventory.WareHouse.ContainsKey(WarehousePlace.FarEast))
                    pc.Inventory.WareHouse.Add(WarehousePlace.FarEast, new List<Item.Item>());
                if (!pc.Inventory.WareHouse.ContainsKey(WarehousePlace.IronSouth))
                    pc.Inventory.WareHouse.Add(WarehousePlace.IronSouth, new List<Item.Item>());
                if (!pc.Inventory.WareHouse.ContainsKey(WarehousePlace.KingdomOfNorthan))
                    pc.Inventory.WareHouse.Add(WarehousePlace.KingdomOfNorthan, new List<Item.Item>());
                if (!pc.Inventory.WareHouse.ContainsKey(WarehousePlace.MiningCamp))
                    pc.Inventory.WareHouse.Add(WarehousePlace.MiningCamp, new List<Item.Item>());
                if (!pc.Inventory.WareHouse.ContainsKey(WarehousePlace.Morg))
                    pc.Inventory.WareHouse.Add(WarehousePlace.Morg, new List<Item.Item>());
                if (!pc.Inventory.WareHouse.ContainsKey(WarehousePlace.Northan))
                    pc.Inventory.WareHouse.Add(WarehousePlace.Northan, new List<Item.Item>());
                if (!pc.Inventory.WareHouse.ContainsKey(WarehousePlace.RepublicOfFarEast))
                    pc.Inventory.WareHouse.Add(WarehousePlace.RepublicOfFarEast, new List<Item.Item>());
                if (!pc.Inventory.WareHouse.ContainsKey(WarehousePlace.Tonka))
                    pc.Inventory.WareHouse.Add(WarehousePlace.Tonka, new List<Item.Item>());
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                return;
            }
        }

        public bool CharExists(string name)
        {
            string sqlstr;
            DataRowCollection result = null;
            sqlstr = "SELECT count(*) FROM `char` WHERE name='" + CheckSQLString(name) + "'";
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
            string sqlstr = "SELECT `account_id` FROM `char` WHERE `char_id`='" + charID + "' LIMIT 1;";

            DataRowCollection result = SQLExecuteQuery(sqlstr);
            if (result.Count == 0)
                return 0;
            else
                return (uint)result[0]["account_id"];
        }

        public uint GetAccountID(ActorPC pc)
        {
            if (pc.Account != null)
                return (uint)pc.Account.AccountID;
            else
                return GetAccountID(pc.CharID);
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
                sqlstr = "SELECT `name`,`job`,`lv`,`jlv1`,`jlv2x`,`jlv2t`,`jlv3` FROM `char` WHERE `char_id`='" + friend + "' LIMIT 1;";
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
                chara.JobLevel3 = (byte)row["jlv3"];
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
            string sqlstr = $"SELECT * FROM `party` WHERE `party_id`='{id}' LIMIT 1;";
            DataRowCollection result = SQLExecuteQuery(sqlstr);
            Party.Party party = new SagaDB.Party.Party();
            if (result.Count != 0)
            {
                party.ID = id;
                uint leader = (uint)result[0]["leader"];
                party.Name = (string)result[0]["name"];
                if (party.Leader == null)
                    return null;
                return party;
            }
            return null;
        }

        public void GetPartyMember(Party.Party party)
        {
            string sqlstr = $"SELECT `char_id` FROM `partymember` WHERE `party_id`='{party.ID}';";
            DataRowCollection result = SQLExecuteQuery(sqlstr);

            for (byte index = 0; index < 7; index++)
            {
                uint member = (uint)result[index]["char_id"];
                if (member != 0)
                {
                    ActorPC pc = new ActorPC();
                    pc.CharID = member;

                    sqlstr = $"SELECT `name`,`job` FROM `char` WHERE `char_id`='{member}' LIMIT 1;";
                    DataRowCollection rows = SQLExecuteQuery(sqlstr);
                    if (rows.Count > 0)
                    {
                        DataRow row = rows[0];
                        pc.Name = (string)row["name"];
                        pc.Job = (PC_JOB)(byte)row["job"];
                        party.Members.Add(index, pc);
                    }
                }
            }
        }

        public void NewParty(Party.Party party)
        {
            uint index = 0;
            string name = party.Name;
            CheckSQLString(ref name);
            string sqlstr = $"INSERT INTO `party`(`name`,`leader`) VALUES ('{name}','{party.Leader.CharID}');";
            SQLExecuteScalar(sqlstr, out index);

            uint pos = 0;
            sqlstr = $"INSERT INTO `partymember`(`party_id`,`char_id`) VALUES ('{index}','{party.Leader.CharID}');";
            SQLExecuteScalar(sqlstr, out pos);
            party.ID = index;
        }

        public void SaveParty(Party.Party party)
        {
            uint leader = party.Leader.CharID;
            string name = party.Name;
            CheckSQLString(ref name);

            string sqlstr = $"UPDATE `party` SET `name`='{name}',`leader`='{leader}'  WHERE `party_id`='{party.ID}' LIMIT 1;";
            SQLExecuteNonQuery(sqlstr);

            sqlstr = $"DELETE FROM `partymember` WHERE `party_id`='{party.ID}';";
            SQLExecuteNonQuery(sqlstr);
            foreach (byte i in party.Members.Keys)
            {
                sqlstr += $"INSERT INTO `partymember`(`party_id`,`char_id`) VALUES ('{party.ID}','{party.Members[i].CharID}');";
                SQLExecuteNonQuery(sqlstr);
            }
        }
        public void DeleteParty(Party.Party party)
        {
            string sqlstr = $"DELETE FROM `party` WHERE `party_id`='{party.ID}';";
            SQLExecuteNonQuery(sqlstr);

            sqlstr = $"DELETE FROM `partymember` WHERE `party_id`='{party.ID}';";
            SQLExecuteNonQuery(sqlstr);
        }

        public Ring.Ring GetRing(uint id)
        {
            string sqlstr = "SELECT `leader`,`name`,`fame`,`ff_id` FROM `ring` WHERE `ring_id`='" + id + "' LIMIT 1;";
            DataRowCollection result = SQLExecuteQuery(sqlstr);

            Ring.Ring ring = new SagaDB.Ring.Ring();
            if (result.Count != 0)
            {
                ring.ID = id;
                uint leader = (uint)result[0]["leader"];
                ring.Name = (string)result[0]["name"];
                ring.Fame = (uint)result[0]["fame"];
                if (result[0]["ff_id"] != null)
                    ring.FF_ID = (uint)result[0]["ff_id"];
                sqlstr = "SELECT * FROM `ringmember` WHERE `ring_id`='" + id + "';";
                DataRowCollection result2 = SQLExecuteQuery(sqlstr);
                for (int i = 0; i < result2.Count; i++)
                {
                    ActorPC pc = new ActorPC();
                    pc.CharID = (uint)result2[i]["char_id"];
                    sqlstr = "SELECT `name`,`job` FROM `char` WHERE `char_id`='" + pc.CharID + "' LIMIT 1;";
                    DataRowCollection rows = SQLExecuteQuery(sqlstr);
                    if (rows.Count > 0)
                    {
                        DataRow row = rows[0];
                        pc.Name = (string)row["name"];
                        pc.Job = (PC_JOB)(byte)row["job"];
                        int index = ring.NewMember(pc);
                        if (index >= 0)
                            ring.Rights[index].Value = (int)(uint)result2[i]["right"];
                        if (leader == pc.CharID)
                            ring.Leader = pc;
                    }
                }
                if (ring.Leader == null)
                    return null;
                return ring;
            }
            return null;
        }

        public void NewRing(Ring.Ring ring)
        {
            uint index = 0;
            string name = ring.Name;
            CheckSQLString(ref name);
            string sqlstr = string.Format("SELECT `name` FROM `ring` WHERE `name`='{0}' LIMIT 1", ring.Name);
            if (SQLExecuteQuery(sqlstr).Count > 0)
            {
                ring.ID = 0xFFFFFFFF;
            }
            else
            {
                sqlstr = string.Format("INSERT INTO `ring`(`leader`,`name`) VALUES " +
                         "('0','{0}');", name);
                SQLExecuteScalar(sqlstr, out index);
                ring.ID = index;
            }
        }

        public void SaveRing(Ring.Ring ring, bool saveMembers)
        {
            string sqlstr = string.Format("UPDATE `ring` SET `leader`='{0}',`name`='{1}',`fame`='{2}',`ff_id`='{3}' WHERE `ring_id`='{4}' LIMIT 1;\r\n",
                ring.Leader.CharID, ring.Name, ring.Fame, ring.FF_ID, ring.ID);
            if (saveMembers)
            {
                sqlstr += string.Format("DELETE FROM `ringmember` WHERE `ring_id`='{0}';\r\n", ring.ID);
                foreach (int i in ring.Members.Keys)
                {
                    sqlstr += string.Format("INSERT INTO `ringmember`(`ring_id`,`char_id`,`right`) VALUES ('{0}','{1}','{2}');\r\n",
                        ring.ID, ring.Members[i].CharID, ring.Rights[i].Value);
                }
            }
            SQLExecuteNonQuery(sqlstr);
        }

        public void DeleteRing(Ring.Ring ring)
        {
            string sqlstr = string.Format("DELETE FROM `ring` WHERE `ring_id`='{0}';", ring.ID);
            sqlstr += string.Format("DELETE FROM `ringmember` WHERE `ring_id`='{0}';", ring.ID);
            SQLExecuteNonQuery(sqlstr);
        }

        public void RingEmblemUpdate(Ring.Ring ring, byte[] buf)
        {
            string sqlstr = string.Format("UPDATE `ring` SET `emblem`=0x{0},`emblem_date`='{1}' WHERE `ring_id`='{2}' LIMIT 1;",
                Conversions.bytes2HexString(buf), ToSQLDateTime(DateTime.Now.ToUniversalTime()), ring.ID);
            SQLExecuteNonQuery(sqlstr);
        }

        public byte[] GetRingEmblem(uint ring_id, DateTime date, out bool needUpdate, out DateTime newTime)
        {
            string sqlstr = string.Format("SELECT `emblem`,`emblem_date` FROM `ring` WHERE `ring_id`='{0}' LIMIT 1", ring_id);

            DataRowCollection result = SQLExecuteQuery(sqlstr);
            if (result.Count != 0)
            {
                if (result[0]["emblem"].GetType() == typeof(System.DBNull))
                {
                    needUpdate = false;
                    newTime = DateTime.Now;
                    return null;
                }
                else
                {
                    newTime = (DateTime)result[0]["emblem_date"];
                    if (date < newTime)
                    {
                        needUpdate = true;
                        byte[] buf = (byte[])result[0]["emblem"];
                        return buf;
                    }
                    else
                    {
                        needUpdate = false;
                        return new byte[0];
                    }
                }
            }
            needUpdate = false;
            newTime = DateTime.Now;

            return null;
        }

        public List<BBS.Post> GetBBS(uint bbsID)
        {
            string sqlstr = string.Format("SELECT * FROM `bbs` WHERE `bbsid`='{0}' ORDER BY `postdate` DESC;", bbsID);
            List<BBS.Post> list = new List<SagaDB.BBS.Post>();
            DataRowCollection result = SQLExecuteQuery(sqlstr);

            foreach (DataRow i in result)
            {
                BBS.Post post = new SagaDB.BBS.Post();
                post.Name = (string)i["name"];
                post.Title = (string)i["title"];
                post.Content = (string)i["content"];
                post.Date = (DateTime)i["postdate"];
                list.Add(post);
            }

            return list;
        }

        public List<BBS.Post> GetBBSPage(uint bbsID, int page)
        {
            string sqlstr = string.Format("SELECT * FROM `bbs` WHERE `bbsid`='{0}' ORDER BY `postdate` DESC LIMIT {1},5;", bbsID, (page - 1) * 5);
            List<BBS.Post> list = new List<SagaDB.BBS.Post>();
            DataRowCollection result = SQLExecuteQuery(sqlstr);

            foreach (DataRow i in result)
            {
                BBS.Post post = new SagaDB.BBS.Post();
                post.Name = (string)i["name"];
                post.Title = (string)i["title"];
                post.Content = (string)i["content"];
                post.Date = (DateTime)i["postdate"];
                list.Add(post);
            }

            return list;
        }

        public List<BBS.Mail> GetMail(ActorPC pc)
        {
            string sqlstr = string.Format("SELECT * FROM `mails` WHERE `char_id`='{0}' ORDER BY `postdate` DESC;", pc.CharID);
            List<BBS.Mail> list = new List<SagaDB.BBS.Mail>();
            DataRowCollection result = SQLExecuteQuery(sqlstr);

            foreach (DataRow i in result)
            {
                BBS.Mail post = new SagaDB.BBS.Mail();
                post.MailID = (uint)i["mail_id"];
                post.Name = (string)i["sender"];
                post.Title = (string)i["title"];
                post.Content = (string)i["content"];
                post.Date = (DateTime)i["postdate"];
                list.Add(post);
            }
            pc.Mails = list;
            return list;
        }
        public bool DeleteGift(BBS.Gift gift)
        {
            string sqlstr = "DELETE FROM `gifts` WHERE mail_id='" + gift.MailID + "';";
            try
            {
                return SQLExecuteNonQuery(sqlstr);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                return false;
            }
        }

        public bool DeleteMail(BBS.Mail mail)
        {
            string sqlstr = "DELETE FROM `mails` WHERE mail_id='" + mail.MailID + "';";
            try
            {
                return SQLExecuteNonQuery(sqlstr);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
                return false;
            }
        }

        public List<BBS.Gift> GetGifts(ActorPC pc)
        {
            if (pc == null) return null;
            string sqlstr = string.Format("SELECT * FROM `gifts` WHERE `a_id`='{0}' ORDER BY `postdate` DESC;", pc.Account.AccountID);
            List<BBS.Gift> list = new List<SagaDB.BBS.Gift>();
            DataRowCollection result = SQLExecuteQuery(sqlstr);

            foreach (DataRow i in result)
            {
                BBS.Gift post = new SagaDB.BBS.Gift();
                post.MailID = (uint)i["mail_id"];
                post.AccountID = (uint)i["a_id"];
                post.Name = (string)i["sender"];
                post.Title = (string)i["title"];
                post.Date = (DateTime)i["postdate"];
                post.Items = new Dictionary<uint, ushort>();
                for (int y = 1; y < 11; y++)
                {
                    uint itemid = (uint)(i["itemid" + y.ToString()]);
                    ushort count = (ushort)(i["count" + y.ToString()]);
                    if (!post.Items.ContainsKey(itemid) && itemid != 0)
                        post.Items.Add(itemid, count);
                }
                list.Add(post);
            }
            pc.Gifts = list;
            return list;
        }

        public uint AddNewGift(BBS.Gift gift)
        {
            List<uint> ids = new List<uint>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            List<ushort> counts = new List<ushort>() { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            byte index = 0;
            foreach (var item in gift.Items.Keys)
            {
                if (item != 0)
                {
                    ids[index] = item;
                    counts[index] = gift.Items[item];
                }
                index++;
            }
            string sqlstr = string.Format("INSERT INTO `gifts`(`a_id`,`sender`,`postdate`,`title`" +
                ",`itemid1`,`itemid2`,`itemid3`,`itemid4`,`itemid5`,`itemid6`,`itemid7`,`itemid8`,`itemid9`,`itemid10`" +
                ",`count1`,`count2`,`count3`,`count4`,`count5`,`count6`,`count7`,`count8`,`count9`,`count10`) VALUES " +
        "('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}','{11}','{12}','{13}','{14}','{15}'" +
        ",'{16}','{17}','{18}','{19}','{20}','{21}','{22}','{23}');",
        gift.AccountID, gift.Name, ToSQLDateTime(DateTime.Now.ToUniversalTime()), gift.Title, ids[0]
        , ids[1], ids[2], ids[3], ids[4], ids[5], ids[6], ids[7], ids[8], ids[9], counts[0], counts[1], counts[2], counts[3], counts[4], counts[5]
        , counts[6], counts[7], counts[8], counts[9]);
            uint ID;
            SQLExecuteScalar(sqlstr, out ID);
            return ID;
        }
        public bool BBSNewPost(ActorPC poster, uint bbsID, string title, string content)
        {
            CheckSQLString(ref title);
            CheckSQLString(ref content);
            string sqlstr = string.Format("INSERT INTO `bbs`(`bbsid`,`postdate`,`charid`,`name`,`title`,`content`) VALUES " +
                    "('{0}','{1}','{2}','{3}','{4}','{5}');", bbsID, ToSQLDateTime(DateTime.Now.ToUniversalTime()), poster.CharID, poster.Name, title, content);
            return SQLExecuteNonQuery(sqlstr);
        }

        private void GetFGarden(ActorPC pc)
        {
            uint account = GetAccountID(pc);
            string sqlstr = string.Format("SELECT * FROM `fgarden` WHERE `account_id`='{0}' LIMIT 1;", account);
            DataRowCollection result = SQLExecuteQuery(sqlstr);
            if (result.Count > 0)
            {
                FGarden.FGarden garden = new SagaDB.FGarden.FGarden(pc);
                garden.ID = (uint)result[0]["fgarden_id"];
                garden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.FLYING_BASE] = (uint)result[0]["part1"];
                garden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.FLYING_SAIL] = (uint)result[0]["part2"];
                garden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.GARDEN_FLOOR] = (uint)result[0]["part3"];
                garden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.GARDEN_MODELHOUSE] = (uint)result[0]["part4"];
                garden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.HouseOutSideWall] = (uint)result[0]["part5"];
                garden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.HouseRoof] = (uint)result[0]["part6"];
                garden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.ROOM_FLOOR] = (uint)result[0]["part7"];
                garden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.ROOM_WALL] = (uint)result[0]["part8"];
                garden.Fuel = (uint)result[0]["fuel"];
                pc.FGarden = garden;
            }
            if (pc.FGarden == null)
                return;
            sqlstr = string.Format("SELECT * FROM `fgarden_furniture` WHERE `fgarden_id`='{0}';", pc.FGarden.ID);
            result = SQLExecuteQuery(sqlstr);
            foreach (DataRow i in result)
            {
                FGarden.FurniturePlace place = (SagaDB.FGarden.FurniturePlace)(byte)i["place"];
                Actor.ActorFurniture actor = new ActorFurniture();
                actor.ItemID = (uint)i["item_id"];
                actor.PictID = (uint)i["pict_id"];
                actor.X = (short)i["x"];
                actor.Y = (short)i["y"];
                actor.Z = (short)i["z"];
                actor.Xaxis = (short)i["xaxis"];
                actor.Yaxis = (short)i["yaxis"];
                actor.Zaxis = (short)i["zaxis"];
                //actor.Dir = (ushort)i["dir"];
                actor.Motion = (ushort)i["motion"];
                actor.Name = (string)i["name"];
                pc.FGarden.Furnitures[place].Add(actor);
            }
        }
        public uint GetFFRindID(uint ffid)
        {
            string sqlstr = string.Format("SELECT `ring_id` FROM `ff` WHERE `ff_id`='{0}' LIMIT 1;", ffid);
            DataRowCollection result = SQLExecuteQuery(sqlstr);
            if (result.Count > 0)
            {
                return (uint)result[0]["ring_id"];
            }
            else
                return 0;
        }
        public void GetFF(ActorPC pc)
        {
            if (pc.Ring != null)
            {
                string sqlstr = string.Format("SELECT * FROM `ff` WHERE `ff_id`='{0}' LIMIT 1;", pc.Ring.FF_ID);
                DataRowCollection result = SQLExecuteQuery(sqlstr);
                if (result.Count > 0)
                {
                    if (pc.Ring.FFarden == null)
                    {
                        pc.Ring.FFarden = new SagaDB.FFarden.FFarden();
                        pc.Ring.FFarden.ID = (uint)result[0]["ff_id"];
                        pc.Ring.FFarden.Name = (string)result[0]["name"];
                        pc.Ring.FFarden.RingID = (uint)result[0]["ring_id"];
                        pc.Ring.FFarden.ObMode = 3;
                        pc.Ring.FFarden.Content = (string)result[0]["content"];
                        pc.Ring.FFarden.Level = (uint)result[0]["level"];
                    }
                }
            }
        }
        public void GetFFurniture(SagaDB.Ring.Ring ring)
        {
            if (ring.FFarden == null)
                return;
            if (!ring.FFarden.Furnitures.ContainsKey(FFarden.FurniturePlace.GARDEN) || !ring.FFarden.Furnitures.ContainsKey(FFarden.FurniturePlace.ROOM))
            {
                ring.FFarden.Furnitures.Add(SagaDB.FFarden.FurniturePlace.GARDEN, new List<ActorFurniture>());
                ring.FFarden.Furnitures.Add(SagaDB.FFarden.FurniturePlace.ROOM, new List<ActorFurniture>());
                ring.FFarden.Furnitures.Add(SagaDB.FFarden.FurniturePlace.FARM, new List<ActorFurniture>());
                ring.FFarden.Furnitures.Add(SagaDB.FFarden.FurniturePlace.FISHERY, new List<ActorFurniture>());
                ring.FFarden.Furnitures.Add(SagaDB.FFarden.FurniturePlace.HOUSE, new List<ActorFurniture>());
                string sqlstr = string.Format("SELECT * FROM `ff_furniture` WHERE `ff_id`='{0}';", ring.FFarden.ID);
                DataRowCollection result = SQLExecuteQuery(sqlstr);
                foreach (DataRow i in result)
                {
                    FFarden.FurniturePlace place = (SagaDB.FFarden.FurniturePlace)(byte)i["place"];
                    Actor.ActorFurniture actor = new ActorFurniture();
                    actor.ItemID = (uint)i["item_id"];
                    actor.PictID = (uint)i["pict_id"];
                    actor.X = (short)i["x"];
                    actor.Y = (short)i["y"];
                    actor.Z = (short)i["z"];
                    actor.Xaxis = (short)i["xaxis"];
                    actor.Yaxis = (short)i["yaxis"];
                    actor.Zaxis = (short)i["zaxis"];
                    actor.Motion = (ushort)i["motion"];
                    actor.Name = (string)i["name"];
                    actor.invisble = false;
                    ring.FFarden.Furnitures[place].Add(actor);
                }
            }
        }
        public void GetFFurnitureCopy(Dictionary<SagaDB.FFarden.FurniturePlace, List<ActorFurniture>> Furnitures)
        {
            Furnitures.Add(SagaDB.FFarden.FurniturePlace.GARDEN, new List<ActorFurniture>());
            Furnitures.Add(SagaDB.FFarden.FurniturePlace.ROOM, new List<ActorFurniture>());
            Furnitures.Add(SagaDB.FFarden.FurniturePlace.FARM, new List<ActorFurniture>());
            Furnitures.Add(SagaDB.FFarden.FurniturePlace.FISHERY, new List<ActorFurniture>());
            Furnitures.Add(SagaDB.FFarden.FurniturePlace.HOUSE, new List<ActorFurniture>());
            string sqlstr = string.Format("SELECT * FROM `ff_furniture_copy` WHERE `ff_id`='3';");
            DataRowCollection result = SQLExecuteQuery(sqlstr);
            foreach (DataRow i in result)
            {
                FFarden.FurniturePlace place = (SagaDB.FFarden.FurniturePlace)(byte)i["place"];
                Actor.ActorFurniture actor = new ActorFurniture();
                actor.ItemID = (uint)i["item_id"];
                actor.PictID = (uint)i["pict_id"];
                actor.X = (short)i["x"];
                actor.Y = (short)i["y"];
                actor.Z = (short)i["z"];
                actor.Xaxis = (short)i["xaxis"];
                actor.Yaxis = (short)i["yaxis"];
                actor.Zaxis = (short)i["zaxis"];
                actor.Motion = (ushort)i["motion"];
                actor.Name = (string)i["name"];
                actor.invisble = false;
                Furnitures[place].Add(actor);
            }
        }
        public List<SagaDB.FFarden.FFarden> GetFFList()
        {
            string sqlstr = string.Format("SELECT * FROM `ff`;");
            List<SagaDB.FFarden.FFarden> list = new List<SagaDB.FFarden.FFarden>();
            DataRowCollection result = SQLExecuteQuery(sqlstr);

            foreach (DataRow i in result)
            {
                SagaDB.FFarden.FFarden ff = new SagaDB.FFarden.FFarden();
                ff.Name = (string)i["name"];
                ff.Content = (string)i["content"];
                ff.ID = (uint)i["ff_id"];
                ff.RingID = (uint)i["ring_id"];
                ff.Level = (uint)i["level"];
                list.Add(ff);
            }

            return list;
        }
        public void SaveFFCopy(Dictionary<SagaDB.FFarden.FurniturePlace, List<ActorFurniture>> Furnitures)
        {
            //uint account = GetAccountID(pc);
            string sqlstr;
            sqlstr = string.Format("DELETE FROM `ff_furniture_copy` WHERE `ff_id`='3';");
            if (Furnitures.ContainsKey(FFarden.FurniturePlace.GARDEN))
            {
                foreach (ActorFurniture i in Furnitures[SagaDB.FFarden.FurniturePlace.GARDEN])
                {
                    sqlstr += string.Format("INSERT INTO `ff_furniture_copy`(`ff_id`,`place`,`item_id`,`pict_id`,`x`,`y`," +
                       "`z`,`xaxis`,`yaxis`,`zaxis`,`motion`,`name`) VALUES ('{0}','0','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}');",
                       3, i.ItemID, i.PictID, i.X, i.Y, i.Z, i.Xaxis, i.Yaxis, i.Zaxis, i.Motion, i.Name);
                }
            }
            if (Furnitures.ContainsKey(FFarden.FurniturePlace.ROOM))
            {
                foreach (ActorFurniture i in Furnitures[SagaDB.FFarden.FurniturePlace.ROOM])
                {
                    sqlstr += string.Format("INSERT INTO `ff_furniture_copy`(`ff_id`,`place`,`item_id`,`pict_id`,`x`,`y`," +
                       "`z`,`xaxis`,`yaxis`,`zaxis`,`motion`,`name`) VALUES ('{0}','1','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}');",
                      3, i.ItemID, i.PictID, i.X, i.Y, i.Z, i.Xaxis, i.Yaxis, i.Zaxis, i.Motion, i.Name);
                }
            }
            if (Furnitures.ContainsKey(FFarden.FurniturePlace.FARM))
            {
                foreach (ActorFurniture i in Furnitures[SagaDB.FFarden.FurniturePlace.FARM])
                {
                    sqlstr += string.Format("INSERT INTO `ff_furniture_copy`(`ff_id`,`place`,`item_id`,`pict_id`,`x`,`y`," +
                       "`z`,`xaxis`,`yaxis`,`zaxis`,`motion`,`name`) VALUES ('{0}','2','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}');",
                      3, i.ItemID, i.PictID, i.X, i.Y, i.Z, i.Xaxis, i.Yaxis, i.Zaxis, i.Motion, i.Name);
                }
            }
            if (Furnitures.ContainsKey(FFarden.FurniturePlace.FISHERY))
            {
                foreach (ActorFurniture i in Furnitures[SagaDB.FFarden.FurniturePlace.FISHERY])
                {
                    sqlstr += string.Format("INSERT INTO `ff_furniture_copy`(`ff_id`,`place`,`item_id`,`pict_id`,`x`,`y`," +
                       "`z`,`xaxis`,`yaxis`,`zaxis`,`motion`,`name`) VALUES ('{0}','3','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}');",
                      3, i.ItemID, i.PictID, i.X, i.Y, i.Z, i.Xaxis, i.Yaxis, i.Zaxis, i.Motion, i.Name);
                }
            }
            if (Furnitures.ContainsKey(FFarden.FurniturePlace.HOUSE))
            {
                foreach (ActorFurniture i in Furnitures[SagaDB.FFarden.FurniturePlace.HOUSE])
                {
                    sqlstr += string.Format("INSERT INTO `ff_furniture_copy`(`ff_id`,`place`,`item_id`,`pict_id`,`x`,`y`," +
                       "`z`,`xaxis`,`yaxis`,`zaxis`,`motion`,`name`) VALUES ('{0}','4','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}');",
                      3, i.ItemID, i.PictID, i.X, i.Y, i.Z, i.Xaxis, i.Yaxis, i.Zaxis, i.Motion, i.Name);
                }
            }
            SQLExecuteNonQuery(sqlstr);
        }
        public void GetPaper(ActorPC pc)
        {
            string sqlstr = string.Format("SELECT * FROM `another_paper` WHERE `char_id`='{0}';", pc.CharID);
            DataRowCollection result = SQLExecuteQuery(sqlstr);
            foreach (DataRow i in result)
            {
                uint paperid = (uint)i["paper_id"];
                AnotherDetail detail = new AnotherDetail();
                detail.value = new BitMask_Long();
                detail.value.Value = (ulong)i["paper_value"];
                detail.lv = (byte)i["paper_lv"];
                if (!pc.AnotherPapers.ContainsKey(paperid))
                    pc.AnotherPapers.Add(paperid, detail);
            }
        }
        public void SavePaper(ActorPC pc)
        {
            if (pc.AnotherPapers != null)
            {
                try
                {
                    string sqlstr = string.Format("DELETE FROM `another_paper` WHERE `char_id`='{0}';", pc.CharID);
                    foreach (var i in pc.AnotherPapers)
                    {
                        sqlstr += string.Format("INSERT INTO `another_paper`(`char_id`,`paper_id`,`paper_lv`,`paper_value`)" +
                            "VALUES ('{0}','{1}','{2}','{3}');", pc.CharID, i.Key, i.Value.lv, i.Value.value.Value);
                    }
                    SQLExecuteNonQuery(sqlstr);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
        }
        public void SaveFF(Ring.Ring ring)
        {
            if (ring != null)
            {
                if (ring.FFarden == null) return;
                //uint account = GetAccountID(pc);
                string sqlstr;
                if (ring.FFarden.ID > 0)
                {
                    sqlstr = string.Format("UPDATE `ff` SET `level`='{0}',`content`='{1}',`name`='{2}' WHERE `ff_id`='{3}';", ring.FFarden.Level, ring.FFarden.Content, ring.Name, ring.FFarden.ID);
                    SQLExecuteNonQuery(sqlstr);
                }
                sqlstr = string.Format("DELETE FROM `ff_furniture` WHERE `ff_id`='{0}';", ring.FFarden.ID);
                if (ring.FFarden.Furnitures.ContainsKey(FFarden.FurniturePlace.GARDEN))
                {
                    foreach (ActorFurniture i in ring.FFarden.Furnitures[SagaDB.FFarden.FurniturePlace.GARDEN])
                    {
                        sqlstr += string.Format("INSERT INTO `ff_furniture`(`ff_id`,`place`,`item_id`,`pict_id`,`x`,`y`," +
                           "`z`,`xaxis`,`yaxis`,`zaxis`,`motion`,`name`) VALUES ('{0}','0','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}');",
                           ring.FFarden.ID, i.ItemID, i.PictID, i.X, i.Y, i.Z, i.Xaxis, i.Yaxis, i.Zaxis, i.Motion, i.Name);
                    }
                }
                if (ring.FFarden.Furnitures.ContainsKey(FFarden.FurniturePlace.ROOM))
                {
                    foreach (ActorFurniture i in ring.FFarden.Furnitures[SagaDB.FFarden.FurniturePlace.ROOM])
                    {
                        sqlstr += string.Format("INSERT INTO `ff_furniture`(`ff_id`,`place`,`item_id`,`pict_id`,`x`,`y`," +
                           "`z`,`xaxis`,`yaxis`,`zaxis`,`motion`,`name`) VALUES ('{0}','1','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}');",
                          ring.FFarden.ID, i.ItemID, i.PictID, i.X, i.Y, i.Z, i.Xaxis, i.Yaxis, i.Zaxis, i.Motion, i.Name);
                    }
                }
                if (ring.FFarden.Furnitures.ContainsKey(FFarden.FurniturePlace.FARM))
                {
                    foreach (ActorFurniture i in ring.FFarden.Furnitures[SagaDB.FFarden.FurniturePlace.FARM])
                    {
                        sqlstr += string.Format("INSERT INTO `ff_furniture`(`ff_id`,`place`,`item_id`,`pict_id`,`x`,`y`," +
                           "`z`,`xaxis`,`yaxis`,`zaxis`,`motion`,`name`) VALUES ('{0}','2','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}');",
                          ring.FFarden.ID, i.ItemID, i.PictID, i.X, i.Y, i.Z, i.Xaxis, i.Yaxis, i.Zaxis, i.Motion, i.Name);
                    }
                }
                if (ring.FFarden.Furnitures.ContainsKey(FFarden.FurniturePlace.FISHERY))
                {
                    foreach (ActorFurniture i in ring.FFarden.Furnitures[SagaDB.FFarden.FurniturePlace.FISHERY])
                    {
                        sqlstr += string.Format("INSERT INTO `ff_furniture`(`ff_id`,`place`,`item_id`,`pict_id`,`x`,`y`," +
                           "`z`,`xaxis`,`yaxis`,`zaxis`,`motion`,`name`) VALUES ('{0}','3','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}');",
                          ring.FFarden.ID, i.ItemID, i.PictID, i.X, i.Y, i.Z, i.Xaxis, i.Yaxis, i.Zaxis, i.Motion, i.Name);
                    }
                }
                if (ring.FFarden.Furnitures.ContainsKey(FFarden.FurniturePlace.HOUSE))
                {
                    foreach (ActorFurniture i in ring.FFarden.Furnitures[SagaDB.FFarden.FurniturePlace.HOUSE])
                    {
                        sqlstr += string.Format("INSERT INTO `ff_furniture`(`ff_id`,`place`,`item_id`,`pict_id`,`x`,`y`," +
                           "`z`,`xaxis`,`yaxis`,`zaxis`,`motion`,`name`) VALUES ('{0}','4','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}');",
                          ring.FFarden.ID, i.ItemID, i.PictID, i.X, i.Y, i.Z, i.Xaxis, i.Yaxis, i.Zaxis, i.Motion, i.Name);
                    }
                }
                SQLExecuteNonQuery(sqlstr);
            }
        }
        public void SaveSerFF(Server.Server ser)
        {
            string sqlstr = string.Format("DELETE FROM `ff_furniture` WHERE `ff_id`='{0}';", 99999);
            foreach (ActorFurniture i in ser.Furnitures[SagaDB.FFarden.FurniturePlace.GARDEN])
            {
                sqlstr += string.Format("INSERT INTO `ff_furniture`(`ff_id`,`place`,`item_id`,`pict_id`,`x`,`y`," +
                   "`z`,`xaxis`,`yaxis`,`zaxis`,`motion`,`name`) VALUES ('{0}','0','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}');",
                   99999, i.ItemID, i.PictID, i.X, i.Y, i.Z, i.Xaxis, i.Yaxis, i.Zaxis, i.Motion, i.Name);
            }
            foreach (ActorFurniture i in ser.Furnitures[SagaDB.FFarden.FurniturePlace.ROOM])
            {
                sqlstr += string.Format("INSERT INTO `ff_furniture`(`ff_id`,`place`,`item_id`,`pict_id`,`x`,`y`," +
                   "`z`,`xaxis`,`yaxis`,`zaxis`,`motion`,`name`) VALUES ('{0}','1','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}');",
                  99999, i.ItemID, i.PictID, i.X, i.Y, i.Z, i.Xaxis, i.Yaxis, i.Zaxis, i.Motion, i.Name);
            }
            foreach (ActorFurniture i in ser.Furnitures[SagaDB.FFarden.FurniturePlace.HOUSE])
            {
                sqlstr += string.Format("INSERT INTO `ff_furniture`(`ff_id`,`place`,`item_id`,`pict_id`,`x`,`y`," +
                   "`z`,`xaxis`,`yaxis`,`zaxis`,`motion`,`name`) VALUES ('{0}','4','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}');",
                  99999, i.ItemID, i.PictID, i.X, i.Y, i.Z, i.Xaxis, i.Yaxis, i.Zaxis, i.Motion, i.Name);
            }
            /*foreach (ActorFurniture i in ser.FurnituresofFG[SagaDB.FFarden.FurniturePlace.GARDEN])
            {
                sqlstr += string.Format("INSERT INTO `ff_furniture`(`ff_id`,`place`,`item_id`,`pict_id`,`x`,`y`," +
                   "`z`,`xaxis`,`yaxis`,`zaxis`,`motion`,`name`) VALUES ('{0}','5','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}');",
                  99999, i.ItemID, i.PictID, i.X, i.Y, i.Z, i.Xaxis, i.Yaxis, i.Zaxis, i.Motion, i.Name);
            }*/
            SQLExecuteNonQuery(sqlstr);

        }
        public void GetSerFFurniture(Server.Server ser)
        {
            if (!ser.Furnitures.ContainsKey(FFarden.FurniturePlace.GARDEN) || !ser.Furnitures.ContainsKey(FFarden.FurniturePlace.ROOM))
            {
                ser.Furnitures.Add(SagaDB.FFarden.FurniturePlace.GARDEN, new List<ActorFurniture>());
                ser.Furnitures.Add(SagaDB.FFarden.FurniturePlace.ROOM, new List<ActorFurniture>());
                ser.Furnitures.Add(SagaDB.FFarden.FurniturePlace.FARM, new List<ActorFurniture>());
                ser.Furnitures.Add(SagaDB.FFarden.FurniturePlace.FISHERY, new List<ActorFurniture>());
                ser.Furnitures.Add(SagaDB.FFarden.FurniturePlace.HOUSE, new List<ActorFurniture>());
                string sqlstr = string.Format("SELECT * FROM `ff_furniture` WHERE `ff_id`='{0}';", 99999);
                DataRowCollection result = SQLExecuteQuery(sqlstr);
                foreach (DataRow i in result)
                {
                    byte p = (byte)i["place"];
                    if (p < 5)
                    {
                        FFarden.FurniturePlace place = (SagaDB.FFarden.FurniturePlace)p;
                        Actor.ActorFurniture actor = new ActorFurniture();
                        actor.ItemID = (uint)i["item_id"];
                        actor.PictID = (uint)i["pict_id"];
                        actor.X = (short)i["x"];
                        actor.Y = (short)i["y"];
                        actor.Z = (short)i["z"];
                        actor.Xaxis = (short)i["xaxis"];
                        actor.Yaxis = (short)i["yaxis"];
                        actor.Zaxis = (short)i["zaxis"];
                        actor.Motion = (ushort)i["motion"];
                        actor.Name = (string)i["name"];
                        actor.invisble = false;
                        ser.Furnitures[place].Add(actor);
                    }
                    else
                    {
                        p -= 4;
                        FFarden.FurniturePlace place = (SagaDB.FFarden.FurniturePlace)p;
                        Actor.ActorFurniture actor = new ActorFurniture();
                        actor.ItemID = (uint)i["item_id"];
                        actor.PictID = (uint)i["pict_id"];
                        actor.X = (short)i["x"];
                        actor.Y = (short)i["y"];
                        actor.Z = (short)i["z"];
                        actor.Xaxis = (short)i["xaxis"];
                        actor.Yaxis = (short)i["yaxis"];
                        actor.Zaxis = (short)i["zaxis"];
                        actor.Motion = (ushort)i["motion"];
                        actor.Name = (string)i["name"];
                        actor.invisble = false;
                        ser.FurnituresofFG[place].Add(actor);
                    }
                }
            }
        }
        public void CreateFF(ActorPC pc)
        {
            if (pc.Ring.FFarden == null) return;
            uint account = GetAccountID(pc);
            string sqlstr;
            sqlstr = string.Format("INSERT INTO `ff`(`ring_id` ,`name`,`content`,`level`) VALUES ('{0}','{1}','{2}','{3}');", pc.Ring.ID, pc.Ring.FFarden.Name, pc.Ring.FFarden.Content, pc.Ring.FFarden.Level);
            uint id = 0;
            SQLExecuteScalar(sqlstr, out id);
            pc.Ring.FFarden.ID = id;
            pc.Ring.FF_ID = id;
            sqlstr = string.Format("UPDATE `ring` SET `ff_id`='{0}' WHERE `ring_id`='{1}' LIMIT 1;\r\n",
    pc.Ring.FF_ID, pc.Ring.ID);
            SQLExecuteNonQuery(sqlstr);
        }


        private void SaveFGarden(ActorPC pc)
        {
            if (pc.FGarden == null) return;
            uint account = GetAccountID(pc);
            string sqlstr;
            if (pc.FGarden.ID > 0)
            {
                sqlstr = string.Format("UPDATE `fgarden` SET `part1`='{0}',`part2`='{1}',`part3`='{2}',`part4`='{3}',`part5`='{4}'," +
                    "`part6`='{5}',`part7`='{6}',`part8`='{7}',`fuel`='{9}' WHERE `fgarden_id`='{8}';",
                    pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.FLYING_BASE],
                    pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.FLYING_SAIL],
                    pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.GARDEN_FLOOR],
                    pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.GARDEN_MODELHOUSE],
                    pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.HouseOutSideWall],
                    pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.HouseRoof],
                    pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.ROOM_FLOOR],
                    pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.ROOM_WALL],
                    pc.FGarden.ID,
                    pc.FGarden.Fuel);
                SQLExecuteNonQuery(sqlstr);
            }
            else
            {
                sqlstr = string.Format("INSERT INTO `fgarden`(`account_id`,`part1`,`part2`,`part3`,`part4`,`part5`," +
                   "`part6`,`part7`,`part8`,`fuel`) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}');",
                   account,
                   pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.FLYING_BASE],
                   pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.FLYING_SAIL],
                   pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.GARDEN_FLOOR],
                   pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.GARDEN_MODELHOUSE],
                   pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.HouseOutSideWall],
                   pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.HouseRoof],
                   pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.ROOM_FLOOR],
                   pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.ROOM_WALL],
                   pc.FGarden.Fuel);
                uint id = 0;
                SQLExecuteScalar(sqlstr, out id);
                pc.FGarden.ID = id;
            }

            sqlstr = string.Format("DELETE FROM `fgarden_furniture` WHERE `fgarden_id`='{0}';", pc.FGarden.ID);
            foreach (ActorFurniture i in pc.FGarden.Furnitures[SagaDB.FGarden.FurniturePlace.GARDEN])
            {
                sqlstr += string.Format("INSERT INTO `fgarden_furniture`(`fgarden_id`,`place`,`item_id`,`pict_id`,`x`,`y`," +
                   "`z`,`xaxis`,`yaxis`,`zaxis`,`motion`,`name`) VALUES ('{0}','0','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}');",
                   pc.FGarden.ID, i.ItemID, i.PictID, i.X, i.Y, i.Z, i.Xaxis, i.Yaxis, i.Zaxis, i.Motion, i.Name);
            }
            foreach (ActorFurniture i in pc.FGarden.Furnitures[SagaDB.FGarden.FurniturePlace.ROOM])
            {
                sqlstr += string.Format("INSERT INTO `fgarden_furniture`(`fgarden_id`,`place`,`item_id`,`pict_id`,`x`,`y`," +
                   "`z`,`xaxis`,`yaxis`,`zaxis`,`motion`,`name`) VALUES ('{0}','1','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}','{9}','{10}');",
                   pc.FGarden.ID, i.ItemID, i.PictID, i.X, i.Y, i.Z, i.Xaxis, i.Yaxis, i.Zaxis, i.Motion, i.Name);
            }
            SQLExecuteNonQuery(sqlstr);
        }

        public List<ActorPC> GetWRPRanking()
        {
            string sqlstr = "SELECT `char_id`,`name`,`lv`,`jlv3`,`job`,`wrp` FROM `char` ORDER BY `wrp` DESC LIMIT 100;";
            DataRowCollection result = SQLExecuteQuery(sqlstr);
            List<ActorPC> res = new List<ActorPC>();
            uint count = 1;
            foreach (DataRow i in result)
            {
                ActorPC pc = new ActorPC();
                pc.CharID = (uint)i["char_id"];
                pc.Name = (string)i["name"];
                pc.Level = (byte)i["lv"];
                pc.JobLevel3 = (byte)i["jlv3"];
                pc.Job = (PC_JOB)(byte)i["job"];
                pc.WRP = (int)i["wrp"];
                pc.WRPRanking = count;
                res.Add(pc);
                count++;
            }
            return res;
        }
        public void SavaLevelLimit()
        {
            SagaDB.LevelLimit.LevelLimit LL = SagaDB.LevelLimit.LevelLimit.Instance;
            try
            {
                string sqlstr = string.Format("UPDATE `levellimit` SET `NowLevelLimit`='{0}',`NextLevelLimit`='{1}',`SetNextUpLevel`='{2}',`SetNextUpDays`='{3}',`ReachTime`='{4}',`NextTime`='{5}'"
                    + ",`FirstPlayer`='{6}',`SecondPlayer`='{7}',`ThirdPlayer`='{8}',`FourthPlayer`='{9}',`FifthPlayer`='{10}',`LastTimeLevelLimit`='{11}',`IsLock`='{12}'"
                    , LL.NowLevelLimit, LL.NextLevelLimit, LL.SetNextUpLevelLimit, LL.SetNextUpDays, LL.ReachTime, LL.NextTime, LL.FirstPlayer, LL.SecondPlayer, LL.Thirdlayer
                    , LL.FourthPlayer, LL.FifthPlayer, LL.LastTimeLevelLimit, LL.IsLock);
                SQLExecuteNonQuery(sqlstr);
            }
            catch (Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
            }
        }
        public void GetLevelLimit()
        {
            string sqlstr = "SELECT `NowLevelLimit`,`NextLevelLimit`,`SetNextUpLevel`,`SetNextUpDays`,`ReachTime`,`NextTime`,`LastTimeLevelLimit`,`FirstPlayer`" +
            ",`SecondPlayer`,`ThirdPlayer`,`FourthPlayer`,`FifthPlayer`,`IsLock` FROM `levellimit`";
            SagaDB.LevelLimit.LevelLimit levellimit = SagaDB.LevelLimit.LevelLimit.Instance;
            DataRowCollection resule = SQLExecuteQuery(sqlstr);
            foreach (DataRow i in resule)
            {
                levellimit.NowLevelLimit = (uint)i["NowLevelLimit"];
                levellimit.NextLevelLimit = (uint)i["NextLevelLimit"];
                levellimit.SetNextUpLevelLimit = (uint)i["SetNextUpLevel"];
                levellimit.LastTimeLevelLimit = (uint)i["LastTimeLevelLimit"];
                levellimit.SetNextUpDays = (uint)i["SetNextUpDays"];
                levellimit.ReachTime = (DateTime)i["ReachTime"];
                levellimit.NextTime = (DateTime)i["NextTime"];
                levellimit.FirstPlayer = (uint)i["FirstPlayer"];
                levellimit.SecondPlayer = (uint)i["SecondPlayer"];
                levellimit.Thirdlayer = (uint)i["ThirdPlayer"];
                levellimit.FourthPlayer = (uint)i["FourthPlayer"];
                levellimit.FifthPlayer = (uint)i["FifthPlayer"];
                levellimit.IsLock = (byte)i["IsLock"];
            }
        }

        public void SaveStamp(ActorPC pc, StampGenre genre)
        {
            string sqlstr = $"SELECT * FROM `stamp` WHERE `stamp_id`='{(byte)genre}' AND `char_id`='{pc.CharID}' ";
            DataRowCollection result = SQLExecuteQuery(sqlstr);
            if (result.Count > 0)
                sqlstr = $"UPDATE `stamp` SET `value`='{pc.Stamp[genre].Value}' WHERE `char_id`='{pc.CharID}' AND `stamp_id`='{(byte)genre}'";
            else
                sqlstr = $"INSERT INTO `stamp`(`char_id`,`stamp_id`,`value`) VALUES ('{pc.CharID}','{(byte)genre}','{pc.Stamp[genre].Value}')";
            SQLExecuteNonQuery(sqlstr);
        }

        public void SaveStamps(ActorPC pc)
        {
            foreach (StampGenre genre in Enum.GetValues(typeof(StampGenre)))
                SaveStamp(pc, genre);
        }

        public void GetStamps(ActorPC pc)
        {
            string sqlstr = $"SELECT * FROM `stamp` WHERE `char_id`='{pc.CharID}' ";
            DataRowCollection ret = SQLExecuteQuery(sqlstr);
            for (int i = 0; i < ret.Count; i++)
            {
                var result = ret[i];
                pc.Stamp[(StampGenre)(byte)result["stamp_id"]].Value = (short)result["value"];
            }
        }

        public List<Tamaire.TamaireLending> GetTamaireLendings()
        {
            string sqlstr = "SELECT * FROM `tamairelending`;";
            DataRowCollection result = SQLExecuteQuery(sqlstr);
            List<Tamaire.TamaireLending> tamaireLendings = new List<Tamaire.TamaireLending>();
            for (int i = 0; i < result.Count; i++)
            {
                Tamaire.TamaireLending tamaireLending = new SagaDB.Tamaire.TamaireLending();
                tamaireLending.Lender = (uint)result[0]["char_id"];
                tamaireLending.Baselv = (byte)result[0]["baselv"];
                tamaireLending.JobType = (byte)result[0]["jobtype"];
                tamaireLending.PostDue = (DateTime)result[0]["postdue"];
                tamaireLending.Comment = (string)result[0]["comment"];
                for (byte j = 1; j <= 4; j++)
                {
                    uint renterid = (uint)result[0]["renter" + j.ToString()];
                    if (renterid != 0)
                        tamaireLending.Renters.Add(renterid);
                }
                tamaireLendings.Add(tamaireLending);
            }
            return tamaireLendings;
        }

        public void GetTamaireLending(ActorPC pc)
        {
            string sqlstr = $"SELECT * FROM `tamairelending` WHERE `char_id`='{pc.CharID}' LIMIT 1;";
            DataRowCollection result = SQLExecuteQuery(sqlstr);
            if (result.Count != 0)
            {
                if (pc.TamaireLending == null)
                    pc.TamaireLending = new SagaDB.Tamaire.TamaireLending();
                pc.TamaireLending.Lender = (uint)result[0]["char_id"];
                pc.TamaireLending.Comment = (string)result[0]["comment"];
                pc.TamaireLending.PostDue = (DateTime)result[0]["postdue"];
                pc.TamaireLending.JobType = (byte)result[0]["jobtype"];
                pc.TamaireLending.Baselv = (byte)result[0]["baselv"];
                for (byte j = 1; j <= 4; j++)
                {
                    uint renterid = (uint)result[0]["renter" + j.ToString()];
                    if (renterid != 0)
                        pc.TamaireLending.Renters.Add(renterid);
                }
            }
        }

        public void CreateTamaireLending(Tamaire.TamaireLending tamaireLending)
        {
            uint index = 0;
            string comment = tamaireLending.Comment;
            CheckSQLString(ref comment);
            string sqlstr = string.Format("INSERT INTO `tamairelending`(`char_id`,`jobtype`,`baselv`,`postdue`,`comment`,`renter1`,`renter2`,`renter3`,`renter4`) VALUES " +
                    "('{0}','{1}','{2}','{3}','{4}','0','0','0','0');", tamaireLending.Lender, tamaireLending.JobType, tamaireLending.Baselv, ToSQLDateTime(tamaireLending.PostDue), comment);
            SQLExecuteScalar(sqlstr, out index);
        }

        public void SaveTamaireLending(Tamaire.TamaireLending tamaireLending)
        {
            uint renter1, renter2, renter3, renter4;
            string comment = tamaireLending.Comment;
            CheckSQLString(ref comment);

            if (tamaireLending.Renters.Count > 0)
                renter1 = tamaireLending.Renters[0];
            else
                renter1 = 0;
            if (tamaireLending.Renters.Count > 1)
                renter2 = tamaireLending.Renters[1];
            else
                renter2 = 0;
            if (tamaireLending.Renters.Count > 2)
                renter3 = tamaireLending.Renters[2];
            else
                renter3 = 0;
            if (tamaireLending.Renters.Count > 3)
                renter4 = tamaireLending.Renters[3];
            else
                renter4 = 0;
            string sqlstr = string.Format("UPDATE `tamairelending` SET `postdue`='{1}',`comment`='{2}', `renter1`='{3}',`renter2`='{4}'" +
                ",`renter3`='{5}',`renter4`='{6}' WHERE `char_id`='{0}' LIMIT 1;",
                tamaireLending.Lender, ToSQLDateTime(tamaireLending.PostDue), comment, renter1, renter2, renter3, renter4);
            SQLExecuteNonQuery(sqlstr);
        }

        public void DeleteTamaireLending(Tamaire.TamaireLending tamaireLending)
        {
            string sqlstr = string.Format("DELETE FROM `tamairelending` WHERE `char_id`='{0}';", tamaireLending.Lender);
            SQLExecuteNonQuery(sqlstr);
        }

        public void GetTamaireRental(ActorPC pc)
        {
            string sqlstr = $"SELECT * FROM `tamairerental` WHERE `char_id`='{pc.CharID}' LIMIT 1;";
            DataRowCollection result = SQLExecuteQuery(sqlstr);
            if (result.Count != 0)
            {
                if (pc.TamaireRental == null)
                    pc.TamaireRental = new SagaDB.Tamaire.TamaireRental();
                pc.TamaireRental.Renter = (uint)result[0]["char_id"];
                pc.TamaireRental.RentDue = (DateTime)result[0]["rentdue"];
                pc.TamaireRental.CurrentLender = (uint)result[0]["currentlender"];
                pc.TamaireRental.LastLender = (uint)result[0]["lastlender"];
            }
        }

        public void CreateTamaireRental(Tamaire.TamaireRental tamaireRental)
        {
            uint index = 0;
            string sqlstr = string.Format("INSERT INTO `tamairerental`(`char_id`,`rentdue`,`currentlender`,`lastlender`) VALUES " +
                    "('{0}','{1}','{2}','{3}');", tamaireRental.Renter, ToSQLDateTime(tamaireRental.RentDue), tamaireRental.CurrentLender, tamaireRental.LastLender);
            SQLExecuteScalar(sqlstr, out index);
        }

        public void SaveTamaireRental(Tamaire.TamaireRental tamaireRental)
        {
            string sqlstr = string.Format("UPDATE `tamairerental` SET `rentdue`='{1}',`currentlender`='{2}',`lastlender`='{3}' WHERE `char_id`='{0}' LIMIT 1;",
                tamaireRental.Renter, ToSQLDateTime(tamaireRental.RentDue), tamaireRental.CurrentLender, tamaireRental.LastLender);
            SQLExecuteNonQuery(sqlstr);
        }

        public void DeleteTamaireRental(Tamaire.TamaireRental tamaireRental)
        {
            string sqlstr = string.Format("DELETE FROM `tamairerental` WHERE `char_id`='{0}';", tamaireRental.Renter);
            SQLExecuteNonQuery(sqlstr);
        }

        public void GetMosterGuide(ActorPC pc)
        {
            Dictionary<uint, bool> guide = new Dictionary<uint, bool>();
            string sqlstr = $"SELECT * FROM `mobstates` WHERE `char_id`='{pc.CharID}'";
            DataRowCollection results = SQLExecuteQuery(sqlstr);
            foreach (DataRow result in results)
            {
                //uint mobID = (uint)result["mob_id"];
                //bool state = (bool)result["state"];
                uint mobID = Convert.ToUInt32(result["mob_id"]);
                bool state = Convert.ToBoolean(result["state"]);

                if (SagaDB.Mob.MobFactory.Instance.Mobs.ContainsKey(mobID))
                    guide.Add(mobID, state);
            }
            pc.MosterGuide = guide;
        }

        public void SaveMosterGuide(ActorPC pc, uint mobID, bool state)
        {
            byte value = 0;
            if (state)
                value = 1;
            string sqlstr = $"SELECT * FROM `mobstates` WHERE `char_id`='{pc.CharID}' AND `mob_id`='{mobID}' ";
            DataRowCollection result = SQLExecuteQuery(sqlstr);
            if (result.Count > 0)
                sqlstr = $"UPDATE `mobstates` SET `state`='{value}' WHERE `char_id`='{pc.CharID}' AND `mob_id`='{mobID}'";
            else
                sqlstr = $"REPLACE INTO `mobstates` (`char_id`,`mob_id`,`state`) VALUES ('{pc.CharID}','{mobID}','{value}')";
            SQLExecuteNonQuery(sqlstr);
        }

        #region 副职相关

        public void GetDualJobInfo(ActorPC pc)
        {
            var sqlstr = $"select * from `dualjob` where `char_id` = '{pc.CharID}'";
            DataRowCollection result = SQLExecuteQuery(sqlstr);
            if (result.Count > 0)
            {
                pc.PlayerDualJobList = new Dictionary<byte, PlayerDualJobInfo>();
                foreach (DataRow item in result)
                {
                    if (pc.PlayerDualJobList.ContainsKey(byte.Parse(item["series_id"].ToString())))
                    {
                        pc.PlayerDualJobList[byte.Parse(item["series_id"].ToString())].DualJobID = byte.Parse(item["series_id"].ToString());
                        pc.PlayerDualJobList[byte.Parse(item["series_id"].ToString())].DualJobLevel = byte.Parse(item["level"].ToString());
                        pc.PlayerDualJobList[byte.Parse(item["series_id"].ToString())].DualJobExp = ulong.Parse(item["exp"].ToString());
                    }
                    else
                    {
                        PlayerDualJobInfo pi = new PlayerDualJobInfo();
                        pi.DualJobID = byte.Parse(item["series_id"].ToString());
                        pi.DualJobLevel = byte.Parse(item["level"].ToString());
                        pi.DualJobExp = ulong.Parse(item["exp"].ToString());
                        pc.PlayerDualJobList.Add(byte.Parse(item["series_id"].ToString()), pi);
                    }
                }
            }
            GetDualJobSkill(pc);
            //else
            //{
            //    pc.PlayerDualJobList = new Dictionary<byte, PlayerDualJobInfo>();
                
            //    var initstr = $"";
            //    for (byte i = 1; i <= 12; i++)
            //    {
            //        initstr += $"insert into `dualjob` values ('',{pc.CharID}, {i}, 1, 0);";
            //    }
            //    SQLExecuteNonQuery(initstr);
            //    GetDualJobInfo(pc);
            //}
        }

        public void SaveDualJobInfo(ActorPC pc, bool allinfo)
        {
            var dic = pc.PlayerDualJobList;
            var insertstr = $"delete from `dualjob` where `char_id` = {pc.CharID};";
            foreach (var item in dic.Keys)
            {
                insertstr += $"insert into `dualjob` values ('',{pc.CharID}, {dic[item].DualJobID},{dic[item].DualJobLevel}, {dic[item].DualJobExp});";
            }
            SQLExecuteNonQuery(insertstr);

            if (allinfo)
            {
                var delskillstr = $"delete from `dualjob_skill` where `char_id` = {pc.CharID} and `series_id` = {pc.DualJobID};";
                foreach (var item in pc.DualJobSkill)
                {
                    delskillstr += $"insert into `dualjob_skill` values ('',{pc.CharID}, {pc.DualJobID}, {item.ID}, {item.Level});";
                }
                SQLExecuteNonQuery(delskillstr);
            }
        }

        public void GetDualJobSkill(ActorPC pc)
        {
            var sqlstr = $"select * from `dualjob_skill` where `char_id` = '{pc.CharID}' and series_id={pc.DualJobID}";
            DataRowCollection result = SQLExecuteQuery(sqlstr);
            if (result.Count > 0)
            {
                pc.DualJobSkill = new List<Skill.Skill>();
                foreach (DataRow item in result)
                {
                    var id = uint.Parse(item["skill_id"].ToString());
                    var level = byte.Parse(item["skill_level"].ToString());
                    var s = Skill.SkillFactory.Instance.GetSkill(id, level);
                    if (s != null)
                        pc.DualJobSkill.Add(s);
                }
            }
            else
                pc.DualJobSkill = new List<Skill.Skill>();
        }

        public void SaveDualJobSkill(ActorPC pc)
        {
            var delskillstr = $"delete from `dualjob_skill` where `char_id` = {pc.CharID} and `series_id` = {pc.DualJobID};";
            foreach (var item in pc.DualJobSkill)
            {
                delskillstr += $"insert into `dualjob_skill` values ('',{pc.CharID}, {pc.DualJobID}, {item.ID}, {item.Level});";
            }
            SQLExecuteNonQuery(delskillstr);
        }

        #endregion
    }
}
