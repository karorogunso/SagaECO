using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:劍士前輩(18000036) X:200 Y:146
namespace SagaScript.M10024000
{
    public class S18000036 : Event
    {
        public S18000036()
        {
            this.EventID = 18000036;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
