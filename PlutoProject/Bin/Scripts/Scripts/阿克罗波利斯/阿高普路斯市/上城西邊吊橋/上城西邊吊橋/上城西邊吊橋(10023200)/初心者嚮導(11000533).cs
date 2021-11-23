using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城西邊吊橋(10023200) NPC基本信息:初心者嚮導(11000533) X:8 Y:131
namespace SagaScript.M10023200
{
    public class S11000533 : Event
    {
        public S11000533()
        {
            this.EventID = 11000533;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
