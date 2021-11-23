using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞大陸廢墟(50032000) NPC基本信息:路標(18000188)
namespace SagaScript.M50032000
{
    public class S18000188 : Event
    {
        public S18000188()
        {
            this.EventID = 18000188;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}