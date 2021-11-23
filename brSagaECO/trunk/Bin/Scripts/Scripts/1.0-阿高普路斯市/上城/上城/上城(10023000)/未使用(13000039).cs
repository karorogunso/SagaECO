using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:未使用(13000039) X:146 Y:131
namespace SagaScript.M10023000
{
    public class S13000039 : Event
    {
        public S13000039()
        {
            this.EventID = 13000039;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
