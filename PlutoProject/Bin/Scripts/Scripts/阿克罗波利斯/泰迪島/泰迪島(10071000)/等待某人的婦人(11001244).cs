using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:等待某人的婦人(11001244) X:209 Y:196
namespace SagaScript.M10071000
{
    public class S11001244 : Event
    {
        public S11001244()
        {
            this.EventID = 11001244;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}