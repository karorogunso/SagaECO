using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:路迪(13000154) X:129 Y:117
namespace SagaScript.M10023000
{
    public class S13000154 : Event
    {
        public S13000154()
        {
            this.EventID = 13000154;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
