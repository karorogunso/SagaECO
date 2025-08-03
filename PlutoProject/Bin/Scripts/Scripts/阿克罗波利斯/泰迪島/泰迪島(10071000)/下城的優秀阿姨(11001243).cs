using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:下城的優秀阿姨(11001243) X:231 Y:105
namespace SagaScript.M10071000
{
    public class S11001243 : Event
    {
        public S11001243()
        {
            this.EventID = 11001243;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}