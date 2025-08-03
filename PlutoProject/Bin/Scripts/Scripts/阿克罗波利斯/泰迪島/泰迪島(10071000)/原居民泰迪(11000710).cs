using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:原居民泰迪(11000710) X:155 Y:92
namespace SagaScript.M10071000
{
    public class S11000710 : Event
    {
        public S11000710()
        {
            this.EventID = 11000710;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000710, 131, "桥的另一端由坏小子们占领了。$R;" +
                                   "$R原住民泰迪总有一天会收复的。$R;", "原居民泰迪");
        }
    }
}




