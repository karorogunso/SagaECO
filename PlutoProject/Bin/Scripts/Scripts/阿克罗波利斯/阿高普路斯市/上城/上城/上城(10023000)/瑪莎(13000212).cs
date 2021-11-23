using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:瑪莎(13000212) X:133 Y:138
namespace SagaScript.M10023000
{
    public class S13000212 : Event
    {
        public S13000212()
        {
            this.EventID = 13000212;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
