using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:泰迪寶貝(11000715) X:141 Y:156
namespace SagaScript.M10071000
{
    public class S11000715 : Event
    {
        public S11000715()
        {
            this.EventID = 11000715;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 11000715, 131, "阿巴巴…$R;", "泰迪宝贝");
        }
    }
}




