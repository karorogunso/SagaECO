using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:路利耶(13000213) X:135 Y:138
namespace SagaScript.M10023000
{
    public class S13000213 : Event
    {
        public S13000213()
        {
            this.EventID = 13000213;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
