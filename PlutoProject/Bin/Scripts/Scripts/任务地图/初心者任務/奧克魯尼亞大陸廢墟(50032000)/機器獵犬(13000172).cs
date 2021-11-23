using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞大陸廢墟(50032000) NPC基本信息:機器獵犬(13000172) X:103 Y:35
namespace SagaScript.M50032000
{
    public class S13000172 : Event
    {
        public S13000172()
        {
            this.EventID = 13000172;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
