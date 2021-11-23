using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:黑之聖堂(30121000) NPC基本信息:技能商店(12001079) X:19 Y:6
namespace SagaScript.M30121000
{
    public class S12001079 : Event
    {
        public S12001079()
        {
            this.EventID = 12001079;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
