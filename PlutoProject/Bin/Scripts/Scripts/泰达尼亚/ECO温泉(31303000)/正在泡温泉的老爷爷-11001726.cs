using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:ECO温泉(31303000)NPC基本信息:11001726-正在泡温泉的老爷爷- X:38 Y:35
namespace SagaScript.M31303000
{
    public class S11001726 : Event
    {
    public S11001726()
        {
            this.EventID = 11001726;
        }


        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
