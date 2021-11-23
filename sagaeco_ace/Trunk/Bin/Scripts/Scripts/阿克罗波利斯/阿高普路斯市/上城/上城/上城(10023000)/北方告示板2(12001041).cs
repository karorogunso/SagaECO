using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:北方告示板2(12001041) X:131 Y:71
namespace SagaScript.M10023000
{
    public class S12001041 : Event
    {
        public S12001041()
        {
            this.EventID = 12001041;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
