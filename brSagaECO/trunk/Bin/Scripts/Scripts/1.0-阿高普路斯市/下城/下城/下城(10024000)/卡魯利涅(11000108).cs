using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:卡魯利涅(11000108) X:72 Y:76
namespace SagaScript.M10024000
{
    public class S11000108 : Event
    {
        public S11000108()
        {
            this.EventID = 11000108;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
