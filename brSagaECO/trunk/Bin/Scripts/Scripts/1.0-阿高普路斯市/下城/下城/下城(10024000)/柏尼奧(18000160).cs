using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:柏尼奧(18000160) X:137 Y:124
namespace SagaScript.M10024000
{
    public class S18000160 : Event
    {
        public S18000160()
        {
            this.EventID = 18000160;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
