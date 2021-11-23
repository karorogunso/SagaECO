using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:東部平原初心者學校(30091005) NPC基本信息:初心者嚮導(11001013) X:9 Y:3
namespace SagaScript.M30091005
{
    public class S11001013 : Event
    {
        public S11001013()
        {
            this.EventID = 11001013;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
