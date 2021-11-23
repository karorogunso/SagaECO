
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
    public class S80000810: Event
    {
        public S80000810()
        {
            this.EventID = 80000810;
        }

        public override void OnEvent(ActorPC pc)
        {
            ChangeMessageBox(pc);
            Say(pc, 0, "（这人好像累得睡着了，不要去打扰他吧。）", pc.Name);
        }
    }
}