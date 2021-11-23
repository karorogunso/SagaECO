using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:聖誕老人的雪橇(13000015) X:126 Y:140
namespace SagaScript.M10023000
{
    public class S13000015 : Event
    {
        public S13000015()
        {
            this.EventID = 13000015;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
