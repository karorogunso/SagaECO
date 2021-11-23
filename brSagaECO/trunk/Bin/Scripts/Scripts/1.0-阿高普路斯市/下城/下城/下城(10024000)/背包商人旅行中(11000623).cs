using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:背包商人旅行中(11000623) X:185 Y:165
namespace SagaScript.M10024000
{
    public class S11000623 : Event
    {
        public S11000623()
        {
            this.EventID = 11000623;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
