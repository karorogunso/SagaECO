using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;

using SagaDB;
using SagaDB.Item;
using SagaDB.Actor;
using SagaDB.DefWar;
using SagaDB.Map;
using SagaDB.Title;
using SagaLib;
using SagaMap;
using SagaMap.Manager;
using SagaMap.PC;

namespace SagaMap.Network.Client
{
    public partial class MapClient
    {
        public DateTime moveStamp = DateTime.Now;
        public DateTime hpmpspStamp = DateTime.Now;
        public DateTime moveCheckStamp = DateTime.Now;

        //发送虚拟actor
        /// <summary>
        /// 设置称号完成进度，并检查
        /// </summary>
        /// <param name="pc">被设置称号的玩家</param>
        /// <param name="ID">称号ID</param>
        /// <param name="value">进度</param>
        public void SetTitleProccess(ActorPC pc, uint ID, uint value)
        {
            if (CheckTitle((int)ID)) return;
            if (TitleFactory.Instance.TitleList.ContainsKey(ID))
            {
                Title t = TitleFactory.Instance.TitleList[ID];
                string name = "称号" + ID.ToString() + "完成度";
                pc.AInt[name] = (int)value;
                if (pc.AInt[name] >= t.ConCount)
                    UnlockTitle(pc, ID);
            }
        }

        /// <summary>
        /// 增加称号完成进度，并检查
        /// </summary>
        /// <param name="pc">被设置称号的玩家</param>
        /// <param name="ID">称号ID</param>
        /// <param name="value">进度</param>
        /// <param name="needdead">是否需要死亡</param>
        public void TitleProccess(ActorPC pc, uint ID, uint value,bool needdead = false)
        {
            if (pc == null) return;
            if (pc.AInt == null) return;
            if (!needdead || (needdead && pc.Buff.Dead))
            {
                //检查服务器是否加载过指定ID的称号
                if (CheckTitle((int)ID)) return;
                if (TitleFactory.Instance.TitleList.ContainsKey(ID))
                {
                    //获取称号信息
                    Title t = TitleFactory.Instance.TitleList[ID];

                    //战斗称号主城不累积
                    if (t.typename == "战斗称号" && pc.MapID == 10054000)
                        return;

                    //推进度
                    string name = "称号" + ID.ToString() + "完成度";
                    pc.AInt[name] += (int)value;
                    //检查是否解锁
                    if (pc.AInt[name] >= t.ConCount)
                        UnlockTitle(pc, ID);
                }
            }
        }

        /// <summary>
        /// 解锁称号
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="ID">解锁称号ID</param>
        public void UnlockTitle(ActorPC pc, uint ID)
        {
            if (TitleFactory.Instance.TitleList.ContainsKey(ID))
            {
                Title t = TitleFactory.Instance.TitleList[ID];
                string name = "称号" + ID.ToString() + "完成度";
                if (Character.AInt[name] >= t.ConCount)
                {
                    if (!CheckTitle((int)ID))
                    {
                        SetTitle((int)ID, true);
                        SendSystemMessage("恭喜你！解锁了『" + t.name + "』称号！");
                        Skill.SkillHandler.Instance.ShowEffectOnActor(pc, 5420);
                    }
                }
            }
        }
        public void OnPlayerSetTitle(Packets.Client.CSMG_PLAYER_SETTITLE p)
        {
            if ((p.GetTSubID < 100000 || CheckTitle((int)p.GetTSubID)) && (p.GetTPredID < 100000 || CheckTitle((int)p.GetTPredID)) && (p.GetTBattleID < 100000 || CheckTitle((int)p.GetTBattleID)))
            {
                Character.AInt["称号_主语"] = (int)p.GetTSubID;
                Character.AInt["称号_连词"] = (int)p.GetTConjID;
                Character.AInt["称号_谓语"] = (int)p.GetTPredID;
                Character.AInt["称号_战斗"] = (int)p.GetTBattleID;
                StatusFactory.Instance.CalcStatus(Character);
                SendPCTitleInfo();
            }
            else
                SendSystemMessage("非法的称号ID");
        }

        public void OnPlayerOpenDailyStamp(Packets.Client.CSMG_DAILY_STAMP_OPEN p2)
        {
            Packets.Server.SSMG_NPC_DAILY_STAMP p = new Packets.Server.SSMG_NPC_DAILY_STAMP();
            p.StampCount = (byte)Character.AInt["每日盖章"];
            p.Type = 1;
            netIO.SendPacket(p);
        }
        public void OnPlayerTitleRequire(Packets.Client.CSMG_PLAYER_TITLE_REQUIRE p)
        {
            if(p.tID == 9 && Character.Gold >= 10000000)
                TitleProccess(Character, 9, 1);

            Packets.Server.SSMG_PLAYER_TITLE_REQ p2 = new Packets.Server.SSMG_PLAYER_TITLE_REQ();
            p2.tID = p.tID;
            p2.mark = 1;
            if (Character.AInt["称号" + p.tID.ToString() + "完成度"] != 0)
                p2.task = (ulong)Character.AInt["称号" + p.tID.ToString() + "完成度"];
            else p2.task = 0;
            netIO.SendPacket(p2);
        }
        public void OnPlayerCancleTitleNew(Packets.Client.CSMG_PLAYER_TITLE_CANCLENEW p)
        {
            int index = (int)p.tID;
            byte page = 1;
            bool bounsflag = false;
            if (index > 64)
            {
                page += (byte)(index / 64);
                index -= 64 * (page - 1);
            }
            BitMask_Long value = new BitMask_Long();
            string name = "N称号记录" + page;
            if (Character.AStr[name] == "")
                value.Value = 0;
            else
            {
                value.Value = ulong.Parse(Character.AStr[name]);
                if(value.Test((ulong)Math.Pow(2, (index - 1))))
                    bounsflag = true;
                value.SetValueForNum(index, false);
            }
            Character.AStr[name] = value.Value.ToString();

            if (bounsflag)
            {
                Title t = TitleFactory.Instance.TitleList[p.tID];
                foreach (var item in t.Bouns.Keys)
                {
                    Item it = ItemFactory.Instance.GetItem(item);
                    it.Stack = t.Bouns[item];
                    AddItem(it, true);
                }
                if (t.Bouns.Count > 0)
                    SendSystemMessage("获得了称号『" + t.name + "』的奖励！");
                else
                    SendSystemMessage("称号『" + t.name + "』没有物品奖励。");
            }
        }

        public bool CheckTitle(int ID)
        {
            if (ID > 100000) return true;
            int index = ID;
            byte page = 1;
            if (index > 64)
            {
                page += (byte)(index / 64);
                index -= 64 * (page - 1);
            }
            BitMask_Long value = new BitMask_Long();
            string name = "称号记录" + page;
            if (Character == null) return false;
            if (Character.AStr[name] == "")
                value.Value = 0;
            else
                value.Value = ulong.Parse(Character.AStr[name]);
            ulong mark = (ulong)Math.Pow(2, (index - 1));
            return value.Test(mark);
        }
        public void SendPCTitleInfo()
        {
            if (Character == null)
                return;
            Packets.Server.SSMG_PLAYER_TITLE p = new Packets.Server.SSMG_PLAYER_TITLE();
            p.TSubID = (uint)Character.AInt["称号_主语"];
            p.TConjID = (uint)Character.AInt["称号_连词"];
            p.TPredID = (uint)Character.AInt["称号_谓语"];
            p.TBallteID = (uint)Character.AInt["称号_战斗"];
            netIO.SendPacket(p);
            StatusFactory.Instance.CalcStatus(Character);
        }
        public void SendTitleList()
        {
            if (Character == null)
                return;
            Packets.Server.SSMG_PLAYER_TITLE_LIST p = new Packets.Server.SSMG_PLAYER_TITLE_LIST();
            ulong Page1Num = 0;
            if (Character.AStr["称号记录1"] != "")
                Page1Num = ulong.Parse(Character.AStr["称号记录1"]);
            ulong Page2Num = 0;
            if (Character.AStr["称号记录2"] != "")
                Page2Num = ulong.Parse(Character.AStr["称号记录2"]);
            ulong Page3Num = 0;
            if (Character.AStr["称号记录3"] != "")
                Page3Num = ulong.Parse(Character.AStr["称号记录3"]);
            ulong Page4Num = 0;
            if (Character.AStr["称号记录4"] != "")
                Page4Num = ulong.Parse(Character.AStr["称号记录4"]);
            ulong Page5Num = 0;
            if (Character.AStr["称号记录5"] != "")
                Page5Num = ulong.Parse(Character.AStr["称号记录5"]);
            p.Page1 = Page1Num;
            p.Page2 = Page2Num;
            p.Page3 = Page3Num;
            p.Page4 = Page4Num;
            p.Page5 = Page5Num;

            ulong nPage1Num = 0;
            if (Character.AStr["N称号记录1"] != "")
                nPage1Num = ulong.Parse(Character.AStr["N称号记录1"]);
            ulong nPage2Num = 0;
            if (Character.AStr["N称号记录2"] != "")
                nPage2Num = ulong.Parse(Character.AStr["N称号记录2"]);
            ulong nPage3Num = 0;
            if (Character.AStr["N称号记录3"] != "")
                nPage3Num = ulong.Parse(Character.AStr["N称号记录3"]);
            ulong nPage4Num = 0;
            if (Character.AStr["N称号记录4"] != "")
                nPage4Num = ulong.Parse(Character.AStr["N称号记录4"]);
            ulong nPage5Num = 0;
            if (Character.AStr["N称号记录5"] != "")
                nPage5Num = ulong.Parse(Character.AStr["N称号记录5"]);
            p.NPage1 = nPage1Num;
            p.NPage2 = nPage2Num;
            p.NPage3 = nPage3Num;
            p.NPage4 = nPage4Num;
            p.NPage5 = nPage5Num;
            netIO.SendPacket(p);
        }

        public void SetTitle(int n,bool v)
        {
            int index = n;
            BitMask_Long value = new BitMask_Long();
            byte page = 1;
            if (index > 64)
            {
                page += (byte)(index / 64);
                index -= 64 * (page - 1);
            }
            string name = "称号记录" + page;
            if (Character.AStr[name] == "")
                value.Value = 0;
            else
                value.Value = ulong.Parse(Character.AStr[name]);
            value.SetValueForNum(index, v);
            Character.AStr[name] = value.Value.ToString();


            name = "N" + name;
            value = new BitMask_Long();
            if (Character.AStr[name] == "")
                value.Value = 0;
            else
                value.Value = ulong.Parse(Character.AStr[name]);
            value.SetValueForNum(index, v);
            Character.AStr[name] = value.Value.ToString();

            SendTitleList();
        }

