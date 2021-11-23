using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:礦工前輩(18000044) X:169 Y:118
namespace SagaScript.M10024000
{
    public class S18000044 : Event
    {
        public S18000044()
        {
            this.EventID = 18000044;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
