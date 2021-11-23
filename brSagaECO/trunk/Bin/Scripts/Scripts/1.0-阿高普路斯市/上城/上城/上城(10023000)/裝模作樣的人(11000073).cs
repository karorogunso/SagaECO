using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:裝模作樣的人(11000073) X:197 Y:145
namespace SagaScript.M10023000
{
    public class S11000073 : Event
    {
        public S11000073()
        {
            this.EventID = 11000073;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
