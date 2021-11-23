using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:鬥技場2(20080011) NPC基本信息:布丁(11000628) X:25 Y:25
namespace SagaScript.M20080011
{
    public class S11000628 : Event
    {
        public S11000628()
        {
            this.EventID = 11000628;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
