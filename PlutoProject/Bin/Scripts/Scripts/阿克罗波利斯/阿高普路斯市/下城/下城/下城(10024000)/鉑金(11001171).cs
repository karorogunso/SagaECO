using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:鉑金(11001171) X:132 Y:99
namespace SagaScript.M10024000
{
    public class S11001171 : Event
    {
        public S11001171()
        {
            this.EventID = 11001171;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
