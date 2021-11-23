using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M11053000
{
    public class S82000000 : Event
    {
        public S82000000()
        {
            this.EventID = 82000000;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "啊……$R这里可真是无聊透了", "萝蕾拉");
            switch(Select(pc,"怎么办呢？","","打开任务控制台","离开"))
            {
                case 1:
                    HandleQuest(pc, 8);
                    break;
            }
        }
    }
}


