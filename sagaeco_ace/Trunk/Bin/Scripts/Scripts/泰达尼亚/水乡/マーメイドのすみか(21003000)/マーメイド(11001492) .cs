using System;
using System.Collections.Generic;
using System.Text;

using SagaDB.Actor;
using SagaMap.Scripting;

using SagaLib;
namespace SagaScript.M21003000
{
    public class S11001492 : Event
    {
        public S11001492()
        {
            this.EventID = 11001492;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "自从你们出现以后$R;" +
           "塔的那边就开始不安宁了。$R;", "美人鱼");
        }


    }
}


