using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:未使用(13000040) X:108 Y:131
namespace SagaScript.M10023000
{
    public class S13000040 : Event
    {
        public S13000040()
        {
            this.EventID = 13000040;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
