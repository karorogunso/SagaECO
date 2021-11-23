using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:原居民泰迪(11000708) X:153 Y:91
namespace SagaScript.M10071000
{
    public class S11000708 : Event
    {
        public S11000708()
        {
            this.EventID = 11000708;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000708, 131, "这里开始是海盗的势力范围。$R;" +
                                   "$R您不能进去喔!!$R;", "原居民泰迪");
        }
    }
}




