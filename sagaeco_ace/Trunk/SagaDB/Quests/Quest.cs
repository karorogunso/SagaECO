using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using SagaDB.Actor;

namespace SagaDB.Quests
{
    public class QuestInfo
    {
        uint id;
        QuestType type;
        string name;
        int time;
        byte minlv, maxlv;
        uint rewarditem;
        byte rewardcount;
        byte requiredQuestPoint;
        uint dungeonID;
        uint exp, jexp, gold, fame, cp;
        uint mapid1, mapid2, mapid3;
        uint obj1, obj2, obj3;
        int count1, count2, count3;
        bool party;
        uint npcsource, npcdest;
        uint groupID;
        string _QuestCounterName;
        JobType jobtype = JobType.NOVICE;
        PC_JOB job = PC_JOB.NONE;
        PC_RACE race = PC_RACE.NONE;
        PC_GENDER gender = PC_GENDER.NONE;


        /// <summary>
        /// 任务ID
        /// </summary>
        public uint ID { get { return this.id; } set { this.id = value; } }

        /// <summary>
        /// 任务组ID
        /// </summary>
        public uint GroupID { get { return this.groupID; } set { this.groupID = value; } }

        /// <summary>
        /// 任务类型
        /// </summary>
        public QuestType QuestType { get { return this.type; } set { this.type = value; } }

        /// <summary>
        /// 任务名称
        /// </summary>
        public string Name { get { return this.name; } set { this.name = value; } }

        /// <summary>
        /// 任务时间限制，分钟为单位
        /// </summary>
        public int TimeLimit { get { return this.time; } set { this.time = value; } }

        /// <summary>
        /// 任务完成奖励道具
        /// </summary>
        public uint RewardItem { get { return this.rewarditem; } set { this.rewarditem = value; } }

        /// <summary>
        /// 任务完成奖励道具的数量
        /// </summary>
        public byte RewardCount { get { return this.rewardcount; } set { this.rewardcount = value; } }

        /// <summary>
        /// 任务需要的任务点
        /// </summary>
        public byte RequiredQuestPoint { get { return this.requiredQuestPoint; } set { this.requiredQuestPoint = value; } }

        /// <summary>
        /// 任务创建的遗迹ID
        /// </summary>
        public uint DungeonID { get { return this.dungeonID; } set { this.dungeonID = value; } }

        /// <summary>
        /// 任务要求最低人物等级
        /// </summary>
        public byte MinLevel { get { return this.minlv; } set { this.minlv = value; } }

        /// <summary>
        /// 任务要求最低人物等级
        /// </summary>
        public byte MaxLevel { get { return this.maxlv; } set { this.maxlv = value; } }

        /// <summary>
        /// 任务奖励经验值
        /// </summary>
        public uint EXP { get { return this.exp; } set { this.exp = value; } }

        /// <summary>
        /// 任务奖励职业经验值
        /// </summary>
        public uint JEXP { get { return this.jexp; } set { this.jexp = value; } }

        /// <summary>
        /// 任务奖励金钱
        /// </summary>
        public uint Gold { get { return this.gold; } set { this.gold = value; } }

        /// <summary>
        /// 任务奖励CP
        /// </summary>
        public uint CP { get { return this.cp; } set { this.cp = value; } }


        /// <summary>
        /// 任务奖励声望
        /// </summary>
        public uint Fame { get { return this.fame; } set { this.fame = value; } }

        /// <summary>
        /// 有效地图1
        /// </summary>
        public uint MapID1 { get { return this.mapid1; } set { this.mapid1 = value; } }

        /// <summary>
        /// 有效地图2
        /// </summary>
        public uint MapID2 { get { return this.mapid2; } set { this.mapid2 = value; } }

        /// <summary>
        /// 有效地图3
        /// </summary>
        public uint MapID3 { get { return this.mapid3; } set { this.mapid3 = value; } }

        /// <summary>
        /// 任务对象ID，可以是道具ID，也可以是怪物ID
        /// </summary>
        public uint ObjectID1 { get { return this.obj1; } set { this.obj1 = value; } }

        /// <summary>
        /// 任务对象ID2，可以是道具ID，也可以是怪物ID
        /// </summary>
        public uint ObjectID2 { get { return this.obj2; } set { this.obj2 = value; } }

