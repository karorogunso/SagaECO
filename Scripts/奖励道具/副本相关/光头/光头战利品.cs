
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
    public class S910000038 : Event
    {
        public S910000038()
        {
            this.EventID = 910000038;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000038) >= 1)
            {
                TakeItem(pc, 910000038, 1);
                奖励(pc);
            }
        }
        void 奖励(ActorPC pc)
        {
            int g = SagaLib.Global.Random.Next(250000, 500000);
            pc.Gold += g;

        }
    }
}

