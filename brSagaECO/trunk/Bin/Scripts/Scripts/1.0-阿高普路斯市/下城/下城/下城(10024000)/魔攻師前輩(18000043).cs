using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:魔攻師前輩(18000043) X:169 Y:142
namespace SagaScript.M10024000
{
    public class S18000043 : Event
    {
        public S18000043()
        {
            this.EventID = 18000043;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
