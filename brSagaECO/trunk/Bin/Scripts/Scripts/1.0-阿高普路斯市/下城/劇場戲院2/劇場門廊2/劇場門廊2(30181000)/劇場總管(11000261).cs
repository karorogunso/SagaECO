using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:劇場門廊2(30181000) NPC基本信息:劇場總管(11000261) X:11 Y:7
namespace SagaScript.M30181000
{
    public class S11000261 : Event
    {
        public S11000261()
        {
            this.EventID = 11000261;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
