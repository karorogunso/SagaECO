using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using System.Text;
using System.Diagnostics;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace 东部地牢副本
{
    public partial class 东部地牢 : Event
    {
        public enum Difficulty
        {
            /// <summary>
            /// 简单
            /// </summary>
            Easy,
            /// <summary>
            /// 普通
            /// </summary>
            Normal,
            /// <summary>
            /// 困难
            /// </summary>
            Hard,
            /// <summary>
            /// 地狱
            /// </summary>
            Hell,
            Single_Easy,
            Single_Normal,
            Single_Hard,
            Single_Hell,
        }
        /// <summary>
        /// 初始化难度变量
        /// </summary>
        void IniLimit(Difficulty d)
        {
            lv = GetLimitLevel(d);//获取等级
            MaxMember = GetLimitMember(d);//获取最大人数
            QuestPoint = GetQuestPoint(d);//获取任务点
        }
        void SetReviveCountForSingle(ActorPC pc, Difficulty d)
        {
            byte count = 4;//定时默认4次
            switch (d)
            {
                case Difficulty.Single_Easy:
                    count = 2;
                    break;
                case Difficulty.Single_Normal:
                    count = 2;
                    break;
                case Difficulty.Single_Hard:
                    count = 0;
                    break;
                case Difficulty.Single_Hell:
                    count = 0;
                    break;
                default:
                    count = 0;
                    break;
            }
            pc.TInt["副本复活标记"] = 4;//复活标记，1为副本用
            pc.TInt["单人复活次数记录"] = count;
            pc.TInt["单人复活次数"] = count;
        }
        /// <summary>
        /// 根据难度设置队伍的复活次数
        /// </summary>
        /// <param name="pc">队员</param>
        /// <param name="d">难度</param>
        void SetReviveCount(ActorPC pc, Difficulty d)
        {
            byte count = 4;//定时默认4次
            switch (d)
            {
                case Difficulty.Easy:
                    count = 8;
                    break;
                case Difficulty.Normal:
                    count = 4;
                    break;
                case Difficulty.Hard:
                    count = 0;
                    break;
                case Difficulty.Hell:
                    count = 0;
                    break;
                default:
                    count = 0;
                    break;
            }
            pc.TInt["副本复活标记"] = 1;//复活标记，1为副本用
            pc.Party.Leader.TInt["复活次数"] = count;//设置剩余复活次数
            pc.Party.Leader.TInt["设定复活次数"] = count;//设置团灭后恢复的复活次数
        }
        /// <summary>
        /// 获取难度名
        /// </summary>
        /// <param name="d">难度</param>
        /// <returns>难度</returns>
        string GetDiffName(Difficulty d)
        {
            switch (d)
            {
                case Difficulty.Easy:
                    return "简单";
                case Difficulty.Normal:
                    return "普通";
                case Difficulty.Hard:
                    return "困难";
                case Difficulty.Hell:
                    return "地狱";
                case Difficulty.Single_Easy:
                    return "单人简单";
                case Difficulty.Single_Normal:
                    return "单人普通";
                case Difficulty.Single_Hard:
                    return "单人困难";
                case Difficulty.Single_Hell:
                    return "单人地狱";
                default:
                    return "错误";
            }
        }
        /// <summary>
        /// 根据难度获取人数限制
        /// </summary>
        /// <param name="d">难度</param>
        /// <returns>人数</returns>
        byte GetLimitMember(Difficulty d)
        {
            switch (d)
            {
                case Difficulty.Easy:
                    return 4;
                case Difficulty.Normal:
                    return 4;
                case Difficulty.Hard:
                    return 4;
                case Difficulty.Hell:
                    return 4;
                case Difficulty.Single_Easy:
                    return 1;
                case Difficulty.Single_Normal:
                    return 1;
                case Difficulty.Single_Hard:
                    return 1;
                case Difficulty.Single_Hell:
                    return 1;
                default:
                    return 4;
            }
        }
        /// <summary>
        /// 根据难度获取任务点要求
        /// </summary>
        /// <param name="d">难度</param>
        /// <returns>需求的任务点数</returns>
        ushort GetQuestPoint(Difficulty d)
        {
            switch (d)
            {
                case Difficulty.Easy:
                    return 5;
                case Difficulty.Normal:
                    return 10;
                case Difficulty.Hard:
                    return 30;
                case Difficulty.Hell:
                    return 250;
                case Difficulty.Single_Easy:
                    return 5;
                case Difficulty.Single_Normal:
                    return 10;
                case Difficulty.Single_Hard:
                    return 30;
                case Difficulty.Single_Hell:
                    return 250;
                default:
                    return 4;
            }
        }
        /// <summary>
        /// 根据难度获取等级限制
        /// </summary>
        /// <param name="d">难度</param>
        /// <returns>等级</returns>
        byte GetLimitLevel(Difficulty d)
        {
            switch (d)
            {
                case Difficulty.Easy:
                    return 45;
                case Difficulty.Normal:
                    return 45;
                case Difficulty.Hard:
                    return 45;
                case Difficulty.Hell:
                    return 45;
                case Difficulty.Single_Easy:
                    return 45;
                case Difficulty.Single_Normal:
                    return 45;
                case Difficulty.Single_Hard:
                    return 45;
                case Difficulty.Single_Hell:
                    return 45;
                default:
                    return 250;
            }
        }
    }
}