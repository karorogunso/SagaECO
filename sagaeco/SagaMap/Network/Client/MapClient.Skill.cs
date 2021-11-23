using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;

using SagaDB;
using SagaDB.Actor;
using SagaDB.Skill;
using SagaLib;
using SagaMap;
using SagaMap.Manager;
using SagaMap.Skill;
using SagaMap.Skill.Additions.Global;
using SagaDB.Item;
using System.Threading;

namespace SagaMap.Network.Client
{
    public partial class MapClient
    {
        DateTime skillDelay = DateTime.Now;
        DateTime attackStamp = DateTime.Now;
        DateTime hackStamp = DateTime.Now;
        DateTime assassinateStamp = DateTime.Now;
        bool AttactFinished = false;
        int hackCount = 0;
        short lastAttackRandom;
        short lastCastRandom;
        public List<uint> nextCombo = new List<uint>();
        public DateTime SkillDelay { set { skillDelay = value; } }
        public void OnSkillLvUP(Packets.Client.CSMG_SKILL_LEVEL_UP p)
        {
            Packets.Server.SSMG_SKILL_LEVEL_UP p1 = new SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP();
            ushort skillID = p.SkillID;
            byte type = 0;
            if (SkillFactory.Instance.CheckSkillList(Character, SkillFactory.SkillPaga.p1).ContainsKey(skillID))
                type = 1;
            else if (SkillFactory.Instance.CheckSkillList(Character, SkillFactory.SkillPaga.p21).ContainsKey(skillID))
                type = 2;
            else if (SkillFactory.Instance.CheckSkillList(Character, SkillFactory.SkillPaga.p22).ContainsKey(skillID))
                type = 3;
            else if (SkillFactory.Instance.CheckSkillList(Character, SkillFactory.SkillPaga.p3).ContainsKey(skillID))
                type = 4;
            if (type == 0)
            {
                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP.LearnResult.SKILL_NOT_EXIST;
            }
            else
            {
                if (type == 1)
                {
                    if (!Character.Skills.ContainsKey((uint)skillID))
                    {
                        p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP.LearnResult.SKILL_NOT_LEARNED;
                    }
                    else
                    {
                        SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(skillID, Character.Skills[skillID].Level);
                        if (Character.JobLevel1 < skill.JobLv)
                        {
                            SendSystemMessage(string.Format("{1} 未达到技能升级等级！需求等级：{0}", skill.JobLv, skill.Name));
                            return;
                        }
                        if (skill.Level + 1 == 4)
                        {
                            if (Character.CInt["进阶技能解锁" + skill.ID] != 1 && Character.Account.GMLevel < 20)
                            {
                                SendSystemMessage(string.Format("你对 {0} 的进阶技巧还尚未领悟！", skill.Name));
                                return;
                            }
                            if (Character.SkillPoint < 2)
                            {
                                SendSystemMessage(string.Format("习得进阶技巧需要2点技能点！", skill.Name));
                                return;
                            }
                        }
                        if (skill.Level + 1 == 5)
                        {
                            if (Character.AInt["高级技能解锁" + skill.ID] != 1 && Character.Account.GMLevel < 20)
                            {
                                SendSystemMessage(string.Format("你对 {0} 的高级技巧还尚未领悟！", skill.Name));
                                return;
                            }
                            if (Character.SkillPoint < 3)
                            {
                                SendSystemMessage(string.Format("习得进阶技巧需要3点技能点！", skill.Name));
                                return;
                            }
                        }
                        if (Character.SkillPoint < 1)
                        {
                            p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP.LearnResult.NOT_ENOUGH_SKILL_POINT;
                        }
                        else
                        {
                            /*if (this.Character.Inventory.GetItem(10022500, SagaDB.Item.Inventory.SearchType.ITEM_ID) == null)//条件待改
                            {
                                this.SendSystemMessage((string.Format("没有技能升级所需要的物品【大壶】，无法升级技能")));
                                return;
                            }*/
                            if (Character.Skills[skillID].Level == Character.Skills[skillID].MaxLevel)
                            {
                                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP.LearnResult.SKILL_MAX_LEVEL_EXEED;
                            }
                            else
                            {
                                if (skill.Level + 1 == 4)
                                    Character.SkillPoint -= 2;
                                else if (skill.Level + 1 == 5)
                                    Character.SkillPoint -= 3;
                                else
                                    Character.SkillPoint -= 1;


                                Character.Skills[skillID] = SkillFactory.Instance.GetSkill(skillID, (byte)(Character.Skills[skillID].Level + 1));
                                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP.LearnResult.OK;
                                p1.SkillID = skillID;
                            }
                        }
                    }
                }
                if (type == 2)
                {
                    if (!Character.Skills2.ContainsKey((uint)skillID))
                    {
                        p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP.LearnResult.SKILL_NOT_LEARNED;
                    }
                    else
                    {
                        SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(skillID, Character.Skills2[skillID].Level);
                        if (Character.JobLevel2X < skill.JobLv)
                        {
                            SendSystemMessage(string.Format("{1} 未达到技能升级等级！需求等级：{0}", skill.JobLv, skill.Name));
                            return;
                        }

                        if (skill.ID == 14030 && skill.Level + 1 >= 2) //电报
                        {
                            if (!(Character.Skills.ContainsKey(14053) && Character.Skills[14053].Level >= 3))   //次元渡航等级在3级以上才可以学习
                            {
                                SendSystemMessage("你对这门技巧的掌握还不够熟练！");
                                return;
                            }
                        }

                        if (skill.Level + 1 == 4)
                        {
                            if (Character.CInt["进阶技能解锁" + skill.ID] != 1 && Character.Account.GMLevel < 20)
                            {
                                SendSystemMessage(string.Format("你对 {0} 的进阶技巧还尚未领悟！", skill.Name));
                                return;
                            }
                            if (Character.SkillPoint < 2)
                            {
                                SendSystemMessage(string.Format("习得进阶技巧需要2点技能点！", skill.Name));
                                return;
                            }
                        }
                        if (skill.Level + 1 == 5)
                        {
                            if (Character.AInt["高级技能解锁" + skill.ID] != 1 && Character.Account.GMLevel < 20)
                            {
                                SendSystemMessage(string.Format("你对 {0} 的高级技巧还尚未领悟！", skill.Name));
                                return;
                            }
                            if (Character.SkillPoint < 3)
                            {
                                SendSystemMessage(string.Format("习得进阶技巧需要3点技能点！", skill.Name));
                                return;
                            }
                        }
                        if (Character.SkillPoint2X < 1)
                        {
                            p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP.LearnResult.NOT_ENOUGH_SKILL_POINT;
                        }
                        else
                        {
                            if (Character.Skills2[skillID].Level == Character.Skills2[skillID].MaxLevel)
                            {
                                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP.LearnResult.SKILL_MAX_LEVEL_EXEED;
                            }
                            else
                            {
                                if (skill.Level + 1 == 4)
                                    Character.SkillPoint2X -= 2;
                                else if (skill.Level + 1 == 5)
                                    Character.SkillPoint2X -= 3;
                                else
                                    Character.SkillPoint2X -= 1;

                                SagaDB.Skill.Skill data = SkillFactory.Instance.GetSkill(skillID, (byte)(Character.Skills2[skillID].Level + 1));
                                Character.Skills2[skillID] = data;
                                Character.Skills2_1[skillID] = data;
                                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP.LearnResult.OK;
                                p1.SkillID = skillID;
                            }
                        }
                    }
                }
                if (type == 3)
                {
                    if (!Character.Skills2.ContainsKey((uint)skillID))
                    {
                        p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP.LearnResult.SKILL_NOT_LEARNED;
                    }
                    else
                    {
                        SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(skillID, Character.Skills2[skillID].Level);
                        if (Character.JobLevel2T < skill.JobLv)
                        {
                            SendSystemMessage(string.Format("{1} 未达到技能升级等级！需求等级：{0}", skill.JobLv, skill.Name));
                            return;
                        }
                        if (skill.Level + 1 == 4)
                        {
                            if (Character.CInt["进阶技能解锁" + skill.ID] != 1 && Character.Account.GMLevel < 20)
                            {
                                SendSystemMessage(string.Format("你对 {0} 的进阶技巧还尚未领悟！", skill.Name));
                                return;
                            }
                            if (Character.SkillPoint < 2)
                            {
                                SendSystemMessage(string.Format("习得进阶技巧需要2点技能点！", skill.Name));
                                return;
                            }
                        }
                        if (skill.Level + 1 == 5)
                        {
                            if (Character.AInt["高级技能解锁" + skill.ID] != 1 && Character.Account.GMLevel < 20)
                            {
                                SendSystemMessage(string.Format("你对 {0} 的高级技巧还尚未领悟！", skill.Name));
                                return;
                            }
                            if (Character.SkillPoint < 3)
                            {
                                SendSystemMessage(string.Format("习得进阶技巧需要2点技能点！", skill.Name));
                                return;
                            }
                        }
                        if (Character.SkillPoint2T < 1)
                        {
                            p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP.LearnResult.NOT_ENOUGH_SKILL_POINT;
                        }
                        else
                        {
                            if (Character.Skills2[skillID].Level == Character.Skills2[skillID].MaxLevel)
                            {
                                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP.LearnResult.SKILL_MAX_LEVEL_EXEED;
                            }
                            else
                            {
                                if (skill.Level + 1 == 4)
                                    Character.SkillPoint2T -= 2;
                                else if (skill.Level + 1 == 5)
                                    Character.SkillPoint2T -= 3;
                                else
                                    Character.SkillPoint2T -= 1;
                                SagaDB.Skill.Skill data = SkillFactory.Instance.GetSkill(skillID, (byte)(Character.Skills2[skillID].Level + 1));
                                Character.Skills2[skillID] = data;
                                Character.Skills2_2[skillID] = data;
                                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP.LearnResult.OK;
                                p1.SkillID = skillID;
                            }
                        }
                    }
                }
                if (type == 4)
                {
                    if (!Character.Skills3.ContainsKey((uint)skillID))
                    {
                        p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP.LearnResult.SKILL_NOT_LEARNED;
                    }
                    else
                    {
                        SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(skillID, Character.Skills3[skillID].Level);
                        if (Character.JobLevel3 < skill.JobLv)
                        {
                            SendSystemMessage(string.Format("{1} 未达到技能升级等级！需求等级：{0}", skill.JobLv, skill.Name));
                            return;
                        }
                        if (skill.Level + 1 == 4)
                        {
                            if (Character.CInt["进阶技能解锁" + skill.ID] != 1 && Character.Account.GMLevel < 20)
                            {
                                SendSystemMessage(string.Format("你对 {0} 的进阶技巧还尚未领悟！", skill.Name));
                                return;
                            }
                            if (Character.SkillPoint < 2)
                            {
                                SendSystemMessage(string.Format("习得进阶技巧需要2点技能点！", skill.Name));
                                return;
                            }
                        }
                        if (skill.Level + 1 == 5)
                        {
                            if (Character.AInt["高级技能解锁" + skill.ID] != 1 && Character.Account.GMLevel < 20)
                            {
                                SendSystemMessage(string.Format("你对 {0} 的高级技巧还尚未领悟！", skill.Name));
                                return;
                            }
                            if (Character.SkillPoint < 3)
                            {
                                SendSystemMessage(string.Format("习得进阶技巧需要2点技能点！", skill.Name));
                                return;
                            }
                        }
                        if (Character.SkillPoint3 < 1)
                        {
                            p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP.LearnResult.NOT_ENOUGH_SKILL_POINT;
                        }
                        else
                        {
                            if (Character.Skills3[skillID].Level == Character.Skills3[skillID].MaxLevel)
                            {
                                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP.LearnResult.SKILL_MAX_LEVEL_EXEED;
                            }
                            else
                            {
                                if (skill.Level + 1 == 4)
                                    Character.SkillPoint3 -= 2;
                                else if (skill.Level + 1 == 5)
                                    Character.SkillPoint3 -= 3;
                                else
                                    Character.SkillPoint3 -= 1;

                                Character.Skills3[skillID] = SkillFactory.Instance.GetSkill(skillID, (byte)(Character.Skills3[skillID].Level + 1));
                                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP.LearnResult.OK;
                                p1.SkillID = skillID;
                            }
                        }
                    }
                }
            }

            p1.SkillPoints = Character.SkillPoint;
            if (Character.Job == Character.Job2X)
            {
                p1.SkillPoints2 = Character.SkillPoint2X;
                p1.Job = 1;
            }
            else if (Character.Job == Character.Job2T)
            {
                p1.SkillPoints2 = Character.SkillPoint2T;
                p1.Job = 2;
            }
            else
            {
                p1.Job = 0;
            }
            netIO.SendPacket(p1);
            SendSkillList();

            SkillHandler.Instance.CastPassiveSkills(Character);
            SendPlayerInfo();

            MapServer.charDB.SaveChar(Character, true);
        }

