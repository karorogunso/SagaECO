using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:副幹部美西亞(18000175) X:19 Y:102
namespace SagaScript.M10071000
{
    public class S18000175 : Event
    {
        public S18000175()
        {
            this.EventID = 18000175;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}