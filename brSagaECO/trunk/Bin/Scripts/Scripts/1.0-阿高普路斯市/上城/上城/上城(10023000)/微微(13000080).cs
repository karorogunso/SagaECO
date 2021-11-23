using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:微微(13000080) X:138 Y:128
namespace SagaScript.M10023000
{
    public class S13000080 : Event
    {
        public S13000080()
        {
            this.EventID = 13000080;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