        public void OnSkillLearn(Packets.Client.CSMG_SKILL_LEARN p)
        {
            Packets.Server.SSMG_SKILL_LEARN p1 = new SagaMap.Packets.Server.SSMG_SKILL_LEARN();
            ushort skillID = p.SkillID;
            byte type = 0;
            if (SkillFactory.Instance.CheckSkillList(Character, SkillFactory.SkillPaga.p1).ContainsKey(skillID))
                type = 1;
            else if (SkillFactory.Instance.CheckSkillList(Character, SkillFactory.SkillPaga.p21).ContainsKey(skillID))
                type = 2;
            else if (SkillFactory.Instance.CheckSkillList(Character, SkillFactory.SkillPaga.p22).ContainsKey(skillID))
                type = 3;
            else if (SkillFactory.Instance.CheckSkillList(Character, SkillFactory.SkillPaga.p3).ContainsKey(skillID))
                type = 4;
            if (type == 0)
            {
                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEARN.LearnResult.SKILL_NOT_EXIST;
            }
            else
            {
                if (type == 1)
                {
                    byte jobLV = SkillFactory.Instance.CheckSkillList(Character, SkillFactory.SkillPaga.p1)[skillID];
                    if (Character.Skills.ContainsKey((uint)skillID))
                    {
                        p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEARN.LearnResult.SKILL_NOT_LEARNED;
                    }
                    else
                    {
                        if (Character.SkillPoint < 3)
                        {
                            p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEARN.LearnResult.NOT_ENOUGH_SKILL_POINT;
                        }
                        else
                        {
                            SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(skillID, 1);
                            if (skill.JobLv != 0)
                                jobLV = skill.JobLv;

                            //if (this.Character.JobLevel1 < jobLV)
                            if (Character.JobLevel3 < jobLV)
                            {
                                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEARN.LearnResult.NOT_ENOUGH_JOB_LEVEL;
                            }

                            else
                            {
                                Character.SkillPoint -= 3;
                                Character.Skills.Add(skillID, skill);
                                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEARN.LearnResult.OK;
                                p1.SkillID = skillID;
                                p1.SkillPoints = Character.SkillPoint;
                                if (Character.Job == Character.Job2X)
                                    p1.SkillPoints2 = Character.SkillPoint2X;
                                else if (Character.Job == Character.Job2T)
                                    p1.SkillPoints2 = Character.SkillPoint2T;
                            }
                        }
                    }
                }
                else if (type == 2)
                {
                    byte jobLV = SkillFactory.Instance.CheckSkillList(Character, SkillFactory.SkillPaga.p21)[skillID];
                    if (Character.Skills2.ContainsKey((uint)skillID))
                    {
                        p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEARN.LearnResult.SKILL_NOT_LEARNED;
                    }
                    else
                    {
                        if (Character.SkillPoint2X < 3)
                        {
                            p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEARN.LearnResult.NOT_ENOUGH_SKILL_POINT;
                        }
                        else
                        {
                            SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(skillID, 1);
                            if (skill.JobLv != 0)
                                jobLV = skill.JobLv;
                            //if (this.Character.JobLevel2X < jobLV)
                            if (Character.JobLevel3 < jobLV)
                            {
                                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEARN.LearnResult.NOT_ENOUGH_JOB_LEVEL;
                            }
                            else
                            {
                                Character.SkillPoint2X -= 3;
                                Character.Skills2.Add(skillID, skill);
                                Character.Skills2_1.Add(skillID, skill);
                                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEARN.LearnResult.OK;
                                p1.SkillID = skillID;
                                p1.SkillPoints = Character.SkillPoint;
                                p1.SkillPoints2 = Character.SkillPoint2X;
                            }
                        }
                    }
                }
                else if (type == 3)
                {
                    byte jobLV = SkillFactory.Instance.CheckSkillList(Character, SkillFactory.SkillPaga.p22)[skillID];

                    if (Character.Skills2.ContainsKey((uint)skillID))
                    {
                        p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEARN.LearnResult.SKILL_NOT_LEARNED;
                    }
                    else
                    {
                        if (Character.SkillPoint2T < 3)
                        {
                            p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEARN.LearnResult.NOT_ENOUGH_SKILL_POINT;
                        }
                        else
                        {
                            SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(skillID, 1);
                            if (skill.JobLv != 0)
                                jobLV = skill.JobLv;
                            //if (this.Character.JobLevel2T < jobLV)
                            if (Character.JobLevel3 < jobLV)
                            {
                                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEARN.LearnResult.NOT_ENOUGH_JOB_LEVEL;
                            }
                            else
                            {
                                Character.SkillPoint2T -= 3;
                                Character.Skills2.Add(skillID, skill);
                                Character.Skills2_2.Add(skillID, skill);
                                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEARN.LearnResult.OK;
                                p1.SkillID = skillID;
                                p1.SkillPoints = Character.SkillPoint;
                                p1.SkillPoints2 = Character.SkillPoint2T;
                            }
                        }
                    }
                }
                else if (type == 4)
                {
                    byte jobLV = SkillFactory.Instance.CheckSkillList(Character, SkillFactory.SkillPaga.p3)[skillID];

                    if (Character.Skills3.ContainsKey((uint)skillID))
                    {
                        p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEARN.LearnResult.SKILL_NOT_LEARNED;
                    }
                    else
                    {
                        if (Character.SkillPoint3 < 3)
                        {
                            p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEARN.LearnResult.NOT_ENOUGH_SKILL_POINT;
                        }
                        else
                        {
                            SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(skillID, 1);
                            if (skill.JobLv != 0)
                                jobLV = skill.JobLv;
                            if (Character.JobLevel3 < jobLV)
                            {
                                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEARN.LearnResult.NOT_ENOUGH_JOB_LEVEL;
                            }
                            else
                            {
                                Character.SkillPoint3 -= 3;
                                Character.Skills3.Add(skillID, skill);
                                //this.Character.Skills.Add(skillID, skill);
                                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEARN.LearnResult.OK;
                                p1.SkillID = skillID;
                                p1.SkillPoints = Character.SkillPoint;
                                p1.SkillPoints2 = Character.SkillPoint3;
                            }
                        }
                    }
                }
            }

            netIO.SendPacket(p1);
            SendSkillList();

            SkillHandler.Instance.CastPassiveSkills(Character);
            SendPlayerInfo();

            MapServer.charDB.SaveChar(Character, true);
        }

