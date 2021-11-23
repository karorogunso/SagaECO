using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:ECO温泉(31303000)NPC基本信息:11001943-艾米尔冒险家- X:48 Y:4
namespace SagaScript.M31303000
{
    public class S11001943 : Event
    {
    public S11001943()
        {
            this.EventID = 11001943;
        }


        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