        public void SendPetInfo()
        {
            if (Character.Partner == null)
                    return;
            Partner.StatusFactory.Instance.CalcPartnerStatus(Character.Partner);
            SendPetDetailInfo();
            SendPetBasicInfo();
        }
        public void SendPetDetailInfo()
        {
            if (Character.Partner != null)
            {
                Packets.Server.SSMG_PARTNER_INFO_DETAIL p = new Packets.Server.SSMG_PARTNER_INFO_DETAIL();
                ActorPartner pet = Character.Partner;
                p.InventorySlot = Character.Inventory.Equipments[EnumEquipSlot.PET].Slot;
                p.MaxHP = pet.MaxHP;
                p.MaxMP = pet.MaxMP;
                p.MaxSP = pet.MaxSP;
                p.MoveSpeed = pet.Speed;
                p.MinPhyATK = pet.Status.min_atk1;
                p.MaxPhyATK = pet.Status.max_atk1;
                p.MinMAGATK = pet.Status.min_matk;
                p.MaxMAGATK = pet.Status.max_matk;
                p.DEF = pet.Status.def;
                p.DEFAdd = pet.Status.def_add;
                p.MDEF = pet.Status.mdef;
                p.MDEFAdd = pet.Status.mdef_add;
                p.ShortHit = pet.Status.hit_melee;
                p.LongHit = pet.Status.hit_ranged;
                p.ShortAvoid = pet.Status.avoid_melee;
                p.LongAvoid = pet.Status.avoid_ranged;
                p.ASPD = pet.Status.aspd;
                p.CSPD = pet.Status.cspd;
                netIO.SendPacket(p);
            }
        }
        public void SendPetBasicInfo()
        {
            if (Character.Partner != null)
            {
                Packets.Server.SSMG_PARTNER_INFO_BASIC p = new Packets.Server.SSMG_PARTNER_INFO_BASIC();
                ActorPartner pet = Character.Partner;
                p.InventorySlot = Character.Inventory.Equipments[EnumEquipSlot.PET].Slot;
                p.Level = pet.Level;

                ulong bexp = ExperienceManager.Instance.GetExpForLevel(pet.Level, Scripting.LevelType.CLEVEL2);
                ulong nextExp = ExperienceManager.Instance.GetExpForLevel((uint)(pet.Level + 1), Scripting.LevelType.CLEVEL2);
                uint cexp = (uint)((float)(pet.exp - bexp) / (nextExp - bexp) * 1000);

                p.EXPPercentage = (uint)cexp;
                p.Rebirth = 0;
                if (pet.rebirth)
                    p.Rebirth = 1;
                byte rank = (byte)(pet.BaseData.base_rank + pet.rank);
                p.Rank = rank;
                p.ReliabilityColor = pet.reliability;
                p.ReliabilityUpRate = pet.reliabilityuprate;

                if (pet.nextfeedtime > DateTime.Now)
                    p.NextFeedTime = (uint)(pet.nextfeedtime - DateTime.Now).TotalSeconds;
                else
                    p.NextFeedTime = 0;

                p.AIMode = pet.ai_mode;
                //p.MaxNextFeedTime = no data
                //p.CustomAISheet = no data
                //p.AICommandCount1 = no data
                //p.AICommandCount2 = no data
                p.PerkPoint = pet.perkpoint;
                //p.PerkListCount = no data
                p.Perk0 = pet.perk0;
                p.Perk1 = pet.perk1;
                p.Perk2 = pet.perk2;
                p.Perk3 = pet.perk3;
                p.Perk4 = pet.perk4;
                p.Perk5 = pet.perk5;
                if (pet.equipments.ContainsKey(SagaDB.Partner.EnumPartnerEquipSlot.WEAPON))
                    p.WeaponID = pet.equipments[SagaDB.Partner.EnumPartnerEquipSlot.WEAPON].ItemID;
                if (pet.equipments.ContainsKey(SagaDB.Partner.EnumPartnerEquipSlot.COSTUME))
                    p.ArmorID = pet.equipments[SagaDB.Partner.EnumPartnerEquipSlot.COSTUME].ItemID;
                netIO.SendPacket(p);
            }
        }
        public void OnAnoPaperEquip(Packets.Client.CSMG_ANO_PAPER_EQUIP p)
        {
            if (Character.AnotherPapers.ContainsKey(p.paperID))
            {
                Character.UsingPaperID = p.paperID;
                StatusFactory.Instance.CalcStatus(Character);
                Packets.Server.SSMG_ANO_EQUIP_RESULT p1 = new Packets.Server.SSMG_ANO_EQUIP_RESULT();
                p1.PaperID = Character.UsingPaperID;
                SendPlayerInfo();
                netIO.SendPacket(p1);
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.PAPER_CHANGE, null, Character, true);
            }

        }
        public void OnAnoPaperTakeOff(Packets.Client.CSMG_ANO_PAPER_TAKEOFF p)
        {
            Character.UsingPaperID =0;
            StatusFactory.Instance.CalcStatus(Character);
            Packets.Server.SSMG_ANO_TAKEOFF_RESULT p1 = new Packets.Server.SSMG_ANO_TAKEOFF_RESULT();
            p1.PaperID = Character.UsingPaperID;
            netIO.SendPacket(p1);
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.PAPER_CHANGE, null, Character, true);
            SendPlayerInfo();
        }
        public void CreateAnotherPaper(uint paperID)
        {
            AnotherDetail detail = new AnotherDetail();
            detail.value = new BitMask_Long();
            detail.lv = 0;
            foreach (var item in AnotherFactory.Instance.AnotherPapers[paperID].Keys)
            {
                if (!detail.skills.ContainsKey(item))
                    detail.skills.Add(item, 0);
            }
            if(!Character.AnotherPapers.ContainsKey(paperID))
                Character.AnotherPapers.Add(paperID,detail);
        }
        public void OnAnoPaperCompound(Packets.Client.CSMG_ANO_PAPER_COMPOUND p)
        {
            Item penItem = Character.Inventory.GetItem(p.SlotID);
            byte paperID = p.paperID;
            byte lv = (byte)(Character.AnotherPapers[paperID].lv + 1);
            ulong value = (ulong)(0xff << 8 * (lv-1));
            if (lv == 1) value = 0xff;
            if (Character.AnotherPapers[paperID].value.Test(value))
            {
                if(lv > 1)
                {
                    uint penID = 0;
                    if (AnotherFactory.Instance.AnotherPapers[paperID][lv].requestItem2 == penItem.ItemID)
                        penID = penItem.ItemID;
                    else if (AnotherFactory.Instance.AnotherPapers[paperID][lv].requestItem1 == penItem.ItemID)
                        penID = penItem.ItemID;
                    else return;
                    if (CountItem(penID) < 1) return;
                    DeleteItemID(penID, 1, true);
                }
                Character.AnotherPapers[paperID].lv = lv;
                Packets.Server.SSMG_ANO_PAPER_COMPOUND_RESULT p2 = new Packets.Server.SSMG_ANO_PAPER_COMPOUND_RESULT();
                p2.lv = lv;
                p2.paperID = paperID;
                netIO.SendPacket(p2);
            }
            else return;
        }
        public void OnAnoPaperUse(Packets.Client.CSMG_ANO_PAPER_USE p)
        {
            Item paperItem = Character.Inventory.GetItem(p.slotID);
            if(paperItem != null)
            {
                byte paperID = p.paperID;
                if(AnotherFactory.Instance.AnotherPapers[paperID][1].paperItems1.Contains(paperItem.ItemID))
                {
                    byte lv = AnotherFactory.Instance.GetPaperLv(Character.AnotherPapers[paperID].value.Value);
                    ulong value = GetPaperValue(paperID, (byte)(lv+1), paperItem.ItemID);
                    if (value == 0) return;
                    if (!Character.AnotherPapers[paperID].value.Test(value))
                        Character.AnotherPapers[paperID].value.SetValue(value, true);
                    else return;
                    DeleteItem(p.slotID, 1, true);
                    Packets.Server.SSMG_ANO_PAPER_USE_RESULT p2 = new Packets.Server.SSMG_ANO_PAPER_USE_RESULT();
                    p2.value = Character.AnotherPapers[paperID].value.Value;
                    p2.paperID = paperID;
                    netIO.SendPacket(p2);
                    MapServer.charDB.SavePaper(Character);
                }
            }
        }
        public ulong GetPaperValue(byte paperID, byte lv, uint ItemID)
        {
            ulong value = 0;
            if (!AnotherFactory.Instance.AnotherPapers.ContainsKey(paperID)) return 0;
            if (!AnotherFactory.Instance.AnotherPapers[paperID].ContainsKey(lv)) return 0;
            if (!AnotherFactory.Instance.AnotherPapers[paperID][lv].paperItems1.Contains(ItemID)) return 0;
            int index = AnotherFactory.Instance.AnotherPapers[paperID][lv].paperItems1.IndexOf(ItemID);
            switch (index)
            {
                case 0:
                    value = 0x1;
                    break;
                case 1:
                    value = 0x2;
                    break;
                case 2:
                    value = 0x4;
                    break;
                case 3:
                    value = 0x8;
                    break;
                case 4:
                    value = 0x10;
                    break;
                case 5:
                    value = 0x20;
                    break;
                case 6:
                    value = 0x40;
                    break;
                case 7:
                    value = 0x80;
                    break;
            }
            value = value << 8 * (lv-1);
            return value;
        }
        public void OnAnoUIOpen(Packets.Client.CSMG_ANO_UI_OPEN p)
        {
            try
            {
                CreateAnotherPaper(1);
                CreateAnotherPaper(2);
                CreateAnotherPaper(4);
                CreateAnotherPaper(6);
                CreateAnotherPaper(7);
                CreateAnotherPaper(8);
                CreateAnotherPaper(9);
                CreateAnotherPaper(10);
                CreateAnotherPaper(11);
                CreateAnotherPaper(13);
                List<ushort> List1 = new List<ushort>();
                List<ulong> List2 = new List<ulong>();
                List<byte> List3 = new List<byte>();
                Packets.Server.SSMG_ANO_SHOW_INFOBOX p2 = new Packets.Server.SSMG_ANO_SHOW_INFOBOX();
                p2.index = p.index;
                p2.cexp = 0;
                p2.usingPaperID = Character.UsingPaperID;
                foreach (var item in Character.AnotherPapers.Keys)
                {
                    if (AnotherFactory.Instance.AnotherPapers.ContainsKey(item))
                    {
                        if (AnotherFactory.Instance.AnotherPapers[item][1].type == p.index)
                        {
                            List1.Add((ushort)item);
                        }
                    }
                }
                p2.papersID = List1;
                if (Character.UsingPaperID != 0)
                    p2.usingPaperValue = Character.AnotherPapers[Character.UsingPaperID].value.Value;
                for (int i = 0; i < List1.Count; i++)
                {
                    List2.Add(Character.AnotherPapers[List1[i]].value.Value);
                }
                p2.paperValues = List2;
                if (Character.UsingPaperID != 0)
                    p2.usingLv = Character.AnotherPapers[Character.UsingPaperID].lv;//AnotherFactory.Instance.GetPaperLv(this.Character.AnotherPapers[this.Character.UsingPaperID].value.Value);
                for (int i = 0; i < List1.Count; i++)
                {
                    List3.Add(Character.AnotherPapers[List1[i]].lv);//AnotherFactory.Instance.GetPaperLv(this.Character.AnotherPapers[List1[i]].value.Value));
                }
                p2.papersLv = List3;
                /*if (this.Character.UsingPaperID != 0)
                    p2.usingSkillEXP_1 = this.Character.AnotherPapers[this.Character.UsingPaperID].skills[0];
                List2 = new List<ulong>();
                for (int i = 0; i < List1.Count; i++)
                {
                    List2.Add(this.Character.AnotherPapers[List1[i]].skills[1]);
                }
                p2.paperSkillsEXP_1 = List2;
                if (this.Character.UsingPaperID != 0)
                    p2.usingSkillEXP_2 = this.Character.AnotherPapers[this.Character.UsingPaperID].skills[1];
                List2 = new List<ulong>();
                for (int i = 0; i < List1.Count; i++)
                {
                    List2.Add(this.Character.AnotherPapers[List1[i]].skills[2]);
                }
                p2.paperSkillsEXP_2 = List2;
                if (this.Character.UsingPaperID != 0)
                    p2.usingSkillEXP_3 = this.Character.AnotherPapers[this.Character.UsingPaperID].skills[2];
                List2 = new List<ulong>();
                for (int i = 0; i < List1.Count; i++)
                {
                    List2.Add(this.Character.AnotherPapers[List1[i]].skills[3]);
                }
                p2.paperSkillsEXP_3 = List2;
                if (this.Character.UsingPaperID != 0)
                    p2.usingSkillEXP_4 = this.Character.AnotherPapers[this.Character.UsingPaperID].skills[3];
                List2 = new List<ulong>();
                for (int i = 0; i < List1.Count; i++)
                {
                    List2.Add(this.Character.AnotherPapers[List1[i]].skills[4]);
                }
                p2.paperSkillsEXP_4 = List2;*/
                netIO.SendPacket(p2);
            }
            catch(Exception ex)
            {
                Logger.ShowError(ex);
            }
        }

        public void OnCharFormChange(Packets.Client.CSMG_CHAR_FORM p)
        {
            Character.TailStyle = p.tailstyle;
            Character.WingStyle = p.wingstyle;
            Character.WingColor = p.wingcolor;
            Character.e.PropertyUpdate(UpdateEvent.CHAR_INFO, 0);
        }
        public void OnPlayerFaceView(Packets.Client.CSMG_ITEM_FACEVIEW p)
        {
            Packet p2 = new Packet(3);
            p2.ID = 0x1CF3;
            netIO.SendPacket(p2);
        }
        public void OnPlayerFaceChange(Packets.Client.CSMG_ITEM_FACECHANGE p)
        {
            uint itemID = Character.Inventory.GetItem(p.SlotID).ItemID;
            if(itemID == FaceFactory.Instance.Faces[p.FaceID])
            {
                DeleteItem(p.SlotID, 1,true);
                Character.Face = p.FaceID;
                SendPlayerInfo();
            }
        }
        /*public void SendNaviList(Packets.Client.CSMG_NAVI_OPEN p)
        {
            Packets.Server.SSMG_NAVI_LIST p1 = new Packets.Server.SSMG_NAVI_LIST();
            p1.CategoryId = p.CategoryId;
            p1.Count = 18;
            p1.Navi = this.Character.Navi;
            this.netIO.SendPacket(p1);
        }*/
        public void SendAnotherButton()
        {
            Packets.Server.SSMG_ANO_BUTTON_APPEAR p = new Packets.Server.SSMG_ANO_BUTTON_APPEAR();
            p.Type = 1;
            netIO.SendPacket(p);
        }
        public void SendRingFF()
        {
            MapServer.charDB.GetFF(Character);
            if (Character.Ring != null)
            {
                if (Character.Ring.FFarden != null)
                {
                    SendRingFFObtainMode();
                    SendRingFFHealthMode();
                    SendRingFFIsLock();
                    SendRingFFName();
                    SendRingFFMaterialPoint();
                    SendRingFFMaterialConsume();
                    SendRingFFLevel();
                    SendRingFFNextFeeTime();
                }
            }
        }
        void SendRingFFObtainMode()
        {
            Packets.Server.SSMG_FF_OBTAIN_MODE p = new SagaMap.Packets.Server.SSMG_FF_OBTAIN_MODE();
            p.value = Character.Ring.FFarden.ObMode;
            netIO.SendPacket(p);
        }
        void SendRingFFHealthMode()
        {
            Packets.Server.SSMG_FF_HEALTH_MODE p = new Packets.Server.SSMG_FF_HEALTH_MODE();
            p.value = Character.Ring.FFarden.HealthMode;
            netIO.SendPacket(p);
        }
        void SendRingFFIsLock()
        {
            Packets.Server.SSMG_FF_ISLOCK p = new Packets.Server.SSMG_FF_ISLOCK();
            if (Character.Ring.FFarden.IsLock)
                p.value = 1;
            else
                p.value = 0;
            netIO.SendPacket(p);
        }
        void SendRingFFName()
        {
            Packets.Server.SSMG_FF_RINGSELF p = new Packets.Server.SSMG_FF_RINGSELF();
            p.name = Character.Ring.FFarden.Name;
            netIO.SendPacket(p);
        }
        void SendRingFFMaterialPoint()
        {
            Packets.Server.SSMG_FF_MATERIAL_POINT p = new Packets.Server.SSMG_FF_MATERIAL_POINT();
            p.value = Character.Ring.FFarden.MaterialPoint;
            netIO.SendPacket(p);
        }
        void SendRingFFMaterialConsume()
        {
            Packets.Server.SSMG_FF_MATERIAL_CONSUME p = new Packets.Server.SSMG_FF_MATERIAL_CONSUME();
            p.value = Character.Ring.FFarden.MaterialConsume;
            netIO.SendPacket(p);
        }
        void SendRingFFLevel()
        {
            Packets.Server.SSMG_FF_LEVEL p = new Packets.Server.SSMG_FF_LEVEL();
            p.level = Character.Ring.FFarden.Level;
            p.value = Character.Ring.FFarden.FFexp;
            netIO.SendPacket(p);
            Packets.Server.SSMG_FF_F_LEVEL p1 = new Packets.Server.SSMG_FF_F_LEVEL();
            p1.level = Character.Ring.FFarden.FLevel;
            p1.value = Character.Ring.FFarden.FFFexp;
            netIO.SendPacket(p1);
            Packets.Server.SSMG_FF_SU_LEVEL p2 = new Packets.Server.SSMG_FF_SU_LEVEL();
            p2.level = Character.Ring.FFarden.SULevel;
            p2.value = Character.Ring.FFarden.FFSUexp;
            netIO.SendPacket(p2);
            Packets.Server.SSMG_FF_BP_LEVEL p3 = new Packets.Server.SSMG_FF_BP_LEVEL();
            p3.level = Character.Ring.FFarden.BPLevel;
            p3.value = Character.Ring.FFarden.FFBPexp;
            netIO.SendPacket(p3);
            Packets.Server.SSMG_FF_DEM_LEVEL p4 = new Packets.Server.SSMG_FF_DEM_LEVEL();
            p4.level = Character.Ring.FFarden.DEMLevel;
            p4.value = Character.Ring.FFarden.FFDEMexp;
            netIO.SendPacket(p4);
        }
        void SendRingFFNextFeeTime()
        {
            Packets.Server.SSMG_FF_NEXTFEE_DATE p = new Packets.Server.SSMG_FF_NEXTFEE_DATE();
            p.UpdateTime = DateTime.Now;
            netIO.SendPacket(p);
        }


        void SendEffect(uint effect)
        {
            EffectArg arg = new EffectArg();
            arg.actorID = Character.ActorID;
            arg.effectID = effect;
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, chara, true);
        }

        public void ResetStatusPoint()
        {
            SagaLogin.Configurations.StartupSetting setting = Configuration.Instance.StartupSetting[Character.Race];
            Character.StatsPoint += PC.StatusFactory.Instance.GetTotalBonusPointForStats(setting.Str, Character.Str);
            Character.StatsPoint += PC.StatusFactory.Instance.GetTotalBonusPointForStats(setting.Dex, Character.Dex);
            Character.StatsPoint += PC.StatusFactory.Instance.GetTotalBonusPointForStats(setting.Int, Character.Int);
            Character.StatsPoint += PC.StatusFactory.Instance.GetTotalBonusPointForStats(setting.Vit, Character.Vit);
            Character.StatsPoint += PC.StatusFactory.Instance.GetTotalBonusPointForStats(setting.Agi, Character.Agi);
            Character.StatsPoint += PC.StatusFactory.Instance.GetTotalBonusPointForStats(setting.Mag, Character.Mag);

            Character.Str = setting.Str;
            Character.Dex = setting.Dex;
            Character.Int = setting.Int;
            Character.Vit = setting.Vit;
            Character.Agi = setting.Agi;
            Character.Mag = setting.Mag;

            PC.StatusFactory.Instance.CalcStatus(Character);
            SendPlayerInfo();
        }

        public void SendRange()
        {

            Packets.Server.SSMG_ITEM_EQUIP p = new SagaMap.Packets.Server.SSMG_ITEM_EQUIP();
            p.InventorySlot = 0xFFFFFFFF;
            p.Target = ContainerType.NONE;
            p.Result = 1;
            p.Range = chara.Range;
            netIO.SendPacket(p);
        }

        public void SendActorID()
        {
            Packets.Server.SSMG_ACTOR_SPEED p2 = new SagaMap.Packets.Server.SSMG_ACTOR_SPEED();
            p2.ActorID = Character.ActorID;
            p2.Speed = 10;
            //p2.Speed = 96;
            netIO.SendPacket(p2);
        }

        public void SendStamp()
        {
            Packets.Server.SSMG_STAMP_INFO p = new SagaMap.Packets.Server.SSMG_STAMP_INFO();
            p.Page = 0;
            p.Stamp = Character.Stamp;
            netIO.SendPacket(p);
            p = new SagaMap.Packets.Server.SSMG_STAMP_INFO();
            p.Page = 1;
            p.Stamp = new Stamp();
            netIO.SendPacket(p);
        }

        public void SendActorMode()
        {
            Character.e.OnPlayerMode(Character);
        }

        public void SendCharOption()
        {
            Packets.Server.SSMG_ACTOR_OPTION p4 = new SagaMap.Packets.Server.SSMG_ACTOR_OPTION();
            p4.Option = SagaMap.Packets.Server.SSMG_ACTOR_OPTION.Options.NONE;
            netIO.SendPacket(p4);
        }

        public void SendCharInfo()
        {
            if (Character.Online)
            {
                CaculateItemSkills();
                Skill.SkillHandler.Instance.CastPassiveSkills(Character);

                SendAttackType();
                Packets.Server.SSMG_PLAYER_INFO p1 = new SagaMap.Packets.Server.SSMG_PLAYER_INFO();
                p1.Player = Character;
                netIO.SendPacket(p1);

                SendPlayerInfo();
            }
        }

        public void SendPlayerInfo()
        {
            if (Character.Online)
            {
                SendGoldUpdate();
                SendActorHPMPSP(Character);
                SendStatus();
                SendRange();
                SendStatusExtend();
                SendCapacity();
                //SendMaxCapacity();
                SendPlayerJob();
                SendSkillList();
                SendPlayerLevel();
                SendEXP();
                SendActorMode();
                SendCL();
                SendMotionList();
                if (Character.e != null) Character.e.PropertyUpdate(UpdateEvent.SPEED, 0);
            }
        }
        public void SendMotionList()
        {
            if (Character.Online)
            {
                Packets.Server.SSMG_CHAT_EXPRESSION_UNLOCK p = new SagaMap.Packets.Server.SSMG_CHAT_EXPRESSION_UNLOCK();
                p.unlock = 0xffffffff;
                netIO.SendPacket(p);
                Packets.Server.SSMG_CHAT_EXEMOTION_UNLOCK p2 = new SagaMap.Packets.Server.SSMG_CHAT_EXEMOTION_UNLOCK();
                p2.List1 = 0xffffffff;
                p2.List2 = 0xffffffff;
                p2.List3 = 0xffffffff;
                p2.List4 = 0xffffffff;
                p2.List5 = 0xffffffff;
                netIO.SendPacket(p2);
            }
        }
        public void SendAttackType()
        {
            if (Character.Online)
            {
                //去掉攻击类型消息显示
                IConcurrentDictionary<EnumEquipSlot, Item> equips;
                if (chara.Form == DEM_FORM.NORMAL_FORM)
                    equips = chara.Inventory.Equipments;
                else
                    equips = chara.Inventory.Parts;
                if (equips.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                {
                    Item item = equips[EnumEquipSlot.RIGHT_HAND];
                    Character.Status.attackType = item.AttackType;                    
                    /*switch (item.AttackType)
                    {
                        case ATTACK_TYPE.BLOW:
                            SendSystemMessage(SagaMap.Packets.Server.SSMG_SYSTEM_MESSAGE.Messages.GAME_SMSG_RECV_POSTUREBLOW_TEXT);
                            break;
                        case ATTACK_TYPE.STAB:
                            SendSystemMessage(SagaMap.Packets.Server.SSMG_SYSTEM_MESSAGE.Messages.GAME_SMSG_RECV_POSTURESTAB_TEXT);
                            break;
                        case ATTACK_TYPE.SLASH:
                            SendSystemMessage(SagaMap.Packets.Server.SSMG_SYSTEM_MESSAGE.Messages.GAME_SMSG_RECV_POSTURESLASH_TEXT);
                            break;
                        default:
                            SendSystemMessage(SagaMap.Packets.Server.SSMG_SYSTEM_MESSAGE.Messages.GAME_SMSG_RECV_POSTUREERROR_TEXT);
                            break;
                    }*/
                }
                else
                {
                    Character.Status.attackType = ATTACK_TYPE.BLOW;
                    //SendSystemMessage(SagaMap.Packets.Server.SSMG_SYSTEM_MESSAGE.Messages.GAME_SMSG_RECV_POSTUREBLOW_TEXT);
                }
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.ATTACK_TYPE_CHANGE, null, Character, true);
            }
        }

        public void SendStatus()
        {
            if (Character.Online)
            {
                Packets.Server.SSMG_PLAYER_STATUS p = new SagaMap.Packets.Server.SSMG_PLAYER_STATUS();
                if (chara.Form == DEM_FORM.MACHINA_FORM || chara.Race != PC_RACE.DEM)
                {
                    p.AgiBase = (ushort)(Character.Agi + chara.Status.m_agi_chip);
                    p.AgiRevide = (short)(Character.Status.agi_rev + Character.Status.agi_item + Character.Status.agi_iris + Character.Status.agi_skill + Character.Status.agi_ano + Character.Status.agi_food);
                    p.AgiBonus = StatusFactory.Instance.RequiredBonusPoint(Character.Agi);
                    p.DexBase = (ushort)(Character.Dex + chara.Status.m_dex_chip);
                    p.DexRevide = (short)(Character.Status.dex_rev + Character.Status.dex_item + Character.Status.dex_iris + Character.Status.dex_skill + Character.Status.dex_ano + Character.Status.dex_food);
                    p.DexBonus = StatusFactory.Instance.RequiredBonusPoint(Character.Dex);
                    p.IntBase = (ushort)(Character.Int + chara.Status.m_int_chip);
                    p.IntRevide = (short)(Character.Status.int_rev + Character.Status.int_item + Character.Status.int_iris + Character.Status.int_skill + Character.Status.int_ano + Character.Status.int_food);
                    p.IntBonus = StatusFactory.Instance.RequiredBonusPoint(Character.Int);
                    p.VitBase = (ushort)(Character.Vit + chara.Status.m_vit_chip);
                    p.VitRevide = (short)(Character.Status.vit_rev + Character.Status.vit_item + Character.Status.vit_iris + Character.Status.vit_skill + Character.Status.vit_ano + Character.Status.vit_food);
                    p.VitBonus = StatusFactory.Instance.RequiredBonusPoint(Character.Vit);
                    p.StrBase = (ushort)(Character.Str + chara.Status.m_str_chip);
                    p.StrRevide = (short)(Character.Status.str_rev + Character.Status.str_item + Character.Status.str_iris + Character.Status.str_skill + Character.Status.str_ano + Character.Status.str_food);
                    p.StrBonus = StatusFactory.Instance.RequiredBonusPoint(Character.Str);
                    p.MagBase = (ushort)(Character.Mag + chara.Status.m_mag_chip);
                    p.MagRevide = (short)(Character.Status.mag_rev + Character.Status.mag_item + Character.Status.mag_iris + Character.Status.mag_skill + Character.Status.mag_ano + Character.Status.mag_food);
                    p.MagBonus = StatusFactory.Instance.RequiredBonusPoint(Character.Mag);
                    netIO.SendPacket(p);
                }
                else
                {
                    p.AgiBase = (ushort)(Character.Agi + chara.Status.m_agi_chip);
                    p.AgiRevide = (short)(Character.Status.agi_rev - chara.Status.m_agi_chip + Character.Status.agi_item + Character.Status.agi_iris + Character.Status.agi_skill + Character.Status.agi_ano + Character.Status.agi_food);
                    p.AgiBonus = StatusFactory.Instance.RequiredBonusPoint(Character.Agi);
                    p.DexBase = (ushort)(Character.Dex + chara.Status.m_dex_chip);
                    p.DexRevide = (short)(Character.Status.dex_rev - chara.Status.m_dex_chip + Character.Status.dex_item + Character.Status.dex_iris + Character.Status.dex_skill + Character.Status.dex_ano + Character.Status.dex_food);
                    p.DexBonus = StatusFactory.Instance.RequiredBonusPoint(Character.Dex);
                    p.IntBase = (ushort)(Character.Int + chara.Status.m_int_chip);
                    p.IntRevide = (short)(Character.Status.int_rev - chara.Status.m_int_chip + Character.Status.int_item + Character.Status.int_iris + Character.Status.int_skill + Character.Status.int_ano + Character.Status.int_food);
                    p.IntBonus = StatusFactory.Instance.RequiredBonusPoint(Character.Int);
                    p.VitBase = (ushort)(Character.Vit + chara.Status.m_vit_chip);
                    p.VitRevide = (short)(Character.Status.vit_rev - chara.Status.m_vit_chip + Character.Status.vit_item + Character.Status.vit_iris + Character.Status.vit_skill + Character.Status.vit_ano + Character.Status.vit_food);
                    p.VitBonus = StatusFactory.Instance.RequiredBonusPoint(Character.Vit);
                    p.StrBase = (ushort)(Character.Str + chara.Status.m_str_chip);
                    p.StrRevide = (short)(Character.Status.str_rev - chara.Status.m_str_chip + Character.Status.str_item + Character.Status.str_iris + Character.Status.str_skill + Character.Status.str_ano + Character.Status.str_food);
                    p.StrBonus = StatusFactory.Instance.RequiredBonusPoint(Character.Str);
                    p.MagBase = (ushort)(Character.Mag + chara.Status.m_mag_chip);
                    p.MagRevide = (short)(Character.Status.mag_rev - chara.Status.m_mag_chip + Character.Status.mag_item + Character.Status.mag_iris + Character.Status.mag_skill + Character.Status.mag_ano + Character.Status.mag_food);
                    p.MagBonus = StatusFactory.Instance.RequiredBonusPoint(Character.Mag);

                    netIO.SendPacket(p);
                }
            }
        }

        public void SendStatusExtend()
        {
            if (Character.Online)
            {
                Packets.Server.SSMG_PLAYER_STATUS_EXTEND p = new SagaMap.Packets.Server.SSMG_PLAYER_STATUS_EXTEND();
                p.ASPD = (short)(Character.Status.aspd);
                switch (chara.Status.attackType)
                {
                    case ATTACK_TYPE.BLOW:
                        p.ATK1Max = Character.Status.max_atk1;
                        p.ATK1Min = Character.Status.min_atk1;
                        break;
                    case ATTACK_TYPE.SLASH:
                        p.ATK1Max = Character.Status.max_atk2;
                        p.ATK1Min = Character.Status.min_atk2;
                        break;
                    case ATTACK_TYPE.STAB:
                        p.ATK1Max = Character.Status.max_atk3;
                        p.ATK1Min = Character.Status.min_atk3;
                        break;
                }
                if (chara.Job == PC_JOB.HAWKEYE)
                {
                    if (chara.TInt["斥候远程模式"] == 1)
                    {
                        p.ATK1Max = Character.Status.max_atk3;
                        p.ATK1Min = Character.Status.min_atk3;
                    }
                }
                p.ATK2Max = Character.Status.max_atk2;
                p.ATK2Min = Character.Status.min_atk2;
                p.ATK3Max = Character.Status.max_atk3;
                p.ATK3Min = Character.Status.min_atk3;
                p.AvoidCritical = Character.Status.avoid_critical;
                p.AvoidMagic = Character.Status.avoid_magic;
                p.AvoidMelee = (ushort)(Skill.SkillHandler.Instance.CalCriBonusRate(Character, null,0)+100f);//物理暴击伤害
                p.AvoidRanged = (ushort)(Skill.SkillHandler.Instance.CalCriBonusRate(Character, null, 1)+100f);//魔法暴击伤害
                p.CSPD = (short)(Character.Status.cspd);
                p.DefAddition = (ushort)Character.Status.def_add;
                p.DefBase = Character.Status.def;
                p.HitCritical = Character.Status.hit_critical;
                p.HitMagic = Character.Status.hit_magic;
                p.HitMelee = (ushort)Skill.SkillHandler.Instance.CalcCriRate(Character, null);//物理暴击
                p.HitRanged = (ushort)Skill.SkillHandler.Instance.CalcCriRate(Character, null);//魔法暴击
                p.MATKMax = Character.Status.max_matk;
                p.MATKMin = Character.Status.min_matk;
                p.MDefAddition = (ushort)Character.Status.mdef_add;
                p.MDefBase = Character.Status.mdef;
                p.Speed = Character.Speed;
                netIO.SendPacket(p);
            }
        }

        public void SendCapacity()
        {
            if (Character.Online)
            {
                Packets.Server.SSMG_PLAYER_CAPACITY p = new SagaMap.Packets.Server.SSMG_PLAYER_CAPACITY();
                /*p.CapacityBack = this.Character.Inventory.Volume[ContainerType.BACK_BAG];
                p.CapacityBody = this.Character.Inventory.Volume[ContainerType.BODY];
                p.CapacityLeft = this.Character.Inventory.Volume[ContainerType.LEFT_BAG];
                p.CapacityRight = this.Character.Inventory.Volume[ContainerType.RIGHT_BAG];
                p.PayloadBack = this.Character.Inventory.Payload[ContainerType.BACK_BAG];
                p.PayloadBody = this.Character.Inventory.Payload[ContainerType.BODY];
                p.PayloadLeft = this.Character.Inventory.Payload[ContainerType.LEFT_BAG];
                p.PayloadRight = this.Character.Inventory.Payload[ContainerType.RIGHT_BAG];*/
                p.Payload = Character.Inventory.Payload[ContainerType.BODY];
                p.Volume = Character.Inventory.Volume[ContainerType.BODY];
                p.MaxPayload = Character.Inventory.MaxPayload[ContainerType.BODY];
                p.MaxVolume = Character.Inventory.MaxVolume[ContainerType.BODY];
                netIO.SendPacket(p);
            }
        }

        public void SendMaxCapacity()
        {
            /*if (this.Character.Online)
            {
                Packets.Server.SSMG_PLAYER_MAX_CAPACITY p = new SagaMap.Packets.Server.SSMG_PLAYER_MAX_CAPACITY();
                /*p.CapacityBack = this.Character.Inventory.MaxVolume[ContainerType.BACK_BAG];
                p.CapacityBody = this.Character.Inventory.MaxVolume[ContainerType.BODY];
                p.CapacityLeft = this.Character.Inventory.MaxVolume[ContainerType.LEFT_BAG];
                p.CapacityRight = this.Character.Inventory.MaxVolume[ContainerType.RIGHT_BAG];
                p.PayloadBack = this.Character.Inventory.MaxPayload[ContainerType.BACK_BAG];
                p.PayloadBody = this.Character.Inventory.MaxPayload[ContainerType.BODY];
                p.PayloadLeft = this.Character.Inventory.MaxPayload[ContainerType.LEFT_BAG];
                p.PayloadRight = this.Character.Inventory.MaxPayload[ContainerType.RIGHT_BAG];
                p.Payload = this.Character.Inventory.MaxPayload[ContainerType.BODY];
                p.Volume = this.Character.Inventory.MaxVolume[ContainerType.BODY]; 
                this.netIO.SendPacket(p);
            }*/
        }

        public void SendChangeMap()
        {
            if (Character.Online)
            {
                Packets.Server.SSMG_PLAYER_CHANGE_MAP p = new SagaMap.Packets.Server.SSMG_PLAYER_CHANGE_MAP();
                if (map.returnori)
                    p.MapID = map.OriID;
                else
                    p.MapID = Character.MapID;
                p.X = Global.PosX16to8(Character.X, map.Width);
                p.Y = Global.PosY16to8(Character.Y, map.Height);
                p.Dir = (byte)(Character.Dir / 45);
                if (map.IsDungeon)
                {
                    p.DungeonDir = map.DungeonMap.Dir;
                    p.DungeonX = map.DungeonMap.X;
                    p.DungeonY = map.DungeonMap.Y;
                    Character.Speed = Configuration.Instance.Speed;
                }
                if (fgTakeOff)
                {
                    p.FGTakeOff = fgTakeOff;
                    fgTakeOff = false;
                }
                else
                {
                    Character.Speed = Configuration.Instance.Speed;
                }
                netIO.SendPacket(p);
                SendRange();
            }
        }

        public void SendGotoFG()
        {
            if (Character.Online)
            {
                Map fgMap = MapManager.Instance.GetMap(Character.MapID);
                if (!fgMap.IsMapInstance)
                {
                    Logger.ShowDebug(string.Format("MapID:{0} isn't a valid flying garden!"), Logger.defaultlogger);
                }
                ActorPC owner = fgMap.Creator;
                Packets.Server.SSMG_PLAYER_GOTO_FG p = new SagaMap.Packets.Server.SSMG_PLAYER_GOTO_FG();
                p.MapID = Character.MapID;
                p.X = Global.PosX16to8(Character.X, map.Width);
                p.Y = Global.PosY16to8(Character.Y, map.Height);
                p.Dir = (byte)(Character.Dir / 45);
                p.Equiptments = owner.FGarden.FGardenEquipments;
                netIO.SendPacket(p);                
            }
        }

        public void SendGotoFF()
        {
            if (Character.Online)
            {
                Map fgMap = MapManager.Instance.GetMap(Character.MapID);
                if (fgMap.ID == 90001999 || fgMap.ID == 10054000)
                {
                    CustomMapManager.Instance.SendGotoSerFFMap(this);
                    return;
                }
                if (!fgMap.IsMapInstance)
                {
                    Logger.ShowDebug(string.Format("MapID:{0} isn't a valid flying garden!"), Logger.defaultlogger);
                }
                ActorPC owner = fgMap.Creator;
                Packets.Server.SSMG_FF_ENTER p = new Packets.Server.SSMG_FF_ENTER();
                p.MapID = Character.MapID;
                p.X = Global.PosX16to8(Character.X, map.Width);
                p.Y = Global.PosY16to8(Character.Y, map.Height);
                p.Dir = (byte)(Character.Dir / 45);
                p.RingID = Character.Ring.ID;
                p.RingHouseID = 30250000;
                netIO.SendPacket(p);
            }
        }

        public void SendDungeonEvent()
        {
            if (Character.Online)
            {
                if (!map.IsMapInstance || !map.IsDungeon)
                    return;
                foreach(Dungeon.GateType i in map.DungeonMap.Gates.Keys)
                {
                    if (map.DungeonMap.Gates[i].NPCID != 0)
                    {
                        Packets.Server.SSMG_NPC_SHOW p = new SagaMap.Packets.Server.SSMG_NPC_SHOW();
                        p.NPCID = map.DungeonMap.Gates[i].NPCID;
                        netIO.SendPacket(p);
                    }
                    if (map.DungeonMap.Gates[i].ConnectedMap != null)
                    {
                        if (i != SagaMap.Dungeon.GateType.Central && i != SagaMap.Dungeon.GateType.Exit)
                        {
                            Packets.Server.SSMG_NPC_SET_EVENT_AREA p1 = new SagaMap.Packets.Server.SSMG_NPC_SET_EVENT_AREA();
                            p1.StartX = map.DungeonMap.Gates[i].X;
                            p1.EndX = map.DungeonMap.Gates[i].X;
                            p1.StartY = map.DungeonMap.Gates[i].Y;
                            p1.EndY = map.DungeonMap.Gates[i].Y;

                            switch (i)
                            {
                                case SagaMap.Dungeon.GateType.North:
                                    p1.EventID = 12001501;
                                    break;
                                case SagaMap.Dungeon.GateType.East:
                                    p1.EventID = 12001502;
                                    break;
                                case SagaMap.Dungeon.GateType.South:
                                    p1.EventID = 12001503;
                                    break;
                                case SagaMap.Dungeon.GateType.West:
                                    p1.EventID = 12001504;
                                    break;

                            }
                            switch (map.DungeonMap.Gates[i].Direction)
                            {
                                case SagaMap.Dungeon.Direction.In:
                                    p1.EffectID = 9002;
                                    break;
                                case SagaMap.Dungeon.Direction.Out:
                                    p1.EffectID = 9005;
                                    break;
                            }
                            netIO.SendPacket(p1);
                        }
                        else
                        {
                            Packets.Server.SSMG_NPC_SET_EVENT_AREA p1 = new SagaMap.Packets.Server.SSMG_NPC_SET_EVENT_AREA();
                            p1.StartX = map.DungeonMap.Gates[i].X;
                            p1.EndX = map.DungeonMap.Gates[i].X;
                            p1.StartY = map.DungeonMap.Gates[i].Y;
                            p1.EndY = map.DungeonMap.Gates[i].Y;
                            p1.EventID = 12001505;
                            p1.EffectID = 9005;
                            netIO.SendPacket(p1);
                        }

                        if (map.DungeonMap.Gates[i].NPCID != 0)
                        {
                            Packets.Server.SSMG_CHAT_MOTION p = new SagaMap.Packets.Server.SSMG_CHAT_MOTION();
                            p.ActorID = map.DungeonMap.Gates[i].NPCID;
                            p.Motion = (MotionType)621;
                            netIO.SendPacket(p);
                        }
                    }
                    else
                    {
                        if (i == SagaMap.Dungeon.GateType.Entrance)
                        {
                            Packets.Server.SSMG_NPC_SET_EVENT_AREA p1 = new SagaMap.Packets.Server.SSMG_NPC_SET_EVENT_AREA();
                            p1.StartX = map.DungeonMap.Gates[i].X;
                            p1.EndX = map.DungeonMap.Gates[i].X;
                            p1.StartY = map.DungeonMap.Gates[i].Y;
                            p1.EndY = map.DungeonMap.Gates[i].Y;
                            p1.EventID = 12001505;
                            p1.EffectID = 9003;
                            netIO.SendPacket(p1);
                        }
                    }
                }

            }
        }

        public void SendFGEvent()
        {
            if (Character.Online)
            {
                Map fgMap = MapManager.Instance.GetMap(Character.MapID);
                if (!fgMap.IsMapInstance)
                    return;
                if (map == null) return;
                if (map.OriID == 70000000)
                {
                    if (map.Creator.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.GARDEN_MODELHOUSE] != 0)
                    {
                        Packets.Server.SSMG_NPC_SET_EVENT_AREA p1 = new SagaMap.Packets.Server.SSMG_NPC_SET_EVENT_AREA();
                        p1.EventID = 10000315;
                        p1.StartX = 6;
                        p1.StartY = 7;
                        p1.EndX = 6;
                        p1.EndY = 7;
                        netIO.SendPacket(p1);
                    }
                }
            }
        }

        public void SendGoldUpdate()
        {
            if (Character.Online)
            {
                Packets.Server.SSMG_PLAYER_GOLD_UPDATE p = new SagaMap.Packets.Server.SSMG_PLAYER_GOLD_UPDATE();
                p.Gold = (ulong)Character.Gold;
                netIO.SendPacket(p);
            }

        }

        public void SendActorHPMPSP(Actor actor)
        {
            if (Character.Online)
            {
                Packets.Server.SSMG_PLAYER_MAX_HPMPSP p = new SagaMap.Packets.Server.SSMG_PLAYER_MAX_HPMPSP();
                p.ActorID = actor.ActorID;
                p.MaxHP = actor.MaxHP;
                p.MaxMP = actor.MaxMP;
                p.MaxSP = actor.MaxSP;
                p.MaxEP = actor.MaxEP;
                netIO.SendPacket(p);
                Packets.Server.SSMG_PLAYER_HPMPSP p10 = new SagaMap.Packets.Server.SSMG_PLAYER_HPMPSP();
                p10.ActorID = actor.ActorID;
                p10.HP = actor.HP;
                p10.MP = actor.MP;
                p10.SP = actor.SP;
                p10.EP = actor.EP;
                netIO.SendPacket(p10);
                if (actor == Character)
                {

                        if (Character.Party != null)
                        {
                            PartyManager.Instance.UpdateMemberHPMPSP(Character.Party, Character);
                        }
                        
                    //if ((DateTime.Now - hpmpspStamp).TotalSeconds >= 2)
                    //{
                    //    hpmpspStamp = DateTime.Now;
                    //}
                }
            }
        }

        public void SendCharXY()
        {
            if (Character.Online)
            {
                Packets.Server.SSMG_ACTOR_MOVE p = new SagaMap.Packets.Server.SSMG_ACTOR_MOVE();
                p.ActorID = Character.ActorID;
                p.Dir = Character.Dir;
                p.X = Character.X;
                p.Y = Character.Y;
                p.MoveType = MoveType.WARP;
                netIO.SendPacket(p);
            }

        }

        public void SendPlayerLevel()
        {
            if (Character.Online)
            {
                Packets.Server.SSMG_PLAYER_LEVEL p = new SagaMap.Packets.Server.SSMG_PLAYER_LEVEL();
                if (map.Info.Flag.Test(MapFlags.Dominion))
                {
                    p.Level = Character.DominionLevel;
                    p.JobLevel = Character.DominionJobLevel;
                    p.JobLevel2T = Character.DominionJobLevel;
                    p.JobLevel2X = Character.DominionJobLevel;
                    //p.UnknownLevel = this.Character.JobLevel3;
                    p.JobLevelJoint = Character.JointJobLevel;
                    p.BonusPoint = Character.StatsPoint;
                    p.SkillPoint = 0;
                    p.Skill2XPoint = 0;
                    p.Skill2TPoint = 0;
                    p.Skill3Point = 0;
                }
                else
                {
                    p.Level = Character.Level;
                    p.JobLevel = Character.JobLevel1;
                    p.JobLevel2T = Character.JobLevel2T;
                    p.JobLevel2X = Character.JobLevel2X;
                    //p.UnknownLevel = this.Character.JobLevel3;
                    if (Character.Job == Character.Job3)
                        p.JobLevelJoint = Character.JobLevel3;
                    else
                        p.JobLevelJoint = Character.JointJobLevel;
                    p.BonusPoint = Character.StatsPoint;
                    p.SkillPoint = Character.SkillPoint;
                    p.Skill2XPoint = Character.SkillPoint2X;
                    p.Skill2TPoint = Character.SkillPoint2T;
                    p.Skill3Point = Character.SkillPoint3;
                }
                netIO.SendPacket(p);
            }
        }

        public void SendPlayerJob()
        {
            if (Character.Online)
            {
                Packets.Server.SSMG_PLAYER_JOB p = new SagaMap.Packets.Server.SSMG_PLAYER_JOB();
                p.Job = Character.Job;
                if (Character.JobJoint != PC_JOB.NONE)
                    p.JointJob = Character.JobJoint;
                netIO.SendPacket(p);
            }
        }


        public void SendCharInfoUpdate()
        {
            if (Character.Online)
            {
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, Character, true);
            }
        }

        public void SendAnnounce(string text)
        {
            if (Character.Online)
            {
                Packets.Server.SSMG_CHAT_PUBLIC p11 = new SagaMap.Packets.Server.SSMG_CHAT_PUBLIC();
                p11.ActorID = 0;
                p11.Message = text;
                netIO.SendPacket(p11);
            }
        }

        public void SendPkMode()
        {
            if (Character.Online)
            {
                Character.Mode = PlayerMode.COLISEUM_MODE;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.PLAYER_MODE, null, Character, true);
            }
        }

        public void SendNormalMode()
        {
            if (Character.Online)
            {
                Character.Mode = PlayerMode.NORMAL;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.PLAYER_MODE, null, Character, true);
            }
        }

        public void SendPlayerSizeUpdate()
        {
            if (Character.Online)
            {
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.PLAYER_SIZE_UPDATE, null, Character, true);
            }

        }

        public void OnMove(Packets.Client.CSMG_PLAYER_MOVE p)
        {
            if (Character.Online)
            {
                if (state != SESSION_STATE.LOADED)
                    return;
                switch (p.MoveType)
                {
                    case MoveType.RUN:
                        Map.MoveActor(Map.MOVE_TYPE.START, Character, new short[2] { p.X, p.Y }, p.Dir, Character.Speed);
                        moveCheckStamp = DateTime.Now;
                        break;
                    case MoveType.CHANGE_DIR:
                        Map.MoveActor(Map.MOVE_TYPE.STOP, Character, new short[2] { p.X, p.Y }, p.Dir, Character.Speed);
                        break;
                    case MoveType.WALK:
                        Map.MoveActor(Map.MOVE_TYPE.START, Character, new short[2] { p.X, p.Y }, p.Dir, Character.Speed,false, MoveType.WALK);
                        moveCheckStamp = DateTime.Now;
                        break;
                }
                if (Character.CInt["NextMoveEventID"] != 0)
                {
                    EventActivate((uint)Character.CInt["NextMoveEventID"]);
                    Character.CInt["NextMoveEventID"] = 0;
                    Character.CInt.Remove("NextMoveEventID");
                }
                if (Character.TTime["特殊刀攻击间隔"] != DateTime.Now)
                    Character.TTime["特殊刀攻击间隔"] = DateTime.Now;
            }
        }

        public void SendActorSpeed(Actor actor, ushort speed)
        {
            if (Character.Online)
            {
                Packets.Server.SSMG_ACTOR_SPEED p = new SagaMap.Packets.Server.SSMG_ACTOR_SPEED();
                p.ActorID = actor.ActorID;
                p.Speed = speed;
                netIO.SendPacket(p);
            }
        }

        public void SendEXP()
        {
            if (Character.Online)
            {
                Packets.Server.SSMG_PLAYER_EXP p = new SagaMap.Packets.Server.SSMG_PLAYER_EXP();
                ulong cexp, jexp;
                ulong bexp = 0, nextExp = 0;
                if (map.Info.Flag.Test(MapFlags.Dominion))
                {
                    bexp = ExperienceManager.Instance.GetExpForLevel(Character.DominionLevel, Scripting.LevelType.CLEVEL);
                    nextExp = ExperienceManager.Instance.GetExpForLevel((uint)(Character.DominionLevel + 1), Scripting.LevelType.CLEVEL);
                    cexp = (uint)((float)(Character.DominionCEXP - bexp) / (nextExp - bexp) * 1000);
                }
                else
                {
                    if (!Character.Rebirth || Character.Job != Character.Job3)
                    {
                        bexp = ExperienceManager.Instance.GetExpForLevel(Character.Level, Scripting.LevelType.CLEVEL);
                        nextExp = ExperienceManager.Instance.GetExpForLevel((uint)(Character.Level + 1), Scripting.LevelType.CLEVEL);
                    }
                    else
                    {
                        bexp = ExperienceManager.Instance.GetExpForLevel(Character.Level, Scripting.LevelType.CLEVEL2);
                        nextExp = ExperienceManager.Instance.GetExpForLevel((uint)(Character.Level + 1), Scripting.LevelType.CLEVEL2);
                    }
                    cexp = (uint)((float)(Character.CEXP - bexp) / (nextExp - bexp) * 1000);
                }
                if (Character.JobJoint == PC_JOB.NONE)
                {
                    if (map.Info.Flag.Test(MapFlags.Dominion))
                    {
                        bexp = ExperienceManager.Instance.GetExpForLevel(Character.DominionJobLevel, Scripting.LevelType.JLEVEL2);
                        nextExp = ExperienceManager.Instance.GetExpForLevel((uint)(Character.DominionJobLevel + 1), Scripting.LevelType.JLEVEL2);

                        jexp = (uint)((float)(Character.DominionJEXP - bexp) / (nextExp - bexp) * 1000);
                    }
                    else
                    {
                        if (Character.Job == Character.JobBasic)
                        {
                            bexp = ExperienceManager.Instance.GetExpForLevel(Character.JobLevel1, Scripting.LevelType.JLEVEL);
                            nextExp = ExperienceManager.Instance.GetExpForLevel((uint)(Character.JobLevel1 + 1), Scripting.LevelType.JLEVEL);
                        }
                        else if (Character.Job == Character.Job2X)
                        {
                            bexp = ExperienceManager.Instance.GetExpForLevel(Character.JobLevel2X, Scripting.LevelType.JLEVEL2);
                            nextExp = ExperienceManager.Instance.GetExpForLevel((uint)(Character.JobLevel2X + 1), Scripting.LevelType.JLEVEL2);
                        }
                        else if (Character.Job == Character.Job2T)
                        {
                            bexp = ExperienceManager.Instance.GetExpForLevel(Character.JobLevel2T, Scripting.LevelType.JLEVEL2);
                            nextExp = ExperienceManager.Instance.GetExpForLevel((uint)(Character.JobLevel2T + 1), Scripting.LevelType.JLEVEL2);
                        }
                        else
                        {
                            bexp = ExperienceManager.Instance.GetExpForLevel(Character.JobLevel3, Scripting.LevelType.JLEVEL3);
                            nextExp = ExperienceManager.Instance.GetExpForLevel((uint)(Character.JobLevel3 + 1), Scripting.LevelType.JLEVEL3);
                        }
                        jexp = (uint)((float)(Character.JEXP - bexp) / (nextExp - bexp) * 1000);
                    }
                }
                else
                {
                    bexp = ExperienceManager.Instance.GetExpForLevel(Character.JointJobLevel, Scripting.LevelType.JLEVEL2);
                    nextExp = ExperienceManager.Instance.GetExpForLevel((uint)(Character.JointJobLevel + 1), Scripting.LevelType.JLEVEL2);

                    jexp = (uint)((float)(Character.JointJEXP - bexp) / (nextExp - bexp) * 1000);
                }
                p.EXPPercentage = (uint)cexp;
                p.JEXPPercentage = (uint)jexp;
                p.WRP = Character.WRP;
                p.ECoin = Character.ECoin;
                if (map.Info.Flag.Test(MapFlags.Dominion))
                {
                    p.Exp = (uint)Character.DominionCEXP;
                    p.JExp = (uint)Character.DominionJEXP;
                }
                else
                {
                    p.Exp = (long)Character.CEXP;
                    p.JExp = (long)Character.JEXP;
                }
                netIO.SendPacket(p);
            }
        }

        public void SendLvUP(Actor pc, byte type)
        {
            if (Character.Online)
            {
                Packets.Server.SSMG_ACTOR_LEVEL_UP p = new SagaMap.Packets.Server.SSMG_ACTOR_LEVEL_UP();
                p.ActorID = pc.ActorID;
                if (map.Info.Flag.Test(MapFlags.Dominion))
                {
                    p.Level = ((ActorPC)pc).DominionLevel;
                    p.JobLevel = ((ActorPC)pc).DominionJobLevel;
                }
                else
                {
                    p.Level = pc.Level;                    
                }
                p.LvType = type;

                netIO.SendPacket(p);
            }
        }

        public void OnPlayerGreetings(Packets.Client.CSMG_PLAYER_GREETINGS p)
        {
            if (chara.TTime["打招呼时间"] + new TimeSpan(0, 0, 3) > DateTime.Now && chara.Account.GMLevel < 200)
            {
                SendSystemMessage("不可以频繁打招呼哦。");
                return;
            }

            Actor actor = map.GetActor(p.ActorID);
            if (actor == null)
            {
                SendSystemMessage("目标错误！");
                return;
            }
            if (actor.Buff.钓鱼状态)
            {
                SendSystemMessage("对方正在钓鱼，不要打扰人家哦。");
                return;
            }
            if (actor != null)
            {
                if (actor.type == ActorType.PC)
                {
                    ActorPC target = (ActorPC)actor;
                    if (target.Online && chara.Online)
                    {
                        ushort dir = map.CalcDir(chara.X, chara.Y, target.X, target.Y);
                        ushort dir2 = map.CalcDir(target.X, target.Y, chara.X, chara.Y);

                        short[] ys = new short[2];
                        ys[0] = chara.X;
                        ys[1] = chara.Y;
                        map.MoveActor(Map.MOVE_TYPE.START, chara, ys, dir, 500, true, MoveType.CHANGE_DIR);
                        if (chara.Partner != null)
                        {
                            ys = new short[2];
                            ys[0] = chara.Partner.X;
                            ys[1] = chara.Partner.Y;
                            map.MoveActor(Map.MOVE_TYPE.START, chara.Partner, ys, dir, 500, true, MoveType.CHANGE_DIR);
                        }
                        ys = new short[2];
                        ys[0] = target.X;
                        ys[1] = target.Y;
                        if (target.AInt["打招呼无动作"] != 1)
                            map.MoveActor(Map.MOVE_TYPE.START, target, ys, dir2, 500, true, MoveType.CHANGE_DIR);
                        if (target.Partner != null)
                        {
                            ys = new short[2];
                            ys[0] = target.Partner.X;
                            ys[1] = target.Partner.Y;
                            map.MoveActor(Map.MOVE_TYPE.START, target.Partner, ys, dir, 500, true, MoveType.CHANGE_DIR);
                        }

                        ushort motionid = 163;
                        byte loop = 0;
                        switch (Global.Random.Next(0, 31))
                        {
                            case 0:
                                motionid = 113;
                                loop = 1;
                                break;
                            case 1:
                                motionid = 163;
                                break;
                            case 2:
                                motionid = 509;
                                break;
                            case 3:
                                motionid = 159;
                                break;
                            case 4:
                                motionid = 210;
                                break;
                            case 5:
                                motionid = 509;
                                break;
                            case 6:
                                motionid = 300;
                                break;
                            case 7:
                                motionid = 2035;
                                break;
                            case 8:
                                motionid = 2040;
                                break;
                            case 9:
                                motionid = 1520;
                                break;
                            case 10:
                                motionid = 1521;
                                break;
                            case 11:
                                motionid = 2020;
                                break;
                            case 12:
                                motionid = 2020;
                                break;
                            case 13:
                                motionid = 2064;
                                break;
                            case 14:
                                motionid = 2065;
                                break;
                            case 15:
                                motionid = 2066;
                                break;
                            case 16:
                                motionid = 2067;
                                break;
                            case 17:
                                motionid = 2069;
                                break;
                            case 18:
                                motionid = 2070;
                                break;
                            case 19:
                                motionid = 1524;
                                break;
                            case 20:
                                motionid = 2084;
                                break;
                            case 21:
                                motionid = 2095;
                                break;
                            case 22:
                                motionid = 2091;
                                break;
                            case 23:
                                motionid = 2085;
                                break;
                            case 24:
                                motionid = 2109;
                                break;
                            case 25:
                                motionid = 2125;
                                break;
                            case 26:
                                motionid = 2098;
                                break;
                            case 27:
                                motionid = 2079;
                                loop = 1;
                                break;
                            case 28:
                                motionid = 1523;
                                break;
                            case 29:
                                motionid = 2080;
                                break;
                            case 30:
                                motionid = 2138;
                                break;
                            case 31:
                                motionid = 2139;
                                break;
                        }
                        ushort personm = motionid;
                        if (Character.AInt["个人固定打招呼动作"] != 0)
                            personm = (ushort)Character.AInt["个人固定打招呼动作"];
                        MapClient tclient = FromActorPC(target);
                        SendMotion((MotionType)personm, loop);
                        if (tclient.Character.AInt["打招呼无动作"] != 1)
                            tclient.SendMotion((MotionType)motionid, loop);
                        SendSystemMessage("你问候了 " + target.Name);
                        tclient.SendSystemMessage(chara.Name + " 正在向你打招呼~");

                        SendEffect(7127);
                        tclient.SendEffect(7127);

                        if (target.AStr["打招呼每日重置2"] != DateTime.Now.ToString("yyyy-MM-dd"))
                        {
                            target.AStr["打招呼每日重置2"] = DateTime.Now.ToString("yyyy-MM-dd");
                            if (target.CIDict["打招呼的玩家"].Count > 0)
                                target.CIDict["打招呼的玩家"] = new VariableHolderA<int, int>();
                            target.AInt["今日被打招呼次数"] = 0;
                        }
                        if (!target.CIDict["打招呼的玩家"].ContainsKey(Character.Account.AccountID))
                        {
                            target.CIDict["打招呼的玩家"][Character.Account.AccountID] = 0;
                            tclient.TitleProccess(target, 62, 1);
                            if (target.AInt["今日被打招呼次数"] < 5)
                            {
                                target.AInt["今日被打招呼次数"]++;
                                int cp = Global.Random.Next(100, 280);
                                target.CP += (uint)cp;
                                tclient.SendSystemMessage("被亲切地问候了！获得" + cp + "CP。");

                                //援力觉醒：装备红晕与人打招呼100次，并令对方拿到CP
                                if (Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.FACE_ACCE))
                                {
                                    if (Character.Inventory.Equipments[EnumEquipSlot.FACE_ACCE].BaseData.id == 50048200)
                                        TitleProccess(Character, 91, 1);
                                }
                            }
                        }

                        //幸运星设定
                        if (target.AStr["今日幸运星重置"] != DateTime.Now.ToString("yyyy-MM-dd"))
                        {
                            target.AStr["今日幸运星重置"] = DateTime.Now.ToString("yyyy-MM-dd");
                            target.AInt["今日幸运星"] = 0;
                        }
                        if (target.AInt["今日幸运星"] == 0)
                        {
                            if (Global.Random.Next(0, 100) < 8)
                            {
                                target.AInt["今日幸运星"] = 1;//1为幸运星
                                tclient.SendSystemMessage("★★★★★你被选为今日的幸运星之一，其他人与你打招呼会获得『惊喜箱子』！★★★★★");
                                Item item = ItemFactory.Instance.GetItem(950000059);
                                item.Stack = 1;
                                tclient.AddItem(item, true);
                                tclient.SendEffect(7151);
                                tclient.TitleProccess(target, 141, 1);
                            }
                            else
                                target.AInt["今日幸运星"] = 2;//2为不是
                        }

                        //找到幸运星
                        if (Character.AStr["找幸运星每日重置"] != DateTime.Now.ToString("yyyy-MM-dd"))
                        {
                            Character.AStr["找幸运星每日重置"] = DateTime.Now.ToString("yyyy-MM-dd");
                            Character.AInt["今天找到幸运星了"] = 0;
                        }
                        if (Character.AInt["今天找到幸运星了"] != 1 && target.AInt["今日幸运星"] == 1)//首次找到幸运星！
                        {
                            SendSystemMessage("★★★★★你和今天的幸运星玩家打招呼了！获得了『惊喜箱子』！★★★★★");
                            Item item = ItemFactory.Instance.GetItem(950000059);
                            item.Stack = 1;
                            AddItem(item, true);
                            Character.AInt["今天找到幸运星了"] = 1;
                            tclient.SendEffect(7151);
                        }

                        chara.TTime["打招呼时间"] = DateTime.Now;
                    }
                }
            }
        }

        public void OnPlayerElements(Packets.Client.CSMG_PLAYER_ELEMENTS p)
        {
            Packets.Server.SSMG_PLAYER_ELEMENTS p1 = new SagaMap.Packets.Server.SSMG_PLAYER_ELEMENTS();
            Dictionary<Elements, int> elements = new Dictionary<Elements, int>();
            foreach (Elements i in chara.AttackElements.Keys)
            {
                elements.Add(i, chara.AttackElements[i] + chara.Status.attackelements_item[i] + chara.Status.attackelements_iris[i] + chara.Status.attackelements_skill[i]);
            }
            p1.AttackElements = elements;
            elements.Clear();
            foreach (Elements i in chara.Elements.Keys)
            {
                elements.Add(i, chara.Elements[i] + chara.Status.elements_item[i] + chara.Status.elements_iris[i] + chara.Status.elements_skill[i]);
            }
            p1.DefenceElements = elements;
            netIO.SendPacket(p1);
        }
        public void OnPlayerElements()
        {
            Packets.Server.SSMG_PLAYER_ELEMENTS p1 = new SagaMap.Packets.Server.SSMG_PLAYER_ELEMENTS();
            Dictionary<Elements, int> elements = new Dictionary<Elements, int>();
            foreach (Elements i in chara.AttackElements.Keys)
            {
                elements.Add(i, chara.AttackElements[i] + chara.Status.attackelements_item[i] + chara.Status.attackelements_iris[i] + chara.Status.attackelements_skill[i]);
            }
            p1.AttackElements = elements;
            elements.Clear();
            foreach (Elements i in chara.Elements.Keys)
            {
                elements.Add(i, chara.Elements[i] + chara.Status.elements_item[i] + chara.Status.elements_iris[i] + chara.Status.elements_skill[i]);
            }
            p1.DefenceElements = elements;
            netIO.SendPacket(p1);
        }
        public void OnRequestPCInfo(Packets.Client.CSMG_ACTOR_REQUEST_PC_INFO p)
        {
            Packets.Server.SSMG_ACTOR_PC_INFO p1 = new SagaMap.Packets.Server.SSMG_ACTOR_PC_INFO();
            Actor pc = map.GetActor(p.ActorID);

            if (pc == null)
            {
                return;
            }

            if (pc.type == ActorType.PC)
            {
                ActorPC a = (ActorPC)pc;
                a.WRPRanking = WRPRankingManager.Instance.GetRanking(a);
            }
            p1.Actor = pc;

            netIO.SendPacket(p1);
            if (pc.type == ActorType.PC)
            {
                ActorPC actor = (ActorPC)pc;
                if (actor.Ring != null)
                {
                    Character.e.OnActorRingUpdate(actor);
                }
            }
        }

        public void OnStatsPreCalc(Packets.Client.CSMG_PLAYER_STATS_PRE_CALC p)
        {
            //backup
            ushort str, dex, intel, agi, vit, mag;
            Packets.Server.SSMG_PLAYER_STATS_PRE_CALC p1 = new SagaMap.Packets.Server.SSMG_PLAYER_STATS_PRE_CALC();
            str = Character.Str;
            dex = Character.Dex;
            intel = Character.Int;
            agi = Character.Agi;
            vit = Character.Vit;
            mag = Character.Mag;

            Character.Str = p.Str;
            Character.Dex = p.Dex;
            Character.Int = p.Int;
            Character.Agi = p.Agi;
            Character.Vit = p.Vit;
            Character.Mag = p.Mag;

            StatusFactory.Instance.CalcStatus(Character);


            p1.ASPD = Character.Status.aspd;
            p1.ATK1Max = Character.Status.max_atk1;
            p1.ATK1Min = Character.Status.min_atk1;
            p1.ATK2Max = Character.Status.max_atk2;
            p1.ATK2Min = Character.Status.min_atk2;
            p1.ATK3Max = Character.Status.max_atk3;
            p1.ATK3Min = Character.Status.min_atk3;
            p1.AvoidCritical = Character.Status.avoid_critical;
            p1.AvoidMagic = Character.Status.avoid_magic;
            p1.AvoidMelee = (ushort)(Skill.SkillHandler.Instance.CalCriBonusRate(Character, null, 0) + 100f);//物理暴击伤害
            p1.AvoidRanged = (ushort)(Skill.SkillHandler.Instance.CalCriBonusRate(Character, null, 1) + 100f);//魔法暴击伤害
            p1.CSPD = Character.Status.cspd;
            p1.DefAddition = (ushort)Character.Status.def_add;
            p1.DefBase = Character.Status.def;
            p1.HitCritical = Character.Status.hit_critical;
            p1.HitMagic = Character.Status.hit_magic;
            p1.HitMelee = (ushort)Skill.SkillHandler.Instance.CalcCriRate(Character, null);//物理暴击
            p1.HitRanged = (ushort)Skill.SkillHandler.Instance.CalcCriRate(Character, null);//魔法暴击
            p1.MATKMax = Character.Status.max_matk;
            p1.MATKMin = Character.Status.min_matk;
            p1.MDefAddition = (ushort)Character.Status.mdef_add;
            p1.MDefBase = Character.Status.mdef;
            p1.Speed = Character.Speed;
            p1.HP = (ushort)Character.MaxHP;
            p1.MP = (ushort)Character.MaxMP;
            p1.SP = (ushort)Character.MaxSP;
            uint count = 0;
            foreach (uint i in Character.Inventory.MaxVolume.Values)
            {
                count += i;
            }
            p1.Capacity = (ushort)count;
            count = 0;
            foreach (uint i in Character.Inventory.MaxPayload.Values)
            {
                count += i;
            }
            p1.Payload = (ushort)count;

            //resotre
            Character.Str = str;
            Character.Dex = dex;
            Character.Int = intel;
            Character.Agi = agi;
            Character.Vit = vit;
            Character.Mag = mag;

            StatusFactory.Instance.CalcStatus(Character);

            netIO.SendPacket(p1);
        }

        public void OnStatsUp(Packets.Client.CSMG_PLAYER_STATS_UP p)
        {
            if (Configuration.Instance.Version < SagaLib.Version.Saga13)
            {
                switch (p.Type)
                {
                    case 0:
                        if (Character.StatsPoint >= StatusFactory.Instance.RequiredBonusPoint(Character.Str))
                        {
                            Character.StatsPoint -= StatusFactory.Instance.RequiredBonusPoint(Character.Str);
                            Character.Str += 1;
                        }
                        break;
                    case 1:
                        if (Character.StatsPoint >= StatusFactory.Instance.RequiredBonusPoint(Character.Dex))
                        {
                            Character.StatsPoint -= StatusFactory.Instance.RequiredBonusPoint(Character.Dex);
                            Character.Dex += 1;
                        }
                        break;
                    case 2:
                        if (Character.StatsPoint >= StatusFactory.Instance.RequiredBonusPoint(Character.Int))
                        {
                            Character.StatsPoint -= StatusFactory.Instance.RequiredBonusPoint(Character.Int);
                            Character.Int += 1;
                        }
                        break;
                    case 3:
                        if (Character.StatsPoint >= StatusFactory.Instance.RequiredBonusPoint(Character.Vit))
                        {
                            Character.StatsPoint -= StatusFactory.Instance.RequiredBonusPoint(Character.Vit);
                            Character.Vit += 1;
                        }
                        break;
                    case 4:
                        if (Character.StatsPoint >= StatusFactory.Instance.RequiredBonusPoint(Character.Agi))
                        {
                            Character.StatsPoint -= StatusFactory.Instance.RequiredBonusPoint(Character.Agi);
                            Character.Agi += 1;
                        }
                        break;
                    case 5:
                        if (Character.StatsPoint >= StatusFactory.Instance.RequiredBonusPoint(Character.Mag))
                        {
                            Character.StatsPoint -= StatusFactory.Instance.RequiredBonusPoint(Character.Mag);
                            Character.Mag += 1;
                        }
                        break;
                }
            }
            else
            {
                if (p.Str > 0)
                {
                    for (int i = p.Str; i > 0; i--)
                    {
                        if (Character.StatsPoint >= StatusFactory.Instance.RequiredBonusPoint(Character.Str))
                        {
                            Character.StatsPoint -= StatusFactory.Instance.RequiredBonusPoint(Character.Str);
                            Character.Str += 1;
                        }
                    }
                }
                if (p.Dex > 0)
                {
                    for (int i = p.Dex; i > 0; i--)
                    {
                        if (Character.StatsPoint >= StatusFactory.Instance.RequiredBonusPoint(Character.Dex))
                        {
                            Character.StatsPoint -= StatusFactory.Instance.RequiredBonusPoint(Character.Dex);
                            Character.Dex += 1;
                        }
                    }
                }
                if (p.Int > 0)
                {
                    for (int i = p.Int; i > 0; i--)
                    {
                        if (Character.StatsPoint >= StatusFactory.Instance.RequiredBonusPoint(Character.Int))
                        {
                            Character.StatsPoint -= StatusFactory.Instance.RequiredBonusPoint(Character.Int);
                            Character.Int += 1;
                        }
                    }
                }
                if (p.Vit > 0)
                {
                    for (int i = p.Vit; i > 0; i--)
                    {
                        if (Character.StatsPoint >= StatusFactory.Instance.RequiredBonusPoint(Character.Vit))
                        {
                            Character.StatsPoint -= StatusFactory.Instance.RequiredBonusPoint(Character.Vit);
                            Character.Vit += 1;
                        }
                    }
                }
                if (p.Agi > 0)
                {
                    for (int i = p.Agi; i > 0; i--)
                    {
                        if (Character.StatsPoint >= StatusFactory.Instance.RequiredBonusPoint(Character.Agi))
                        {
                            Character.StatsPoint -= StatusFactory.Instance.RequiredBonusPoint(Character.Agi);
                            Character.Agi += 1;
                        }
                    }
                }

                if (p.Mag > 0)
                {
                    for (int i = p.Mag; i > 0; i--)
                    {
                        if (Character.StatsPoint >= StatusFactory.Instance.RequiredBonusPoint(Character.Mag))
                        {
                            Character.StatsPoint -= StatusFactory.Instance.RequiredBonusPoint(Character.Mag);
                            Character.Mag += 1;
                        }
                    }
                }

            }
            StatusFactory.Instance.CalcStatus(Character);
            SendActorHPMPSP(Character);
            SendStatus();
            SendStatusExtend();
            SendCapacity();
            //SendMaxCapacity();
            SendPlayerLevel();
        }

        public void SendWRPRanking(ActorPC pc)
        {
            Packets.Server.SSMG_ACTOR_WRP_RANKING p = new SagaMap.Packets.Server.SSMG_ACTOR_WRP_RANKING();
            p.ActorID = pc.ActorID;
            p.Ranking = pc.WRPRanking;
            netIO.SendPacket(p);
        }
        public void RevivePC(ActorPC pc)
        {
            pc.Buff.Dead = false;
            pc.HP = pc.MaxHP;
            pc.MP = pc.MaxMP;
            pc.SP = pc.MaxSP;
            if (pc.Job != PC_JOB.HAWKEYE)
                pc.EP = pc.MaxEP;

            if (pc.Job == PC_JOB.CARDINAL)
                pc.EP = 5000;

            if (pc.Job == PC_JOB.ASTRALIST)//魔法师
                pc.EP = 0;

            if (pc.Job == PC_JOB.HAWKEYE)
            {
                pc.MP = 0;
                pc.SP = 0;
                //pc.EP = 0;
            }

            if (!pc.Status.Additions.ContainsKey("HolyVolition"))
            {
                Skill.Additions.Global.DefaultBuff skill = new Skill.Additions.Global.DefaultBuff(null, pc, "HolyVolition", 2000);
                Skill.SkillHandler.ApplyAddition(pc, skill);
            }

            if (pc.SaveMap == 0)
            {
                pc.SaveMap = 91000999;
                pc.SaveX = 21;
                pc.SaveY = 21;
            }

            pc.BattleStatus = 0;
            SendChangeStatus();
            
            
            pc.Buff.紫になる = false;
            pc.Motion = MotionType.STAND;
            pc.MotionLoop = false;
            Skill.SkillHandler.Instance.ShowVessel(pc, (int)-pc.MaxHP);
            Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, pc, true);

            Skill.SkillHandler.Instance.ShowEffectByActor(pc, 5116);
            Skill.SkillHandler.Instance.CastPassiveSkills(pc);
            SendPlayerInfo();

            if (!pc.Tasks.ContainsKey("Recover"))//自然恢复
            {
                Tasks.PC.Recover reg = new Tasks.PC.Recover(FromActorPC(pc));
                pc.Tasks.Add("Recover", reg);
                reg.Activate();
            }
            if (Character.TInt["副本复活标记"] == 2)
            {
                MapInfo info = MapInfoFactory.Instance.MapInfo[(uint)Character.TInt["副本savemapid"]];
                Map.SendActorToMap(Character, (uint)Character.TInt["副本savemapid"], Global.PosX8to16((byte)Character.TInt["副本savemapX"], info.width),
                    Global.PosY8to16((byte)Character.TInt["副本savemapY"], info.height));
                Character.TInt["PVP连杀"] = 0;
            }
            if (Character.TInt["副本复活标记"] < 1 && Character.TInt["复活次数"] <= -1)
            {
                MapInfo info = MapInfoFactory.Instance.MapInfo[91000000];
                Map.SendActorToMap(Character, 91000999, Global.PosX8to16(17, info.width),
                    Global.PosY8to16(27, info.height));
                FromActorPC(Character).SendSystemMessage("你的原地复活次数已用完");
            }
            if(Character.MapID == 20020000 || Character.MapID == 20021000 || Character.MapID == 20022000)
            {
                MapInfo info = MapInfoFactory.Instance.MapInfo[10063000];
                Map.SendActorToMap(Character, 10063000, Global.PosX8to16(161, info.width),
                    Global.PosY8to16(157, info.height));
                FromActorPC(Character).SendSystemMessage("胜败乃兵家常事也，大侠请重新来过吧。");
            }
            Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, pc, true);

            if(scriptThread!=null)
            ClientManager.RemoveThread(scriptThread.Name);
            scriptThread = null;
            currentEvent = null;

            /*Scripting.Event evnt = null;
            if (evnt != null)
            {
                evnt.CurrentPC = null;
                this.scriptThread = null;
                this.currentEvent = null;
                ClientManager.RemoveThread(System.Threading.Thread.CurrentThread.Name);
                //ClientManager.LeaveCriticalArea();
            }*/
        }
        public void OnPlayerReturnHome(Packets.Client.CSMG_PLAYER_RETURN_HOME p)
        {
            if(Character.TInt["大逃杀模式"] == 1)
            {
                Character.TInt["大逃杀模式"] = 0;
                RevivePC(Character);
                map.SendActorToMap(Character, 20080009, 15, 15);
                return;
            }
            if (Character.HP > 0)
            {
                Character.Buff.Dead = false;
                Character.Buff.紫になる = false;
                Character.Motion = MotionType.NONE;
                Character.MotionLoop = false;
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, Character, true);
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, Character, true);
                SendPlayerInfo();
                return;
            }
            if (Character.MapID == 63001000)  //活动地图
            {
                FromActorPC(Character).SendSystemMessage("在幻象中无法复活。");
                return;
            }
            if (Character.TInt["副本复活标记"] == 1)
            {
                bool canrevive = true;
                if (Character.Party != null)
                {
                    if (Character.Party.Leader.TInt["复活次数"] <1)
                    {
                        Character.Buff.紫になる = true;
                        Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, Character, true);
                        foreach (var item in Character.Party.Members)
                            if (item.Value.Online && item.Value.MapID == item.Value.Party.Leader.MapID)
                                if (!item.Value.Buff.紫になる)
                                    canrevive = false;
                    }
                }
                if(!canrevive)
                {
                    Character.Buff.紫になる = true;
                    FromActorPC(Character).SendSystemMessage("你的原地复活次数已用完，请等待队伍结束挑战后方可复活。");
                    if(Character.Party != null)
                        foreach (var item in Character.Party.Members.Values)
                            if(item != null)
                                FromActorPC(item).SendSystemMessage("队友 " + Character.Name +" 进入复活等待状态。");

                    return;
                }
            }

            if (Character.TInt["副本复活标记"] == 3 && Character.TInt["复活次数"] < 1)
            {
                FromActorPC(Character).SendSystemMessage("你的原地复活次数已用完，2分钟后将复活并重置复活次数。");
                FromActorPC(Character).SendSystemMessage("你也可以随时输入/home立刻复活，并返回至据点。");
                FromActorPC(Character).SendSystemMessage("如果遇到了卡复活的问题，请输入/revive。");
                if (!Character.Tasks.ContainsKey("Revive"))
                {
                    Tasks.PC.Revive reg;
                    reg = new Tasks.PC.Revive(this, 120, Character.TInt["副本复活标记"]);
                    Character.Tasks.Add("Revive", reg);
                    reg.Activate();
                }
                return;
            }

            if (!Character.Tasks.ContainsKey("Revive") && Character.TInt["副本复活标记"] !=0 && Character.TInt["副本复活标记"] !=4)//复活计时
            {
                Tasks.PC.Revive reg;
                int time = 20;
                if (Character.TInt["副本复活标记"] == 2)
                    reg = new Tasks.PC.Revive(this, time);
                else
                    reg = new Tasks.PC.Revive(this);
                Character.Tasks.Add("Revive", reg);
                reg.Activate();
            }

            if(Character.TInt["副本复活标记"] == 1)
            {
                if (Character.Party != null)
                {
                    if (Character.Party.Leader.TInt["复活次数"] > 0)
                    {
                        Character.Party.Leader.TInt["复活次数"] -= 1;
                        foreach (var item in Character.Party.Members)
                            if (item.Value.Online)
                                FromActorPC(item.Value).SendSystemMessage(Character.Name + "使用了一次复活，现在还剩下" + Character.Party.Leader.TInt["复活次数"].ToString() + "次原地复活次数。");
                    }
                    else
                    {
                        try
                        {
                            List<Actor> actors = new List<Actor>();
                            foreach (var a in FromActorPC(Character).map.Actors)
                            {
                                actors.Add(a.Value);
                            }
                            foreach (var item in actors)
                            {
                                if (item != null)
                                    if (item.type == ActorType.MOB)
                                        if (!item.Buff.Dead)
                                            item.HP = item.MaxHP;
                            }
                            foreach (var item in Character.Party.Members)
                            {
                                if (item.Value != null && item.Value.Online && item.Value.MapID == item.Value.Party.Leader.MapID)
                                {
                                    if (!item.Value.Tasks.ContainsKey("Revive"))//自然恢复
                                    {
                                        Tasks.PC.Revive reg = new Tasks.PC.Revive(FromActorPC(item.Value));
                                        item.Value.Tasks.Add("Revive", reg);
                                        reg.Activate();
                                    }
                                    item.Value.Party.Leader.TInt["复活次数"] = item.Value.Party.Leader.TInt["设定复活次数"];
                                    FromActorPC(item.Value).SendSystemMessage("复活次数已重置");
                                }
                            }
                        }
                        catch(Exception ex)
                        {
                            SagaLib.Logger.ShowError(ex);
                        }
   
                    }
                }
            }
            if (Character.TInt["副本复活标记"] == 0 || (Character.TInt["副本复活标记"] == 1 && Character.Party == null))
            {
                FromActorPC(Character).SendSystemMessage("你在野外死亡了，等待30秒后将原地复活。");
                FromActorPC(Character).SendSystemMessage("你也可以随时输入/home立刻复活，并返回至据点。");
                FromActorPC(Character).SendSystemMessage("如果遇到了卡复活的问题，请输入/revive。");
                if (!Character.Tasks.ContainsKey("Revive"))
                {
                    Tasks.PC.Revive reg;
                    reg = new Tasks.PC.Revive(this, 30, Character.TInt["副本复活标记"]);
                    Character.Tasks.Add("Revive", reg);
                    reg.Activate();
                }

                return;
            }
            else if (Character.TInt["副本复活标记"] == 4)
            {

                FromActorPC(Character).SendSystemMessage("你死亡了，等待10秒后将原地复活。");
                FromActorPC(Character).SendSystemMessage("你也可以随时输入/home立刻复活，并返回至据点。");
                FromActorPC(Character).SendSystemMessage("如果遇到了卡复活的问题，请输入/revive。");
                if (!Character.Tasks.ContainsKey("Revive"))
                {
                    Tasks.PC.Revive reg;
                    reg = new Tasks.PC.Revive(this, 10, Character.TInt["副本复活标记"]);
                    Character.Tasks.Add("Revive", reg);
                    reg.Activate();
                }

                List<Actor> actors = new List<Actor>();
                foreach (var a in FromActorPC(Character).map.Actors)
                {
                    actors.Add(a.Value);
                }
                foreach (var item in actors)
                {
                    if (item != null)
                        if (item.type == ActorType.MOB)
                            if (!item.Buff.Dead)
                                item.HP = item.MaxHP;
                }
                return;
            }
            else if (Character.TInt["复活次数"] > 0 && Character.TInt["副本复活标记"] != 1)  //7月31日更改，复活次数限制
            {
                Character.TInt["复活次数"] -= 1;
                MapClient.FromActorPC(Character).SendSystemMessage("你经历了一次死亡，现在还剩下" + Character.TInt["复活次数"].ToString() + "次原地复活次数。");
            }
            else if (Character.TInt["复活次数"] == -2 && Character.TInt["副本复活标记"] != 1)//不回城
                MapClient.FromActorPC(Character).SendSystemMessage("啊呀——！");
            Scripting.Event evnt = null;
            if (evnt != null)
            {
                evnt.CurrentPC = null;
                scriptThread = null;
                currentEvent = null;
                ClientManager.RemoveThread(System.Threading.Thread.CurrentThread.Name);
                ClientManager.LeaveCriticalArea();
            }
        }


        public void SendDefWarChange(DefWar text)
        {
            if (Character.Online)
            {
                Packets.Server.SSMG_DEFWAR_SET p11 = new SagaMap.Packets.Server.SSMG_DEFWAR_SET();
                p11.MapID = map.ID;
                p11.Data = text;
                netIO.SendPacket(p11);
            }
        }

        public void SendDefWarResult(byte r1, byte r2, int exp, int jobexp, int cp, byte u = 0)
        {
            if (Character.Online)
            {
                Packets.Server.SSMG_DEFWAR_RESULT p11 = new SagaMap.Packets.Server.SSMG_DEFWAR_RESULT();
                p11.Result1 = r1;
                p11.Result2 = r2;
                p11.EXP = exp;
                p11.JOBEXP = jobexp;
                p11.CP = cp;

                p11.Unknown = u;
                netIO.SendPacket(p11);
            }
        }
        public void SendDefWarState(byte rate)
        {
            if (Character.Online)
            {
                Packets.Server.SSMG_DEFWAR_STATE p1 = new SagaMap.Packets.Server.SSMG_DEFWAR_STATE();
                p1.MapID = map.ID;
                p1.Rate = rate;
                netIO.SendPacket(p1);
            }
        }

        public void SendDefWarStates(Dictionary<uint, byte> list)
        {
            if (Character.Online)
            {
                Packets.Server.SSMG_DEFWAR_STATES p1 = new SagaMap.Packets.Server.SSMG_DEFWAR_STATES();
                p1.List = list;
                netIO.SendPacket(p1);
            }
        }

    }
}
