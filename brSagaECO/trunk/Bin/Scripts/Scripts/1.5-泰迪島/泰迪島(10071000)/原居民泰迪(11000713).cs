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
            Say(pc, 11000713, 131, "這裡是「原住民泰迪」的村落，$R;" +
                                   "從以前就在這座島生活了。$R;" +
                                   "$P這座島是我們的土地，$R;" +
                                   "成為我們的朋友來玩吧。$R;", "原居民泰迪");
        }
    }
}




