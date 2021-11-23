
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S80000200 : Event
    {
        public S80000200()
        {
            this.EventID = 80000200;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Party == null)
            {
                /*if (pc.TInt["鱼缸岛危机老一死亡"] != 1)
                {
                    Say(pc, 0, "那边有位很强大的敌人呢...", "暗鸣");
                    return;
                }*/
                if (Select(pc, "要进入下一张地图吗？", "是的", "算了") == 1)
                {
                    Warp(pc, (uint)pc.TInt["S20004000"], 71, 22);
                    SetNextMoveEvent(pc, 87000000);
                }
                return;
            }
            if (pc.Party == null) return;
            if (pc.Party.Leader == null) return;
            if(pc.Party.Leader.TInt["鱼缸岛危机老一死亡"] != 1)
            {
                Say(pc, 0, "那边有位很强大的敌人呢...", "暗鸣");
                return;
            }
            if (pc.Party.Leader != pc)
            {
                Say(pc, 0, "嗯？$R还是等队长来了一起下去吧。", "暗鸣");
                return;
            }
            foreach (var item in pc.Party.Members.Values)
            {
                if (!item.Online && item.MapID != pc.Party.Leader.MapID)
                {
                    foreach (var item2 in pc.Party.Members.Values)
                        SagaMap.Network.Client.MapClient.FromActorPC(item2).SendSystemMessage("有玩家不在这里，进入取消");
                    return;
                }
                foreach (var item2 in pc.Party.Members.Values)
                    SagaMap.Network.Client.MapClient.FromActorPC(item2).SendSystemMessage("等待玩家" + item.Name + " 接受中..");
                if (Select(item, "准备跳水了，是否跳下？", "", "跳!", "不接受！！") == 1)
                {
                    foreach (var item2 in pc.Party.Members.Values)
                        SagaMap.Network.Client.MapClient.FromActorPC(item2).SendSystemMessage(item.Name + "接受了[跳水]");
                }
                else
                {
                    foreach (var item2 in pc.Party.Members.Values)
                        SagaMap.Network.Client.MapClient.FromActorPC(item2).SendSystemMessage(item.Name + "拒绝了[跳水]，跳水取消。");
                    return;
                }
            }
            foreach (var item in pc.Party.Members.Values)
            {
                if (item.Buff.Dead || item == null || !item.Online)
                {
                    foreach (var item2 in pc.Party.Members.Values)
                        SagaMap.Network.Client.MapClient.FromActorPC(item2).SendSystemMessage(item.Name + "状态异常，进入取消。");
                    return;
                }
            }
            foreach (var item in pc.Party.Members.Values)
            {
                byte count = 4;
                if (item.Party.TInt["普通挑战模式"] == 1)
                    count = 0;
                item.TInt["副本复活标记"] = 1;
                item.Party.Leader.TInt["复活次数"] = count;
                item.Party.Leader.TInt["设定复活次数"] = count;
                Warp(item, (uint)item.Party.Leader.TInt["S20004000"], 71, 22);
                SetNextMoveEvent(item, 87000000);
            }
        }
    }
}

