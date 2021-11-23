
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
    public class S10001548 : Event
    {
        public S10001548()
        {
            this.EventID = 10001548;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch(Select(pc,"要坐船前往哪里呢？","", "通天塔之岛", "什么也不做"))
            {
                case 1:
                    Warp(pc, 11058000, 128, 245);
                    break;
            }
        }
    }
}