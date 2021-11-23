
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
    public class S10001547 : Event
    {
        public S10001547()
        {
            this.EventID = 10001547;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch(Select(pc,"要坐船前往哪里呢？","", "沃特雷亚", "ECO城遗迹(尚未开放)","什么也不做"))
            {
                case 1:
                    Warp(pc, 11053000, 24, 231);
                    break;
                case 2:

                    break;
            }
        }
    }
}