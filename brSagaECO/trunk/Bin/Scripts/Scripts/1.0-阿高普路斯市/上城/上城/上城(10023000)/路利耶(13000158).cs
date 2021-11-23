using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:路利耶(13000158) X:116 Y:128
namespace SagaScript.M10023000
{
    public class S13000158 : Event
    {
        public S13000158()
        {
            this.EventID = 13000158;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
