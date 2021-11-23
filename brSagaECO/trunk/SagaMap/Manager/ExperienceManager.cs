using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SagaDB.Actor;
using SagaDB.Item;
using SagaDB.Party;
using SagaLib;
using SagaLib.VirtualFileSystem;
using SagaMap.Network.Client;
using SagaMap.Scripting;

namespace SagaMap.Scripting
{
    public enum LevelType : byte
    {
        CLEVEL = 1,
        JLEVEL = 2,
        JLEVEL2 = 3,
        JLEVEL2T = 4,
        CLEVEL2 = 5,
        JLEVEL3 = 6,
    }

}
namespace SagaMap.Manager
{
    public sealed class ExperienceManager : Singleton<ExperienceManager>
    {
        short[,] clTable = new short[,]{
            {0,1},
            {82,3},
            {96,4},
            {124,5},
            {152,6},
            {163,11},
            {177,12},
            {192,13},
            {207,14},
            {223,15},
            {238,16},
            {243,23},
            {248,24},
            {258,25},
            {269,26},
            {279,27},
            {289,28},
            {300,29},
            {310,30},
            {320,31},
            {320,31},
            {325,41},
            {332,42},
            {340,43},
            {347,44},
            {355,45},
            {363,46},
            {371,47},
            {380,48},
            {388,49},
            {395,50},
            {402,51},
            {406,63},
            {409,64},
            {415,65},
            {422,66},
            {427,67},
            {433,68},
            {440,69},
            {446,70},
            
            {30000,70},
            {30000,70}
        };
        short[,] clTableDom = new short[,]{
            {0,1},
            {82,2},
            {135,3},
            {163,5},
            {205,6},
            {30000,6},
                {30000,6}
    };

        #region Private fields
        private Dictionary<uint, Level> Chart = new Dictionary<uint, Level>();

        // Fields of local use only, declared here for memory optimization
        private uint currentMax;
        #endregion

        #region Public fields

        //public uint MaxCLevel = SagaDB.LevelLimit.LevelLimit.Instance.NowLevelLimit;
        //public readonly uint MaxJLevel = 50;
        //public uint MaxCLevel2 = SagaDB.LevelLimit.LevelLimit.Instance.NowLevelLimit;
        //public readonly uint MaxJLevel3 = 50;
        //public uint LastTimeLevelLimit = SagaDB.LevelLimit.LevelLimit.Instance.LastTimeLevelLimit;

        public readonly uint MaxCLevel = 110;
        public readonly uint MaxJLevel = 50;
        public readonly uint MaxCLevel2 = 110;
        public readonly uint MaxJLevel3 = 50;
        public uint LastTimeLevelLimit = 110;
        #endregion

        #region Enums/Structs

        public struct Level
        {
            public readonly ulong cxp, jxp, jxp2, cxp2, jexp3;
            public Level(ulong cxp, ulong jxp, ulong jxp2, ulong cxp2, ulong jexp3)
            {
                this.cxp = cxp;
                this.jxp = jxp;
                this.jxp2 = jxp2;
                this.cxp2 = cxp2;
                this.jexp3 = jexp3;
            }
        }
        #endregion


        #region Public methods
        #region EXP table loading methods
        public void LoadTable(string path)
        {
            XmlDocument xml = new XmlDocument();
            try
            {
                XmlElement root;
                XmlNodeList list;
                xml.Load(VirtualFileSystemManager.Instance.FileSystem.OpenFile(path));
                root = xml["exp"];
                list = root.ChildNodes;
                foreach (object j in list)
                {
                    XmlElement i;
                    if (j.GetType() != typeof(XmlElement)) continue;
                    i = (XmlElement)j;
                    switch (i.Name.ToLower())
                    {
                        case "level":
                            XmlNodeList maps = i.ChildNodes;
                            ulong cexp = 0, cexp2 = 0, jexp = 0, jexp2 = 0, jexp3 = 0;
                            byte lv = 0;
                            foreach (object l in maps)
                            {
                                XmlElement k;
                                if (l.GetType() != typeof(XmlElement)) continue;
                                k = (XmlElement)l;
                                switch (k.Name.ToLower())
                                {
                                    case "lv":
                                        lv = byte.Parse(k.InnerText);
                                        break;
                                    case "c":
                                        cexp = ulong.Parse(k.InnerText);
                                        break;
                                    case "j1":
                                        jexp = ulong.Parse(k.InnerText);
                                        break;
                                    case "j2":
                                        jexp2 = ulong.Parse(k.InnerText);
                                        break;
                                    case "j3":
                                        jexp3 = ulong.Parse(k.InnerText);
                                        break;
                                    case "c2":
                                        cexp2 = ulong.Parse(k.InnerText);
                                        break;
                                }
                            }
                            Level newLv = new Level(cexp, jexp, jexp2, cexp2, jexp3);
                            Chart.Add(lv, newLv);
                            break;
                    }
                }
                Logger.ShowInfo("EXP table loaded");
            }
            catch (Exception ex)
            {
                Logger.ShowError(ex);
            }
        }
        #endregion

