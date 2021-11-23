
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
    public class S80000604: Event
    {
        public S80000604()
        {
            this.EventID = 80000604;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "……姐，不要哭了，$R我相信小妹一定会没事的。", "有些男孩子气的少女");
        }
    }
}