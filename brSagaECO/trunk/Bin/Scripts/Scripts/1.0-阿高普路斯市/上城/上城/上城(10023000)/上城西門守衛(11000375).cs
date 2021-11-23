using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:上城西門守衛(11000375) X:33 Y:129
namespace SagaScript.M10023000
{
    public class S11000375 : Event
    {
        public S11000375()
        {
            this.EventID = 11000375;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
