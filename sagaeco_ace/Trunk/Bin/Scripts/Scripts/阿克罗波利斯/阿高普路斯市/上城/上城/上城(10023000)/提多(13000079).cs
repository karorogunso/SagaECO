using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:提多(13000079) X:138 Y:127
namespace SagaScript.M10023000
{
    public class S13000079 : Event
    {
        public S13000079()
        {
            this.EventID = 13000079;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
