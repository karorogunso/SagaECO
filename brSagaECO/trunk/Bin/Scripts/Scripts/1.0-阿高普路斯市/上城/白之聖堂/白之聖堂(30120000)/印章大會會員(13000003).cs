using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:白之聖堂(30120000) NPC基本信息:印章大會會員(13000003) X:15 Y:8
namespace SagaScript.M30120000
{
    public class S13000003 : Event
    {
        public S13000003()
        {
            this.EventID = 13000003;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
