using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaLib.VirtualFileSystem;
using SagaDB.Actor;

namespace SagaDB.Partner
{
    public class PartnerFactory : Singleton<PartnerFactory>
    {
        Dictionary<uint, PartnerData> partners_info = new Dictionary<uint, PartnerData>();
        Dictionary<uint, PartnerData> partners_db = new Dictionary<uint, PartnerData>();
        Dictionary<uint, PartnerFood> partnerfoods_db = new Dictionary<uint, PartnerFood>();
        Dictionary<uint, PartnerEquipment> partnerequips_db = new Dictionary<uint, PartnerEquipment>();
        Dictionary<uint, TalkInfo> actcubes_talks_db = new Dictionary<uint, TalkInfo>();
        Dictionary<uint, ActCubeData> actcubes_db_itemID = new Dictionary<uint, ActCubeData>();
        public Dictionary<ushort, ActCubeData> actcubes_db_uniqueID = new Dictionary<ushort, ActCubeData>();

        Dictionary<uint, List<PartnerMotion>> partner_motion_info = new Dictionary<uint, List<PartnerMotion>>();

        public List<uint> RankBPets = new List<uint>();
        public List<uint> RankAPets = new List<uint>();
        public List<uint> RankSPets = new List<uint>();
        public List<uint> RankSSPets = new List<uint>();
        public List<uint> RankSSSPets = new List<uint>();

        public List<uint> PartnerPictList = new List<uint>();
        public PartnerFactory()
        {
        }
        public void ClearPartnerEquips()
        {
            partnerequips_db.Clear();
        }

        public List<PartnerMotion> GetPartnerMotion(uint itemid)
        {
            if (!partner_motion_info.ContainsKey(itemid)) return null;
            return partner_motion_info[itemid];
        }
        public TalkInfo GetPartnerTalks(uint parterid)
        {
            if (!actcubes_talks_db.ContainsKey(parterid)) return null;
            return actcubes_talks_db[parterid];
        }
        public PartnerData GetPartnerInfo(uint partnerid) //original csv data
        {
            return this.partners_info[partnerid];
        }
        public PartnerData GetPartnerData(uint partnerid) //custom csv data
        {
            return this.partners_db[partnerid];
        }
        public PartnerFood GetPartnerFood(uint itemid)
        {
            return this.partnerfoods_db[itemid];
        }
        public PartnerEquipment GetPartnerEquip(uint itemid)
        {
            return this.partnerequips_db[itemid];
        }
        public ActCubeData GetCubeItemID(uint itemid)
        {
            return this.actcubes_db_itemID[itemid];
        }
        public ActCubeData GetCubeUniqueID(ushort uniqueid)
        {
            return this.actcubes_db_uniqueID[uniqueid];
        }
        public Dictionary<uint, PartnerData> Partners { get { return this.partners_db; } }

        public void InitPartnerInfo(string path,System.Text.Encoding encoding)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), encoding);
            int count = 0;
#if !Web
            string label = "Loading partner info";
            Logger.ProgressBarShow(0, (uint)sr.BaseStream.Length, label);
