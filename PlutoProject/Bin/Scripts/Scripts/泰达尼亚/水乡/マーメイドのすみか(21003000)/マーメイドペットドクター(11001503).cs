using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21003000
{
    public class S11001503 : Event
    {
        public S11001503()
        {
            this.EventID = 11001503;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "嗯嗯……除了人的问题。$R;", "美人鱼宠物医生ー");
        }


    }
}


