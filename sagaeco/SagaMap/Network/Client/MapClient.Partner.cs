using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;

using SagaDB;
using SagaDB.Item;
using SagaDB.Actor;
using SagaDB.Partner;
using SagaDB.Skill;
using SagaLib;
using SagaMap;
using SagaMap.Manager;
using SagaMap.Skill;

namespace SagaMap.Network.Client
{
    public partial class MapClient
    {
        #region Partner System
        public enum TALK_EVENT
        {
            SUMMONED,BATTLE,MASTERDEAD,NORMAL,JOINPARTY,LEAVEPARTY,MASTERFIGHTING,MASTERLVUP,
            MASTERQUIT,MASTERSIT,MASTERRALEX,MASTERBOW,MASTERLOGIN,LVUP,EQUIP,EAT,EATREADY
        }
        public void SendPartner(Item item)
        {
            try
            {
                if (item.ChangeMode || item.ChangeMode2)
                    return;
                if (item.BaseData.itemType == ItemType.PARTNER)
                {
                    if (item.ActorPartnerID == 0)
                    {
                        item.ActorPartnerID = MapServer.charDB.CreatePartner(item);
                    }
                    ActorPartner partner = MapServer.charDB.GetActorPartner(item.ActorPartnerID, item);
                    if (partner.nextfeedtime > DateTime.Now)
                    {
                        if (!partner.Tasks.ContainsKey("Feed"))//This is a double check here
                        {
                            uint seconds = (uint)((partner.nextfeedtime - DateTime.Now).TotalSeconds);
                            Tasks.Partner.Feed task = new Tasks.Partner.Feed(this,partner, seconds);
                            partner.Tasks.Add("Feed", task);
                            task.Activate();
                        }
                    }
                    else
                        partner.reliabilityuprate = 100;
                    partner.Range = 1;
                    partner.BaseData.range = 1;
                    Character.Partner = partner;
                    partner.MapID = this.Character.MapID;
                    partner.X = this.Character.X;
                    partner.Y = this.Character.Y;
                    partner.Dir = Character.Dir;

                    //此段代码可能在17-11-06 20：57:21时造成了一次死锁，因此加上try catch!
                    try
                    {
                        //取自己背后的点。这里可以用一个for循环遍历8个点，找到符合条件点时在if中break出去，来实现在周围8格放伙伴（没有可以放的点时会放在自己脚底下）
                        short[,] posI = new short[,] { { 0, 1 }, { 1, 1 }, { 1, 0 }, { 1, -1 }, { 0, -1 }, { -1, -1 }, { -1, 0 }, { -1, 1 } };
                        int NDIR = ((Character.Dir + 22) / 45) % 8;
                        short[] pos = { (short)(Character.X + posI[NDIR, 0] * 100), (short)(Character.Y + posI[NDIR, 1] * 100) };
                        if (map.Info.walkable[Global.PosX16to8(pos[0], map.Width), Global.PosY16to8(pos[1], map.Height)] == 2)
                        {
                            partner.X = pos[0];
                            partner.Y = pos[1];
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.ShowError(ex);
                        partner.X = this.Character.X;
                        partner.Y = this.Character.Y;
                    }

                    partner.Owner = Character;
                   
                    ActorEventHandlers.PartnerEventHandler eh = new ActorEventHandlers.PartnerEventHandler(partner);
                    partner.e = eh;
                    if (Partner.PartnerAIFactory.Instance.Items.ContainsKey(item.BaseData.petID))
                        eh.AI.Mode = Partner.PartnerAIFactory.Instance.Items[item.BaseData.petID];
                    else
                        eh.AI.Mode = new Partner.AIMode(0);
                    SetPartnerSkills(partner);
                    eh.AI.Start();
                    if (!eh.AI.Hate.ContainsKey(Character.ActorID))
                        eh.AI.Hate.Add(Character.ActorID, 10);
                    //Mob.AIThread.Instance.RegisterAI(eh.AI);
                    /*if (!partner.Tasks.ContainsKey("ReliabilityGrow"))
                    {
                        Tasks.Partner.ReliabilityGrow task = new Tasks.Partner.ReliabilityGrow(partner);
                        partner.Tasks.Add("ReliabilityGrow", task);
                        task.Activate();
                    }*/
                    //信赖度关闭
                    this.map.RegisterActor(partner);
                    partner.invisble = false;
                    this.map.OnActorVisibilityChange(partner);
                    this.map.SendVisibleActorsToActor(partner);
                    partner.HP = partner.MaxHP;
                    Tasks.Partner.TalkAtFreeTime taft = new Tasks.Partner.TalkAtFreeTime(this);
                    partner.Tasks.Add("TalkAtFreeTime", taft);
                    taft.Activate();

                }
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }
        public void SetPartnerSkills(ActorPartner partner)
        {
            //比较呆，需要完善，只做了主动技能！
            ActorEventHandlers.PartnerEventHandler eh = (ActorEventHandlers.PartnerEventHandler)partner.e;
            if (partner.equipcubes_activeskill.Count > 0)
            {
                eh.AI.Mode.EventAttacking.Clear();
                for (int i = 0; i < partner.equipcubes_activeskill.Count; i++)
                {
                    ushort cubeid = partner.equipcubes_activeskill[i];
                    if (PartnerFactory.Instance.actcubes_db_uniqueID.ContainsKey(cubeid))
                    {
                        ActCubeData acd = PartnerFactory.Instance.actcubes_db_uniqueID[cubeid];

                        eh.AI.Mode.EventAttacking.Add(acd.skillID, 20);
                        eh.AI.Mode.EventAttackingSkillRate = 10;
                    }
                }
            }
        }
        public void DeletePartner()
        {
            if (this.Character.Partner == null)
                return;
            /*if (this.Character.Pet.Ride)
            {
                return;
            }*/
            ActorPartner partner = this.Character.Partner;
            ActorEventHandlers.PartnerEventHandler eh = (ActorEventHandlers.PartnerEventHandler)this.Character.Partner.e;
            eh.AI.Pause();
            eh.AI.Activated = false;
            if (partner.Tasks.ContainsKey("Feed"))
            {
                partner.Tasks["Feed"].Deactivate();
                partner.Tasks.Remove("Feed");
            }
            if (partner.Tasks.ContainsKey("ReliabilityGrow"))
            {
                partner.Tasks["ReliabilityGrow"].Deactivate();
                partner.Tasks.Remove("ReliabilityGrow");
            }
            if (partner.Tasks.ContainsKey("TalkAtFreeTime"))
            {
                partner.Tasks["TalkAtFreeTime"].Deactivate();
                partner.Tasks.Remove("TalkAtFreeTime");
            }
            MapServer.charDB.SavePartner(this.Character.Partner);
            Manager.MapManager.Instance.GetMap(this.Character.Partner.MapID).DeleteActor(this.Character.Partner);
            
            
            this.Character.Partner = null;
        }
        void Speak(Actor actor, string message)
        {
            if (message == "") return;
            ChatArg arg = new ChatArg();
            arg.content = message;
            MapManager.Instance.GetMap(actor.MapID).SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAT, arg, actor, false);
        }
        public void PartnerTalking(ActorPartner partner, TALK_EVENT type, int rate)
        {
            PartnerTalking(partner, type, rate, 20000);
        }
        string getmessage(List<string>m)
        {
            string s = "";
            if (m.Count == 0) return s;
            if (m.Count == 1)
            {
                s = m[0];
                s = s.Replace("name", Character.Name);
                return s;
            };
            s = m[Global.Random.Next(0, m.Count - 1)];
            s = s.Replace("name", Character.Name);
            return s;
        }
        public void PartnerTalking(ActorPartner partner, TALK_EVENT type,int rate,int delay)
        {
            if (partner == null) return;
            if (Global.Random.Next(0, 100) > rate) return;
            if (partner.Status.Additions.ContainsKey("PartnerShutUp") && delay != 0) return;
            if (delay > 0)
            {
                if (!partner.Status.Additions.ContainsKey("PartnerShutUp") && type != TALK_EVENT.NORMAL)
                {
                    Skill.Additions.Global.OtherAddition cd = new Skill.Additions.Global.OtherAddition(null, partner, "PartnerShutUp", delay);
                    SkillHandler.ApplyAddition(partner, cd);
                }
            }
            if (type != TALK_EVENT.NORMAL)
            {
                if (!partner.Status.Additions.ContainsKey("NotAtFreeTime"))
                {
                    Skill.Additions.Global.OtherAddition cd = new Skill.Additions.Global.OtherAddition(null, partner, "NotAtFreeTime", 50000);
                    SkillHandler.ApplyAddition(partner, cd);
                }
                else
                {
                    Addition cd = partner.Status.Additions["NotAtFreeTime"];
                    TimeSpan span = new TimeSpan(0, 0, 0, 0, 50000);
                }
            }
            uint id = partner.BaseData.id;
            TalkInfo ti = PartnerFactory.Instance.GetPartnerTalks(id);
            if (ti == null) return;
            switch (type)
            {
                case TALK_EVENT.SUMMONED:
                    if (ti.Onsummoned.Count > 0)
                        Speak(partner, getmessage(ti.Onsummoned));
                    break;
                case TALK_EVENT.BATTLE:
                    if (ti.OnBattle.Count > 0)
                        Speak(partner, getmessage(ti.OnBattle));
                    break;
                case TALK_EVENT.MASTERDEAD:
                    if (ti.OnMasterDead.Count > 0)
                        Speak(partner, getmessage(ti.OnMasterDead));
                    break;
                case TALK_EVENT.NORMAL:
                    if (ti.OnNormal.Count > 0)
                        Speak(partner, getmessage(ti.OnNormal));
                    break;
                case TALK_EVENT.JOINPARTY:
                    if (ti.OnJoinParty.Count > 0)
                        Speak(partner, getmessage(ti.OnJoinParty));
                    break;
                case TALK_EVENT.LEAVEPARTY:
                    if (ti.OnLeaveParty.Count > 0)
                        Speak(partner, getmessage(ti.OnLeaveParty));
                    break;
                case TALK_EVENT.MASTERFIGHTING:
                    if (ti.OnMasterFighting.Count > 0)
                        Speak(partner, getmessage(ti.OnMasterFighting));
                    break;
                case TALK_EVENT.MASTERLVUP:
                    if (ti.OnMasterLevelUp.Count > 0)
                        Speak(partner, getmessage(ti.OnMasterLevelUp));
                    break;
                case TALK_EVENT.MASTERQUIT:
                    if (ti.OnMasterQuit.Count > 0)
                        Speak(partner, getmessage(ti.OnMasterQuit));
                    break;
                case TALK_EVENT.MASTERSIT:
                    if (ti.OnMasterSit.Count > 0)
                        Speak(partner, getmessage(ti.OnMasterSit));
                    break;
                case TALK_EVENT.MASTERRALEX:
                    if (ti.OnMasterRelax.Count > 0)
                        Speak(partner, getmessage(ti.OnMasterRelax));
                    break;
                case TALK_EVENT.MASTERBOW:
                    if (ti.OnMasterBow.Count > 0)
                        Speak(partner, getmessage(ti.OnMasterBow));
                    break;
                case TALK_EVENT.MASTERLOGIN:
                    if (ti.OnMasterLogin.Count > 0)
                        Speak(partner, getmessage(ti.OnMasterLogin));
                    break;
                case TALK_EVENT.LVUP:
                    if (ti.OnLevelUp.Count > 0)
                        Speak(partner, getmessage(ti.OnLevelUp));
                    break;
                case TALK_EVENT.EQUIP:
                    if (ti.OnEquip.Count > 0)
                        Speak(partner, getmessage(ti.OnEquip));
                    break;
                case TALK_EVENT.EAT:
                    if (ti.OnEat.Count > 0)
                        Speak(partner, getmessage(ti.OnEat));
                    break;
                case TALK_EVENT.EATREADY:
                    if (ti.OnEatReady.Count > 0)
                        Speak(partner, getmessage(ti.OnEatReady));
                    break;
            }
        }
        public void AddPartnerItemSkills(Item item)
        {
            if (item.BaseData.possibleSkill != 0)//装备附带主动技能
            {
                SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(item.BaseData.possibleSkill, 1);
                if (skill != null)
                {
                    if (!this.Character.Partner.Skills.ContainsKey(item.BaseData.possibleSkill))
                    {
                        this.Character.Partner.Skills.Add(item.BaseData.possibleSkill, skill);
                    }
                }
            }
            if (item.BaseData.passiveSkill != 0)//装备附带被动技能
            {
                SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(item.BaseData.passiveSkill, 1);
                if (skill != null)
                {
                    if (!this.Character.Partner.Skills.ContainsKey(item.BaseData.passiveSkill))
                    {
                        this.Character.Partner.Skills.Add(item.BaseData.passiveSkill, skill);
                        if (!skill.BaseData.active)
                        {
                            SkillArg arg = new SkillArg();
                            arg.skill = skill;
                            SkillHandler.Instance.SkillCast(this.Character.Partner, this.Character.Partner, arg);
                        }
                    }
                }
            }
        }
        public void CleanPartnerItemSkills(Item item)
        {
            if (item.BaseData.possibleSkill != 0)//装备附带主动技能
            {
                SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(item.BaseData.possibleSkill, 1);
                if (skill != null)
                {
                    if (this.Character.Partner.Skills.ContainsKey(item.BaseData.possibleSkill))
                    {
                        this.Character.Partner.Skills.Remove(item.BaseData.possibleSkill);
                    }
                }
            }
            if (item.BaseData.passiveSkill != 0)//装备附带被动技能
            {
                SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(item.BaseData.passiveSkill, 1);
                if (skill != null)
                {
                    if (this.Character.Partner.Skills.ContainsKey(item.BaseData.passiveSkill))
                    {
                        this.Character.Partner.Skills.Remove(item.BaseData.passiveSkill);
                        //SkillHandler.Instance.CastPassiveSkills(this.Character); to be checked
                    }
                }
            }
        }
        /// <summary>
        /// partner装备卸下过程，卸下该格子里的装备对应的所有格子里的道具，并移除道具附加的技能
        /// </summary>
        /// <param name="p"></param>
        public void OnPartnerItemUnequipt(EnumPartnerEquipSlot eqslot)
        {
            if (!this.Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                return;
            if (!this.Character.Inventory.Equipments[EnumEquipSlot.PET].IsPartner)
                return;
            ActorPartner partner = this.Character.Partner;
            if (!partner.equipments.ContainsKey(eqslot))
                return;
            Item oriItem = partner.equipments[eqslot];
            CleanPartnerItemSkills(oriItem);
            foreach (EnumPartnerEquipSlot i in oriItem.PartnerEquipSlot)
            {
                if (partner.equipments.ContainsKey(i))
                {
                    if (partner.equipments[i].Stack == 0)
                    {
                        partner.equipments.Remove(i);
                    }
                    else
                    {
                        AddItem(partner.equipments[i], false);
                        partner.equipments.Remove(i);
                    }
                }
            }
            MapServer.charDB.SavePartnerEquip(partner);
            Packets.Server.SSMG_PARTNER_EQUIP_RESULT p = new Packets.Server.SSMG_PARTNER_EQUIP_RESULT();
            p.PartnerInventorySlot = this.Character.Inventory.Equipments[EnumEquipSlot.PET].Slot;
            p.EquipItemID = oriItem.ItemID;
            p.PartnerEquipSlot = eqslot;
            p.MoveType = 1;
            this.netIO.SendPacket(p);

            Partner.StatusFactory.Instance.CalcPartnerStatus(partner);
            
            //broadcast
            SendPetBasicInfo();
            SendPetDetailInfo();
            PC.StatusFactory.Instance.CalcStatus(Character);
            SendPlayerInfo();
        }
        /// <summary>
        /// 检查partner道具装备条件
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public int CheckPartnerEquipRequirement(Item item)
        {
            if (this.Character.Buff.Dead || this.Character.Buff.Confused || this.Character.Buff.Frosen || this.Character.Buff.Paralysis || this.Character.Buff.Sleep || this.Character.Buff.Stone || this.Character.Buff.Stun)
                return -3;
            byte lv = this.Character.Partner.Level;
            //if (lv < item.BaseData.possibleLv)
              //  return -15;
            if ((item.BaseData.itemType == ItemType.RIDE_PET || item.BaseData.itemType == ItemType.RIDE_PARTNER) && this.Character.Marionette != null)
                return -2;
            if (item.BaseData.possibleRebirth)
                if (!this.Character.Partner.rebirth)
                    return -31;
            return 0;
        }
        /// <summary>
        /// 加点预览
        /// </summary>
        /// <param name="p"></param>
        public void OnPartnerPerkPreview(Packets.Client.CSMG_PARTNER_PERK_PREVIEW p)
        {
            if (!this.Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                return;
            if (!this.Character.Inventory.Equipments[EnumEquipSlot.PET].IsPartner)
                return;

            byte p0, p1, p2, p3, p4, p5;
            ActorPartner partner = this.Character.Partner;
            if (partner == null) return;
            p0 = partner.perk0;
            p1 = partner.perk1;
            p2 = partner.perk2;
            p3 = partner.perk3;
            p4 = partner.perk4;
            p5 = partner.perk5;


            partner.perk0 = p.Perk0;
            partner.perk1 = p.Perk1;
            partner.perk2 = p.Perk2;
            partner.perk3 = p.Perk3;
            partner.perk4 = p.Perk4;
            partner.perk5 = p.Perk5;

            Partner.StatusFactory.Instance.CalcPartnerStatus(partner);


            Packets.Server.SSMG_PARTNER_PERK_PREVIEW pp2 = new Packets.Server.SSMG_PARTNER_PERK_PREVIEW();
            pp2.MaxPhyATK = partner.Status.max_atk1;
            pp2.MinPhyATK = partner.Status.min_atk1;

            pp2.MaxMAGATK = partner.Status.max_matk;
            pp2.MinMAGATK = partner.Status.min_matk;

            pp2.DEFAdd = (ushort)partner.Status.def_add;
            pp2.MaxHP = partner.MaxHP;
            pp2.DEF = partner.Status.def;

            pp2.MDEFAdd = (ushort)partner.Status.mdef_add;
            pp2.CSPD = partner.Status.cspd;
            pp2.MDEF = partner.Status.mdef;

            pp2.LongHit = partner.Status.hit_ranged;
            pp2.ShortHit = partner.Status.hit_melee;

            pp2.LongAvoid = partner.Status.avoid_ranged;
            pp2.ShortAvoid = partner.Status.avoid_melee;
            pp2.ASPD = partner.Status.aspd;

            partner.perk0 = p0;
            partner.perk1 = p1;
            partner.perk2 = p2;
            partner.perk3 = p3;
            partner.perk4 = p4;
            partner.perk5 = p5;

            Partner.StatusFactory.Instance.CalcPartnerStatus(partner);

            this.netIO.SendPacket(pp2);
        }
        /// <summary>
        /// 加点确认
        /// </summary>
        /// <param name="p"></param>
        public void OnPartnerPerkConfirm(Packets.Client.CSMG_PARTNER_PERK_CONFIRM p)
        {
            if (!this.Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                return;
            if (!this.Character.Inventory.Equipments[EnumEquipSlot.PET].IsPartner)
                return;
            Packets.Server.SSMG_PARTNER_PERK_CONFIRM pp2 = new Packets.Server.SSMG_PARTNER_PERK_CONFIRM();
            ActorPartner partner = this.Character.Partner;
            if (partner == null) return;
            if(p.Perk0 > 0)
            {
                for (int i = 0; i < p.Perk0; i++)
                {
                    if(partner.perkpoint >= 1)
                    {
                        partner.perkpoint--;
                        partner.perk0++;
                    }
                }
            }
            if (p.Perk1 > 0)
            {
                for (int i = 0; i < p.Perk1; i++)
                {
                    if (partner.perkpoint >= 1)
                    {
                        partner.perkpoint--;
                        partner.perk1++;
                    }
                }
            }
            if (p.Perk2 > 0)
            {
                for (int i = 0; i < p.Perk2; i++)
                {
                    if (partner.perkpoint >= 1)
                    {
                        partner.perkpoint--;
                        partner.perk2++;
                    }
                }
            }
            if (p.Perk3 > 0)
            {
                for (int i = 0; i < p.Perk3; i++)
                {
                    if (partner.perkpoint >= 1)
                    {
                        partner.perkpoint--;
                        partner.perk3++;
                    }
                }
            }
            if (p.Perk4 > 0)
            {
                for (int i = 0; i < p.Perk4; i++)
                {
                    if (partner.perkpoint >= 1)
                    {
                        partner.perkpoint--;
                        partner.perk4++;
                    }
                }
            }
            if (p.Perk5 > 0)
            {
                for (int i = 0; i < p.Perk5; i++)
                {
                    if (partner.perkpoint >= 1)
                    {
                        partner.perkpoint--;
                        partner.perk5++;
                    }
                }
            }

            Partner.StatusFactory.Instance.CalcPartnerStatus(partner);
            pp2.InventorySlot = Character.Inventory.Equipments[EnumEquipSlot.PET].Slot;

            pp2.Perkpoints = partner.perkpoint;
            pp2.Perk0 = partner.perk0;
            pp2.Perk1 = partner.perk1;
            pp2.Perk2 = partner.perk2;
            pp2.Perk3 = partner.perk3;
            pp2.Perk4 = partner.perk4;
            pp2.Perk5 = partner.perk5;

            pp2.MaxPhyATK = partner.Status.max_atk1;
            pp2.MinPhyATK = partner.Status.min_atk1;

            pp2.MaxMAGATK = partner.Status.max_matk;
            pp2.MinMAGATK = partner.Status.min_matk;

            pp2.DEFAdd = (ushort)partner.Status.def_add;
            pp2.MaxHP = partner.MaxHP;
            pp2.DEF = partner.Status.def;

            pp2.MDEFAdd = (ushort)partner.Status.mdef_add;
            pp2.CSPD = partner.Status.cspd;
            pp2.MDEF = partner.Status.mdef;

            pp2.LongHit = partner.Status.hit_ranged;
            pp2.ShortHit = partner.Status.hit_melee;

            pp2.LongAvoid = partner.Status.avoid_ranged;
            pp2.ShortAvoid = partner.Status.avoid_melee;
            pp2.ASPD = partner.Status.aspd;

            this.netIO.SendPacket(pp2);
            MapServer.charDB.SavePartner(partner);
            SendPetBasicInfo();
            SendPetDetailInfo();
            SagaMap.PC.StatusFactory.Instance.CalcStatus(this.Character);
        }
        /// <summary>
        /// 装备partner道具
        /// </summary>
        public void OnPartnerItemEquipt(Packets.Client.CSMG_PARTNER_SETEQUIP p)
        {
            if (!this.Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                return;
            if (!this.Character.Inventory.Equipments[EnumEquipSlot.PET].IsPartner)
                return;
            ActorPartner partner = this.Character.Partner;
            if (p.EquipItemInventorySlot == 0xFFFFFFFF)
            {
                OnPartnerItemUnequipt(p.PartnerEquipSlot);
                return;
            }
            Item item = this.Character.Inventory.GetItem(p.EquipItemInventorySlot); //item是这次装备的装备
            if (item == null)
                    return;
            ushort count = item.Stack;//count是实际移动数量

            int result;//返回不能装备的类型
            result = CheckPartnerEquipRequirement(item);//检查装备条件
            if (result < 0)//不能装备
            {
                Packets.Server.SSMG_ITEM_EQUIP p4 = new SagaMap.Packets.Server.SSMG_ITEM_EQUIP();
                p4.InventorySlot = 0xffffffff;
                p4.Target = ContainerType.NONE;
                p4.Result = result;
                p4.Range = this.Character.Range;
                this.netIO.SendPacket(p4);
                return;
            }
            List<EnumPartnerEquipSlot> targetslots = new List<EnumPartnerEquipSlot>(); //PartnerEquipSlot involved in this item target slots
            foreach (EnumPartnerEquipSlot i in item.PartnerEquipSlot)
            {
                if (!targetslots.Contains(i))
                    targetslots.Add(i);
            }
            //卸下
            foreach (EnumPartnerEquipSlot i in targetslots)
            {
                //检查
                if (!partner.equipments.ContainsKey(i))
                {
                    //该格子原来就是空的 直接下一个格子 特殊检查在循环外写
                    continue;
                }
                //卸下
                //foreach (EnumPartnerEquipSlot j in partner.equipments[i].EquipSlot)
                {
                    //j位置的装备正式卸下
                    if (partner.equipments[i].BaseData.itemType == ItemType.UNION_WEAPON)
                        OnPartnerItemUnequipt(EnumPartnerEquipSlot.WEAPON);
                    else
                        OnPartnerItemUnequipt(EnumPartnerEquipSlot.COSTUME);
                }
            }
            if (count == 0) return;
            DeleteItem(item.Slot, 1, false);
            if (item.Stack > 0)
            {
                partner.equipments.Add(item.PartnerEquipSlot[0], item);//maybe check this in the future
                Packets.Server.SSMG_PARTNER_EQUIP_RESULT pr = new Packets.Server.SSMG_PARTNER_EQUIP_RESULT();
                pr.PartnerInventorySlot = this.Character.Inventory.Equipments[EnumEquipSlot.PET].Slot;
                pr.EquipItemID = item.ItemID;
                pr.PartnerEquipSlot = item.PartnerEquipSlot[0];
                pr.MoveType = 0;
                this.netIO.SendPacket(pr);
            }
            //renew stauts
            MapServer.charDB.SavePartnerEquip(partner);
            AddPartnerItemSkills(item);
            Partner.StatusFactory.Instance.CalcPartnerStatus(partner);
            //broadcast
            SendPetBasicInfo();
            SendPetDetailInfo();
            PC.StatusFactory.Instance.CalcStatus(Character);
            SendPlayerInfo();
        }

        public void OnPartnerFoodListSet(Packets.Client.CSMG_PARTNER_SETFOOD p)
        {
            if (!this.Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                return;
            if (!this.Character.Inventory.Equipments[EnumEquipSlot.PET].IsPartner)
                return;
            ActorPartner partner = this.Character.Partner;
            byte foodslot = 0;
            if (p.MoveType == 1)//into
            {
                partner.foods.Add(ItemFactory.Instance.GetItem(p.ItemID));
                for (int i = 0; i < partner.foods.Count; i++)
                {
                    if (partner.foods[i].ItemID == p.ItemID)
                        foodslot = (byte)i;
                }
                //foodslot = (byte)(partner.foods.IndexOf(ItemFactory.Instance.GetItem(p.ItemID)));
            }
            else//out
            {
                //foodslot = (byte)(partner.foods.IndexOf(ItemFactory.Instance.GetItem(p.ItemID)));
                for (int i = 0; i < partner.foods.Count; i++)
                {
                    if (partner.foods[i].ItemID == p.ItemID)
                        foodslot = (byte)i;
                }
                partner.foods.RemoveAt(foodslot);
            }
            Packets.Server.SSMG_PARTNER_FOOD_LIST_RESULT pr = new Packets.Server.SSMG_PARTNER_FOOD_LIST_RESULT();
            pr.FoodSlot = foodslot;
            pr.MoveType = p.MoveType;
            pr.FoodItemID = p.ItemID;
            this.netIO.SendPacket(pr);
        }
        float GetReliabilityRate(byte reliability)
        {
            switch(reliability)
            {
                case 0:
                    return 0f;
                case 1:
                    return 0.1f;
                case 2:
                    return 0.2f;
                case 3:
                    return 0.3f;
                case 4:
                    return 0.4f;
                case 5:
                    return 0.5f;
                case 6:
                    return 0.6f;
                case 7:
                    return 0.7f;
                case 8:
                    return 0.8f;
                case 9:
                    return 0.9f;
            }
            return 1f;
        }
        public bool OnPartnerFeed(uint FoodInventorySlot)
        {
            if (!this.Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                return false;
            if (!this.Character.Inventory.Equipments[EnumEquipSlot.PET].IsPartner)
                return false;
            Item item = this.Character.Inventory.GetItem(FoodInventorySlot);
            if (item == null)
            {
                return false;
            }
            PartnerFood food = PartnerFactory.Instance.GetPartnerFood(item.ItemID);
            ActorPartner partner = this.Character.Partner;
            if (partner == null) return false;
            if(partner.Tasks.ContainsKey("Feed"))
            {
                SendSystemMessage("伙伴已经饱了哦");
                return false;
            }
            uint fexp = food.rankexp;
            if (partner.reliability >=4 && !partner.rebirth)
            {
                fexp = 0;
                SendSystemMessage("伙伴 " + partner.Name + " 已经达到未转生前的最大信赖度");
            }
            ExperienceManager.Instance.ApplyPartnerReliabilityEXP(partner, fexp);
            string nextlvup = "";
            if(partner.reliability + 1 < 10)
                nextlvup = "(" + partner.reliabilityexp + "/" + ExperienceManager.Instance.PartnerReliabilityEXPChart[(byte)(partner.reliability + 1)] + ")";
            SendSystemMessage("伙伴 " + partner.Name + " 获得了" + fexp + "点信赖经验值"+ nextlvup);
            ExperienceManager.Instance.ApplyPartnerLvExp(Character, food.rankexp);
            partner.reliabilityuprate = (ushort)(food.reliabilityuprate + GetReliabilityRate(partner.reliability));
            int second = (int)food.nextfeedtime;
            partner.nextfeedtime = DateTime.Now + new TimeSpan(0, 0, second);
            //need food calculation here if rank changes need to send rank_update //check if ok to eat at this time
            if (!partner.Tasks.ContainsKey("Feed"))//This is a double check here
            {
                Tasks.Partner.Feed task = new Tasks.Partner.Feed(this,partner, food.nextfeedtime);
                partner.Tasks.Add("Feed", task);
                task.Activate();
            }
            //DeleteItem(item.Slot, 1, false);
            Packets.Server.SSMG_PARTNER_FEED_RESULT pr = new Packets.Server.SSMG_PARTNER_FEED_RESULT();
            pr.MessageSwitch = 0;
            pr.PartnerInventorySlot = this.Character.Inventory.Equipments[EnumEquipSlot.PET].Slot;
            pr.FoodItemID = food.itemID;
            pr.ReliabilityUpRate = partner.reliabilityuprate;
            if (partner.nextfeedtime > DateTime.Now)
                pr.NextFeedTime = (uint)(partner.nextfeedtime - DateTime.Now).TotalSeconds;
            else
                pr.NextFeedTime = 0;
            pr.PartnerRank = this.Character.Partner.rank;
            this.netIO.SendPacket(pr);
            Partner.StatusFactory.Instance.CalcPartnerStatus(partner);
            SendPetBasicInfo();
            SendPetDetailInfo();
            PC.StatusFactory.Instance.CalcStatus(this.Character);
            if (food.itemID == 168500107)//成功使用【超级棒棒糖Ver.A】10次
                TitleProccess(Character, 29, 1);
            return true;
        }

        public void OnPartnerCubeLearn(uint CubeItemID)
        {
            if (!this.Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                return;
            if (!this.Character.Inventory.Equipments[EnumEquipSlot.PET].IsPartner)
                return;
            Item item = ItemFactory.Instance.GetItem(CubeItemID);
            if (item == null)
            {
                return;
            }
            ActCubeData cube = PartnerFactory.Instance.GetCubeItemID(item.ItemID);
            switch (cube.cubetype)
            {
                case PartnerCubeType.CONDITION:
                    if (!Character.Partner.equipcubes_condition.Contains(cube.uniqueID))
                        Character.Partner.equipcubes_condition.Add(cube.uniqueID);
                    else
                    {
                        SendSystemMessage("这个技能块已经学习过了。");
                        return;
                    }
                    break;
                case PartnerCubeType.ACTION:
                    if (!Character.Partner.equipcubes_action.Contains(cube.uniqueID))
                        Character.Partner.equipcubes_action.Add(cube.uniqueID);
                    else
                    {
                        SendSystemMessage("这个技能块已经学习过了。");
                        return;
                    }
                    break;
                case PartnerCubeType.ACTIVESKILL:
                    if (!Character.Partner.equipcubes_activeskill.Contains(cube.uniqueID))
                        Character.Partner.equipcubes_activeskill.Add(cube.uniqueID);
                    else
                    {
                        SendSystemMessage("这个技能块已经学习过了。");
                        return;
                    }
                    break;
                case PartnerCubeType.PASSIVESKILL:
                    if (!Character.Partner.equipcubes_passiveskill.Contains(cube.uniqueID))
                        Character.Partner.equipcubes_passiveskill.Add(cube.uniqueID);
                    else
                    {
                        SendSystemMessage("这个技能块已经学习过了。");
                        return;
                    }
                    break;
            }
            DeleteItemID(CubeItemID, 1, true);
            SetPartnerSkills(Character.Partner);
            SendSystemMessage("技能块【"+ cube.cubename +"】学习成功了！");
            OnPartnerAIOpen();
            MapServer.charDB.SavePartnerCube(Character.Partner);
        }

        public void OnPartnerCubeDelete(Packets.Client.CSMG_PARTNER_CUBE_DELETE p)
        {
            ushort cubeID = p.CubeID;
            if (Character.Partner.equipcubes_activeskill.Contains(cubeID))
                Character.Partner.equipcubes_activeskill.Remove(cubeID);
            if (Character.Partner.equipcubes_passiveskill.Contains(cubeID))
                Character.Partner.equipcubes_passiveskill.Remove(cubeID);
            if (Character.Partner.equipcubes_action.Contains(cubeID))
                Character.Partner.equipcubes_action.Remove(cubeID);
            if (Character.Partner.equipcubes_condition.Contains(cubeID))
                Character.Partner.equipcubes_condition.Remove(cubeID);
            SetPartnerSkills(Character.Partner);
            OnPartnerAIOpen();
            MapServer.charDB.SavePartnerCube(Character.Partner);
        }
        public void OnPartnerAIOpen()
        {
            try
            {
                if (!this.Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                    return;
                if (!this.Character.Inventory.Equipments[EnumEquipSlot.PET].IsPartner)
                    return;
                ActorPartner partner = this.Character.Partner;
                Packets.Server.SSMG_PARTNER_AI_DETAIL pr = new Packets.Server.SSMG_PARTNER_AI_DETAIL((byte)(partner.equipcubes_condition.Count), (byte)(partner.equipcubes_action.Count), (byte)(partner.equipcubes_activeskill.Count), (byte)(partner.equipcubes_passiveskill.Count));
                pr.PartnerInventorySlot = this.Character.Inventory.Equipments[EnumEquipSlot.PET].Slot;
                pr.Conditions_ID = partner.ai_conditions;
                pr.Reactions_ID = partner.ai_reactions;
                pr.Time_Intervals = partner.ai_intervals;
                pr.AI_states = partner.ai_states;
                pr.BasicAI = partner.basic_ai_mode;
                pr.Cubes_Condition = partner.equipcubes_condition;
                pr.Cubes_Action = partner.equipcubes_action;
                pr.Cubes_Activeskill = partner.equipcubes_activeskill;
                pr.Cubes_Passiveskill = partner.equipcubes_passiveskill;
                netIO.SendPacket(pr);
                SendSystemMessage("【提示】宠物AI系统正在完善及测试中！由于AI系统的复杂性，该系统需要较长时间完善，敬请见谅。");
            }
            catch(Exception ex)
            {
                SagaLib.Logger.ShowError(ex);
            }
        }

        public void OnPartnerAISetup(Packets.Client.CSMG_PARTNER_AI_DETAIL_SETUP p)
        {
            if (!this.Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                return;
            if (!this.Character.Inventory.Equipments[EnumEquipSlot.PET].IsPartner)
                return;
            //SendSystemMessage("当前版本无法自定义宠物AI");
            //return;
            ActorPartner partner = this.Character.Partner;
            partner.ai_conditions = p.Conditions_ID;
            partner.ai_reactions = p.Reactions_ID;
            partner.ai_intervals = p.Time_Intervals;
            partner.ai_states = p.AI_states;
            partner.basic_ai_mode = p.BasicAI;
            Packets.Server.SSMG_PARTNER_AI_SETUP_RESULT pr = new Packets.Server.SSMG_PARTNER_AI_SETUP_RESULT();
            pr.Success = 0;
            pr.PartnerInventorySlot = this.Character.Inventory.Equipments[EnumEquipSlot.PET].Slot;
            this.netIO.SendPacket(pr);
        }

        public void OnPartnerAIClose()
        { }

        public void OnPartnerAIModeSelection(Packets.Client.CSMG_PARTNER_AI_MODE_SELECTION p)
        {
            if (!this.Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                return;
            if (!this.Character.Inventory.Equipments[EnumEquipSlot.PET].IsPartner)
                return;
            if (p.AIMode >= 3)
            {
                SendSystemMessage("当前版本无法自定义宠物AI");
                return;
            }
            ActorPartner partner = Character.Partner;
            partner.ai_mode = p.AIMode;
            Packets.Server.SSMG_PARTNER_AI_MODE_SELECTION pr = new Packets.Server.SSMG_PARTNER_AI_MODE_SELECTION();
            pr.PartnerInventorySlot = this.Character.Inventory.Equipments[EnumEquipSlot.PET].Slot;
            pr.AIMode = partner.ai_mode;
            this.netIO.SendPacket(pr);
        }
        /// <summary>
        /// 宠物PY请求
        /// </summary>
        /// <param name="p"></param>
        public void OnPartnerItemPY(Packets.Client.CSMG_PARTNER_PY_REQUEST p)
        {
            Item partneritem = Character.Inventory.GetItem(p.Slot);
            if (Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET) && partneritem == Character.Inventory.Equipments[EnumEquipSlot.PET])
            {
                SendSystemMessage("正在装备的伙伴不能进行凭依");
                return;
            }
            if (partneritem.ActorPartnerID==0)
            {
                SendSystemMessage("这个伙伴还没有分配过能力——请先为其分配能力再进行伙伴凭依");
                return;
            }
            //ActorPartner partner = Character.Partner;
            //if (partner == null) return;
            //if (Pict == partner.BaseData.pictid) return;
            if (partneritem != null)
            {
                ActorPartner partner = MapServer.charDB.GetActorPartner(partneritem.ActorPartnerID, partneritem);
                uint Pict = partneritem.BaseData.petID;
                Partner.StatusFactory.Instance.CalcPartnerStatus(partner);
                switch (p.Type)
                {
                    case 0:
                        if (Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                        {
                            Character.PossessionPartnerSlotIDinRightHand = Pict;
                            Character.Status.max_atk1_petpy = (short)(partner.Status.max_atk1 / 2);
                            Character.Status.min_atk1_petpy = (short)(partner.Status.min_atk1 / 2);
                            Character.Status.max_matk_petpy = (short)(partner.Status.max_matk / 2);
                            Character.Status.min_matk_petpy = (short)(partner.Status.min_matk / 2);
                        }
                        else
                        {
                            Character.PossessionPartnerSlotIDinRightHand = 0;
                            Character.Status.max_atk1_petpy = 0;
                            Character.Status.min_atk1_petpy = 0;
                            Character.Status.max_matk_petpy = 0;
                            Character.Status.min_matk_petpy = 0;
                        }
                        break;
                    case 1:
                        if (Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND))
                        {
                            Character.PossessionPartnerSlotIDinLeftHand = Pict;
                            Character.Status.def_add_petpy = (short)(partner.Status.def_add / 2);
                            Character.Status.mdef_add_petpy = (short)(partner.Status.mdef_add / 2);
                        }
                        else
                        {
                            Character.PossessionPartnerSlotIDinLeftHand = 0;
                            Character.Status.def_add_petpy = 0;
                            Character.Status.mdef_add_petpy = 0;
                        }
                        break;
                    case 2:
                        if (Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.CHEST_ACCE))
                        {
                            Character.PossessionPartnerSlotIDinAccesory = Pict;
                            Character.Status.aspd_petpy = (short)(partner.Status.aspd / 2);
                            Character.Status.cspd_petpy = (short)(partner.Status.cspd / 2);
                        }
                        else
                        {
                            Character.PossessionPartnerSlotIDinAccesory = 0;
                            Character.Status.aspd_petpy = 0;
                            Character.Status.cspd_petpy = 0;
                        }
                        break;
                    case 3:
                        if (Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.UPPER_BODY))
                        {
                            Character.PossessionPartnerSlotIDinClothes = Pict;
                            Character.Status.hp_petpy = (short)(partner.MaxHP / 20);
                        }
                        else
                        {
                            Character.PossessionPartnerSlotIDinClothes = 0;
                            Character.Status.hp_petpy = 0;
                        }
                        break;
                }
                PC.StatusFactory.Instance.CalcStatus(Character);
                Packets.Server.SSMG_PARTNER_PY_CONFIRM p2 = new Packets.Server.SSMG_PARTNER_PY_CONFIRM();
                p2.SlotID = p.Slot;
                p2.Type = p.Type;
                netIO.SendPacket(p2);
                SendActorHPMPSP(Character);
                SendStatus();
                SendStatusExtend();
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, Character, true);

            }
        }
        /// <summary>
        /// 宠物PY请求
        /// </summary>
        /// <param name="p"></param>
        public void OnPartnerItemPYCancel(Packets.Client.CSMG_PARTNER_PY_CANCEL p)
        {
            switch (p.Type)
            {
                case 0:
                    Character.PossessionPartnerSlotIDinRightHand = 0;
                    Character.Status.max_atk1_petpy = 0;
                    Character.Status.min_atk1_petpy = 0;
                    Character.Status.max_matk_petpy = 0;
                    Character.Status.min_matk_petpy = 0;
                    break;
                case 1:
                    Character.PossessionPartnerSlotIDinLeftHand = 0;
                    Character.Status.def_add_petpy = 0;
                    Character.Status.mdef_add_petpy = 0;
                    break;
                case 2:
                    Character.PossessionPartnerSlotIDinAccesory = 0;
                    Character.Status.aspd_petpy = 0;
                    Character.Status.cspd_petpy = 0;
                    break;
                case 3:
                    Character.PossessionPartnerSlotIDinClothes = 0;
                    Character.Status.hp_petpy = 0;
                    break;
            }
            PC.StatusFactory.Instance.CalcStatus(Character);
            Packets.Server.SSMG_PARTNER_PY_CANCEL p2 = new Packets.Server.SSMG_PARTNER_PY_CANCEL();
            p2.Type = p.Type;
            netIO.SendPacket(p2);
            SendActorHPMPSP(Character);
            SendStatus();
            SendStatusExtend();
            map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, Character, true);

        }
        #endregion
        public void OnPartnerMotion(Packets.Client.CSMG_PARTNER_PARTNER_MOTION p)
        {
            if (!Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET)) return;
            uint itemid = Character.Inventory.Equipments[EnumEquipSlot.PET].BaseData.id;
            List<PartnerMotion> motions = PartnerFactory.Instance.GetPartnerMotion(itemid);
            if (motions == null) return;
            int id = (int)p.id;

            ActorPartner partner = Character.Partner;

            PartnerMotion motion = motions[id];
            SendMotion((MotionType)motion.MasterMotionID, 1);

            ChatArg arg = new ChatArg();
            arg.motion = (MotionType)motion.PartnerMotionID;
            arg.loop = 1;
            partner.Motion = arg.motion;
            partner.MotionLoop = true;
            Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.MOTION, arg, partner, true);

        }
    }
}