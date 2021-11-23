using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:埃米爾少女的支持者(13000074) X:116 Y:130
namespace SagaScript.M10023000
{
    public class S13000074 : Event
    {
        public S13000074()
        {
            this.EventID = 13000074;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