        public short GetEPRequired(ActorPC pc)
        {
            if (pc.Race == PC_RACE.DEM)
            {
                Map map = MapManager.Instance.GetMap(pc.MapID);
                if (map.Info.Flag.Test(SagaDB.Map.MapFlags.Dominion))
                {
                    return GetEPRequired(pc.DominionCL, true);
                }
                else
                {
                    return GetEPRequired(pc.CL, false);
                }
            }
            return 0;
        }

        public short GetEPRequired(short cl, bool dominion)
        {
            if (dominion)
            {
                for (int i = 0; i < clTableDom.Length; i++)
                {
                    if (cl < clTableDom[i, 0])
                        return clTableDom[i - 1, 1];
                }
            }
            else
            {
                for (int i = 0; i < clTableDom.Length; i++)
                {
                    if (cl < clTable[i, 0])
                        return clTable[i - 1, 1];
                }
            }
            return 0;
        }

        public void ApplyEP(ActorPC pc, short count)
        {
            /*bool dominion = MapManager.Instance.GetMap(pc.MapID).Info.Flag.Test(SagaDB.Map.MapFlags.Dominion);
            short cl = 0;
            while (count > 0)
            {
                if (dominion)
                {
                    short max_ep = GetEPRequired((short)(pc.DominionCL + cl), dominion);
                    if (pc.DominionEPUsed + count >= max_ep)
                    {
                        cl++;
                        count -= (short)(max_ep - pc.DominionEPUsed);
                        pc.DominionEPUsed = 0;
                    }
                    else
                    {
                        pc.DominionEPUsed += count;
                        count = 0;
                    }
                }
                else
                {
                    short max_ep = GetEPRequired((short)(pc.CL + cl), dominion);
                    if (pc.EPUsed + count >= max_ep)
                    {
                        cl++;
                        count -= (short)(max_ep - pc.EPUsed);
                        pc.EPUsed = 0;
                    }
                    else
                    {
                        pc.EPUsed += count;
                        count = 0;
                    }
                }
            }
            if (cl > 0)
                ApplyCL(pc, cl);*/
            //改革！
        }

        public void ApplyCL(ActorPC pc, short count)
        {
            if (pc.Race == PC_RACE.DEM)
            {
                Map map = MapManager.Instance.GetMap(pc.MapID);
                if (map.Info.Flag.Test(SagaDB.Map.MapFlags.Dominion))
                {
                    pc.DominionCL += count;
                }
                else
                {
                    pc.CL += count;
                }
                checkCL(pc);
            }
        }

        /// <summary>
        /// Apply input percentage of experience from input targetNPC to input targetPC.
        /// The percentage gets capped at 1f and are multiplied by global EXP rate(s).
        /// </summary>
        /// <param name="targetPC">The target PC (the player)</param>
        /// <param name="percentage">The percentage of the NPC's exp to gain (for instance: the percentage of HP deducted by input player)</param>

        public void ApplyQuestExp(ActorPC targetPC, uint exp, uint jexp, float percentage)
        {
            // TODO implement different rates for different exp types
            //percentage *= (float)Config.Instance.EXPRate / 100f;
            //Weapon weapon = WeaponFactory.GetActiveWeapon(targetPC);

            percentage = percentage * Configuration.Instance.CalcQuestRateForPC(targetPC);

            float realexp = exp * percentage;
            float realjobexp = jexp * percentage;
            
            ApplyExp(targetPC, (ulong)realexp, (ulong)realjobexp, false);
        }

