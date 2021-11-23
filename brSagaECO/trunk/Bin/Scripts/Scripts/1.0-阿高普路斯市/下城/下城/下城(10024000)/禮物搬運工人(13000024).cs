using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:禮物搬運工人(13000024) X:141 Y:185
namespace SagaScript.M10024000
{
    public class S13000024 : Event
    {
        public S13000024()
        {
            this.EventID = 13000024;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
