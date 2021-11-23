using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:ECO温泉(31303000)NPC基本信息:11001869-困惑的少女- X:57 Y:56
namespace SagaScript.M31303000
{
    public class S11001869 : Event
    {
    public S11001869()
        {
            this.EventID = 11001869;
        }


        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
