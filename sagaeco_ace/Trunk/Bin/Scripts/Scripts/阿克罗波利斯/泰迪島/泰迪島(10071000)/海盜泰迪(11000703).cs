using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:海盜泰迪(11000703) X:129 Y:85
namespace SagaScript.M10071000
{
    public class S11000703 : Event
    {
        public S11000703()
        {
            this.EventID = 11000703;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000703, 131, "你是原住民的间谍吧?$R;" +
                                   "$R不能让您通过!!$R;", "海盗泰迪");
        }
    }
}




