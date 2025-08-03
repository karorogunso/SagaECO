using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M10065000
{
    public class S20010031 : Event
    {
        public S20010031()
        {
            this.EventID = 20010031;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 20010031, 131, "这里禁止进入$R;", "男僧兵");
        }
    }
}