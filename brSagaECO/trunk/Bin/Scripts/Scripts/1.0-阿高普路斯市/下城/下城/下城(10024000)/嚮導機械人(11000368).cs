using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:嚮導機械人(11000368) X:130 Y:48
namespace SagaScript.M10024000
{
    public class S11000368 : Event
    {
        public S11000368()
        {
            this.EventID = 11000368;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
