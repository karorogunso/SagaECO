using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;
//所在地圖:上城(10023000) NPC基本信息:西方告示板2(12001047) X:71 Y:126
namespace SagaScript.M10023000
{
    public class S12001047 : Event
    {
        public S12001047()
        {
            this.EventID = 12001047;
        }

        public override void OnEvent(ActorPC pc)
        {

        }
    }
}
