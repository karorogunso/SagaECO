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
                MySqlCommand cmd = new MySqlCommand(string.Format("INSERT INTO `inventory`(`char_id`,`data`) VALUES ('{0}',?data);\r\n",
                aChar.CharID));
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
                        cmd = new MySqlCommand(string.Format("INSERT INTO `warehouse`(`account_id`,`data`) VALUES ('{0}',?data);\r\n",
                        account_id));
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
                string MasterName = " ";
                if (ap.Owner != null)
                    MasterName = ap.Owner.Name;
                sqlstr = string.Format("UPDATE `partner` SET `pid`='{1}',`name`='{2}',`lv`='{3}',`tlv`='{4}',`rb`='{5}',`rank`='{6}',`perkspoints`='{7}'," +
                    "`hp`='{8}',`maxhp`='{9}',`mp`='{10}',`maxmp`='{11}',`sp`='{12}',`maxsp`='{13}',`perk0`='{14}',`perk1`='{15}',`perk2`='{16}',`perk3`='{17}'" +
                    ",`perk4`='{18}',`perk5`='{19}',`aimode`='{20}',`basicai1`='{21}',`basicai2`='{22}',`exp` = '{23}',`pictid` = '{24}',`nextfeedtime` = '{25}'" +
                 ", `reliabilityuprate`='{26}',`texp`='{27}',`mastername`='{28}' WHERE apid='{0}' LIMIT 1",
                    apid, ap.partnerid, ap.Name, ap.Level, ap.reliability, rb, ap.rank, ap.perkpoint, ap.HP, ap.MaxHP, ap.MP, ap.MaxMP, ap.SP, ap.MaxSP,
                    ap.perk0, ap.perk1, ap.perk2, ap.perk3, ap.perk4, ap.perk5, ap.ai_mode, ap.basic_ai_mode, ap.basic_ai_mode_2, ap.exp, ap.PictID,
                    ap.nextfeedtime, ap.reliabilityuprate, ap.reliabilityexp, MasterName);
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
                else ap.rebirth = true;
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
                    if ((byte)i["type"] == 1)
                        ap.equipments[Partner.EnumPartnerEquipSlot.COSTUME] = item;
                    if ((byte)i["type"] == 2)
                        ap.equipments[Partner.EnumPartnerEquipSlot.WEAPON] = item;
                    if ((byte)i["type"] == 3)
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
            if (aChar.TInt["大逃杀模式"] == 1)
                return;
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
                if (aChar.Account.AccountID <= 247)
                {
                    if (aChar.MapID != 30201002)
                    {
                        mapid = 30201002;
                        info = Map.MapInfoFactory.Instance.MapInfo[30201002];
                        x = (byte)SagaLib.Global.Random.Next(3, 9);
                        y = (byte)SagaLib.Global.Random.Next(4, 11);
                    }
                }
                else
                {
                    if (aChar.MapID != 10054000)
                    {
                        mapid = 10054000;
                        info = Map.MapInfoFactory.Instance.MapInfo[10054000];
                        x = (byte)SagaLib.Global.Random.Next(196, 198);
                        y = (byte)SagaLib.Global.Random.Next(164, 166);
                    }
                }
                bool online = aChar.Online;
                byte dreserve = 0;
                if (aChar.DominionReserveSkill)
                    dreserve = 1;
                aChar.Online = false;
                sqlstr = string.Format("UPDATE `char` SET `name`='{0}',`race`='{1}',`gender`='{2}',`hairStyle`='{3}',`hairColor`='{4}',`wig`='{5}'," +
                     "`face`='{6}',`job`='{7}',`mapID`='{8}',`lv`='{9}',`jlv1`='{10}',`jlv2x`='{11}',`jlv2t`='{12}',`questRemaining`='0',`slot`='{14}'" +
                     ",`x`='{16}',`y`='{17}',`dir`='{18}',`hp`='{19}',`max_hp`='{20}',`mp`='{21}'," +
                    "`max_mp`='{22}',`sp`='{23}',`max_sp`='{24}',`str`='{25}',`dex`='{26}',`intel`='{27}',`vit`='{28}',`agi`='{29}',`mag`='{30}'," +
                    "`statspoint`='{31}',`skillpoint`='{32}',`skillpoint2x`='{33}',`skillpoint2t`='{34}',`skillpoint3`='{93}',`gold`='{35}',`cexp`='{36}',`jexp`='{37}'," +
                    "`save_map`='{38}',`save_x`='{39}',`save_y`='{40}',`possession_target`='{41}',`questid`='{42}',`questendtime`='{43}'" +
                    ",`queststatus`='{44}',`questcurrentcount1`='{45}',`questcurrentcount2`='{46}',`questcurrentcount3`='{47}'" +
                    ",`questresettime`='{48}',`fame`='{49}',`party`='{50}',`ring`='{51}',`golem`='{52}',`stamp1`='{53}'" +
                    ",`stamp2`='{54}',`stamp3`='{55}',`stamp4`='{56}',`stamp5`='{57}',`stamp6`='{58}',`stamp7`='{59}',`stamp8`='{60}'" +
                    ",`stamp9`='{61}',`stamp10`='{62}',`stamp11`='{63}',`cp`='{64}',`ecoin`='{65}',`dominionlv`='{66}'," +
                    "`dominionjlv`='{67}',`jointjlv`='{68}',`dcexp`='{69}',`djexp`='{70}',`jjexp`='{71}',`wrp`='{72}'," +
                    "`dstr`='{73}',`ddex`='{74}',`dintel`='{75}',`dvit`='{76}',`dagi`='{77}',`dmag`='{78}',`dstatpoint`='{79}'," +
                    "`dreserve`='{80}',`ep`='{81}',`eplogindate`='{82}',`epgreetingdate`='{83}',`cl`='{84}',`dcl`='{85}'," +
                    "`epused`='{86}',`depused`='{87}',`tailStyle`='{88}',`wingStyle`='{89}',`wingColor`='{90}',`lv1`='{91}',`jlv3`='{92}',`explorerEXP`='{93}',`usingpaper_id` = '"+ aChar.UsingPaperID.ToString()+"'" +
                    " WHERE char_id='{15}' LIMIT 1",

                    CheckSQLString(aChar.Name), (byte)aChar.Race, (byte)aChar.Gender, aChar.HairStyle, aChar.HairColor, aChar.Wig,
                    aChar.Face, (byte)aChar.Job, mapid, aChar.Level, aChar.JobLevel1, aChar.JobLevel2X, aChar.JobLevel2T,
                    aChar.QuestRemaining, aChar.Slot, aChar.CharID, x, y, (byte)(aChar.Dir / 45), aChar.HP, aChar.MaxHP, aChar.MP,
                    aChar.MaxMP, aChar.SP, aChar.MaxSP, aChar.Str, aChar.Dex, aChar.Int, aChar.Vit, aChar.Agi, aChar.Mag, aChar.StatsPoint,
                    aChar.SkillPoint, aChar.SkillPoint2X, aChar.SkillPoint2T, aChar.Gold, aChar.CEXP, aChar.JEXP, aChar.SaveMap, aChar.SaveX, aChar.SaveY,
                    aChar.PossessionTarget, questid, ToSQLDateTime(questtime), (byte)status, count1, count2, count3, ToSQLDateTime(aChar.QuestNextResetTime),
                    aChar.Fame, partyid, ringid, golemid, aChar.Stamp[StampGenre.Special].Value, aChar.Stamp[StampGenre.Pururu].Value
                    , aChar.Stamp[StampGenre.Field].Value, aChar.Stamp[StampGenre.Coast].Value, aChar.Stamp[StampGenre.Wild].Value
                    , aChar.Stamp[StampGenre.Cave].Value, aChar.Stamp[StampGenre.Snow].Value, aChar.Stamp[StampGenre.Colliery].Value
                    , aChar.Stamp[StampGenre.Northan].Value, aChar.Stamp[StampGenre.IronSouth].Value, aChar.Stamp[StampGenre.SouthDungeon].Value
                    , aChar.CP, aChar.ECoin, aChar.DominionLevel, aChar.DominionJobLevel, aChar.JointJobLevel, aChar.DominionCEXP, aChar.DominionJEXP
                    , aChar.JointJEXP, aChar.WRP, aChar.DominionStr, aChar.DominionDex, aChar.DominionInt, aChar.DominionVit, aChar.DominionAgi,
                    aChar.DominionMag, aChar.DominionStatsPoint, dreserve, aChar.EP, ToSQLDateTime(aChar.EPLoginTime), ToSQLDateTime(aChar.EPGreetingTime),
                    aChar.CL, aChar.DominionCL, aChar.EPUsed, aChar.DominionEPUsed, aChar.TailStyle, aChar.WingStyle, aChar.WingColor,
                    aChar.Level1, aChar.JobLevel3, aChar.SkillPoint3, aChar.ExplorerEXP,aChar.UsingPaperID);//aChar.DefLv,aChar.MDefLv,aChar.DefPoint,aChar.MDefPoint);
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

                    SaveNPCStates(aChar);
                }
                SaveQuestInfo(aChar);
                SaveBuffs(aChar);
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
                //pc.FirstName = (string)result["firstname"];
                //pc.ShowFirstName = (byte)result["showfirstname"];
                pc.Race = (PC_RACE)(byte)result["race"];
                pc.UsingPaperID = (ushort)result["usingpaper_id"];
                //pc.PlayerTitleID = (ushort)result["title_id"];
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
                //pc.JobLevel3 = (byte)result["jlv3"];
                pc.DominionLevel = (byte)result["dominionlv"];
                pc.DominionJobLevel = (byte)result["dominionjlv"];
                pc.JointJobLevel = (byte)result["jointjlv"];
                pc.DominionReserveSkill = ((byte)result["dreserve"] == 1);

                //pc.QuestRemaining = (ushort)result["questRemaining"];
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
                pc.MaxHP = (uint)result["max_hp"];
                pc.MaxMP = (uint)result["max_mp"];
                pc.MaxSP = (uint)result["max_sp"];
                pc.HP = (uint)result["hp"];
                pc.MP = (uint)result["mp"];
                pc.SP = (uint)result["sp"];
                pc.EP = (uint)result["ep"];
                pc.EPLoginTime = (DateTime)result["eplogindate"];
                pc.EPGreetingTime = (DateTime)result["epgreetingdate"];
                pc.EPUsed = (short)result["epused"];
                pc.DominionEPUsed = (short)result["depused"];
                pc.CL = (short)result["cl"];
                pc.DominionCL = (short)result["dcl"];
                pc.Str = (ushort)result["str"];
                pc.Dex = (ushort)result["dex"];
                pc.Int = (ushort)result["intel"];
                pc.Vit = (ushort)result["vit"];
                pc.Agi = (ushort)result["agi"];
                pc.Mag = (ushort)result["mag"];
                pc.DominionStr = (ushort)result["dstr"];
                pc.DominionDex = (ushort)result["ddex"];
                pc.DominionInt = (ushort)result["dintel"];
                pc.DominionVit = (ushort)result["dvit"];
                pc.DominionAgi = (ushort)result["dagi"];
                pc.DominionMag = (ushort)result["dmag"];
                pc.StatsPoint = (ushort)result["statspoint"];
                pc.DominionStatsPoint = (ushort)result["dstatpoint"];
                pc.SkillPoint = (ushort)result["skillpoint"];
                pc.SkillPoint2X = (ushort)result["skillpoint2x"];
                pc.SkillPoint2T = (ushort)result["skillpoint2t"];
                pc.SkillPoint3 = (ushort)result["skillpoint3"];
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
                pc.DominionCEXP = (ulong)result["dcexp"];
                pc.DominionJEXP = (ulong)result["djexp"];
                pc.JointJEXP = (ulong)result["jjexp"];
                pc.ExplorerEXP = (ulong)result["explorerEXP"];
                pc.WRP = (int)result["wrp"];
                pc.PossessionTarget = (uint)result["possession_target"];
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

                pc.Stamp[StampGenre.Special].Value = (short)result["stamp1"];
                pc.Stamp[StampGenre.Pururu].Value = (short)result["stamp2"];
                pc.Stamp[StampGenre.Field].Value = (short)result["stamp3"];
                pc.Stamp[StampGenre.Coast].Value = (short)result["stamp4"];
                pc.Stamp[StampGenre.Wild].Value = (short)result["stamp5"];
                pc.Stamp[StampGenre.Cave].Value = (short)result["stamp6"];
                pc.Stamp[StampGenre.Snow].Value = (short)result["stamp7"];
                pc.Stamp[StampGenre.Colliery].Value = (short)result["stamp8"];
                pc.Stamp[StampGenre.Northan].Value = (short)result["stamp9"];
                pc.Stamp[StampGenre.IronSouth].Value = (short)result["stamp10"];
                pc.Stamp[StampGenre.SouthDungeon].Value = (short)result["stamp11"];

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
                GetBuffs(pc);
                //GetGifts(pc);
                //GetMail(pc);
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

        public void GetBuffs(ActorPC pc)
        {
            string sqlstr = "SELECT * FROM `buffs` WHERE `charID`='" + pc.CharID + "';";

            DataRowCollection result = SQLExecuteQuery(sqlstr);
            foreach (DataRow i in result)
            {
                string addtionName = (string)i["AdditionName"];
                DateTime endtime = (DateTime)i["EndTime"];
                if (!pc.PendingAddtions.ContainsKey(addtionName))
                    pc.PendingAddtions.Add(addtionName, endtime);
            }
        }
        public void SaveBuffs(ActorPC pc)
        {
            string sqlstr = string.Format("DELETE FROM `buffs` WHERE `charID`='" + pc.CharID + "';");
            //sqlstr += "DELETE FROM `buffs` WHERE `EndTime` < '"+ DateTime.Now+"';";
            foreach (var i in pc.PendingAddtions)
            {
                if (i.Value > DateTime.Now)
                    sqlstr += string.Format("INSERT INTO `buffs`(`CharID`,`AdditionName`,`EndTime`) VALUES ('" + pc.CharID + "','" + i.Key + "','" + i.Value + "');");
            }
            SQLExecuteNonQuery(sqlstr);
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
            string sqlstr = string.Format("DELETE FROM `skill` WHERE `char_id`='{0}' AND `jobbasic`='{1}';", pc.CharID, (int)pc.JobBasic);
            MySqlCommand cmd = new MySqlCommand(string.Format("INSERT INTO `skill`(`char_id`,`skills`,`jobbasic`,`joblv`,`jobexp`,`skillpoint`,`skillpoint2x`," +
                "`skillpoint2t`,`skillpoint3`) VALUES ('{0}',?data,'{1}','{2}','{3}','{4}','{5}','{6}','{7}');",
                   pc.CharID, (int)pc.JobBasic, pc.JobLevel3, pc.JEXP, pc.SkillPoint, pc.SkillPoint2T, pc.SkillPoint2X, pc.SkillPoint3));
            cmd.Parameters.Add("?data", MySqlDbType.Blob).Value = ms.ToArray();
            ms.Close();
            try
            {
                SQLExecuteNonQuery(sqlstr);
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
                sqlstr += string.Format("INSERT INTO `sVar`(`name`,`type`,`content`) VALUES " +
                    "('{0}',0,'{1}');", i, fakepc.AStr[i]);
            }
            foreach (string i in fakepc.AInt.Keys)
            {
                sqlstr += string.Format("INSERT INTO `sVar`(`name`,`type`,`content`) VALUES " +
                   "('{0}',1,'{1}');", i, fakepc.AInt[i]);
            }
            foreach (string i in fakepc.AMask.Keys)
            {
                sqlstr += string.Format("INSERT INTO `sVar`(`name`,`type`,`content`) VALUES " +
                    "('{0}',2,'{1}');", i, fakepc.AMask[i].Value);
            }
            SQLExecuteNonQuery(sqlstr);
            /*
            sqlstr = "TRUNCATE TABLE `sList`;";
            sqlstr += "DELETE FROM `sList` WHERE `name` = '多开MAC限制';";
            sqlstr += "DELETE FROM `sList` WHERE `name` = '多开当日限制登录的账号';";
            foreach (var item in fakepc.Adict)
            {
                foreach (var i in item.Value.Keys)
                {
                    sqlstr += string.Format("INSERT INTO `sList`(`name`,`key`,`type`,`content`) VALUES " +
                     "('{0}','{1}',1,'{2}');", item.Key, i, item.Value[i]);
                }
            }
            SQLExecuteNonQuery(sqlstr);*/
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

                    if(pc.Account != null)
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
                if(result.Count>0)
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

                    Dictionary<uint, byte> skills = Skill.SkillFactory.Instance.CheckSkillList(pc, Skill.SkillFactory.SkillPaga.p1, true);
                    Dictionary<uint, byte> skills2x = Skill.SkillFactory.Instance.CheckSkillList(pc, Skill.SkillFactory.SkillPaga.p21, true);
                    Dictionary<uint, byte> skills2t = Skill.SkillFactory.Instance.CheckSkillList(pc, Skill.SkillFactory.SkillPaga.p22, true);
                    Dictionary<uint, byte> skills3 = Skill.SkillFactory.Instance.CheckSkillList(pc, Skill.SkillFactory.SkillPaga.p3, true);

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
                                pc.Skills2_1.Add(skill.ID, skill);
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
                                pc.Skills2_2.Add(skill.ID, skill);
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
                            if (!pc.SkillsEquip.ContainsKey(skill.ID))
                                pc.SkillsEquip.Add(skill.ID, skill);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Logger.ShowError(ex);
                return;
            }
        }

        public void SaveNPCStates(ActorPC pc)
        {
            System.IO.MemoryStream ms = new System.IO.MemoryStream();
            System.IO.BinaryWriter bw = new System.IO.BinaryWriter(ms);
            bw.Write(pc.NPCStates.Count);
            foreach (uint i in pc.NPCStates.Keys)
            {
                bw.Write(i);
                bw.Write(pc.NPCStates[i].Count);
                foreach (uint j in pc.NPCStates[i].Keys)
                {
                    bw.Write(j);
                    bw.Write(pc.NPCStates[i][j]);
                }
            }
            ms.Flush();

            MySqlCommand cmd = new MySqlCommand(string.Format("REPLACE INTO `npcstates`(`char_id`,`data`) VALUES ('{0}',?data);\r\n",
                    pc.CharID));
            cmd.Parameters.Add("?data", MySqlDbType.Blob).Value = ms.ToArray();
            try
            {
                SQLExecuteNonQuery(cmd);
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
            ms.Close();
        }

        void GetNPCStates(ActorPC pc)
        {
            string sqlstr;
            DataRowCollection result = null;
            uint account = GetAccountID(pc);
            sqlstr = "SELECT * FROM `npcstates` WHERE `char_id`='" + pc.CharID + "' LIMIT 1;";
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
                byte[] buf = (byte[])result[0]["data"];
                System.IO.MemoryStream ms = new System.IO.MemoryStream(buf);
                System.IO.BinaryReader br = new System.IO.BinaryReader(ms);
                int count = br.ReadInt32();
                for (int i = 0; i < count; i++)
                {
                    uint mapID = br.ReadUInt32();
                    pc.NPCStates.Add(mapID, new Dictionary<uint, bool>());
                    int count2 = br.ReadInt32();
                    for (int j = 0; j < count2; j++)
                    {
                        uint npcID = br.ReadUInt32();
                        bool value = br.ReadBoolean();
                        pc.NPCStates[mapID].Add(npcID, value);
                    }
                }
                ms.Close();
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
                            pc.Inventory.WareHouse = new IConcurrentDictionary<WarehousePlace, List<SagaDB.Item.Item>>();
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
                if (party.Leader == null)
                    return null;
                return party;
            }
            return null;
        }

        public void NewParty(Party.Party party)
        {
            uint index = 0;
            string name = party.Name;
            CheckSQLString(ref name);
            string sqlstr = string.Format("INSERT INTO `party`(`leader`,`name`,`member1`,`member2`,`member3`,`member4`,`member5`,`member6`,`member7`,`member8`) VALUES " +
                    "('0','{0}','0','0','0','0','0','0','0','0');", name);
            SQLExecuteScalar(sqlstr, out index);
            party.ID = index;
        }

        public void SaveParty(Party.Party party)
        {
            uint leader = party.Leader.CharID;
            uint member1, member2, member3, member4, member5, member6, member7, member8;
            string name = party.Name;
            CheckSQLString(ref name);

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
                leader, member1, member2, member3, member4, member5, member6, member7, member8, party.ID, name);
            SQLExecuteNonQuery(sqlstr);
        }

        public void DeleteParty(Party.Party party)
        {
            string sqlstr = string.Format("DELETE FROM `party` WHERE `party_id`='{0}';", party.ID);
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
                catch(Exception ex)
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
                    "`part6`='{5}',`part7`='{6}',`part8`='{7}' WHERE `fgarden_id`='{8}';", pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.FLYING_BASE],
                    pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.FLYING_SAIL], pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.GARDEN_FLOOR],
                    pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.GARDEN_MODELHOUSE],
                    pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.HouseOutSideWall],
                    pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.HouseRoof],
                    pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.ROOM_FLOOR],
                    pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.ROOM_WALL], pc.FGarden.ID);
                SQLExecuteNonQuery(sqlstr);
            }
            else
            {
                sqlstr = string.Format("INSERT INTO `fgarden`(`account_id`,`part1`,`part2`,`part3`,`part4`,`part5`," +
                   "`part6`,`part7`,`part8`) VALUES ('{0}','{1}','{2}','{3}','{4}','{5}','{6}','{7}','{8}');", account,
                   pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.FLYING_BASE],
                   pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.FLYING_SAIL],
                   pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.GARDEN_FLOOR],
                   pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.GARDEN_MODELHOUSE],
                   pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.HouseOutSideWall],
                   pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.HouseRoof],
                   pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.ROOM_FLOOR],
                   pc.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.ROOM_WALL]);
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
            string sqlstr = "SELECT `char_id`,`name`,`dominionlv`,`dominionjlv`,`job`,`wrp` FROM `char` ORDER BY `wrp` DESC LIMIT 100;";
            DataRowCollection result = SQLExecuteQuery(sqlstr);
            List<ActorPC> res = new List<ActorPC>();
            uint count = 1;
            foreach (DataRow i in result)
            {
                ActorPC pc = new ActorPC();
                pc.CharID = (uint)i["char_id"];
                pc.Name = (string)i["name"];
                pc.DominionLevel = (byte)i["dominionlv"];
                pc.DominionJobLevel = (byte)i["dominionjlv"];
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
    }
}
