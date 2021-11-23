using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:休息中的泰迪(11000717) X:59 Y:212
namespace SagaScript.M10071000
{
    public class S11000717 : Event
    {
        public S11000717()
        {
            this.EventID = 11000717;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000717, 131, "沒看我在看海嗎?$R;" +
                                   "$R走開走開…$R;", "休息中的泰迪");
        }
    }
}




