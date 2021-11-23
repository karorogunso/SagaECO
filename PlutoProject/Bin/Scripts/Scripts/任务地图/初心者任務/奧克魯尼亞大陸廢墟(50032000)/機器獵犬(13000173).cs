using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞大陸廢墟(50032000) NPC基本信息:機器獵犬(13000173) X:106 Y:14
namespace SagaScript.M50032000
{
    public class S13000173 : Event
    {
        public S13000173()
        {
            this.EventID = 13000173;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
