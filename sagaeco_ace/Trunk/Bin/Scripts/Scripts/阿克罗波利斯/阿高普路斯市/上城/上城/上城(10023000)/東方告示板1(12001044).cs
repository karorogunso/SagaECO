using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:東方告示板1(12001044) X:184 Y:126
namespace SagaScript.M10023000
{
    public class S12001044 : Event
    {
        public S12001044()
        {
            this.EventID = 12001044;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
