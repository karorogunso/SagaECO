using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:地下告示板(12001050) X:118 Y:167
namespace SagaScript.M10024000
{
    public class S12001050 : Event
    {
        public S12001050()
        {
            this.EventID = 12001050;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
