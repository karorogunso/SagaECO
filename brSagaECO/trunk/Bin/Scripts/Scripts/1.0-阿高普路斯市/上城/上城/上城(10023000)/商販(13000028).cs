using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:商販(13000028) X:126 Y:138
namespace SagaScript.M10023000
{
    public class S13000028 : Event
    {
        public S13000028()
        {
            this.EventID = 13000028;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
