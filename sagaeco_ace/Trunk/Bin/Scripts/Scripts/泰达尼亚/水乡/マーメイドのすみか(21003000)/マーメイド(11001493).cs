using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21003000
{
    public class S11001493 : Event
    {
        public S11001493()
        {
            this.EventID = 11001493;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 133, "用双脚走路是什么感觉呢ー？$R;", "美人鱼");

        }


    }
}


