
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
    public abstract class 东部地牢传送模板 : SagaMap.Scripting.Event
    {
        public uint mapID;
        public byte x1, y1;
        public byte x2, y2;

        public void Init(uint eventID, uint mapID, byte x1, byte y1, byte x2, byte y2)
        {
            this.EventID = eventID;
            this.mapID = mapID;
            this.x1 = x1;
            this.y1 = y1;
            this.x2 = x2;
            this.y2 = y2;
        }
        public override void OnEvent(ActorPC pc)
        {
            uint id = 0;
            byte x, y;
            if (pc.Party == null)//队伍炸了
            {
                if(pc.TInt["副本复活标记"] != 4)
                Say(pc, 131, "你不能通过这个传送门。");
                else
                {
                    id = (uint)pc.TInt["S" + mapID.ToString()];//获取地图变量，S + 地图ID
                    x = (byte)Global.Random.Next(x1, x2);//随机
                    y = (byte)Global.Random.Next(y1, y2);//随机
                    Warp(pc, id, x, y);
                }
                return;
            }
            if (pc.Party != null)//队伍没炸
                id = (uint)pc.Party.TInt["S" + mapID.ToString()];//获取地图变量，S + 地图ID
            if (id == 0)//如果地图获取失败
            {
                Say(pc, 131, "你不能通过这个传送门。。" + "S" + mapID.ToString() + " " + id.ToString());
                return;
            }


            x = (byte)Global.Random.Next(x1, x2);//随机
            y = (byte)Global.Random.Next(y1, y2);//随机


            if (检查(pc))//如果检查通过
            {
                foreach (var item in pc.Party.Members.Values)
                {
                    item.Buff.黑暗压制 = true;
                    item.TInt["副本复活标记"] = 1;
                    item.Party.Leader.TInt["复活次数"] = item.Party.Leader.TInt["设定复活次数"];
                    Warp(item, id, x, y);
                }
            }
        }


        bool 检查(ActorPC pc)
        {
            if (pc.Party.Leader == null) return false;//队长炸了
            if (pc.MapID != pc.Party.Leader.MapID) return false;//和队长不在一个图
            if (pc.Party.Leader != pc)//如果不是队长
            {
                Say(pc, 0, "嗯？$R还是等队长来了一起下去吧。", "");
                return false;
            }
            foreach (var item in pc.Party.Members.Values)
            {
                if (!item.Online && item.MapID != pc.Party.Leader.MapID)
                {
                    SendSystemMessageToMember(item, "有玩家不在这里，进入取消");
                    return false;
                }
                SendSystemMessageToMember(item, "等待玩家" + item.Name + " 接受中..");
                if (Select(item, "是否同意进入下一张地图？", "", "同意", "不同意！！") == 1)
                    SendSystemMessageToMember(item, item.Name + "选择了[同意]，等待下一位队员确认。");
                else
                {
                    SendSystemMessageToMember(item, item.Name + "选择了[不同意]，进入取消。");
                    return false;
                }
            }
            foreach (var item in pc.Party.Members.Values)
            {
                if (item.Buff.Dead || item == null || !item.Online)
                {
                    SendSystemMessageToMember(item, item.Name + "状态异常，进入取消。");
                    return false;
                }
            }
            return true;
        }
        /// <summary>
        /// 发送系统信息给队伍所有人
        /// </summary>
        /// <param name="pc">玩家</param>
        /// <param name="msg">信息内容</param>
        void SendSystemMessageToMember(ActorPC pc, string msg)
        {
            if (pc.Party == null) return;//如果玩家没有队伍，则直接返回
            foreach (var item in pc.Party.Members.Values)//遍历玩家队伍成员
            {
                if (item.Online)//如果队员在线
                    SagaMap.Network.Client.MapClient.FromActorPC(item).SendSystemMessage(msg);//发送系统信息
            }
        }

    }
}