        Packets.Client.CSMG_SKILL_ATTACK Lastp;
        int delay;

        public bool WeaponAttack(uint id, uint actorID, ActorPC pc, byte level = 1)
        {
            try
            {
                Actor dActor = Map.GetActor(actorID);
                int range = 3;
                if (id > 19000 || id == 3001 || id == 3073 || id == 3083) range = 8;
                if (id > 12000 && id < 13000) range = 15;
                int delay = 1500;

                delay = 1900 - pc.Status.aspd * 2;
                if (delay < 1000)
                    delay = 1000;

                if (range + 5 < Math.Max(Math.Abs(Character.X - dActor.X) / 100, Math.Abs(Character.Y - dActor.Y) / 100))
                {
                    return true;
                }
                if (DateTime.Now >= pc.TTime["特殊刀攻击间隔"] && DateTime.Now >= pc.TTime["剑法间隔"])
                {
                    if (pc.Status.Additions.ContainsKey("魔法少女")&& pc.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))
                    {
                        ItemType tp = pc.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType;
                        switch (tp)
                        {
                            case ItemType.GUN:
                                level = 4;
                                delay = 1000;
                                break;
                            case ItemType.RIFLE:
                                level = 4;
                                break;
                            case ItemType.BOW:
                                level = 3;
                                break;
                            case ItemType.SPEAR:
                            case ItemType.RAPIER:
                                level = 2;
                                break;
                            default:
                                break;
                        }
                    }
                    SkillArg arg1 = new SkillArg();
                    arg1.Init();
                    arg1.skill = SkillFactory.Instance.GetSkill(id, level);
                    arg1.dActor = dActor.ActorID;
                    arg1.argType = SkillArg.ArgType.Cast;
                    arg1.delay = 0;
                    arg1.result = 0;
                    arg1.useMPSP = true;
                    Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, arg1, Character, true);

                    if (arg1.skill.SP > pc.SP || arg1.skill.MP > pc.MP)
                    {
                        if (Character.Status.Additions.ContainsKey("旋风斩")) SkillHandler.RemoveAddition(Character, "旋风斩");
                        if (Character.Status.Additions.ContainsKey("地裂斩")) SkillHandler.RemoveAddition(Character, "地裂斩");
                        if (Character.Status.Additions.ContainsKey("炎龙斩")) SkillHandler.RemoveAddition(Character, "炎龙斩");
                        if (Character.Status.Additions.ContainsKey("寒冰斩")) SkillHandler.RemoveAddition(Character, "寒冰斩");
                        if (Character.Status.Additions.ContainsKey("圣光之矛")) SkillHandler.RemoveAddition(Character, "圣光之矛");
                        if (Character.Status.Additions.ContainsKey("Scorponok")) SkillHandler.RemoveAddition(Character, "Scorponok");
                        SkillArg arg = new SkillArg();
                        arg.sActor = Character.ActorID;
                        arg.type = (ATTACK_TYPE)0xff;
                        arg.affectedActors.Add(Character);
                        arg.Init();
                        Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.ATTACK, arg, Character, true);
                        SendSkillDummy(3311, 1);
                        return false;
                    }

                    Tasks.PC.SkillCast task = new SagaMap.Tasks.PC.SkillCast(this, arg1);
                    if (!Character.Tasks.ContainsKey("SkillCast"))
                        Character.Tasks.Add("SkillCast", task);
                    task.Activate();

                    pc.TTime["特殊刀攻击间隔"] = DateTime.Now + new TimeSpan(0, 0, 0, 0, delay);
                    pc.TTime["剑法间隔"] = DateTime.Now + new TimeSpan(0, 0, 0, 0, delay);

                    if (Character.Job == PC_JOB.CARDINAL)
                    {
                        if (Character.EP < 5000 && Character.TInt["漆黑之魂增益"] > 0)
                        {
                            Character.MP += (uint)Character.TInt["漆黑之魂增益"];
                            if (Character.MP > Character.MaxMP)
                                Character.MP = Character.MaxMP;
                            if (Character.TInt["漆黑之魂增益"] == 125)
                            {
                                if (Character.EP > 300)
                                    Character.EP -= 300;
                                else Character.EP = 0;
                            }
                        }
                        else if (Character.EP >= 5000 && Character.TInt["纯白信仰增益2"] > 0)
                        {
                            Character.MP += (uint)Character.TInt["纯白信仰增益2"];
                            if (Character.MP > Character.MaxMP)
                                Character.MP = Character.MaxMP;
                            if (Character.TInt["纯白信仰增益2"] == 80)
                            {
                                Character.EP += 300;
                                if (Character.EP > Character.MaxEP)
                                    Character.EP = Character.MaxEP;
                            }
                        }
                    }

