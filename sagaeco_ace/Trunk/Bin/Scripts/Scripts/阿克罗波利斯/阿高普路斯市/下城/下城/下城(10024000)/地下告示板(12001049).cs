using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:地下告示板(12001049) X:88 Y:118
namespace SagaScript.M10024000
{
    public class S12001049 : Event
    {
        public S12001049()
        {
            this.EventID = 12001049;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
