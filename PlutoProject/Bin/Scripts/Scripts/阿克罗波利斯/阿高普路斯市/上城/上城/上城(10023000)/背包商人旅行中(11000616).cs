using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:背包商人旅行中(11000616) X:70 Y:105
namespace SagaScript.M10023000
{
    public class S11000616 : Event
    {
        public S11000616()
        {
            this.EventID = 11000616;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
