
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
    public class S910000127 : Event
    {
        public S910000127()
        {
            this.EventID = 910000127;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000127) > 0)
            {
                GiveItem(pc, 950000027, 1);//S突破
                GiveItem(pc, 910000116, 1);//CP1000
                GiveItem(pc, 950000025, 10);//KUJI
                GiveItem(pc, 951000000, 30);//雷池
                TakeItem(pc, 910000127, 1);
            }
        }
    }
}

