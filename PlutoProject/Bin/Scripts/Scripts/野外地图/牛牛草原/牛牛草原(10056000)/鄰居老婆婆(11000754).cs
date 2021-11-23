using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10056000
{
    public class S11000754 : Event
    {
        public S11000754()
        {
            this.EventID = 11000754;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "这个桥，到底什么时候能完工呢？$R;");
        }
    }
}