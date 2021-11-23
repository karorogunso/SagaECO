using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞東部平原(10025000) NPC基本信息:初心者告示板(12001064) X:104 Y:120
namespace SagaScript.M10025000
{
    public class S12001064 : Event
    {
        public S12001064()
        {
            this.EventID = 12001064;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
