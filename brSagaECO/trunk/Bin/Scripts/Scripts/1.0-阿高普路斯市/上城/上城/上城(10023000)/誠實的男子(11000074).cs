using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:誠實的男子(11000074) X:117 Y:85
namespace SagaScript.M10023000
{
    public class S11000074 : Event
    {
        public S11000074()
        {
            this.EventID = 11000074;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
