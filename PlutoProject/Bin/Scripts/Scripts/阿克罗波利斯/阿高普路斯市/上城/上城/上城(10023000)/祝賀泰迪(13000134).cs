using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:祝賀泰迪(13000134) X:126 Y:109
namespace SagaScript.M10023000
{
    public class S13000134 : Event
    {
        public S13000134()
        {
            this.EventID = 13000134;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
