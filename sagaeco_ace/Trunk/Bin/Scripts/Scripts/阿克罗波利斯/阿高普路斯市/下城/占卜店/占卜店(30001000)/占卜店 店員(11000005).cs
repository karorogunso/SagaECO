using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:占卜店(30001000) NPC基本信息:占卜店 店員(11000005) X:2 Y:1
namespace SagaScript.M30001000
{
    public class S11000005 : Event
    {
        public S11000005()
        {
            this.EventID = 11000005;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
