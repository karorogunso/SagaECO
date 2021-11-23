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
            Say(pc, 11000011, 131, "您好，這裡是行會宮殿2樓唷!$R;" +
                                   "$P北邊是弓手總管的房間。$R;" +
                                   "東邊是劍士總管的房間。$R;" +
                                   "南邊是盜賊總管的房間。$R;" +
                                   "西邊有騎士總管的房間。$R;", "行會宮殿嚮導");
        }
    }
}
