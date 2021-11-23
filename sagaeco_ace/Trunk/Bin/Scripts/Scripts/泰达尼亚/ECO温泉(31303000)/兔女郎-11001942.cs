using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:ECO温泉(31303000)NPC基本信息:11001942-兔女郎- X:44 Y:77
namespace SagaScript.M31303000
{
    public class S11001942 : Event
    {
    public S11001942()
        {
            this.EventID = 11001942;
        }


        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
