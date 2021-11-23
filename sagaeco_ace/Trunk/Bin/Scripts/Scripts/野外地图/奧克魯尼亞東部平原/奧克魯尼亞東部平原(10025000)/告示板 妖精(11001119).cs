using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞東部平原(10025000) NPC基本信息:告示板 妖精(11001119)X:102 Y:120
namespace SagaScript.M10025000
{
    public class S11001119 : Event
    {
        public S11001119()
        {
            this.EventID = 11001119;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
