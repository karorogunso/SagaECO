using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:熊次郎(13000129) X:74 Y:136
namespace SagaScript.M10071000
{
    public class S13000129 : Event
    {
        public S13000129()
        {
            this.EventID = 13000129;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}