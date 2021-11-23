
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
    public class S80000605: Event
    {
        public S80000605()
        {
            this.EventID = 80000605;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "请过吧。", "年轻的守卫");
            Say(pc, 0, "最近突然多了很多危险的魔物，$R还请多加小心。", "年轻的守卫");
        }
    }
}