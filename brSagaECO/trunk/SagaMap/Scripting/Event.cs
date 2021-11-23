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
using SagaDB.Npc;

namespace SagaMap.Scripting
{
    public delegate void MobAttackCallback(ActorEventHandlers.MobEventHandler eh, ActorPC pc);
    public delegate void MobCallback(ActorEventHandlers.MobEventHandler eh,ActorPC pc);
    public delegate void TimerCallback(Timer timer,ActorPC pc);

    /// <summary>
    /// 所有脚本文件的基类
    /// </summary>
    public abstract partial class Event
    {
        uint eventID;
        uint buyLimit = 2000;
        /// <summary>
        /// 如果玩家已经领了任务，NPC回复
        /// </summary>
        protected string alreadyHasQuest = "";
        /// <summary>
        /// 玩家领取一般任务(非搬运任务）后的回复
        /// </summary>
        protected string gotNormalQuest = "";
        /// <summary>
        /// 玩家领取搬运任务后的回复
        /// </summary>
        protected string gotTransportQuest = "";
        /// <summary>
        /// 任务完成后的回复
        /// </summary>
        protected string questCompleted = "";
        /// <summary>
        /// 如果是搬运任务，问候语
        /// </summary>
        protected string transport = "";
        /// <summary>
        /// 任务取消后回复
        /// </summary>
        protected string questCanceled = "";
        /// <summary>
        /// 任务失败的回复
        /// </summary>
        protected string questFailed = "";
        /// <summary>
        /// 领取任务需要最少任务点
        /// </summary>
        protected int leastQuestPoint = 1;
        /// <summary>
        /// 任务点不够时回复
        /// </summary>
        protected string notEnoughQuestPoint = "";

        /// <summary>
        /// 任务太简单的回复
        /// </summary>
        protected string questTooEasy = "";

        /// <summary>
        /// 任务太困难的回复
        /// </summary>
        protected string questTooHard = "";

        /// <summary>
        /// 搬运任务起始NPC回复
        /// </summary>
        protected string questTransportSource = "";

        /// <summary>
        /// 搬运完成后起始NPC回复
        /// </summary>
        protected string questTransportCompleteSrc = "";
        /// <summary>
        /// 搬运任务目标NPC回复
        /// </summary>
        protected string questTransportDest = "";

        /// <summary>
        /// 搬运完成后目标NPC回复
        /// </summary>
        protected string questTransportCompleteDest = "";

        List<uint> goods = new List<uint>();
        ActorPC currentPC;

        /// <summary>
        /// 触发当前脚本的玩家
        /// </summary>
        public ActorPC CurrentPC { get { return this.currentPC; } set { this.currentPC = value; } }

        /// <summary>
        /// 当前脚本所设置的默认商店物品列表
        /// </summary>
        public List<uint> Goods { get { return this.goods; } }

        /// <summary>
        /// NPC所收购的物品价值的上限
        /// <remarks>默认为2000</remarks>
        /// </summary>
        protected uint BuyLimit { get { return this.buyLimit; } set { this.buyLimit = value; } }

        /// <summary>
        /// 服务器专有字符串变量集
        /// </summary>
        protected VariableHolder<string, string> SStr
        {
            get
            {
                return ScriptManager.Instance.VariableHolder.AStr;
            }
        }

        /// <summary>
        /// 服务器专有整数变量集
        /// </summary>
        protected VariableHolder<string, int> SInt
        {
            get
            {
                return ScriptManager.Instance.VariableHolder.AInt;
            }
        }

        /// <summary>
        /// 服务器专有标识变量集
        /// </summary>
        protected VariableHolderA<string, BitMask> SMask
        {
            get
            {
                return ScriptManager.Instance.VariableHolder.AMask;
            }
        }

        /// <summary>
        /// 当前脚本的EventID
        /// </summary>
        public uint EventID { get { return this.eventID; } set { this.eventID = value; } }
        
        private MapClient GetMapClient(ActorPC pc)
        {
            ActorEventHandlers.PCEventHandler eh = (SagaMap.ActorEventHandlers.PCEventHandler)pc.e;
            return eh.Client;
        }

        public override string ToString()
        {
            if (NPCFactory.Instance.Items.ContainsKey(this.eventID))
            {
                return NPCFactory.Instance.Items[this.eventID].Name + "(" + this.eventID.ToString() + ")";
            }
            else
                return base.ToString();
        }
    }
}
