using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:ECO温泉(31303000)NPC基本信息:11001947-猫灵（白子）- X:39 Y:35
namespace SagaScript.M31303000
{
    public class S11001947 : Event
    {
    public S11001947()
        {
            this.EventID = 11001947;
        }


        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