#endif
            DateTime time = DateTime.Now;
            string[] paras;
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();                    
                try
                {
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                        continue;
                    paras = line.Split(',');
                    for (int i = 0; i < paras.Length; i++)
                    {
                        if (paras[i] == "" || paras[i].ToLower() == "null")
                            paras[i] = "0";
                    }
                    PartnerData partner = new Partner.PartnerData();
                    partner.id = uint.Parse(paras[0]);
                    partner.name = paras[1];
                    partner.pictid = uint.Parse(paras[2]);
                    partner.partnerSize = float.Parse(paras[3]);
                    partner.motionsetnumber = ushort.Parse(paras[4]);
                    try
                    {
                        List<string> typeinfo=paras[6].Split('_').ToList();
                        partner.partnertypeid = ushort.Parse(typeinfo[typeinfo.Count - 1]);
                        typeinfo.RemoveAt(typeinfo.Count - 1);
                        string type = string.Join<string>("_", typeinfo);
                        partner.partnertype = (PartnerType)Enum.Parse(typeof(PartnerType), type);
                    }
                    catch (Exception)
                    {
                        while (paras[6].Substring(paras[6].Length - 1) != "_")
                        {
                            paras[6] = paras[6].Substring(0, paras[6].Length - 1);
                        }
                        paras[6] = paras[6].Substring(0, paras[6].Length - 1);
                        List<string> typeinfo = paras[6].Split('_').ToList();
                        partner.partnertypeid = ushort.Parse(typeinfo[typeinfo.Count - 1]);
                        typeinfo.RemoveAt(typeinfo.Count - 1);
                        string type = string.Join<string>("_", typeinfo);
                        partner.partnertype = (PartnerType)Enum.Parse(typeof(PartnerType), type);
                    }
                    partner.partnersystemid = ushort.Parse(paras[7]);
                    partner.fly = toBool(paras[8]);
                    partner.speed = ushort.Parse(paras[9]);
                    partner.attackType = (ATTACK_TYPE)Enum.Parse(typeof(ATTACK_TYPE), paras[10]);
                    partner.isrange = toBool(paras[11]);
                    partner.range = 1;//float.Parse(paras[12]);

                    if (partners_info.ContainsKey(partner.id))
                    {
                        Logger.ShowError("重复的PartnerID:" + partner.id.ToString() + ",[" + partners_info[partner.id].name + "]的已存在" + ",让[" + partner.name + "]没有被添加到Info。");
                    }
                    else
                    {
                        partners_info.Add(partner.id, partner);
                        count++;
                    }
#if !Web
                    if ((DateTime.Now - time).TotalMilliseconds > 40)
                    {
                        time = DateTime.Now;
                        Logger.ProgressBarShow((uint)sr.BaseStream.Position, (uint)sr.BaseStream.Length, label);
                    }
#endif                   
                }
                catch (Exception ex)
                {
#if !Web
                    Logger.ShowError("Error on parsing Partner Info!\r\nat line:" + line);
                    Logger.ShowError(ex);
#endif
                }
            }
#if !Web
            Logger.ProgressBarHide(count + " partners infos loaded.");
#endif
            sr.Close();
        }
        public void InitPartnerRankDB(string path, System.Text.Encoding encoding)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), encoding);
#if !Web
            string label = "Loading partner Rank database";
            Logger.ProgressBarShow(0, (uint)sr.BaseStream.Length, label);
