using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:告示板電路機械(18000151) X:128 Y:153
namespace SagaScript.M10023000
{
    public class S18000151 : Event
    {
        public S18000151()
        {
            this.EventID = 18000151;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
