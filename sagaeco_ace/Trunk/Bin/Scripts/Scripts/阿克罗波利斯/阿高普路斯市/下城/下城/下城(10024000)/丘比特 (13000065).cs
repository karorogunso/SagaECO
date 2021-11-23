using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:丘比特 (13000065) X:102 Y:161
namespace SagaScript.M10024000
{
    public class S13000065 : Event
    {
        public S13000065()
        {
            this.EventID = 13000065;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
