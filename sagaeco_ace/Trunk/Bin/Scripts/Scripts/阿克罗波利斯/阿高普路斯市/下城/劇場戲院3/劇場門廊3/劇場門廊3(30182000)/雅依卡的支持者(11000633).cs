using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:劇場門廊3(30182000) NPC基本信息:雅依卡的支持者(11000633) X:16 Y:10
namespace SagaScript.M30182000
{
    public class S11000633 : Event
    {
        public S11000633()
        {
            this.EventID = 11000633;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
