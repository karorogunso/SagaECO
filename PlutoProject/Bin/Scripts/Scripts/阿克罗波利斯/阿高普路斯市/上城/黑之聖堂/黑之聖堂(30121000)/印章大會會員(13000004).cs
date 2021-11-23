using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:黑之聖堂(30121000) NPC基本信息:印章大會會員(13000004) X:15 Y:8
namespace SagaScript.M30121000
{
    public class S13000004 : Event
    {
        public S13000004()
        {
            this.EventID = 13000004;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
