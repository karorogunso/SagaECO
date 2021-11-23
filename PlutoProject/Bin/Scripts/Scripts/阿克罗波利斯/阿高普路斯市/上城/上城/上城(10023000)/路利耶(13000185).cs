using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:路利耶(13000185) X:107 Y:122
namespace SagaScript.M10023000
{
    public class S13000185 : Event
    {
        public S13000185()
        {
            this.EventID = 13000185;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
