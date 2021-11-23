
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
    public class S10000941: Event
    {
        public S10000941()
        {
            this.EventID = 10000941;
        }

        public override void OnEvent(ActorPC pc)
        {
                Warp(pc, 10057002, 172, 84);
        }
    }
}