using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:原居民泰迪(11000713) X:150 Y:143
namespace SagaScript.M10071000
{
    public class S11000713 : Event
    {
        public S11000713()
        {
            this.EventID = 11000713;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000713, 131, "这里是「原住民泰迪」的村落，$R;" +
                                   "从以前就在这座岛生活了。$R;" +
                                   "$P这座岛是我们的土地，$R;" +
                                   "成为我们的朋友来玩吧。$R;", "原居民泰迪");
        }
    }
}




