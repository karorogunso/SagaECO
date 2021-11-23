using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:格伊瑞(12001121) X:41 Y:196
namespace SagaScript.M10071000
{
    public class S12001121 : Event
    {
        public S12001121()
        {
            this.EventID = 12001121;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}