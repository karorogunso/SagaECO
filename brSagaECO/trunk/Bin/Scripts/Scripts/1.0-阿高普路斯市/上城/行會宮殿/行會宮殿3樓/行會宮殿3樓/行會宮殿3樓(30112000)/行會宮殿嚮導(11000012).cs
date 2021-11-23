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
            Say(pc, 11000012, 131, "您好，這裡是行會宮殿3樓唷!$R;" +
                                   "$P北邊是農夫總管的房間。$R;" +
                                   "東邊是魔法系總管的房間。$R;" +
                                   "南邊是機械師總管的房間。$R;" +
                                   "西邊是礦工總管的房間。$R;", "行會宮殿嚮導");
        }
    }
}
