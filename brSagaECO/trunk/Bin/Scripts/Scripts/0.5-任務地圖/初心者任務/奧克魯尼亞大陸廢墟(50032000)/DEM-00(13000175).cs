using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:奧克魯尼亞大陸廢墟(50032000) NPC基本信息:DEM-00(13000175) X:109 Y:21
namespace SagaScript.M50032000
{
    public class S13000175 : Event
    {
        public S13000175()
        {
            this.EventID = 13000175;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