        /// <summary>
        /// Apply input percentage of experience from input targetNPC to input targetPC.
        /// The percentage gets capped at 1f and are multiplied by global EXP rate(s).
        /// </summary>
        /// <param name="targetPC">The target PC (the player)</param>
        /// <param name="targetNPC">The target NPC (the "mob")</param>
        /// <param name="percentage">The percentage of the NPC's exp to gain (for instance: the percentage of HP deducted by input player)</param>
        public void ApplyMobExp(ActorPC targetPC, ActorMob targetNPC, float percentage)
        {
            // TODO implement different rates for different exp types
            //percentage *= (float)Config.Instance.EXPRate / 100f;
            //Weapon weapon = WeaponFactory.GetActiveWeapon(targetPC);
            ulong baseExp = (ulong)(targetNPC.BaseData.baseExp * percentage * Configuration.Instance.CalcCEXPRateForPC(targetPC));
            ulong jobExp = (ulong)(targetNPC.BaseData.jobExp * percentage * Configuration.Instance.CalcJEXPRateForPC(targetPC));

            ApplyExp(targetPC, baseExp, jobExp, false);

            foreach (ActorPC i in targetPC.PossesionedActors)
            {
                if (i == targetPC)
                    continue;
                baseExp = (ulong)(baseExp*GetPossesionLevelDiffExpFactor(i, targetPC));
                jobExp = (ulong)(baseExp*GetPossesionLevelDiffExpFactor(i, targetPC));
                ApplyExp(i, baseExp, jobExp, true);
            }
        }

        /// <summary>
        /// Check whether input clients experience at the input level type has reached beyond it's current level or not.
        /// If it has, process the new level (update database and inform client), if not, proceed as nothing happened.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="type"></param>
        public void CheckLvUp(MapClient client, LevelType type, byte when = 1)
        {
            if (client == null)
                return;
            if (client.state != MapClient.SESSION_STATE.LOADED && when == 1)
                return;
            if (client.state != MapClient.SESSION_STATE.LOADING && when == 0)
                return;
            uint lvlDelta = 0;
            switch (type)
            {
                case LevelType.CLEVEL:
                    lvlDelta = this.GetLevelDelta(client.Character.Level, client.Character.CEXP, LevelType.CLEVEL, true);
                    if (lvlDelta > 0)
                        this.SendLevelUp(client, type, lvlDelta);
                    break;
                case LevelType.JLEVEL:
                    lvlDelta = this.GetLevelDelta(client.Character.JobLevel1, client.Character.JEXP, LevelType.JLEVEL, true);
                    if (lvlDelta > 0)
                        this.SendLevelUp(client, type, lvlDelta);
                    break;
                case LevelType.JLEVEL2:
                    if (client.Character.JobJoint == PC_JOB.NONE)
                    {
                        if (client.Character.Job == client.Character.Job2X)
                        {
                            lvlDelta = this.GetLevelDelta(client.Character.JobLevel2X, client.Character.JEXP, LevelType.JLEVEL2, true);
                            if (lvlDelta > 0)
                                this.SendLevelUp(client, type, lvlDelta);
                        }
                        else
                        {
                            lvlDelta = this.GetLevelDelta(client.Character.JobLevel2T, client.Character.JEXP, LevelType.JLEVEL2, true);
                            if (lvlDelta > 0)
                                this.SendLevelUp(client, LevelType.JLEVEL2T, lvlDelta);
                        }
                    }
                    else
                    {
                        lvlDelta = this.GetLevelDelta(client.Character.JointJobLevel, client.Character.JointJEXP, LevelType.JLEVEL2, true);
                        if (lvlDelta > 0)
                            this.SendLevelUp(client, type, lvlDelta);

                    }
                    break;
                case LevelType.CLEVEL2:
                    lvlDelta = this.GetLevelDelta(client.Character.Level, client.Character.CEXP, LevelType.CLEVEL2, true);
                    if (lvlDelta > 0)
                        this.SendLevelUp(client, type, lvlDelta);
                    break;
                case LevelType.JLEVEL3:
                    lvlDelta = this.GetLevelDelta(client.Character.JobLevel3, client.Character.JEXP, LevelType.JLEVEL3, true);
                    if (lvlDelta > 0)
                        this.SendLevelUp(client, type, lvlDelta);
                    break;
            }


        }

