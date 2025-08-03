using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:原居民泰迪(11000711) X:148 Y:155
namespace SagaScript.M10071000
{
    public class S11000711 : Event
    {
        public S11000711()
        {
            this.EventID = 11000711;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000711, 131, "信不信我揍你?$R;", "原居民泰迪");
        }
    }
}




