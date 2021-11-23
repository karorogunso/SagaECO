using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:狩獵女神(13000131) X:72 Y:136
namespace SagaScript.M10071000
{
    public class S13000131 : Event
    {
        public S13000131()
        {
            this.EventID = 13000131;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}