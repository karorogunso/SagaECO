using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:軍團成員招募告示板1(12001053) X:70 Y:169
namespace SagaScript.M10023000
{
    public class S12001053 : Event
    {
        public S12001053()
        {
            this.EventID = 12001053;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
