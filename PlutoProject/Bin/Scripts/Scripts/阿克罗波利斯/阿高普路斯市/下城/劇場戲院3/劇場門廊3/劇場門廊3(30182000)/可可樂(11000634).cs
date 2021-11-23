using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:劇場門廊3(30182000) NPC基本信息:可可樂(11000634) X:16 Y:11
namespace SagaScript.M30182000
{
    public class S11000634 : Event
    {
        public S11000634()
        {
            this.EventID = 11000634;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
