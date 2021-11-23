using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:露天倉庫屋商人(13000113) X:133 Y:50
namespace SagaScript.M10024000
{
    public class S13000113 : Event
    {
        public S13000113()
        {
            this.EventID = 13000113;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
