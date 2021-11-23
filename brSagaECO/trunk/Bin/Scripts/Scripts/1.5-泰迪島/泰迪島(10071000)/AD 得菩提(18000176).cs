using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:AD 得菩提(18000176) X:195 Y:100
namespace SagaScript.M10071000
{
    public class S18000176 : Event
    {
        public S18000176()
        {
            this.EventID = 18000176;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}