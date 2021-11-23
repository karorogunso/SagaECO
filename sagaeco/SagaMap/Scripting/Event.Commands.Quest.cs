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
            if (pc.Quest != null)
            {
                if (pc.Quest.Status == QuestStatus.OPEN)
                {
                    if (pc.Quest.QuestType == QuestType.GATHER)
                        Say(pc, 131, this.transport);
                    List<SagaDB.Item.Item> item = NPCTrade(pc);
                    List<SagaDB.Item.Item> itemreturn = new List<SagaDB.Item.Item>();
                    if (item.Count > 0)
                    {
                        foreach (SagaDB.Item.Item i in item)
                        {
                            List<uint> ids = new List<uint> { pc.Quest.Detail.ObjectID1, pc.Quest.Detail.ObjectID2, pc.Quest.Detail.ObjectID3 };
                            if (!ids.Contains(i.ItemID))
                                itemreturn.Add(i);
                            if (i.ItemID == pc.Quest.Detail.ObjectID1)
                            {
                                if (i.Stack + pc.Quest.CurrentCount1 > pc.Quest.Detail.Count1)
                                {
                                    i.Stack -= (ushort)(pc.Quest.Detail.Count1 - pc.Quest.CurrentCount1);
                                    pc.Quest.CurrentCount1 = pc.Quest.Detail.Count1;
                                    itemreturn.Add(i);
                                }
                                else
                                {
                                    pc.Quest.CurrentCount1 += i.Stack;
                                }
                            }
                            if (i.ItemID == pc.Quest.Detail.ObjectID2)
                            {
                                if (i.Stack + pc.Quest.CurrentCount2 > pc.Quest.Detail.Count2)
                                {
                                    i.Stack -= (ushort)(pc.Quest.Detail.Count2 - pc.Quest.CurrentCount2);
                                    pc.Quest.CurrentCount2 = pc.Quest.Detail.Count2;
                                    itemreturn.Add(i);
                                }
                                else
                                {
                                    pc.Quest.CurrentCount2 += i.Stack;
                                }
                            }
                            if (i.ItemID == pc.Quest.Detail.ObjectID3)
                            {
                                if (i.Stack + pc.Quest.CurrentCount3 > pc.Quest.Detail.Count3)
                                {
                                    i.Stack -= (ushort)(pc.Quest.Detail.Count3 - pc.Quest.CurrentCount3);
                                    pc.Quest.CurrentCount3 = pc.Quest.Detail.Count3;
                                    itemreturn.Add(i);
                                }
                                else
                                {
                                    pc.Quest.CurrentCount3 += i.Stack;
                                }
                            }
                        }

                        foreach (SagaDB.Item.Item i in itemreturn)
                        {
                            GiveItem(pc, i);
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
                    ExperienceManager.Instance.ApplyExp(pc, pc.Quest.Detail.EXP, pc.Quest.Detail.JEXP, expfactor);
                    int g = (int)(pc.Quest.Detail.Gold * Configuration.Instance.CalcQuestGoldRateForPC(pc));
                    pc.Gold += g;
                    pc.CP += pc.Quest.Detail.CP;
                    string day = DateTime.Now.ToString("d");
                    SInt[day + "任务产生金币数"] += g;
                    SInt[day + "任务产生声望数"] += (int)pc.Quest.Detail.Fame;
                    WeeklyQPoints(pc, pc.Quest.Detail.RequiredQuestPoint);
                    //pc.AInt["本周消耗任务点"] += pc.Quest.Detail.RequiredQuestPoint;
                    //Skill.SkillHandler.SendSystemMessage(pc, "本周累积消耗了任务点：" + pc.AInt["本周消耗任务点"] + "点");

                    if(Global.Random.Next(0,500) < pc.Quest.Detail.RequiredQuestPoint)
                    {
                        GiveItem(pc, 950000059, 1);
                    }

                    DateTime now = DateTime.Now;
                    if (now.Year == 2017 && now.Month == 9 && now.Day >= 10 && now.Day <= 17)
                    {
                        SagaDB.Item.Item jiaqiao = ItemFactory.Instance.GetItem(10007500);
                        jiaqiao.Stack = (ushort)Global.Random.Next(1, pc.Quest.Detail.RequiredQuestPoint * 2);
                        client.AddItem(jiaqiao, true);
                    }

                    //pc.EP++;
                    if (pc.EP > pc.MaxEP)
                        pc.EP = pc.MaxEP;
                    if (pc.Quest.Difficulty(pc) != QuestDifficulty.TOO_EASY)
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
            if (map.Info.Flag.Test(MapFlags.Dominion))
                lv = pc.DominionLevel;
            else
                lv = pc.Level;
            var quests =
                from q in QuestFactory.Instance.Items.Values
                where q.GroupID == groupID && ((lv >= q.MinLevel && lv <= q.MaxLevel) || q.MinLevel == 255)
                && ((pc.Job == q.Job) || q.Job == PC_JOB.NONE)
                && ((pc.JobType == q.JobType) || q.JobType == JobType.NOVICE)
                && ((pc.Race == q.Race) || q.Race == PC_RACE.NONE)
                && ((pc.Gender == q.Gender) || q.Gender == PC_GENDER.NONE)
                select q;
            List<QuestInfo> list = quests.ToList<QuestInfo>();
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
