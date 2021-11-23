using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:魔物印章告示板(12001066) X:149 Y:162
namespace SagaScript.M10023000
{
    public class S12001066 : Event
    {
        public S12001066()
        {
            this.EventID = 12001066;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
