using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:1週年紀念碑(11001236) X:206 Y:139
namespace SagaScript.M10071000
{
    public class S11001236 : Event
    {
        public S11001236()
        {
            this.EventID = 11001236;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}