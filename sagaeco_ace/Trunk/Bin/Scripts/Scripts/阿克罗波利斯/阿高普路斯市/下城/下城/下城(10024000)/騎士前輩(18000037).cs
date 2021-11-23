using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:騎士前輩(18000037) X:199 Y:150
namespace SagaScript.M10024000
{
    public class S18000037 : Event
    {
        public S18000037()
        {
            this.EventID = 18000037;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
