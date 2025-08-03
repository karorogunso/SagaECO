using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10056000
{
    public class S12001055 : Event
    {
        public S12001055()
        {
            this.EventID = 12001055;
        }

        public override void OnEvent(ActorPC pc)
        {
            OpenBBS(pc, 7, 0);

        }
    }
}