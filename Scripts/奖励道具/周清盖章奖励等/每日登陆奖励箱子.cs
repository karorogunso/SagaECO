
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
    public class S910000002 : Event
    {
        public S910000002()
        {
            this.EventID = 910000002;
        }

        public override void OnEvent(ActorPC pc)
        {
            if(CountItem(pc, 910000002) > 0)
            {
                PlaySound(pc, 2040, false, 100, 50);
GiveItem(pc, 950000025, 1);
GiveItem(pc, 951000000, 1);
                GiveItem(pc, (uint)Global.Random.Next(910000016, 910000019), 1);
                TakeItem(pc, 910000002, 1);
            }
        }
    }
}

