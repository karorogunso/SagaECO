using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:裁縫阿姨(11001238) X:133 Y:219
namespace SagaScript.M10071000
{
    public class S11001238 : Event
    {
        public S11001238()
        {
            this.EventID = 11001238;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}