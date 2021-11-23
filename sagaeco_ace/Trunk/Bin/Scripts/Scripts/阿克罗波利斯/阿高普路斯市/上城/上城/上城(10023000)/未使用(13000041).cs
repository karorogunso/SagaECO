using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:未使用(13000041) X:126 Y:117
namespace SagaScript.M10023000
{
    public class S13000041 : Event
    {
        public S13000041()
        {
            this.EventID = 13000041;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
