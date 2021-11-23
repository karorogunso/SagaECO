using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:性感的女孩(11000075) X:97 Y:73
namespace SagaScript.M10023000
{
    public class S11000075 : Event
    {
        public S11000075()
        {
            this.EventID = 11000075;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
