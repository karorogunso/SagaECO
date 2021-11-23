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
            Say(pc, 11000013, 131, "您好，這裡是行會宮殿4樓唷!$R;" +
                                   "$P北邊是商人總管的房間。$R;" +
                                   "東邊是鍊金術師總管的房間。$R;" +
                                   "南邊是冒險家總管的房間。$R;" +
                                   "西邊是木偶使總管的房間。$R;", "行會宮殿嚮導");
        }
    }
}
