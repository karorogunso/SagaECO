using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:帳篷(18000047) X:167 Y:121
namespace SagaScript.M10024000
{
    public class S18000047 : Event
    {
        public S18000047()
        {
            this.EventID = 18000047;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
