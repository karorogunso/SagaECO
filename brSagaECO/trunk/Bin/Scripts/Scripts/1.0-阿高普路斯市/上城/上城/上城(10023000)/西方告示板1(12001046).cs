using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:西方告示板1(12001046) X:71 Y:129
namespace SagaScript.M10023000
{
    public class S12001046 : Event
    {
        public S12001046()
        {
            this.EventID = 12001046;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
