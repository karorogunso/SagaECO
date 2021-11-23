using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:提多少女的支持者(13000075) X:140 Y:125
namespace SagaScript.M10023000
{
    public class S13000075 : Event
    {
        public S13000075()
        {
            this.EventID = 13000075;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
