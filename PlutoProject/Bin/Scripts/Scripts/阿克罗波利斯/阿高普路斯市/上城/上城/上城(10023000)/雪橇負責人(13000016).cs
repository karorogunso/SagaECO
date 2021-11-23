using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:雪橇負責人(13000016) X:129 Y:139
namespace SagaScript.M10023000
{
    public class S13000016 : Event
    {
        public S13000016()
        {
            this.EventID = 13000016;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
