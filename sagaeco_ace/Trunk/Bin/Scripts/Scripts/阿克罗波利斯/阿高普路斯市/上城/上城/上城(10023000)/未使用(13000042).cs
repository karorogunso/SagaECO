using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:未使用(13000042) X:119 Y:138
namespace SagaScript.M10023000
{
    public class S13000042 : Event
    {
        public S13000042()
        {
            this.EventID = 13000042;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
