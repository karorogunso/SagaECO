using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:機械師行會(30046000) NPC基本信息:機械師總管(11000021) X:3 Y:3
namespace SagaScript.M30046000
{
    public class S11000021 : Event
    {
        public S11000021()
        {
            this.EventID = 11000021;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
