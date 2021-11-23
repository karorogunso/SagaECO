using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:ECO温泉(31303000)NPC基本信息:11001872-容光焕发的少女- X:40 Y:57
namespace SagaScript.M31303000
{
    public class S11001872 : Event
    {
    public S11001872()
        {
            this.EventID = 11001872;
        }


        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