#endif
            DateTime time = DateTime.Now;
            string[] paras;
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                try
                {
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                        continue;
                    paras = line.Split(',');
                    uint id = uint.Parse(paras[0]);
                    byte baserank = byte.Parse(paras[1]);
                    if (partners_db.ContainsKey(id))
                        partners_db[id].base_rank = baserank;
                    else
                        Logger.ShowError("不存在ID为" + id.ToString() + "的伙伴，设置初始RANK失败。");
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    Logger.ShowError(line);
                }
            }

            foreach (var item in Item.ItemFactory.Instance.Items.Values)
            {
                if (item.petID != 0)
                {
                    if (partners_db.ContainsKey(item.petID) && item.itemType == Item.ItemType.PARTNER)
                    {
                        uint baserank = partners_db[item.petID].base_rank;
                        uint itemid2 = item.id;

                        switch (baserank)
                        {
                            case 61:
                                if (!RankBPets.Contains(itemid2))
                                    RankBPets.Add(itemid2);
                                break;
                            case 71:
                                if (!RankAPets.Contains(itemid2))
                                    RankAPets.Add(itemid2);
                                break;
                            case 81:
                                if (!RankSPets.Contains(itemid2))
                                    RankSPets.Add(itemid2);
                                break;
                            case 91:
                                if (!RankSSPets.Contains(itemid2))
                                    RankSSPets.Add(itemid2);
                                break;
                            case 101:
                                if (!RankSSSPets.Contains(itemid2))
                                    RankSSSPets.Add(itemid2);
                                break;
                        }
                    }
                }
            }
        }
        public void InitPartnerTalksInfo(string path, System.Text.Encoding encoding)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), encoding);
            string label = "Loading Talks database";
            Logger.ProgressBarShow(0, (uint)sr.BaseStream.Length, label);
            DateTime time = DateTime.Now;
            string[] paras;
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                try
                {
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                        continue;
                    paras = line.Split(',');
                    uint id = uint.Parse(paras[0]);
                    TalkInfo ti;
                    if (!actcubes_talks_db.ContainsKey(id))
                        ti = new TalkInfo();
                    else
                        ti = actcubes_talks_db[id];
                    if (paras[1] != "")
                        ti.Onsummoned.Add(paras[1]);
                    if (paras[2] != "")
                        ti.OnBattle.Add(paras[2]);
                    if (paras[3] != "")
                        ti.OnMasterDead.Add(paras[3]);
                    if (paras[4] != "")
                        ti.OnNormal.Add(paras[4]);
                    if (paras[5] != "")
                        ti.OnJoinParty.Add(paras[5]);
                    if (paras[6] != "")
                        ti.OnLeaveParty.Add(paras[6]);
                    if (paras[7] != "")
                        ti.OnMasterFighting.Add(paras[7]);
                    if (paras[8] != "")
                        ti.OnMasterLevelUp.Add(paras[8]);
                    if (paras[9] != "")
                        ti.OnMasterQuit.Add(paras[9]);
                    if (paras[10] != "")
                        ti.OnMasterSit.Add(paras[10]);
                    if (paras[11] != "")
                        ti.OnMasterRelax.Add(paras[11]);
                    if (paras[12] != "")
                        ti.OnMasterBow.Add(paras[12]);
                    if (paras[13] != "")
                        ti.OnMasterLogin.Add(paras[13]);
                    if (paras[14] != "")
                        ti.OnLevelUp.Add(paras[14]);
                    if (paras[15] != "")
                        ti.OnEquip.Add(paras[15]);
                    if (paras[16] != "")
                        ti.OnEat.Add(paras[16]);
                    if (paras[17] != "")
                        ti.OnEatReady.Add(paras[17]);
                    if (!actcubes_talks_db.ContainsKey(id))
                        actcubes_talks_db.Add(id, ti);
                    else
                        actcubes_talks_db[id] = ti;
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                    Logger.ShowError(line);
                }
            }
        }
        public void InitPartnerPicts(string path, System.Text.Encoding encoding)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), encoding);
            string label = "Loading PartnerPict database";
            Logger.ProgressBarShow(0, (uint)sr.BaseStream.Length, label);
            DateTime time = DateTime.Now;
            string[] paras;
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                try
                {
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                        continue;
                    paras = line.Split(',');
                    uint pictid = uint.Parse(paras[0]);
                    if(!PartnerPictList.Contains(pictid))
                    {
                        PartnerPictList.Add(pictid);
                    }
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
        }

        public void InitPartnerMotions(string path, System.Text.Encoding encoding)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), encoding);
            string label = "Loading Talks database";
            Logger.ProgressBarShow(0, (uint)sr.BaseStream.Length, label);
            DateTime time = DateTime.Now;
            string[] paras;
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                try
                {
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                        continue;
                    paras = line.Split(',');
                    uint itemid = uint.Parse(paras[0]);
                    PartnerMotion pm = new PartnerMotion();
                    pm.ID = byte.Parse(paras[1]);
                    pm.MasterMotionID = uint.Parse(paras[6]);
                    pm.PartnerMotionID = uint.Parse(paras[4]);
                    if (!partner_motion_info.ContainsKey(itemid))
                        partner_motion_info.Add(itemid, new List<PartnerMotion>());
                    partner_motion_info[itemid].Add(pm);
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
        }
        public void InitPartnerDB(string path, System.Text.Encoding encoding)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), encoding);
            int count = 0;
#if !Web
            string label = "Loading partner database";
            Logger.ProgressBarShow(0, (uint)sr.BaseStream.Length, label);
