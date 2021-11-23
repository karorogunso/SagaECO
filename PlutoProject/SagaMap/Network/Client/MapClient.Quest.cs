using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Net;
using System.Net.Sockets;

using SagaDB;
using SagaDB.Item;
using SagaDB.Actor;
using SagaDB.Npc;
using SagaDB.Quests;
using SagaLib;
using SagaMap;
using SagaMap.Manager;
using System.Globalization;

namespace SagaMap.Network.Client
{
    public partial class MapClient
    {
        public uint questID;

        public void OnDailyDungeonOpen()
        {
            //if(Character.MapID != 10054000)
            //{
            //    SendSystemMessage("当前区域无法打开每日地牢。");
            //    return;
            //}
            if(Character.AStr["每日地牢记录"] == DateTime.Now.ToString("yyyy-MM-dd"))
            {
                SendSystemMessage("你今天已经入场过了，请明天再来吧。");
                return;
            }
            if(Character.Party != null)
            {
                SendSystemMessage("请先退出队伍。");
                return;
            }
            Packets.Server.SSMG_DAILYDUNGEON_INFO p = new Packets.Server.SSMG_DAILYDUNGEON_INFO();
            p.RemainSecond = (uint)(86400 - (DateTime.Now.Hour * 3600) - (DateTime.Now.Minute * 60) - (DateTime.Now.Second));
            List<byte> ids = new List<byte>();
            ids.Add(0);
            p.IDs = ids;
            this.netIO.SendPacket(p);
        }

        public void OnDailyDungeonJoin(Packets.Client.CSMG_DAILYDUNGEON_JOIN p)
        {
            if (p.QID == 0)
                EventActivate(980000101);
        }

        public void OnQuestDetailRequest(Packets.Client.CSMG_QUEST_DETAIL_REQUEST p)
        {
            if (QuestFactory.Instance.Items.ContainsKey(p.QuestID))
            {
                QuestInfo quest = QuestFactory.Instance.Items[p.QuestID];
                uint map1 = 0, map2 = 0, map3 = 0;
                string name1 = " ", name2 = " ", name3 = " ";
                NPC npc1 = null, npc2 = null, npc3 = null;
                if (NPCFactory.Instance.Items.ContainsKey(quest.NPCSource))
                {
                    npc2 = NPCFactory.Instance.Items[quest.NPCSource];
                }
                if (NPCFactory.Instance.Items.ContainsKey(quest.NPCDestination))
                {
                    npc3 = NPCFactory.Instance.Items[quest.NPCDestination];
                }
                if (npc1 != null)
                {
                    map1 = npc1.MapID;
                    name1 = npc1.Name;
                }
                if (npc2 != null)
                {
                    map2 = npc2.MapID;
                    name2 = npc2.Name;
                }
                if (npc3 != null)
                {
                    map3 = npc3.MapID;
                    name3 = npc3.Name;
                }
                Packets.Server.SSMG_QUEST_DETAIL p2 = new SagaMap.Packets.Server.SSMG_QUEST_DETAIL();
                p2.SetDetail(quest.QuestType, quest.Name, map1, map2, map3, name1, name2, name3, quest.MapID1, quest.MapID2, quest.MapID3, quest.ObjectID1, quest.ObjectID2, quest.ObjectID3, (uint)quest.Count1, (uint)quest.Count2, (uint)quest.Count3, quest.TimeLimit, 0);
                this.netIO.SendPacket(p2);
            }
        }

        public void OnQuestSelect(Packets.Client.CSMG_QUEST_SELECT p)
        {
            this.questID = p.QuestID;
        }

