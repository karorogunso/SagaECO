using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:海盜泰迪(11000704) X:63 Y:98
namespace SagaScript.M10071000
{
    public class S11000704 : Event
    {
        public S11000704()
        {
            this.EventID = 11000704;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000704, 131, "我们坐船来到了这里，$R;" +
                                   "$P那边就是我们「海盗泰迪」的大本营，$R;" +
                                   "不要靠近，不然会出大事的。$R;", "海盗泰迪");
        }
    }
}




