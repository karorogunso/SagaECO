using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:軍團成員招募告示板2(12001054) X:76 Y:169
namespace SagaScript.M10023000
{
    public class S12001054 : Event
    {
        public S12001054()
        {
            this.EventID = 12001054;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
