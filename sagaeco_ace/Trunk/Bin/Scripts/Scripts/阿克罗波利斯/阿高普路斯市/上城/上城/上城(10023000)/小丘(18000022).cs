using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:小丘(18000022) X:127 Y:139
namespace SagaScript.M10023000
{
    public class S18000022 : Event
    {
        public S18000022()
        {
            this.EventID = 18000022;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
