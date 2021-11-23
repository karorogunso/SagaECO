using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M31304000
{
    public class S11001726 : Event
    {
        public S11001726()
        {
            this.EventID = 11001726;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "嘩～？？$R;", "温泉に入っているおじいさん");
            /*
            Say(pc, 131, "ほわ～？？$R;", "温泉に入っているおじいさん");
            */
            //白 任务
        }


    }
}


