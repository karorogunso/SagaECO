using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:嚮導機械人(11000340) X:206 Y:126
namespace SagaScript.M10024000
{
    public class S11000340 : Event
    {
        public S11000340()
        {
            this.EventID = 11000340;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
