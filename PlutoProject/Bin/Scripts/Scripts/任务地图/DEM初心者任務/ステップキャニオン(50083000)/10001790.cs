using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
using SagaScript.Chinese.Enums;
namespace SagaScript.M50083000
{
    public class S10001790 : Event
    {
        public S10001790()
        {
            this.EventID = 10001790;
        }

        public override void OnEvent(ActorPC pc)
        {
            int oldMap = pc.CInt["Beginner_Map"];           
            DeleteMapInstance(oldMap);

            Warp(pc, 50084000, 41, 252);
        }
    }
}