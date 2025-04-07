﻿using System;
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
#pragma warning disable CS0169 // 从不使用字段“MapClient.lastAttackRandom”
        short lastAttackRandom;
#pragma warning restore CS0169 // 从不使用字段“MapClient.lastAttackRandom”
        short lastCastRandom;
        public List<uint> nextCombo = new List<uint>();
        //技能独立cd列表
        Dictionary<uint, DateTime> SingleCDLst = new Dictionary<uint, DateTime>();
        public DateTime SkillDelay { set { skillDelay = value; } }
        public void OnSkillLvUP(Packets.Client.CSMG_SKILL_LEVEL_UP p)
        {
            Packets.Server.SSMG_SKILL_LEVEL_UP p1 = new SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP();
            ushort skillID = p.SkillID;
            byte type = 0;
            if (SkillFactory.Instance.SkillList(this.Character.JobBasic).ContainsKey(skillID))
                type = 1;
            else if (SkillFactory.Instance.SkillList(this.Character.Job2X).ContainsKey(skillID))
                type = 2;
            else if (SkillFactory.Instance.SkillList(this.Character.Job2T).ContainsKey(skillID))
                type = 3;
            else if (SkillFactory.Instance.SkillList(this.Character.Job3).ContainsKey(skillID))
                type = 4;
            if (type == 0)
            {
                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP.LearnResult.SKILL_NOT_EXIST;
            }
            else
            {
                if (type == 1)
                {
                    if (!this.Character.Skills.ContainsKey((uint)skillID))
                    {
                        p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP.LearnResult.SKILL_NOT_LEARNED;
                    }
                    else
                    {
                        SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(skillID, this.Character.Skills[skillID].Level);
                        if (this.Character.JobLevel1 < skill.JobLv)
                        {
                            this.SendSystemMessage(string.Format("{1} 未达到技能升级等级！需求等级：{0}", skill.JobLv, skill.Name));
                            return;
                        }
                        if (this.Character.SkillPoint < 1)
                        {
                            p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP.LearnResult.NOT_ENOUGH_SKILL_POINT;
                        }
                        else
                        {
                            if (skill.MaxLevel == 6 && this.Character.Skills[skillID].Level == 5)
                            {
                                this.SendSystemMessage((string.Format("无法直接领悟这项技能的精髓。")));
                                return;
                            }
                            if (this.Character.Skills[skillID].Level == this.Character.Skills[skillID].MaxLevel)
                            {
                                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP.LearnResult.SKILL_MAX_LEVEL_EXEED;
                            }
                            else
                            {
                                this.Character.SkillPoint -= 1;
                                this.Character.Skills[skillID] = SkillFactory.Instance.GetSkill(skillID, (byte)(this.Character.Skills[skillID].Level + 1));
                                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP.LearnResult.OK;
                                p1.SkillID = skillID;
                            }
                        }
                    }
                }
                if (type == 2)
                {
                    if (!this.Character.Skills2.ContainsKey((uint)skillID))
                    {
                        p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP.LearnResult.SKILL_NOT_LEARNED;
                    }
                    else
                    {
                        SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(skillID, this.Character.Skills2[skillID].Level);
                        if (this.Character.JobLevel2X < skill.JobLv)
                        {
                            this.SendSystemMessage(string.Format("{1} 未达到技能升级等级！需求等级：{0}", skill.JobLv, skill.Name));
                            return;
                        }
                        if (this.Character.SkillPoint2X < 1)
                        {
                            p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP.LearnResult.NOT_ENOUGH_SKILL_POINT;
                        }
                        else
                        {
                            if (skill.MaxLevel == 6 && this.Character.Skills[skillID].Level == 5)
                            {
                                this.SendSystemMessage((string.Format("无法直接领悟这项技能的精髓。")));
                                return;
                            }
                            if (this.Character.Skills2[skillID].Level == this.Character.Skills2[skillID].MaxLevel)
                            {
                                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP.LearnResult.SKILL_MAX_LEVEL_EXEED;
                            }
                            else
                            {
                                this.Character.SkillPoint2X -= 1;
                                SagaDB.Skill.Skill data = SkillFactory.Instance.GetSkill(skillID, (byte)(this.Character.Skills2[skillID].Level + 1));
                                this.Character.Skills2[skillID] = data;
                                this.Character.Skills2_1[skillID] = data;
                                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP.LearnResult.OK;
                                p1.SkillID = skillID;
                            }
                        }
                    }
                }
                if (type == 3)
                {
                    if (!this.Character.Skills2.ContainsKey((uint)skillID))
                    {
                        p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP.LearnResult.SKILL_NOT_LEARNED;
                    }
                    else
                    {
                        SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(skillID, this.Character.Skills2[skillID].Level);
                        if (this.Character.JobLevel2T < skill.JobLv)
                        {
                            this.SendSystemMessage(string.Format("{1} 未达到技能升级等级！需求等级：{0}", skill.JobLv, skill.Name));
                            return;
                        }
                        if (this.Character.SkillPoint2T < 1)
                        {
                            p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP.LearnResult.NOT_ENOUGH_SKILL_POINT;
                        }
                        else
                        {
                            if (skill.MaxLevel == 6 && this.Character.Skills[skillID].Level == 5)
                            {
                                this.SendSystemMessage((string.Format("无法直接领悟这项技能的精髓。")));
                                return;
                            }
                            if (this.Character.Skills2[skillID].Level == this.Character.Skills2[skillID].MaxLevel)
                            {
                                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP.LearnResult.SKILL_MAX_LEVEL_EXEED;
                            }
                            else
                            {
                                this.Character.SkillPoint2T -= 1;
                                SagaDB.Skill.Skill data = SkillFactory.Instance.GetSkill(skillID, (byte)(this.Character.Skills2[skillID].Level + 1));
                                this.Character.Skills2[skillID] = data;
                                this.Character.Skills2_2[skillID] = data;
                                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP.LearnResult.OK;
                                p1.SkillID = skillID;
                            }
                        }
                    }
                }
                if (type == 4)
                {
                    if (!this.Character.Skills3.ContainsKey((uint)skillID))
                    {
                        p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP.LearnResult.SKILL_NOT_LEARNED;
                    }
                    else
                    {
                        SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(skillID, this.Character.Skills3[skillID].Level);
                        if (this.Character.JobLevel3 < skill.JobLv)
                        {
                            this.SendSystemMessage(string.Format("{1} 未达到技能升级等级！需求等级：{0}", skill.JobLv, skill.Name));
                            return;
                        }
                        if (this.Character.SkillPoint3 < 1)
                        {
                            p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP.LearnResult.NOT_ENOUGH_SKILL_POINT;
                        }
                        else
                        {
                            if (skill.MaxLevel == 6 && this.Character.Skills[skillID].Level == 5)
                            {
                                this.SendSystemMessage((string.Format("无法直接领悟这项技能的精髓。")));
                                return;
                            }
                            if (this.Character.Skills3[skillID].Level == this.Character.Skills3[skillID].MaxLevel)
                            {
                                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP.LearnResult.SKILL_MAX_LEVEL_EXEED;
                            }
                            else
                            {
                                this.Character.SkillPoint3 -= 1;
                                this.Character.Skills3[skillID] = SkillFactory.Instance.GetSkill(skillID, (byte)(this.Character.Skills3[skillID].Level + 1));
                                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEVEL_UP.LearnResult.OK;
                                p1.SkillID = skillID;
                            }
                        }
                    }
                }
            }

            p1.SkillPoints = this.Character.SkillPoint;
            if (this.Character.Job == this.Character.Job2X)
            {
                p1.SkillPoints2 = this.Character.SkillPoint2X;
                p1.Job = 1;
            }
            else if (this.Character.Job == this.Character.Job2T)
            {
                p1.SkillPoints2 = this.Character.SkillPoint2T;
                p1.Job = 2;
            }
            else if (this.Character.Job == this.Character.Job3)
            {
                p1.SkillPoints2 = this.Character.SkillPoint3;
                p1.Job = 3;
            }
            else
            {
                p1.Job = 0;
            }
            this.netIO.SendPacket(p1);
            SendSkillList();

            SkillHandler.Instance.CastPassiveSkills(this.Character);
            SendPlayerInfo();

            MapServer.charDB.SaveChar(this.Character, true);
        }

        public void OnSkillLearn(Packets.Client.CSMG_SKILL_LEARN p)
        {
            Packets.Server.SSMG_SKILL_LEARN p1 = new SagaMap.Packets.Server.SSMG_SKILL_LEARN();
            ushort skillID = p.SkillID;
            byte type = 0;
            if (SkillFactory.Instance.SkillList(this.Character.JobBasic).ContainsKey(skillID))
                type = 1;
            else if (SkillFactory.Instance.SkillList(this.Character.Job2X).ContainsKey(skillID))
                type = 2;
            else if (SkillFactory.Instance.SkillList(this.Character.Job2T).ContainsKey(skillID))
                type = 3;
            else if (SkillFactory.Instance.SkillList(this.Character.Job3).ContainsKey(skillID))
                type = 4;
            if (type == 0)
            {
                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEARN.LearnResult.SKILL_NOT_EXIST;
            }
            else
            {
                if (type == 1)
                {
                    byte jobLV = SkillFactory.Instance.SkillList(this.Character.JobBasic)[skillID];
                    if (this.Character.Skills.ContainsKey((uint)skillID))
                    {
                        p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEARN.LearnResult.SKILL_NOT_LEARNED;
                    }
                    else
                    {
                        if (this.Character.SkillPoint < 3)
                        {
                            p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEARN.LearnResult.NOT_ENOUGH_SKILL_POINT;
                        }
                        else
                        {
                            SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(skillID, 1);
                            if (skill.JobLv != 0)
                                jobLV = skill.JobLv;

                            if (this.Character.JobLevel1 < jobLV)
                            {
                                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEARN.LearnResult.NOT_ENOUGH_JOB_LEVEL;
                            }

                            else
                            {
                                this.Character.SkillPoint -= 3;
                                this.Character.Skills.Add(skillID, skill);
                                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEARN.LearnResult.OK;
                                p1.SkillID = skillID;
                                p1.SkillPoints = this.Character.SkillPoint;
                                if (this.Character.Job == this.Character.Job2X)
                                    p1.SkillPoints2 = this.Character.SkillPoint2X;
                                else if (this.Character.Job == this.Character.Job2T)
                                    p1.SkillPoints2 = this.Character.SkillPoint2T;
                            }
                        }
                    }
                }
                else if (type == 2)
                {
                    byte jobLV = SkillFactory.Instance.SkillList(this.Character.Job2X)[skillID];
                    if (this.Character.Skills2.ContainsKey((uint)skillID))
                    {
                        p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEARN.LearnResult.SKILL_NOT_LEARNED;
                    }
                    else
                    {
                        if (this.Character.SkillPoint2X < 3)
                        {
                            p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEARN.LearnResult.NOT_ENOUGH_SKILL_POINT;
                        }
                        else
                        {
                            SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(skillID, 1);
                            if (skill.JobLv != 0)
                                jobLV = skill.JobLv;
                            if (this.Character.JobLevel2X < jobLV)
                            {
                                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEARN.LearnResult.NOT_ENOUGH_JOB_LEVEL;
                            }
                            else
                            {
                                this.Character.SkillPoint2X -= 3;
                                this.Character.Skills2.Add(skillID, skill);
                                this.Character.Skills2_1.Add(skillID, skill);
                                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEARN.LearnResult.OK;
                                p1.SkillID = skillID;
                                p1.SkillPoints = this.Character.SkillPoint;
                                p1.SkillPoints2 = this.Character.SkillPoint2X;
                            }
                        }
                    }
                }
                else if (type == 3)
                {
                    byte jobLV = SkillFactory.Instance.SkillList(this.Character.Job2T)[skillID];

                    if (this.Character.Skills2.ContainsKey((uint)skillID))
                    {
                        p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEARN.LearnResult.SKILL_NOT_LEARNED;
                    }
                    else
                    {
                        if (this.Character.SkillPoint2T < 3)
                        {
                            p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEARN.LearnResult.NOT_ENOUGH_SKILL_POINT;
                        }
                        else
                        {
                            SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(skillID, 1);
                            if (skill.JobLv != 0)
                                jobLV = skill.JobLv;
                            if (this.Character.JobLevel2T < jobLV)
                            {
                                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEARN.LearnResult.NOT_ENOUGH_JOB_LEVEL;
                            }
                            else
                            {
                                this.Character.SkillPoint2T -= 3;
                                this.Character.Skills2.Add(skillID, skill);
                                this.Character.Skills2_2.Add(skillID, skill);
                                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEARN.LearnResult.OK;
                                p1.SkillID = skillID;
                                p1.SkillPoints = this.Character.SkillPoint;
                                p1.SkillPoints2 = this.Character.SkillPoint2T;
                            }
                        }
                    }
                }
                else if (type == 4)
                {
                    byte jobLV = SkillFactory.Instance.SkillList(this.Character.Job3)[skillID];

                    if (this.Character.Skills3.ContainsKey((uint)skillID))
                    {
                        p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEARN.LearnResult.SKILL_NOT_LEARNED;
                    }
                    else
                    {
                        if (this.Character.SkillPoint3 < 3)
                        {
                            p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEARN.LearnResult.NOT_ENOUGH_SKILL_POINT;
                        }
                        else
                        {
                            SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(skillID, 1);
                            if (skill.JobLv != 0)
                                jobLV = skill.JobLv;
                            if (this.Character.JobLevel3 < jobLV)
                            {
                                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEARN.LearnResult.NOT_ENOUGH_JOB_LEVEL;
                            }
                            else
                            {
                                this.Character.SkillPoint3 -= 3;
                                this.Character.Skills3.Add(skillID, skill);
                                p1.Result = SagaMap.Packets.Server.SSMG_SKILL_LEARN.LearnResult.OK;
                                p1.SkillID = skillID;
                                p1.SkillPoints = this.Character.SkillPoint;
                                p1.SkillPoints2 = this.Character.SkillPoint3;
                            }
                        }
                    }
                }
            }

            this.netIO.SendPacket(p1);
            SendSkillList();

            SkillHandler.Instance.CastPassiveSkills(this.Character);
            SendPlayerInfo();

            MapServer.charDB.SaveChar(this.Character, true);
        }

        Packets.Client.CSMG_SKILL_ATTACK Lastp;
        int delay;

        Thread main;
        public void OnSkillAttack(Packets.Client.CSMG_SKILL_ATTACK p, bool auto)
        {
            bool needthread = true;

            if (this.Character == null)
                return;
            if (!this.Character.Online || this.Character.HP == 0)
                return;

            Actor dActor = this.Map.GetActor(p.ActorID);
            SkillArg arg;

            Actor sActor = map.GetActor(Character.ActorID);
            if (sActor == null) return;
            if (dActor == null) return;
            if (sActor.MapID != dActor.MapID) return;
            if (sActor.TInt["targetID"] != dActor.ActorID)
            {
                sActor.TInt["targetID"] = (int)dActor.ActorID;
                //SendSystemMessage("锁定了【" + dActor.Name + "】作为目标");
                //Character.AutoAttack = true;

                this.Character.PartnerTartget = dActor; // Partner will follow the entity assigned to PartnerTarget.
            }

            if (needthread)
            {
                if (!auto && this.Character.AutoAttack)//客户端发来的攻击，但已开启自动
                {
                    Character.TInt["攻击检测"] += 1;
                    if (Character.TInt["攻击检测"] >= 3)
                        ScriptManager.Instance.VariableHolder.AInt[Character.Name + "攻击检测"] += Character.TInt["攻击检测"];
                    Lastp = p;
                    //return;
                }
                if (auto && !this.Character.AutoAttack)//自动攻击，但人物处于不能自动攻击状态
                    return;
            }
            byte s = 0;

            //射程判定
            if (this.Character == null || dActor == null)
                return;
            if (this.Character.Range + 1
                < Math.Max(Math.Abs(this.Character.X - dActor.X) / 100
                , Math.Abs(this.Character.Y - dActor.Y) / 100))
            {
                arg = new SkillArg();
                arg.sActor = this.Character.ActorID;
                arg.type = (ATTACK_TYPE)0xff;
                arg.affectedActors.Add(Character);
                arg.Init();
                this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.ATTACK, arg, this.Character, true);
                this.Character.AutoAttack = false;
                return;
            }
            this.Character.LastAttackActorID = 0;

            //this.lastAttackRandom = p.Random;
            if (this.Character.Status.Additions.ContainsKey("Meditatioon"))
            {
                this.Character.Status.Additions["Meditatioon"].AdditionEnd();
                this.Character.Status.Additions.Remove("Meditatioon");
            }
            if (this.Character.Status.Additions.ContainsKey("Hiding"))
            {
                this.Character.Status.Additions["Hiding"].AdditionEnd();
                this.Character.Status.Additions.Remove("Hiding");
            }
            if (this.Character.Status.Additions.ContainsKey("fish"))
            {
                this.Character.Status.Additions["fish"].AdditionEnd();
                this.Character.Status.Additions.Remove("fish");
            }
            if (this.Character.Status.Additions.ContainsKey("IAmTree"))
            {
                this.Character.Status.Additions["IAmTree"].AdditionEnd();
                this.Character.Status.Additions.Remove("IAmTree");
            }
            if (this.Character.Status.Additions.ContainsKey("Cloaking"))
            {
                this.Character.Status.Additions["Cloaking"].AdditionEnd();
                this.Character.Status.Additions.Remove("Cloaking");
            }
            if (this.Character.Status.Additions.ContainsKey("Invisible"))
            {
                this.Character.Status.Additions["Invisible"].AdditionEnd();
                this.Character.Status.Additions.Remove("Invisible");
            }
            if (this.Character.Status.Additions.ContainsKey("Stun") || this.Character.Status.Additions.ContainsKey("Sleep") || this.Character.Status.Additions.ContainsKey("Frosen") ||
            this.Character.Status.Additions.ContainsKey("Stone"))
                return;
            if (dActor == null || DateTime.Now < attackStamp)
            {
                if (s == 1)
                {
                    arg = new SkillArg();
                    arg.sActor = this.Character.ActorID;
                    arg.type = (ATTACK_TYPE)0xff;
                    arg.affectedActors.Add(this.Character);
                    arg.Init();
                    this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.ATTACK, arg, this.Character, true);
                    this.Character.AutoAttack = false;
                    return;
                }
                else
                {
                    arg = new SkillArg();
                    arg.sActor = this.Character.ActorID;
                    arg.type = (ATTACK_TYPE)0xff;
                    arg.affectedActors.Add(this.Character);
                    arg.Init();
                    this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.ATTACK, arg, this.Character, true);
                    this.Character.AutoAttack = false;
                    return;
                }
            }
            if (dActor.HP == 0 || dActor.Buff.Dead)
            {
                arg = new SkillArg();
                arg.sActor = this.Character.ActorID;
                arg.type = (ATTACK_TYPE)0xff;
                arg.affectedActors.Add(this.Character);
                arg.Init();
                this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.ATTACK, arg, this.Character, true);
                this.Character.AutoAttack = false;
                return;
            }
            arg = new SkillArg();

            delay = (int)((float)2000 * (1.0f - (float)this.Character.Status.aspd / 1000.0f));
            delay = (int)(delay * arg.delayRate);
            if (this.Character.Status.aspd_skill_perc >= 1f)
                delay = (int)(delay / this.Character.Status.aspd_skill_perc);

            if (!needthread && Character.HP > 0)
                SkillHandler.Instance.Attack(this.Character, dActor, arg);//攻击

            if (this.Character.HP > 0 && !AttactFinished && needthread)//处于战斗状态
                SkillHandler.Instance.Attack(this.Character, dActor, arg);//攻击

            if (arg.affectedActors.Count > 0)
                attackStamp = DateTime.Now + new TimeSpan(0, 0, 0, 0, delay);

            this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.ATTACK, arg, this.Character, true);

            AttactFinished = false;
            PartnerTalking(Character.Partner, TALK_EVENT.BATTLE, 1, 20000);
            //新加
            if (needthread && s == 0)
            {
                Lastp = p;
                this.Character.LastAttackActorID = dActor.ActorID;
                delay = (int)((float)2000 * (1.0f - (float)this.Character.Status.aspd / 1000.0f));
                delay = (int)(delay * arg.delayRate);
                if (this.Character.Status.aspd_skill_perc >= 1f)
                    delay = (int)(delay / this.Character.Status.aspd_skill_perc);

                //谜一样的弓,双枪延迟缩短,先注释掉
                //if (Character.Inventory.Equipments.ContainsKey(EnumEquipSlot.LEFT_HAND))
                //{
                //    ItemType it = Character.Inventory.Equipments[EnumEquipSlot.LEFT_HAND].BaseData.itemType;
                //    if (it == ItemType.DUALGUN || it == ItemType.BOW)
                //        delay = (int)(delay * 0.6f);
                //}

                try
                {
                    if (main != null)
                        ClientManager.RemoveThread(main.Name);
                    if (Character == null)
                        return;
                    if (this == null)
                        return;
                    main = new Thread(MainLoop);
                    main.Name = string.Format("ThreadPoolMainLoopAUTO({0})" + Character.Name, main.ManagedThreadId);
                    ClientManager.AddThread(main);
                    this.Character.AutoAttack = true;
                    main.Start();
                }
                catch (Exception ex)
                {
                    Logger.ShowError(ex);
                }
            }
        }


        private void MainLoop()
        {
            try
            {
                if (Character == null)
                {
                    if (main != null)
                        ClientManager.RemoveThread(main.Name);
                    return;
                }
                if (this == null)
                    return;

                if (delay <= 0)
                    delay = 60;
                Thread.Sleep(delay);

                if (Character != null)
                {
                    OnSkillAttack(Lastp, true);
                    Character.TInt["攻击检测"] = 0;
                }
                else
                    ClientManager.RemoveThread(main.Name);
            }

            catch (Exception ex)
            {
                Logger.ShowError(main.Name + " Thread " + ex);
                return;
            }
        }


        public void OnSkillChangeBattleStatus(Packets.Client.CSMG_SKILL_CHANGE_BATTLE_STATUS p)
        {
            if (p.Status == 0)
                this.Character.AutoAttack = false;

            if (this.Character.BattleStatus != p.Status)
            {
                this.Character.BattleStatus = p.Status;
                SendChangeStatus();
            }
            if (this.Character.Tasks.ContainsKey("RangeAttack") && Character.BattleStatus == 0)
            {
                Character.Tasks["RangeAttack"].Deactivate();
                Character.Tasks.Remove("RangeAttack");
                Character.TInt["RangeAttackMark"] = 0;
            }
            if (this.Character.Tasks.ContainsKey("SkillCast") && Character.BattleStatus == 0 && (Character.Skills.ContainsKey(14000) || Character.Skills3.ContainsKey(14000)) && (Character.Job == PC_JOB.CARDINAL || Character.Job == PC_JOB.ASTRALIST))
            {
                /*if (this.Character.Tasks["SkillCast"].getActivated())
                {
                    this.Character.Tasks["SkillCast"].Deactivate();
                    this.Character.Tasks.Remove("SkillCast");
                }*/

                Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, this.Character, true);

                Packets.Server.SSMG_SKILL_CAST_RESULT p2 = new SagaMap.Packets.Server.SSMG_SKILL_CAST_RESULT();
                p2.ActorID = Character.ActorID;
                p2.Result = 20;
                this.netIO.SendPacket(p2);
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
            Dictionary<uint, byte> skills3;
            Dictionary<uint, byte> skillsHeat;
            List<SagaDB.Skill.Skill> list = new List<SagaDB.Skill.Skill>();
            skills = SkillFactory.Instance.CheckSkillList(this.Character, SkillFactory.SkillPaga.p1);
            skills2X = SkillFactory.Instance.CheckSkillList(this.Character, SkillFactory.SkillPaga.p21);
            skills2T = SkillFactory.Instance.CheckSkillList(this.Character, SkillFactory.SkillPaga.p22);
            skills3 = SkillFactory.Instance.CheckSkillList(this.Character, SkillFactory.SkillPaga.p3);
            skillsHeat = SkillFactory.Instance.CheckSkillList(this.Character, SkillFactory.SkillPaga.none);

            if (this.chara.Skills.ContainsKey(skillID))
            {
                if (this.chara.Skills[skillID].Level >= skillLV)
                    return true;
            }
            if (this.chara.Skills2.ContainsKey(skillID))
            {
                if (this.chara.Skills2[skillID].Level >= skillLV)
                    return true;
            }
            if (this.chara.Skills2_1.ContainsKey(skillID))
            {
                if (this.chara.Skills2_1[skillID].Level >= skillLV)
                    return true;
            }
            if (this.chara.Skills2_2.ContainsKey(skillID))
            {
                if (this.chara.Skills2_2[skillID].Level >= skillLV)
                    return true;
            }
            if (this.chara.Skills2.ContainsKey(skillID))
            {
                if (this.chara.Skills2[skillID].Level >= skillLV)
                    return true;
            }
            if (this.chara.Skills3.ContainsKey(skillID))
            {
                if (this.chara.Skills3[skillID].Level >= skillLV)
                    return true;
            }
            if (this.chara.SkillsReserve.ContainsKey(skillID))
            {
                if (this.chara.SkillsReserve[skillID].Level >= skillLV)
                    return true;
            }
            if (this.chara.SkillsReserve.ContainsKey(skillID) && this.Character.DominionReserveSkill)
            {
                if (this.chara.SkillsReserve[skillID].Level >= skillLV)
                    return true;
            }
            if (this.Character.JobJoint != PC_JOB.NONE)
            {
                {
                    var skill =
                        from c in SkillFactory.Instance.SkillList(this.Character.JobJoint)
                        where c.Value <= this.Character.JointJobLevel
                        select c;
                    foreach (KeyValuePair<uint, byte> i in skill)
                    {
                        if (i.Key == skillID && this.chara.JointJobLevel >= i.Value)
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

        /// <summary>
        /// 检查技能是否符合使用条件
        /// </summary>
        /// <param name="skill">技能数据</param>
        /// <param name="arg">技能参数</param>
        /// <param name="mp">mp</param>
        /// <param name="sp">sp</param>
        /// <param name="ep">ep</param>
        /// <returns>结果</returns>
        private short CheckSkillUse(SagaDB.Skill.Skill skill, SkillArg arg, ushort mp, ushort sp, ushort ep)
        {
            if (SingleCDLst.ContainsKey(arg.skill.ID) && DateTime.Now < SingleCDLst[arg.skill.ID] && !this.nextCombo.Contains(arg.skill.ID))
                return -30;
            if (arg.skill.ID == 3372)
            {
                SingleCDLst.Clear();
                return 0;
            }
            if (DateTime.Now < skillDelay && !this.nextCombo.Contains(arg.skill.ID))
                return -30;
            if (this.Character.SP < sp || this.Character.MP < mp || this.Character.EP < ep)
            {
                if (this.Character.SP < sp && this.Character.MP < mp)
                    return -1;
                else if (this.Character.SP < sp)
                    return -16;
                else if (this.Character.MP < mp)
                    return -15;
                else
                    return -62;
            }

            if (!SkillHandler.Instance.CheckSkillCanCastForWeapon(this.chara, arg))
                return -5;

            if (this.Character.Status.Additions.ContainsKey("Silence"))
                return -7;

            if (this.Character.Status.Additions.ContainsKey("居合模式"))
            {
                if (arg.skill.ID != 2129)
                    return -7;
                else
                {
                    this.Character.Status.Additions["居合模式"].AdditionEnd();
                    this.Character.Status.Additions.Remove("居合模式");
                }
            }
            if (this.GetPossessionTarget() != null)
            {
                if (this.GetPossessionTarget().Buff.Dead && arg.skill.ID != 3055)
                    return -27;
            }
            if (this.scriptThread != null)
            {
                return -59;
            }
            if (skill.NoPossession)
            {
                if (this.chara.Buff.GetReadyPossession || this.chara.PossessionTarget != 0)
                {
                    return -25;
                }
            }
            if (skill.NotBeenPossessed)
            {
                if (this.chara.PossesionedActors.Count > 0)
                {
                    return -24;
                }
            }
            if (this.Character.Tasks.ContainsKey("SkillCast"))
            {
                if (arg.skill.ID == 3311)
                    return 0;
                else
                    return -8;
            }
            short res = (short)SkillHandler.Instance.TryCast(this.Character, this.Map.GetActor(arg.dActor), arg);
            if (res < 0)
                return res;
            return 0;
        }

        public void OnSkillCast(Packets.Client.CSMG_SKILL_CAST p, bool useMPSP, bool nocheck)
        {
            if (((!checkSkill(p.SkillID, p.SkillLv) && this.chara.Account.GMLevel < 2) ||
                (p.Random == this.lastCastRandom && this.chara.Account.GMLevel < 2)) && !nocheck)
            {
                SendHack();
                if (hackCount > 2)
                    return;
            }

            //断掉自动放技能
            Character.AutoAttack = false;
            if (main != null)
                ClientManager.RemoveThread(main.Name);


            this.lastCastRandom = p.Random;
            SagaDB.Skill.Skill skill = SkillFactory.Instance.GetSkill(p.SkillID, p.SkillLv);
            if (this.Character.Status.Additions.ContainsKey("Meditatioon"))
            {
                this.Character.Status.Additions["Meditatioon"].AdditionEnd();
                this.Character.Status.Additions.Remove("Meditatioon");
            }
            if (this.Character.Status.Additions.ContainsKey("Hiding"))
            {
                this.Character.Status.Additions["Hiding"].AdditionEnd();
                this.Character.Status.Additions.Remove("Hiding");
            }
            if (this.Character.Status.Additions.ContainsKey("fish"))
            {
                this.Character.Status.Additions["fish"].AdditionEnd();
                this.Character.Status.Additions.Remove("fish");
            }
            if (this.Character.Status.Additions.ContainsKey("Cloaking"))
            {
                this.Character.Status.Additions["Cloaking"].AdditionEnd();
                this.Character.Status.Additions.Remove("Cloaking");
            }
            if (this.Character.Status.Additions.ContainsKey("IAmTree"))
            {
                this.Character.Status.Additions["IAmTree"].AdditionEnd();
                this.Character.Status.Additions.Remove("IAmTree");
            }
            if (this.Character.Status.Additions.ContainsKey("Invisible"))
            {
                this.Character.Status.Additions["Invisible"].AdditionEnd();
                this.Character.Status.Additions.Remove("Invisible");
            }
            if (this.Character.Tasks.ContainsKey("Regeneration"))
            {
                this.Character.Tasks["Regeneration"].Deactivate();
                this.Character.Tasks.Remove("Regeneration");
            }

            SkillArg arg = new SkillArg();
            arg.sActor = this.Character.ActorID;
            arg.dActor = p.ActorID;
            arg.skill = skill;
            arg.x = p.X;
            arg.y = p.Y;
            arg.argType = SkillArg.ArgType.Cast;
            ushort sp, mp, ep;
            //凭依时消耗加倍
            if (this.Character.PossessionTarget != 0)
            {
                sp = (ushort)(skill.SP * 2);
                mp = (ushort)(skill.MP * 2);
            }
            else
            {
                sp = skill.SP;
                mp = skill.MP;
            }
            if (this.Character.Status.Additions.ContainsKey("SwordEaseSp"))
            {
                //sp = (ushort)(skill.SP * 2);
                //mp = (ushort)(skill.MP * 2);
                sp = (ushort)(skill.SP * 0.7);
                //mp = (ushort)(skill.MP * 0.7);
            }
            if (Character.Status.Additions.ContainsKey("元素解放"))
            {
                sp = (ushort)(skill.SP * 2);
                mp = (ushort)(skill.MP * 2);
            }

            if (this.Character.Status.zenList.Contains((ushort)skill.ID) || this.Character.Status.darkZenList.Contains((ushort)skill.ID))
                mp = (ushort)(mp * 2);

            if (this.Character.Status.Additions.ContainsKey("EnergyExcess"))//能量增幅耗蓝加深
            {
                float[] rate = { 0, 0.05f, 0.16f, 0.28f, 0.4f, 0.65f };
                mp += (ushort)(skill.MP * rate[this.Character.TInt["EnergyExcess"]]);
            }
            if (!useMPSP)
            {
                sp = 0;
                mp = 0;
            }
            ep = skill.EP;
            arg.useMPSP = useMPSP;
            //检查技能是否复合使用条件 0为符合, 其他为使用失败
            arg.result = CheckSkillUse(skill, arg, mp, sp, ep);

            if (arg.result == 0)
            {
                //使物理技能的读条时间受aspd影响,法系读条受cspd影响.
                //2018.07.13 现在魔法系职业的读条时间不可能小于0.5秒.
                if (skill.BaseData.flag.Test(SkillFlags.PHYSIC))
                    arg.delay = (uint)((float)skill.CastTime * (1.0f - (float)this.Character.Status.aspd / 1000.0f));
                else
                    arg.delay = (uint)Math.Max(((float)skill.CastTime * (1.0f - (float)this.Character.Status.cspd / 1000.0f)), 500);
                if (arg.skill.ID == 2559)
                {
                    if (this.Character.Gold >= 90000000)
                        arg.delay = (uint)((float)arg.delay * 0.5f);
                    else if (this.Character.Gold >= 50000000)
                        arg.delay = (uint)((float)arg.delay * 0.6f);
                    else if (this.Character.Gold >= 5000000)
                        arg.delay = (uint)((float)arg.delay * 0.7f);
                    else if (this.Character.Gold >= 500000)
                        arg.delay = (uint)((float)arg.delay * 0.8f);
                    else if (this.Character.Gold >= 50000)
                        arg.delay = (uint)((float)arg.delay * 0.9f);
                }

                if (this.Character.Status.delayCancelList.ContainsKey((ushort)arg.skill.ID))
                {
                    int rate = this.Character.Status.delayCancelList[(ushort)arg.skill.ID];
                    arg.delay = (uint)(arg.delay * (1f - ((float)rate / 100.0f)));
                }
                //bool get = Character.Status.Additions.ContainsKey("EaseCt");
                if (Character.Status.Additions.ContainsKey("EaseCt") && arg.skill.ID != 2238)//杀界模块
                {
                    float eclv = new float[] { 0f, 0.5f, 0.7f, 0.8f, 0.9f, 1.0f }[Character.Status.EaseCt_lv];
                    arg.delay = (uint)(arg.delay * (1.0f - eclv));
                    SkillHandler.RemoveAddition(Character, "EaseCt");

                }


                this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, arg, this.Character, true);
                //if (this.Character.Status.Additions.ContainsKey("SwordEaseSp"))
                //{
                //    this.nextCombo.Clear();
                //    OnSkillCastComplete(arg);
                //}
                //else 
                if (skill.CastTime > 0 && !this.nextCombo.Contains(arg.skill.ID))
                {
                    Tasks.PC.SkillCast task = new SagaMap.Tasks.PC.SkillCast(this, arg);
                    this.Character.Tasks.Add("SkillCast", task);

                    task.Activate();
                    this.nextCombo.Clear(); ;
                }
                else
                {
                    this.nextCombo.Clear();
                    OnSkillCastComplete(arg);
                }
                if (this.Character.Status.Additions.ContainsKey("Parry"))
                    arg.delay = (uint)arg.skill.BaseData.delay;
            }
            else
            {
                this.Character.e.OnActorSkillUse(this.Character, arg);
            }
        }

        public void OnSkillCastComplete(SkillArg skill)
        {
            if (this.Character.Status.Additions.ContainsKey("Meditatioon"))
            {
                this.Character.Status.Additions["Meditatioon"].AdditionEnd();
                this.Character.Status.Additions.Remove("Meditatioon");
            }
            if (this.Character.Status.Additions.ContainsKey("Hiding"))
            {
                this.Character.Status.Additions["Hiding"].AdditionEnd();
                this.Character.Status.Additions.Remove("Hiding");
            }
            if (this.Character.Status.Additions.ContainsKey("fish"))
            {
                this.Character.Status.Additions["fish"].AdditionEnd();
                this.Character.Status.Additions.Remove("fish");
            }
            if (this.Character.Status.Additions.ContainsKey("Cloaking"))
            {
                this.Character.Status.Additions["Cloaking"].AdditionEnd();
                this.Character.Status.Additions.Remove("Cloaking");
            }
            if (this.Character.Status.Additions.ContainsKey("IAmTree"))
            {
                this.Character.Status.Additions["IAmTree"].AdditionEnd();
                this.Character.Status.Additions.Remove("IAmTree");
            }

            if (skill.dActor != 0xFFFFFFFF)
            {
                Actor dActor = this.Map.GetActor(skill.dActor);
                if (dActor != null)
                {
                    skill.argType = SkillArg.ArgType.Active;
                    PartnerTalking(Character.Partner, TALK_EVENT.BATTLE, 1, 20000);
                    if (skill.useMPSP)
                    {
                        uint mpCost = skill.skill.MP;
                        uint spCost = skill.skill.SP;
                        uint epCost = skill.skill.EP;
                        if (Character.Status.sp_rate_down_iris < 100)
                        {
                            spCost = (uint)(spCost * (float)(Character.Status.sp_rate_down_iris / 100.0f));
                        }
                        if (Character.Status.mp_rate_down_iris < 100)
                        {
                            mpCost = (uint)(mpCost * (float)(Character.Status.mp_rate_down_iris / 100.0f));
                        }

                        if (this.Character.Status.doubleUpList.Contains((ushort)skill.skill.ID))
                            spCost = (ushort)(spCost * 2);

                        if (this.Character.Status.Additions.ContainsKey("SwordEaseSp"))
                        {
                            //mpCost = (ushort)(mpCost*0.7);
                            spCost = (ushort)(spCost * 0.7);
                        }
                        if (this.Character.Status.Additions.ContainsKey("HarvestMaster"))
                        {
                            mpCost = (ushort)(mpCost * (float)(1.0f - Character.Status.HarvestMaster_Lv * 0.05));
                            spCost = (ushort)(spCost * (float)(1.0f - Character.Status.HarvestMaster_Lv * 0.05));
                        }
                        if (skill.skill.ID == 2527 && (Character.Skills2_2.ContainsKey(2355) || Character.DualJobSkill.Exists(x => x.ID == 2355)))//当技能为神速斩
                        {
                            //这里取副职的拔刀斩等级
                            var duallv = 0;
                            if (Character.DualJobSkill.Exists(x => x.ID == 2355))
                                duallv = Character.DualJobSkill.FirstOrDefault(x => x.ID == 2355).Level;

                            //这里取主职的拔刀斩等级
                            var mainlv = 0;
                            if (Character.Skills2_2.ContainsKey(2355))
                                mainlv = Character.Skills2_2[2355].Level;
                            //获取最高的拔刀斩等级
                            int maxlevel = Math.Max(duallv, mainlv);
                            spCost = (ushort)(spCost - (spCost * maxlevel * 0.04f));

                        }

                        if (this.Character.PossessionTarget != 0)
                        {
                            mpCost = (ushort)(mpCost * 2);
                            spCost = (ushort)(spCost * 2);
                        }

                        if (this.Character.Status.Additions.ContainsKey("Zensss"))
                            mpCost *= 2;

                        if (this.Character.Status.Additions.ContainsKey("EnergyExcess"))//能量增幅耗蓝加深
                        {
                            float[] rate = { 0, 0.05f, 0.16f, 0.28f, 0.4f, 0.65f };
                            mpCost += (ushort)(mpCost * rate[this.Character.TInt["EnergyExcess"]]);
                        }
                        if (mpCost > this.Character.MP && spCost > this.Character.SP)
                        {
                            skill.result = -1;
                            this.Character.e.OnActorSkillUse(this.Character, skill);
                            return;
                        }
                        else if (mpCost > this.Character.MP)
                        {
                            skill.result = -15;
                            this.Character.e.OnActorSkillUse(this.Character, skill);
                            return;
                        }
                        else if (spCost > this.Character.SP)
                        {
                            skill.result = -16;
                            this.Character.e.OnActorSkillUse(this.Character, skill);
                            return;
                        }
                        this.Character.MP -= mpCost;
                        if (this.Character.MP < 0)
                            this.Character.MP = 0;

                        this.Character.SP -= spCost;
                        if (this.Character.SP < 0)
                            this.Character.SP = 0;

                        this.Character.EP -= epCost;
                        if (this.Character.EP < 0)
                            this.Character.EP = 0;

                        this.SendActorHPMPSP(this.Character);
                    }
                    SkillHandler.Instance.SkillCast(this.Character, dActor, skill);
                }
                else
                {
                    skill.result = -11;
                    this.Character.e.OnActorSkillUse(this.Character, skill);
                }
            }
            else
            {
                skill.argType = SkillArg.ArgType.Active;
                if (skill.useMPSP)
                {
                    if (skill.skill.MP > this.Character.MP && skill.skill.SP > this.Character.SP)
                    {
                        skill.result = -1;
                        this.Character.e.OnActorSkillUse(this.Character, skill);
                        return;
                    }
                    else if (skill.skill.MP > this.Character.MP)
                    {
                        skill.result = -15;
                        this.Character.e.OnActorSkillUse(this.Character, skill);
                        return;
                    }
                    else if (skill.skill.SP > this.Character.SP)
                    {
                        skill.result = -16;
                        this.Character.e.OnActorSkillUse(this.Character, skill);
                        return;
                    }
                    this.Character.MP -= skill.skill.MP;
                    this.Character.SP -= skill.skill.SP;
                    this.SendActorHPMPSP(this.Character);
                }
                SkillHandler.Instance.SkillCast(this.Character, this.Character, skill);

            }

            if (this.Character.Pet != null)
            {
                if (this.Character.Pet.Ride)
                {
                    SkillHandler.Instance.ProcessPetGrowth(this.Character.Pet, PetGrowthReason.UseSkill);
                }
            }

            //技能延迟
            //if (this.Character.Status.Additions.ContainsKey("SwordEaseSp"))
            //{
            //    skillDelay = DateTime.Now + new TimeSpan(0, 0, 0, 0, (int)(skill.skill.Delay * 0.2f));
            //}
            //else 
            if (this.Character.Status.delayCancelList.ContainsKey((ushort)skill.skill.ID))
            {
                skillDelay = DateTime.Now + new TimeSpan(0, 0, 0, 0, (int)(skill.skill.Delay * (1f - ((float)this.Character.Status.delayCancelList[(ushort)skill.skill.ID] / 100.0f))));
            }
            else
                skillDelay = DateTime.Now + new TimeSpan(0, 0, 0, 0, skill.skill.Delay);

            //if (this.Character.Status.Additions.ContainsKey("DelayOut"))
            //    skillDelay = DateTime.Now + new TimeSpan(0, 0, 0, 0, 1);

            if (Character.Status.Additions.ContainsKey("OverWork") && !skill.skill.BaseData.flag.Test(SkillFlags.PHYSIC))//狂乱时间
            {
                int DelayTime = (Character.Status.Additions["OverWork"] as DefaultBuff).Variable["OverWork"];
                skillDelay = DateTime.Now + new TimeSpan(0, 0, 0, 0, (int)(skill.skill.Delay * (1f - ((float)DelayTime / 100.0f))));
            }

            if (this.Character.Status.aspd_skill_perc >= 1f)
                skillDelay = DateTime.Now + new TimeSpan(0, 0, 0, 0, (int)(skill.skill.Delay / this.Character.Status.aspd_skill_perc));

            //独立cd
            if (!SingleCDLst.ContainsKey(skill.skill.ID))
                SingleCDLst.Add(skill.skill.ID, DateTime.Now + new TimeSpan(0, 0, 0, 0, skill.skill.SinglgCD));
            else
                SingleCDLst[skill.skill.ID] = DateTime.Now + new TimeSpan(0, 0, 0, 0, skill.skill.SinglgCD);
            //if (!this.Character.Status.Additions.ContainsKey("DelayOut"))
            //{
            //    

            //}

            this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, skill, this.Character, true);

            if (skill.skill.Effect != 0 && (skill.skill.Target == 4 || (skill.skill.Target == 2 && skill.sActor == skill.dActor)))
            {
                EffectArg eff = new EffectArg();
                eff.actorID = skill.dActor;
                eff.effectID = skill.skill.Effect;
                eff.x = skill.x;
                eff.y = skill.y;
                this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SHOW_EFFECT, eff, this.Character, true);
            }

            if (this.Character.Tasks.ContainsKey("AutoCast"))
                this.Character.Tasks["AutoCast"].Activate();
            else
            {
                if (skill.autoCast.Count != 0)
                {
                    this.Character.Buff.CannotMove = true;
                    this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, this.Character, true);
                    Tasks.Skill.AutoCast task = new SagaMap.Tasks.Skill.AutoCast(this.Character, skill);
                    this.Character.Tasks.Add("AutoCast", task);
                    task.Activate();
                }
            }
        }

        public void SendChangeStatus()
        {
            if (this.Character.Tasks.ContainsKey("Regeneration"))
            {
                this.Character.Tasks["Regeneration"].Deactivate();
                this.Character.Tasks.Remove("Regeneration");
            }
            if (this.Character.Motion != MotionType.NONE && this.Character.Motion != MotionType.DEAD)
            {
                this.Character.Motion = MotionType.NONE;
                this.Character.MotionLoop = false;
            }
            this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.CHANGE_STATUS, null, this.Character, true);
        }

        public void SendRevive(byte level)
        {
            this.Character.Buff.Dead = false;
            this.Character.Buff.TurningPurple = false;
            this.Character.Motion = MotionType.STAND;
            this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.BUFF_CHANGE, null, this.Character, true);

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

            this.Character.HP = (uint)(this.Character.MaxHP * factor);
            this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, this.Character, true);
            SkillArg arg = new SkillArg();
            arg.sActor = this.Character.ActorID;
            arg.dActor = 0;
            arg.skill = SkillFactory.Instance.GetSkill(10002, level);
            arg.x = 0;
            arg.y = 0;
            arg.hp = new List<int>();
            arg.sp = new List<int>();
            arg.mp = new List<int>();
            arg.hp.Add((int)(-this.Character.MaxHP * factor));
            arg.sp.Add(0);
            arg.mp.Add(0);
            arg.flag.Add(AttackFlag.HP_HEAL);
            arg.affectedActors.Add(this.Character);
            arg.argType = SkillArg.ArgType.Active;
            this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, arg, this.Character, true);

            if (!this.Character.Tasks.ContainsKey("AutoSave"))
            {
                Tasks.PC.AutoSave task = new SagaMap.Tasks.PC.AutoSave(this.Character);
                this.Character.Tasks.Add("AutoSave", task);
                task.Activate();
            }
            if (!Character.Tasks.ContainsKey("Recover"))//自然恢复
            {
                Tasks.PC.Recover reg = new Tasks.PC.Recover(FromActorPC(Character));
                Character.Tasks.Add("Recover", reg);
                reg.Activate();
            }

            SkillHandler.Instance.CastPassiveSkills(this.Character);
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
            bool ifDominion = this.map.Info.Flag.Test(SagaDB.Map.MapFlags.Dominion);
            if (ifDominion)
            {
                skills = new Dictionary<uint, byte>();
                skills2X = new Dictionary<uint, byte>();
                skills2T = new Dictionary<uint, byte>();
                skills3 = new Dictionary<uint, byte>();
            }
            else
            {
                skills = SkillFactory.Instance.SkillList(this.Character.JobBasic);
                skills2X = SkillFactory.Instance.SkillList(this.Character.Job2X);
                skills2T = SkillFactory.Instance.SkillList(this.Character.Job2T);
                skills3 = SkillFactory.Instance.SkillList(this.Character.Job3);
            }
            {
                var skill =
                    from c in skills.Keys
                    where !this.Character.Skills.ContainsKey(c)
                    select c;
                foreach (uint i in skill)
                {
                    SagaDB.Skill.Skill sk = SkillFactory.Instance.GetSkill(i, 0);
                    list.Add(sk);
                }
                foreach (SagaDB.Skill.Skill i in this.Character.Skills.Values)
                {
                    list.Add(i);
                }
            }
            p.Skills(list, 0, this.Character.JobBasic, ifDominion, this.Character);
            this.netIO.SendPacket(p);
            if (this.Character.Rebirth || this.Character.Job == this.Character.Job3)
            {
                {
                    list.Clear();
                    {
                        var skill =
                            from c in skills2X.Keys
                            where !this.Character.Skills2_1.ContainsKey(c)
                            select c;
                        foreach (uint i in skill)
                        {
                            SagaDB.Skill.Skill sk = SkillFactory.Instance.GetSkill(i, 0);
                            list.Add(sk);
                        }
                        foreach (SagaDB.Skill.Skill i in this.Character.Skills2_1.Values)
                        {
                            list.Add(i);
                        }
                    }

                    p.Skills(list, 1, this.Character.Job2X, ifDominion, this.Character);
                    this.netIO.SendPacket(p);
                }
                {
                    list.Clear();
                    {
                        var skill =
                            from c in skills2T.Keys
                            where !this.Character.Skills2_2.ContainsKey(c)
                            select c;
                        foreach (uint i in skill)
                        {
                            SagaDB.Skill.Skill sk = SkillFactory.Instance.GetSkill(i, 0);
                            list.Add(sk);
                        }
                        foreach (SagaDB.Skill.Skill i in this.Character.Skills2_2.Values)
                        {
                            list.Add(i);
                        }
                    }
                    p.Skills(list, 2, this.Character.Job2T, ifDominion, this.Character);
                    this.netIO.SendPacket(p);
                }
                {
                    list.Clear();
                    {
                        var skill =
                            from c in skills3.Keys
                            where !this.Character.Skills3.ContainsKey(c)
                            select c;
                        foreach (uint i in skill)
                        {
                            SagaDB.Skill.Skill sk = SkillFactory.Instance.GetSkill(i, 0);
                            list.Add(sk);
                        }
                        foreach (SagaDB.Skill.Skill i in this.Character.Skills3.Values)
                        {
                            list.Add(i);
                        }
                    }

                    p.Skills(list, 3, this.Character.Job3, ifDominion, this.Character);
                    this.netIO.SendPacket(p);
                }

            }
            else
            {
                if (this.Character.Job == this.Character.Job2X)
                {
                    list.Clear();
                    {
                        var skill =
                            from c in skills2X.Keys
                            where !this.Character.Skills2.ContainsKey(c)
                            select c;
                        foreach (uint i in skill)
                        {
                            SagaDB.Skill.Skill sk = SkillFactory.Instance.GetSkill(i, 0);
                            list.Add(sk);
                        }
                        foreach (SagaDB.Skill.Skill i in this.Character.Skills2.Values)
                        {
                            list.Add(i);
                        }
                    }

                    p.Skills(list, 1, this.Character.Job2X, ifDominion, this.Character);
                    this.netIO.SendPacket(p);
                }
                if (this.Character.Job == this.Character.Job2T)
                {
                    list.Clear();
                    {
                        var skill =
                            from c in skills2T.Keys
                            where !this.Character.Skills2.ContainsKey(c)
                            select c;
                        foreach (uint i in skill)
                        {
                            SagaDB.Skill.Skill sk = SkillFactory.Instance.GetSkill(i, 0);
                            list.Add(sk);
                        }
                        foreach (SagaDB.Skill.Skill i in this.Character.Skills2.Values)
                        {
                            list.Add(i);
                        }
                    }
                    p.Skills(list, 2, this.Character.Job2T, ifDominion, this.Character);
                    this.netIO.SendPacket(p);
                }
                if (this.map.Info.Flag.Test(SagaDB.Map.MapFlags.Dominion))
                {
                    if (this.Character.DominionReserveSkill)
                    {
                        Packets.Server.SSMG_SKILL_RESERVE_LIST p2 = new SagaMap.Packets.Server.SSMG_SKILL_RESERVE_LIST();
                        p2.Skills = this.Character.SkillsReserve.Values.ToList();
                        this.netIO.SendPacket(p2);
                    }
                    else
                    {
                        Packets.Server.SSMG_SKILL_RESERVE_LIST p2 = new SagaMap.Packets.Server.SSMG_SKILL_RESERVE_LIST();
                        p2.Skills = new List<SagaDB.Skill.Skill>();
                        this.netIO.SendPacket(p2);
                    }
                }
                else
                {
                    Packets.Server.SSMG_SKILL_RESERVE_LIST p2 = new SagaMap.Packets.Server.SSMG_SKILL_RESERVE_LIST();
                    p2.Skills = this.Character.SkillsReserve.Values.ToList();
                    this.netIO.SendPacket(p2);
                }
            }


            if (this.Character.JobJoint != PC_JOB.NONE)
            {
                list.Clear();
                {
                    var skill =
                        from c in SkillFactory.Instance.SkillList(this.Character.JobJoint)
                        where c.Value <= this.Character.JointJobLevel
                        select c;
                    foreach (KeyValuePair<uint, byte> i in skill)
                    {
                        SagaDB.Skill.Skill sk = SkillFactory.Instance.GetSkill(i.Key, 1);
                        list.Add(sk);
                    }
                }
                Packets.Server.SSMG_SKILL_JOINT_LIST p2 = new SagaMap.Packets.Server.SSMG_SKILL_JOINT_LIST();
                p2.Skills = list;
                this.netIO.SendPacket(p2);
            }
            else
            {
                Packets.Server.SSMG_SKILL_JOINT_LIST p2 = new SagaMap.Packets.Server.SSMG_SKILL_JOINT_LIST();
                p2.Skills = new List<SagaDB.Skill.Skill>();
                this.netIO.SendPacket(p2);
            }
        }

        public void SendSkillDummy()
        {
            SendSkillDummy(3311, 1);
        }

        public void SendSkillDummy(uint skillid, byte level)
        {
            if (this.Character.Tasks.ContainsKey("SkillCast"))
            {
                if (this.Character.Tasks["SkillCast"].getActivated())
                {
                    this.Character.Tasks["SkillCast"].Deactivate();
                    this.Character.Tasks.Remove("SkillCast");
                }

                SkillArg arg = new SkillArg();
                arg.sActor = this.Character.ActorID;
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
                this.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.SKILL, arg, this.Character, true);
            }
        }
        public void OnSkillRangeAttack(Packets.Client.CSMG_SKILL_RANGE_ATTACK p)
        {
            Packets.Server.SSMG_SKILL_RANGEA_RESULT p2 = new Packets.Server.SSMG_SKILL_RANGEA_RESULT();
            p2.ActorID = p.ActorID;
            if (!Character.Status.Additions.ContainsKey("自由射击"))
                p2.Speed = 410;
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
            int totalPoints = 0;
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
                        this.Character.SkillPoint2X = 0;
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
                        this.Character.SkillPoint2T = 0;
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
            SkillHandler.Instance.CastPassiveSkills(this.Character);
        }

        public void OnFishBaitsEquip(Packets.Client.CSMG_FF_FISHBAIT_EQUIP p)
        {
            if (p.InventorySlot == 0)
            {
                this.Character.EquipedBaitID = 0;

                Packets.Server.SSMG_FISHBAIT_EQUIP_RESULT p2 = new Packets.Server.SSMG_FISHBAIT_EQUIP_RESULT();
                p2.InventoryID = 0;
                p2.IsEquip = 1;
                this.netIO.SendPacket(p2);
            }
            else
            {
                SagaDB.Item.Item item = this.Character.Inventory.GetItem(p.InventorySlot);
                if (item.ItemID >= 10104900 || item.ItemID <= 10104906)
                {
                    this.Character.EquipedBaitID = item.ItemID;

                    Packets.Server.SSMG_FISHBAIT_EQUIP_RESULT p2 = new Packets.Server.SSMG_FISHBAIT_EQUIP_RESULT();
                    p2.InventoryID = p.InventorySlot;
                    p2.IsEquip = 0;
                    this.netIO.SendPacket(p2);
                }
            }
        }
    }
}
