using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城南邊吊橋(10023300) NPC基本信息:初心者嚮導(11000534) X:131 Y:247
namespace SagaScript.M10023300
{
    public class S11000534 : Event
    {
        public S11000534()
        {
            this.EventID = 11000534;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
