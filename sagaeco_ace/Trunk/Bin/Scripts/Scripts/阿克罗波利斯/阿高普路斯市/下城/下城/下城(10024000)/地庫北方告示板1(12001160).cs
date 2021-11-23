using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:地庫北方告示板1(12001160) X:119 Y:88
namespace SagaScript.M10024000
{
    public class S12001160 : Event
    {
        public S12001160()
        {
            this.EventID = 12001160;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
