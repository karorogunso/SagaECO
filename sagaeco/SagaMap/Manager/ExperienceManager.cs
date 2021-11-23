using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Xml;
using SagaDB.Actor;
using SagaDB.Item;
using SagaDB.Partner;
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
 
		private Dictionary<uint, Level> PCEXPChart = new Dictionary<uint, Level>();
        /// <summary>
        /// Cummulative experience for partner level, level starts at 1
        /// </summary>
        private Dictionary<byte, ulong> PartnerLvEXPChart = new Dictionary<byte, ulong>();
        /// <summary>
        /// Cummulative experience for partner rank, rank starts at 1
        /// </summary>
        private Dictionary<byte, ulong> PartnerRankEXPChart = new Dictionary<byte, ulong>();
        /// <summary>
        /// Cummulative experience for partner reliability color, color starts at 0
        /// </summary>
        public Dictionary<byte, ulong> PartnerReliabilityEXPChart = new Dictionary<byte, ulong>()
            {
               {0,0},
               {1,55},
               {2,672},
               {3,2698},
               {4,36906},
               {5,95023},
               {6,280980},
               {7,552980},
               {8,1345980},
               {9,2251980},
            };

        private byte PartnerLvMax = 30;
        private byte PartnerRankMax = 100;
        private byte PartnerReliabilityMax = 9;
		// Fields of local use only, declared here for memory optimization
        private uint currentMax;

		#endregion

        #region Public fields
        public uint MaxCLevel = 100;
        public readonly uint MaxJLevel = 60;
        public uint MaxCLevel2 = 100;
        public readonly uint MaxJLevel3 = 60;
        public readonly uint LastTimeLevelLimit = 100;
		#endregion

        #region Enums/Structs
       
        public struct Level
        {
            public readonly ulong cxp, jxp, jxp2, cxp2;
            public Level(ulong cxp, ulong jxp, ulong jxp2, ulong cxp2)
            {
                this.cxp = cxp;
                this.jxp = jxp;
                this.jxp2 = jxp2;
                this.cxp2 = cxp2;
            }
        }
        #endregion

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
                            ulong cexp = 0, cexp2 = 0, jexp = 0, jexp2 = 0;
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
                                    case "c2":
                                        cexp2 = ulong.Parse(k.InnerText);
                                        break;
                                }
                            }
                            Level newLv = new Level(cexp, jexp, jexp2, cexp2);
                            PCEXPChart.Add(lv, newLv);
                            PartnerLvEXPChart.Add(lv, cexp2);
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

        #region Public methods
        
        ulong checkExpGap(uint ori, uint add, byte maxLv, LevelType type)
        {
            ulong output = ori + add;
            if (output >= GetExpForLevel((uint)(maxLv + 1), type))
                output = GetExpForLevel((uint)(maxLv + 1), type) - 1;
            return output;
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

        public short GetEPRequired(ActorPC pc)
        {
            if (pc.Race == PC_RACE.DEM)
            {
                return GetEPRequired(pc.CL, false);
            }
            return 0;
        }

        public short GetEPRequired(short cl,bool dominion)
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
                ApplyCL(pc, cl);*///改革！
        }

        public void ApplyCL(ActorPC pc, short count)
        {
            if (pc.Race == PC_RACE.DEM)
            {
                pc.CL += count;
                checkCL(pc);
            }
        }

        /// <summary>
        /// Apply input percentage of experience from input targetNPC to input targetPC.
        /// The percentage gets capped at 1f and are multiplied by global EXP rate(s).
        /// </summary>
        /// <param name="targetPC">The target PC (the player)</param>
        /// <param name="percentage">The percentage of the NPC's exp to gain (for instance: the percentage of HP deducted by input player)</param>
        public void ApplyExp(ActorPC targetPC, uint exp, uint jexp, float percentage)
        {
            ApplyExp(targetPC, exp, jexp, percentage, 0);
        }
        public void ApplyExp(ActorPC targetPC, uint exp, uint jexp, float percentage, uint type)
        {
            ApplyExp(targetPC, exp, jexp, percentage, type, false);
        }
        public void ApplyExp(ActorPC targetPC, uint exp, uint jexp, float percentage, uint type, bool changeless)
        {
            ApplyPartnerLvExp(targetPC, (ulong)exp);
            if (targetPC.JEXP > 1844674407370)
                targetPC.JEXP = 0;
            //targetPC.AInt["本周累积经验"] += (int)exp;
            if (targetPC.Level >= MaxCLevel || targetPC.Level >= MaxCLevel2)
            {
                exp = 0;
            }
            if (targetPC.JobLevel3 >= MaxJLevel)
                jexp = 0;
            if (!changeless)

            percentage = percentage * Configuration.Instance.CalcQuestRateForPC(targetPC);
            percentage = 1f;
            /*
            if (targetPC.Level < LastTimeLevelLimit)
                percentage = percentage * 1.5f;
             * */

            float realexp;
            float realjobexp;
            /*if (type == 0)
            {
                realexp = (float)(exp) / 100 * percentage * (GetExpForLevel((uint)(targetPC.Level + 1), LevelType.CLEVEL) - GetExpForLevel((uint)(targetPC.Level), LevelType.CLEVEL));
                if (targetPC.Job == targetPC.JobBasic)
                {
                    realjobexp = (float)(jexp) / 100 * percentage * (GetExpForLevel((uint)(targetPC.JobLevel1 + 1), LevelType.JLEVEL) - GetExpForLevel((uint)(targetPC.JobLevel1), LevelType.JLEVEL));
                }
                else
                {
                    if (targetPC.Job == targetPC.Job2X)
                    {
                        realjobexp = (float)(jexp) / 100 * percentage * (GetExpForLevel((uint)(targetPC.JobLevel2X + 1), LevelType.JLEVEL2) - GetExpForLevel((uint)(targetPC.JobLevel2X), LevelType.JLEVEL2));
                    }
                    else if (targetPC.Job == targetPC.Job2T)
                    {
                        realjobexp = (float)(jexp) / 100 * percentage * (GetExpForLevel((uint)(targetPC.JobLevel2T + 1), LevelType.JLEVEL2T) - GetExpForLevel((uint)(targetPC.JobLevel2T), LevelType.JLEVEL2T));
                    }
                    else
                    {
                        realjobexp = (float)(jexp) / 100 * percentage * (GetExpForLevel((uint)(targetPC.JobLevel3 + 1), LevelType.JLEVEL3) - GetExpForLevel((uint)(targetPC.JobLevel3), LevelType.JLEVEL3));
                    }
                }
            }
            else*/
            {
                realexp = exp * percentage;
                realjobexp = jexp * percentage;             
            }
            ActorEventHandlers.PCEventHandler eh = (ActorEventHandlers.PCEventHandler)targetPC.e;
            string s = "";
            //s += " / 本周已累积经验：" + targetPC.AInt["本周累积经验"];
            if (targetPC.Level < 45)
            {
                realexp = (ulong)(realexp * 1.3f);
                realjobexp = (ulong)(realjobexp * 1.3f);
                s += " 45级以前经验值上升30%！";
            }
            eh.Client.SendSystemMessage(string.Format(LocalManager.Instance.Strings.GET_EXP, (uint)(realexp), (uint)(realjobexp)) + s);
            if (targetPC.JobLevel3 >= 20 || targetPC.JobLevel1 >= 20 || targetPC.JobLevel2T >= 20 || targetPC.JobLevel2X >= 20)
                realjobexp = (ulong)(realjobexp * 0.5f);
            if (targetPC.JobLevel3 >= 30 || targetPC.JobLevel1 >= 30 || targetPC.JobLevel2T >= 30 || targetPC.JobLevel2X >= 30)
                realjobexp = (ulong)(realjobexp * 0.3f);
            if (targetPC.JobLevel3 >= 40 || targetPC.JobLevel1 >= 40 || targetPC.JobLevel2T >= 40 || targetPC.JobLevel2X >= 40)
                realjobexp = (ulong)(realjobexp * 0.1f);
            if (targetPC.Level >= 60)
                realexp = (ulong)(realexp * 0.2f);

            if (targetPC.Level <= 45)
                realexp *= 10;
            else if (targetPC.Level <= 60)
                realexp *= 7;
            else if (targetPC.Level <= 65)
                realexp *= 5;
            else if (targetPC.Level <= 70)
                realexp *= 3;

            if (targetPC.JobLevel3 <= 35)
                realjobexp *= 8;
            else if (targetPC.JobLevel3 <= 40)
                realjobexp *= 5;
            else if (targetPC.JobLevel3 <= 50)
                realjobexp *= 2;

            if (realexp > 1000000000 || realjobexp > 1000000000)
            {
                realexp = 0;
                realjobexp = 0;
            }
           
            Map map = MapManager.Instance.GetMap(targetPC.MapID);
            if (!targetPC.Rebirth || targetPC.Job != targetPC.Job3)
            {
                if (targetPC.CEXP < GetExpForLevel(MaxCLevel, LevelType.CLEVEL))
                    targetPC.CEXP += (ulong)(realexp);
            }
            else
            {
                if (targetPC.CEXP < GetExpForLevel(MaxCLevel2, LevelType.CLEVEL2))
                    targetPC.CEXP += (ulong)(realexp);
            }
            if (targetPC.JobJoint == PC_JOB.NONE)
            {
                if (targetPC.Job == targetPC.JobBasic)
                {
                    if (targetPC.JEXP < GetExpForLevel(MaxJLevel, LevelType.JLEVEL))
                        targetPC.JEXP += (ulong)(realjobexp);
                }
                else if (!targetPC.Rebirth || targetPC.Job != targetPC.Job3)
                {
                    if (targetPC.JEXP < GetExpForLevel(MaxJLevel, LevelType.JLEVEL2))
                        targetPC.JEXP += (ulong)(realjobexp);
                }
                else
                {
                    if (targetPC.JEXP < GetExpForLevel(MaxJLevel3, LevelType.JLEVEL3))
                        targetPC.JEXP += (ulong)(realjobexp);
                }
            }
            else
            {
                targetPC.JointJEXP += (ulong)(realjobexp);
                if (targetPC.JointJEXP >= GetExpForLevel(50, LevelType.JLEVEL2))
                    targetPC.JointJEXP = GetExpForLevel(50, LevelType.JLEVEL2) - 1;
            }
            //checkEXP for level changes and max limits
            if (targetPC.Race != PC_RACE.DEM)
            {
                eh = (ActorEventHandlers.PCEventHandler)targetPC.e;
                if (!targetPC.Rebirth)
                {
                    CheckExp(eh.Client, LevelType.CLEVEL);
                }
                else
                {
                    CheckExp(eh.Client, LevelType.CLEVEL2);
                }
                if (targetPC.JobJoint == PC_JOB.NONE)
                {
                    if (targetPC.Job == targetPC.JobBasic)
                        CheckExp(eh.Client, LevelType.JLEVEL);
                    else if (!targetPC.Rebirth || targetPC.Job != targetPC.Job3)
                        CheckExp(eh.Client, LevelType.JLEVEL2);
                    else
                        CheckExp(eh.Client, LevelType.JLEVEL3);
                }
                else
                {
                    CheckExp(eh.Client, LevelType.JLEVEL2);
                }
            }
            
        }

		/// <summary>
		/// Apply input percentage of experience from input targetNPC to input targetPC.
		/// The percentage gets capped at 1f and are multiplied by global EXP rate(s).
		/// </summary>
		/// <param name="targetPC">The target PC (the player)</param>
		/// <param name="targetNPC">The target NPC (the "mob")</param>
		/// <param name="percentage">The percentage of the NPC's exp to gain (for instance: the percentage of HP deducted by input player)</param>
		public void ApplyExp(ActorPC targetPC, ActorMob targetNPC, float percentage)
		{			
            percentage = percentage * Configuration.Instance.CalcEXPRateForPC(targetPC);

            ActorEventHandlers.PCEventHandler eh = (ActorEventHandlers.PCEventHandler)targetPC.e;
            ulong baseExp;
            ulong jobExp;
            baseExp = (ulong)(targetNPC.baseExp * percentage);
            jobExp = (ulong)(targetNPC.jobExp * percentage);

            ApplyPartnerLvExp(targetPC, baseExp);

            if (targetPC.Level > targetNPC.Level)
            {
                int dif = targetPC.Level - targetNPC.Level;
                if (dif == 1) percentage = percentage * 0.9f;
                else if (dif == 2) percentage = percentage * 0.7f;
                else if (dif == 3) percentage = percentage * 0.5f;
                else if (dif == 4) percentage = percentage * 0.3f;
                else if (dif == 5) percentage = percentage * 0.1f;
                else percentage = percentage * 0.05f;
            }
            baseExp = (ulong)(targetNPC.baseExp * percentage);

            if (targetPC.Level >= MaxCLevel || targetPC.Level >= MaxCLevel2)
            {
                baseExp = 0; 
            }
            if (targetPC.JEXP > 1844674407370)
                targetPC.JEXP = 0;
            if(targetPC.TInt["PlayerExpUPRate"] > 0 && targetPC.Status.Additions.ContainsKey("PlayerExpUP"))
            {
                float r = targetPC.TInt["PlayerExpUPRate"] / 100f;
                baseExp += (ulong)(baseExp * r);
                jobExp += (ulong)(jobExp * r);
            }
            string s = "";
            //s += " / 本周已累积经验：" + targetPC.AInt["本周累积经验"];
            if (targetPC.Level < 70)
            {
                baseExp = (ulong)(baseExp * 1.3f);
                jobExp = (ulong)(jobExp * 1.3f);
                s += " 55级以前经验值上升30%！";
            }

            if (targetPC.Buff.経験値上昇 && targetPC.Status.Additions.ContainsKey("篝火效果"))
            {
                baseExp = (ulong)(baseExp * 1.3f);
                jobExp = (ulong)(jobExp * 1.3f);
                s += " 『篝火效果』经验值上升30%！";
            }

            if ((baseExp != 0 || jobExp != 0))
                eh.Client.SendSystemMessage(string.Format(LocalManager.Instance.Strings.GET_EXP, baseExp, jobExp) +s);
            if (targetPC.JobLevel3 >= 20 || targetPC.JobLevel1 >= 20 || targetPC.JobLevel2T >= 20 || targetPC.JobLevel2X >= 20)
                jobExp = (ulong)(jobExp * 0.7f);
            if (targetPC.JobLevel3 >= 30 || targetPC.JobLevel1 >= 30 || targetPC.JobLevel2T >= 30 || targetPC.JobLevel2X >= 30)
                jobExp = (ulong)(jobExp * 0.5f);
            if (targetPC.JobLevel3 >= 40 || targetPC.JobLevel1 >= 40 || targetPC.JobLevel2T >= 40 || targetPC.JobLevel2X >= 40)
                jobExp = (ulong)(jobExp * 0.3f);
            if (targetPC.Level >= 60)
                baseExp = (ulong)(baseExp * 0.2f);
            if (targetPC.JobLevel3 < 40 && targetPC.Status.expup_job40_iris > 0)//初心者光环
            {
                baseExp += (ulong)(baseExp * 0.05 * targetPC.Status.expup_job40_iris);
                jobExp += (ulong)(jobExp * 0.05 * targetPC.Status.expup_job40_iris);
            }



            if (targetPC.Level <= 45)
                baseExp *= 10;
            else if (targetPC.Level <= 60)
                baseExp *= 7;
            else if (targetPC.Level <= 65)
                baseExp *= 5;
            else if (targetPC.Level <= 70)
                baseExp *= 3;

            if (targetPC.JobLevel3 <= 35)
                jobExp *= 8;
            else if (targetPC.JobLevel3 <= 40)
                jobExp *= 5;
            else if (targetPC.JobLevel3 <= 50)
                jobExp *= 2;

            if (baseExp > 1000000000 || jobExp > 1000000000)
            {
                baseExp = 0;
                jobExp = 0;
            }
            Map map = MapManager.Instance.GetMap(targetPC.MapID);
            if (!targetPC.Rebirth || targetPC.Job != targetPC.Job3)
            {
                if (targetPC.CEXP < GetExpForLevel(MaxCLevel, LevelType.CLEVEL))
                    targetPC.CEXP += (ulong)(baseExp);
            }
            else
            {
                if (targetPC.CEXP < GetExpForLevel(MaxCLevel2, LevelType.CLEVEL2))
                    targetPC.CEXP += (ulong)(baseExp);
            }
            if (targetPC.JobJoint == PC_JOB.NONE)
            {
                if (targetPC.Job == targetPC.JobBasic)
                {
                    if (targetPC.JEXP < GetExpForLevel(MaxJLevel, LevelType.JLEVEL))
                        targetPC.JEXP += (ulong)(jobExp);
                }
                else if (!targetPC.Rebirth || targetPC.Job != targetPC.Job3)
                {
                    if (targetPC.JEXP < GetExpForLevel(MaxJLevel, LevelType.JLEVEL2))
                        targetPC.JEXP += (ulong)(jobExp);
                }
                else
                {
                    if (targetPC.JEXP < GetExpForLevel(MaxJLevel3, LevelType.JLEVEL3))
                        targetPC.JEXP += (ulong)(jobExp);
                }
            }
            else
            {
                targetPC.JointJEXP += (ulong)(jobExp);
                if (targetPC.JointJEXP >= GetExpForLevel(50, LevelType.JLEVEL2))
                    targetPC.JointJEXP = GetExpForLevel(50, LevelType.JLEVEL2) - 1;
            }
            eh = (ActorEventHandlers.PCEventHandler)targetPC.e;
            if (eh.Client.state != MapClient.SESSION_STATE.DISCONNECTED)
            {
                eh.Client.SendEXP();
            }
            if (targetPC.Race != PC_RACE.DEM)
            {
                if (!targetPC.Rebirth || targetPC.Job != targetPC.Job3)
                {
                    CheckExp(eh.Client, LevelType.CLEVEL);
                }
                else
                {
                    CheckExp(eh.Client, LevelType.CLEVEL2);
                }
                if (targetPC.JobJoint == PC_JOB.NONE)
                {
                    if (targetPC.Job == targetPC.JobBasic)
                        CheckExp(eh.Client, LevelType.JLEVEL);
                    else if (!targetPC.Rebirth || targetPC.Job != targetPC.Job3)
                        CheckExp(eh.Client, LevelType.JLEVEL2);
                    else
                        CheckExp(eh.Client, LevelType.JLEVEL3);
                }
                else
                    CheckExp(eh.Client, LevelType.JLEVEL2);
            }
            
		}

        /// <summary>
        /// Check whether input clients experience at the input level type has reached beyond it's current level or not.
        /// If it has, process the new level (update database and inform client), if not, proceed as nothing happened.
        /// </summary>
        /// <param name="client"></param>
        /// <param name="type"></param>
        public void CheckExp(MapClient client, LevelType type)
        {
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
            if (client.Character.Level >= 15)
                client.TitleProccess(client.Character, 48, 1);
            if (client.Character.Level >= 25)
                client.TitleProccess(client.Character, 49, 1);
            if (client.Character.Level >= 35)
                client.TitleProccess(client.Character, 50, 1);
            if (client.Character.Level >= 45)
                client.TitleProccess(client.Character, 51, 1);
            if (client.Character.Level >= 55)
                client.TitleProccess(client.Character, 52, 1);
            if (client.Character.Level >= 65)
                client.TitleProccess(client.Character, 53, 1);
            if (client.Character.Level >= 75)
                client.TitleProccess(client.Character, 54, 1);
        }

		/// <summary>
        /// Get ccumulative experience to get the input level. 
		/// </summary>
		/// <param name="level">The level to get the experience for</param>
		/// <param name="type">The level type to get the experience for</param>
		/// <returns> 
		/// If a non existing type or level is input, the method returns 0.
		/// </returns>
        public ulong GetExpForLevel(uint level, LevelType type)
        {
            if (level == 1) return 0;
            ulong exp = 0;
			if (this.PCEXPChart.ContainsKey(level))
            {
                Level levelData = this.PCEXPChart[level];
                switch (type)
                {
                    case LevelType.CLEVEL:
                        exp = levelData.cxp;
                        break;
                    case LevelType.CLEVEL2:
                        exp = levelData.cxp2;
                        break;
                    case LevelType.JLEVEL:
                        exp = levelData.jxp;
                        break;
                    case LevelType.JLEVEL2:
                    case LevelType.JLEVEL2T:
                    case LevelType.JLEVEL3:
                        exp = levelData.jxp2;
                        break;
                    default:
                        exp = ulong.MaxValue;
                        break;
                }
            }
            else
            {
                exp = ulong.MaxValue;
            }
            if (exp == 0) exp = ulong.MaxValue;
            return exp;
        }

        float CalcLevelDiffReduction(int lvDelta)
        {
            /*if (lvDelta >= 1 && lvDelta < 3)
                return 0.5f;
            if (lvDelta >= 3 && lvDelta < 5)
                return 0.3f;
            if (lvDelta >= 5 && lvDelta < 10)
                return 0.1f;
            if (lvDelta >= 10)
                return 0.01f;*/
            return 1f;
        }

        float CalcPlayerLevelFactor(byte pclevel)
        {
            return 1f;
        }
        /// <summary>
        /// 处理怪物死亡的经验分配
        /// </summary>
        /// <param name="mob">怪物</param>
        public void ProcessMobExp(ActorMob mob)
        {
            ActorEventHandlers.MobEventHandler eh = (SagaMap.ActorEventHandlers.MobEventHandler)mob.e;
            Dictionary<uint, int> damageParty = new Dictionary<uint, int>();
            uint maxHP = mob.MaxHP;
            List<Actor> actors = new List<Actor>();
            //处理伤害表
            foreach (uint i in eh.AI.DamageTable.Keys)
            {
                Actor actor = eh.AI.map.GetActor(i);
                if (actors.Contains(actor)) continue;
                if (actor == null) continue;
                if (actor.type != ActorType.PC) continue;
                ActorPC pc = (ActorPC)actor;
                if (pc.Party == null)//如果不属于任何团队
                {
                    int lvDelta = Math.Abs(pc.Level - mob.Level);
                    //if (!pc.Buff.Dead)
                    ApplyExp(pc, mob, ((float)CalcPlayerLevelFactor(pc.Level) * (eh.AI.DamageTable[i]) / maxHP) * CalcLevelDiffReduction(lvDelta));
                }
                else
                {
                    //计算团队总伤害
                    if (damageParty.ContainsKey(pc.Party.ID))
                        damageParty[pc.Party.ID] += eh.AI.DamageTable[i];
                    else
                        damageParty.Add(pc.Party.ID, eh.AI.DamageTable[i]);
                    if (damageParty[pc.Party.ID] > mob.MaxHP)
                        damageParty[pc.Party.ID] = (int)mob.MaxHP;
                }
                actors.Add(actor);
            }

            //处理团队经验分配
            foreach (uint i in damageParty.Keys)
            {
                Party party = PartyManager.Instance.GetParty(i);
                List<ActorPC> validPC = new List<ActorPC>();
                //计算有效成员
                if (party == null)
                    continue;
                foreach (ActorPC pc in party.Members.Values)
                {
                    if (!pc.Online) //|| pc.Buff.Dead)
                        continue;
                    if (pc.MapID != mob.MapID)
                        continue;
                    if (pc.Party != party)
                        continue;
                    //if (Math.Abs(mob.X - pc.X) > 1200 || Math.Abs(mob.Y - pc.Y) > 1200)
                        //continue;
                    validPC.Add(pc);
                }
                if (validPC.Count == 0)
                    continue;
                float bonus;
                //计算成员人数经验总量
                if (validPC.Count > 1)
                    bonus = 1f - (0.03f * validPC.Count);
                else
                    bonus = 1f;
                //计算团队伤害比重
                bonus = bonus * ((float)(damageParty[i]) / maxHP);


                uint maxlv = 0;
                uint minlv = 150;
                foreach (ActorPC pc in validPC)
                {
                    if (pc.Level > maxlv)
                        maxlv = pc.Level;
                    if (pc.Level < minlv)
                        minlv = pc.Level;
                }
                uint difference = maxlv - minlv;
                /*if (difference < 5)
                    bonus = bonus * 1f;
                else if (difference >= 5 && difference < 10)
                    bonus = bonus * 0.9f;
                else if (difference >= 10 && difference < 15)
                    bonus = bonus * 0.5f;
                else if (difference >= 15 && difference < 20)
                    bonus = bonus * 0.2f;
                else if (difference >= 20 && difference < 30)
                    bonus = bonus * 0.1f;
                else if (difference >= 30)
                    bonus = bonus * 0.01f;*/
                //分配经验
                foreach (ActorPC pc in validPC)
                {
                    int lvDelta = Math.Abs(pc.Level - mob.Level);
                    ApplyExp(pc, mob, CalcPlayerLevelFactor(pc.Level) * bonus * CalcLevelDiffReduction(lvDelta));
                }
            }
        }

        public void DeathPenalty(ActorPC pc)
        {
            MapClient client = MapClient.FromActorPC(pc);
            client.SendEXP();
            client.SendPlayerLevel();
            client.SendSystemMessage(LocalManager.Instance.Strings.DEATH_PENALTY);
        }

        public void ProcessWrp(ActorPC src, ActorPC dst)
        {
            int shouldWrp = 0;
            int srcLv = 1, dstLv = 1;
            Map map = MapManager.Instance.GetMap(dst.MapID);
            if (map.Info.Flag.Test(SagaDB.Map.MapFlags.Dominion))
            {
                srcLv = src.DominionLevel;
                dstLv = dst.DominionLevel;
            }
            else
            {
                srcLv = src.Level;
                dstLv = src.Level;
            }
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
        int getPartnerMaxLv(ActorPartner partner)
        {
            int maxlv = PartnerLvMax;
            int rank = partner.rank + 1;
            if (rank > 10) rank = 10;
            int i = 1;
            if (partner.rebirth)
                i = 2;
            switch (partner.BaseData.base_rank)
            {
                case 61://B
                    maxlv = PartnerLvMax + rank * i;
                    break;
                case 71://A
                    maxlv = PartnerLvMax + rank * 2 * i;
                    break;
                case 81://S
                    maxlv = PartnerLvMax + rank * 3 * i;
                    break;
                case 91://SS
                    maxlv = PartnerLvMax + rank * 4 * i;
                    break;
                case 101://SSS
                    maxlv = PartnerLvMax + rank * 5 * i;
                    break;
                default:
                    maxlv = PartnerLvMax + rank * i;
                    break;
            }
            if (partner.rebirth) maxlv += 10;
            return maxlv;
        }
        int getPartnerMaxLv(ActorPC PC)
        {
            ActorPartner partner = PC.Partner;
            return getPartnerMaxLv(partner);
        }
        public void ApplyPartnerLvExp(ActorPC PC, ulong exp)
        {
            if (!PC.Inventory.Equipments.ContainsKey(EnumEquipSlot.PET))
                return;
            if (!PC.Inventory.Equipments[EnumEquipSlot.PET].IsPartner)
                return;
            ActorPartner partner = PC.Partner;
            if (partner == null) return;
            if (PC.TInt["PartnerExpUPRate"] > 0 && PC.Status.Additions.ContainsKey("PartnerExpUP"))
            {
                float r = PC.TInt["PartnerExpUPRate"] / 100f;
                exp += (ulong)(exp * r);
            }
            int maxlv = getPartnerMaxLv(PC);
            exp /= 10;
            if (partner.Level >= maxlv) return;
            partner.exp += exp;
            byte lvup = GetPartnerSpecDelta(partner.Level, partner.exp, 0, (byte)maxlv);
            if (lvup > 0)
            {
                partner.perkpoint += lvup;

                partner.Level += lvup;
                Skill.SkillHandler.Instance.ShowEffectOnActor(partner, 9964);
                Partner.StatusFactory.Instance.CalcPartnerStatus(partner);
                partner.HP = partner.MaxHP;
                partner.MP = partner.MaxMP;
                partner.SP = partner.MaxSP;
                //broadcast status and hpmpsp
                MapServer.charDB.SavePartner(partner);
                MapClient.FromActorPC(PC).SendPetBasicInfo();
                MapClient.FromActorPC(PC).SendPetDetailInfo();

                MapClient.FromActorPC(PC).PartnerTalking(partner, MapClient.TALK_EVENT.LVUP, 100, 0);
            }
            PC.Inventory.Equipments[EnumEquipSlot.PET].PartnerLevel = partner.Level;
            MapClient.FromActorPC(PC).SendItemInfo(PC.Inventory.Equipments[EnumEquipSlot.PET]);
            MapClient.FromActorPC(PC).SendSystemMessage("伙伴 "+partner.Name + " 获得了" + exp + "点经验值");
        }

        public void ApplyPartnerFoodEXP(ActorPartner partner, uint foodrankexp)
        {
            if (partner.rank >= PartnerRankMax)
            {
                foodrankexp = 0;
            }
            /*partner.rankexp += foodrankexp;
            byte rankup = GetPartnerSpecDelta(partner.rank, partner.rankexp, 1);
            if (rankup > 0)
            {
                partner.rank += rankup;
                partner.perkpoint += rankup;
                Packets.Server.SSMG_PARTNER_RANK_UPDATE pr = new Packets.Server.SSMG_PARTNER_RANK_UPDATE();
                pr.PartnerInventorySlot = partner.Owner.Inventory.Equipments[EnumEquipSlot.PET].Slot;
                pr.PartnerRank = partner.rank;
                pr.PerkPoint = partner.perkpoint;
                ActorEventHandlers.PCEventHandler eh = (ActorEventHandlers.PCEventHandler)partner.Owner.e;
                eh.Client.netIO.SendPacket(pr);
            }*/
            int maxlv = getPartnerMaxLv(partner);
            partner.exp += foodrankexp;
            byte lvup = GetPartnerSpecDelta(partner.Level, partner.exp, 0, (byte)maxlv);
            if (lvup > 0)
            {
                partner.perkpoint += lvup;

                partner.Level += lvup;
                Skill.SkillHandler.Instance.ShowEffectOnActor(partner, 9964);
                Partner.StatusFactory.Instance.CalcPartnerStatus(partner);
                partner.HP = partner.MaxHP;
                partner.MP = partner.MaxMP;
                partner.SP = partner.MaxSP;
                //broadcast status and hpmpsp
                MapServer.charDB.SavePartner(partner);
            }
            MapServer.charDB.SavePartner(partner);
        }

        public void ApplyPartnerReliabilityEXP(ActorPartner partner, uint reliabilityexp)
        {
            if (partner.reliability >= PartnerReliabilityMax)
            {
                reliabilityexp = 0;
            }
            partner.reliabilityexp += reliabilityexp;
            byte reliabilityup = GetPartnerSpecDelta(partner.reliability, partner.reliabilityexp, 2, 9);
            if (reliabilityup > 0 && partner.reliability < 9)
            {
                partner.reliability += reliabilityup;
                Skill.SkillHandler.Instance.ShowEffectOnActor(partner, 9913);
            }
            MapServer.charDB.SavePartner(partner);
        }

        #endregion

		#region Private methods
		/// <summary>
		/// Send level up packet to client and update database
		/// </summary>
		/// <param name="client">The MapClient</param>
		/// <param name="type">The LevelType that gained level(s)</param>
		/// <param name="numLevels">The number of levels gained</param>
		private void SendLevelUp(MapClient client, LevelType type, uint numLevels)
		{
            byte lvtype = 0;
            if (type == LevelType.CLEVEL || type == LevelType.CLEVEL2)
            {
                ushort stats = 0;
                if (client.Character.Race != PC_RACE.DEM)
                {
                    for (int i = 0; i < numLevels; i++)
                    {
                        stats += 5;
                    }
                    if (!Configuration.Instance.AJIMode)
                        client.Character.StatsPoint += stats; //7月26日注释
                }
                client.Character.Level += (byte)numLevels;
                lvtype = 1;
            }
            else
            {
                if (client.Character.Race != PC_RACE.DEM)
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
                        else
                        {
                            client.Character.JobLevel3 += (byte)numLevels;
                            client.Character.SkillPoint3 += (byte)numLevels;
                            /*client.Character.SkillPoint += (byte)numLevels;
                            client.Character.SkillPoint2X += (byte)numLevels;
                            client.Character.SkillPoint2T += (byte)numLevels;*/
                        }
                    }
                    else
                    {
                        client.Character.JointJobLevel += (byte)numLevels;
                    }
                }
                lvtype = 2;
            }
            SkillArg arg = new SkillArg();
            arg.x = lvtype;

            client.PartnerTalking(client.Character.Partner, MapClient.TALK_EVENT.LVUP, 100, 0);

            client.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.LEVEL_UP, arg, client.Character, true);
            PC.StatusFactory.Instance.CalcStatus(client.Character);
            client.Character.HP = client.Character.MaxHP;
            client.Character.MP = client.Character.MaxMP;
            client.Character.SP = client.Character.MaxSP;
            client.Map.SendEventToAllActorsWhoCanSeeActor(Map.EVENT_TYPE.HPMPSP_UPDATE, null, client.Character, false);
            client.SendPlayerInfo();
            client.SendRingMember();
            client.SendPartyInfo();
            /*
            if (client.Character.Level >= SagaDB.LevelLimit.LevelLimit.Instance.NowLevelLimit)
                SagaMap.LevelLimit.LevelLimitManager.Instance.ReachLevelLimit(client.Character);
             * */
			Logger.ShowInfo(client.Character.Name + " gained " + numLevels + "x" + type.ToString(), null);

			//if (client.Party != null)
			//    client.Party.UpdateMemberInfo(client);
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
			switch(type)
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
        /// <summary>
        /// Get the change in partner lv, rank or reliability
        /// </summary>
        /// <param name="level"></param>
        /// <param name="exp"></param>
        /// <param name="type">0-lv, 1-rank, 2-reliability</param>
        /// <returns></returns>
        /// 
        private byte GetPartnerSpecDelta(byte level, ulong exp, byte type, byte maxlv = 30)
        {
            byte max = 0;
            Dictionary<byte, ulong> expchart = new Dictionary<byte, ulong>();

            switch (type)
            {
                case 0:
                    max = maxlv;
                    expchart = PartnerLvEXPChart;
                    break;
                case 1:
                    max = PartnerRankMax;
                    expchart = PartnerRankEXPChart;
                    break;
                case 2:
                    max = PartnerReliabilityMax;
                    expchart = PartnerReliabilityEXPChart;
                    break;
            }
            if (level <= max)
                this.currentMax = (uint)(max - level);	// Calculate maximum allowed levels to gain from current level
            else
                this.currentMax = 0;
            if(level >= 115)
                currentMax = 0;

            byte delta;
            for (delta = 0;delta < this.currentMax &&								// Max level constraint
                exp >= expchart[(byte)(level + delta + 1)];		       	// Walk the passed levels (note that GetExpForLevel() returns 0 if level is greater than max level, so it's vital that the max levels are synced with the exp chart)
                delta++) ;												// Increase level delta

            return delta;
        }

		#endregion

    }
}
