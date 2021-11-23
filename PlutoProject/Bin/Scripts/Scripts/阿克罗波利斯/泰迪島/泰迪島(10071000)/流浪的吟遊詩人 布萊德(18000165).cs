using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:流浪的吟遊詩人 布萊德(18000165) X:000 Y:000
namespace SagaScript.M10071000
{
    public class S18000165 : Event
    {
        public S18000165()
        {
            this.EventID = 18000165;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}