using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:南方告示板1(12001042) X:129 Y:188
namespace SagaScript.M10023000
{
    public class S12001042 : Event
    {
        public S12001042()
        {
            this.EventID = 12001042;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
