using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:行會宮殿4樓(30113000) NPC基本信息:技能商店(12001072) X:15 Y:24
namespace SagaScript.M30113000
{
    public class S12001072 : Event
    {
        public S12001072()
        {
            this.EventID = 12001072;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
