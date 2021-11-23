using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:未使用(13000043) X:147 Y:131
namespace SagaScript.M10023000
{
    public class S13000043 : Event
    {
        public S13000043()
        {
            this.EventID = 13000043;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