        /// <summary>
        /// 处理怪物死亡的经验分配
        /// </summary>
        /// <param name="mob">怪物</param>
        public void ProcessMobExp(ActorMob mob)
        {
            ActorEventHandlers.MobEventHandler eh = (SagaMap.ActorEventHandlers.MobEventHandler)mob.e;
            Dictionary<uint, int> damagetable = eh.AI.DamageTable;
            Dictionary<uint, int> damageParty = new Dictionary<uint, int>();
            uint maxHP = mob.MaxHP;
            //处理伤害表
            foreach (uint i in damagetable.Keys)
            {
                Actor actor = eh.AI.map.GetActor(i);
                if (actor == null)
                    continue;
                if (actor.type != ActorType.PC)
                    continue;
                ActorPC pc = (ActorPC)actor;
                if (!pc.Online)
                    continue;
                if (pc.Party == null)//如果不属于任何团队
                {
                    int lvDelta = Math.Abs(pc.Level - mob.Level);
                    if (!pc.Buff.Dead)
                        ApplyMobExp(pc, mob, ((float)CalcPlayerLevelFactor(pc.Level) * (damagetable[i]) / maxHP) * CalcLevelDiffReduction(lvDelta));
                }
                else
                {
                    //计算团队总伤害
                    if (damageParty.ContainsKey(pc.Party.ID))
                        damageParty[pc.Party.ID] += damagetable[i];
                    else
                        damageParty.Add(pc.Party.ID, damagetable[i]);
                    if (damageParty[pc.Party.ID] > mob.MaxHP)
                        damageParty[pc.Party.ID] = (int)mob.MaxHP;
                }
            }

            //处理团队经验分配
            foreach (uint i in damageParty.Keys)
            {
                Party party = PartyManager.Instance.GetParty(i);
                List<ActorPC> validPC = new List<ActorPC>();
                //计算有效成员
                foreach (ActorPC pc in party.Members.Values)
                {
                    if (!pc.Online || pc.Buff.Dead)
                        continue;
                    if (pc.MapID != mob.MapID)
                        continue;
                    if (Math.Abs(mob.X - pc.X) > 1200 || Math.Abs(mob.Y - pc.Y) > 1200)
                        continue;
                    validPC.Add(pc);
                }
                if (validPC.Count == 0)
                    continue;
                float bonus;
                //计算成员人数经验总量
                if (validPC.Count > 1)
                    bonus = 1.2f + (0.3f * validPC.Count);
                else
                    bonus = 1f;
                //计算团队伤害比重
                bonus = bonus * ((float)(damageParty[i]) / maxHP);
                //队伍等级差计算
                uint maxlv = 0;
                uint minlv = 150;
                uint totallevel = 0;
                foreach (ActorPC pc in validPC)
                {
                    if (pc.Level > maxlv)
                        maxlv = pc.Level;
                    if (pc.Level < minlv)
                        minlv = pc.Level;
                    totallevel += pc.Level;
                }
                //计算团队经验分配
                uint difference = maxlv - minlv;
                if (difference < 10)
                    bonus = bonus * 1f;
                else if (difference >= 10 && difference < 20)
                    bonus = bonus * 0.9f;
                else if (difference >= 20 && difference < 30)
                    bonus = bonus * 0.5f;
                else if (difference >= 30 && difference < 40)
                    bonus = bonus * 0.2f;
                else if (difference >= 40 && difference < 50)
                    bonus = bonus * 0.1f;
                else if (difference >= 50)
                    bonus = bonus * 0.01f;
                //现在经验奖励不再除以队伍人数了
                //bonus = bonus / validPC.Count;
                //分配经验
                foreach (ActorPC pc in validPC)
                {
                    int lvDelta = Math.Abs(pc.Level - mob.Level);
                    //if (eh.AI.map.Info.Flag.Test(SagaDB.Map.MapFlags.Dominion))
                    //    ApplyExp(pc, mob, CalcPlayerLevelFactor(pc.Level) * bonus * CalcLevelDiffReduction(lvDelta) * ((float)pc.Level / (float)totallevel));
                    //else
                    ApplyMobExp(pc, mob, CalcPlayerLevelFactor(pc.Level) * bonus * CalcLevelDiffReduction(lvDelta) * ((float)pc.Level / (float)totallevel));
                }
            }
        }

