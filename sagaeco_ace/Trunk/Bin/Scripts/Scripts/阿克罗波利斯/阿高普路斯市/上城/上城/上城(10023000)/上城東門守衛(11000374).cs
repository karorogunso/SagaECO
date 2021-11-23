using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:上城東門守衛(11000374) X:222 Y:126
namespace SagaScript.M10023000
{
    public class S11000374 : Event
    {
        public S11000374()
        {
            this.EventID = 11000374;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