        public void SendQuestInfo()
        {
            Quest quest = this.Character.Quest;
            uint map1 = 0, map2 = 0, map3 = 0;
            string name1 = " ", name2 = " ", name3 = " ";
            if (quest == null)
                return;
            Packets.Server.SSMG_QUEST_ACTIVATE p2 = new SagaMap.Packets.Server.SSMG_QUEST_ACTIVATE();
            NPC npc1 = null, npc2 = null, npc3 = null;
            npc1 = quest.NPC;
            if (npc1 == null && NPCFactory.Instance.Items.ContainsKey(currentEventID))
            {
                npc1 = NPCFactory.Instance.Items[currentEventID];
            }
            if (NPCFactory.Instance.Items.ContainsKey(quest.Detail.NPCSource))
            {
                npc2 = NPCFactory.Instance.Items[quest.Detail.NPCSource];
            }
            if (NPCFactory.Instance.Items.ContainsKey(quest.Detail.NPCDestination))
            {
                npc3 = NPCFactory.Instance.Items[quest.Detail.NPCDestination];
            }
            if (npc1 != null)
            {
                map1 = npc1.MapID;
                name1 = npc1.Name;
            }
            if (npc2 != null)
            {
                map2 = npc2.MapID;
                name2 = npc2.Name;
            }
            if (npc3 != null)
            {
                map3 = npc3.MapID;
                name3 = npc3.Name;
            }

            //p2.SetDetail(quest.QuestType, quest.Name, map1, map2, map3, name1, name2, name3, quest.Status, quest.Detail.MapID1, quest.Detail.MapID2, quest.Detail.MapID3, quest.Detail.ObjectID1, quest.Detail.ObjectID2, quest.Detail.ObjectID3, (uint)quest.Detail.Count1, (uint)quest.Detail.Count2, (uint)quest.Detail.Count3, quest.Detail.TimeLimit, 0);
            p2.SetDetail(quest.ID, currentEventID, quest.Detail.NPCSource, quest.Detail.NPCDestination, quest.Status, quest.Detail.MapID1, quest.Detail.MapID2, quest.Detail.MapID3, quest.Detail.ObjectID1, quest.Detail.ObjectID2, quest.Detail.ObjectID3, (uint)quest.Detail.Count1, (uint)quest.Detail.Count2, (uint)quest.Detail.Count3, quest.Detail.TimeLimit, 0, quest.Detail.EXP, 0, quest.Detail.JEXP, quest.Detail.Gold);
            string ss = p2.DumpData();
            this.netIO.SendPacket(p2);
        }

        public void SendQuestPoints()
        {
            Packets.Server.SSMG_QUEST_POINT p = new SagaMap.Packets.Server.SSMG_QUEST_POINT();
            if (this.Character.QuestNextResetTime > DateTime.Now)
            {
                p.ResetTime = (uint)(this.Character.QuestNextResetTime - DateTime.Now).TotalHours;
            }
            else
            {
                int hours = (int)(DateTime.Now - this.Character.QuestNextResetTime).TotalHours;
                if (hours > 24000)
                {
                    this.Character.QuestNextResetTime = DateTime.Now + new TimeSpan(0, Configuration.Instance.QuestUpdateTime, 0, 0);
                }
                else
                {
                    if (Character.Account.questNextTime <= Character.QuestNextResetTime)
                    {
                        this.Character.QuestRemaining += (ushort)(((hours / Configuration.Instance.QuestUpdateTime) + 1) * Configuration.Instance.QuestUpdateAmount);
                        if (this.Character.QuestRemaining > Configuration.Instance.QuestPointsMax)
                            this.Character.QuestRemaining = (ushort)Configuration.Instance.QuestPointsMax;
                        this.Character.QuestNextResetTime = this.Character.QuestNextResetTime + new TimeSpan(0, ((hours / Configuration.Instance.QuestUpdateTime) + 1) * Configuration.Instance.QuestUpdateTime, 0, 0);
                        Character.Account.questNextTime = Character.QuestNextResetTime;
                    }
                    else
                        Character.QuestNextResetTime = Character.Account.questNextTime;
                }
                p.ResetTime = (uint)(this.Character.QuestNextResetTime - DateTime.Now).TotalHours;
            }
            p.QuestPoint = this.Character.QuestRemaining;
            this.netIO.SendPacket(p);

        }

        public void SendQuestCount()
        {
            if (this.Character.Quest != null)
            {
                Packets.Server.SSMG_QUEST_COUNT_UPDATE p = new SagaMap.Packets.Server.SSMG_QUEST_COUNT_UPDATE();
                p.Count1 = this.Character.Quest.CurrentCount1;
                p.Count2 = this.Character.Quest.CurrentCount2;
                p.Count3 = this.Character.Quest.CurrentCount3;
                this.netIO.SendPacket(p);
                if (this.Character.Quest.Status != QuestStatus.FAILED)
                {
                    if (this.Character.Quest.CurrentCount1 == this.Character.Quest.Detail.Count1 &&
                        this.Character.Quest.CurrentCount2 == this.Character.Quest.Detail.Count2 &&
                        this.Character.Quest.CurrentCount3 == this.Character.Quest.Detail.Count3 && this.Character.Quest.QuestType != QuestType.TRANSPORT)
                    {
                        this.Character.Quest.Status = QuestStatus.COMPLETED;
                        this.SendQuestStatus();
                    }
                }
            }
        }

        public void SendQuestTime()
        {
            if (this.Character.Quest != null)
            {
                Packets.Server.SSMG_QUEST_RESTTIME_UPDATE p = new SagaMap.Packets.Server.SSMG_QUEST_RESTTIME_UPDATE();
                if (this.Character.Quest.EndTime > DateTime.Now)
                {
                    p.RestTime = (int)(this.Character.Quest.EndTime - DateTime.Now).TotalMinutes;
                }
                else
                {
                    if (this.Character.Quest.Status != QuestStatus.COMPLETED)
                    {
                        this.Character.Quest.Status = QuestStatus.FAILED;
                        this.SendQuestStatus();
                    }
                }
                this.netIO.SendPacket(p);
            }
        }

