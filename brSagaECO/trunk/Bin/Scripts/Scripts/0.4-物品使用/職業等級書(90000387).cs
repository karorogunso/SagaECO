using System;
using System.Collections.Generic;
using System.Text;
using SagaDB.Item;
using SagaDB.Actor;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
using SagaLib;
using System.Data;
using SagaScript.Chinese;
namespace SagaScript
{
    public class S90000387 : Event
    {
        public S90000387()
        {
            this.EventID = 90000387;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (pc.JobLevel1 < 50)
            {
                TakeItem(pc, 16014800,1);
                pc.JobLevel1 += 1;
                Say(pc, 0, "提升了 1次職 職業等級");
                return;
            }
            if (pc.JobLevel2X < 50)
            {
                TakeItem(pc, 16014800, 1);
                pc.JobLevel2X += 1;
                Say(pc, 0, "提升了 2-1次職 職業等級");
                return;
            }
            if (pc.JobLevel2T < 50)
            {
                TakeItem(pc, 16014800, 1);
                pc.JobLevel2T += 1;
                Say(pc, 0, "提升了 2-2次職 職業等級");
                return;
            }
            Say(pc, 0, "JOB等級已滿");
            return;
        }
    }
}