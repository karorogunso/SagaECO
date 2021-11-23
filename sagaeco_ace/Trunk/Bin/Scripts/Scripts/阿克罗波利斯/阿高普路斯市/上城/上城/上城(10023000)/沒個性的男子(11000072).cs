using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:沒個性的男子(11000072) X:116 Y:205
namespace SagaScript.M10023000
{
    public class S11000072 : Event
    {
        public S11000072()
        {
            this.EventID = 11000072;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
