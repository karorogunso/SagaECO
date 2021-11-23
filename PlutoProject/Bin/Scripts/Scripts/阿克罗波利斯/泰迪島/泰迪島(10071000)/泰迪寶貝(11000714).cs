using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:泰迪寶貝(11000714) X:140 Y:155
namespace SagaScript.M10071000
{
    public class S11000714 : Event
    {
        public S11000714()
        {
            this.EventID = 11000714;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000714, 131, "猛麻…猛麻…$R;", "泰迪宝贝");
        }
    }
}