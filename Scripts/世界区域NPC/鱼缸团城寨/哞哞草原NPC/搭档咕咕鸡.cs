
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
    public class S80000607: Event
    {
        public S80000607()
        {
            this.EventID = 80000607;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "咕咕？咕咕咕，咕咕咕咕咕咕。", "搭档咕咕鸡");
        }
    }
}