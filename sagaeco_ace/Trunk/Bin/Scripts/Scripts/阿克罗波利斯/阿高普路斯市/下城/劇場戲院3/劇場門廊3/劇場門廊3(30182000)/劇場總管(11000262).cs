using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:劇場門廊3(30182000) NPC基本信息:劇場總管(11000262) X:11 Y:7
namespace SagaScript.M30182000
{
    public class S11000262 : Event
    {
        public S11000262()
        {
            this.EventID = 11000262;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
