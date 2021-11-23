using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:地庫南方告示板1(12001159) X:137 Y:167
namespace SagaScript.M10024000
{
    public class S12001159 : Event
    {
        public S12001159()
        {
            this.EventID = 12001159;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
