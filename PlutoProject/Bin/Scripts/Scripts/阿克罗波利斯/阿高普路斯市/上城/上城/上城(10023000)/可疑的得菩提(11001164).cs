using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:可疑的得菩提(11001164) X:128 Y:153
namespace SagaScript.M10023000
{
    public class S11001164 : Event
    {
        public S11001164()
        {
            this.EventID = 11001164;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
