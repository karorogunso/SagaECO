using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:ECO温泉(31303000)NPC基本信息:11001870-知识广博的德拜- X:33 Y:23
namespace SagaScript.M31303000
{
    public class S11001870 : Event
    {
    public S11001870()
        {
            this.EventID = 11001870;
        }


        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
