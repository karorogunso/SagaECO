using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:行會宮殿2樓(30111000) NPC基本信息:行會宮殿嚮導(11000011) X:10 Y:16
namespace SagaScript.M30111000
{
    public class S11000011 : Event
    {
        public S11000011()
        {
            this.EventID = 11000011;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000011, 131, "您好，这里是行会宫殿2楼!$R;" +
                                   "$P北边是弓箭手总管的房间。$R;" +
                                   "东边是剑士总管的房间。$R;" +
                                   "南边是盗贼总管的房间。$R;" +
                                   "西边是骑士总管的房间。$R;", "行会宫殿向导");
        }
    }
}