#endif
            DateTime time = DateTime.Now;
            string[] paras;
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                try
                {
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                        continue;
                    paras = line.Split(',');
                    for (int i = 0; i < paras.Length; i++)
                    {
                        if (paras[i] == "" || paras[i].ToLower() == "null")
                            paras[i] = "0";
                    }
                    PartnerData partner = new Partner.PartnerData();
                    partner.id = uint.Parse(paras[0]);
                    partner.name = paras[1];
                    partner.pictid = uint.Parse(paras[2]);
                    partner.partnerSize = float.Parse(paras[3]);
                    partner.motionsetnumber = ushort.Parse(paras[4]);
                    try
                    {
                        List<string> typeinfo = paras[6].Split('_').ToList();
                        partner.partnertypeid = ushort.Parse(typeinfo[typeinfo.Count - 1]);
                        typeinfo.RemoveAt(typeinfo.Count - 1);
                        string type = string.Join<string>("_", typeinfo);
                        partner.partnertype = (PartnerType)Enum.Parse(typeof(PartnerType), type);
                    }
                    catch (Exception)
                    {
                        while (paras[6].Substring(paras[6].Length - 1) != "_")
                        {
                            paras[6] = paras[6].Substring(0, paras[6].Length - 1);
                        }
                        paras[6] = paras[6].Substring(0, paras[6].Length - 1);
                        List<string> typeinfo = paras[6].Split('_').ToList();
                        partner.partnertypeid = ushort.Parse(typeinfo[typeinfo.Count - 1]);
                        typeinfo.RemoveAt(typeinfo.Count - 1);
                        string type = string.Join<string>("_", typeinfo);
                        partner.partnertype = (PartnerType)Enum.Parse(typeof(PartnerType), type);
                    }
                    partner.partnersystemid = ushort.Parse(paras[7]);
                    partner.fly = toBool(paras[8]);
                    partner.speed = ushort.Parse(paras[9]);
                    partner.attackType = (ATTACK_TYPE)Enum.Parse(typeof(ATTACK_TYPE), paras[10]);
                    partner.isrange = toBool(paras[11]);
                    partner.range = float.Parse(paras[12]);
                    partner.level_in = 1;
                    partner.hp_in = 100;
                    partner.mp_in = 0;
                    partner.sp_in = 0;
                    partner.hp_fn = 1000;
                    partner.mp_fn = 0;
                    partner.sp_fn = 0;
                    partner.hp_in_re = 100;
                    partner.mp_in_re = 100;
                    partner.sp_in_re = 100;
                    partner.hp_fn_re = 500;
                    partner.mp_fn_re = 0;
                    partner.sp_fn_re = 0;
                    partner.hp_rec_in = 0;
                    partner.mp_rec_in = 0;
                    partner.sp_rec_in = 0;
                    partner.hp_rec_fn = 0;
                    partner.mp_rec_fn = 0;
                    partner.sp_rec_fn = 0;
                    partner.hp_rec_in_re = 0;
                    partner.mp_rec_in_re = 0;
                    partner.sp_rec_in_re = 0;
                    partner.hp_rec_fn_re = 0;
                    partner.mp_rec_fn_re = 0;
                    partner.sp_rec_fn_re = 0;
                    partner.atk_min_in = 1;
                    partner.atk_max_in = 1;
                    partner.atk_min_fn = 1;
                    partner.atk_max_fn = 1;
                    partner.atk_min_in_re = 1;
                    partner.atk_max_in_re = 1;
                    partner.atk_min_fn_re = 1;
                    partner.atk_max_fn_re = 1;
                    partner.matk_min_in = 1;
                    partner.matk_max_in = 1;
                    partner.matk_min_fn = 1;
                    partner.matk_max_fn = 1;
                    partner.matk_min_in_re = 1;
                    partner.matk_max_in_re = 1;
                    partner.matk_min_fn_re = 1;
                    partner.matk_max_fn_re = 1;
                    partner.def_in = 1;
                    partner.def_add_in = 1;
                    partner.def_fn = 1;
                    partner.def_add_fn = 1;
                    partner.def_in_re = 1;
                    partner.def_add_in_re = 1;
                    partner.def_fn_re = 1;
                    partner.def_add_fn_re = 1;
                    partner.mdef_in = 1;
                    partner.mdef_add_in = 1;
                    partner.mdef_fn = 1;
                    partner.mdef_add_fn = 1;
                    partner.mdef_in_re = 1;
                    partner.mdef_add_in_re = 1;
                    partner.mdef_fn_re = 1;
                    partner.mdef_add_fn_re = 1;
                    partner.hit_melee_in = 1;
                    partner.hit_ranged_in = 1;
                    partner.hit_magic_in = 1;
                    partner.hit_critical_in = 1;
                    partner.hit_melee_fn = 1;
                    partner.hit_ranged_fn = 1;
                    partner.hit_magic_fn = 1;
                    partner.hit_critical_fn = 1;
                    partner.hit_melee_in_re = 1;
                    partner.hit_ranged_in_re = 1;
                    partner.hit_magic_in_re = 1;
                    partner.hit_critical_in_re = 1;
                    partner.hit_melee_fn_re = 1;
                    partner.hit_ranged_fn_re = 1;
                    partner.hit_magic_fn_re = 1;
                    partner.hit_critical_fn_re = 1;
                    partner.aspd_in = 1;
                    partner.cspd_in = 1;
                    partner.aspd_fn = 1;
                    partner.cspd_fn = 1;
                    partner.aspd_in_re = 1;
                    partner.cspd_in_re = 1;
                    partner.aspd_fn_re = 1;
                    partner.cspd_fn_re = 1;

                    partner.aiMode = 0;

                    if (partners_db.ContainsKey(partner.id))
                    {
                        Logger.ShowError("重复的PartnerID:" + partner.id.ToString() + ",[" + partners_db[partner.id].name + "]的已存在" + ",让[" + partner.name + "]没有被添加到DB。");
                    }
                    else
                    {
                        partners_db.Add(partner.id, partner);
                        count++;
                    }
#if !Web
                    if ((DateTime.Now - time).TotalMilliseconds > 40)
                    {
                        time = DateTime.Now;
                        Logger.ProgressBarShow((uint)sr.BaseStream.Position, (uint)sr.BaseStream.Length, label);
                    }
#endif
                }
                catch (Exception ex)
                {
#if !Web
                    Logger.ShowError("Error on parsing Partner DB!\r\nat line:" + line);
                    Logger.ShowError(ex);
#endif
                }
            }
