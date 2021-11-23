using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:農夫前輩(18000045) X:167 Y:117
namespace SagaScript.M10024000
{
    public class S18000045 : Event
    {
        public S18000045()
        {
            this.EventID = 18000045;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
