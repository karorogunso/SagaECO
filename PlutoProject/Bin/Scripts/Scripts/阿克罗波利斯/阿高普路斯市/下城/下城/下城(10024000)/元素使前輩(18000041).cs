using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:元素使前輩(18000041) X:168 Y:138
namespace SagaScript.M10024000
{
    public class S18000041 : Event
    {
        public S18000041()
        {
            this.EventID = 18000041;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
