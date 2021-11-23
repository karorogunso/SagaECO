using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:劇場門廊4(30183000) NPC基本信息:劇場小姐(11000163) X:1 Y:10
namespace SagaScript.M30183000
{
    public class S11000163 : Event
    {
        public S11000163()
        {
            this.EventID = 11000163;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
