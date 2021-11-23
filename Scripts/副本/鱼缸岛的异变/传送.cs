using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;

namespace SagaScript
{
    //原始地圖:大陸之洞窟B1F(20000000)
    //目標地圖:大陸之洞窟B2F(20001000)
    //目標坐標:(105,106) ~ (106,109)

    public class P10000173 : Event
    {
        public P10000173()
        {
            this.EventID = 10000173;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.Party == null)
            {
                if (Select(pc, "进入下一张图吗？", "", "是的", "算了") == 1)
                {
                    pc.TInt["副本复活标记"] = 4;
                    pc.TInt["复活次数"] = 3;
                    pc.TInt["设定复活次数"] = 3;
                    Warp(pc, (uint)pc.TInt["S20000000"], 105, 107);
                    SetNextMoveEvent(pc, 87000000);//AAA剧情
                }
                return;
            }

            if (pc.Party.Leader == null) return;
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
                Warp(item, (uint)item.Party.Leader.TInt["S20000000"], 105, 107);
                SetNextMoveEvent(item, 87000000);//AAA剧情
            }
        }
    }
    //原始地圖:大陸之洞窟B2F(20001000)
    //目標地圖:大陸之洞窟B1F(20000000)
    //目標坐標:(105,106) ~ (106,109)

    public class P10000175 : Event
    {
        public P10000175()
        {
            this.EventID = 10000175;
        }
        public override void OnEvent(ActorPC pc)
        {
            if (pc.Party == null)
            {
                if (Select(pc, "进入下一张图吗？", "", "是的", "算了") == 1)
                {
                    pc.TInt["副本复活标记"] = 4;
                    pc.TInt["复活次数"] = 3;
                    pc.TInt["设定复活次数"] = 3;
                    Warp(pc, (uint)pc.TInt["S20001000"], 21, 63);
                }
                return;
            }
            if (pc.Party.Leader == null) return;
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
                Warp(item, (uint)item.Party.Leader.TInt["S20001000"], 21, 63);
            }
        }
    }
    //原始地圖:大陸之洞窟B3F(20002000)
    //目標地圖:大陸之洞窟B2F(20001000)
    //目標坐標:(21,62) ~ (22,65)

    //原始地圖:大陸之洞窟B3F(20002000)
    //目標地圖:大陸之洞窟B4F(20003000)
    //目標坐標:(105,62) ~ (106,65)

    public class P10000177 : Event
    {
        public P10000177()
        {
            this.EventID = 10000177;
        }
        public override void OnEvent(ActorPC pc)
        {
            if (pc.Party == null)
            {
                if (Select(pc, "进入下一张图吗？", "", "是的", "算了") == 1)
                {
                    pc.TInt["副本复活标记"] = 4;
                    pc.TInt["复活次数"] = 3;
                    pc.TInt["设定复活次数"] = 3;
                    Warp(pc, (uint)pc.TInt["S20002000"], 105, 63);
                }
                return;
            }
            if (pc.Party.Leader == null) return;
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
                Warp(item, (uint)item.Party.Leader.TInt["S20002000"], 105, 63);
            }
        }
    }
    //原始地圖:大陸之洞窟B4F(20003000)
    //目標地圖:大陸之洞窟B3F(20002000)
    //目標坐標:(105,62) ~ (106,65)

    public class P10000179 : Event
    {
        public P10000179()
        {
            this.EventID = 10000179;
        }
        public override void OnEvent(ActorPC pc)
        {
            if (pc.Party == null)
            {
                if (Select(pc, "进入下一张图吗？", "", "是的", "算了") == 1)
                {
                    pc.TInt["副本复活标记"] = 4;
                    pc.TInt["复活次数"] = 3;
                    pc.TInt["设定复活次数"] = 3;
                    Warp(pc, (uint)pc.TInt["S20003000"], 61, 103);
                    SetNextMoveEvent(pc, 87000000);//AAA剧情
                }
                return;
            }
            if (pc.Party.Leader == null) return;
            if (pc.Party.Leader != pc)
            {
                Say(pc, 0, "嗯？$R还是等队长来了一起下去吧。", "暗鸣");
                return;
            }
            if (pc.Party.Leader.TInt["鱼缸岛危机老二死亡"] != 1)
            {
                Say(pc, 0, "那边有位很强大的敌人呢...", "暗鸣");
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
                if(item.Buff.Dead || item == null || !item.Online)
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
                Warp(item, (uint)item.Party.Leader.TInt["S20003000"], 61, 103);
                SetNextMoveEvent(item, 87000000);//AAA剧情
            }
        }
    }
    //原始地圖:大陸之洞窟B5F(20004000)
    //目標地圖:大陸之洞窟B4F(20003000)
    //目標坐標:(61,103) ~ (62,104)
}