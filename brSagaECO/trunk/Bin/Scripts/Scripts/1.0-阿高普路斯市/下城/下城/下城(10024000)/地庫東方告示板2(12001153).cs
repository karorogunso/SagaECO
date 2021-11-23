using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:地庫東方告示板2(12001153) X:167 Y:136
namespace SagaScript.M10024000
{
    public class S12001153 : Event
    {
        public S12001153()
        {
            this.EventID = 12001153;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
