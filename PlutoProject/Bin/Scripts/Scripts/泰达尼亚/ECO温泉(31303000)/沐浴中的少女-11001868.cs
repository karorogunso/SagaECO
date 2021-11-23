using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:ECO温泉(31303000)NPC基本信息:11001868-沐浴中的少女- X:55 Y:78
namespace SagaScript.M31303000
{
    public class S11001868 : Event
    {
    public S11001868()
        {
            this.EventID = 11001868;
        }


        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
