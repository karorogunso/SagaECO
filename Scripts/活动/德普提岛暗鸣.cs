
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
    public class S80000805: Event
    {
        public S80000805()
        {
            this.EventID = 80000805;
        }

        public override void OnEvent(ActorPC pc)
        {
            ChangeMessageBox(pc);
            Say(pc, 0, "姆哈哈哈哈，你们亲爱的团长怎么可能会错过这种盛大的活动呢？", "暗鸣");
        }
    }
}