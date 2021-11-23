using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:埃米爾(13000063) X:127 Y:138
namespace SagaScript.M10023000
{
    public class S13000063 : Event
    {
        public S13000063()
        {
            this.EventID = 13000063;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
