using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaLib;
using SagaDB.Actor;
namespace SagaDB.LevelLimit
{
    public class LevelLimit : Singleton<LevelLimit>
    {
        uint firstLevelLimit, nowLevelLimit, nextLevelLimit, lastTimeLevelLimit;
        uint setNextUpLevelLimit, setNextUpDays;
        DateTime reachTime, nextTime;
        List<ActorPC> finishPlayers = new List<ActorPC>();
        uint firstPlayer, secondPlayer, thirdPlayer, fourthPlayer, fifthPlyaer;
        byte isLock;

        /// <summary>
        /// 是否正在等待标记
        /// </summary>
        public byte IsLock { get { return this.isLock; } set { this.isLock = value; } }

        /// <summary>
        /// 初次设定的等级上限
        /// </summary>
        public uint FirstLevelLimit { get { return this.firstLevelLimit; } set { this.firstLevelLimit = value; } }

        /// <summary>
        /// 当前的等级上限
        /// </summary>
        public uint NowLevelLimit { get { return this.nowLevelLimit; } set { this.nowLevelLimit = value; } }

        /// <summary>
        /// 下次的等级上限
        /// </summary>
        public uint NextLevelLimit { get { return this.nextLevelLimit; } set { this.nextLevelLimit = value; } }

        /// <summary>
        /// 上次的等级上限
        /// </summary>
        public uint LastTimeLevelLimit { get { return this.lastTimeLevelLimit; } set { this.lastTimeLevelLimit = value; } }

        /// <summary>
        /// 设置下次的等级上限增幅度
        /// </summary>
        public uint SetNextUpLevelLimit { get { return this.setNextUpLevelLimit; } set { this.setNextUpLevelLimit = value; } }

        /// <summary>
        /// 设置下次达成上限后等待的天数
        /// </summary>
        public uint SetNextUpDays { get { return this.setNextUpDays; } set { this.setNextUpDays = value; } }

        /// <summary>
        /// 当前完成的时间
        /// </summary>
        public DateTime ReachTime { get { return this.reachTime; } set { this.reachTime = value; } }

        /// <summary>
        /// 下次开始新上限的时间
        /// </summary>
        public DateTime NextTime { get { return this.nextTime; } set { this.nextTime = value; } }

        /// <summary>
        /// 第一个达成的玩家CharID
        /// </summary>
        public uint FirstPlayer { get { return this.firstPlayer; } set { this.firstPlayer = value; } }

        /// <summary>
        /// 第二个达成的玩家CharID
        /// </summary>
        public uint SecondPlayer { get { return this.secondPlayer; } set { this.secondPlayer = value; } }

        /// <summary>
        /// 第三个达成的玩家CharID
        /// </summary>
        public uint Thirdlayer { get { return this.thirdPlayer; } set { this.thirdPlayer = value; } }

        /// <summary>
        /// 第四个达成的玩家CharID
        /// </summary>
        public uint FourthPlayer { get { return this.fourthPlayer; } set { this.fourthPlayer = value; } }

        /// <summary>
        /// 第五个达成的玩家CharID
        /// </summary>
        public uint FifthPlayer { get { return this.fifthPlyaer; } set { this.fifthPlyaer = value; } }

        /// <summary>
        /// 达成上限的玩家列表
        /// </summary>
        public List<ActorPC> FinishPlayers { get { return this.finishPlayers; } set { this.finishPlayers = value; } }
    }
}