using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:行會宮殿4樓(30113000) NPC基本信息:行會宮殿嚮導(11000013) X:10 Y:16
namespace SagaScript.M30113000
{
    public class S11000013 : Event
    {
        public S11000013()
        {
            this.EventID = 11000013;
        }


        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000013, 131, "您好，这里是行会宫殿4楼!$R;" +
                                   "$P北边是商人总管的房间。$R;" +
                                   "东边是炼金术师总管的房间。$R;" +
                                   "南边是冒险家总管的房间。$R;" +
                                   "西边是人偶师总管的房间。$R;", "行会宫殿向导");
        }
    }
}
