using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地图:ECO温泉(31303000)NPC基本信息:11001946-有趣的男人- X:5 Y:12
namespace SagaScript.M31303000
{
    public class S11001946 : Event
    {
    public S11001946()
        {
            this.EventID = 11001946;
        }


        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
