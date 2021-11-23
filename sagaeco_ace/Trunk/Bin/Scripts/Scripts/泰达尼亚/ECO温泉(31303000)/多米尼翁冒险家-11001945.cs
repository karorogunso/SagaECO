using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:ECO温泉(31303000)NPC基本信息:11001945-多米尼翁冒险家- X:49 Y:4
namespace SagaScript.M31303000
{
    public class S11001945 : Event
    {
    public S11001945()
        {
            this.EventID = 11001945;
        }


        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
