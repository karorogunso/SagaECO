using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:休息中的泰迪(11000738) X:63 Y:214
namespace SagaScript.M10071000
{
    public class S11000738 : Event
    {
        public S11000738()
        {
            this.EventID = 11000738;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000738, 131, "唉，休息一會兒吧。$R;", "休息中的泰迪");
        }
    }
}




