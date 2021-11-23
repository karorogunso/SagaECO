using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;

using SagaDB;
using SagaDB.Item;
using SagaDB.Actor;
using SagaDB.Map;
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

        public void OnCharFormChange(Packets.Client.CSMG_CHAR_FORM p)
        {
            this.Character.TailStyle = p.tailstyle;
            this.Character.WingStyle = p.wingstyle;
            this.Character.WingColor = p.wingcolor;
            this.Character.e.PropertyUpdate(UpdateEvent.CHAR_INFO, 0);
        }
        public void OnPlayerFaceView(Packets.Client.CSMG_ITEM_FACEVIEW p)
        {
            Packet p2 = new Packet(3);
            p2.ID = 0x1CF3;
            this.netIO.SendPacket(p2);
        }
        public void OnPlayerFaceChange(Packets.Client.CSMG_ITEM_FACECHANGE p)
        {
            uint itemID = this.Character.Inventory.GetItem(p.SlotID).ItemID;
            if (itemID == FaceFactory.Instance.Faces[p.FaceID])
            {
                this.DeleteItem(p.SlotID, 1, true);
                this.Character.Face = p.FaceID;
                this.SendPlayerInfo();
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
        public void SendRingFF()
        {
            MapServer.charDB.GetFF(this.Character);
            if (this.Character.Ring != null)
            {
                if (this.Character.Ring.FFarden != null)
                {
                    this.SendRingFFObtainMode();
                    this.SendRingFFHealthMode();
                    this.SendRingFFIsLock();
                    this.SendRingFFName();
                    this.SendRingFFMaterialPoint();
                    this.SendRingFFMaterialConsume();
                    this.SendRingFFLevel();
                    this.SendRingFFNextFeeTime();
                }
            }
        }
        void SendRingFFObtainMode()
        {
            Packets.Server.SSMG_FF_OBTAIN_MODE p = new SagaMap.Packets.Server.SSMG_FF_OBTAIN_MODE();
            p.value = this.Character.Ring.FFarden.ObMode;
            this.netIO.SendPacket(p);
        }
        void SendRingFFHealthMode()
        {
            Packets.Server.SSMG_FF_HEALTH_MODE p = new Packets.Server.SSMG_FF_HEALTH_MODE();
            p.value = this.Character.Ring.FFarden.HealthMode;
            this.netIO.SendPacket(p);
        }
        void SendRingFFIsLock()
        {
            Packets.Server.SSMG_FF_ISLOCK p = new Packets.Server.SSMG_FF_ISLOCK();
            if (this.Character.Ring.FFarden.IsLock)
                p.value = 1;
            else
                p.value = 0;
            this.netIO.SendPacket(p);
        }
        void SendRingFFName()
        {
            Packets.Server.SSMG_FF_RINGSELF p = new Packets.Server.SSMG_FF_RINGSELF();
            p.name = this.Character.Ring.FFarden.Name;
            this.netIO.SendPacket(p);
        }
        void SendRingFFMaterialPoint()
        {
            Packets.Server.SSMG_FF_MATERIAL_POINT p = new Packets.Server.SSMG_FF_MATERIAL_POINT();
            p.value = this.Character.Ring.FFarden.MaterialPoint;
            this.netIO.SendPacket(p);
        }
        void SendRingFFMaterialConsume()
        {
            Packets.Server.SSMG_FF_MATERIAL_CONSUME p = new Packets.Server.SSMG_FF_MATERIAL_CONSUME();
            p.value = this.Character.Ring.FFarden.MaterialConsume;
            this.netIO.SendPacket(p);
        }
        void SendRingFFLevel()
        {
            Packets.Server.SSMG_FF_LEVEL p = new Packets.Server.SSMG_FF_LEVEL();
            p.level = this.Character.Ring.FFarden.Level;
            p.value = this.Character.Ring.FFarden.FFexp;
            this.netIO.SendPacket(p);
            Packets.Server.SSMG_FF_F_LEVEL p1 = new Packets.Server.SSMG_FF_F_LEVEL();
            p1.level = this.Character.Ring.FFarden.FLevel;
            p1.value = this.Character.Ring.FFarden.FFFexp;
            this.netIO.SendPacket(p1);
            Packets.Server.SSMG_FF_SU_LEVEL p2 = new Packets.Server.SSMG_FF_SU_LEVEL();
            p2.level = this.Character.Ring.FFarden.SULevel;
            p2.value = this.Character.Ring.FFarden.FFSUexp;
            this.netIO.SendPacket(p2);
            Packets.Server.SSMG_FF_BP_LEVEL p3 = new Packets.Server.SSMG_FF_BP_LEVEL();
            p3.level = this.Character.Ring.FFarden.BPLevel;
            p3.value = this.Character.Ring.FFarden.FFBPexp;
            this.netIO.SendPacket(p3);
            Packets.Server.SSMG_FF_DEM_LEVEL p4 = new Packets.Server.SSMG_FF_DEM_LEVEL();
            p4.level = this.Character.Ring.FFarden.DEMLevel;
            p4.value = this.Character.Ring.FFarden.FFDEMexp;
            this.netIO.SendPacket(p4);
        }
        void SendRingFFNextFeeTime()
        {
            Packets.Server.SSMG_FF_NEXTFEE_DATE p = new Packets.Server.SSMG_FF_NEXTFEE_DATE();
            p.UpdateTime = DateTime.Now;
            this.netIO.SendPacket(p);
        }


        void SendEffect(uint effect)
        {
            EffectArg arg = new EffectArg();
            arg.actorID = this.Character.ActorID;
            arg.effectID = effect;
            this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, arg, this.chara, true);
        }

        public void ResetStatusPoint()
        {
            SagaLogin.Configurations.StartupSetting setting = Configuration.Instance.StartupSetting[this.Character.Race];
            this.Character.StatsPoint += PC.StatusFactory.Instance.GetTotalBonusPointForStats(setting.Str, this.Character.Str);
            this.Character.StatsPoint += PC.StatusFactory.Instance.GetTotalBonusPointForStats(setting.Dex, this.Character.Dex);
            this.Character.StatsPoint += PC.StatusFactory.Instance.GetTotalBonusPointForStats(setting.Int, this.Character.Int);
            this.Character.StatsPoint += PC.StatusFactory.Instance.GetTotalBonusPointForStats(setting.Vit, this.Character.Vit);
            this.Character.StatsPoint += PC.StatusFactory.Instance.GetTotalBonusPointForStats(setting.Agi, this.Character.Agi);
            this.Character.StatsPoint += PC.StatusFactory.Instance.GetTotalBonusPointForStats(setting.Mag, this.Character.Mag);

            this.Character.Str = setting.Str;
            this.Character.Dex = setting.Dex;
            this.Character.Int = setting.Int;
            this.Character.Vit = setting.Vit;
            this.Character.Agi = setting.Agi;
            this.Character.Mag = setting.Mag;

            PC.StatusFactory.Instance.CalcStatus(this.Character);
            SendPlayerInfo();
        }

        public void SendRange()
        {
            Packets.Server.SSMG_ITEM_EQUIP p = new SagaMap.Packets.Server.SSMG_ITEM_EQUIP();
            p.InventorySlot = 0xFFFFFFFF;
            p.Target = ContainerType.NONE;
            p.Result = 1;
            p.Range = this.chara.Range;
            this.netIO.SendPacket(p);
        }

        public void SendActorID()
        {
            Packets.Server.SSMG_ACTOR_SPEED p2 = new SagaMap.Packets.Server.SSMG_ACTOR_SPEED();
            p2.ActorID = this.Character.ActorID;
            p2.Speed = 10;
            //p2.Speed = 96;
            this.netIO.SendPacket(p2);
        }

        public void SendStamp()
        {
            Packets.Server.SSMG_STAMP_INFO p = new SagaMap.Packets.Server.SSMG_STAMP_INFO();
            p.Stamp = this.Character.Stamp;
            this.netIO.SendPacket(p);
        }

        public void SendActorMode()
        {
            this.Character.e.OnPlayerMode(this.Character);
        }

        public void SendCharOption()
        {
            Packets.Server.SSMG_ACTOR_OPTION p4 = new SagaMap.Packets.Server.SSMG_ACTOR_OPTION();
            p4.Option = SagaMap.Packets.Server.SSMG_ACTOR_OPTION.Options.NONE;
            this.netIO.SendPacket(p4);
        }

        public void SendCharInfo()
        {
            if (this.Character.Online)
            {
                Skill.SkillHandler.Instance.CastPassiveSkills(this.Character);

                SendAttackType();
                Packets.Server.SSMG_PLAYER_INFO p1 = new SagaMap.Packets.Server.SSMG_PLAYER_INFO();
                p1.Player = this.Character;
                this.netIO.SendPacket(p1);

                SendPlayerInfo();
            }
        }

        public void SendPlayerInfo()
        {
            if (this.Character.Online)
            {
                SendGoldUpdate();
                SendActorHPMPSP(this.Character);
                SendStatus();
                SendRange();
                SendStatusExtend();
                SendCapacity();
                SendMaxCapacity();
                SendPlayerJob();
                SendSkillList();
                SendPlayerLevel();
                SendEXP();
                SendActorMode();
                SendCL();
                SendMotionList();
            }
        }

        public void SendMotionList()
        {
            if (this.Character.Online)
            {
                Packets.Server.SSMG_CHAT_EXPRESSION_UNLOCK p = new SagaMap.Packets.Server.SSMG_CHAT_EXPRESSION_UNLOCK();
                p.unlock = 0xff;
                this.netIO.SendPacket(p);
                Packets.Server.SSMG_CHAT_EXEMOTION_UNLOCK p2 = new SagaMap.Packets.Server.SSMG_CHAT_EXEMOTION_UNLOCK();
                p2.List1 = 0xffffffff;
                p2.List2 = 0xffffffff;
                p2.List3 = 0xffffffff;
                p2.List4 = 0xffffffff;
                p2.List5 = 0xffffffff;
                this.netIO.SendPacket(p2);
            }
        }

        public void SendAttackType()
        {
            if (this.Character.Online)
            {
                Dictionary<EnumEquipSlot, Item> equips;
                if (this.chara.Form == DEM_FORM.NORMAL_FORM)
                    equips = this.chara.Inventory.Equipments;
                else
                    equips = this.chara.Inventory.Parts;
                if (equips.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                {
                    Item item = equips[EnumEquipSlot.RIGHT_HAND];
                    this.Character.Status.attackType = item.AttackType;
                    switch (item.AttackType)
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
                    }
                }
                else
                {
                    this.Character.Status.attackType = ATTACK_TYPE.BLOW;
                    SendSystemMessage(SagaMap.Packets.Server.SSMG_SYSTEM_MESSAGE.Messages.GAME_SMSG_RECV_POSTUREBLOW_TEXT);
                }
                map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.ATTACK_TYPE_CHANGE, null, this.Character, true);
            }
        }

        public void SendStatus()
        {
            if (this.Character.Online)
            {
                Packets.Server.SSMG_PLAYER_STATUS p = new SagaMap.Packets.Server.SSMG_PLAYER_STATUS();
                if (this.chara.Form == DEM_FORM.MACHINA_FORM || this.chara.Race != PC_RACE.DEM)
                {
                    p.AgiBase = (ushort)(this.Character.Agi + this.chara.Status.m_agi_chip);
                    p.AgiRevide = (short)(this.Character.Status.agi_rev + this.Character.Status.agi_item + this.Character.Status.agi_mario + this.Character.Status.agi_skill);
                    p.AgiBonus = StatusFactory.Instance.RequiredBonusPoint(this.Character.Agi);
                    p.DexBase = (ushort)(this.Character.Dex + this.chara.Status.m_dex_chip);
                    p.DexRevide = (short)(this.Character.Status.dex_rev + this.Character.Status.dex_item + this.Character.Status.dex_mario + this.Character.Status.dex_skill);
                    p.DexBonus = StatusFactory.Instance.RequiredBonusPoint(this.Character.Dex);
                    p.IntBase = (ushort)(this.Character.Int + this.chara.Status.m_int_chip);
                    p.IntRevide = (short)(this.Character.Status.int_rev + this.Character.Status.int_item + this.Character.Status.int_mario + this.Character.Status.int_skill);
                    p.IntBonus = StatusFactory.Instance.RequiredBonusPoint(this.Character.Int);
                    p.VitBase = (ushort)(this.Character.Vit + this.chara.Status.m_vit_chip);
                    p.VitRevide = (short)(this.Character.Status.vit_rev + this.Character.Status.vit_item + this.Character.Status.vit_mario + this.Character.Status.vit_skill);
                    p.VitBonus = StatusFactory.Instance.RequiredBonusPoint(this.Character.Vit);
                    p.StrBase = (ushort)(this.Character.Str + this.chara.Status.m_str_chip);
                    p.StrRevide = (short)(this.Character.Status.str_rev + this.Character.Status.str_item + this.Character.Status.str_mario + this.Character.Status.str_skill);
                    p.StrBonus = StatusFactory.Instance.RequiredBonusPoint(this.Character.Str);
                    p.MagBase = (ushort)(this.Character.Mag + this.chara.Status.m_mag_chip);
                    p.MagRevide = (short)(this.Character.Status.mag_rev + this.Character.Status.mag_item + this.Character.Status.mag_mario + this.Character.Status.mag_skill);
                    p.MagBonus = StatusFactory.Instance.RequiredBonusPoint(this.Character.Mag);
                    this.netIO.SendPacket(p);
                }
                else
                {
                    p.AgiBase = (ushort)(this.Character.Agi + this.chara.Status.m_agi_chip);
                    p.AgiRevide = (short)(this.Character.Status.agi_rev - this.chara.Status.m_agi_chip + this.Character.Status.agi_item + this.Character.Status.agi_mario + this.Character.Status.agi_skill);
                    p.AgiBonus = StatusFactory.Instance.RequiredBonusPoint(this.Character.Agi);
                    p.DexBase = (ushort)(this.Character.Dex + this.chara.Status.m_dex_chip);
                    p.DexRevide = (short)(this.Character.Status.dex_rev - this.chara.Status.m_dex_chip + this.Character.Status.dex_item + this.Character.Status.dex_mario + this.Character.Status.dex_skill);
                    p.DexBonus = StatusFactory.Instance.RequiredBonusPoint(this.Character.Dex);
                    p.IntBase = (ushort)(this.Character.Int + this.chara.Status.m_int_chip);
                    p.IntRevide = (short)(this.Character.Status.int_rev - this.chara.Status.m_int_chip + this.Character.Status.int_item + this.Character.Status.int_mario + this.Character.Status.int_skill);
                    p.IntBonus = StatusFactory.Instance.RequiredBonusPoint(this.Character.Int);
                    p.VitBase = (ushort)(this.Character.Vit + this.chara.Status.m_vit_chip);
                    p.VitRevide = (short)(this.Character.Status.vit_rev - this.chara.Status.m_vit_chip + this.Character.Status.vit_item + this.Character.Status.vit_mario + this.Character.Status.vit_skill);
                    p.VitBonus = StatusFactory.Instance.RequiredBonusPoint(this.Character.Vit);
                    p.StrBase = (ushort)(this.Character.Str + this.chara.Status.m_str_chip);
                    p.StrRevide = (short)(this.Character.Status.str_rev - this.chara.Status.m_str_chip + this.Character.Status.str_item + this.Character.Status.str_mario + this.Character.Status.str_skill);
                    p.StrBonus = StatusFactory.Instance.RequiredBonusPoint(this.Character.Str);
                    p.MagBase = (ushort)(this.Character.Mag + this.chara.Status.m_mag_chip);
                    p.MagRevide = (short)(this.Character.Status.mag_rev - this.chara.Status.m_mag_chip + this.Character.Status.mag_item + this.Character.Status.mag_mario + this.Character.Status.mag_skill);
                    p.MagBonus = StatusFactory.Instance.RequiredBonusPoint(this.Character.Mag);

                    this.netIO.SendPacket(p);
                }
            }
        }

        public void SendStatusExtend()
        {
            if (this.Character.Online)
            {
                Packets.Server.SSMG_PLAYER_STATUS_EXTEND p = new SagaMap.Packets.Server.SSMG_PLAYER_STATUS_EXTEND();

                switch (this.chara.Status.attackType)
                {
                    case ATTACK_TYPE.BLOW:
                        p.ATK1Max = this.Character.Status.max_atk1;
                        p.ATK1Min = this.Character.Status.min_atk1;
                        break;
                    case ATTACK_TYPE.SLASH:
                        p.ATK1Max = this.Character.Status.max_atk2;
                        p.ATK1Min = this.Character.Status.min_atk2;
                        break;
                    case ATTACK_TYPE.STAB:
                        p.ATK1Max = this.Character.Status.max_atk3;
                        p.ATK1Min = this.Character.Status.min_atk3;
                        break;
                }
                p.ATK2Max = this.Character.Status.max_atk2;
                p.ATK2Min = this.Character.Status.min_atk2;
                p.ATK3Max = this.Character.Status.max_atk3;
                p.ATK3Min = this.Character.Status.min_atk3;
                p.MATKMax = this.Character.Status.max_matk;
                p.MATKMin = this.Character.Status.min_matk;

                p.ASPD = (short)(this.Character.Status.aspd);// + this.Character.Status.aspd_skill);
                p.CSPD = (short)(this.Character.Status.cspd);// + this.Character.Status.cspd_skill);

                p.AvoidCritical = this.Character.Status.avoid_critical;
                p.AvoidMagic = this.Character.Status.avoid_magic;
                p.AvoidMelee = this.Character.Status.avoid_melee;
                p.AvoidRanged = this.Character.Status.avoid_ranged;

                p.DefAddition = (ushort)this.Character.Status.def_add;
                p.DefBase = this.Character.Status.def;
                p.MDefAddition = (ushort)this.Character.Status.mdef_add;
                p.MDefBase = this.Character.Status.mdef;

                p.HitCritical = this.Character.Status.hit_critical;
                p.HitMagic = this.Character.Status.hit_magic;
                p.HitMelee = this.Character.Status.hit_melee;
                p.HitRanged = this.Character.Status.hit_ranged;

                p.Speed = this.Character.Speed;

                this.netIO.SendPacket(p);
            }
        }

        public void SendCapacity()
        {
            if (this.Character.Online)
            {
                Packets.Server.SSMG_PLAYER_CAPACITY p = new SagaMap.Packets.Server.SSMG_PLAYER_CAPACITY();
                p.CapacityBack = this.Character.Inventory.Volume[ContainerType.BACK_BAG];
                p.CapacityBody = this.Character.Inventory.Volume[ContainerType.BODY];
                p.CapacityLeft = this.Character.Inventory.Volume[ContainerType.LEFT_BAG];
                p.CapacityRight = this.Character.Inventory.Volume[ContainerType.RIGHT_BAG];
                p.PayloadBack = this.Character.Inventory.Payload[ContainerType.BACK_BAG];
                p.PayloadBody = this.Character.Inventory.Payload[ContainerType.BODY];
                p.PayloadLeft = this.Character.Inventory.Payload[ContainerType.LEFT_BAG];
                p.PayloadRight = this.Character.Inventory.Payload[ContainerType.RIGHT_BAG];
                this.netIO.SendPacket(p);
            }
        }

        public void SendMaxCapacity()
        {
            if (this.Character.Online)
            {
                Packets.Server.SSMG_PLAYER_MAX_CAPACITY p = new SagaMap.Packets.Server.SSMG_PLAYER_MAX_CAPACITY();
                p.CapacityBack = this.Character.Inventory.MaxVolume[ContainerType.BACK_BAG];
                p.CapacityBody = this.Character.Inventory.MaxVolume[ContainerType.BODY];
                p.CapacityLeft = this.Character.Inventory.MaxVolume[ContainerType.LEFT_BAG];
                p.CapacityRight = this.Character.Inventory.MaxVolume[ContainerType.RIGHT_BAG];
                p.PayloadBack = this.Character.Inventory.MaxPayload[ContainerType.BACK_BAG];
                p.PayloadBody = this.Character.Inventory.MaxPayload[ContainerType.BODY];
                p.PayloadLeft = this.Character.Inventory.MaxPayload[ContainerType.LEFT_BAG];
                p.PayloadRight = this.Character.Inventory.MaxPayload[ContainerType.RIGHT_BAG];
                this.netIO.SendPacket(p);
            }
        }

        public void SendChangeMap()
        {
            if (this.Character.Online)
            {
                Packets.Server.SSMG_PLAYER_CHANGE_MAP p = new SagaMap.Packets.Server.SSMG_PLAYER_CHANGE_MAP();
                if (this.map.returnori)
                    p.MapID = this.map.OriID;
                else
                    p.MapID = this.Character.MapID;
                p.X = Global.PosX16to8(this.Character.X, this.map.Width);
                p.Y = Global.PosY16to8(this.Character.Y, this.map.Height);
                p.Dir = (byte)(this.Character.Dir / 45);
                if (this.map.IsDungeon)
                {
                    p.DungeonDir = this.map.DungeonMap.Dir;
                    p.DungeonX = this.map.DungeonMap.X;
                    p.DungeonY = this.map.DungeonMap.Y;
                    this.Character.Speed = (ushort)(Configuration.Instance.Speed + this.Character.Status.speed_skill + this.Character.Status.speed_item);
                }
                if (this.fgTakeOff)
                {
                    p.FGTakeOff = fgTakeOff;
                    fgTakeOff = false;
                }
                else
                {
                    this.Character.Speed = (ushort)(Configuration.Instance.Speed + this.Character.Status.speed_skill + this.Character.Status.speed_item);
                }
                this.netIO.SendPacket(p);
            }
        }

        public void SendGotoFG()
        {
            if (this.Character.Online)
            {
                Map fgMap = MapManager.Instance.GetMap(this.Character.MapID);
                if (!fgMap.IsMapInstance)
                {
                    Logger.ShowDebug(string.Format("MapID:{0} isn't a valid flying garden!"), Logger.defaultlogger);
                }
                ActorPC owner = fgMap.Creator;
                Packets.Server.SSMG_PLAYER_GOTO_FG p = new SagaMap.Packets.Server.SSMG_PLAYER_GOTO_FG();
                p.MapID = this.Character.MapID;
                p.X = Global.PosX16to8(this.Character.X, this.map.Width);
                p.Y = Global.PosY16to8(this.Character.Y, this.map.Height);
                p.Dir = (byte)(this.Character.Dir / 45);
                p.Equiptments = owner.FGarden.FGardenEquipments;
                this.netIO.SendPacket(p);
            }
        }

        public void SendGotoFF()
        {
            if (this.Character.Online)
            {
                Map fgMap = MapManager.Instance.GetMap(this.Character.MapID);
                if (fgMap.ID == 90000999 || fgMap.ID == 91000999)
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
                p.MapID = this.Character.MapID;
                p.X = Global.PosX16to8(this.Character.X, this.map.Width);
                p.Y = Global.PosY16to8(this.Character.Y, this.map.Height);
                p.Dir = (byte)(this.Character.Dir / 45);
                p.RingID = this.Character.Ring.ID;
                p.RingHouseID = 30250000;
                this.netIO.SendPacket(p);
            }
        }

        public void SendDungeonEvent()
        {
            if (this.Character.Online)
            {
                if (!this.map.IsMapInstance || !this.map.IsDungeon)
                    return;
                foreach (Dungeon.GateType i in this.map.DungeonMap.Gates.Keys)
                {
                    if (this.map.DungeonMap.Gates[i].NPCID != 0)
                    {
                        Packets.Server.SSMG_NPC_SHOW p = new SagaMap.Packets.Server.SSMG_NPC_SHOW();
                        p.NPCID = this.map.DungeonMap.Gates[i].NPCID;
                        this.netIO.SendPacket(p);
                    }
                    if (this.map.DungeonMap.Gates[i].ConnectedMap != null)
                    {
                        if (i != SagaMap.Dungeon.GateType.Central && i != SagaMap.Dungeon.GateType.Exit)
                        {
                            Packets.Server.SSMG_NPC_SET_EVENT_AREA p1 = new SagaMap.Packets.Server.SSMG_NPC_SET_EVENT_AREA();
                            p1.StartX = this.map.DungeonMap.Gates[i].X;
                            p1.EndX = this.map.DungeonMap.Gates[i].X;
                            p1.StartY = this.map.DungeonMap.Gates[i].Y;
                            p1.EndY = this.map.DungeonMap.Gates[i].Y;

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
                            switch (this.map.DungeonMap.Gates[i].Direction)
                            {
                                case SagaMap.Dungeon.Direction.In:
                                    p1.EffectID = 9002;
                                    break;
                                case SagaMap.Dungeon.Direction.Out:
                                    p1.EffectID = 9005;
                                    break;
                            }
                            this.netIO.SendPacket(p1);
                        }
                        else
                        {
                            Packets.Server.SSMG_NPC_SET_EVENT_AREA p1 = new SagaMap.Packets.Server.SSMG_NPC_SET_EVENT_AREA();
                            p1.StartX = this.map.DungeonMap.Gates[i].X;
                            p1.EndX = this.map.DungeonMap.Gates[i].X;
                            p1.StartY = this.map.DungeonMap.Gates[i].Y;
                            p1.EndY = this.map.DungeonMap.Gates[i].Y;
                            p1.EventID = 12001505;
                            p1.EffectID = 9005;
                            this.netIO.SendPacket(p1);
                        }

                        if (this.map.DungeonMap.Gates[i].NPCID != 0)
                        {
                            Packets.Server.SSMG_CHAT_MOTION p = new SagaMap.Packets.Server.SSMG_CHAT_MOTION();
                            p.ActorID = this.map.DungeonMap.Gates[i].NPCID;
                            p.Motion = (MotionType)621;
                            this.netIO.SendPacket(p);
                        }
                    }
                    else
                    {
                        if (i == SagaMap.Dungeon.GateType.Entrance)
                        {
                            Packets.Server.SSMG_NPC_SET_EVENT_AREA p1 = new SagaMap.Packets.Server.SSMG_NPC_SET_EVENT_AREA();
                            p1.StartX = this.map.DungeonMap.Gates[i].X;
                            p1.EndX = this.map.DungeonMap.Gates[i].X;
                            p1.StartY = this.map.DungeonMap.Gates[i].Y;
                            p1.EndY = this.map.DungeonMap.Gates[i].Y;
                            p1.EventID = 12001505;
                            p1.EffectID = 9003;
                            this.netIO.SendPacket(p1);
                        }
                    }
                }

            }
        }

        public void SendFGEvent()
        {
            if (this.Character.Online)
            {
                Map fgMap = MapManager.Instance.GetMap(this.Character.MapID);
                if (!fgMap.IsMapInstance)
                    return;
                if ((this.map.ID / 10) == 7000000)
                {
                    if (this.map.Creator.FGarden.FGardenEquipments[SagaDB.FGarden.FGardenSlot.GARDEN_MODELHOUSE] != 0)
                    {
                        Packets.Server.SSMG_NPC_SET_EVENT_AREA p1 = new SagaMap.Packets.Server.SSMG_NPC_SET_EVENT_AREA();
                        p1.EventID = 10000315;
                        p1.StartX = 6;
                        p1.StartY = 7;
                        p1.EndX = 6;
                        p1.EndY = 7;
                        this.netIO.SendPacket(p1);
                    }
                }
            }
        }

        public void SendGoldUpdate()
        {
            if (this.Character.Online)
            {
                Packets.Server.SSMG_PLAYER_GOLD_UPDATE p = new SagaMap.Packets.Server.SSMG_PLAYER_GOLD_UPDATE();
                p.Gold = (uint)this.Character.Gold;
                this.netIO.SendPacket(p);
            }

        }

        public void SendActorHPMPSP(Actor actor)
        {
            if (this.Character.Online)
            {
                Packets.Server.SSMG_PLAYER_MAX_HPMPSP p = new SagaMap.Packets.Server.SSMG_PLAYER_MAX_HPMPSP();
                p.ActorID = actor.ActorID;
                p.MaxHP = actor.MaxHP;
                p.MaxMP = actor.MaxMP;
                p.MaxSP = actor.MaxSP;
                p.MaxEP = actor.MaxEP;
                this.netIO.SendPacket(p);
                Packets.Server.SSMG_PLAYER_HPMPSP p10 = new SagaMap.Packets.Server.SSMG_PLAYER_HPMPSP();
                p10.ActorID = actor.ActorID;
                p10.HP = actor.HP;
                p10.MP = actor.MP;
                p10.SP = actor.SP;
                p10.EP = actor.EP;
                this.netIO.SendPacket(p10);
                if (actor == this.Character)
                {
                    if ((DateTime.Now - hpmpspStamp).TotalSeconds >= 2)
                    {
                        if (this.Character.Party != null)
                        {
                            PartyManager.Instance.UpdateMemberHPMPSP(this.Character.Party, this.Character);
                        }
                        hpmpspStamp = DateTime.Now;
                    }
                }
            }
        }

        public void SendCharXY()
        {
            if (this.Character.Online)
            {
                Packets.Server.SSMG_ACTOR_MOVE p = new SagaMap.Packets.Server.SSMG_ACTOR_MOVE();
                p.ActorID = this.Character.ActorID;
                p.Dir = this.Character.Dir;
                p.X = this.Character.X;
                p.Y = this.Character.Y;
                p.MoveType = MoveType.WARP;
                this.netIO.SendPacket(p);
            }

        }

        public void SendPlayerLevel()
        {
            if (this.Character.Online)
            {
                Packets.Server.SSMG_PLAYER_LEVEL p = new SagaMap.Packets.Server.SSMG_PLAYER_LEVEL();
                if (this.map.Info.Flag.Test(MapFlags.Dominion))
                {
                    p.Level = this.Character.DominionLevel;
                    p.JobLevel = this.Character.DominionJobLevel;
                    p.JobLevel2T = this.Character.DominionJobLevel;
                    p.JobLevel2X = this.Character.DominionJobLevel;
                    p.JobLevelJoint = this.Character.JointJobLevel;
                    p.BonusPoint = this.Character.StatsPoint;
                    p.SkillPoint = 0;
                    p.Skill2XPoint = 0;
                    p.Skill2TPoint = 0;
                    p.Skill3Point = 0;
                }
                else
                {
                    p.Level = this.Character.Level;
                    p.JobLevel = this.Character.JobLevel1;
                    p.JobLevel2T = this.Character.JobLevel2T;
                    p.JobLevel2X = this.Character.JobLevel2X;
                    if (this.Character.JobJoint != PC_JOB.NOVICE)
                        p.JobLevelJoint = this.Character.JobLevel3;
                    else
                        p.JobLevelJoint = this.Character.JointJobLevel;
                    p.BonusPoint = this.Character.StatsPoint;
                    p.SkillPoint = this.Character.SkillPoint;
                    p.Skill2XPoint = this.Character.SkillPoint2X;
                    p.Skill2TPoint = this.Character.SkillPoint2T;
                    p.Skill3Point = this.Character.SkillPoint3;
                }
                this.netIO.SendPacket(p);
            }
        }

        public void SendPlayerJob()
        {
            if (this.Character.Online)
            {
                Packets.Server.SSMG_PLAYER_JOB p = new SagaMap.Packets.Server.SSMG_PLAYER_JOB();
                p.Job = this.Character.Job;
                if (this.Character.JobJoint != PC_JOB.NONE)
                    p.JointJob = this.Character.JobJoint;
                this.netIO.SendPacket(p);
            }
        }


        public void SendCharInfoUpdate()
        {
            if (this.Character.Online)
            {
                this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHAR_INFO_UPDATE, null, this.Character, true);
            }
        }

        public void SendAnnounce(string text)
        {
            if (this.Character.Online)
            {
                Packets.Server.SSMG_CHAT_PUBLIC p11 = new SagaMap.Packets.Server.SSMG_CHAT_PUBLIC();
                p11.ActorID = 0;
                p11.Message = text;
                this.netIO.SendPacket(p11);
            }
        }

        public void SendPkMode()
        {
            if (this.Character.Online)
            {
                this.Character.Mode = PlayerMode.COLISEUM_MODE;
                this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.PLAYER_MODE, null, this.Character, true);
            }
        }

        public void SendNormalMode()
        {
            if (this.Character.Online)
            {
                this.Character.Mode = PlayerMode.NORMAL;
                this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.PLAYER_MODE, null, this.Character, true);
            }
        }

        public void SendPlayerSizeUpdate()
        {
            if (this.Character.Online)
            {
                this.map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.PLAYER_SIZE_UPDATE, null, this.Character, true);
            }

        }

        public void OnMove(Packets.Client.CSMG_PLAYER_MOVE p)
        {
            if (this.Character.Online)
            {
                if (this.state != SESSION_STATE.LOADED)
                    return;
                switch (p.MoveType)
                {
                    case MoveType.RUN:
                        this.Map.MoveActor(Map.MOVE_TYPE.START, this.Character, new short[2] { p.X, p.Y }, p.Dir, this.Character.Speed);
                        moveCheckStamp = DateTime.Now;
                        break;
                    case MoveType.CHANGE_DIR:
                        this.Map.MoveActor(Map.MOVE_TYPE.STOP, this.Character, new short[2] { p.X, p.Y }, p.Dir, this.Character.Speed);
                        break;
                    case MoveType.WALK:
                        this.Map.MoveActor(Map.MOVE_TYPE.START, this.Character, new short[2] { p.X, p.Y }, p.Dir, this.Character.Speed);
                        moveCheckStamp = DateTime.Now;
                        break;
                }
                if (this.Character.CInt["NextMoveEventID"] != 0)
                {
                    this.EventActivate((uint)this.Character.CInt["NextMoveEventID"]);
                    this.Character.CInt["NextMoveEventID"] = 0;
                    this.Character.CInt.Remove("NextMoveEventID");
                }
            }
        }

        public void SendActorSpeed(Actor actor, ushort speed)
        {
            if (this.Character.Online)
            {
                Packets.Server.SSMG_ACTOR_SPEED p = new SagaMap.Packets.Server.SSMG_ACTOR_SPEED();
                p.ActorID = actor.ActorID;
                p.Speed = speed;
                this.netIO.SendPacket(p);
            }
        }

        public void SendEXP()
        {
            if (this.Character.Online)
            {
                Packets.Server.SSMG_PLAYER_EXP p = new SagaMap.Packets.Server.SSMG_PLAYER_EXP();
                ulong cexp, jexp;
                ulong bexp = 0, nextExp = 0;
                if (this.map.Info.Flag.Test(MapFlags.Dominion))
                {
                    bexp = ExperienceManager.Instance.GetExpForLevel(this.Character.DominionLevel, Scripting.LevelType.CLEVEL);
                    nextExp = ExperienceManager.Instance.GetExpForLevel((uint)(this.Character.DominionLevel + 1), Scripting.LevelType.CLEVEL);
                    cexp = (uint)((float)(this.Character.DominionCEXP - bexp) / (nextExp - bexp) * 1000);
                }
                else
                {
                    if (!this.Character.Rebirth)
                    {
                        bexp = ExperienceManager.Instance.GetExpForLevel(this.Character.Level, Scripting.LevelType.CLEVEL);
                        nextExp = ExperienceManager.Instance.GetExpForLevel((uint)(this.Character.Level + 1), Scripting.LevelType.CLEVEL);
                    }
                    else
                    {
                        bexp = ExperienceManager.Instance.GetExpForLevel(this.Character.Level, Scripting.LevelType.CLEVEL2);
                        nextExp = ExperienceManager.Instance.GetExpForLevel((uint)(this.Character.Level + 1), Scripting.LevelType.CLEVEL2);
                    }
                    cexp = (uint)((float)(this.Character.CEXP - bexp) / (float)(nextExp - bexp) * 1000);
                }
                if (this.Character.JobJoint == PC_JOB.NONE)
                {
                    if (this.map.Info.Flag.Test(MapFlags.Dominion))
                    {
                        bexp = ExperienceManager.Instance.GetExpForLevel(this.Character.DominionJobLevel, Scripting.LevelType.JLEVEL2);
                        nextExp = ExperienceManager.Instance.GetExpForLevel((uint)(this.Character.DominionJobLevel + 1), Scripting.LevelType.JLEVEL2);

                        jexp = (uint)((float)(this.Character.DominionJEXP - bexp) / (nextExp - bexp) * 1000);
                    }
                    else
                    {
                        if (this.Character.Job == this.Character.JobBasic)
                        {
                            bexp = ExperienceManager.Instance.GetExpForLevel(this.Character.JobLevel1, Scripting.LevelType.JLEVEL);
                            nextExp = ExperienceManager.Instance.GetExpForLevel((uint)(this.Character.JobLevel1 + 1), Scripting.LevelType.JLEVEL);
                        }
                        else if (this.Character.Job == this.Character.Job2X)
                        {
                            bexp = ExperienceManager.Instance.GetExpForLevel(this.Character.JobLevel2X, Scripting.LevelType.JLEVEL2);
                            nextExp = ExperienceManager.Instance.GetExpForLevel((uint)(this.Character.JobLevel2X + 1), Scripting.LevelType.JLEVEL2);
                        }
                        else if (this.Character.Job == this.Character.Job2T)
                        {
                            bexp = ExperienceManager.Instance.GetExpForLevel(this.Character.JobLevel2T, Scripting.LevelType.JLEVEL2);
                            nextExp = ExperienceManager.Instance.GetExpForLevel((uint)(this.Character.JobLevel2T + 1), Scripting.LevelType.JLEVEL2);
                        }
                        else
                        {
                            bexp = ExperienceManager.Instance.GetExpForLevel(this.Character.JobLevel3, Scripting.LevelType.JLEVEL3);
                            nextExp = ExperienceManager.Instance.GetExpForLevel((uint)(this.Character.JobLevel3 + 1), Scripting.LevelType.JLEVEL3);
                        }
                        jexp = (uint)((float)(this.Character.JEXP - bexp) / (nextExp - bexp) * 1000);
                    }
                }
                else
                {
                    bexp = ExperienceManager.Instance.GetExpForLevel(this.Character.JointJobLevel, Scripting.LevelType.JLEVEL2);
                    nextExp = ExperienceManager.Instance.GetExpForLevel((uint)(this.Character.JointJobLevel + 1), Scripting.LevelType.JLEVEL2);

                    jexp = (uint)((float)(this.Character.JointJEXP - bexp) / (nextExp - bexp) * 1000);
                }
                p.EXPPercentage = (uint)cexp;
                p.JEXPPercentage = (uint)jexp;
                p.WRP = this.Character.WRP;
                p.ECoin = this.Character.ECoin;
                if (map.Info.Flag.Test(MapFlags.Dominion))
                {
                    p.Exp = (uint)this.Character.DominionCEXP;
                    p.JExp = (uint)this.Character.DominionJEXP;
                }
                else
                {
                    p.Exp = (long)this.Character.CEXP;
                    p.JExp = (long)this.Character.JEXP;
                }
                this.netIO.SendPacket(p);
            }
        }

        public void SendLvUP(Actor pc, byte type)
        {
            if (this.Character.Online)
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
                this.netIO.SendPacket(p);
            }
        }

        public void OnPlayerGreetings(Packets.Client.CSMG_PLAYER_GREETINGS p)
        {
            Actor actor = this.map.GetActor(p.ActorID);
            if (actor != null)
            {
                if (actor.type == ActorType.PC)
                {
                    ActorPC target = (ActorPC)actor;
                    if (target.Online && this.chara.Online)
                    {
                        ushort dir = this.map.CalcDir(this.chara.X, this.chara.Y, target.X, target.Y);
                        ushort dir2 = this.map.CalcDir(target.X, target.Y, this.chara.X, this.chara.Y);

                        Packets.Client.CSMG_PLAYER_MOVE p1 = new SagaMap.Packets.Client.CSMG_PLAYER_MOVE();
                        p1.X = this.chara.X;
                        p1.Y = this.chara.Y;
                        p1.Dir = dir + 180 > 360 ? (ushort)(dir - 180) : (ushort)(dir + 180);
                        p1.MoveType = MoveType.CHANGE_DIR;

                        this.OnMove(p1);

                        p1 = new SagaMap.Packets.Client.CSMG_PLAYER_MOVE();
                        p1.X = target.X;
                        p1.Y = target.Y;
                        p1.Dir = dir2;
                        p1.MoveType = MoveType.CHANGE_DIR;
                        MapClient.FromActorPC(target).OnMove(p1);

                        this.SendCharInfoUpdate();
                        MapClient.FromActorPC(target).SendCharInfoUpdate();

                        switch (Global.Random.Next(0, 3))
                        {
                            case 0:
                                SendMotion((MotionType)113, 1);
                                MapClient.FromActorPC(target).SendMotion((MotionType)113, 1);
                                break;
                            case 1:
                                SendMotion((MotionType)159, 0);
                                MapClient.FromActorPC(target).SendMotion((MotionType)159, 0);
                                break;
                            case 2:
                                SendMotion((MotionType)163, 0);
                                MapClient.FromActorPC(target).SendMotion((MotionType)163, 0);
                                break;
                        }

                        this.SendSystemMessage(string.Format("你向" + target.Name + "打招呼"));
                        MapClient.FromActorPC(target).SendSystemMessage(string.Format(this.Character.Name + "在和你打招呼~"));
                    }
                }
            }
        }

        public void OnPlayerElements(Packets.Client.CSMG_PLAYER_ELEMENTS p)
        {
            Packets.Server.SSMG_PLAYER_ELEMENTS p1 = new SagaMap.Packets.Server.SSMG_PLAYER_ELEMENTS();
            p1.AttackElements = this.chara.AttackElements;
            p1.DefenceElements = this.chara.Elements;
            this.netIO.SendPacket(p1);
        }
        public void OnPlayerElements()
        {
            Packets.Server.SSMG_PLAYER_ELEMENTS p1 = new SagaMap.Packets.Server.SSMG_PLAYER_ELEMENTS();
            p1.AttackElements = this.chara.AttackElements;
            p1.DefenceElements = this.chara.Elements;
            this.netIO.SendPacket(p1);
        }
        public void OnRequestPCInfo(Packets.Client.CSMG_ACTOR_REQUEST_PC_INFO p)
        {
            Packets.Server.SSMG_ACTOR_PC_INFO p1 = new SagaMap.Packets.Server.SSMG_ACTOR_PC_INFO();
            Actor pc = this.map.GetActor(p.ActorID);

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

            this.netIO.SendPacket(p1);
            if (pc.type == ActorType.PC)
            {
                ActorPC actor = (ActorPC)pc;
                if (actor.Ring != null)
                {
                    this.Character.e.OnActorRingUpdate(actor);
                }
            }


        }

        public void OnStatsPreCalc(Packets.Client.CSMG_PLAYER_STATS_PRE_CALC p)
        {
            //backup
            ushort str, dex, intel, agi, vit, mag;
            Packets.Server.SSMG_PLAYER_STATS_PRE_CALC p1 = new SagaMap.Packets.Server.SSMG_PLAYER_STATS_PRE_CALC();
            str = this.Character.Str;
            dex = this.Character.Dex;
            intel = this.Character.Int;
            agi = this.Character.Agi;
            vit = this.Character.Vit;
            mag = this.Character.Mag;

            this.Character.Str = p.Str;
            this.Character.Dex = p.Dex;
            this.Character.Int = p.Int;
            this.Character.Agi = p.Agi;
            this.Character.Vit = p.Vit;
            this.Character.Mag = p.Mag;

            StatusFactory.Instance.CalcStatus(this.Character);

            p1.ASPD = this.Character.Status.aspd;
            p1.ATK1Max = this.Character.Status.max_atk1;
            p1.ATK1Min = this.Character.Status.min_atk1;
            p1.ATK2Max = this.Character.Status.max_atk2;
            p1.ATK2Min = this.Character.Status.min_atk2;
            p1.ATK3Max = this.Character.Status.max_atk3;
            p1.ATK3Min = this.Character.Status.min_atk3;
            p1.AvoidCritical = this.Character.Status.avoid_critical;
            p1.AvoidMagic = this.Character.Status.avoid_magic;
            p1.AvoidMelee = this.Character.Status.avoid_melee;
            p1.AvoidRanged = this.Character.Status.avoid_ranged;
            p1.CSPD = this.Character.Status.cspd;
            p1.DefAddition = (ushort)this.Character.Status.def_add;
            p1.DefBase = this.Character.Status.def;
            p1.HitCritical = this.Character.Status.hit_critical;
            p1.HitMagic = this.Character.Status.hit_magic;
            p1.HitMelee = this.Character.Status.hit_melee;
            p1.HitRanged = this.Character.Status.hit_ranged;
            p1.MATKMax = this.Character.Status.max_matk;
            p1.MATKMin = this.Character.Status.min_matk;
            p1.MDefAddition = (ushort)this.Character.Status.mdef_add;
            p1.MDefBase = this.Character.Status.mdef;
            p1.Speed = this.Character.Speed;
            p1.HP = (ushort)this.Character.MaxHP;
            p1.MP = (ushort)this.Character.MaxMP;
            p1.SP = (ushort)this.Character.MaxSP;
            uint count = 0;
            foreach (uint i in this.Character.Inventory.MaxVolume.Values)
            {
                count += i;
            }
            p1.Capacity = (ushort)count;
            count = 0;
            foreach (uint i in this.Character.Inventory.MaxPayload.Values)
            {
                count += i;
            }
            p1.Payload = (ushort)count;

            //resotre
            this.Character.Str = str;
            this.Character.Dex = dex;
            this.Character.Int = intel;
            this.Character.Agi = agi;
            this.Character.Vit = vit;
            this.Character.Mag = mag;

            StatusFactory.Instance.CalcStatus(this.Character);

            this.netIO.SendPacket(p1);
        }

        public void OnStatsUp(Packets.Client.CSMG_PLAYER_STATS_UP p)
        {
            if (Configuration.Instance.Version < SagaLib.Version.Saga13)
            {
                switch (p.Type)
                {
                    case 0:
                        if (this.Character.StatsPoint >= StatusFactory.Instance.RequiredBonusPoint(this.Character.Str))
                        {
                            this.Character.StatsPoint -= StatusFactory.Instance.RequiredBonusPoint(this.Character.Str);
                            this.Character.Str += 1;
                        }
                        break;
                    case 1:
                        if (this.Character.StatsPoint >= StatusFactory.Instance.RequiredBonusPoint(this.Character.Dex))
                        {
                            this.Character.StatsPoint -= StatusFactory.Instance.RequiredBonusPoint(this.Character.Dex);
                            this.Character.Dex += 1;
                        }
                        break;
                    case 2:
                        if (this.Character.StatsPoint >= StatusFactory.Instance.RequiredBonusPoint(this.Character.Int))
                        {
                            this.Character.StatsPoint -= StatusFactory.Instance.RequiredBonusPoint(this.Character.Int);
                            this.Character.Int += 1;
                        }
                        break;
                    case 3:
                        if (this.Character.StatsPoint >= StatusFactory.Instance.RequiredBonusPoint(this.Character.Vit))
                        {
                            this.Character.StatsPoint -= StatusFactory.Instance.RequiredBonusPoint(this.Character.Vit);
                            this.Character.Vit += 1;
                        }
                        break;
                    case 4:
                        if (this.Character.StatsPoint >= StatusFactory.Instance.RequiredBonusPoint(this.Character.Agi))
                        {
                            this.Character.StatsPoint -= StatusFactory.Instance.RequiredBonusPoint(this.Character.Agi);
                            this.Character.Agi += 1;
                        }
                        break;
                    case 5:
                        if (this.Character.StatsPoint >= StatusFactory.Instance.RequiredBonusPoint(this.Character.Mag))
                        {
                            this.Character.StatsPoint -= StatusFactory.Instance.RequiredBonusPoint(this.Character.Mag);
                            this.Character.Mag += 1;
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
                        if (this.Character.StatsPoint >= StatusFactory.Instance.RequiredBonusPoint(this.Character.Str))
                        {
                            this.Character.StatsPoint -= StatusFactory.Instance.RequiredBonusPoint(this.Character.Str);
                            this.Character.Str += 1;
                        }
                    }
                }
                if (p.Dex > 0)
                {
                    for (int i = p.Dex; i > 0; i--)
                    {
                        if (this.Character.StatsPoint >= StatusFactory.Instance.RequiredBonusPoint(this.Character.Dex))
                        {
                            this.Character.StatsPoint -= StatusFactory.Instance.RequiredBonusPoint(this.Character.Dex);
                            this.Character.Dex += 1;
                        }
                    }
                }
                if (p.Int > 0)
                {
                    for (int i = p.Int; i > 0; i--)
                    {
                        if (this.Character.StatsPoint >= StatusFactory.Instance.RequiredBonusPoint(this.Character.Int))
                        {
                            this.Character.StatsPoint -= StatusFactory.Instance.RequiredBonusPoint(this.Character.Int);
                            this.Character.Int += 1;
                        }
                    }
                }
                if (p.Vit > 0)
                {
                    for (int i = p.Vit; i > 0; i--)
                    {
                        if (this.Character.StatsPoint >= StatusFactory.Instance.RequiredBonusPoint(this.Character.Vit))
                        {
                            this.Character.StatsPoint -= StatusFactory.Instance.RequiredBonusPoint(this.Character.Vit);
                            this.Character.Vit += 1;
                        }
                    }
                }
                if (p.Agi > 0)
                {
                    for (int i = p.Agi; i > 0; i--)
                    {
                        if (this.Character.StatsPoint >= StatusFactory.Instance.RequiredBonusPoint(this.Character.Agi))
                        {
                            this.Character.StatsPoint -= StatusFactory.Instance.RequiredBonusPoint(this.Character.Agi);
                            this.Character.Agi += 1;
                        }
                    }
                }

                if (p.Mag > 0)
                {
                    for (int i = p.Mag; i > 0; i--)
                    {
                        if (this.Character.StatsPoint >= StatusFactory.Instance.RequiredBonusPoint(this.Character.Mag))
                        {
                            this.Character.StatsPoint -= StatusFactory.Instance.RequiredBonusPoint(this.Character.Mag);
                            this.Character.Mag += 1;
                        }
                    }
                }

            }
            StatusFactory.Instance.CalcStatus(this.Character);
            SendActorHPMPSP(this.Character);
            SendStatus();
            SendStatusExtend();
            SendCapacity();
            SendMaxCapacity();
            SendPlayerLevel();
        }

        public void SendWRPRanking(ActorPC pc)
        {
            Packets.Server.SSMG_ACTOR_WRP_RANKING p = new SagaMap.Packets.Server.SSMG_ACTOR_WRP_RANKING();
            p.ActorID = pc.ActorID;
            p.Ranking = pc.WRPRanking;
            this.netIO.SendPacket(p);
        }

        public void OnPlayerReturnHome(Packets.Client.CSMG_PLAYER_RETURN_HOME p)
        {
            if (this.Character.HP == 0)
            {
                this.Character.HP = 1;
                this.Character.MP = 1;
                this.Character.SP = 1;
            }

            if (this.Character.SaveMap == 0)
            {
                this.Character.SaveMap = 10023100;
                this.Character.SaveX = 242;
                this.Character.SaveY = 128;
            }

            this.Character.BattleStatus = 0;
            this.SendChangeStatus();
            this.Character.Buff.Dead = false;
            this.Character.Buff.TurningPurple = false;
            this.Character.Motion = MotionType.STAND;
            this.Character.MotionLoop = false;
            this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, this.Character, true);

            Skill.SkillHandler.Instance.CastPassiveSkills(this.Character);
            SendPlayerInfo();

            if (this.map.ID == this.Character.SaveMap)
            {
                if (this.Map.Info.Healing)
                {
                    if (!this.Character.Tasks.ContainsKey("CityRecover"))
                    {
                        Tasks.PC.CityRecover task = new SagaMap.Tasks.PC.CityRecover(this);
                        this.Character.Tasks.Add("CityRecover", task);
                        task.Activate();
                    }
                }
                else
                {
                    if (this.Character.Tasks.ContainsKey("CityRecover"))
                    {
                        this.Character.Tasks["CityRecover"].Deactivate();
                        this.Character.Tasks.Remove("CityRecover");
                    }
                }

                if (this.Map.Info.Cold || this.map.Info.Hot || this.map.Info.Wet)
                {
                    if (!this.Character.Tasks.ContainsKey("CityDown"))
                    {
                        Tasks.PC.CityDown task = new SagaMap.Tasks.PC.CityDown(this);
                        this.Character.Tasks.Add("CityDown", task);
                        task.Activate();
                    }
                }
                else
                {
                    if (this.Character.Tasks.ContainsKey("CityDown"))
                    {
                        this.Character.Tasks["CityDown"].Deactivate();
                        this.Character.Tasks.Remove("CityDown");
                    }
                }
            }

            if (Configuration.Instance.HostedMaps.Contains(this.Character.SaveMap))
            {
                MapInfo info = MapInfoFactory.Instance.MapInfo[this.Character.SaveMap];
                this.Map.SendActorToMap(this.Character, this.Character.SaveMap, Global.PosX8to16(this.Character.SaveX, info.width),
                    Global.PosY8to16(this.Character.SaveY, info.height));
            }

            Scripting.Event evnt = null;
            if (evnt != null)
            {
                evnt.CurrentPC = null;
                this.scriptThread = null;
                this.currentEvent = null;
                ClientManager.RemoveThread(System.Threading.Thread.CurrentThread.Name);
                ClientManager.LeaveCriticalArea();
            }
        }

    }
}
