using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:上城南門守衛(11000376) X:129 Y:223
namespace SagaScript.M10023000
{
    public class S11000376 : Event
    {
        public S11000376()
        {
            this.EventID = 11000376;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
