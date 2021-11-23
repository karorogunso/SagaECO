using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:豆豆(13000066) X:98 Y:158
namespace SagaScript.M10024000
{
    public class S13000066 : Event
    {
        public S13000066()
        {
            this.EventID = 13000066;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
