using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:阿諾米奈(18000010) X:200 Y:130
namespace SagaScript.M10024000
{
    public class S18000010 : Event
    {
        public S18000010()
        {
            this.EventID = 18000010;
        }

        public override void OnEvent(ActorPC pc)
        {
            Select(pc, "", "", "", "");
        }
    }
}
