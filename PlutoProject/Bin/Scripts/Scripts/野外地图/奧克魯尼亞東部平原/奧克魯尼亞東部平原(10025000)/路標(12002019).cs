using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞東部平原(10025000) NPC基本信息:路標(12002019) X:62 Y:253
namespace SagaScript.M10025000
{
    public class S12002019 : Event
    {
        public S12002019()
        {
            this.EventID = 12002019;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
