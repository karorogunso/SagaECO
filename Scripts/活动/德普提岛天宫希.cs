
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using WeeklyExploration;
using System.Globalization;
namespace SagaScript.M30210000
{
    public class S80000809: Event
    {
        public S80000809()
        {
            this.EventID = 80000809;
        }

        public override void OnEvent(ActorPC pc)
        {
            ChangeMessageBox(pc);
            Say(pc, 0, "挺热闹的嘛，嗯……不错。", "天宫希");
        }
    }
}