        /// <summary>
        /// 任务对象ID3，可以是道具ID，也可以是怪物ID
        /// </summary>
        public uint ObjectID3 { get { return this.obj3; } set { this.obj3 = value; } }

        /// <summary>
        /// 任务要求对象个数1
        /// </summary>
        public int Count1 { get { return this.count1; } set { this.count1 = value; } }

        /// <summary>
        /// 任务要求对象个数2
        /// </summary>
        public int Count2 { get { return this.count2; } set { this.count2 = value; } }

        /// <summary>
        /// 任务要求对象个数2
        /// </summary>
        public int Count3 { get { return this.count3; } set { this.count3 = value; } }

        /// <summary>
        /// 是否可以组队进行
        /// </summary>
        public bool Party { get { return this.party; } set { this.party = value; } }

        /// <summary>
        /// 搬运任务委托NPC
        /// </summary>
        public uint NPCSource { get { return this.npcsource; } set { this.npcsource = value; } }

        /// <summary>
        /// 搬运任务目标NPC
        /// </summary>
        public uint NPCDestination { get { return this.npcdest; } set { this.npcdest = value; } }

        /// <summary>
        /// 任务完成计数器
        /// </summary>
        public string QuestCounterName { get { return this._QuestCounterName; } set { this._QuestCounterName = value; } }

        /// <summary>
        /// 任务要求的职业类别
        /// </summary>
        public JobType JobType { get { return this.jobtype; } set { this.jobtype = value; } }

        /// <summary>
        /// 任务要求的职业职业
        /// </summary>
        public PC_JOB Job { get { return this.job; } set { this.job = value; } }

        /// <summary>
        /// 任务要求的种族
        /// </summary>
        public PC_RACE Race { get { return this.race; } set { this.race = value; } }

        /// <summary>
        /// 任务要求的性别
        /// </summary>
        public PC_GENDER Gender { get { return this.gender; } set { this.gender = value; } }

        public override string ToString()
        {
            return this.name;
        }
    }

    public class Quest
    {
        QuestInfo info;
        QuestStatus status;
        DateTime endTime;
        int count1, count2, count3;
        Npc.NPC npc;

        public Quest(uint id)
        {
            info = QuestFactory.Instance.Items[id];
        }

        public uint ID { get { return info.ID; } set { info.ID = value; } }
        public QuestType QuestType { get { return info.QuestType; } set { info.QuestType = value; } }
        public string Name { get { return info.Name; } set { info.Name = value; } }

        public QuestInfo Detail { get { return info; } }

        /// <summary>
        /// 任务状态
        /// </summary>
        public QuestStatus Status { get { return status; } set { status = value; } }

        /// <summary>
        /// 任务结束时间
        /// </summary>
        public DateTime EndTime { get { return this.endTime; } set { this.endTime = value; } }

        /// <summary>
        /// 目前完成数量1
        /// </summary>
        public int CurrentCount1 { get { return this.count1; } set { this.count1 = value; } }

        /// <summary>
        /// 目前完成数量2
        /// </summary>
        public int CurrentCount2 { get { return this.count2; } set { this.count2 = value; } }

        /// <summary>
        /// 目前完成数量3
        /// </summary>
        public int CurrentCount3 { get { return this.count3; } set { this.count3 = value; } }

        /// <summary>
        /// 委托任务的NPC
        /// </summary>
        public Npc.NPC NPC { get { return this.npc; } set { this.npc = value; } }

        /// <summary>
        /// 该任务对指定玩家来说的难度
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <returns>难度</returns>
        public QuestDifficulty Difficulty(Actor.ActorPC pc)
        {
            int diff = this.Detail.MinLevel - pc.Level;
            if (Math.Abs(diff) <= 3)
                return QuestDifficulty.BEST_FIT;
            if (Math.Abs(diff) > 3 && Math.Abs(diff) <= 9)
                return QuestDifficulty.NORMAL;
            if (diff > 9)
                return QuestDifficulty.TOO_HARD;
            if (diff < -9)
                return QuestDifficulty.TOO_EASY;
            return QuestDifficulty.NORMAL;
        }
    }
}
