using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:染色店阿姨(11000403) X:192 Y:84
namespace SagaScript.M10024000
{
    public class S11000403 : Event
    {
        public S11000403()
        {
            this.EventID = 11000403;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
