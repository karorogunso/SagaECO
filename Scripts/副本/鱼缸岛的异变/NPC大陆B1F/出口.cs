
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
    public class S10000171: Event
    {
        public S10000171()
        {
            this.EventID = 10000171;
        }

        public override void OnEvent(ActorPC pc)
        {
            if(pc.Party == null)
            {
                /*if (pc.TInt["鱼缸岛危机老三死亡"] != 1)
                {
                    Say(pc, 0, "休想从我这里踏出一步！！", "斯芬喵喵");
                    return;
                }*/
                if (Select(pc,"要进入下一张地图吗？","是的","算了") == 1)
                {
                    Warp(pc, (uint)pc.TInt["S30131002"], 6, 0);
                    SetNextMoveEvent(pc, 87000000);//AAA剧情
                }
                return;
            }
            if (pc.Party.Leader == null) return;
            if (pc.Party.Leader.TInt["鱼缸岛危机老三死亡"] != 1)
            {
                Say(pc, 0, "休想从我这里踏出一步！！", "斯芬喵喵");
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
                if (Select(item, "是否同意进入下一张地图？", "", "同意", "不同意！！") == 1)
                {
                    foreach (var item2 in pc.Party.Members.Values)
                        SagaMap.Network.Client.MapClient.FromActorPC(item2).SendSystemMessage(item.Name + "选择了[同意]");
                }
                else
                {
                    foreach (var item2 in pc.Party.Members.Values)
                        SagaMap.Network.Client.MapClient.FromActorPC(item2).SendSystemMessage(item.Name + "选择了[不同意]，进入取消。");
                    return;
                }
            }
            foreach (var item in pc.Party.Members.Values)
            {
                Warp(item, (uint)item.Party.Leader.TInt["S30131002"], 6, 0);
                SetNextMoveEvent(item, 87000000);//AAA剧情
               
            }
        }
    }
}

