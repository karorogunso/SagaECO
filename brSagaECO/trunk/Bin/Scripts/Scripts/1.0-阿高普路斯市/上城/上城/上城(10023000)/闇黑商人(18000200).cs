using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:闇黑商人(18000200) X:133 Y:147
namespace SagaScript.M10023000
{
    public class S18000200 : Event
    {
        public S18000200()
        {
            this.EventID = 18000200;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
