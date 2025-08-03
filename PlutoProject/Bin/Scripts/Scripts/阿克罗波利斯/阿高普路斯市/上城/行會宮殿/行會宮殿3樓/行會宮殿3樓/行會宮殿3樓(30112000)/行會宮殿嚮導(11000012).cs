using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:行會宮殿3樓(30112000) NPC基本信息:行會宮殿嚮導(11000012) X:10 Y:16
namespace SagaScript.M30112000
{
    public class S11000012 : Event
    {
        public S11000012()
        {
            this.EventID = 11000012;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000012, 131, "您好，这里是行会宫殿3楼哦!$R;" +
                                   "$P北边是农夫总管的房间。$R;" +
                                   "东边是魔法师总管的房间。$R;" +
                                   "南边是机械师总管的房间。$R;" +
                                   "西边是矿工总管的房间。$R;", "行会宫殿向导");
        }
    }
}
