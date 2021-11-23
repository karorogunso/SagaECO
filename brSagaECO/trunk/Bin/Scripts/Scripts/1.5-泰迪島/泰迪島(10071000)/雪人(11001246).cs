using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:雪人(11001246) X:17 Y:144
namespace SagaScript.M10071000
{
    public class S11001246 : Event
    {
        public S11001246()
        {
            this.EventID = 11001246;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}