using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:劇場門廊1(30180000) NPC基本信息:劇場總管(11000191) X:11 Y:7
namespace SagaScript.M30180000
{
    public class S11000191 : Event
    {
        public S11000191()
        {
            this.EventID = 11000191;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
