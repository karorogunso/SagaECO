using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:泰迪島(10071000) NPC基本信息:古老大鐘 (佈魯)(18000174) X:116 Y:79
namespace SagaScript.M10071000
{
    public class S18000174 : Event
    {
        public S18000174()
        {
            this.EventID = 18000174;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}