using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:泰迪寶貝(11000716) X:144 Y:152
namespace SagaScript.M10071000
{
    public class S11000716 : Event
    {
        public S11000716()
        {
            this.EventID = 11000716;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000716, 131, "嘉嘉…$R;", "泰迪宝贝");
        }
    }
}




