using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:提多少女的支持者(13000060) X:140 Y:125
namespace SagaScript.M10023000
{
    public class S13000060 : Event
    {
        public S13000060()
        {
            this.EventID = 13000060;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
