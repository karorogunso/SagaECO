using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:微微(13000073) X:138 Y:128
namespace SagaScript.M10023000
{
    public class S13000073 : Event
    {
        public S13000073()
        {
            this.EventID = 13000073;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
