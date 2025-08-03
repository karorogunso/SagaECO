using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:出租書店主人(18000164) X:000 Y:000
namespace SagaScript.M10071000
{
    public class S18000164 : Event
    {
        public S18000164()
        {
            this.EventID = 18000164;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}