using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:埃米爾(13000077) X:117 Y:129
namespace SagaScript.M10023000
{
    public class S13000077 : Event
    {
        public S13000077()
        {
            this.EventID = 13000077;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