#if !Web
            Logger.ProgressBarHide(count + " partners loaded.");
#endif
            sr.Close();
        }

        public void InitPartnerFoodDB(string path, System.Text.Encoding encoding)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), encoding);
            int count = 0;
#if !Web
            string label = "Loading partner food database";
            Logger.ProgressBarShow(0, (uint)sr.BaseStream.Length, label);
#endif
            DateTime time = DateTime.Now;
            string[] paras;
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                try
                {
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                        continue;
                    paras = line.Split(',');
                    for (int i = 0; i < paras.Length; i++)
                    {
                        if (paras[i] == "" || paras[i].ToLower() == "null")
                            paras[i] = "0";
                    }
                    PartnerFood food = new PartnerFood();
                    food.itemID = uint.Parse(paras[0]);
                    food.partnerrank_min = byte.Parse(paras[1]);
                    food.partnerrank_max = byte.Parse(paras[2]);
                    food.systemID = byte.Parse(paras[3]);
                    food.nextfeedtime = 60 * uint.Parse(paras[4]);//from mins to seconds
                    food.rankexp = uint.Parse(paras[5]);
                    food.reliabilityuprate = ushort.Parse(paras[6]);

                    if (partnerfoods_db.ContainsKey(food.itemID))
                    {
                        Logger.ShowError("重复的PartnerFoodID:" + food.itemID.ToString() + ",已存在,没有被添加到DB。");
                    }
                    else
                    {
                        partnerfoods_db.Add(food.itemID, food);
                        count++;
                    }
#if !Web
                    if ((DateTime.Now - time).TotalMilliseconds > 40)
                    {
                        time = DateTime.Now;
                        Logger.ProgressBarShow((uint)sr.BaseStream.Position, (uint)sr.BaseStream.Length, label);
                    }
#endif
                }
                catch (Exception ex)
                {
#if !Web
                    Logger.ShowError("Error on parsing Partner Food DB!\r\nat line:" + line);
                    Logger.ShowError(ex);
#endif
                }
            }
