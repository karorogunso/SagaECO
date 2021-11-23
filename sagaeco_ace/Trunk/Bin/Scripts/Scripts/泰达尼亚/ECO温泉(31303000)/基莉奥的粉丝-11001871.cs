using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:ECO温泉(31303000)NPC基本信息:11001871-基莉奥的粉丝- X:32 Y:24
namespace SagaScript.M31303000
{
    public class S11001871 : Event
    {
    public S11001871()
        {
            this.EventID = 11001871;
        }


        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