        public void DeathPenalty(ActorPC pc)
        {
            Map map = MapManager.Instance.GetMap(pc.MapID);
            MapClient client = MapClient.FromActorPC(pc);
            ulong shouldBase = 0;
            ulong shouldJob = 0;

            if (pc.Race == PC_RACE.DEM)
            {
                pc.CEXP -= (ulong)(pc.CEXP * Configuration.Instance.DeathPenaltyBaseEmil);
                pc.JEXP -= (ulong)(pc.JEXP * Configuration.Instance.DeathPenaltyJobEmil);
                return;
            }
            if (pc.Job == pc.JobBasic)
                shouldJob = (ulong)((GetExpForLevel((uint)(pc.JobLevel1 + 1), LevelType.JLEVEL) - GetExpForLevel(pc.JobLevel1, LevelType.JLEVEL)) * Configuration.Instance.DeathPenaltyJobEmil);
            else if (pc.Job == pc.Job2X)
                shouldJob = (ulong)((GetExpForLevel((uint)(pc.JobLevel2X + 1), LevelType.JLEVEL2) - GetExpForLevel(pc.JobLevel2X, LevelType.JLEVEL2)) * Configuration.Instance.DeathPenaltyJobEmil);
            else if (pc.Job == pc.Job2T)
                shouldJob = (ulong)((GetExpForLevel((uint)(pc.JobLevel2T + 1), LevelType.JLEVEL2) - GetExpForLevel(pc.JobLevel2T, LevelType.JLEVEL2)) * Configuration.Instance.DeathPenaltyJobEmil);
            else
                shouldJob = (ulong)((GetExpForLevel((uint)(pc.JobLevel3 + 1), LevelType.JLEVEL3) - GetExpForLevel(pc.JobLevel3, LevelType.JLEVEL3)) * Configuration.Instance.DeathPenaltyJobEmil);

            if (!pc.Rebirth)
                shouldBase = (ulong)((GetExpForLevel((uint)(pc.Level + 1), LevelType.CLEVEL) - GetExpForLevel(pc.Level, LevelType.CLEVEL)) * Configuration.Instance.DeathPenaltyBaseEmil);
            else
                shouldBase = (ulong)((GetExpForLevel((uint)(pc.Level + 1), LevelType.CLEVEL2) - GetExpForLevel(pc.Level, LevelType.CLEVEL2)) * Configuration.Instance.DeathPenaltyBaseEmil);

            if (pc.CEXP > shouldBase)
                pc.CEXP -= shouldBase;
            else
                pc.CEXP = 0;
            if (pc.JEXP > shouldJob)
                pc.JEXP -= shouldJob;
            else
                pc.JEXP = 0;
            if (!pc.Rebirth)
            {
                if (pc.CEXP < GetExpForLevel(pc.Level, LevelType.CLEVEL))
                    pc.CEXP = GetExpForLevel(pc.Level, LevelType.CLEVEL);
            }
            else if (pc.CEXP < GetExpForLevel(pc.Level, LevelType.CLEVEL2))
                pc.CEXP = GetExpForLevel(pc.Level, LevelType.CLEVEL2);


            if (pc.Job == pc.JobBasic)
            {
                if (pc.JEXP < GetExpForLevel(pc.JobLevel1, LevelType.JLEVEL))
                    pc.JEXP = GetExpForLevel(pc.JobLevel1, LevelType.JLEVEL);
            }
            else if (pc.Job == pc.Job2X)
            {
                if (pc.JEXP < GetExpForLevel(pc.JobLevel2X, LevelType.JLEVEL2))
                    pc.JEXP = GetExpForLevel(pc.JobLevel2X, LevelType.JLEVEL2);
            }
            else if (pc.Job == pc.Job2T)
            {
                if (pc.JEXP < GetExpForLevel(pc.JobLevel2T, LevelType.JLEVEL2))
                    pc.JEXP = GetExpForLevel(pc.JobLevel2T, LevelType.JLEVEL2);
            }
            else
            {
                if (pc.JEXP < GetExpForLevel(pc.JobLevel3, LevelType.JLEVEL3))
                    pc.JEXP = GetExpForLevel(pc.JobLevel3, LevelType.JLEVEL3);
            }

            client.SendEXP();
            client.SendPlayerLevel();
            if (map.Info.Flag.Test(SagaDB.Map.MapFlags.Dominion))
            {
                client.SendSystemMessage(LocalManager.Instance.Strings.DEATH_PENALTY);
            }
        }

