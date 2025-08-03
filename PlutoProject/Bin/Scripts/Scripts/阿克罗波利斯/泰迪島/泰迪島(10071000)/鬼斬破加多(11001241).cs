using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:鬼斬破加多(11001241) X:27 Y:36
namespace SagaScript.M10071000
{
    public class S11001241 : Event
    {
        public S11001241()
        {
            this.EventID = 11001241;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}