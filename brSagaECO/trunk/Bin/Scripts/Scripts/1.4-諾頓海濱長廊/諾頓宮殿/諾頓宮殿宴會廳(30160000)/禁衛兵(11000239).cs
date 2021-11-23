using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30160000
{
    public class S11000239 : Event
    {
        public S11000239()
        {
            this.EventID = 11000239;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 131, "我們只能進入女王陛下的禮賓室$R;" +
                "$R其他地方都不能進去的$R;");
        }
    }
}