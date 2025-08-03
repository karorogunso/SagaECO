using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:(13000211) X:20 Y:153
namespace SagaScript.M10071000
{
    public class S13000211 : Event
    {
        public S13000211()
        {
            this.EventID = 13000211;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}