        /// <summary>
        /// Get experience for the input level. 
        /// </summary>
        /// <remarks>
        /// This is the experience required to "finish" the level, not the experience required to Get the level.
        /// </remarks>
        /// <param name="level">The level to get the experience for</param>
        /// <param name="type">The level type to get the experience for</param>
        /// <returns>
        /// The experience required to finish the input level for the input level type. 
        /// If a non existing type or level is input, the method returns 0.
        /// </returns>
        public ulong GetExpForLevel(uint level, LevelType type)
        {
            if (this.Chart.ContainsKey(level))
            {
                Level levelData = this.Chart[level];
                switch (type)
                {
                    case LevelType.CLEVEL:
                        return levelData.cxp;
                    case LevelType.CLEVEL2:
                        return levelData.cxp2;
                    case LevelType.JLEVEL:
                        return levelData.jxp;
                    case LevelType.JLEVEL2:
                    case LevelType.JLEVEL2T:
                        return levelData.jxp2;
                    case LevelType.JLEVEL3:
                        return levelData.jexp3;
                    default:
                        return uint.MaxValue;
                }
            }
            else
            {
                return uint.MaxValue;
            }
        }

        public void ProcessWrp(ActorPC src, ActorPC dst)
        {
            int shouldWrp = 0;
            int srcLv = 1, dstLv = 1;
            srcLv = src.Level;
            dstLv = src.Level;
            if (dstLv > srcLv)
            {
                shouldWrp = (dstLv - srcLv) + 10;
            }
            else
                shouldWrp = 5;
            if (src.Party == null)
            {
                src.WRP += shouldWrp;
            }
            else
            {
                List<ActorPC> validPC = new List<ActorPC>();
                foreach (ActorPC pc in src.Party.Members.Values)
                {
                    if (!pc.Online || pc.Buff.Dead)
                        continue;
                    if (pc.MapID != dst.MapID)
                        continue;
                    if (Math.Abs(dst.X - pc.X) > 1200 || Math.Abs(dst.Y - pc.Y) > 1200)
                        continue;
                    validPC.Add(pc);
                }
                int value = shouldWrp / validPC.Count;
                if (value == 0)
                    value = 1;
                foreach (ActorPC pc in validPC)
                {
                    pc.WRP += value;
                }
            }
            int lostWrp = shouldWrp / 2;
            if (dst.WRP > lostWrp)
                dst.WRP -= lostWrp;
            else
                dst.WRP = 0;
            WRPRankingManager.Instance.UpdateRanking();
        }

        #endregion

        #region Private methods



