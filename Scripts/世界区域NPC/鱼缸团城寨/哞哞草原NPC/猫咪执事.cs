
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
    public class S80000610: Event
    {
        public S80000610()
        {
            this.EventID = 80000610;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "喵？", "猫咪执事");
        }
    }
}