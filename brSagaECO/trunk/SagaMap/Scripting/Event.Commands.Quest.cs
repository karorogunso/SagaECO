using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaLib;
using SagaMap;
using SagaMap.Network.Client;
using SagaMap.Manager;
using SagaDB.Actor;
using SagaDB.Map;
using SagaDB.Item;
using SagaDB.Skill;
using SagaDB.Quests;

namespace SagaMap.Scripting
{
    public abstract partial class Event
    {
        /// <summary>
        /// 处理任务事件
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="groupID">NPC的任务组ID</param>
        protected void HandleQuest(ActorPC pc, uint groupID)
        {
            MapClient client = GetMapClient(pc);
            int ExcessItemID1, ExcessItemID2, ExcessItemID3, ExcessItemCount1, ExcessItemCount2, ExcessItemCount3;
            if (pc.Quest != null)
            {
                if (pc.Quest.Status == QuestStatus.OPEN)
                {
                    if (pc.Quest.QuestType == QuestType.GATHER && CountItem(pc, pc.Quest.Detail.ObjectID1) > 0)
                    {
                        if (pc.Quest.QuestType == QuestType.GATHER)
                            Say(pc, 131, this.transport);
                        List<SagaDB.Item.Item> item = NPCTrade(pc);
                        foreach (SagaDB.Item.Item i in item)
                        {
                            if (i.ItemID == pc.Quest.Detail.ObjectID1)
                                pc.Quest.CurrentCount1 += i.Stack;
                            if (pc.Quest.CurrentCount1 > pc.Quest.Detail.Count1)
                            {
                                ExcessItemCount1 = pc.Quest.CurrentCount1 - pc.Quest.Detail.Count1;
                                ExcessItemID1 = (int)pc.Quest.Detail.ObjectID1;
                                pc.Quest.CurrentCount1 = pc.Quest.Detail.Count1;
                                if (ExcessItemCount1 != 0)
                                    GiveItem(pc, (uint)ExcessItemID1, (ushort)ExcessItemCount1);
                            }
                            if (i.ItemID == pc.Quest.Detail.ObjectID2)
                                pc.Quest.CurrentCount2 += i.Stack;
                            if (pc.Quest.CurrentCount2 > pc.Quest.Detail.Count2)
                            {
                                ExcessItemCount2 = pc.Quest.CurrentCount2 - pc.Quest.Detail.Count2;
                                ExcessItemID2 = (int)pc.Quest.Detail.ObjectID2;
                                pc.Quest.CurrentCount2 = pc.Quest.Detail.Count2;
                                if (ExcessItemCount2 != 0)
                                    GiveItem(pc, (uint)ExcessItemID2, (ushort)ExcessItemCount2);
                            }
                            if (i.ItemID == pc.Quest.Detail.ObjectID3)
                                pc.Quest.CurrentCount3 += i.Stack;
                            if (pc.Quest.CurrentCount3 > pc.Quest.Detail.Count3)
                            {
                                ExcessItemCount3 = pc.Quest.CurrentCount3 - pc.Quest.Detail.Count3;
                                ExcessItemID3 = (int)pc.Quest.Detail.ObjectID3;
                                pc.Quest.CurrentCount3 = pc.Quest.Detail.Count3;
                                if (ExcessItemCount3 != 0)
                                    GiveItem(pc, (uint)ExcessItemID3, (ushort)ExcessItemCount3);
                            }
                        }
                        client.SendQuestCount();
                    }
                    else
                    {
                        Say(pc, 131, this.alreadyHasQuest);
                        if (Select(pc, LocalManager.Instance.Strings.QUEST_HOW_TO_DO, "",
                            LocalManager.Instance.Strings.QUEST_NOT_CANCEL,
                            LocalManager.Instance.Strings.QUEST_CANCEL) == 2)
                        {
                            if (pc.Quest.Detail.DungeonID != 0 && pc.DungeonID != 0)
                                Dungeon.DungeonFactory.Instance.GetDungeon(pc.DungeonID).Destory(SagaMap.Dungeon.DestroyType.QuestCancel);
                            Say(pc, 131, this.questCanceled);
                            Say(pc, 131, LocalManager.Instance.Strings.QUEST_CANCELED);
                            PlaySound(pc, 4007, false, 100, 50);
                            if (pc.Fame > 0) pc.Fame--;
                            pc.Quest.Status = QuestStatus.FAILED;
                            OnQuestUpdate(pc, pc.Quest);
                            pc.Quest = null;
                            client.SendQuestDelete();

                            return;
                        }
                    }
                }
                if (pc.Quest.Status == QuestStatus.COMPLETED)
                {
                    if (pc.Quest.Detail.DungeonID != 0 && pc.DungeonID != 0)
                        Dungeon.DungeonFactory.Instance.GetDungeon(pc.DungeonID).Destory(SagaMap.Dungeon.DestroyType.QuestCancel);

                    Say(pc, 131, this.questCompleted);
                    float expfactor = 1.0f;

                    Say(pc, 131, LocalManager.Instance.Strings.QUEST_REWARDED);
                    PlaySound(pc, 4006, false, 100, 50);
                    if (pc.Level < pc.Quest.Detail.MinLevel + 5)
                    {
                        expfactor = 1.0f;
                    }
                    else
                    {
                        if (pc.Level < pc.Quest.Detail.MinLevel + 10)
                        {
                            expfactor = 0.6f;
                        }
                        else
                        {
                            expfactor = 0.1f;
                        }
                    }
                    ExperienceManager.Instance.ApplyQuestExp(pc, pc.Quest.Detail.EXP, pc.Quest.Detail.JEXP, expfactor);
                    pc.Gold += (long)(pc.Quest.Detail.Gold * Configuration.Instance.CalcQuestGoldRateForPC(pc));
                    pc.CP += (uint)(pc.Quest.Detail.CP);

                    //任务特殊奖励
                    if (Configuration.Instance.ActivceQuestSpecialReward)
                        if (pc.Quest.Difficulty(pc) != QuestDifficulty.TOO_EASY)
                            if (Global.Random.Next(0, 10000) <= Configuration.Instance.QuestSpecialRewardRate)
                                GiveItem(pc, Configuration.Instance.QuestSpecialRewardID, (ushort)(pc.Quest.Detail.RequiredQuestPoint * 2));


                    //灰色任务无法获得声望?
                    //if (pc.Quest.Difficulty(pc) != QuestDifficulty.TOO_EASY)
                    pc.Fame += pc.Quest.Detail.Fame;
                    if (pc.Ring != null)
                    {
                        pc.Ring.Fame += pc.Quest.Detail.Fame;
                        RingManager.Instance.UpdateRingInfo(pc.Ring, SagaMap.Packets.Server.SSMG_RING_INFO.Reason.UPDATED);
                    }
                    if (pc.Quest.Detail.RewardItem != 0)
                    {
                        GiveItem(pc, pc.Quest.Detail.RewardItem, pc.Quest.Detail.RewardCount);
                    }
                    this.OnQuestUpdate(pc, pc.Quest);
                    pc.Quest = null;
                    client.SendQuestDelete();
                    client.SendPlayerInfo();
                }
                else if (pc.Quest.Status == QuestStatus.FAILED)
                {
                    if (pc.Quest.Detail.DungeonID != 0 && pc.DungeonID != 0)
                        Dungeon.DungeonFactory.Instance.GetDungeon(pc.DungeonID).Destory(SagaMap.Dungeon.DestroyType.QuestCancel);
                    Say(pc, 131, this.questFailed);
                    Say(pc, 131, LocalManager.Instance.Strings.QUEST_FAILED);
                    PlaySound(pc, 4007, false, 100, 50);
                    if (pc.Fame > 0) pc.Fame--;
                    this.OnQuestUpdate(pc, pc.Quest);
                    pc.Quest = null;
                    client.SendQuestDelete();
                }
            }
            else
            {
                if (pc.QuestRemaining < this.leastQuestPoint)
                {
                    Say(pc, 131, this.notEnoughQuestPoint);
                }
                else
                {
                    Quest quest = SendQuestList(pc, groupID);
                    if (quest != null)
                    {
                        if (pc.QuestRemaining < quest.Detail.RequiredQuestPoint)
                        {
                            Say(pc, 131, this.notEnoughQuestPoint);
                        }
                        else
                        {
                            QuestDifficulty difficulty = quest.Difficulty(pc);
                            if (difficulty == QuestDifficulty.TOO_EASY || difficulty == QuestDifficulty.TOO_HARD)
                            {
                                if (difficulty == QuestDifficulty.TOO_EASY)
                                    Say(pc, 131, this.questTooEasy);
                                else
                                    Say(pc, 131, this.questTooHard);
                                if (Select(pc, LocalManager.Instance.Strings.QUEST_IF_TAKE_QUEST, "",
                                    LocalManager.Instance.Strings.QUEST_TAKE, LocalManager.Instance.Strings.QUEST_NOT_TAKE) == 1)
                                {
                                    if (quest.QuestType != QuestType.GATHER)
                                        Say(pc, 131, this.gotNormalQuest);
                                    else
                                        Say(pc, 131, this.gotTransportQuest);
                                    QuestActivate(pc, quest);
                                    pc.QuestRemaining -= quest.Detail.RequiredQuestPoint;
                                    client.SendQuestPoints();
                                }
                            }
                            else
                            {
                                if (CanTakeQuest(pc, quest))
                                {
                                    if (quest.QuestType != QuestType.GATHER)
                                        Say(pc, 131, this.gotNormalQuest);
                                    else
                                        Say(pc, 131, this.gotTransportQuest);
                                    QuestActivate(pc, quest);
                                    pc.QuestRemaining -= quest.Detail.RequiredQuestPoint;
                                    client.SendQuestPoints();
                                    OnQuestUpdate(pc, quest);
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// 判断是否可以接此任务
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="quest">任务</param>
        public virtual bool CanTakeQuest(ActorPC pc, Quest quest)
        {
            return true;
        }

        /// <summary>
        /// 某任务状态改变后触发的事件
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="quest">任务</param>
        public virtual void OnQuestUpdate(ActorPC pc, Quest quest)
        {

        }



        /// <summary>
        /// 向玩家发送任务列表
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="groupID">任务组ID</param>
        /// <returns>玩家选择的任务</returns>
        Quest SendQuestList(ActorPC pc, uint groupID)
        {
            byte lv = 1;
            Map map = MapManager.Instance.GetMap(pc.MapID);
            lv = pc.Level;

            List<QuestInfo> list = QuestFactory.Instance.Items.Values.Where(x => x.GroupID == groupID && ((lv >= x.MinLevel && lv <= x.MaxLevel) || x.MinLevel == 255)
                && ((pc.Job == x.Job) || x.Job == PC_JOB.NONE)
                && ((pc.JobType == x.JobType) || x.JobType == JobType.NOVICE)
                && ((pc.Race == x.Race) || x.Race == PC_RACE.NONE)
                && ((pc.Gender == x.Gender) || x.Gender == PC_GENDER.NONE)).ToList();

            MapClient client = GetMapClient(pc);
            client.questID = 0;
            client.SendQuestList(list);

            ClientManager.LeaveCriticalArea();
            while (client.questID == 0)
            {
                System.Threading.Thread.Sleep(500);
            }
            ClientManager.EnterCriticalArea();

            if (QuestFactory.Instance.Items.ContainsKey(client.questID))
            {
                if (!list.Contains(QuestFactory.Instance.Items[client.questID]))
                {
                    return null;
                }
                else
                    return new Quest(client.questID);
            }
            else
                return null;
        }

        /// <summary>
        /// 激活任务
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="quest">任务</param>
        void QuestActivate(ActorPC pc, Quest quest)
        {
            quest.Status = QuestStatus.OPEN;
            if (quest.Detail.TimeLimit >= 0)
                quest.EndTime = DateTime.Now + new TimeSpan(0, quest.Detail.TimeLimit, 0);
            else
                quest.EndTime = new DateTime(9999, 1, 1);
            MapClient client = GetMapClient(pc);
            client.Character.Quest = quest;
            client.SendQuestInfo();
            client.SendQuestWindow();
        }
    }
}
