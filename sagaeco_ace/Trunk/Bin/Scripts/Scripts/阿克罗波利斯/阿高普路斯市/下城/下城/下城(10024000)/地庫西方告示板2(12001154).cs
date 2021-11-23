using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:地庫西方告示板2(12001154) X:88 Y:118
namespace SagaScript.M10024000
{
    public class S12001154 : Event
    {
        public S12001154()
        {
            this.EventID = 12001154;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
