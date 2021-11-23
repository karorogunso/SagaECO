using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:？？？的房間(30055000) NPC基本信息:小丘比(11000032) X:3 Y:3
namespace SagaScript.M30055000
{
    public class S11000032 : Event
    {
        public S11000032()
        {
            this.EventID = 11000032;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