                    if (Character.Status.Additions.ContainsKey("Scorponok"))
                    {
                        pc.TTime["特殊刀攻击间隔"] = DateTime.Now + new TimeSpan(0, 0, 0, 0, 1000);
                        pc.TTime["剑法间隔"] = DateTime.Now;
                    }
                    Scripting.Timer timer = new Scripting.Timer("斩系间隔" + Character.Name, 0, delay);
                    timer.AttachedPC = Character;
                    timer.OnTimerCall += (s, e) =>
                    {
                        if (e == null) timer.Deactivate();
                        if (s == null) timer.Deactivate();
                        SkillArg arg = new SkillArg();
                        try
                        {
                            arg.sActor = Character.ActorID;
                            arg.type = (ATTACK_TYPE)0xff;
                            arg.affectedActors.Add(Character);
                            arg.Init();
                            Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.ATTACK, arg, Character, true);
                            timer.Deactivate();
                        }
                        catch (Exception ex)
                        {
                            Logger.ShowError(timer.Name);
                            timer.Deactivate();
                            SagaLib.Logger.ShowError(ex);
                        }
                    };
                    timer.Activate();
                    /*DefaultBuff 斩系间隔 = new DefaultBuff(arg1.skill, this.Character, "斩系间隔", 1000);
                    斩系间隔.OnAdditionEnd += (s, e) =>
                    {
                        SkillArg arg = new SkillArg();
                        arg.sActor = this.Character.ActorID;
                        arg.type = (ATTACK_TYPE)0xff;
                        arg.affectedActors.Add(this.Character);
                        arg.Init();
                        this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.ATTACK, arg, this.Character, true);
                    };
                    SkillHandler.ApplyAddition(this.Character, 斩系间隔);*/
                }
                else
                {
                    SkillArg arg1 = new SkillArg();
                    arg1.Init();
                    arg1.skill = SkillFactory.Instance.GetSkill(id, level);
                    arg1.dActor = dActor.ActorID;
                    SkillArg arg = new SkillArg();
                    arg.sActor = Character.ActorID;
                    arg.type = (ATTACK_TYPE)0xff;
                    arg.affectedActors.Add(Character);
                    arg.Init();
                    Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.ATTACK, arg, Character, true);
                }
                return true;
            }
            catch (Exception ex) { SagaLib.Logger.ShowError(ex); return false; }

        }

        Thread main;
        public void OnSkillAttack(Packets.Client.CSMG_SKILL_ATTACK p, bool auto)
        {
            try
            {
                if (Character.Tasks.ContainsKey("SkillCast"))
                    throw new Exception("正在吟唱其他技能");
                bool needthread = true;
                if (Character == null || !Character.Online || Character.HP == 0)
                    throw new Exception("角色状态异常");
                Actor dActor = Map.GetActor(p.ActorID);
                SkillArg arg;
                if (dActor.type == ActorType.PC && ((ActorPC)dActor).Mode != PlayerMode.COLISEUM_MODE)
                    throw new Exception("非PVP状态玩家目标");
                Actor sActor = map.GetActor(Character.ActorID);
                if (sActor == null || dActor == null)
                    throw new Exception("角色状态异常");
                if (sActor.MapID != dActor.MapID)
                    throw new Exception("不同地图");
                if (sActor.TInt["targetID"] != dActor.ActorID)
                {
                    sActor.TInt["targetID"] = (int)dActor.ActorID;
                    SendSystemMessage("锁定了【" + dActor.Name + "】作为目标");
                }

                if (needthread)
                {
                    if (!auto && Character.AutoAttack)//客户端发来的攻击，但已开启自动
                    {
                        Character.TInt["攻击检测"] += 1;
                        if (Character.TInt["攻击检测"] >= 3)
                            ScriptManager.Instance.VariableHolder.AInt[Character.Name + "攻击检测"] += Character.TInt["攻击检测"];
                        Lastp = p;
                        return;
                    }
                    if (auto && !Character.AutoAttack)//自动攻击，但人物处于不能自动攻击状态
                        throw new Exception("无法进行自动攻击");
                }
                byte s = 0;
                if (Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.RIGHT_HAND))//自動特殊攻擊
                {
                    ItemType itype = Character.Inventory.Equipments[EnumEquipSlot.RIGHT_HAND].BaseData.itemType;
                    if ((!Character.Status.Additions.ContainsKey("自由射击") && (itype == ItemType.BOOK || itype == ItemType.STAFF || itype == ItemType.STRINGS))||
                        (Character.Status.Additions.ContainsKey("魔法少女") && (itype == ItemType.GUN || itype == ItemType.BOW || itype == ItemType.SPEAR || itype == ItemType.RIFLE || itype == ItemType.RAPIER)))
                    {
                        uint skillid = 3001;
                        byte level = 1;
                        if (Character.Job == PC_JOB.CARDINAL)
                        {
                            if (Character.EP > 5000)
                                skillid = 3073;
                            else
                            {
                                skillid = 3083;
                                if (Character.TInt["漆黑之魂增益"] == 125)
                                    level = 2;
                            }
                        }
                        if (WeaponAttack(skillid, dActor.ActorID, Character, level))
                            return;
                    }
                }
                //射程判定
                if (Character == null || dActor == null)
                    throw new Exception("角色状态异常");
                if (Character.Range + 1
                    < Math.Max(Math.Abs(Character.X - dActor.X) / 100
                    , Math.Abs(Character.Y - dActor.Y) / 100))
                {
                    arg = new SkillArg();
                    arg.sActor = Character.ActorID;
                    arg.type = (ATTACK_TYPE)0xff;
                    arg.affectedActors.Add(Character);
                    arg.Init();
                    Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.ATTACK, arg, Character, true);
                    Character.AutoAttack = false;
                    return;
                }
                Character.LastAttackActorID = 0;
                
                if (Character.Status.Additions.ContainsKey("Meditatioon"))
                {
                    Character.Status.Additions["Meditatioon"].AdditionEnd();
                    Character.Status.Additions.Remove("Meditatioon");
                }
                if (Character.Status.Additions.ContainsKey("Hiding"))
                {
                    Character.Status.Additions["Hiding"].AdditionEnd();
                    Character.Status.Additions.Remove("Hiding");
                }
                if (Character.Status.Additions.ContainsKey("fish"))
                {
                    Character.Status.Additions["fish"].AdditionEnd();
                    Character.Status.Additions.Remove("fish");
                }
                if (Character.Status.Additions.ContainsKey("IAmTree"))
                {
                    Character.Status.Additions["IAmTree"].AdditionEnd();
                    Character.Status.Additions.Remove("IAmTree");
                }
                if (Character.Status.Additions.ContainsKey("Cloaking"))
                {
                    Character.Status.Additions["Cloaking"].AdditionEnd();
                    Character.Status.Additions.Remove("Cloaking");
                }
                if (Character.Status.Additions.ContainsKey("Invisible"))
                {
                    Character.Status.Additions["Invisible"].AdditionEnd();
                    Character.Status.Additions.Remove("Invisible");
                }
                if (Character.Status.Additions.ContainsKey("Stun") || Character.Status.Additions.ContainsKey("Sleep") || Character.Status.Additions.ContainsKey("Frosen") ||
                Character.Status.Additions.ContainsKey("Stone"))
                    return;
                if (dActor == null || DateTime.Now + new TimeSpan(0, 0, 0, 0, 100) < attackStamp )
                    return;
                if (dActor.HP == 0 || dActor.Buff.Dead)
                {
                    Character.AutoAttack = false;
                    return;
                }
                arg = new SkillArg();
                delay = 600 * (900 - Character.Status.aspd) / 900 + 250;
               
                if (Character.Status.Additions.ContainsKey("时间扭曲"))
                    delay = (int)(delay * 0.5f);
                delay = (int)(delay * arg.delayRate);
                if (delay < 150)
                    delay = 150;
                
                if (Character.HP > 0 && !AttactFinished && needthread && auto)//处于战斗状态
                    SkillHandler.Instance.Attack(Character, dActor, arg);//攻击
                if (arg.affectedActors.Count > 0)
                    attackStamp = DateTime.Now + new TimeSpan(0, 0, 0, 0, delay);
                Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.ATTACK, arg, Character, true);
                AttactFinished = false;
                PartnerTalking(Character.Partner, TALK_EVENT.BATTLE, 1, 20000);

                if (needthread)
                {
                    Lastp = p;
                    Character.LastAttackActorID = dActor.ActorID;
                    try
                    {
                        if (Character == null) return;
                        if (this == null) return;
                        if (auto) return;
                        int duetime = delay;
                        if (DateTime.Now > attackStamp)
                            duetime = 0;
                        if (!Character.Tasks.ContainsKey("自动攻击线程"))
                        {
                            Character.AutoAttack = true;
                            Tasks.PC.AutoAttack task = new Tasks.PC.AutoAttack(this,duetime, delay, Lastp);
                            Character.Tasks.Add("自动攻击线程", task);
                            task.Activate();
                        }
                    }
                    catch (Exception ex)
                    {
                        Logger.ShowError(ex);
                    }
                }
            }
            catch (Exception ex)
            {
                SkillArg arg = new SkillArg();
                arg.sActor = Character.ActorID;
                arg.type = (ATTACK_TYPE)0xff;
                arg.affectedActors.Add(Character);
                arg.Init();
                Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.ATTACK, arg, Character, true);
                Character.AutoAttack = false;
                return;
            }
        }

        
        public void OnSkillChangeBattleStatus(Packets.Client.CSMG_SKILL_CHANGE_BATTLE_STATUS p)
        {
            if (p.Status == 0)
                Character.AutoAttack = false;
            //else
                //Character.AutoAttack = true;

            if (Character.BattleStatus != p.Status)
            {
                Character.BattleStatus = p.Status;
                SendChangeStatus();
            }
            if (Character.Tasks.ContainsKey("RangeAttack") && Character.BattleStatus == 0)
            {
                Character.Tasks["RangeAttack"].Deactivate();
                Character.Tasks.Remove("RangeAttack");
                Character.TInt["RangeAttackMark"] = 0;
            }
            if (Character.Tasks.ContainsKey("SkillCast") && Character.BattleStatus == 0 && (Character.Skills.ContainsKey(14000)) && (Character.Job == PC_JOB.CARDINAL || Character.Job == PC_JOB.ASTRALIST))
            {
                /*if (this.Character.Tasks["SkillCast"].getActivated())
                {
                    this.Character.Tasks["SkillCast"].Deactivate();
                    this.Character.Tasks.Remove("SkillCast");
                }*/
                Character.TInt["移动施法"] = 1;
                //if (Character.MP > 100)
                //    Character.MP -= 100;
                //Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, this.Character, true);

                Packets.Server.SSMG_SKILL_CAST_RESULT p2 = new SagaMap.Packets.Server.SSMG_SKILL_CAST_RESULT();
                p2.ActorID = Character.ActorID;
                p2.Result = 20;
                netIO.SendPacket(p2);
            }
        }

        public void OnSkillCast(Packets.Client.CSMG_SKILL_CAST p)
        {
            OnSkillCast(p, true);
        }

        bool checkSkill(uint skillID, byte skillLV)
        {
            Packets.Server.SSMG_SKILL_LIST p = new SagaMap.Packets.Server.SSMG_SKILL_LIST();
            Dictionary<uint, byte> skills;
            Dictionary<uint, byte> skills2X;
            Dictionary<uint, byte> skills2T;
            List<SagaDB.Skill.Skill> list = new List<SagaDB.Skill.Skill>();
            bool ifDominion = map.Info.Flag.Test(SagaDB.Map.MapFlags.Dominion);
            skills = SkillFactory.Instance.CheckSkillList(Character, SkillFactory.SkillPaga.p1);
            skills2X = SkillFactory.Instance.CheckSkillList(Character, SkillFactory.SkillPaga.p21);
            skills2T = SkillFactory.Instance.CheckSkillList(Character, SkillFactory.SkillPaga.p22);

            if (!ifDominion)
            {
                if (chara.Skills.ContainsKey(skillID))
                {
                    if (chara.Skills[skillID].Level >= skillLV)
                        return true;
                }
                if (chara.Skills2.ContainsKey(skillID))
                {
                    if (chara.Skills2[skillID].Level >= skillLV)
                        return true;
                }
                if (chara.SkillsReserve.ContainsKey(skillID))
                {
                    if (chara.SkillsReserve[skillID].Level >= skillLV)
                        return true;
                }
            }
            else
            {
                if (chara.Skills.ContainsKey(skillID))
                {
                    if (chara.Skills[skillID].Level >= skillLV)
                    {
                        if (skills.ContainsKey(skillID))
                        {
                            if (chara.DominionJobLevel >= skills[skillID])
                                return true;
                        }
                        else
                            return true;
                    }
                }
                if (chara.Job == chara.Job2X)
                {
                    if (chara.Skills2.ContainsKey(skillID))
                    {
                        if (chara.Skills2[skillID].Level >= skillLV)
                        {
                            if (skills2X.ContainsKey(skillID))
                            {
                                if (chara.DominionJobLevel >= skills2X[skillID])
                                    return true;
                            }
                        }
                    }
                }
                if (chara.Job == chara.Job2T)
                {
                    if (chara.Skills2.ContainsKey(skillID))
                    {
                        if (chara.Skills2[skillID].Level >= skillLV)
                        {
                            if (skills2T.ContainsKey(skillID))
                            {
                                if (chara.DominionJobLevel >= skills2T[skillID])
                                    return true;
                            }
                        }
                    }
                }
                if (chara.SkillsReserve.ContainsKey(skillID) && Character.DominionReserveSkill)
                {
                    if (chara.SkillsReserve[skillID].Level >= skillLV)
                        return true;
                }
            }
            if (Character.JobJoint != PC_JOB.NONE)
            {
                {
                    var skill =
                        from c in SkillFactory.Instance.SkillList(Character.JobJoint)
                        where c.Value <= Character.JointJobLevel
                        select c;
                    foreach (KeyValuePair<uint, byte> i in skill)
                    {
                        if (i.Key == skillID && chara.JointJobLevel >= i.Value)
                            return true;
                    }
                }
            }
            return false;
        }

        public void OnSkillCast(Packets.Client.CSMG_SKILL_CAST p, bool useMPSP)
        {
            OnSkillCast(p, useMPSP, false);
        }

        public void OnSkillCast(Packets.Client.CSMG_SKILL_CAST p, bool useMPSP, bool nocheck)
        {
            if (((!checkSkill(p.SkillID, p.SkillLv) && chara.Account.GMLevel < 2) ||
                (p.Random == lastCastRandom && chara.Account.GMLevel < 2)) && !nocheck)
            {
                SendHack();
                if (hackCount > 2)
                    return;
            }

            //断掉自动放技能
            Character.AutoAttack = false;
            if (main != null)
                ClientManager.RemoveThread(main.Name);

            Character.TInt["移动施法"] = 0;
            lastCastRandom = p.Random;
            SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(p.SkillID, p.SkillLv);
            if (Character.Status.Additions.ContainsKey("Meditatioon"))
            {
                Character.Status.Additions["Meditatioon"].AdditionEnd();
                Character.Status.Additions.Remove("Meditatioon");
            }
            if (Character.Status.Additions.ContainsKey("Hiding"))
            {
                Character.Status.Additions["Hiding"].AdditionEnd();
                Character.Status.Additions.Remove("Hiding");
            }
            if (Character.Status.Additions.ContainsKey("fish"))
            {
                Character.Status.Additions["fish"].AdditionEnd();
                Character.Status.Additions.Remove("fish");
            }
            if (Character.Status.Additions.ContainsKey("Cloaking"))
            {
                Character.Status.Additions["Cloaking"].AdditionEnd();
                Character.Status.Additions.Remove("Cloaking");
            }
            if (Character.Status.Additions.ContainsKey("IAmTree"))
            {
                Character.Status.Additions["IAmTree"].AdditionEnd();
                Character.Status.Additions.Remove("IAmTree");
            }
            /*if (this.Character.Status.Additions.ContainsKey("Invisible"))
            {
                this.Character.Status.Additions["Invisible"].AdditionEnd();
                this.Character.Status.Additions.Remove("Invisible");
            }*/
            if (Character.Tasks.ContainsKey("Regeneration"))
            {
                Character.Tasks["Regeneration"].Deactivate();
                Character.Tasks.Remove("Regeneration");
            }
            //SendChangeStatus();
            SkillArg arg = new SkillArg();
            arg.sActor = Character.ActorID;
            arg.dActor = p.ActorID;
            arg.skill = skill;
            arg.x = p.X;
            arg.y = p.Y;
            arg.argType = SkillArg.ArgType.Cast;
            ushort[] cost = caculate3P(Character, arg);
            ushort mp=cost[0], sp = cost[1], ep = cost[2];
            try
            {
                if (DateTime.Now >= skillDelay || (DateTime.Now >= skillDelay && nextCombo.Contains(arg.skill.ID)))
                {
                    if (Character.SP >= sp && Character.MP >= mp && Character.EP >= ep)
                    {
                        arg.result = (short)SkillHandler.Instance.TryCast(Character, Map.GetActor(arg.dActor), arg);
                        //if (Character.MapID == 10054000 && Character.Account.GMLevel < 200 && skill.ID != 3250 && skill.ID != 14040 && skill.ID != 14041) arg.result = -32;
                        if (Character.MapID == 63001000 && Character.Account.GMLevel < 200 && !(skill.ID>=25000 && skill.ID<=26000)) arg.result = -32;//活动地图不能使用其他技能
                        if (Character.TInt["围观群众"] == 1 && Character.Account.GMLevel < 200 && Character.Mode!=PlayerMode.COLISEUM_MODE) arg.result = -32;//活动地图不能使用其他技能
                        if (!SkillHandler.Instance.CheckSkillCanCastForWeapon(chara, arg))
                        {
                            arg.result = -5;
                        }
                        if (Character.Status.Additions.ContainsKey("Silence") && arg.skill.Magical)
                        {
                            arg.result = -7;
                        }
                        if(Character.Buff.Dead)
                        {
                            arg.result = -99;
                            SendSystemMessage("死亡状态无法使用技能");
                        }
                        if (Character.Status.Additions.ContainsKey("居合模式"))
                        {
                            if (arg.skill.ID != 2129)
                                arg.result = -7;
                            else
                            {
                                Character.Status.Additions["居合模式"].AdditionEnd();
                                Character.Status.Additions.Remove("居合模式");
                            }
                        }
                        if (GetPossessionTarget() != null)
                        {
                            if (GetPossessionTarget().Buff.Dead && arg.skill.ID != 3055)
                                arg.result = -27;
                        }
                        /*if (this.scriptThread != null)
                        {
                            arg.result = -59;
                        }*/
                        if (skill.NoPossession)
                        {
                            if (chara.Buff.憑依準備 || chara.PossessionTarget != 0)
                            {
                                arg.result = -25;
                            }
                        }
                        if (skill.NotBeenPossessed)
                        {
                            if (chara.PossesionedActors.Count > 0)
                            {
                                arg.result = -24;
                            }
                        }
                        if (Character.Tasks.ContainsKey("SkillCast"))
                        {
                            if (arg.skill.ID == 3311)
                                arg.result = 0;
                            else
                                arg.result = -8;
                        }
                        if (Character.TInt["GM技能调试模式"] == 1)
                            arg.result = 0;
                        if (arg.result == 0)
                        {
                            arg.delay = (uint)(skill.CastTime * (1f - (Character.Status.cspd) / 1000f));

                            //2012.6.29修正cspd效果为旧效果的80%
                            //这是啥修正啊喂！
                            //arg.delay = (uint)(skill.CastTime * (1f - (this.Character.Status.cspd + this.Character.Status.cspd_skill) * 0.8f / 1000f));

                            short spd = (short)(Character.Status.cspd + Character.Status.cspd_skill);
                            if (skill.BaseData.flag.Test(SkillFlags.PHYSIC))
                                spd = (short)(Character.Status.aspd + Character.Status.aspd_skill);

                            //2016.11.14 YGG内测第一次整顿，修改技能吟唱算法
                            //2017.9.5 速度改版，满速为1000
                            spd -= 500;
                            if (spd <= 1) spd = 1;
                            //9月15日临时修改加速
                            int ct = skill.CastTime;
                            if (!skill.BaseData.flag.Test(SkillFlags.PHYSIC))
                                ct = (int)(ct * 0.7f);


                            arg.delay = (uint)(ct * (1f - (spd / 1200f)));

                            if (arg.skill.ID == 14032)
                            {
                                int smp = (int)(Character.MaxMP - Character.MP);
                                if (smp > 500)
                                    arg.delay = (uint)((smp / 500f) * (1500 - Character.Int * 20));
                                else
                                    arg.delay = 1000;
                            }
                            if (skill.ID == 32100 || skill.ID == 32101)
                                arg.delay = 1000;

                            if (skill.ID >= 25000 && skill.ID <= 26000)
                            {
                                arg.delay = (uint)skill.CastTime;
                                if (Character.Status.Additions.ContainsKey("泼冷水"))
                                    arg.delay = (uint)(arg.delay * 1.2);
                                if (Character.Status.Additions.ContainsKey("焚烬之火"))
                                    arg.delay = 0;
                            }

                            if(Character.Status.Additions.ContainsKey("施法时间减半"))
                                arg.delay = (uint)(arg.delay * 0.5f);

                            /*if(arg.skill.ID == 13102)
                            {
                                float delayreduce = (10000 - Character.EP) / 10000f;
                                arg.delay = (uint)(arg.delay * delayreduce);
                            }*/

                            if (Character.Status.delayCancelList.ContainsKey((ushort)arg.skill.ID))
                            {
                                int rate = Character.Status.delayCancelList[(ushort)arg.skill.ID];
                                arg.delay = (uint)(arg.delay * (1f - ((float)rate / 100)));
                            }
                            if (Character.Status.Additions.ContainsKey("SwordEaseSp"))
                                arg.delay = (uint)(arg.delay * 0.5f);

                            if (Character.Status.Additions.ContainsKey("时间扭曲"))
                                arg.delay = (uint)(arg.delay * 0.75f);

                            if (arg.skill.ID == 14010 && Character.Status.Additions.ContainsKey("彼岸焚烧"))//烈焰焚烧瞬发
                                arg.delay = 0;

                            if (skill.BaseData.flag.Test(SkillFlags.PHYSIC))
                                arg.delay = (uint)skill.CastTime;

                            if(skill.BaseData.id == 12117)//枪决
                                arg.delay = (uint)skill.CastTime;

                            Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, arg, Character, true);
                            if (skill.CastTime > 200 && nextCombo.Contains(arg.skill.ID))
                            {
                                arg.delay = 100;
                                if (arg.skill.ID == 11107)
                                    arg.delay = 100;
                                nextCombo.Clear();
                            }
                            if (Character.Status.Additions.ContainsKey("SwordEaseSp"))
                            {
                                nextCombo.Clear();
                                OnSkillCastComplete(arg);
                            }
                            else if (skill.CastTime > 0)
                            {
                                Tasks.PC.SkillCast task = new Tasks.PC.SkillCast(this, arg);
                                Character.Tasks.Add("SkillCast", task);

                                task.Activate();
                            }
                            else
                            {
                                nextCombo.Clear();
                                OnSkillCastComplete(arg);
                            }
                            if (Character.Status.Additions.ContainsKey("Parry"))
                                arg.delay = (uint)arg.skill.BaseData.delay;

                        }
                        else
                        {
                            Character.e.OnActorSkillUse(Character, arg);
                        }
                    }
                    else
                    {
                        if (Character.SP < sp && Character.MP < mp)
                            arg.result = -1;
                        else if (Character.SP < sp)
                            arg.result = -16;
                        else if (Character.MP < mp)
                            arg.result = -15;
                        else
                            arg.result = -62;
                        Character.e.OnActorSkillUse(Character, arg);
                    }
                }
                else
                {
                    arg.result = -30;
                    Character.e.OnActorSkillUse(Character, arg);
                }
            }
            catch (Exception ex)
            {
                //强行返回-100错误代码，防止卡技能
                arg.result = -100;
                Character.e.OnActorSkillUse(Character, arg);
                Logger.ShowError(ex);
            }
        }

        ushort checkirismpcost(ushort mpcost)
        {
            if (Character.Status.mpcost_70down_iris > 0 && Character.MP > Character.MaxMP * 0.7)//魔力洪流
                mpcost -= (ushort)(mpcost * 0.02 * Character.Status.mpcost_70down_iris);
            if (Character.Status.mpnocost_iris > 0)
            {
                if (Character.Status.mpnocost_iris * 1 >= Global.Random.Next(1, 100))
                    mpcost = 0;
            }
            if (Character.PlayerTitleID == 16 && !Character.Status.Additions.ContainsKey("糖果称号CD"))
            {
                if (Global.Random.Next(1, 100) < 8)
                {
                    uint mpheal = (uint)(Character.MaxMP * 0.2f);
                    Character.MP += mpheal;
                    if (Character.MP > Character.MaxMP)
                        Character.MP = Character.MaxMP;
                    OtherAddition cd = new OtherAddition(null, Character, "糖果称号CD", 10000);
                    SkillHandler.ApplyAddition(Character, cd);
                    SkillHandler.Instance.ShowVessel(Character, 0, (int)-mpheal, 0, SkillHandler.AttackResult.Hit);
                }
            }
            return mpcost;
        }
        ushort checkirisspcost(ushort spcost)
        {
            if (Character.Status.spcost_70down_iris > 0 && Character.SP > Character.MaxSP * 0.7)//洪荒之力
                spcost -= (ushort)(spcost * 0.02 * Character.Status.spcost_70down_iris);
            if (Character.Status.spnocost_iris > 0)
            {
                if (Character.Status.spnocost_iris * 1 >= Global.Random.Next(1, 100))
                    spcost = 0;
            }
            return spcost;
        }
        ushort checkirisepcost(ushort epcost)
        {
            return epcost;
        }

        ushort[] caculate3P(Actor sActor, SkillArg arg)
        {
            ushort mp, sp, ep;

            //凭依时消耗加倍
            if (Character.PossessionTarget != 0)
            {
                mp = (ushort)(arg.skill.MP * 2);
                sp = (ushort)(arg.skill.SP * 2);
                ep = (ushort)(arg.skill.AP * 2);
            }
            else
            {
                mp = arg.skill.MP;
                sp = arg.skill.SP;
                ep = arg.skill.AP;
            }
            if (Character.Status.Additions.ContainsKey("SwordEaseSp"))
            {
                sp *= 2;
                mp *= 2;
                ep *= 2;
            }
            if (Character.Status.Additions.ContainsKey("元素解放"))
            {
                sp *= 2;
                mp *= 2;
                ep *= 2;
            }
            if (Character.Status.zenList.Contains((ushort)arg.skill.ID) || Character.Status.darkZenList.Contains((ushort)arg.skill.ID))
                mp *= 2;

            if (Character.Status.Additions.ContainsKey("EnergyExcess"))//能量增幅耗蓝加深
            {
                float[] rate = { 0, 0.05f, 0.16f, 0.28f, 0.4f, 0.65f };
                mp += (ushort)(mp * rate[Character.TInt["EnergyExcess"]]);
            }

            if (Character.Status.Additions.ContainsKey("神圣祷告") || Character.Status.Additions.ContainsKey("湮灭之心"))
                mp /= 2;
            if (Character.Status.Additions.ContainsKey("狙击模式") && arg.skill.ID == 2000)
                ep /= 2;
            if (Character.Status.Additions.ContainsKey("圣母咏叹连续") && arg.skill.ID == 11108)
                ep /= 2;
            if (Character.Status.Additions.ContainsKey("流星坠") && arg.skill.ID == 12115)
                sp = 0;
            if (Character.Status.Additions.ContainsKey("审判") && arg.skill.ID == 12108)
                ep = 0;

            mp = checkirismpcost(mp);
            sp = checkirisspcost(sp);
            ep = checkirisepcost(ep);

            if (Character.TInt["GM技能调试模式"] == 1)
            {
                arg.useMPSP = false;
                sp = 0;
                mp = 0;
                ep = 0;
            }
            SendActorHPMPSP(Character);
            return new ushort[3] { mp, sp, ep };
        }


        public void OnSkillCastComplete(SkillArg skill)
        {
            if (Character.Status.Additions.ContainsKey("Meditatioon"))
            {
                Character.Status.Additions["Meditatioon"].AdditionEnd();
                Character.Status.Additions.Remove("Meditatioon");
            }
            if (Character.Status.Additions.ContainsKey("Hiding"))
            {
                Character.Status.Additions["Hiding"].AdditionEnd();
                Character.Status.Additions.Remove("Hiding");
            }
            if (Character.Status.Additions.ContainsKey("fish"))
            {
                Character.Status.Additions["fish"].AdditionEnd();
                Character.Status.Additions.Remove("fish");
            }
            if (Character.Status.Additions.ContainsKey("Cloaking"))
            {
                Character.Status.Additions["Cloaking"].AdditionEnd();
                Character.Status.Additions.Remove("Cloaking");
            }
            if (Character.Status.Additions.ContainsKey("IAmTree"))
            {
                Character.Status.Additions["IAmTree"].AdditionEnd();
                Character.Status.Additions.Remove("IAmTree");
            }
            //SendChangeStatus();

            skill.argType = SkillArg.ArgType.Active;
            PartnerTalking(Character.Partner, TALK_EVENT.BATTLE, 1, 20000);
            ushort[] cost = caculate3P(Character, skill);
            ushort mp = cost[0], sp = cost[1], ep = cost[2];
            if (skill.useMPSP)
            {
                if (Character.Status.Additions.ContainsKey("闪光一刺") && Character.TInt["闪光一刺提升%"] > 0 && Global.Random.Next(0, 100) < 10)
                {
                    sp = 0;
                    ep /= 2;
                }
                if (Character.Status.Playman > 0 && Character.Job == PC_JOB.HAWKEYE && Global.Random.Next(0, 100) < Character.Status.Playman)//戏命师
                    mp = 0;

                if (Character.Status.Additions.ContainsKey("韵律精通") && Character.Job == PC_JOB.CARDINAL)
                {
                    Character.SP += (uint)Character.TInt["韵律精通Recover"];
                    if (Character.SP > Character.MaxSP)
                        Character.SP = Character.MaxSP;
                }

                if (Character.Status.Additions.ContainsKey("活力之幻想曲Buff"))
                {
                    if (Global.Random.Next(0, 100) < 20)
                    {
                        if (mp > 0)
                        {
                            int mpheal = (int)(mp * (100 - Character.TInt["活力之幻想曲Value"]) / 100f);
                            Character.MP += (uint)mpheal;
                            if (Character.MP > Character.MaxMP)
                                Character.MP = Character.MaxMP;
                            SkillHandler.Instance.ShowVessel(Character, 0, -mpheal);
                        }
                        if (sp > 0)
                        {
                            int spheal = (int)(sp * (100 - Character.TInt["活力之幻想曲Value"]) / 100f);
                            Character.SP += (uint)spheal;
                            if (Character.SP > Character.MaxSP)
                                Character.SP = Character.MaxSP;
                            SkillHandler.Instance.ShowVessel(Character, 0, -spheal);
                        }
                    }
                }
                if (Character.MP > mp)
                    Character.MP -= mp;
                else
                    Character.MP = 0;

                if (Character.SP > sp)
                    Character.SP -= sp;
                else
                    Character.SP = 0;

                if (Character.EP > ep)
                    Character.EP -= ep;
                else
                    Character.EP = 0;

                if (Character.Job == PC_JOB.ASTRALIST && mp > 0)//魔法师
                {
                    uint epup = mp;
                    if (Character.TInt["火属性魔法释放"] == 2)
                    {
                        Character.TInt["火属性魔法释放"] = 1;
                        Character.TInt["水属性魔法释放"] = 0;
                        epup -= (uint)(epup * (Character.TInt["元素协调值"] / 100f));
                    }

                    if (Character.TInt["水属性魔法释放"] == 2)
                    {
                        Character.TInt["水属性魔法释放"] = 1;
                        Character.TInt["火属性魔法释放"] = 0;
                        epup -= (uint)(epup * (Character.TInt["元素协调值"] / 100f));
                    }
                    Character.EP += epup;
                }
            }
            SendActorHPMPSP(Character);
            if (skill.dActor != 0xFFFFFFFF)
            {
                Actor dActor = Map.GetActor(skill.dActor);
                if (dActor != null)
                    SkillHandler.Instance.SkillCast(Character, dActor, skill);
                else
                {
                    skill.result = -11;
                    Character.e.OnActorSkillUse(Character, skill);
                }
            }
            else
            {
                skill.argType = SkillArg.ArgType.Active;
                SkillHandler.Instance.SkillCast(Character, Character, skill);
            }


            if (Character.Pet != null)
            {
                if (Character.Pet.Ride)
                {
                    SkillHandler.Instance.ProcessPetGrowth(Character.Pet, PetGrowthReason.UseSkill);
                }
            }

            //技能延迟
            if (Character.Status.Additions.ContainsKey("SwordEaseSp"))
            {
                skillDelay = DateTime.Now + new TimeSpan(0, 0, 0, 0, (int)(skill.skill.Delay * 0.2f));
            }
            else if (Character.Status.delayCancelList.ContainsKey((ushort)skill.skill.ID))
            {
                skillDelay = DateTime.Now + new TimeSpan(0, 0, 0, 0, (int)(skill.skill.Delay * (1f - ((float)Character.Status.delayCancelList[(ushort)skill.skill.ID] / 100))));
            }
            else
                skillDelay = DateTime.Now + new TimeSpan(0, 0, 0, 0, skill.skill.Delay);

            if (skill.affectedActors.Count<32)
                Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, skill, Character, true);
            else
                for (int i = 0; i < skill.affectedActors.Count; i += 32)
                    Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, skill.SplitPart(i, 32), Character, true);

            if (skill.skill.Effect != 0 && skill.showEffect)
            {
                EffectArg eff = new EffectArg()
                {
                    actorID = skill.dActor,
                    effectID = skill.skill.Effect,
                    x = skill.x,
                    y = skill.y
                };
                Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, eff, Character, true);
            }

            /*foreach (uint i in skill.autoCast.Keys)
            {
                skillDelay = DateTime.Now;
                Packets.Client.CSMG_SKILL_CAST p1 = new SagaMap.Packets.Client.CSMG_SKILL_CAST();
                p1.ActorID = skill.dActor;
                p1.SkillID = (ushort)i;
                p1.SkillLv = skill.autoCast[i];
                p1.X = skill.x;
                p1.Y = skill.y;

                this.OnSkillCast(p1);
            }*/
            if (Character.Tasks.ContainsKey("AutoCast"))
                Character.Tasks["AutoCast"].Activate();
            else
            {
                if (skill.autoCast.Count != 0)
                {
                    Character.Buff.CannotMove = true;
                    Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, Character, true);
                    Tasks.Skill.AutoCast task = new SagaMap.Tasks.Skill.AutoCast(Character, skill);
                    Character.Tasks.Add("AutoCast", task);
                    task.Activate();
                }
            }
            if (Character.Status.Additions.ContainsKey("Invisible"))
            {
                if (Character.Status.Additions["Invisible"].TotalLifeTime - Character.Status.Additions["Invisible"].RestLifeTime > 100)
                {
                    Character.Status.Additions["Invisible"].AdditionEnd();
                    Character.Status.Additions.Remove("Invisible");
                }
            }
        }

        public void SendChangeStatus()
        {
            if (Character.Tasks.ContainsKey("Regeneration"))
            {
                Character.Tasks["Regeneration"].Deactivate();
                Character.Tasks.Remove("Regeneration");
            }
            if (Character.Buff.Sit)
            {
                Character.Buff.Sit = false;
                Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, Character, true);
                Character.Motion = MotionType.NONE;
                Character.MotionLoop = false;
            }
            if (Character.Motion != MotionType.NONE && Character.Motion != MotionType.DEAD)
            {
                Character.Motion = MotionType.NONE;
                Character.MotionLoop = false;
            }
            Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_STATUS, null, Character, true);
        }

        public void SendRevive(byte level)
        {
            Character.Buff.Dead = false;
            Character.Buff.紫になる = false;
            Character.Motion = MotionType.STAND;
            Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, Character, true);

            float factor = 0;
            switch (level)
            {
                case 1:
                    factor = 0.1f;
                    break;
                case 2:
                    factor = 0.2f;
                    break;
                case 3:
                    factor = 0.45f;
                    break;
                case 4:
                    factor = 0.5f;
                    break;
                case 5:
                    factor = 0.75f;
                    break;
                case 6:
                    factor = 1f;
                    break;
            }

            Character.HP = (uint)(Character.MaxHP * factor);
            Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, Character, true);
            SkillArg arg = new SkillArg();
            arg.sActor = Character.ActorID;
            arg.dActor = 0;
            arg.skill = SkillFactory.Instance.GetSkill(10002, level);
            arg.x = 0;
            arg.y = 0;
            arg.hp = new List<int>();
            arg.sp = new List<int>();
            arg.mp = new List<int>();
            arg.hp.Add((int)(-Character.MaxHP * factor));
            arg.sp.Add(0);
            arg.mp.Add(0);
            arg.flag.Add(AttackFlag.HP_HEAL);
            arg.affectedActors.Add(Character);
            arg.argType = SkillArg.ArgType.Active;
            Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, arg, Character, true);

            if (!Character.Tasks.ContainsKey("AutoSave"))
            {
                Tasks.PC.AutoSave task = new SagaMap.Tasks.PC.AutoSave(Character);
                Character.Tasks.Add("AutoSave", task);
                task.Activate();
            }
            if (!Character.Tasks.ContainsKey("Recover"))//自然恢复
            {
                Tasks.PC.Recover reg = new Tasks.PC.Recover(FromActorPC(Character));
                Character.Tasks.Add("Recover", reg);
                reg.Activate();
            }

            SkillHandler.Instance.CastPassiveSkills(Character);
            SendPlayerInfo();
        }

        public void SendSkillList()
        {
            Packets.Server.SSMG_SKILL_LIST p = new SagaMap.Packets.Server.SSMG_SKILL_LIST();
            Dictionary<uint, byte> skills;
            Dictionary<uint, byte> skills2X;
            Dictionary<uint, byte> skills2T;
            Dictionary<uint, byte> skills3;
            List<SagaDB.Skill.Skill> list = new List<SagaDB.Skill.Skill>();
            bool ifDominion = map.Info.Flag.Test(SagaDB.Map.MapFlags.Dominion);
            if (ifDominion)
            {
                skills = new Dictionary<uint, byte>();
                skills2X = new Dictionary<uint, byte>();
                skills2T = new Dictionary<uint, byte>();
                skills3 = new Dictionary<uint, byte>();
            }
            else
            {
                skills = SkillFactory.Instance.CheckSkillList(Character, SkillFactory.SkillPaga.p1);
                skills2X = SkillFactory.Instance.CheckSkillList(Character, SkillFactory.SkillPaga.p21);
                skills2T = SkillFactory.Instance.CheckSkillList(Character, SkillFactory.SkillPaga.p22);
                skills3 = SkillFactory.Instance.CheckSkillList(Character, SkillFactory.SkillPaga.p3);
            }
            {
                var skill =
                    from c in skills.Keys
                    where !Character.Skills.ContainsKey(c)
                    select c;
                foreach (uint i in skill)
                {
                    SagaDB.Skill.Skill sk = SkillFactory.Instance.GetSkill(i, 0);
                    list.Add(sk);
                }
                foreach (SagaDB.Skill.Skill i in Character.Skills.Values)
                {
                    list.Add(i);
                }
                foreach (SagaDB.Skill.Skill i in Character.SkillsEquip.Values)
                {
                    list.Add(i);
                }

            }
            p.Skills(list, 0, Character.JobBasic, ifDominion, Character);
            netIO.SendPacket(p);
            if (Character.Rebirth || Character.Job == Character.Job3)
            {
                {
                    list.Clear();
                    {
                        var skill =
                            from c in skills2X.Keys
                            where !Character.Skills2_1.ContainsKey(c)
                            select c;
                        foreach (uint i in skill)
                        {
                            SagaDB.Skill.Skill sk = SkillFactory.Instance.GetSkill(i, 0);
                            list.Add(sk);
                        }
                        foreach (SagaDB.Skill.Skill i in Character.Skills2_1.Values)
                        {
                            list.Add(i);
                        }
                    }

                    p.Skills(list, 1, Character.Job2X, ifDominion, Character);
                    netIO.SendPacket(p);
                }
                {
                    list.Clear();
                    {
                        var skill =
                            from c in skills2T.Keys
                            where !Character.Skills2_2.ContainsKey(c)
                            select c;
                        foreach (uint i in skill)
                        {
                            SagaDB.Skill.Skill sk = SkillFactory.Instance.GetSkill(i, 0);
                            list.Add(sk);
                        }
                        foreach (SagaDB.Skill.Skill i in Character.Skills2_2.Values)
                        {
                            list.Add(i);
                        }
                    }
                    p.Skills(list, 2, Character.Job2T, ifDominion, Character);
                    netIO.SendPacket(p);
                }
                {
                    list.Clear();
                    {
                        var skill =
                            from c in skills3.Keys
                            where !Character.Skills3.ContainsKey(c)
                            select c;
                        foreach (uint i in skill)
                        {
                            SagaDB.Skill.Skill sk = SkillFactory.Instance.GetSkill(i, 0);
                            list.Add(sk);
                        }
                        foreach (SagaDB.Skill.Skill i in Character.Skills3.Values)
                        {
                            list.Add(i);
                        }
                    }

                    p.Skills(list, 3, Character.Job3, ifDominion, Character);
                    netIO.SendPacket(p);
                }

            }
            else
            {
                if (Character.Job == Character.Job2X)
                {
                    list.Clear();
                    {
                        var skill =
                            from c in skills2X.Keys
                            where !Character.Skills2.ContainsKey(c)
                            select c;
                        foreach (uint i in skill)
                        {
                            SagaDB.Skill.Skill sk = SkillFactory.Instance.GetSkill(i, 0);
                            list.Add(sk);
                        }
                        foreach (SagaDB.Skill.Skill i in Character.Skills2.Values)
                        {
                            list.Add(i);
                        }
                    }

                    p.Skills(list, 1, Character.Job2X, ifDominion, Character);
                    netIO.SendPacket(p);
                }
                if (Character.Job == Character.Job2T)
                {
                    list.Clear();
                    {
                        var skill =
                            from c in skills2T.Keys
                            where !Character.Skills2.ContainsKey(c)
                            select c;
                        foreach (uint i in skill)
                        {
                            SagaDB.Skill.Skill sk = SkillFactory.Instance.GetSkill(i, 0);
                            list.Add(sk);
                        }
                        foreach (SagaDB.Skill.Skill i in Character.Skills2.Values)
                        {
                            list.Add(i);
                        }
                    }
                    p.Skills(list, 2, Character.Job2T, ifDominion, Character);
                    netIO.SendPacket(p);
                }
                if (map.Info.Flag.Test(SagaDB.Map.MapFlags.Dominion))
                {
                    if (Character.DominionReserveSkill)
                    {
                        Packets.Server.SSMG_SKILL_RESERVE_LIST p2 = new SagaMap.Packets.Server.SSMG_SKILL_RESERVE_LIST();
                        p2.Skills = Character.SkillsReserve.Values.ToList();
                        netIO.SendPacket(p2);
                    }
                    else
                    {
                        Packets.Server.SSMG_SKILL_RESERVE_LIST p2 = new SagaMap.Packets.Server.SSMG_SKILL_RESERVE_LIST();
                        p2.Skills = new List<SagaDB.Skill.Skill>();
                        netIO.SendPacket(p2);
                    }
                }
                else
                {
                    Packets.Server.SSMG_SKILL_RESERVE_LIST p2 = new SagaMap.Packets.Server.SSMG_SKILL_RESERVE_LIST();
                    p2.Skills = Character.SkillsReserve.Values.ToList();
                    netIO.SendPacket(p2);
                }
            }


            if (Character.JobJoint != PC_JOB.NONE)
            {
                list.Clear();
                {
                    var skill =
                        from c in SkillFactory.Instance.SkillList(Character.JobJoint)
                        where c.Value <= Character.JointJobLevel
                        select c;
                    foreach (KeyValuePair<uint, byte> i in skill)
                    {
                        SagaDB.Skill.Skill sk = SkillFactory.Instance.GetSkill(i.Key, 1);
                        list.Add(sk);
                    }
                }
                Packets.Server.SSMG_SKILL_JOINT_LIST p2 = new SagaMap.Packets.Server.SSMG_SKILL_JOINT_LIST();
                p2.Skills = list;
                netIO.SendPacket(p2);
            }
            else
            {
                Packets.Server.SSMG_SKILL_JOINT_LIST p2 = new SagaMap.Packets.Server.SSMG_SKILL_JOINT_LIST();
                p2.Skills = new List<SagaDB.Skill.Skill>();
                netIO.SendPacket(p2);
            }
        }

        public void SendSkillDummy()
        {
            SendSkillDummy(3311, 1);
        }

        public void SendSkillDummy(uint skillid, byte level)
        {
            if (Character.Tasks.ContainsKey("SkillCast"))
            {
                if (Character.Tasks["SkillCast"].getActivated())
                {
                    Character.Tasks["SkillCast"].Deactivate();
                    Character.Tasks.Remove("SkillCast");
                }

                SkillArg arg = new SkillArg();
                arg.sActor = Character.ActorID;
                arg.dActor = 0;
                arg.skill = SkillFactory.Instance.GetSkill(skillid, level);
                arg.x = 0;
                arg.y = 0;
                arg.hp = new List<int>();
                arg.sp = new List<int>();
                arg.mp = new List<int>();
                arg.hp.Add(0);
                arg.sp.Add(0);
                arg.mp.Add(0);
                arg.flag.Add(AttackFlag.NONE);
                //arg.affectedActors.Add(this.Character);
                arg.argType = SkillArg.ArgType.Active;
                Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, arg, Character, true);
            }
        }
        public void OnSkillRangeAttack(Packets.Client.CSMG_SKILL_RANGE_ATTACK p)
        {
            Packets.Server.SSMG_SKILL_RANGEA_RESULT p2 = new Packets.Server.SSMG_SKILL_RANGEA_RESULT();
            p2.ActorID = p.ActorID;
            if (!Character.Status.Additions.ContainsKey("自由射击"))
                p2.Speed = 500;
            else
                p2.Speed = 0;
            netIO.SendPacket(p2);
            Character.TTime["远程蓄力"] = DateTime.Now;

            if (Character.Tasks.ContainsKey("RangeAttack"))
            {
                Character.Tasks["RangeAttack"].Deactivate();
                Character.Tasks.Remove("RangeAttack");
            }
            Tasks.PC.RangeAttack ra = new Tasks.PC.RangeAttack(this);
            Character.Tasks.Add("RangeAttack", ra);
            ra.Activate();
        }
        /// <summary>
        /// 重置技能
        /// </summary>
        /// <param name="job">1为1转，2为2转</param>
        public void ResetSkill(byte job)
        {
            /*要重写！因为写了技能树！
             * int totalPoints = 0;
            List<uint> delList = new List<uint>();
            switch (job)
            {
                case 1:
                    foreach (SagaDB.Skill.Skill i in this.Character.Skills.Values)
                    {
                        if (SkillFactory.Instance.SkillList(this.Character.JobBasic).ContainsKey(i.ID))
                        {
                            totalPoints += (i.Level + 2);
                            delList.Add(i.ID);
                        }
                    }
                    this.Character.SkillPoint += (ushort)totalPoints;
                    foreach (uint i in delList)
                    {
                        this.Character.Skills.Remove(i);
                    }
                    break;
                case 2:
                    if (!this.Character.Rebirth)
                    {
                        foreach (SagaDB.Skill.Skill i in this.Character.Skills2.Values)
                        {
                            if (SkillFactory.Instance.SkillList(this.Character.Job).ContainsKey(i.ID))
                            {
                                totalPoints += (i.Level + 2);
                                delList.Add(i.ID);
                            }
                        }
                        foreach (uint i in delList)
                        {
                            this.Character.Skills2.Remove(i);
                        }
                        if (this.Character.Job == this.Character.Job2X)
                            this.Character.SkillPoint2X += (ushort)totalPoints;
                        if (this.Character.Job == this.Character.Job2T)
                            this.Character.SkillPoint2T += (ushort)totalPoints;
                    }
                    else
                    {
                        foreach (SagaDB.Skill.Skill i in this.Character.Skills2_1.Values)
                        {
                            if (SkillFactory.Instance.SkillList(this.Character.Job2X).ContainsKey(i.ID))
                            {
                                totalPoints += (i.Level + 2);
                                delList.Add(i.ID);
                            }
                        }
                        foreach (uint i in delList)
                        {
                            this.Character.Skills2_1.Remove(i);
                            this.Character.Skills2.Remove(i);
                        }
                        this.Character.SkillPoint2X += (ushort)totalPoints;

                        totalPoints = 0;
                        delList.Clear();
                        foreach (SagaDB.Skill.Skill i in this.Character.Skills2_2.Values)
                        {
                            if (SkillFactory.Instance.SkillList(this.Character.Job2T).ContainsKey(i.ID))
                            {
                                totalPoints += (i.Level + 2);
                                delList.Add(i.ID);
                            }
                        }
                        foreach (uint i in delList)
                        {
                            this.Character.Skills2_2.Remove(i);
                            this.Character.Skills2.Remove(i);
                        }
                        this.Character.SkillPoint2T += (ushort)totalPoints;
 
                    }
                    break;
                case 3:
                    foreach (SagaDB.Skill.Skill i in this.Character.Skills3.Values)
                    {
                        if (SkillFactory.Instance.SkillList(this.Character.Job3).ContainsKey(i.ID))
                        {
                            totalPoints += (i.Level + 2);
                            delList.Add(i.ID);
                        }
                    }
                    this.Character.SkillPoint3 += (ushort)totalPoints;
                    foreach (uint i in delList)
                    {
                        this.Character.Skills3.Remove(i);
                    }
                    break;
            }
            SkillHandler.Instance.CastPassiveSkills(this.Character);*/
        }

        public void OnFishBaitsEquip(Packets.Client.CSMG_FF_FISHBAIT_EQUIP p)
        {
            if (p.InventorySlot == 0)
            {
                Packets.Server.SSMG_FISHBAIT_EQUIP_RESULT p2 = new Packets.Server.SSMG_FISHBAIT_EQUIP_RESULT();
                p2.InventoryID = 0;
                p2.IsEquip = 1;
                netIO.SendPacket(p2);
            }
            else
            {
                SagaDB.Item.Item item = Character.Inventory.GetItem(p.InventorySlot);
                if (item.ItemID >= 10104900 || item.ItemID <= 10104906)
                {
                    Packets.Server.SSMG_FISHBAIT_EQUIP_RESULT p2 = new Packets.Server.SSMG_FISHBAIT_EQUIP_RESULT();
                    p2.InventoryID = p.InventorySlot;
                    p2.IsEquip = 0;
                    netIO.SendPacket(p2);
                }
            }
        }
    }
}
