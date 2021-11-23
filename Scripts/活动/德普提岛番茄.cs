
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
    public class S80000803: Event
    {
        public S80000803()
        {
            this.EventID = 80000803;
        }

        public override void OnEvent(ActorPC pc)
        {
            ChangeMessageBox(pc);
            Say(pc, 0, "老夏！！不要，不要离开我……", "番茄茄");
        }
    }
}