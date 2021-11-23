
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
    public class S60000016: Event
    {
        public S60000016()
        {
            this.EventID = 60000016;
        }

        public override void OnEvent(ActorPC pc)
        {
            Say(pc, 0, "什么？你都出来了，还想要回去？", "2梦");
            Wait(pc, 500);
            Say(pc, 0, "…", "2梦");
            Say(pc,0,"嘛，过去吧", "2梦");
            Warp(pc, 10054000, 153, 161);
        }
    }
}