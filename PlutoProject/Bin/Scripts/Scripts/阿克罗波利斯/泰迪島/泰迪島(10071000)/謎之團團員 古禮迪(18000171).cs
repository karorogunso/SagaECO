using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:謎之團團員 古禮迪(18000171) X:80 Y:50
namespace SagaScript.M10071000
{
    public class S18000171 : Event
    {
        public S18000171()
        {
            this.EventID = 18000171;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}