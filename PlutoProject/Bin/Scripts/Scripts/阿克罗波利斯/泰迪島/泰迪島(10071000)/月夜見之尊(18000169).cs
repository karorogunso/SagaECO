using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:月夜見之尊(18000169) X:000 Y:000
namespace SagaScript.M10071000
{
    public class S18000169 : Event
    {
        public S18000169()
        {
            this.EventID = 18000169;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}