        private void ApplyExp(ActorPC targetPC, ulong baseExp, ulong jobExp, bool possession)
        {
            if (targetPC.Level >= MaxCLevel || targetPC.Level >= MaxCLevel2)
                baseExp = 0;
            if (targetPC.CurrentJobLevel >= MaxJLevel)
                jobExp = 0;
            if (baseExp == 0 && jobExp == 0)
                return;

            ActorEventHandlers.PCEventHandler eh = (ActorEventHandlers.PCEventHandler)targetPC.e;
            if (!targetPC.Rebirth)
                {
                    if (targetPC.CEXP < GetExpForLevel(MaxCLevel, LevelType.CLEVEL))
                        targetPC.CEXP += (ulong)(baseExp);
                }
            else if (targetPC.CEXP < GetExpForLevel(MaxCLevel2, LevelType.CLEVEL2))
                targetPC.CEXP += (ulong)(baseExp);

            if (targetPC.JobJoint == PC_JOB.NONE)
            {
                if (targetPC.Job == targetPC.JobBasic)
                {
                    if (targetPC.JEXP < GetExpForLevel(MaxJLevel, LevelType.JLEVEL))
                        targetPC.JEXP += (ulong)(jobExp);
                }
                else if (!targetPC.Rebirth)
                {
                    if (targetPC.JEXP < GetExpForLevel(MaxJLevel, LevelType.JLEVEL2))
                        targetPC.JEXP += (ulong)(jobExp);
                }
                else if (targetPC.JEXP < GetExpForLevel(MaxJLevel3, LevelType.JLEVEL3))
                    targetPC.JEXP += (ulong)(jobExp);
            }
            else
            {
                targetPC.JointJEXP += (ulong)(jobExp);
                if (Configuration.Instance.Version >= SagaLib.Version.Saga10)
                {
                    if (targetPC.JointJEXP >= GetExpForLevel(51, LevelType.JLEVEL2))
                        targetPC.JointJEXP = GetExpForLevel(51, LevelType.JLEVEL2) - 1;
                }
                else if (targetPC.JointJEXP >= GetExpForLevel(31, LevelType.JLEVEL2))
                    targetPC.JointJEXP = GetExpForLevel(31, LevelType.JLEVEL2) - 1;
            }
            
            if (targetPC.Race != PC_RACE.DEM)
            {
                if (!targetPC.Rebirth)
                    CheckLvUp(eh.Client, LevelType.CLEVEL);
                else
                    CheckLvUp(eh.Client, LevelType.CLEVEL2);
                if (targetPC.JobJoint == PC_JOB.NONE)
                {
                    if (targetPC.Job == targetPC.JobBasic)
                        CheckLvUp(eh.Client, LevelType.JLEVEL);
                    else if (!targetPC.Rebirth)
                        CheckLvUp(eh.Client, LevelType.JLEVEL2);
                    else
                        CheckLvUp(eh.Client, LevelType.JLEVEL3);
               }
                else
                    CheckLvUp(eh.Client, LevelType.JLEVEL2);
            }

            if (eh.Client.state != MapClient.SESSION_STATE.DISCONNECTED)
            {
                eh.Client.SendEXP();
                if (possession)
                    eh.Client.SendSystemMessage(string.Format(LocalManager.Instance.Strings.POSSESSION_EXP, (uint)baseExp, (uint)jobExp));
                else
                    eh.Client.SendSystemMessage(string.Format(LocalManager.Instance.Strings.GET_EXP, (uint)baseExp, (uint)jobExp));
            }
        }

        float CalcPlayerLevelFactor(byte pclevel)
        {
            if (pclevel <= 20)
                return 2.0f;
            if (pclevel > 20 && pclevel <= 110)
                return 1.0f;
            return 0.1f;
        }

        float CalcLevelDiffReduction(int lvDelta)
        {
            if (lvDelta >= 10 && lvDelta < 30)
                return 0.75f;
            if (lvDelta >= 30 && lvDelta < 35)
                return 0.50f;
            if (lvDelta >= 35 && lvDelta < 40)
                return 0.25f;
            if (lvDelta >= 40)
                return 0.10f;
            return 1f;
        }

        private void checkCL(ActorPC pc)
        {
            if (pc.Race == PC_RACE.DEM)
            {
                byte shouldLv = (byte)((pc.CL - 9) / 4 + 1);
                if (shouldLv > pc.Level)
                    SendLevelUp(MapClient.FromActorPC(pc), LevelType.CLEVEL, (uint)(shouldLv - pc.Level));
            }
        }

