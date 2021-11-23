using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:劇場門廊3(30182000) NPC基本信息:劇場小姐(11000162) X:1 Y:10
namespace SagaScript.M30182000
{
    public class S11000162 : Event
    {
        public S11000162()
        {
            this.EventID = 11000162;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
