using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:月亮(13000130) X:74 Y:134
namespace SagaScript.M10071000
{
    public class S13000130 : Event
    {
        public S13000130()
        {
            this.EventID = 13000130;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}