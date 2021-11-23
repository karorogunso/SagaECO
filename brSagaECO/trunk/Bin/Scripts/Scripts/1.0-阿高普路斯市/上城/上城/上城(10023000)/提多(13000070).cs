using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:提多(13000070) X:138 Y:127
namespace SagaScript.M10023000
{
    public class S13000070 : Event
    {
        public S13000070()
        {
            this.EventID = 13000070;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
