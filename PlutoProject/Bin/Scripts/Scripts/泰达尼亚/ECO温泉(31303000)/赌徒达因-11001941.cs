using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:ECO温泉(31303000)NPC基本信息:11001941-赌徒达因- X:48 Y:75
namespace SagaScript.M31303000
{
    public class S11001941 : Event
    {
    public S11001941()
        {
            this.EventID = 11001941;
        }


        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
