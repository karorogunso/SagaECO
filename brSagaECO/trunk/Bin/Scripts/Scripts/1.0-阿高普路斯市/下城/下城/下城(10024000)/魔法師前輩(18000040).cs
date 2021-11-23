using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:魔法師前輩(18000040) X:170 Y:138
namespace SagaScript.M10024000
{
    public class S18000040 : Event
    {
        public S18000040()
        {
            this.EventID = 18000040;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
