using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:染色店阿姨(11000765) X:87 Y:61
namespace SagaScript.M10024000
{
    public class S11000765 : Event
    {
        public S11000765()
        {
            this.EventID = 11000765;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
