using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:原居民泰迪(11000712) X:144 Y:149
namespace SagaScript.M10071000
{
    public class S11000712 : Event
    {
        public S11000712()
        {
            this.EventID = 11000712;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000712, 131, "您也可以成为原住民泰迪的朋友哦!$R;" +
                                   "$R海盗是很坏很坏的泰迪。$R;", "原居民泰迪");
        }
    }
}