        public void SendQuestStatus()
        {
            if (this.Character.Quest != null)
            {
                Packets.Server.SSMG_QUEST_STATUS_UPDATE p = new SagaMap.Packets.Server.SSMG_QUEST_STATUS_UPDATE();
                p.Status = this.Character.Quest.Status;
                this.netIO.SendPacket(p);
            }
        }

        public void SendQuestList(List<QuestInfo> quests)
        {
            Packets.Server.SSMG_QUEST_LIST p = new SagaMap.Packets.Server.SSMG_QUEST_LIST();
            p.Quests = quests;
            this.netIO.SendPacket(p);
        }

        public void SendQuestWindow()
        {
            Packets.Server.SSMG_QUEST_WINDOW p = new SagaMap.Packets.Server.SSMG_QUEST_WINDOW();
            this.netIO.SendPacket(p);
        }

        public void SendQuestDelete()
        {
            Packets.Server.SSMG_QUEST_DELETE p = new SagaMap.Packets.Server.SSMG_QUEST_DELETE();
            this.netIO.SendPacket(p);
        }

        public void QuestMobKilled(ActorMob mob, bool party)
        {
            if (this.Character.Quest != null)
            {
                if (this.Character.Quest.QuestType == SagaDB.Quests.QuestType.HUNT)
                {
                    if (party && !this.Character.Quest.Detail.Party)
                        return;
                    if (mob.MapID == this.Character.Quest.Detail.MapID1 ||
                        mob.MapID == this.Character.Quest.Detail.MapID2 ||
                        mob.MapID == this.Character.Quest.Detail.MapID3 ||
                        (this.Character.Quest.Detail.MapID1 == 0 && this.Character.Quest.Detail.MapID2 == 0 && this.Character.Quest.Detail.MapID3 == 0) ||
                        (this.Character.Quest.Detail.MapID1 == 60000000 && this.map.IsDungeon) ||
                        (this.Character.Quest.Detail.MapID1 == (this.map.ID / 1000 * 1000) && this.map.IsMapInstance) ||
                        (this.Character.Quest.Detail.MapID2 == (this.map.ID / 1000 * 1000) && this.map.IsMapInstance) ||
                        (this.Character.Quest.Detail.MapID3 == (this.map.ID / 1000 * 1000) && this.map.IsMapInstance))
                    {
                        if (this.Character.Quest.Detail.ObjectID1 == mob.MobID)
                            this.Character.Quest.CurrentCount1++;
                        if (this.Character.Quest.Detail.ObjectID1 == 0 && this.Character.Quest.Detail.Count1 != 0)
                            this.Character.Quest.CurrentCount1++;

                        if (this.Character.Quest.Detail.ObjectID2 == mob.MobID)
                            this.Character.Quest.CurrentCount2++;
                        if (this.Character.Quest.Detail.ObjectID2 == 0 && this.Character.Quest.Detail.Count2 != 0)
                            this.Character.Quest.CurrentCount2++;

                        if (this.Character.Quest.Detail.ObjectID3 == mob.MobID)
                            this.Character.Quest.CurrentCount3++;
                        if (this.Character.Quest.Detail.ObjectID3 == 0 && this.Character.Quest.Detail.Count3 != 0)
                            this.Character.Quest.CurrentCount3++;

                        if (this.Character.Quest.CurrentCount1 > this.Character.Quest.Detail.Count1)
                            this.Character.Quest.CurrentCount1 = this.Character.Quest.Detail.Count1;
                        if (this.Character.Quest.CurrentCount2 > this.Character.Quest.Detail.Count2)
                            this.Character.Quest.CurrentCount2 = this.Character.Quest.Detail.Count2;
                        if (this.Character.Quest.CurrentCount3 > this.Character.Quest.Detail.Count3)
                            this.Character.Quest.CurrentCount3 = this.Character.Quest.Detail.Count3;
                        this.SendQuestCount();
                    }
                }
            }
        }
        public void EventMobKilled(ActorMob mob)
        {
            uint MobId = mob.MobID;
            foreach (KeyValuePair<uint, ActorPC.KillInfo> i in this.Character.KillList)
            {
                if (!i.Value.isFinish)
                {
                    if (i.Key == MobId)
                    {
                        i.Value.Count++;
                        this.SendSystemMessage("击杀任务：已击杀 " + mob.BaseData.name + " (" + i.Value.Count.ToString() + "/" + i.Value.TotalCount.ToString() + ")");
                        if (i.Value.Count == i.Value.TotalCount)
                            this.SendSystemMessage("击杀任务：击杀 " + mob.BaseData.name + " 已完成！");
                    }
                    if (i.Value.Count >= i.Value.TotalCount)
                        i.Value.isFinish = true;
                }
            }
        }
    }
}
