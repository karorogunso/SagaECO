using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:海盜泰迪(11000706) X:26 Y:108
namespace SagaScript.M10071000
{
    public class S11000706 : Event
    {
        public S11000706()
        {
            this.EventID = 11000706;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000706, 131, "哎，不能靠近呢!$R;", "海盜泰迪");
        }
    }
}




