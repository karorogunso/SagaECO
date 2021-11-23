using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:劇場門廊4(30183000) NPC基本信息:劇場總管(11000263) X:11 Y:7
namespace SagaScript.M30183000
{
    public class S11000263 : Event
    {
        public S11000263()
        {
            this.EventID = 11000263;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
