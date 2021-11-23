using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Manager;
using SagaDB.LevelLimit;
namespace SagaMap.LevelLimit
{
    public class LevelLimitManager :Singleton<LevelLimitManager>
    {
        /// <summary>
        /// 载入等级上限信息
        /// </summary>
        public void LoadLevelLimit()
        {
            SagaDB.LevelLimit.LevelLimit LL = SagaDB.LevelLimit.LevelLimit.Instance;
            MapServer.charDB.GetLevelLimit();
            LL.FirstLevelLimit = (uint)SagaMap.Configuration.Instance.FirstLevelLimit;
            if (LL.NowLevelLimit < LL.FirstLevelLimit)
                LL.NowLevelLimit = LL.FirstLevelLimit;
            if(DateTime.Now > LL.ReachTime)
                Tasks.System.LevelLimit.Instance.Activate();
        }

        /// <summary>
        /// 有达成等级上限时
        /// </summary>
        public void ReachLevelLimit(ActorPC pc)
        {
            SagaDB.LevelLimit.LevelLimit LL = SagaDB.LevelLimit.LevelLimit.Instance;
            if (LL.FirstPlayer == 0)
            {
                MapClientManager.Instance.Announce(string.Format("【公告】 " + pc.Name + " 成为第一位达成{0}级等级上限的玩家！",LL.NowLevelLimit));
                MapClientManager.Instance.Announce(string.Format("【公告】下次等级上限开始解冻，下一次的等级上限为{0}，开放时间为{1}。",LL.NextLevelLimit,LL.NextTime));
                MapClientManager.Instance.Announce("【福利】圣塔的能量开始溢出！所有达到等级上限的玩家掉落率上升！");
                LL.FirstPlayer = pc.CharID;
                this.StartLevelLimitWait();
            }
            else if (LL.SecondPlayer == 0)
            {
                MapClientManager.Instance.Announce(string.Format("【公告】 恭喜 " + pc.Name + " 成为第二位达成{0}级等级上限的玩家！",LL.NowLevelLimit));
                LL.SecondPlayer = pc.CharID;
            }
            else if (LL.Thirdlayer == 0)
            {
                MapClientManager.Instance.Announce(string.Format("【公告】 恭喜 " + pc.Name + " 成为第三位达成{0}级等级上限的玩家！",LL.NowLevelLimit));
                LL.Thirdlayer = pc.CharID;
            }
            else if (LL.FourthPlayer == 0)
            {
                MapClientManager.Instance.Announce(string.Format("【公告】 恭喜 " + pc.Name + " 成为第四位达成{0}级等级上限的玩家！",LL.NowLevelLimit));
                LL.FourthPlayer = pc.CharID;
            }
            else if (LL.FifthPlayer == 0)
            {
                MapClientManager.Instance.Announce(string.Format("【公告】 恭喜 " + pc.Name + " 成为第五位达成{0}级等级上限的玩家！",LL.NowLevelLimit));
                LL.FifthPlayer = pc.CharID;
            }
            else
            {
                MapClientManager.Instance.Announce(string.Format("【公告】 恭喜 " + pc.Name + " 达成了{0}级等级上限。", LL.NowLevelLimit));
            }
            if (LL.IsLock == 0)
                LL.IsLock = 1;
            this.SendReachInfo(SagaMap.Network.Client.MapClient.FromActorPC(pc));
            this.SavaLevelLimitInfo();
        }

        /// <summary>
        /// 开始解冻当前的等级上限
        /// </summary>
        public void StartLevelLimitWait()
        {
            SagaDB.LevelLimit.LevelLimit LL = SagaDB.LevelLimit.LevelLimit.Instance;
            LL.ReachTime = DateTime.Now;
            LL.NextTime = DateTime.Now.AddDays(LL.SetNextUpDays);
            LL.IsLock = 1;
            Tasks.System.LevelLimit.Instance.Activate();
            this.SavaLevelLimitInfo();
        }
        /// <summary>
        /// 等待结束后，等级上限提升
        /// </summary>
        public void UpdataLevelLimit()
        {
            SagaDB.LevelLimit.LevelLimit LL = SagaDB.LevelLimit.LevelLimit.Instance;
            if (LL.IsLock == 1)
            {
                LL.IsLock = 0;
                LL.FirstPlayer = 0;
                LL.SecondPlayer = 0;
                LL.Thirdlayer = 0;
                LL.FourthPlayer = 0;
                LL.FifthPlayer = 0;
                LL.LastTimeLevelLimit = LL.NowLevelLimit;
                LL.NowLevelLimit = LL.NextLevelLimit;
                LL.NextLevelLimit += LL.SetNextUpLevelLimit;
                SagaMap.Manager.ExperienceManager.Instance.MaxCLevel = LL.NowLevelLimit;
                SagaMap.Manager.ExperienceManager.Instance.MaxCLevel2 = LL.NextLevelLimit;
                MapClientManager.Instance.Announce(string.Format("【公告】新的等级上限已解除，本次的等级上限为{0}级。", LL.NowLevelLimit));
                MapClientManager.Instance.Announce(string.Format("【福利】圣塔开始积累新的能量，满级玩家的掉落率恢复原状，{0}级以前的玩家将获得额外的经验奖励。", LL.LastTimeLevelLimit));
            }
            Tasks.System.LevelLimit.Instance.Deactivate();
            this.SavaLevelLimitInfo();
        }

        /// <summary>
        /// 发送满级圣塔发光信息
        /// </summary>
        /// <param name="client">玩家</param>
        public void SendReachInfo(SagaMap.Network.Client.MapClient client)
        {
            SagaDB.LevelLimit.LevelLimit LL = SagaDB.LevelLimit.LevelLimit.Instance;
            if (LL.IsLock == 1)
            {
                Packets.Server.SSMG_ACTIVITY_RECYCLE_MODE p = new Packets.Server.SSMG_ACTIVITY_RECYCLE_MODE();
                p.EndTime = LL.NextTime;
                p.Result = 1;
                client.netIO.SendPacket(p);
                this.SavaLevelLimitInfo();
            }
        }

        /// <summary>
        /// 保存等级上限
        /// </summary>
        public void SavaLevelLimitInfo()
        {
            MapServer.charDB.SavaLevelLimit();
        }
    }
}
