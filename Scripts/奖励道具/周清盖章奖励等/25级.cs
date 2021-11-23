
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
    public class S910000125 : Event
    {
        public S910000125()
        {
            this.EventID = 910000125;
        }

        public override void OnEvent(ActorPC pc)
        {
            if (CountItem(pc, 910000125) > 0)
            {
                GiveItem(pc, 950000000, 1);//发型
                GiveItem(pc, 950000001, 1);//脸型
                GiveItem(pc, 950000025, 6);//KUJI
                GiveItem(pc, 951000000, 20);//雷池
                TakeItem(pc, 910000125, 1);
            }
        }
    }
}