#if !Web
            Logger.ProgressBarHide(count + " partner foods loaded.");
#endif
            sr.Close();
        }

        public void InitPartnerEquipDB(string path, System.Text.Encoding encoding)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), encoding);
            int count = 0;
#if !Web
            string label = "Loading partner equip database";
            Logger.ProgressBarShow(0, (uint)sr.BaseStream.Length, label);
#endif
            DateTime time = DateTime.Now;
            string[] paras;
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                try
                {
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                        continue;
                    paras = line.Split(',');
                    for (int i = 0; i < paras.Length; i++)
                    {
                        if (paras[i] == "" || paras[i].ToLower() == "null")
                            paras[i] = "0";
                    }
                    PartnerEquipment equip = new PartnerEquipment();
                    equip.itemID = uint.Parse(paras[0]);//[1] is item name
                    Item.Item item = Item.ItemFactory.Instance.GetItem(equip.itemID);

                    item.BaseData.hp = short.Parse(paras[2]);
                    item.BaseData.mp = short.Parse(paras[3]);
                    item.BaseData.sp = short.Parse(paras[4]);
                    item.BaseData.atk1 = short.Parse(paras[5]);
                    item.BaseData.atk2 = short.Parse(paras[5]);
                    item.BaseData.atk3 = short.Parse(paras[5]);
                    item.BaseData.matk = short.Parse(paras[6]);
                    item.BaseData.def = short.Parse(paras[7]);
                    item.BaseData.mdef = short.Parse(paras[7]);
                    item.BaseData.hitMelee = short.Parse(paras[9]);
                    item.BaseData.hitRanged = short.Parse(paras[10]);
                    item.BaseData.hitMagic = short.Parse(paras[11]);
                    item.BaseData.avoidMelee = short.Parse(paras[12]);
                    item.BaseData.avoidRanged = short.Parse(paras[13]);
                    item.BaseData.avoidMagic = short.Parse(paras[14]);
                    item.BaseData.hitCritical = short.Parse(paras[15]);
                    item.BaseData.avoidCritical = short.Parse(paras[16]);
                    
                    equip.hp_up = int.Parse(paras[2]);
                    equip.mp_up = int.Parse(paras[3]);
                    equip.sp_up = int.Parse(paras[4]);
                    equip.atk = short.Parse(paras[5]);
                    equip.matk = short.Parse(paras[6]);
                    equip.def = short.Parse(paras[7]);
                    equip.mdef = short.Parse(paras[8]);
                    equip.Shit = short.Parse(paras[9]);
                    equip.Lhit = short.Parse(paras[10]);
                    equip.Mhit = short.Parse(paras[11]);
                    equip.Savd = short.Parse(paras[12]);
                    equip.Lavd = short.Parse(paras[13]);
                    equip.Mavd = short.Parse(paras[14]);
                    equip.Chit = short.Parse(paras[15]);
                    equip.Cavd = short.Parse(paras[16]);
                    equip.hp_rec = int.Parse(paras[17]);
                    equip.mp_rec = int.Parse(paras[18]);
                    equip.sp_rec = int.Parse(paras[19]);
                    for (int i = 0; i < 7; i++)
                    {
                        equip.elements.Add((Elements)i, int.Parse(paras[20 + i]));
                    }
                    for (int i = 0; i < 9; i++)
                    {
                        equip.abnormalStatus.Add((AbnormalStatus)i, short.Parse(paras[27 + i]));
                    }
                    equip.partnerrank = byte.Parse(paras[36]);//[37]is attacktype null
                    equip.systemID = byte.Parse(paras[38]);

                    if (partnerequips_db.ContainsKey(equip.itemID))
                    {
                        Logger.ShowError("重复的PartnerEquipID:" + equip.itemID.ToString() + ",已存在,没有被添加到DB。");
                    }
                    else
                    {
                        partnerequips_db.Add(equip.itemID, equip);
                        count++;
                    }
#if !Web
                    if ((DateTime.Now - time).TotalMilliseconds > 40)
                    {
                        time = DateTime.Now;
                        Logger.ProgressBarShow((uint)sr.BaseStream.Position, (uint)sr.BaseStream.Length, label);
                    }
#endif
                }
                catch (Exception ex)
                {
#if !Web
                    Logger.ShowError("Error on parsing Partner Equip DB!\r\nat line:" + line);
                    Logger.ShowError(ex);
#endif
                }
            }
