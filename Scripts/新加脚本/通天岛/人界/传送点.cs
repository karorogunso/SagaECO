
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
    public class S10001543 : Event
    {
        public S10001543()
        {
            this.EventID = 10001543;
        }

        public override void OnEvent(ActorPC pc)
        {
            switch(Select(pc,"要去哪里呢？","", "鱼缸岛","什么也不做"))
            {
                case 1:
                    Warp(pc, 10054000, 206, 172);
                    break;
                case 2:

                    break;
            }
        }
    }
}