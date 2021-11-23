using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:ECO温泉(31303000)NPC基本信息:11001867-沐浴中的少年- X:5 Y:78
namespace SagaScript.M31303000
{
    public class S11001867 : Event
    {
    public S11001867()
        {
            this.EventID = 11001867;
        }


        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
