using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:仰望故鄉的少年(11001245) X:209 Y:199
namespace SagaScript.M10071000
{
    public class S11001245 : Event
    {
        public S11001245()
        {
            this.EventID = 11001245;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}