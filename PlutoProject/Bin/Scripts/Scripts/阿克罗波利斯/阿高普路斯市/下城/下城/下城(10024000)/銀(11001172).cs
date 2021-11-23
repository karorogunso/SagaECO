using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:下城(10024000) NPC基本信息:銀(11001172) X:132 Y:97
namespace SagaScript.M10024000
{
    public class S11001172 : Event
    {
        public S11001172()
        {
            this.EventID = 11001172;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
