using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:主人(11000406) X:136 Y:50
namespace SagaScript.M10024000
{
    public class S11000406 : Event
    {
        public S11000406()
        {
            this.EventID = 11000406;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
