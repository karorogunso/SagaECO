using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:埃米爾少女的支持者(13000059) X:128 Y:139
namespace SagaScript.M10023000
{
    public class S13000059 : Event
    {
        public S13000059()
        {
            this.EventID = 13000059;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
