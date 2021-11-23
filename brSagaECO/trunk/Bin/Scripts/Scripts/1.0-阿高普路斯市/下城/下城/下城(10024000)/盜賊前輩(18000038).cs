using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:盜賊前輩(18000038) X:201 Y:151
namespace SagaScript.M10024000
{
    public class S18000038 : Event
    {
        public S18000038()
        {
            this.EventID = 18000038;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
