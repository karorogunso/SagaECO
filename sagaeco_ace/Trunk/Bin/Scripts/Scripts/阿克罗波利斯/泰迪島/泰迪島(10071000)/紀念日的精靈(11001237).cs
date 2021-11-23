using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:紀念日的精靈(11001237) X:206 Y:142
namespace SagaScript.M10071000
{
    public class S11001237 : Event
    {
        public S11001237()
        {
            this.EventID = 11001237;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}