using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:塗鴉機械(12001009) X:91 Y:134
namespace SagaScript.M10024000
{
    public class S12001009 : Event
    {
        public S12001009()
        {
            this.EventID = 12001009;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