        ulong checkExpGap(uint ori, uint add, byte maxLv, LevelType type)
        {
            ulong output = ori + add;
            if (output >= GetExpForLevel((uint)(maxLv + 1), type))
                output = GetExpForLevel((uint)(maxLv + 1), type) - 1;
            return output;
        }
        /// <summary>
        /// Send level up packet to client and update database
        /// </summary>
        /// <param name="client">The MapClient</param>
        /// <param name="type">The LevelType that gained level(s)</param>
        /// <param name="numLevels">The number of levels gained</param>
        private void SendLevelUp(MapClient client, LevelType type, uint numLevels)
        {
            byte lvtype = 0;
            if (client.Character.Race != PC_RACE.DEM)
            {
                if (type == LevelType.CLEVEL || type == LevelType.CLEVEL2)
                {
                    ushort stats = 0;
                    for (int i = 1; i <= numLevels; i++)
                    {
                        //stats += (ushort)((client.Character.Level + i) / 5 + 3);
                        //stats += (ushort)(Math.Ceiling((float)(client.Character.Level + i) / 3.0f) + 2);
                        stats += (ushort)(Math.Floor((float)(client.Character.Level) / 3.0f) + 3);
                    }
                    client.Character.StatsPoint += stats;
                    client.Character.Level += (byte)numLevels;
                    lvtype = 1;
                }
                else
                {
                    if (client.Character.JobJoint == PC_JOB.NONE)
                    {
                        if (type == LevelType.JLEVEL)
                        {
                            client.Character.JobLevel1 += (byte)numLevels;
                            client.Character.SkillPoint += (byte)numLevels;
                        }
                        else if (type == LevelType.JLEVEL2)
                        {
                            client.Character.JobLevel2X += (byte)numLevels;
                            client.Character.SkillPoint2X += (byte)numLevels;
                        }
                        else if (type == LevelType.JLEVEL2T)
                        {
                            client.Character.JobLevel2T += (byte)numLevels;
                            client.Character.SkillPoint2T += (byte)numLevels;
                        }
                        else if (type == LevelType.JLEVEL3)
                        {
                            client.Character.JobLevel3 += (byte)numLevels;
                            client.Character.SkillPoint3 += (byte)numLevels;
                        }
                    }
                    else
                    {
                        client.Character.JointJobLevel += (byte)numLevels;
                    }
                    lvtype = 2;
                }
            }
            SkillArg arg = new SkillArg();
            arg.x = lvtype;
            client.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.LEVEL_UP, arg, client.Character, true);
            PC.StatusFactory.Instance.CalcStatus(client.Character);
            client.Character.HP = client.Character.MaxHP;
            client.Character.MP = client.Character.MaxMP;
            client.Character.SP = client.Character.MaxSP;
            client.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, client.Character, false);
            client.SendPlayerInfo();
            client.SendRingMember();
            client.SendPartyInfo();
        }

        /// <summary>
        /// Get the number of levels the overflow of exp represents compared to the current level.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="exp"></param>
        /// <param name="type"></param>
        /// <param name="allowMultilevel"></param>
        private uint GetLevelDelta(uint level, ulong exp, LevelType type, bool allowMultilevel)
        {
            if (level <= this.GetTypeSpecificMaxLevel(type))
                this.currentMax = this.GetTypeSpecificMaxLevel(type) - level;	// Calculate maximum allowed levels to gain from current level
            else
                this.currentMax = 0;
            uint delta;
            for (delta = 0;

                (allowMultilevel ? true : delta < 2) &&					// Multilevel constraint
                delta < this.currentMax &&								// Max level constraint
                exp > this.GetExpForLevel(level + delta + 1, type);			// Walk the passed levels (note that GetExpForLevel() returns 0 if level is greater than max level, so it's vital that the max levels are synced with the exp chart)

                delta++) ;												// Increase level delta

            return delta;
        }

        /// <summary>
        /// Get the max level for the input LevelType
        /// </summary>
        /// <param name="type">The LevelType to get the max level for</param>
        /// <returns>The max level for the input LevelType</returns>
        private uint GetTypeSpecificMaxLevel(LevelType type)
        {
            switch (type)
            {
                case LevelType.CLEVEL: return MaxCLevel;
                case LevelType.JLEVEL: return MaxJLevel;
                case LevelType.JLEVEL2: return MaxJLevel;
                case LevelType.CLEVEL2: return MaxCLevel2;
                case LevelType.JLEVEL3: return MaxJLevel3;
                default:
                    return 0;
            }
        }

        private float GetPossesionLevelDiffExpFactor(Actor aPossesionor, Actor aPossesioned)
        {
            float result = 1.0f;
            int leveldiff = aPossesionor.Level - aPossesioned.Level;
            if (leveldiff > 100)
                return 0.6f;
            if (leveldiff > 90 && leveldiff <= 100)
                return 0.5f;
            if (leveldiff > 60 && leveldiff <= 90)
                return 0.4f;
            if (leveldiff > 30 && leveldiff <= 60)
                return 0.3f;
            if (leveldiff > -30 && leveldiff <= 30)
                return 0.1f;
            if (leveldiff >= -60 && leveldiff <= -30)
                return 0.005f;
            if (leveldiff >= -90 && leveldiff < -60)
                return 0.001f;
            if (leveldiff >= -99 && leveldiff < -90)
                return 0.0001f;
            if (leveldiff < -100)
                return 0.00001f;
            return result;
        }
        #endregion

    }
}
