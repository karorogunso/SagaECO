using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:鬼小僧(11001247) X:14 Y:144
namespace SagaScript.M10071000
{
    public class S11001247 : Event
    {
        public S11001247()
        {
            this.EventID = 11001247;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}