#if !Web
            Logger.ProgressBarHide(count + " partner equips loaded.");
#endif
            sr.Close();
        }

        public void InitActCubeDB(string path, System.Text.Encoding encoding)
        {
            System.IO.StreamReader sr = new System.IO.StreamReader(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path), encoding);
            int count_itemID = 0;
            int count_uniqueID = 0;
#if !Web
            string label = "Loading partner cube database";
            Logger.ProgressBarShow(0, (uint)sr.BaseStream.Length, label);
#endif
            DateTime time = DateTime.Now;
            string[] paras;
            while (!sr.EndOfStream)
            {
                string line;
                line = sr.ReadLine();
                try
                {
                    if (line == "") continue;
                    if (line.Substring(0, 1) == "#")
                        continue;
                    paras = line.Split(',');
                    for (int i = 0; i < paras.Length; i++)
                    {
                        if (paras[i] == "" || paras[i].ToLower() == "null")
                            paras[i] = "0";
                    }
                    ActCubeData cube = new ActCubeData();
                    cube.uniqueID = ushort.Parse(paras[0]);
                    cube.itemID = uint.Parse(paras[1]);
                    cube.cubetype = (PartnerCubeType)(int.Parse(paras[2])); //[3] is cube name
                    cube.cubename = paras[3];
                    cube.systemID = byte.Parse(paras[4]); //[5]-[15] is system related 0s
                    cube.reliability = byte.Parse(paras[16]);
                    cube.rebirth = byte.Parse(paras[17]); //[18] is description
                    cube.skillID = uint.Parse(paras[19]); //[20] is partner restriction
                    cube.actionID = ushort.Parse(paras[21]);
                    cube.parameter1 = uint.Parse(paras[22]);
                    cube.parameter2 = uint.Parse(paras[23]);
                    cube.parameter3 = uint.Parse(paras[24]); //[25] is unknown

                    if (actcubes_db_itemID.ContainsKey(cube.itemID))
                    {
                        Logger.ShowError("重复的PartnerCubeItemID:" + cube.itemID.ToString() + ",已存在,没有被添加到DB。");
                    }
                    else
                    {
                        actcubes_db_itemID.Add(cube.itemID, cube);
                        count_itemID++;
                    }
                    if (actcubes_db_uniqueID.ContainsKey(cube.uniqueID))
                    {
                        Logger.ShowError("重复的PartnerCubeUniqueID:" + cube.uniqueID.ToString() + ",已存在,没有被添加到DB。");
                    }
                    else
                    {
                        actcubes_db_uniqueID.Add(cube.uniqueID, cube);
                        count_uniqueID++;
                    }
#if !Web
                    if ((DateTime.Now - time).TotalMilliseconds > 40)
                    {
                        time = DateTime.Now;
                        Logger.ProgressBarShow((uint)sr.BaseStream.Position, (uint)sr.BaseStream.Length, label);
                    }
#endif
                }
                catch (Exception ex)
                {
#if !Web
                    Logger.ShowError("Error on parsing Partner Cube DB!\r\nat line:" + line);
                    Logger.ShowError(ex);
#endif
                }
            }
#if !Web
            Logger.ProgressBarHide(count_itemID+"/"+count_uniqueID + " partner cubes loaded.");
#endif
            sr.Close();
        }

        private bool toBool(string input)
        {
            if (input == "1") return true; else return false;
        }
    }
}
