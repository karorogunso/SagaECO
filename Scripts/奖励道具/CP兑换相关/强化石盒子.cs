
using System;
using System.Collections.Generic;
using System.Text;
using SagaLib;
using SagaDB.Actor;
using SagaDB.Item;
using SagaMap.Scripting;
using SagaScript.Chinese.Enums;
namespace SagaScript.M30210000
{
    public class S910000036 : Event
    {
        public S910000036()
        {
            this.EventID = 910000036;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000036) >= 1)
            {
                TakeItem(pc, 910000036, 1);
                奖励(pc);
            }
        }
        void 奖励(ActorPC pc)
        {
            int rate = Global.Random.Next(0, 100);
            if (rate < 85)
                GiveItem(pc, 940000000, 1);
            else if (rate < 97)
                GiveItem(pc, 940000001, 1);
            else
                GiveItem(pc, 940000002, 1);
        }
    }
}

