using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:北方告示板1(12001040) X:124 Y:71
namespace SagaScript.M10023000
{
    public class S12001040 : Event
    {
        public S12001040()
        {
            this.EventID = 12001040;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
