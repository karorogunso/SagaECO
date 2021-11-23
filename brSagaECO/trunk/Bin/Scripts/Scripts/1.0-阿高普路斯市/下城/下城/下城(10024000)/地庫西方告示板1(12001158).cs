using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:地庫西方告示板1(12001158) X:88 Y:136
namespace SagaScript.M10024000
{
    public class S12001158 : Event
    {
        public S12001158()
        {
            this.EventID = 12001158;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
